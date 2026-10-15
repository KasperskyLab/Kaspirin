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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Kaspirin.UI.Framework.Tests.TestHelpers;
using ITimer = Kaspirin.UI.Framework.Threading.Timers.ITimer;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

public abstract class BaseTimerTests
{
    protected abstract ITimer CreateTimer(
        Action? onTimer = null,
        TimeSpan? interval = null,
        TimerOptions options = TimerOptions.None);

    #region State machine

    [TestMethod]
    public void State_OfNewTimer_IsNeverRun()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: ShortInterval);

        Assert.AreEqual(TimerState.NeverRun, timer.State);
        Assert.IsFalse(timer.IsRunning);
    }

    [TestMethod]
    public void Start_OnNewTimer_ReturnsTrueAndRuns()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);

        var started = timer.Start(delay: FarFuture);

        Assert.IsTrue(started, "Start must succeed on a new timer.");
        Assert.IsTrue(timer.IsRunning);
        Assert.AreEqual(TimerState.Running, timer.State);
    }

    [TestMethod]
    public void Start_WhenAlreadyRunning_ReturnsFalse()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.Start(delay: FarFuture);

        var startedAgain = timer.Start(delay: FarFuture);

        Assert.IsFalse(startedAgain, "Start must fail when the timer is already running.");
        Assert.IsTrue(timer.IsRunning, "Failed Start must not stop a running timer.");
    }

    [TestMethod]
    public void Start_AfterStop_ReturnsTrue()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.Start(delay: FarFuture);
        timer.Stop();

        var restarted = timer.Start(delay: FarFuture);

        Assert.IsTrue(restarted, "A stopped timer must support starting again.");
        Assert.IsTrue(timer.IsRunning);
    }

    [TestMethod]
    public void Stop_WhenRunning_ReturnsTrueAndStops()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.Start(delay: FarFuture);

        var stopped = timer.Stop();

        Assert.IsTrue(stopped, "Stop must succeed on a running timer.");
        Assert.IsFalse(timer.IsRunning);
        Assert.AreEqual(TimerState.Stopped, timer.State);
    }

    [TestMethod]
    public void Stop_WhenNeverRun_ReturnsFalse()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);

        var stopped = timer.Stop();

        Assert.IsFalse(stopped, "Stop must fail on a timer that has never run.");
        Assert.AreEqual(TimerState.NeverRun, timer.State, "Failed Stop must not change the timer state.");
    }

    [TestMethod]
    public void Stop_WhenAlreadyStopped_ReturnsFalse()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.Start(delay: FarFuture);
        timer.Stop();

        var stoppedAgain = timer.Stop();

        Assert.IsFalse(stoppedAgain, "Stop must fail on an already stopped timer.");
    }

    [TestMethod]
    public void Restart_WhenRunning_ReturnsTrue()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.Start(delay: FarFuture);

        var restarted = timer.Restart(delay: FarFuture);

        Assert.IsTrue(restarted, "Restart must succeed on a running timer.");
        Assert.IsTrue(timer.IsRunning);
    }

    [TestMethod]
    public void Restart_WhenNotRunning_StartsTimer()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);

        var restarted = timer.Restart(delay: FarFuture);

        Assert.IsTrue(restarted, "Restart must start a timer that is not running.");
        Assert.IsTrue(timer.IsRunning);
    }

    [TestMethod]
    public void Dispose_ChangesStateToDisposed()
    {
        var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.Start(delay: FarFuture);

        timer.Dispose();

        Assert.AreEqual(TimerState.Disposed, timer.State);
        Assert.IsFalse(timer.IsRunning);

        timer.Dispose();

        Assert.AreEqual(TimerState.Disposed, timer.State);
    }

    [TestMethod]
    public void StartStopRestart_AfterDispose_ReturnFalse()
    {
        var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.Start(delay: FarFuture);
        timer.Dispose();

        Assert.IsFalse(timer.Start(), "Start must fail on a disposed timer.");
        Assert.IsFalse(timer.Restart(), "Restart must fail on a disposed timer.");
        Assert.IsFalse(timer.Stop(), "Stop must fail on a disposed timer.");
        Assert.AreEqual(TimerState.Disposed, timer.State);
    }

    #endregion

    #region Ticking

    [TestMethod]
    public void Start_InvokesTickActionAndTickEvent()
    {
        var probe = new TickProbe();
        var eventProbe = new TickProbe();
        ITimer? tickSender = null;

        Action onTimer = probe.OnTick;
        using var timer = CreateTimer(onTimer, interval: FarFuture);

        Action<ITimer> onTick = t =>
        {
            tickSender = t;
            eventProbe.OnTick();
        };
        timer.Tick += onTick;

        timer.Start();

        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Tick action was not invoked after Start.");
        Assert.IsTrue(eventProbe.WaitForCount(1, WaitTimeout), "Tick event was not raised after Start.");
        Assert.AreSame(timer, tickSender, "Tick event must pass the timer itself as the sender.");

        GC.KeepAlive(onTimer);
        GC.KeepAlive(onTick);
    }

    [TestMethod]
    public void Start_WithInterval_TicksPeriodically()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;
        using var timer = CreateTimer(onTimer, interval: ShortInterval);

        timer.Start();

        Assert.IsTrue(probe.WaitForCount(3, WaitTimeout), $"Expected at least 3 ticks, got {probe.Count}.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Start_WithoutInterval_TicksOnlyOnce()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;
        using var timer = CreateTimer(onTimer, interval: null);

        timer.Start();

        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Single-shot timer did not tick.");
        Assert.IsFalse(probe.WaitForCount(2, SilenceWindow), "Single-shot timer must not tick more than once.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Start_WithDelay_DefersFirstTick()
    {
        var delay = TimeSpan.FromMilliseconds(500);

        var probe = new TickProbe();
        Action onTimer = probe.OnTick;
        using var timer = CreateTimer(onTimer, interval: null);

        var stopwatch = Stopwatch.StartNew();
        timer.Start(delay);

        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Tick was not raised after the delay.");
        stopwatch.Stop();

        Assert.IsTrue(
            stopwatch.ElapsedMilliseconds >= delay.TotalMilliseconds - 100,
            $"Tick fired after {stopwatch.ElapsedMilliseconds} ms, expected the {delay.TotalMilliseconds} ms delay to be respected.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Restart_WhileWaitingForDelayedTick_OverridesDelay()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;
        using var timer = CreateTimer(onTimer, interval: ShortInterval);

        timer.Start(delay: FarFuture);
        var restarted = timer.Restart();

        Assert.IsTrue(restarted, "Restart must succeed on a running timer.");
        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Tick was not raised after Restart without delay.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Stop_PreventsFurtherTicks()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;
        using var timer = CreateTimer(onTimer, interval: ShortInterval);

        timer.Start();
        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");

        timer.Stop();
        Assert.IsTrue(timer.TickExecutionEvent.WaitOne(WaitTimeout), "In-flight tick did not complete.");

        var countAfterStop = probe.Count;
        Thread.Sleep(SilenceWindow);

        Assert.AreEqual(countAfterStop, probe.Count, "Timer must not tick after being stopped.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Dispose_PreventsFurtherTicks()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;
        var timer = CreateTimer(onTimer, interval: ShortInterval);

        timer.Start();
        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");

        timer.Dispose();
        Assert.IsTrue(timer.TickExecutionEvent.WaitOne(WaitTimeout), "In-flight tick did not complete.");

        var countAfterDispose = probe.Count;
        Thread.Sleep(SilenceWindow);

        Assert.AreEqual(countAfterDispose, probe.Count, "Timer must not tick after being disposed.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Tick_WhenActionThrows_TimerContinuesTicking()
    {
        var probe = new TickProbe();
        var thrown = 0;

        Action onTimer = () =>
        {
            if (Interlocked.Exchange(ref thrown, 1) == 0)
            {
                throw new InvalidOperationException("Test exception from tick action.");
            }

            probe.OnTick();
        };

        using var timer = CreateTimer(onTimer, interval: ShortInterval);

        timer.Start();

        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer stopped ticking after an exception in the tick action.");
        Assert.AreEqual(1, thrown, "The exception must have been thrown exactly once.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Tick_WhenActionThrows_TickEventIsStillRaised()
    {
        var eventProbe = new TickProbe();

        Action onTimer = () => throw new InvalidOperationException("Test exception from tick action.");
        Action<ITimer> onTick = eventProbe.OnTick;

        using var timer = CreateTimer(onTimer, interval: ShortInterval);
        timer.Tick += onTick;

        timer.Start();

        Assert.IsTrue(
            eventProbe.WaitForCount(2, WaitTimeout),
            "Tick event subscribers must be notified even when the tick action throws.");

        GC.KeepAlive(onTimer);
        GC.KeepAlive(onTick);
    }

    [TestMethod]
    public void SingleShot_AfterTick_StopsAndRaisesStateChanged()
    {
        var probe = new TickProbe();
        var recorder = new StateChangeRecorder();

        Action onTimer = probe.OnTick;
        Action<ITimer> onStateChanged = recorder.OnStateChanged;

        using var timer = CreateTimer(onTimer, interval: null);
        timer.StateChanged += onStateChanged;

        timer.Start();

        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Single-shot timer did not tick.");
        Assert.IsTrue(recorder.WaitForState(TimerState.Stopped, WaitTimeout), "Single-shot timer must raise StateChanged when it auto-stops.");
        Assert.AreEqual(TimerState.Stopped, timer.State, "Single-shot timer must transition to Stopped after its tick.");
        Assert.IsFalse(timer.IsRunning);
        Assert.IsTrue(timer.Start(delay: FarFuture), "A completed single-shot timer must support starting again.");

        GC.KeepAlive(onTimer);
        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void SingleShot_RestartedFromTickHandler_KeepsRunning()
    {
        using var restarted = new ManualResetEvent(false);

        ITimer? timer = null;
        Action onTimer = () =>
        {
            timer!.Restart(delay: FarFuture);
            restarted.Set();
        };

        timer = CreateTimer(onTimer, interval: null);

        using (timer)
        {
            timer.Start();

            Assert.IsTrue(restarted.WaitOne(WaitTimeout), "Tick handler did not restart the timer.");
            Assert.IsTrue(timer.TickExecutionEvent.WaitOne(WaitTimeout), "Tick did not complete.");

            Assert.AreEqual(
                TimerState.Running,
                timer.State,
                "A single-shot timer restarted from its own tick handler must keep running.");
        }

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void SingleShot_TickHandlerObservesStoppedState()
    {
        using var observed = new ManualResetEvent(false);
        var stateDuringTick = default(TimerState);

        ITimer? timer = null;
        Action onTimer = () =>
        {
            stateDuringTick = timer!.State;
            observed.Set();
        };

        timer = CreateTimer(onTimer, interval: null);

        using (timer)
        {
            timer.Start();

            Assert.IsTrue(observed.WaitOne(WaitTimeout), "Single-shot timer did not tick.");
            Assert.AreEqual(
                TimerState.Stopped,
                stateDuringTick,
                "A single-shot timer must already be Stopped when its tick handler runs.");
        }

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Tick_AfterUnsubscribe_HandlerIsNotInvoked()
    {
        var probe = new TickProbe();
        Action<ITimer> handler = probe.OnTick;

        using var timer = CreateTimer(OnIgnoredTick, interval: ShortInterval);
        timer.Tick += handler;

        timer.Start();
        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Tick event subscriber did not receive ticks.");

        timer.Tick -= handler;
        Assert.IsTrue(timer.TickExecutionEvent.WaitOne(WaitTimeout), "In-flight tick did not complete.");

        var countAfterUnsubscribe = probe.Count;
        Thread.Sleep(SilenceWindow);

        Assert.AreEqual(countAfterUnsubscribe, probe.Count, "Handler must not be invoked after unsubscribing.");

        GC.KeepAlive(handler);
    }

    #endregion

    #region Interval

    [TestMethod]
    public void Interval_ReflectsConstructorAndSetterValues()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: TimeSpan.FromSeconds(1));

        Assert.AreEqual(TimeSpan.FromSeconds(1), timer.Interval);

        timer.Interval = TimeSpan.FromSeconds(2);
        Assert.AreEqual(TimeSpan.FromSeconds(2), timer.Interval);

        timer.Interval = null;
        Assert.IsNull(timer.Interval);
    }

    [TestMethod]
    public void Interval_ChangedWhileRunning_TakesEffect()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;
        using var timer = CreateTimer(onTimer, interval: FarFuture);

        timer.Start();
        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");

        timer.Interval = ShortInterval;

        Assert.IsTrue(probe.WaitForCount(4, WaitTimeout), $"Expected ticks at the new interval, got {probe.Count}.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Interval_SetToNullWhileRunning_StopsTimer()
    {
        var probe = new TickProbe();
        var recorder = new StateChangeRecorder();

        Action onTimer = probe.OnTick;
        Action<ITimer> onStateChanged = recorder.OnStateChanged;

        using var timer = CreateTimer(onTimer, interval: ShortInterval);
        timer.StateChanged += onStateChanged;

        timer.Start();
        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");

        timer.Interval = null;

        Assert.IsTrue(recorder.WaitForState(TimerState.Stopped, WaitTimeout), "Setting Interval to null on a running timer must stop it and raise StateChanged.");
        Assert.AreEqual(TimerState.Stopped, timer.State);
        Assert.IsNull(timer.Interval);

        Assert.IsTrue(timer.TickExecutionEvent.WaitOne(WaitTimeout), "In-flight tick did not complete.");

        var countAfterStop = probe.Count;
        Thread.Sleep(SilenceWindow);

        Assert.AreEqual(countAfterStop, probe.Count, "Timer must not tick after its interval was set to null.");

        GC.KeepAlive(onTimer);
        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void Interval_SetOnDisposedTimer_IsIgnored()
    {
        var timer = CreateTimer(OnIgnoredTick, interval: ShortInterval);
        timer.Dispose();

        timer.Interval = FarFuture;

        Assert.AreEqual(ShortInterval, timer.Interval, "Interval of a disposed timer must not change.");
    }

    #endregion

    #region Events

    [TestMethod]
    public void StartAndStop_RaiseStateChangedWithCurrentState()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);

        var recorder = new StateChangeRecorder();
        Action<ITimer> onStateChanged = recorder.OnStateChanged;
        timer.StateChanged += onStateChanged;

        timer.Start(delay: FarFuture);

        Assert.IsTrue(recorder.WaitForCount(1, WaitTimeout), "StateChanged was not raised on Start.");
        Assert.AreEqual(TimerState.Running, recorder.LastState, "StateChanged handler must observe the Running state after Start.");
        Assert.AreSame(timer, recorder.LastSender, "StateChanged must pass the timer itself as the sender.");

        timer.Stop();

        Assert.IsTrue(recorder.WaitForCount(2, WaitTimeout), "StateChanged was not raised on Stop.");
        Assert.AreEqual(TimerState.Stopped, recorder.LastState, "StateChanged handler must observe the Stopped state after Stop.");

        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void Start_WhenItFails_DoesNotRaiseStateChanged()
    {
        var recorder = new StateChangeRecorder();
        Action<ITimer> onStateChanged = recorder.OnStateChanged;

        var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.StateChanged += onStateChanged;

        timer.Start(delay: FarFuture);
        Assert.AreEqual(1, recorder.Count, "StateChanged must be raised on a successful Start.");

        timer.Start(delay: FarFuture);
        Assert.AreEqual(1, recorder.Count, "StateChanged must not be raised when Start fails on a running timer.");

        timer.Dispose();
        Assert.AreEqual(2, recorder.Count, "Disposing a running timer must raise StateChanged.");

        timer.Start();
        Assert.AreEqual(2, recorder.Count, "StateChanged must not be raised when Start fails on a disposed timer.");

        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void Stop_WhenItFails_DoesNotRaiseStateChanged()
    {
        var recorder = new StateChangeRecorder();
        Action<ITimer> onStateChanged = recorder.OnStateChanged;

        var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.StateChanged += onStateChanged;

        timer.Stop();
        Assert.AreEqual(0, recorder.Count, "StateChanged must not be raised when Stop fails on a never-run timer.");

        timer.Start(delay: FarFuture);
        timer.Stop();
        Assert.AreEqual(2, recorder.Count, "StateChanged must be raised on a successful Stop (after the one from Start).");

        timer.Stop();
        Assert.AreEqual(2, recorder.Count, "StateChanged must not be raised when Stop fails on an already stopped timer.");

        timer.Dispose();
        Assert.AreEqual(3, recorder.Count, "Disposing a stopped timer must raise StateChanged.");

        timer.Stop();
        Assert.AreEqual(3, recorder.Count, "StateChanged must not be raised when Stop fails on a disposed timer.");

        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void Dispose_RaisesStateChangedWithDisposedState()
    {
        var recorder = new StateChangeRecorder();
        Action<ITimer> onStateChanged = recorder.OnStateChanged;

        var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);
        timer.StateChanged += onStateChanged;

        timer.Start(delay: FarFuture);
        Assert.AreEqual(1, recorder.Count, "StateChanged must be raised on a successful Start.");

        timer.Dispose();

        Assert.AreEqual(2, recorder.Count, "Transition to the Disposed state must raise StateChanged exactly once.");
        Assert.AreEqual(TimerState.Disposed, recorder.LastState, "StateChanged handler must observe the Disposed state.");
        Assert.AreEqual(TimerState.Disposed, timer.State);

        timer.Dispose();
        Assert.AreEqual(2, recorder.Count, "A repeated Dispose must not raise StateChanged.");

        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void StateChanged_UnderConcurrentStartStop_HandlerAlwaysReadsAuthoritativeState()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: FarFuture);

        var sawUnexpectedState = 0;
        var unexpectedState = default(TimerState);

        Action<ITimer> onStateChanged = t =>
        {
            var observed = t.State;
            if (observed != TimerState.Running && observed != TimerState.Stopped)
            {
                if (Interlocked.Exchange(ref sawUnexpectedState, 1) == 0)
                {
                    unexpectedState = observed;
                }
            }
        };
        timer.StateChanged += onStateChanged;

        const int iterations = 2000;

        void Hammer()
        {
            for (var i = 0; i < iterations; i++)
            {
                timer.Start(delay: FarFuture);
                timer.Stop();
            }
        }

        var first = new Thread(Hammer);
        var second = new Thread(Hammer);

        first.Start();
        second.Start();

        Assert.IsTrue(first.Join(WaitTimeout), "Concurrent Start/Stop did not finish in time (possible deadlock).");
        Assert.IsTrue(second.Join(WaitTimeout), "Concurrent Start/Stop did not finish in time (possible deadlock).");

        Assert.AreEqual(
            0,
            Volatile.Read(ref sawUnexpectedState),
            $"StateChanged handler observed an unexpected state: {unexpectedState}.");

        Assert.IsTrue(
            timer.State is TimerState.Running or TimerState.Stopped,
            $"Final state must be a live state, was {timer.State}.");

        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void TickExecutionEvent_TracksTickExecution()
    {
        using var tickStarted = new ManualResetEvent(false);
        using var tickGate = new ManualResetEvent(false);

        Action onTimer = () =>
        {
            tickStarted.Set();
            tickGate.WaitOne(WaitTimeout);
        };

        using var timer = CreateTimer(onTimer, interval: null);

        Assert.IsTrue(timer.TickExecutionEvent.WaitOne(0), "TickExecutionEvent must be signaled before the first tick.");

        timer.Start();

        Assert.IsTrue(tickStarted.WaitOne(WaitTimeout), "Tick did not start.");
        Assert.IsFalse(timer.TickExecutionEvent.WaitOne(0), "TickExecutionEvent must be non-signaled while a tick is executing.");

        tickGate.Set();

        Assert.IsTrue(timer.TickExecutionEvent.WaitOne(WaitTimeout), "TickExecutionEvent must be signaled after the tick completes.");

        GC.KeepAlive(onTimer);
    }

    #endregion

    #region Empty timer and options

    [TestMethod]
    public void Start_WithoutTickActionAndSubscribers_ThrowsAndDisposesTimer()
    {
        var timer = CreateTimer(onTimer: null, interval: ShortInterval);

        Assert.ThrowsException<InvalidOperationException>(() => timer.Start());
        Assert.AreEqual(TimerState.Disposed, timer.State, "An empty timer must dispose itself on a start attempt.");
    }

    [TestMethod]
    public void Start_WhenItThrowsAndDisposes_RaisesStateChangedOnce()
    {
        var recorder = new StateChangeRecorder();
        Action<ITimer> onStateChanged = recorder.OnStateChanged;

        var timer = CreateTimer(onTimer: null, interval: ShortInterval);
        timer.StateChanged += onStateChanged;

        Assert.ThrowsException<InvalidOperationException>(() => timer.Start());

        Assert.AreEqual(TimerState.Disposed, timer.State);
        Assert.AreEqual(1, recorder.Count, "Disposing the empty timer on a failed Start must raise StateChanged exactly once.");
        Assert.AreEqual(TimerState.Disposed, recorder.LastState, "StateChanged handler must observe the Disposed state.");

        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void Start_WithStrongRefAndWithoutTickAction_Throws()
    {
        var timer = CreateTimer(onTimer: null, interval: ShortInterval, options: TimerOptions.StrongRef);

        Assert.ThrowsException<InvalidOperationException>(
            () => timer.Start(),
            "StrongRef does not relax the empty-timer rule; Start must fail without AllowEmpty.");
    }

    [TestMethod]
    public void Start_WithAllowEmptyAndWithoutTickAction_Succeeds()
    {
        using var timer = CreateTimer(onTimer: null, interval: ShortInterval, options: TimerOptions.AllowEmpty);

        var started = timer.Start();

        Assert.IsTrue(started, "Start must succeed when AllowEmpty is specified.");
        Assert.IsTrue(timer.IsRunning);
    }

    [TestMethod]
    public void Start_WithOnlyTickSubscriber_Succeeds()
    {
        var probe = new TickProbe();
        Action<ITimer> handler = probe.OnTick;

        using var timer = CreateTimer(onTimer: null, interval: ShortInterval);
        timer.Tick += handler;

        var started = timer.Start();

        Assert.IsTrue(started, "A timer with a Tick event subscriber is not empty and must start.");
        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Tick event subscriber did not receive ticks.");

        GC.KeepAlive(handler);
    }

    [TestMethod]
    public void Tag_StoresUserData()
    {
        using var timer = CreateTimer(OnIgnoredTick, interval: ShortInterval);

        Assert.IsNull(timer.Tag);

        var tag = new object();
        timer.Tag = tag;

        Assert.AreSame(tag, timer.Tag);
    }

    #endregion

    #region Weak and strong references

    [TestMethod]
    public void TickAction_HeldWeaklyByDefault_TimerDisposesAfterTargetIsCollected()
    {
        var probe = new TickProbe();

        var (timer, handlerTarget) = StartTimerWithCollectibleTickAction(probe, TimerOptions.None);
        using (timer)
        {
            Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick while the handler target was alive.");

            Assert.IsTrue(
                TestGCOperations.TryCollect(handlerTarget),
                "Handler target was not collected: the timer must not hold a strong reference to it by default.");

            Assert.IsTrue(
                WaitForState(timer, TimerState.Disposed, WaitTimeout),
                "Timer must dispose itself after its only tick action has been collected.");

            var countAfterCollection = probe.Count;
            Thread.Sleep(SilenceWindow);

            Assert.AreEqual(countAfterCollection, probe.Count, "Timer must not tick after the tick action has been collected.");
        }
    }

    [TestMethod]
    public void AutoDispose_OnTickActionCollected_RaisesStateChangedOnce()
    {
        var probe = new TickProbe();
        var recorder = new StateChangeRecorder();
        Action<ITimer> onStateChanged = recorder.OnStateChanged;

        var (timer, handlerTarget) = StartTimerWithCollectibleTickAction(probe, TimerOptions.None);
        using (timer)
        {
            timer.StateChanged += onStateChanged;

            Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick while the handler target was alive.");

            Assert.IsTrue(
                TestGCOperations.TryCollect(handlerTarget),
                "Handler target was not collected: the timer must not hold a strong reference to it by default.");

            Assert.IsTrue(
                recorder.WaitForState(TimerState.Disposed, WaitTimeout),
                "Auto-dispose after the tick action is collected must raise StateChanged(Disposed).");

            Thread.Sleep(SilenceWindow);

            Assert.AreEqual(1, recorder.Count, "Auto-dispose must raise StateChanged exactly once.");
        }

        GC.KeepAlive(onStateChanged);
    }

    [TestMethod]
    public void TickSubscriber_HeldWeaklyByDefault_TimerDisposesAfterTargetIsCollected()
    {
        var probe = new TickProbe();

        var (timer, handlerTarget) = StartTimerWithCollectibleTickSubscriber(probe);
        using (timer)
        {
            Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick while the subscriber target was alive.");

            Assert.IsTrue(
                TestGCOperations.TryCollect(handlerTarget),
                "Subscriber target was not collected: the timer must not hold a strong reference to it by default.");

            Assert.IsTrue(
                WaitForState(timer, TimerState.Disposed, WaitTimeout),
                "Timer must dispose itself after its only Tick subscriber has been collected.");
        }
    }

    [TestMethod]
    public void TickAction_WithStrongRefOption_SurvivesGarbageCollection()
    {
        var probe = new TickProbe();

        var (timer, handlerTarget) = StartTimerWithCollectibleTickAction(probe, TimerOptions.StrongRef);
        using (timer)
        {
            Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");

            TestGCOperations.Collect();

            Assert.IsTrue(handlerTarget.IsAlive, "A StrongRef timer must keep the tick action target alive.");

            var countBeforeWait = probe.Count;

            Assert.IsTrue(
                probe.WaitForCount(countBeforeWait + 2, WaitTimeout),
                "A StrongRef timer must keep ticking after garbage collection.");
        }
    }

    [TestMethod]
    public void TickAction_WeakWithAliveTarget_KeepsTicking()
    {
        var probe = new TickProbe();
        var handler = new CollectibleHandler(probe);

        using var timer = CreateTimer(handler.OnTick, interval: ShortInterval);
        timer.Start();

        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");

        TestGCOperations.Collect();

        var countBeforeWait = probe.Count;

        Assert.IsTrue(
            probe.WaitForCount(countBeforeWait + 2, WaitTimeout),
            "A weak timer must keep ticking while the handler target is alive.");

        GC.KeepAlive(handler);
    }

    #endregion

    #region Helpers

    private void OnIgnoredTick()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private (ITimer Timer, WeakReference HandlerTarget) StartTimerWithCollectibleTickAction(TickProbe probe, TimerOptions options)
    {
        var handler = new CollectibleHandler(probe);

        var timer = CreateTimer(handler.OnTick, interval: ShortInterval, options: options);
        timer.Start();

        return (timer, new WeakReference(handler));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private (ITimer Timer, WeakReference HandlerTarget) StartTimerWithCollectibleTickSubscriber(TickProbe probe)
    {
        var handler = new CollectibleHandler(probe);

        var timer = CreateTimer(onTimer: null, interval: ShortInterval);
        timer.Tick += handler.OnTick;
        timer.Start();

        return (timer, new WeakReference(handler));
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

    protected sealed class TickProbe
    {
        public int Count => Volatile.Read(ref _count);

        public void OnTick()
        {
            Interlocked.Increment(ref _count);
            _countChanged.Set();
        }

        public void OnTick(ITimer _)
            => OnTick();

        public bool WaitForCount(int expectedCount, TimeSpan timeout)
        {
            var deadline = DateTime.UtcNow + timeout;

            while (Count < expectedCount)
            {
                var remaining = deadline - DateTime.UtcNow;
                if (remaining <= TimeSpan.Zero || !_countChanged.WaitOne(remaining))
                {
                    return Count >= expectedCount;
                }
            }

            return true;
        }

        private readonly AutoResetEvent _countChanged = new(false);

        private int _count;
    }

    protected sealed class StateChangeRecorder
    {
        public void OnStateChanged(ITimer timer)
        {
            lock (_sync)
            {
                _observedStates.Add(timer.State);
                _lastSender = timer;
            }

            _changed.Set();
        }

        public int Count
        {
            get
            {
                lock (_sync)
                {
                    return _observedStates.Count;
                }
            }
        }

        public TimerState? LastState
        {
            get
            {
                lock (_sync)
                {
                    return _observedStates.Count == 0 ? null : _observedStates[_observedStates.Count - 1];
                }
            }
        }

        public ITimer? LastSender
        {
            get
            {
                lock (_sync)
                {
                    return _lastSender;
                }
            }
        }

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

        public bool WaitForState(TimerState state, TimeSpan timeout)
        {
            var deadline = DateTime.UtcNow + timeout;

            while (!HasObserved(state))
            {
                var remaining = deadline - DateTime.UtcNow;
                if (remaining <= TimeSpan.Zero || !_changed.WaitOne(remaining))
                {
                    return HasObserved(state);
                }
            }

            return true;
        }

        private bool HasObserved(TimerState state)
        {
            lock (_sync)
            {
                return _observedStates.Contains(state);
            }
        }

        private readonly List<TimerState> _observedStates = new();
        private readonly AutoResetEvent _changed = new(false);
        private readonly object _sync = new();

        private ITimer? _lastSender;
    }

    private sealed class CollectibleHandler
    {
        public CollectibleHandler(TickProbe probe)
        {
            _probe = probe;
        }

        public void OnTick()
            => _probe.OnTick();

        public void OnTick(ITimer _)
            => _probe.OnTick();

        private readonly TickProbe _probe;
    }

    protected static readonly TimeSpan ShortInterval = TimeSpan.FromMilliseconds(50);
    protected static readonly TimeSpan FarFuture = TimeSpan.FromHours(1);
    protected static readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(5);
    protected static readonly TimeSpan SilenceWindow = TimeSpan.FromMilliseconds(400);

    #endregion
}
