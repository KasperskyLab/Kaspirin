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
using Kaspirin.UI.Framework.Threading.Timers.Internals;

namespace Kaspirin.UI.Framework.Threading.Executing;

/// <summary>
///     The delegate executor in the pool threads.
/// </summary>
public sealed class ThreadPoolExecutor : BaseExecutor<TaskFactory>, IThreadPoolExecutor
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ThreadPoolExecutor" /> class using the default
    ///     task factory <see cref="Task.Factory" />.
    /// </summary>
    public ThreadPoolExecutor()
        : base(() => Task.Factory)
    {
        IsAvailable = true;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ThreadPoolExecutor" /> class.
    /// </summary>
    /// <param name="taskFactory">
    ///     A task factory used to execute delegates.
    /// </param>
    public ThreadPoolExecutor(TaskFactory taskFactory)
        : base(() => taskFactory)
    {
        IsAvailable = true;
    }

    /// <inheritdoc/>
    public void ExecuteSync(Action action, TaskCreationOptions options, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled(cancellationToken))
        {
            return;
        }

        if (VerifyThread())
        {
            action();
            return;
        }

        var factory = GetFactoryOrThrow();
        var factoryScheduler = factory.Scheduler ?? TaskScheduler.Default;

        options |= _defaultOptions;

        var task = factory.StartNew(action, cancellationToken, options, factoryScheduler);
        task.Wait(cancellationToken);
    }

    /// <inheritdoc/>
    public TResult ExecuteSync<TResult>(Func<TResult> action, TaskCreationOptions options, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled(cancellationToken))
        {
            return default!;
        }

        if (VerifyThread())
        {
            return action();
        }

        var factory = GetFactoryOrThrow();
        var factoryScheduler = factory.Scheduler ?? TaskScheduler.Default;

        options |= _defaultOptions;

        var task = factory.StartNew(action, cancellationToken, options, factoryScheduler);
        task.Wait(cancellationToken);

        return task.IsCanceled
            ? default!
            : task.Result;
    }

    /// <inheritdoc/>
    public Task ExecuteAsync(Action action, TaskCreationOptions options, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var factory = GetFactoryOrThrow();
        var factoryScheduler = factory.Scheduler ?? TaskScheduler.Default;

        options |= _defaultOptions;

        return factory.StartNew(action, cancellationToken, options, factoryScheduler);
    }

    /// <inheritdoc/>
    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, TaskCreationOptions options, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled<TResult>(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var factory = GetFactoryOrThrow();
        var factoryScheduler = factory.Scheduler ?? TaskScheduler.Default;

        options |= _defaultOptions;

        return factory.StartNew(action, cancellationToken, options, factoryScheduler);
    }

    /// <inheritdoc/>
    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, TaskCreationOptions options, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled<object?>(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var timer = new ThreadPoolUnifiedTimer(
            tickAction: () => ExecuteAsync(action, options, cancellationToken).ContinueWith(t => tcs.SetResult(null), cancellationToken),
            interval: null,
            name: string.Empty,
            options: TimerOptions.None);

        timer.Tick += (t) => t.Dispose();
        timer.Start(delay);

        return tcs.Task;
    }

    /// <inheritdoc/>
    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, TaskCreationOptions options, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled<TResult>(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var timer = new ThreadPoolUnifiedTimer(
            tickAction: () => ExecuteAsync(action, options, cancellationToken).ContinueWith(t => tcs.SetResult(t.Result), cancellationToken),
            interval: null,
            name: string.Empty,
            options: TimerOptions.None);

        timer.Tick += (t) => t.Dispose();
        timer.Start(delay);

        return tcs.Task;
    }

    /// <inheritdoc/>
    public override void ExecuteSync(Action action, CancellationToken cancellationToken)
        => ExecuteSync(action, TaskCreationOptions.None, cancellationToken);

    /// <inheritdoc/>
    public override TResult ExecuteSync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteSync(action, TaskCreationOptions.None, cancellationToken);

    /// <inheritdoc/>
    public override Task ExecuteAsync(Action action, CancellationToken cancellationToken)
        => ExecuteAsync(action, TaskCreationOptions.None, cancellationToken);

    /// <inheritdoc/>
    public override Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteAsync(action, TaskCreationOptions.None, cancellationToken);

    /// <inheritdoc/>
    public override Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, CancellationToken cancellationToken)
        => ExecuteAsyncWithDelay(action, delay, TaskCreationOptions.None, cancellationToken);

    /// <inheritdoc/>
    public override Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, CancellationToken cancellationToken)
        => ExecuteAsyncWithDelay(action, delay, TaskCreationOptions.None, cancellationToken);

    /// <inheritdoc/>
    public override bool VerifyThread()
        => Thread.CurrentThread.IsThreadPoolThread;

#if NETCOREAP
    private readonly TaskCreationOptions _defaultOptions =  TaskCreationOptions.RunContinuationsAsynchronously;
#else
    private readonly TaskCreationOptions _defaultOptions = TaskCreationOptions.None;
#endif
}
