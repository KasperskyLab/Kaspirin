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

#pragma warning disable KAS002 // Creating timers is prohibited

using System;
using System.Threading;

namespace Kaspirin.UI.Framework.Threading.Timers.Internals;

internal sealed class ThreadPoolUnifiedTimer : BaseTimer
{
    public ThreadPoolUnifiedTimer(Action? tickAction, TimeSpan? interval, string name, TimerOptions options)
        : base(tickAction, interval, name, options)
    {
        var enableTraces = Options.HasFlag(TimerOptions.EnableTraces);

        var actionThrottlerName = Name.IsNotEmpty() ? $"{Name}_Action_Throttler" : string.Empty;
        var actionThrottlerOptions = enableTraces
               ? DeferredActionOptions.EnableTraces
               : DeferredActionOptions.None;

        _timerTickThrottler = DeferredActionFactory.CreateThrottlerOnTp(
            action: OnTimerTick,
            options: actionThrottlerOptions,
            name: actionThrottlerName);

        _timer = new Timer(o => _timerTickThrottler.Execute());
    }

    protected override void TimerStop()
    {
        _timer.Change(_infiniteTimeSpan, _infiniteTimeSpan);
    }

    protected override void TimerStart(TimeSpan delay)
    {
        _timer.Change(delay, Interval ?? _infiniteTimeSpan);
    }

    protected override void TimerDispose()
    {
        _timer.Dispose();
        _timerTickThrottler.Dispose();
    }

    protected override void TimerResetInterval()
    {
        Guard.Assert(Interval.HasValue, "Interval must be set");

        _timer.Change(TimeSpan.Zero, Interval.Value);
    }

    private static readonly TimeSpan _infiniteTimeSpan = TimeSpan.FromMilliseconds(Timeout.Infinite);

    private readonly IDeferredAction _timerTickThrottler;
    private readonly Timer _timer;
}
