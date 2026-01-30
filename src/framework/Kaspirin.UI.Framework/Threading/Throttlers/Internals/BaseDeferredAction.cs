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
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.Threading.Throttlers.Internals;

internal abstract class BaseDeferredAction<TArgument> : IDeferredAction<TArgument>
{
    protected BaseDeferredAction(DeferredActionParameters parameters)
        : this(parameters.DelayBeforeAction,
               parameters.DelayAfterAction,
               parameters.Options,
               parameters.Mode,
               parameters.Name)
    {
        Guard.ArgumentIsNotNull(parameters);

        _action = Options.HasFlag(DeferredActionOptions.Weak)
            ? WeakEventHandler.Wrap(parameters.Action, _ => OnActionCollected())
            : parameters.Action;
    }

    protected BaseDeferredAction(DeferredActionParameters<TArgument> parameters)
        : this(parameters.DelayBeforeAction,
               parameters.DelayAfterAction,
               parameters.Options,
               parameters.Mode,
               parameters.Name)
    {
        Guard.ArgumentIsNotNull(parameters);

        _actionWithArg = Options.HasFlag(DeferredActionOptions.Weak)
            ? WeakEventHandler.Wrap(parameters.Action, _ => OnActionCollected())
            : parameters.Action;
    }

    private BaseDeferredAction(TimeSpan delayBeforeAction, TimeSpan delayAfterAction, DeferredActionOptions options, DeferredActionMode mode, string name)
    {
        DelayBeforeAction = delayBeforeAction;
        DelayAfterAction = delayAfterAction;

        _actionCts = new CancellationTokenSource();

        Options = options;
        Mode = mode;
        Name = name;

        if (Options.HasFlag(DeferredActionOptions.EnableTraces))
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
            _tracer.TraceInformation($"DeferredAction created.");
        }
    }

    public DeferredActionState State { get; private set; }

    public DeferredActionMode Mode { get; }

    public DeferredActionOptions Options { get; }

    public TimeSpan DelayBeforeAction { get; }

    public TimeSpan DelayAfterAction { get; }

    public string Name { get; }

    public void Execute(TArgument? argument)
    {
        lock (_sync)
        {
            _lastArgument = argument;
            _executeRequestedCount++;

            if (Mode == DeferredActionMode.Throttle && State is DeferredActionState.ActionRunning or
                                                                DeferredActionState.DelayAfterAction or
                                                                DeferredActionState.DelayBeforeAction)
            {
                _tracer?.TraceInformation($"Action execution ignored because is was started before.");
                return;
            }

            if (Mode == DeferredActionMode.Debounce && State is DeferredActionState.ActionRunning or
                                                                DeferredActionState.DelayAfterAction)
            {
                _tracer?.TraceInformation($"Action execution ignored because of previous execution is not completed yet.");
                return;
            }

            _currentArgument = argument;

            _actionCts = ResetCancellationToken(_actionCts);

            if (DelayBeforeAction != TimeSpan.Zero)
            {
                if (!TryChangeState(DeferredActionState.DelayBeforeAction))
                {
                    return;
                }

                _tracer?.TraceInformation($"Action execution scheduled after timeout [{DelayBeforeAction}].");

                StartAction(StartExecution, DelayBeforeAction, _actionCts.Token);
            }
            else
            {
                StartExecution();
            }
        }
    }

    public void Cancel()
    {
        lock (_sync)
        {
            if (!TryChangeState(DeferredActionState.Cancelled))
            {
                return;
            }

            _tracer?.TraceInformation($"Action cancellation requested.");

            CleanUp();
        }
    }

    public void Dispose()
    {
        lock (_sync)
        {
            if (!TryChangeState(DeferredActionState.Disposed))
            {
                return;
            }

            CleanUp();

            _action = null;
            _actionWithArg = null;

            _tracer?.TraceInformation($"Action disposed.");
        }
    }

    protected abstract Task StartAction(Action action, TimeSpan delay, CancellationToken token);

    private void StartExecution()
    {
        lock (_sync)
        {
            if (!TryChangeState(DeferredActionState.ActionRunning))
            {
                return;
            }

            Guard.IsNotNull(_actionCts);

            _tracer?.TraceInformation($"Action execution started.");

            var argument = _currentArgument;

            StartAction(() => action(argument), delay: TimeSpan.Zero, _actionCts.Token)
                .ContinueWith(
                    continuationAction: _ => StartFinalization(),
                    continuationOptions: TaskContinuationOptions.ExecuteSynchronously,
                    scheduler: TaskScheduler.Default,
                    cancellationToken: _actionCts.Token);

            void action(TArgument? argument)
            {
                // Check that only one action (_action or _actionWithArg) was assigned.
                Guard.Argument(_action != null || _actionWithArg != null);
                Guard.Argument(_action == null || _actionWithArg == null);

                _action?.Invoke();
                _actionWithArg?.Invoke(argument);

                _tracer?.TraceInformation($"Action execution completed.");
            }
        }
    }

    private void StartFinalization()
    {
        lock (_sync)
        {
            if (DelayAfterAction != TimeSpan.Zero)
            {
                if (!TryChangeState(DeferredActionState.DelayAfterAction))
                {
                    return;
                }

                _tracer?.TraceInformation($"Action finalization scheduled after timeout [{DelayAfterAction}].");

                Guard.IsNotNull(_actionCts);

                StartAction(FinalizeExecution, DelayAfterAction, _actionCts.Token);
            }
            else
            {
                FinalizeExecution();
            }
        }
    }

    private void FinalizeExecution()
    {
        lock (_sync)
        {
            _tracer?.TraceInformation($"Action finalization started.");

            if (!TryChangeState(DeferredActionState.Idle))
            {
                return;
            }

            var hasSkippedActions = Interlocked.Exchange(ref _executeRequestedCount, 0) > 1;
            if (hasSkippedActions && Options.HasFlag(DeferredActionOptions.RunLastSkippedAction))
            {
                _tracer?.TraceInformation($"Action execution started for last ignored request.");

                Execute(_lastArgument);
            }
            else
            {
                CleanUp();

                _tracer?.TraceInformation($"Action finalization completed.");
            }
        }
    }

    private void CleanUp()
    {
        lock (_sync)
        {
            _actionCts = ResetCancellationToken(_actionCts);

            _executeRequestedCount = 0;

            _currentArgument = default;
            _lastArgument = default;
        }
    }

    private CancellationTokenSource ResetCancellationToken(CancellationTokenSource current)
    {
        current.Cancel();
        current.Dispose();
        return new CancellationTokenSource();
    }

    private bool TryChangeState(DeferredActionState state)
    {
        lock (_sync)
        {
            var oldState = State;
            var newState = state;
            var canChange = false;

            if (oldState == DeferredActionState.Disposed)
            {
                canChange = false;
            }
            else if (newState == DeferredActionState.Disposed ||
                     newState == DeferredActionState.Cancelled)
            {
                canChange = true;
            }
            else
            {
                canChange = oldState switch
                {
                    DeferredActionState.Idle => newState == DeferredActionState.DelayBeforeAction ||
                                                newState == DeferredActionState.ActionRunning,

                    DeferredActionState.Cancelled => newState == DeferredActionState.DelayBeforeAction ||
                                                     newState == DeferredActionState.ActionRunning,

                    DeferredActionState.DelayBeforeAction => newState == DeferredActionState.DelayBeforeAction ||
                                                             newState == DeferredActionState.ActionRunning,

                    DeferredActionState.ActionRunning => newState == DeferredActionState.DelayAfterAction ||
                                                         newState == DeferredActionState.Idle,

                    DeferredActionState.DelayAfterAction => newState == DeferredActionState.Idle,
                    _ => false,
                };
            }

            if (canChange)
            {
                State = newState;
                _tracer?.TraceInformation($"State changed from {oldState} to {newState}.");
            }
            else
            {
                Guard.Fail($"Failed to change state from {oldState} to {newState}.");
            }

            return canChange;
        }
    }

    private void OnActionCollected()
    {
        lock (_sync)
        {
            _tracer?.TraceInformation($"Action reference is null. Disposing.");
            Dispose();
        }
    }

    private readonly ComponentTracer? _tracer;
    private readonly object _sync = new();

    private int _executeRequestedCount;
    private CancellationTokenSource _actionCts;
    private TArgument? _currentArgument;
    private TArgument? _lastArgument;
    private Action? _action;
    private Action<TArgument?>? _actionWithArg;
}
