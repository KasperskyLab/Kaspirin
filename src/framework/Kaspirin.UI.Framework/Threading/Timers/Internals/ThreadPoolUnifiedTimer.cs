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
using System.Threading;

namespace Kaspirin.UI.Framework.Threading.Timers.Internals;

internal sealed class ThreadPoolUnifiedTimer : BaseTimer
{
    public ThreadPoolUnifiedTimer(Action tickAction, TimeSpan? interval, string name, TimerOptions options)
        : base(tickAction, interval, name, options)
    {

        var enableTraces = Options.HasFlag(TimerOptions.EnableTraces);

        var actionThrottlerName = Name.IsNotEmpty() ? $"{Name}_Action_Throttler" : string.Empty;
        var actionThrottlerOptions = DeferredActionOptions.RunLastSkippedAction | (enableTraces
               ? DeferredActionOptions.EnableTraces
               : DeferredActionOptions.None);

        _timerTickThrottler = DeferredActionFactory.CreateThrottlerOnTp(
            action: OnTimerTick,
            options: actionThrottlerOptions,
            name: actionThrottlerName);

        _timer = new Timer(o => _timerTickThrottler.Execute());
    }

    protected override void TimerStop()
    {
        var dueTime = Timeout.Infinite;
        var interval = Timeout.Infinite;

        _timer.Change(dueTime, interval);
    }

    protected override void TimerStart(TimeSpan delay)
    {
        var dueTime = (int)delay.TotalMilliseconds;
        var interval = (int)(Interval?.TotalMilliseconds ?? Timeout.Infinite);

        _timer.Change(dueTime, interval);
    }

    protected override void TimerDispose()
    {
        _timer.Dispose();
        _timerTickThrottler.Dispose();
    }

    protected override void TimerResetInterval()
    {
        var dueTime = 0;
        var interval = (int)(Interval?.TotalMilliseconds ?? Timeout.Infinite);

        _timer.Change(dueTime, interval);
    }

    private readonly IDeferredAction _timerTickThrottler;
    private readonly Timer _timer;
}
