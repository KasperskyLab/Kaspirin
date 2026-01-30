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
///     The base class of the delegate executor.
/// </summary>
/// <typeparam name="TFactory">
///     The type of factory for running delegates.
/// </typeparam>
public abstract class BaseExecutor<TFactory> : IExecutor where TFactory : class
{
    internal BaseExecutor(Func<TFactory> factory)
    {
        Tracer = ComponentTracer.Get(ComponentTracers.Threading, this);

        _taskFactory = new Lazy<TFactory>(factory);
    }

    /// <inheritdoc/>
    public bool IsAvailable
    {
        get => _isAvailable;
        protected set
        {
            if (_isAvailable != value)
            {
                _isAvailable = value;

                Tracer.TraceInformation($"IsAvailable changed to {_isAvailable}");

                IsAvailableChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <inheritdoc/>
    public event EventHandler? IsAvailableChanged;

    /// <inheritdoc/>
    public abstract bool VerifyThread();

    /// <inheritdoc/>
    public abstract void ExecuteSync(Action action, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract TResult ExecuteSync<TResult>(Func<TResult> action, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task ExecuteAsync(Action action, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, CancellationToken cancellationToken);

    /// <summary>
    ///     The message tracer.
    /// </summary>
    protected ComponentTracer Tracer { get; }

    /// <summary>
    ///     Gets the current factory for running delegates.
    /// </summary>
    /// <returns>
    ///     Factory for launching delegates.
    /// </returns>
    protected TFactory GetFactory()
    {
        return Guard.EnsureIsNotNull(_taskFactory.Value);
    }

    /// <summary>
    ///     Retrieves the current factory for running delegates, or throws an exception if running delegates is not possible.
    /// </summary>
    /// <returns>
    ///     Factory for launching delegates.
    /// </returns>
    protected TFactory GetFactoryOrThrow()
    {
        Guard.Assert(IsAvailable);

        return GetFactory();
    }

    /// <summary>
    ///     Checks that the specified <paramref name="CancellationToken" /> has not been canceled.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Cancellation token for verification.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the token has not been canceled, otherwise <see langword="false" />.
    /// </returns>
    protected bool EnsureNotCancelled(CancellationToken cancellationToken)
        => EnsureNotCancelled(cancellationToken, out _);

    /// <summary>
    ///     Verifies that the specified <paramref name="CancellationToken" /> has not been canceled, and
    ///     creates a task completion source <paramref name="tcs" />.
    /// </summary>
    /// <remarks>
    ///     If the cancellation token has already been requested, it sets the cancellation status to <paramref name="tcs" />.
    /// </remarks>
    /// <param name="cancellationToken">
    ///     Cancellation token for verification.
    /// </param>
    /// <param name="tcs">
    ///     The created task completion source.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the token has not been canceled, otherwise <see langword="false" />.
    /// </returns>
#if NETCOREAPP
    protected bool EnsureNotCancelled(CancellationToken cancellationToken, out TaskCompletionSource tcs)
    {
        tcs = new TaskCompletionSource();
#else
    protected bool EnsureNotCancelled(CancellationToken cancellationToken, out TaskCompletionSource<object?> tcs)
    {
        tcs = new TaskCompletionSource<object?>();
#endif
        return EnsureNotCancelled(cancellationToken, tcs);
    }

    /// <summary>
    ///     Checks that the specified <paramref name="CancellationToken" /> has not been canceled.
    /// </summary>
    /// <remarks>
    ///     If the cancellation token has already been requested, it sets the cancellation status to <paramref name="tcs" />.
    /// </remarks>
    /// <param name="cancellationToken">
    ///     Cancellation token for verification.
    /// </param>
    /// <param name="tcs">
    ///     The source of the task completion.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the token has not been canceled, otherwise <see langword="false" />.
    /// </returns>
#if NETCOREAPP
    protected bool EnsureNotCancelled(CancellationToken cancellationToken, TaskCompletionSource tcs)
    {
#else
    protected bool EnsureNotCancelled(CancellationToken cancellationToken, TaskCompletionSource<object?> tcs)
    {
#endif
        if (cancellationToken.IsCancellationRequested)
        {
#if NETCOREAPP
            tcs.SetCanceled(cancellationToken);
#else
            tcs.SetCanceled();
#endif
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Verifies that the specified <paramref name="CancellationToken" /> has not been canceled, and
    ///     creates a task completion source <paramref name="tcs" />.
    /// </summary>
    /// <remarks>
    ///     If the cancellation token has already been requested, it sets the cancellation status to <paramref name="tcs" />.
    /// </remarks>
    /// <typeparam name="TResult">
    ///     The type of task result.
    /// </typeparam>
    /// <param name="cancellationToken">
    ///     Cancellation token for verification.
    /// </param>
    /// <param name="tcs">
    ///     The created task completion source.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the token has not been canceled, otherwise <see langword="false" />.
    /// </returns>
    protected bool EnsureNotCancelled<TResult>(CancellationToken cancellationToken, out TaskCompletionSource<TResult> tcs)
    {
        tcs = new TaskCompletionSource<TResult>();

        return EnsureNotCancelled(cancellationToken, tcs);
    }

    /// <summary>
    ///     Checks that the specified <paramref name="CancellationToken" /> has not been canceled.
    /// </summary>
    /// <remarks>
    ///     If the cancellation token has already been requested, it sets the cancellation status to <paramref name="tcs" />.
    /// </remarks>
    /// <typeparam name="TResult">
    ///     The type of task result.
    /// </typeparam>
    /// <param name="cancellationToken">
    ///     Cancellation token for verification.
    /// </param>
    /// <param name="tcs">
    ///     The source of the task completion.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the token has not been canceled, otherwise <see langword="false" />.
    /// </returns>
    protected bool EnsureNotCancelled<TResult>(CancellationToken cancellationToken, TaskCompletionSource<TResult> tcs)
    {
        if (cancellationToken.IsCancellationRequested)
        {
#if NETCOREAPP
            tcs.SetCanceled(cancellationToken);
#else
            tcs.SetCanceled();
#endif
            return false;
        }

        return true;
    }

    private readonly Lazy<TFactory> _taskFactory;
    private bool _isAvailable;
}
