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

internal abstract class BaseTimer : ITimer
{
    protected BaseTimer(Action? tickAction, TimeSpan? interval, string name, TimerOptions options)
    {
        Guard.Argument(interval == null || interval >= TimeSpan.Zero);
        Guard.ArgumentIsNotNull(name);

        var strongRef = options.HasFlag(TimerOptions.StrongRef);

        _stateChanged = new BaseTimerEvent(strongRef);
        _tick = new BaseTimerEvent(strongRef, OnTickHandlerCollected);
        _tickAction = tickAction != null ? strongRef ? tickAction : WeakEventHandlerFactory.Wrap(tickAction, OnTickActionCollected) : _emptyAction;

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

    public event Action<ITimer> StateChanged
    {
        add => _stateChanged.Subscribe(value);
        remove => _stateChanged.Unsubscribe(value);
    }

    public event Action<ITimer> Tick
    {
        add => _tick.Subscribe(value);
        remove => _tick.Unsubscribe(value);
    }

    public WaitHandle TickExecutionEvent => _tickExecutionEvent;

    public TimeSpan? Interval
    {
        get => _interval;
        set
        {
            InvokeStateChangeAction(() =>
            {
                if (State == TimerState.Disposed)
                {
                    _tracer?.TraceError($"Attempting to change interval on already disposed Timer.");
                    return;
                }

                if (_interval == value)
                {
                    return;
                }

                _interval = value;

                _tracer?.TraceInformation($"Timer tick interval changed to [{_interval?.ToString() ?? "<null>"}].");

                if (State != TimerState.Running)
                {
                    return;
                }

                if (_interval == null)
                {
                    State = TimerState.Stopped;

                    TimerStop();

                    _tracer?.TraceInformation($"Timer stopped due to interval became null.");
                }
                else
                {
                    TimerResetInterval();

                    _tracer?.TraceInformation($"Timer is restarted with new tick interval [{_interval}].");
                }
            });
        }
    }

    public TimerOptions Options { get; }

    public string Name { get; }

    public TimerState State { get; private set; }

    public object? Tag { get; set; }

    public bool IsRunning
    {
        get
        {
            lock (_sync)
            {
                return State == TimerState.Running;
            }
        }
    }

    public bool Start(TimeSpan? delay = null)
        => StartCore(delay, restart: false);

    public bool Restart(TimeSpan? delay = null)
        => StartCore(delay, restart: true);

    public bool Stop()
        => StopCore();

    public void Dispose()
    {
        InvokeStateChangeAction(() =>
        {
            if (State == TimerState.Disposed)
            {
                return;
            }

            State = TimerState.Disposed;

            TimerStop();
            TimerDispose();

            _tracer?.TraceInformation($"Timer disposed.");
        });
    }

    protected abstract void TimerStop();

    protected abstract void TimerStart(TimeSpan delay);

    protected abstract void TimerDispose();

    protected abstract void TimerResetInterval();

    protected void OnTimerTick()
    {
        var skipTick = false;

        InvokeStateChangeAction(() =>
        {
            if (State == TimerState.Stopped || State == TimerState.Disposed)
            {
                skipTick = true;
                return;
            }

            if (TryDisposeOnEmpty())
            {
                skipTick = true;
                return;
            }

            if (IsEmpty)
            {
                skipTick = true;
            }
            else
            {
                _tickExecutionEvent.Reset();
            }

            if (Interval == null)
            {
                State = TimerState.Stopped;

                TimerStop();

                _tracer?.TraceInformation($"Timer stopped due to interval is null on tick execution.");
            }

            if (_tracer != null && skipTick)
            {
                _tracer.TraceDebug($"Timer tick skipped due to timer state [{State}] or empty state [{IsEmpty}].");
                return;
            }
        });

        if (skipTick)
        {
            return;
        }

        InvokeSafeAction(() => _tickAction.Invoke());
        InvokeSafeAction(() => _tick.Invoke(this));

        _tickExecutionEvent.Set();
    }

    private bool TryDisposeOnEmpty()
    {
        if (State == TimerState.Disposed)
        {
            return false;
        }

        if (IsEmpty && !IsEmptyAllowed)
        {
            Dispose();
            _tracer?.TraceInformation($"Timer disposed due to no actions registered.");

            return true;
        }

        return false;
    }

    private void OnTickActionCollected(Action tickAction)
    {
        InvokeStateChangeAction(() =>
        {
            _tickAction = _emptyAction;
            _tracer?.TraceInformation($"Timer tick action unregistered.");

            TryDisposeOnEmpty();
        });
    }

    private void OnTickHandlerCollected()
    {
        InvokeStateChangeAction(() =>
        {
            _tracer?.TraceInformation($"Timer tick event handler unregistered. ");

            TryDisposeOnEmpty();
        });
    }

    private bool StartCore(TimeSpan? delay, bool restart)
    {
        Guard.Argument(delay == null || delay >= TimeSpan.Zero);

        var isStarted = false;

        InvokeStateChangeAction(() =>
        {
            if (State == TimerState.Disposed)
            {
                _tracer?.TraceError($"Attempting to start already disposed Timer.");
                return;
            }

            if (State == TimerState.Running && !restart)
            {
                _tracer?.TraceWarning($"Attempting to start already started Timer.");
                return;
            }

            if (TryDisposeOnEmpty())
            {
                throw new InvalidOperationException("Starting timer that has no actions registered on tick is not allowed.");
            }

            if (State == TimerState.Running && restart)
            {
                State = TimerState.Stopped;

                TimerStop();

                _tracer?.TraceInformation($"Timer stopped due to restart.");
            }

            State = TimerState.Running;

            TimerStart(delay: delay ?? TimeSpan.Zero);

            _tracer?.TraceInformation($"Timer {(restart ? "restarted" : "started")} with delay [{delay?.ToString() ?? "<null>"}]" +
                                      $" and interval [{Interval?.ToString() ?? "<null>"}].");

            isStarted = true;
        });

        return isStarted;
    }

    private bool StopCore()
    {
        var isStopped = false;

        InvokeStateChangeAction(() =>
        {
            if (State == TimerState.Disposed)
            {
                _tracer?.TraceError($"Attempting to stop already disposed Timer.");
                return;
            }

            if (State == TimerState.Stopped || State == TimerState.NeverRun)
            {
                _tracer?.TraceWarning($"Attempting to stop already stopped Timer.");
                return;
            }

            State = TimerState.Stopped;

            TimerStop();

            _tracer?.TraceInformation($"Timer stopped.");

            isStopped = true;
        });

        return isStopped;
    }

    private void InvokeStateChangeAction(Action action)
    {
        var stateChanged = false;

        try
        {
            lock (_sync)
            {
                if (_stateChanging)
                {
                    action.Invoke();
                    return;
                }

                _stateChanging = true; // prevent reentrancy

                var stateBefore = State;

                try
                {
                    action.Invoke();
                }
                finally
                {
                    _stateChanging = false;

                    stateChanged = State != stateBefore;
                }
            }
        }
        finally
        {
            if (stateChanged)
            {
                _stateChanged.Invoke(this);
            }
        }
    }

    private void InvokeSafeAction(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception e)
        {
            e.TraceException();
        }
    }

    private bool IsEmpty => _tickAction == _emptyAction && _tick.HandlersCount == 0;

    private bool IsEmptyAllowed => Options.HasFlag(TimerOptions.AllowEmpty);

    private readonly BaseTimerEvent _stateChanged;
    private readonly BaseTimerEvent _tick;
    private readonly object _sync = new();
    private readonly ComponentTracer? _tracer;
    private readonly ManualResetEvent _tickExecutionEvent = new(true);

    private static readonly Action _emptyAction = EmptyDelegate.For<Action>();

    private bool _stateChanging;
    private Action _tickAction;
    private TimeSpan? _interval;
}
