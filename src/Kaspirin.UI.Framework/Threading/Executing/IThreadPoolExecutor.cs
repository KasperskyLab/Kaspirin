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
///     Interface for executing delegates in pool threads.
/// </summary>
public interface IThreadPoolExecutor : IExecutor
{
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
    void ExecuteSync(Action action, TaskCreationOptions options, CancellationToken cancellationToken);

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
    ///     The result of the delegate execution.
    /// </returns>
    TResult ExecuteSync<TResult>(Func<TResult> action, TaskCreationOptions options, CancellationToken cancellationToken);

    /// <summary>
    ///     Asynchronously executes the delegate <paramref name="action" /> in the pool thread.
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
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    Task ExecuteAsync(Action action, TaskCreationOptions options, CancellationToken cancellationToken);

    /// <summary>
    ///     Asynchronously executes the delegate <paramref name="action" /> in the pool thread and returns the result.
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
    ///     The task in which the delegate will be executed.
    /// </returns>
    Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, TaskCreationOptions options, CancellationToken cancellationToken);

    /// <summary>
    ///     Asynchronously executes the delegate <paramref name="action" /> in the pool thread after waiting
    ///     for the time specified in <paramref name="delay" />.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="delay">
    ///     The waiting time before starting the delegate execution.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, TaskCreationOptions options, CancellationToken cancellationToken);

    /// <summary>
    ///     Asynchronously executes the delegate <paramref name="action" /> with the return value in the
    ///     pool thread after waiting for the time specified in <paramref name="delay" />.
    /// </summary>
    /// <typeparam name="TResult">
    ///     The type of the returned value.
    /// </typeparam>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="delay">
    ///     The waiting time before starting the delegate execution.
    /// </param>
    /// <param name="options">
    ///     Parameters of the task being created, in which the delegate will be executed.
    /// </param>
    /// <param name="cancellationToken">
    ///     Delegate cancellation token.
    /// </param>
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, TaskCreationOptions options, CancellationToken cancellationToken);
}
