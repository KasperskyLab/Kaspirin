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

namespace Kaspirin.UI.Framework.Threading.Executing;

/// <summary>
///     Delegate executor based on a thread pool with a queue of tasks and a configurable limit on
///     the parallelism of their execution.
/// </summary>
/// <remarks>
///     The class ensures that tasks are completed in turn, ensuring that only the number of tasks
///     specified in the constructor is processed at a time.
/// </remarks>
public sealed class LimitedThreadPoolExecutor : IExecutor
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LimitedThreadPoolExecutor" /> class.
    /// </summary>
    /// <param name="parallelTaskCount">
    ///     The maximum number of parallel tasks.
    /// </param>
    public LimitedThreadPoolExecutor(int parallelTaskCount = 1)
    {
        Guard.Argument(parallelTaskCount > 0);

        var factoryScheduler = new LimitedTaskScheduler(parallelTaskCount);
        var factory = new TaskFactory(factoryScheduler);

        _syncedExecutor = new ThreadPoolExecutor(factory);
    }

    /// <inheritdoc/>
    public event EventHandler IsAvailableChanged
    {
        add { _syncedExecutor.IsAvailableChanged += value; }
        remove { _syncedExecutor.IsAvailableChanged -= value; }
    }

    /// <inheritdoc/>
    public bool IsAvailable => _syncedExecutor.IsAvailable;

    /// <inheritdoc cref="IExecutor.ExecuteSync"/>
    public void ExecuteSync(Action action)
        => _syncedExecutor.ExecuteSync(action, _defaultOptions, CancellationToken.None);

    /// <inheritdoc cref="IExecutor.ExecuteSync{T}"/>
    public TResult ExecuteSync<TResult>(Func<TResult> action)
        => _syncedExecutor.ExecuteSync(action, _defaultOptions, CancellationToken.None);

    /// <inheritdoc cref="IExecutor.ExecuteAsync"/>
    public Task ExecuteAsync(Action action)
        => _syncedExecutor.ExecuteAsync(action, _defaultOptions, CancellationToken.None);

    /// <inheritdoc cref="IExecutor.ExecuteAsync{T}"/>
    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action)
        => _syncedExecutor.ExecuteAsync(action, _defaultOptions, CancellationToken.None);

    /// <inheritdoc cref="IExecutor.ExecuteAsyncWithDelay"/>
    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay)
        => _syncedExecutor.ExecuteAsyncWithDelay(action, delay, _defaultOptions, CancellationToken.None);

    /// <inheritdoc cref="IExecutor.ExecuteAsyncWithDelay{T}"/>
    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay)
        => _syncedExecutor.ExecuteAsyncWithDelay(action, delay, _defaultOptions, CancellationToken.None);

    /// <inheritdoc/>
    public void ExecuteSync(Action action, CancellationToken cancellationToken)
        => _syncedExecutor.ExecuteSync(action, _defaultOptions, cancellationToken);

    /// <inheritdoc/>
    public TResult ExecuteSync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => _syncedExecutor.ExecuteSync(action, _defaultOptions, cancellationToken);

    /// <inheritdoc/>
    public Task ExecuteAsync(Action action, CancellationToken cancellationToken)
        => _syncedExecutor.ExecuteAsync(action, _defaultOptions, cancellationToken);

    /// <inheritdoc/>
    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => _syncedExecutor.ExecuteAsync(action, _defaultOptions, cancellationToken);

    /// <inheritdoc/>
    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, CancellationToken cancellationToken)
        => _syncedExecutor.ExecuteAsyncWithDelay(action, delay, _defaultOptions, cancellationToken);

    /// <inheritdoc/>
    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, CancellationToken cancellationToken)
        => _syncedExecutor.ExecuteAsyncWithDelay(action, delay, _defaultOptions, cancellationToken);

    /// <inheritdoc/>
    public bool VerifyThread()
        => _syncedExecutor.VerifyThread();

#if NETCOREAP
    private readonly TaskCreationOptions _defaultOptions =  TaskCreationOptions.HideScheduler;
#else
    private readonly TaskCreationOptions _defaultOptions = TaskCreationOptions.None;
#endif

    private readonly ThreadPoolExecutor _syncedExecutor;
}