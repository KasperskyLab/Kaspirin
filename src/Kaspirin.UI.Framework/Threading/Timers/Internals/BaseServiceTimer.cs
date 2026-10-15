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
using System.Threading;

namespace Kaspirin.UI.Framework.Threading.Timers.Internals;

internal abstract class BaseServiceTimer : ITimer
{
    public BaseServiceTimer(
        object owner,
        Action tickAction,
        MutableParameter<TimeSpan> interval,
        Func<ITimer> timerFactory)
    {
        _ownerWeak = new WeakReference(Guard.EnsureArgumentIsNotNull(owner));

        _tickAction = Guard.EnsureArgumentIsNotNull(tickAction);
        _timerInterval = Guard.EnsureArgumentIsNotNull(interval);
        _timerInterval.ValueChanged += OnIntervalChanged;

        _timer = Guard.EnsureArgumentIsNotNull(timerFactory).Invoke();
        _timer.Tick += OnTick;
        _timer.Interval = _timerInterval.Value;
    }

    public TimeSpan? Interval
    {
        get => _timer.Interval;
        set => throw new InvalidOperationException("ServiceTimer does not support changing of interval.");
    }

    public TimerOptions Options => _timer.Options;

    public TimerState State => _timer.State;

    public string Name => _timer.Name;

    public WaitHandle TickExecutionEvent => _timer.TickExecutionEvent;

    public object? Tag
    {
        get => _timer.Tag;
        set => _timer.Tag = value;
    }

    public bool IsRunning => _timer.IsRunning;

    public event Action<ITimer> StateChanged
    {
        add { _timer.StateChanged += value; }
        remove { _timer.StateChanged -= value; }
    }

    public event Action<ITimer> Tick
    {
        add { _timer.Tick += value; }
        remove { _timer.Tick -= value; }
    }

    public bool Start(TimeSpan? delay = null)
        => _timer.Start(delay);

    public bool Restart(TimeSpan? delay = null)
        => _timer.Restart(delay);

    public bool Stop()
        => _timer.Stop();

    public void Dispose()
        => _timer.Dispose();

    private void OnIntervalChanged(MutableParameter<TimeSpan> sender, MutableParameterChangedArguments<TimeSpan> arguments)
        => _timer.Interval = sender.Value;

    private void OnTick(ITimer timer)
    {
        if (_ownerWeak.IsAlive)
        {
            _tickAction.Invoke();
        }
        else
        {
            _timer.Dispose();
        }
    }

    private readonly Action _tickAction;
    private readonly MutableParameter<TimeSpan> _timerInterval;
    private readonly WeakReference _ownerWeak;
    private readonly ITimer _timer;
}
