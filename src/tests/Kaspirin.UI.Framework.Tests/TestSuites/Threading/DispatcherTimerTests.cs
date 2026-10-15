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
using System.Windows.Threading;
using Kaspirin.UI.Framework.Threading.Timers.Internals;
using ITimer = Kaspirin.UI.Framework.Threading.Timers.ITimer;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

[TestClass]
public sealed class DispatcherTimerTests : BaseTimerTests
{
    [TestInitialize]
    public void InitializeTests()
        => _host = new DispatcherTestHost();

    [TestCleanup]
    public void CleanupTests()
        => _host?.Dispose();

    [TestMethod]
    public void Tick_ExecutesOnDispatcherThread()
    {
        var tickThreadId = 0;
        using var tickReceived = new ManualResetEvent(false);

        Action onTimer = () =>
        {
            tickThreadId = Environment.CurrentManagedThreadId;
            tickReceived.Set();
        };

        using var timer = CreateTimer(onTimer, interval: null);
        timer.Start();

        Assert.IsTrue(tickReceived.WaitOne(WaitTimeout), "Tick was not raised.");
        Assert.AreEqual(
            _host!.Dispatcher.Thread.ManagedThreadId,
            tickThreadId,
            "Tick of a dispatcher timer must execute on the dispatcher thread.");

        GC.KeepAlive(onTimer);
    }

    protected override ITimer CreateTimer(
        Action? onTimer = null,
        TimeSpan? interval = null,
        TimerOptions options = TimerOptions.None)
    {
        return new DispatcherUnifiedTimer(
            onTimer,
            interval,
            string.Empty,
            options,
            _host!.Dispatcher,
            DispatcherPriority.Normal);
    }

    private DispatcherTestHost? _host;
}
