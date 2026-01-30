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

namespace Kaspirin.UI.Framework.Threading.Timers.Internals;

internal abstract class BaseTimer : ITimer
{
    protected BaseTimer(Action tickAction, TimeSpan? interval, string name, TimerOptions options)
    {
        Guard.Argument(interval == null || interval >= TimeSpan.Zero);
        Guard.ArgumentIsNotNull(tickAction);
        Guard.ArgumentIsNotNull(name);

        _tickAction = options.HasFlag(TimerOptions.Weak)
            ? WeakEventHandler.Wrap(tickAction, OnTickActionCollected)
            : tickAction;

        _interval = interval;

        Options = options;
        Name = name;
        State = TimerState.NeverRun;

        if (options.HasFlag(TimerOptions.EnableTraces))
        {
            var tracerParams = new ComponentTracerParameters(ComponentTracers.Threading)
            {
                HashSource = this,
                PrefixSource = this,
                PrefixFunc = (o) => name.IsEmpty()
                    ? $"{o.GetType().Name}"
                    : $"{o.GetType().Name} ({name})"
            };

            _tracer = ComponentTracer.Get(tracerParams);
            _tracer.TraceInformation($"Timer created.");
        }
    }

    public event Action<ITimer>? Started;

    public event Action<ITimer>? Stopped;

    public event Action<ITimer>? Tick;

    public TimeSpan? Interval
    {
        get => _interval;
        set
        {
            lock (_sync)
            {
                if (State == TimerState.Disposed)
                {
                    _tracer?.TraceError($"Attempting to change interval on already disposed Timer.");
                    return;
                }

                if (_interval != value)
                {
                    _interval = value;
                    _tracer?.TraceInformation($"Timer tick interval changed to [{Interval?.ToString() ?? "<null>"}].");
                    OnIntervalChanged();
                }
            }
        }
    }

    public TimerOptions Options { get; }

    public string Name { get; }

    public TimerState State { get; private set; }

    public bool Start(TimeSpan? delay = null)
    {
        Guard.Argument(delay == null || delay >= TimeSpan.Zero);

        var result = false;

        lock (_sync)
        {
            if (State == TimerState.Disposed)
            {
                _tracer?.TraceError($"Attempting to start already disposed Timer.");
                result = false;
            }

            else if (State == TimerState.Running)
            {
                _tracer?.TraceWarning($"Attempting to start already started Timer.");
                result = false;
            }
            else
            {
                State = TimerState.Running;

                TimerStart(delay: delay ?? TimeSpan.Zero);

                _tracer?.TraceInformation($"Timer started with delay [{delay?.ToString() ?? "<null>"}] and interval [{Interval?.ToString() ?? "<null>"}].");

                result = true;
            }
        }

        Started?.Invoke(this);
        return result;
    }

    public bool Stop()
    {
        var result = false;

        lock (_sync)
        {
            if (State == TimerState.Disposed)
            {
                _tracer?.TraceError($"Attempting to stop already disposed Timer.");
                result = false;
            }
            else if (State == TimerState.Stopped || State == TimerState.NeverRun)
            {
                _tracer?.TraceWarning($"Attempting to stop already stopped Timer.");
                result = false;
            }
            else
            {
                State = TimerState.Stopped;

                TimerStop();

                _tracer?.TraceInformation($"Timer stopped.");
                result = true;
            }
        }

        Stopped?.Invoke(this);
        return result;
    }

    public void Dispose()
    {
        lock (_sync)
        {
            if (State != TimerState.Disposed)
            {
                State = TimerState.Disposed;

                TimerStop();
                TimerDispose();

                _tracer?.TraceInformation($"Timer disposed.");
            }
        }
    }

    protected abstract void TimerStop();

    protected abstract void TimerStart(TimeSpan delay);

    protected abstract void TimerDispose();

    protected abstract void TimerResetInterval();

    protected void OnTimerTick()
    {
        Tick?.Invoke(this);

        try
        {
            _tickAction.Invoke();
        }
        catch (Exception e)
        {
            e.TraceException();
        }
    }

    private void OnIntervalChanged()
    {
        lock (_sync)
        {
            if (State == TimerState.Running)
            {
                TimerResetInterval();

                _tracer?.TraceInformation($"Timer is restarted with new tick interval [{Interval?.ToString() ?? "<null>"}].");
            }
        }
    }

    private void OnTickActionCollected(Action action)
    {
        _tracer?.TraceInformation($"Timer tick action reference is null. Disposing timer.");

        Dispose();
    }

    private readonly object _sync = new();
    private readonly ComponentTracer? _tracer;
    private readonly Action _tickAction;

    private TimeSpan? _interval;
}
