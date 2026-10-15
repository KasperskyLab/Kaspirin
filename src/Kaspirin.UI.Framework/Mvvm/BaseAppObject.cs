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
using System.Collections.Generic;

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Provides a basic implementation for traceable application objects.
/// </summary>
public abstract class BaseAppObject
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseAppObject" /> class.
    /// </summary>
    /// <remarks>
    ///     The tracer <see cref="Tracer" /> uses the type name as the component name.
    /// </remarks>
    internal BaseAppObject()
    {
        Tracer = ComponentTracer.Get(
            traceComponent: GetType().Name,
            prefix: string.Empty);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseAppObject" /> class.
    /// </summary>
    /// <param name="traceComponent">
    ///     The name of the component for the message tracer.
    /// </param>
    /// <remarks>
    ///     The tracer <see cref="Tracer" /> uses the type name as a message prefix.
    /// </remarks>
    internal BaseAppObject(string traceComponent)
    {
        Guard.ArgumentIsNotNullOrEmpty(traceComponent);

        Tracer = ComponentTracer.Get(
            traceComponent: traceComponent,
            source: this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseAppObject" /> class.
    /// </summary>
    /// <param name="tracer">
    ///     The message tracer.
    /// </param>
    internal BaseAppObject(ComponentTracer tracer)
    {
        Guard.ArgumentIsNotNull(tracer);

        Tracer = tracer;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseAppObject" /> class.
    /// </summary>
    /// <param name="tracerParameters">
    ///     Message tracer parameters.
    /// </param>
    /// <remarks>
    ///     If the properties <see cref="ComponentTracerParameters.HashSource" /> or <see cref="ComponentTracerParameters.PrefixSource" />
    ///     of the <paramref name="tracerParameters" /> object are <see langword="null" />, then <see langword="this" />
    ///     is automatically inserted there.
    /// </remarks>
    protected BaseAppObject(ComponentTracerParameters tracerParameters)
    {
        Guard.ArgumentIsNotNull(tracerParameters);

        tracerParameters.HashSource ??= this;
        tracerParameters.PrefixSource ??= this;

        Tracer = ComponentTracer.Get(tracerParameters);
    }

    /// <summary>
    ///     The message tracer.
    /// </summary>
    protected ComponentTracer Tracer { get; }

    /// <summary>
    ///     Creates a new timer with the specified action and interval.
    /// </summary>
    /// <remarks>
    ///     The timer will stop automatically if the object that created the timer is deleted from memory by the garbage collector.
    /// </remarks>
    /// <param name="tickAction">
    ///     The action performed at each tick of the timer.
    /// </param>
    /// <param name="timerInterval">
    ///     ID of the timer interval.
    /// </param>
    /// <returns>
    ///     A timer waiting to start.
    /// </returns>
    protected ITimer CreateTimer(Action tickAction, TimerIntervals timerInterval)
        => CreateTimer(tickAction, intervalId: timerInterval);

    /// <summary>
    ///     Creates a new timer with the specified action and interval.
    /// </summary>
    /// <remarks>
    ///     The timer will stop automatically if the object that created the timer is deleted from memory by the garbage collector.
    /// </remarks>
    /// <param name="tickAction">
    ///     The action performed at each tick of the timer.
    /// </param>
    /// <param name="intervalId">
    ///     ID of the timer interval.
    /// </param>
    /// <returns>
    ///     A timer waiting to start.
    /// </returns>
    protected ITimer CreateTimer(Action tickAction, Enum intervalId)
    {
        var interval = _mutableParameterProvider.Value.GetParameter<TimeSpan>(intervalId);

        var timer = CreateTimerCore(tickAction, interval);

        lock (_sync)
        {
            _activeTimers.RemoveAll(static timer => !timer.TryGetTarget(out _));
            _activeTimers.Add(new(timer));
        }

        return timer;
    }

    internal abstract ITimer CreateTimerCore(Action tickAction, MutableParameter<TimeSpan> interval);

    private readonly object _sync = new();
    private readonly List<WeakReference<ITimer>> _activeTimers = new();
    private readonly Lazy<IMutableParameterProvider> _mutableParameterProvider = new(ServiceLocator.GetService<IMutableParameterProvider>);
}
