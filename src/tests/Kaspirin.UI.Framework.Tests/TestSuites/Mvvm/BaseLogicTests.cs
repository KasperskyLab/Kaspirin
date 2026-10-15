// Copyright © 2026 AO Kaspersky Lab.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Kaspirin.UI.Framework.Tests.TestHelpers;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

[TestClass]
public sealed class BaseLogicTests
{
    [TestInitialize]
    public void InitializeTests()
    {
        var container = new RapidUnityContainer();
        container.BuildFrameworkServices();

        _originContainer = ServiceLocator.ExchangeContainer(container);
    }

    [TestCleanup]
    public void CleanupTests()
        => ServiceLocator.ExchangeContainer(_originContainer);

    [TestMethod]
    public void PushTask_Action_RunsTheAction()
    {
        var logic = new TestLogic();
        using var done = new ManualResetEventSlim(false);

        logic.PushTask(() => done.Set());

        Assert.IsTrue(done.Wait(_waitTimeout), "PushTask must run the action.");

        GC.KeepAlive(logic);
    }

    [TestMethod]
    public void PushTask_Func_ResultFlowsToTypedContinuation()
    {
        var logic = new TestLogic();
        using var done = new ManualResetEventSlim(false);
        var observed = 0;

        var task = logic.PushTask(() => 42);
        task.ContinueWith(result =>
        {
            observed = result;
            done.Set();
        });

        Assert.IsTrue(done.Wait(_waitTimeout), "Continuation did not run.");
        Assert.AreEqual(42, observed, "The antecedent result must flow to the continuation.");

        GC.KeepAlive(logic);
    }

    [TestMethod]
    public void ContinueWith_RunsContinuation()
    {
        var logic = new TestLogic();
        using var done = new ManualResetEventSlim(false);

        var task = logic.PushTask(() => { });
        task.ContinueWith(() => done.Set());

        Assert.IsTrue(done.Wait(_waitTimeout), "ContinueWith must schedule and run the continuation.");

        GC.KeepAlive(logic);
    }

    [TestMethod]
    public void ContinueWith_Throws_WhenOwnerCollected()
    {
        var (task, ownerRef) = CreateTaskWithCollectibleOwner();

        Assert.IsTrue(TestGCOperations.TryCollect(ownerRef), "Owner was not collected: the task must reference it weakly.");

        Assert.ThrowsException<InvalidOperationException>(() => task.ContinueWith(() => { }));
    }

    [TestMethod]
    public void ContinueWith_WhenAntecedentFaulted_DoesNotRunContinuation()
    {
        var logic = new TestLogic();
        var continuationRan = 0;

        var antecedent = logic.PushTask(() => throw new InvalidOperationException("boom"));
        var continuation = antecedent.ContinueWith(() => Interlocked.Exchange(ref continuationRan, 1));

        Assert.IsTrue(WaitForCompletion(continuation, _waitTimeout), "Continuation did not complete.");
        Assert.AreEqual(TaskStatus.Faulted, continuation.Status, "The continuation must fault when the antecedent faulted.");
        Assert.AreEqual(0, Volatile.Read(ref continuationRan), "The continuation must not run after a faulted antecedent.");

        GC.KeepAlive(logic);
    }

    [TestMethod]
    public void ContinueWith_WhenAntecedentCanceled_DoesNotRunContinuation()
    {
        var logic = new TestLogic();

        using var blockerStarted = new ManualResetEventSlim(false);
        using var releaseBlocker = new ManualResetEventSlim(false);
        var continuationRan = 0;

        logic.PushTask(() =>
        {
            blockerStarted.Set();
            releaseBlocker.Wait(_waitTimeout);
        });

        Assert.IsTrue(blockerStarted.Wait(_waitTimeout), "Blocking task did not start.");

        var antecedent = logic.PushTask(() => { });
        var continuation = antecedent.ContinueWith(() => Interlocked.Exchange(ref continuationRan, 1));
        antecedent.Cancel();

        releaseBlocker.Set();

        Assert.IsTrue(WaitForCompletion(continuation, _waitTimeout), "Continuation did not complete.");
        Assert.AreEqual(0, Volatile.Read(ref continuationRan), "The continuation must not run after a canceled antecedent.");

        GC.KeepAlive(logic);
    }

    [TestMethod]
    public void Cancel_PreventsQueuedTaskFromRunning()
    {
        var logic = new TestLogic();

        using var blockerStarted = new ManualResetEventSlim(false);
        using var releaseBlocker = new ManualResetEventSlim(false);
        var secondTaskRan = 0;

        logic.PushTask(() =>
        {
            blockerStarted.Set();
            releaseBlocker.Wait(_waitTimeout);
        });

        Assert.IsTrue(blockerStarted.Wait(_waitTimeout), "Blocking task did not start.");

        var secondTask = logic.PushTask(() => Interlocked.Exchange(ref secondTaskRan, 1));
        secondTask.Cancel();

        releaseBlocker.Set();

        Thread.Sleep(_silenceWindow);

        Assert.AreEqual(0, Volatile.Read(ref secondTaskRan), "A cancelled task must not run.");

        GC.KeepAlive(logic);
    }

    [TestMethod]
    public void Dispose_InvokesProtectedDispose()
    {
        var logic = new TestLogic();

        Assert.IsFalse(logic.DisposeCalled);

        ((IDisposable)logic).Dispose();

        Assert.IsTrue(logic.DisposeCalled, "IDisposable.Dispose must invoke the protected Dispose(bool).");
    }

    [TestMethod]
    public void CreateTimer_ReturnsThreadPoolServiceTimerThatTicks()
    {
        var logic = new TestLogic();
        using var ticked = new ManualResetEventSlim(false);

        using var timer = logic.CreateTimerForTest(() => ticked.Set(), TimerIntervals.Short);
        timer.Start();

        Assert.IsTrue(ticked.Wait(_waitTimeout), "Timer created by BaseLogic did not tick.");
        Assert.AreEqual(
            TimeSpan.FromMilliseconds((int)TimerIntervals.Short),
            timer.Interval,
            "The timer interval must be resolved from the TimerIntervals value.");

        GC.KeepAlive(logic);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static (BaseLogicTask Task, WeakReference OwnerRef) CreateTaskWithCollectibleOwner()
    {
        var logic = new TestLogic();
        var task = logic.PushTask(() => { });

        return (task, new WeakReference(logic));
    }

    private static bool WaitForCompletion(BaseLogicTask task, TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            var status = task.Status;
            if (status is TaskStatus.RanToCompletion or TaskStatus.Faulted or TaskStatus.Canceled)
            {
                return true;
            }

            Thread.Sleep(10);
        }

        return false;
    }

    private static readonly TimeSpan _waitTimeout = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan _silenceWindow = TimeSpan.FromMilliseconds(400);

    private IUnityContainer? _originContainer;
}
