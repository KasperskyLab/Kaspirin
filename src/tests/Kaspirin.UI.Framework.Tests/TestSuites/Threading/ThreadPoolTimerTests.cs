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
using System.Threading;
using Kaspirin.UI.Framework.Threading.Timers.Internals;
using ITimer = Kaspirin.UI.Framework.Threading.Timers.ITimer;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

[TestClass]
public sealed class ThreadPoolTimerTests : BaseTimerTests
{
    [TestMethod]
    public void Tick_ExecutesOnThreadPoolThread()
    {
        var isThreadPoolThread = false;
        using var tickReceived = new ManualResetEvent(false);

        Action onTimer = () =>
        {
            isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
            tickReceived.Set();
        };

        using var timer = CreateTimer(onTimer, interval: null);
        timer.Start();

        Assert.IsTrue(tickReceived.WaitOne(WaitTimeout), "Tick was not raised.");
        Assert.IsTrue(isThreadPoolThread, "Tick of a thread pool timer must execute on a thread pool thread.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Start_WithDelayBeyondInt32Milliseconds_DoesNotTickEarly()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;

        var hugeDelay = TimeSpan.FromMilliseconds((double)int.MaxValue + 60_000);

        using var timer = CreateTimer(onTimer, interval: null);
        timer.Start(delay: hugeDelay);

        Assert.IsFalse(
            probe.WaitForCount(1, SilenceWindow),
            "A delay beyond Int32 milliseconds must not be truncated into an early tick.");

        GC.KeepAlive(onTimer);
    }

    [TestMethod]
    public void Start_WithIntervalBeyondInt32Milliseconds_TicksOnceThenWaits()
    {
        var probe = new TickProbe();
        Action onTimer = probe.OnTick;

        var hugeInterval = TimeSpan.FromMilliseconds((double)int.MaxValue + 60_000);

        using var timer = CreateTimer(onTimer, interval: hugeInterval);
        timer.Start();

        Assert.IsTrue(probe.WaitForCount(1, WaitTimeout), "Timer did not tick after Start.");
        Assert.IsFalse(
            probe.WaitForCount(2, SilenceWindow),
            "An interval beyond Int32 milliseconds must not be truncated into an early second tick.");

        GC.KeepAlive(onTimer);
    }

    protected override ITimer CreateTimer(
        Action? onTimer = null,
        TimeSpan? interval = null,
        TimerOptions options = TimerOptions.None)
    {
        return new ThreadPoolUnifiedTimer(onTimer, interval, string.Empty, options);
    }
}
