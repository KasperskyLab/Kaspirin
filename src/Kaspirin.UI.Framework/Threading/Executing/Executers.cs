// Copyright © 2024 AO Kaspersky Lab.
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
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading.Executing;

/// <summary>
///     Executes delegates in the UI thread or in pool threads.
/// </summary>
public static class Executers
{
    /// <summary>
    ///     The logic responsible for executing delegates in the UI thread.
    /// </summary>
    public static IDispatcherExecutor DispatcherExecutor { get; set; } = new DispatcherExecutor();

    /// <summary>
    ///     The logic responsible for executing delegates in pool threads.
    /// </summary>
    public static IThreadPoolExecutor ThreadPoolExecutor { get; set; } = new ThreadPoolExecutor();

    /// <summary>
    ///     Executes the <paramref name="action" /> delegate in the UI thread synchronously or asynchronously,
    ///     depending on the value of <paramref name="sync" />.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="sync">
    ///     Execution mode. If <see langword="true" /> - execute synchronously, otherwise - asynchronously.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    public static void InUiSyncOrAsync(Action action, bool sync, DispatcherPriority? priority = null, CancellationToken? cancellationToken = null)
    {
        if (sync)
        {
            InUiSync(action, priority, cancellationToken);
        }
        else
        {
            InUiAsync(action, priority, null, cancellationToken);
        }
    }

    /// <summary>
    ///     Executes the <paramref name="action" /> delegate in the UI thread synchronously or asynchronously,
    ///     depending on which thread the method is called in. If the method is called in the UI thread,
    ///     then synchronously, otherwise asynchronously.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    public static void InUiSyncOrAsync(Action action, DispatcherPriority? priority = null, CancellationToken? cancellationToken = null)
    {
        var needExecuteSync = DispatcherExecutor.VerifyThread();

        InUiSyncOrAsync(action, needExecuteSync, priority, cancellationToken);
    }

    /// <summary>
    ///     The <paramref name="action" /> delegate executes synchronously in the UI thread.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    public static void InUiSync(Action action, DispatcherPriority? priority = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);

        var priorityValue = priority ?? DispatcherPriority.Normal;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        DispatcherExecutor.ExecuteSync(action, priorityValue, cancellationTokenValue);
    }

    /// <summary>
    ///     Synchronously executes the <paramref name="action" /> delegate in the UI thread and returns the result.
    /// </summary>
    /// <typeparam name="TResult">
    ///     The type of the returned value.
    /// </typeparam>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The result of executing <paramref name="action" />.
    /// </returns>
    public static TResult InUiSync<TResult>(Func<TResult> action, DispatcherPriority? priority = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);

        var priorityValue = priority ?? DispatcherPriority.Normal;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        return DispatcherExecutor.ExecuteSync(action, priorityValue, cancellationTokenValue);
    }

    /// <summary>
    ///     Asynchronously executes the <paramref name="action" /> delegate in the UI thread.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="delay">
    ///     The waiting time before starting the delegate execution.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    public static Task InUiAsync(Action action, DispatcherPriority? priority = null, TimeSpan? delay = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);
        Guard.Argument(delay == null || delay >= TimeSpan.Zero);

        var priorityValue = priority ?? DispatcherPriority.Normal;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        return delay == null
            ? DispatcherExecutor.ExecuteAsync(action, priorityValue, cancellationTokenValue)
            : DispatcherExecutor.ExecuteAsyncWithDelay(action, delay.Value, priorityValue, cancellationTokenValue);
    }

    /// <summary>
    ///     Asynchronously executes the delegate <paramref name="action" /> with the return value in the UI thread.
    /// </summary>
    /// <typeparam name="TResult">
    ///     The type of the returned value.
    /// </typeparam>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="delay">
    ///     The waiting time before starting the delegate execution.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    public static Task<TResult> InUiAsync<TResult>(Func<TResult> action, DispatcherPriority? priority = null, TimeSpan? delay = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);
        Guard.Argument(delay == null || delay >= TimeSpan.Zero);

        var priorityValue = priority ?? DispatcherPriority.Normal;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        return delay == null
            ? DispatcherExecutor.ExecuteAsync(action, priorityValue, cancellationTokenValue)
            : DispatcherExecutor.ExecuteAsyncWithDelay(action, delay.Value, priorityValue, cancellationTokenValue);
    }

    /// <summary>
    ///     Executes the delegate <paramref name="action" /> synchronously or asynchronously in the pool
    ///     thread, depending on the value of <paramref name="sync" />.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="sync">
    ///     Execution mode. If <see langword="true" /> - execute synchronously, otherwise - asynchronously.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    public static void InTpSyncOrAsync(Action action, bool sync, TaskCreationOptions? options = null, CancellationToken? cancellationToken = null)
    {
        if (sync)
        {
            InTpSync(action, options, cancellationToken);
        }
        else
        {
            InTpAsync(action, options, null, cancellationToken);
        }
    }

    /// <summary>
    ///     Executes the delegate <paramref name="action" /> synchronously or asynchronously in the pool
    ///     thread, depending on which thread the method is called in. If the method is called in a pool
    ///     thread, then synchronously, otherwise asynchronously.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    public static void InTpSyncOrAsync(Action action, TaskCreationOptions? options = null, CancellationToken? cancellationToken = null)
    {
        var needExecuteSync = ThreadPoolExecutor.VerifyThread();

        InTpSyncOrAsync(action, needExecuteSync, options, cancellationToken);
    }

    /// <summary>
    ///     Synchronously executes the delegate <paramref name="action" /> in the pool thread.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    public static void InTpSync(Action action, TaskCreationOptions? options = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);

        var optionsValue = options ?? TaskCreationOptions.None;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        ThreadPoolExecutor.ExecuteSync(action, optionsValue, cancellationTokenValue);
    }

    /// <summary>
    ///     Synchronously executes the delegate <paramref name="action" /> in the pool thread and returns the result.
    /// </summary>
    /// <typeparam name="TResult">
    ///     The type of the returned value.
    /// </typeparam>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The result of executing <paramref name="action" />.
    /// </returns>
    public static TResult InTpSync<TResult>(Func<TResult> action, TaskCreationOptions? options = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);

        var optionsValue = options ?? TaskCreationOptions.None;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        return ThreadPoolExecutor.ExecuteSync(action, optionsValue, cancellationTokenValue);
    }

    /// <summary>
    ///     Asynchronously executes the delegate <paramref name="action" /> in the pool thread.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="delay">
    ///     The waiting time before starting the delegate execution.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    public static Task InTpAsync(Action action, TaskCreationOptions? options = null, TimeSpan? delay = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);
        Guard.Argument(delay == null || delay >= TimeSpan.Zero);

        var optionsValue = options ?? TaskCreationOptions.None;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        return delay == null
            ? ThreadPoolExecutor.ExecuteAsync(action, optionsValue, cancellationTokenValue)
            : ThreadPoolExecutor.ExecuteAsyncWithDelay(action, delay.Value, optionsValue, cancellationTokenValue);
    }

    /// <summary>
    ///     Asynchronously executes the delegate <paramref name="action" /> with the return value in the pool thread.
    /// </summary>
    /// <typeparam name="TResult">
    ///     The type of the returned value.
    /// </typeparam>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="delay">
    ///     The waiting time before starting the delegate execution.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    public static Task<TResult> InTpAsync<TResult>(Func<TResult> action, TaskCreationOptions? options = null, TimeSpan? delay = null, CancellationToken? cancellationToken = null)
    {
        Guard.ArgumentIsNotNull(action);
        Guard.Argument(delay == null || delay >= TimeSpan.Zero);

        var optionsValue = options ?? TaskCreationOptions.None;
        var cancellationTokenValue = cancellationToken ?? CancellationToken.None;

        return delay == null
            ? ThreadPoolExecutor.ExecuteAsync(action, optionsValue, cancellationTokenValue)
            : ThreadPoolExecutor.ExecuteAsyncWithDelay(action, delay.Value, optionsValue, cancellationTokenValue);
    }
}