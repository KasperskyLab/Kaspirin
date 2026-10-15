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

#pragma warning disable KAS001 // Lambda usage in Timer

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Kaspirin.UI.Framework.Tests.TestHelpers;
using ITimer = Kaspirin.UI.Framework.Threading.Timers.ITimer;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

public abstract class BaseServiceTimerTests
{
    protected abstract ITimer CreateServiceTimer(object owner, Action tickAction, MutableParameter<TimeSpan> interval);

    [TestMethod]
    public void Start_InvokesTickAction_WhileOwnerIsAlive()
    {
        var owner = new object();
        var counter = new TickCounter();
        var interval = CreateInterval(ShortInterval);

        using var timer = CreateServiceTimer(owner, counter.OnTick, interval.Parameter);
        timer.Start();

        Assert.IsTrue(counter.WaitForCount(3, WaitTimeout), $"Expected periodic ticks while the owner is alive, got {counter.Count}.");

        GC.KeepAlive(owner);
    }

    [TestMethod]
    public void Interval_Getter_ReflectsConfiguredValue()
    {
        var owner = new object();
        var interval = CreateInterval(TimeSpan.FromSeconds(2));

        using var timer = CreateServiceTimer(owner, OnIgnoredTick, interval.Parameter);

        Assert.AreEqual(TimeSpan.FromSeconds(2), timer.Interval);

        GC.KeepAlive(owner);
    }

    [TestMethod]
    public void Interval_Setter_Throws()
    {
        var owner = new object();
        var interval = CreateInterval(ShortInterval);

        using var timer = CreateServiceTimer(owner, OnIgnoredTick, interval.Parameter);

        Assert.ThrowsException<InvalidOperationException>(
            () => timer.Interval = TimeSpan.FromSeconds(1),
            "A service timer must not allow changing its interval directly.");

        GC.KeepAlive(owner);
    }

    [TestMethod]
    public void Interval_ChangedThroughParameter_IsAppliedToTimer()
    {
        var owner = new object();
        var interval = CreateInterval(TimeSpan.FromSeconds(1));

        using var timer = CreateServiceTimer(owner, OnIgnoredTick, interval.Parameter);

        interval.ChangeTo(TimeSpan.FromSeconds(3));

        Assert.AreEqual(
            TimeSpan.FromSeconds(3),
            timer.Interval,
            "Changing the interval parameter must propagate to the timer interval.");

        GC.KeepAlive(owner);
    }

    [TestMethod]
    public void StartAndStop_ForwardToInnerTimerState()
    {
        var owner = new object();
        var interval = CreateInterval(FarFuture);

        using var timer = CreateServiceTimer(owner, OnIgnoredTick, interval.Parameter);

        Assert.AreEqual(TimerState.NeverRun, timer.State);
        Assert.IsFalse(timer.IsRunning);

        Assert.IsTrue(timer.Start(delay: FarFuture), "Start must succeed.");
        Assert.IsTrue(timer.IsRunning);
        Assert.AreEqual(TimerState.Running, timer.State);

        Assert.IsTrue(timer.Stop(), "Stop must succeed.");
        Assert.IsFalse(timer.IsRunning);
        Assert.AreEqual(TimerState.Stopped, timer.State);

        GC.KeepAlive(owner);
    }

    [TestMethod]
    public void StateChanged_IsForwardedFromInnerTimer()
    {
        var owner = new object();
        var interval = CreateInterval(FarFuture);

        using var timer = CreateServiceTimer(owner, OnIgnoredTick, interval.Parameter);

        var stateChanges = new TickCounter();
        Action<ITimer> handler = stateChanges.OnTick;
        timer.StateChanged += handler;

        timer.Start(delay: FarFuture);

        Assert.IsTrue(stateChanges.WaitForCount(1, WaitTimeout), "StateChanged must be forwarded from the inner timer.");

        GC.KeepAlive(owner);
        GC.KeepAlive(handler);
    }

    [TestMethod]
    public void Dispose_StopsTicking()
    {
        var owner = new object();
        var counter = new TickCounter();
        var interval = CreateInterval(ShortInterval);

        var timer = CreateServiceTimer(owner, counter.OnTick, interval.Parameter);

        timer.Start();
        Assert.IsTrue(counter.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");

        timer.Dispose();
        Assert.AreEqual(TimerState.Disposed, timer.State);
        Assert.IsTrue(timer.TickExecutionEvent.WaitOne(WaitTimeout), "In-flight tick did not complete.");

        var countAfterDispose = counter.Count;
        Thread.Sleep(SilenceWindow);

        Assert.AreEqual(countAfterDispose, counter.Count, "A disposed service timer must not tick.");

        GC.KeepAlive(owner);
    }

    [TestMethod]
    public void AfterOwnerCollected_DisposesTimerAndStopsTicking()
    {
        var counter = new TickCounter();
        var interval = CreateInterval(ShortInterval);

        var (timer, ownerRef) = StartTimerWithCollectibleOwner(counter, interval.Parameter);
        using (timer)
        {
            Assert.IsTrue(counter.WaitForCount(1, WaitTimeout), "Timer did not tick while the owner was alive.");

            Assert.IsTrue(
                TestGCOperations.TryCollect(ownerRef),
                "Owner was not collected: the service timer must reference its owner weakly.");

            Assert.IsTrue(
                WaitForState(timer, TimerState.Disposed, WaitTimeout),
                "A service timer must dispose itself once its owner has been collected.");

            var countAfterCollection = counter.Count;
            Thread.Sleep(SilenceWindow);

            Assert.AreEqual(
                countAfterCollection,
                counter.Count,
                "A service timer must not invoke the tick action after its owner is gone.");
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private (ITimer Timer, WeakReference OwnerRef) StartTimerWithCollectibleOwner(TickCounter counter, MutableParameter<TimeSpan> interval)
    {
        var owner = new object();

        var timer = CreateServiceTimer(owner, counter.OnTick, interval);
        timer.Start();

        return (timer, new WeakReference(owner));
    }

    private static void OnIgnoredTick()
    {
    }

    protected static IntervalSource CreateInterval(TimeSpan interval)
    {
        var storage = new IntervalStorage(interval);
        var parameter = new MutableParameter<TimeSpan>(storage, IntervalId.Value);

        return new IntervalSource(parameter, storage);
    }

    private static bool WaitForState(ITimer timer, TimerState state, TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;

        while (timer.State != state && DateTime.UtcNow < deadline)
        {
            Thread.Sleep(10);
        }

        return timer.State == state;
    }

    private enum IntervalId
    {
        Value,
    }

    protected sealed class IntervalSource
    {
        public IntervalSource(MutableParameter<TimeSpan> parameter, IntervalStorage storage)
        {
            Parameter = parameter;
            _storage = storage;
        }

        public MutableParameter<TimeSpan> Parameter { get; }

        public void ChangeTo(TimeSpan value)
            => _storage.Change(value);

        private readonly IntervalStorage _storage;
    }

    protected sealed class IntervalStorage : IMutableParameterStorage
    {
        public IntervalStorage(TimeSpan value)
        {
            _value = value;
        }

        public event Action<Enum> ParameterChanged = _ => { };

        public void Change(TimeSpan value)
        {
            _value = value;

            ParameterChanged.Invoke(IntervalId.Value);
        }

        public object GetParameter(Enum parameterId)
            => _value;

        private TimeSpan _value;
    }

    protected sealed class TickCounter
    {
        public int Count => Volatile.Read(ref _count);

        public void OnTick()
        {
            Interlocked.Increment(ref _count);
            _changed.Set();
        }

        public void OnTick(ITimer _)
            => OnTick();

        public bool WaitForCount(int expectedCount, TimeSpan timeout)
        {
            var deadline = DateTime.UtcNow + timeout;

            while (Count < expectedCount)
            {
                var remaining = deadline - DateTime.UtcNow;
                if (remaining <= TimeSpan.Zero || !_changed.WaitOne(remaining))
                {
                    return Count >= expectedCount;
                }
            }

            return true;
        }

        private readonly AutoResetEvent _changed = new(false);

        private int _count;
    }

    protected static readonly TimeSpan ShortInterval = TimeSpan.FromMilliseconds(50);
    protected static readonly TimeSpan FarFuture = TimeSpan.FromHours(1);
    protected static readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(5);
    protected static readonly TimeSpan SilenceWindow = TimeSpan.FromMilliseconds(400);
}
