// Copyright © 2025 AO Kaspersky Lab.
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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

[TestClass]
public sealed class TimerTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        Executers.ThreadPoolExecutor = new ThreadPoolExecutor(Task.Factory);
    }

    [TestMethod]
    public void Timer_StartTriggersTick()
    {
        var tickEventFromCtor = new ManualResetEvent(false);
        var tickEventFromEvent = new ManualResetEvent(false);

        var timer = TimerFactory.CreateOnTp(
            onTimer: () => tickEventFromCtor.Set(),
            interval: TimeSpan.FromMilliseconds(500),
            name: "TestStartTriggersTick");

        timer.Tick += (t) => tickEventFromEvent.Set();
        timer.Start();
        Assert.IsTrue(tickEventFromCtor.WaitOne(100), "Initial tick not triggered.");
        Assert.IsTrue(tickEventFromEvent.WaitOne(100), "Initial tick not triggered.");

        tickEventFromCtor.Reset();
        tickEventFromEvent.Reset();

        Assert.IsTrue(tickEventFromCtor.WaitOne(2000), "Tick event was not triggered within the expected time.");
        Assert.IsTrue(tickEventFromEvent.WaitOne(2000), "Tick event was not triggered within the expected time.");
    }

    [TestMethod]
    public void Timer_StartReturnsFalseWhenAlreadyRunning()
    {
        var timer = TimerFactory.CreateOnTp(
            onTimer: () => { },
            interval: TimeSpan.FromMilliseconds(100),
            name: "TestStartReturnsFalseWhenAlreadyRunning");

        timer.Start();
        var result = timer.Start();
        Assert.IsFalse(result, "Start should return false when already running.");
    }

    [TestMethod]
    public void Timer_StopPreventsFurtherTicks()
    {
        var tickEvent = new ManualResetEvent(false);

        var timer = TimerFactory.CreateOnTp(
            onTimer: () => tickEvent.Set(),
            interval: TimeSpan.FromMilliseconds(500),
            name: "TestStopPreventsFurtherTicks");

        timer.Start();
        Assert.IsTrue(tickEvent.WaitOne(100), "Initial tick not triggered.");

        tickEvent.Reset();

        Assert.IsTrue(tickEvent.WaitOne(2000), "First tick not triggered.");

        tickEvent.Reset();
        timer.Stop();

        Assert.IsFalse(tickEvent.WaitOne(1000), "Second tick triggered after stopping.");
    }

    [TestMethod]
    public void Timer_DisposePreventsFurtherTicks()
    {
        var tickEvent = new ManualResetEvent(false);

        var timer = TimerFactory.CreateOnTp(
            onTimer: () => tickEvent.Set(),
            interval: TimeSpan.FromMilliseconds(500),
            name: "TestDisposePreventsFurtherTicks");

        timer.Start();
        Assert.IsTrue(tickEvent.WaitOne(100), "Initial tick not triggered.");

        tickEvent.Reset();

        Assert.IsTrue(tickEvent.WaitOne(2000), "First tick not triggered.");

        tickEvent.Reset();
        timer.Dispose();

        Assert.IsFalse(tickEvent.WaitOne(1000), "Second tick triggered after disposal.");
    }

    [TestMethod]
    public void Timer_StopAfterDispose()
    {
        var tickEvent = new ManualResetEvent(false);

        var timer = TimerFactory.CreateOnTp(
            onTimer: () => tickEvent.Set(),
            interval: TimeSpan.FromMilliseconds(100),
            name: "TestStopAfterDispose");

        timer.Dispose();

        var result = timer.Stop();
        Assert.IsFalse(result, "Stop should return false when timer is disposed.");
    }

    [TestMethod]
    public void Timer_DisposeReturnsFalse()
    {
        var timer = TimerFactory.CreateOnTp(
            onTimer: () => { },
            interval: TimeSpan.FromMilliseconds(100),
            name: "TestDisposeReturnsFalse");

        timer.Dispose();
        var result = timer.Start();
        Assert.IsFalse(result, "Start should return false when timer is disposed.");
    }

    [TestMethod]
    public void Timer_ChangeInterval()
    {
        var tickEvent = new ManualResetEvent(false);

        var timer = TimerFactory.CreateOnTp(
            onTimer: () =>
            {
                tickEvent.Set();
                Trace.WriteLine("Timer Fired");
            },
            interval: TimeSpan.FromMilliseconds(500),
            name: "TestChangeInterval");

        timer.Start();
        Assert.IsTrue(tickEvent.WaitOne(100), "Initial tick not triggered.");

        tickEvent.Reset();

        Assert.IsTrue(tickEvent.WaitOne(2000), "First tick not triggered after initial interval.");

        timer.Interval = TimeSpan.FromMilliseconds(2000);
        Trace.WriteLine("Interval changed");
        Assert.IsTrue(tickEvent.WaitOne(200), "Initial tick not triggered after interval change.");

        tickEvent.WaitOne(500);
        tickEvent.Reset();
        tickEvent.WaitOne(500);
        tickEvent.Reset();

        Assert.IsFalse(tickEvent.WaitOne(1000), "Second tick triggered before interval elapsed.");
    }

    [TestMethod]
    public void Timer_StartWithDelay()
    {
        var tickEvent = new ManualResetEvent(false);

        var timer = TimerFactory.CreateOnTp(
            onTimer: () => tickEvent.Set(),
            interval: TimeSpan.FromMilliseconds(500),
            name: "TestStartWithDelay");

        timer.Start(TimeSpan.FromMilliseconds(500));
        Assert.IsFalse(tickEvent.WaitOne(100), "Initial tick triggered.");

        Assert.IsTrue(tickEvent.WaitOne(600), "Tick not triggered after delay.");
    }

    [TestMethod]
    public void Timer_SingleShot()
    {
        var tickEvent = new ManualResetEvent(false);

        var timer = TimerFactory.CreateOnTp(
            onTimer: () => tickEvent.Set(),
            interval: null,
            name: "TestSingleShot");

        timer.Start(TimeSpan.FromMilliseconds(500));
        Assert.IsFalse(tickEvent.WaitOne(100), "Initial tick triggered.");

        Assert.IsTrue(tickEvent.WaitOne(1000), "First tick not triggered.");
        tickEvent.Reset();

        Assert.IsFalse(tickEvent.WaitOne(1000), "Second tick triggered for single-shot timer.");
    }

    [TestMethod]
    public void Timer_ContinuesAfterException()
    {
        var tickEvent = new ManualResetEvent(false);
        var firstExceptionFired = false;

        var timer = TimerFactory.CreateOnTp(
            onTimer: () =>
            {
                if (!firstExceptionFired)
                {
                    firstExceptionFired = true;
                    throw new InvalidOperationException("Test exception in onTimer");
                }

                tickEvent.Set();
            },
            interval: TimeSpan.FromMilliseconds(500),
            name: "TestContinuesAfterException");

        timer.Start();

        Assert.IsFalse(tickEvent.WaitOne(300), "Initial tick triggered.");
        Assert.IsTrue(firstExceptionFired, "Exception should have been fired at least once.");
        Assert.IsTrue(tickEvent.WaitOne(1000), "Timer should continue firing ticks after an exception in onTimer.");
    }
}
