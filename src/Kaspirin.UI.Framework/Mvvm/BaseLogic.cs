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
using Kaspirin.UI.Framework.Threading.Timers.Internals;

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Provides basic functionality for the business logic of the application.
/// </summary>
public abstract class BaseLogic : BaseAppObject, IDisposable
{
    /// <summary>
    ///     Creates a new instance of the <see cref="BaseLogic" /> class.
    /// </summary>
    protected BaseLogic()
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="BaseLogic" /> class.
    /// </summary>
    /// <param name="traceComponent">
    ///     The name of the component for the message tracer.
    /// </param>
    protected BaseLogic(string traceComponent)
        : base(traceComponent)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="BaseLogic" /> class.
    /// </summary>
    /// <param name="tracer">
    ///     The message tracer.
    /// </param>
    protected BaseLogic(ComponentTracer tracer)
        : base(tracer)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="BaseLogic" /> class.
    /// </summary>
    /// <param name="tracerParameters">
    ///     Message tracer parameters.
    /// </param>
    /// <remarks>
    ///     If the properties <see cref="ComponentTracerParameters.HashSource" /> or <see cref="ComponentTracerParameters.PrefixSource" />
    ///     of the <paramref name="tracerParameters" /> object are <see langword="null" />, then <see langword="this" />
    ///     is automatically inserted there.
    /// </remarks>
    protected BaseLogic(ComponentTracerParameters tracerParameters)
        : base(tracerParameters)
    {
    }

    #region IDisposable

    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
    }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion

    internal sealed override ITimer CreateTimerCore(Action tickAction, MutableParameter<TimeSpan> interval)
        => new ThreadPoolServiceTimer(this, tickAction, interval);

    /// <summary>
    ///     Adds a service task with the specified action to the task queue.
    /// </summary>
    /// <param name="taskAction">
    ///     The action performed by the task.
    /// </param>
    /// <param name="cancellationToken">
    ///     The task cancellation token.
    /// </param>
    internal protected BaseLogicTask PushTask(Action taskAction, CancellationToken cancellationToken = default)
    {
        var taskCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var task = _executor.ExecuteAsync(taskAction, taskCts.Token);

        return new(this, task, taskCts);
    }

    /// <summary>
    ///     Adds a service task with the specified action to the task queue.
    /// </summary>
    /// <param name="taskAction">
    ///     The action performed by the task.
    /// </param>
    /// <param name="cancellationToken">
    ///     The task cancellation token.
    /// </param>
    internal protected BaseLogicTask<TResult> PushTask<TResult>(Func<TResult> taskAction, CancellationToken cancellationToken = default)
    {
        var taskCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var task = _executor.ExecuteAsync(taskAction, taskCts.Token);

        return new(this, task, taskCts);
    }

    private readonly IExecutor _executor = new LimitedThreadPoolExecutor();
}
