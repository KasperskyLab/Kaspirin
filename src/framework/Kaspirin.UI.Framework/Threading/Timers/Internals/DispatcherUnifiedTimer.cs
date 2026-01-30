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
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading.Timers.Internals;

internal sealed class DispatcherUnifiedTimer : BaseTimer
{
    public DispatcherUnifiedTimer(Action tickAction, TimeSpan? interval, string name, TimerOptions options, Dispatcher dispatcher, DispatcherPriority priority)
        : base(tickAction, interval, name, options)
    {
        Guard.ArgumentIsNotNull(dispatcher);

        _primaryTimer = new DispatcherTimer(priority, dispatcher);
        _primaryTimer.Tick += (o, e) => OnTimerTick();
        _primaryTimer.Interval = Interval.GetValueOrDefault();

        _delayTimer = new DispatcherTimer(priority, dispatcher);
        _delayTimer.Tick += (o, e) => OnDelayTimerTick();
    }

    protected override void TimerStop()
    {
        lock (_sync)
        {
            _primaryTimer.Stop();
            _delayTimer.Stop();

            _isStarted = false;
        }
    }

    protected override void TimerStart(TimeSpan delay)
    {
        lock (_sync)
        {
            _delayTimer.Stop();
            _delayTimer.Interval = delay;
            _delayTimer.Start();

            _isStarted = true;
        }
    }

    protected override void TimerDispose()
    {
    }

    protected override void TimerResetInterval()
    {
        lock (_sync)
        {
            _primaryTimer.Interval = Interval.GetValueOrDefault();
        }
    }

    private void OnTimerStart()
    {
        lock (_sync)
        {
            if (_isStarted)
            {
                _primaryTimer.Start();
            }
        }
    }

    private void OnDelayTimerTick()
    {
        lock (_sync)
        {
            _delayTimer.Stop();
        }

        if (Interval == null)
        {
            OnTimerTick();
        }
        else
        {
            OnTimerStart();
        }
    }

    private readonly DispatcherTimer _primaryTimer;
    private readonly DispatcherTimer _delayTimer;
    private readonly object _sync = new();

    private bool _isStarted;
}
