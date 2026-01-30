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
using System.Windows.Threading;
using Kaspirin.UI.Framework.Threading.Timers.Internals;

namespace Kaspirin.UI.Framework.Threading.Executing;

/// <summary>
///     The delegate executor in the specified thread manager.
/// </summary>
public sealed class DispatcherExecutor : BaseExecutor<Dispatcher>, IDispatcherExecutor
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DispatcherExecutor" /> class using the default
    ///     task factory <see cref="WpfThread.CurrentDispatcher" />.
    /// </summary>
    public DispatcherExecutor()
        : base(() => Guard.EnsureIsNotNull(WpfThread.Current?.CurrentDispatcher))
    {
        WpfThread.RegisterObserver(new WpfThreadObserver(this));

        var dispatcher = WpfThread.Current?.CurrentDispatcher;
        if (dispatcher != null)
        {
            SubscribeOnDispatcherEvents();
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DispatcherExecutor" /> class.
    /// </summary>
    /// <param name="dispatcher">
    ///     The thread manager used for executing delegates.
    /// </param>
    public DispatcherExecutor(Dispatcher dispatcher)
        : base(() => dispatcher)
    {
        SubscribeOnDispatcherEvents();
    }

    /// <inheritdoc/>
    public Task ExecuteAsync(Action action, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var factory = GetFactoryOrThrow();

#if NETCOREAPP
        return factory.InvokeAsync(action, priority, cancellationToken).Task;
#else
        factory.BeginInvoke(priority, () =>
        {
            if (!EnsureNotCancelled(cancellationToken, tcs))
            {
                return;
            }

            action();
            tcs.SetResult(null);
        });

        return tcs.Task;
#endif
    }

    /// <inheritdoc/>
    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled<TResult>(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var factory = GetFactoryOrThrow();

#if NETCOREAPP
        return factory.InvokeAsync(action, priority, cancellationToken).Task;
#else
        factory.BeginInvoke(priority, () =>
        {
            if (!EnsureNotCancelled(cancellationToken, tcs))
            {
                return;
            }

            var result = action();
            tcs.SetResult(result!);
        });

        return tcs.Task;
#endif
    }

    /// <inheritdoc/>
    public void ExecuteSync(Action action, DispatcherPriority priority, CancellationToken cancellationToken)
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

#if NETCOREAPP
        factory.Invoke(action, priority, cancellationToken);
#else
        factory.Invoke(priority, () =>
        {
            if (!EnsureNotCancelled(cancellationToken))
            {
                return;
            }

            action();
        });
#endif
    }

    /// <inheritdoc/>
    public TResult ExecuteSync<TResult>(Func<TResult> action, DispatcherPriority priority, CancellationToken cancellationToken)
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

#if NETCOREAPP
        return factory.Invoke(action, priority, cancellationToken);
#else
        return (TResult)factory.Invoke(priority, () =>
        {
            if (!EnsureNotCancelled(cancellationToken))
            {
                return default!;
            }

            return action();
        });
#endif
    }

    /// <inheritdoc/>
    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled<object?>(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var factory = GetFactoryOrThrow();

        var timer = new DispatcherUnifiedTimer(
            tickAction: () => ExecuteAsync(action, priority, cancellationToken).ContinueOnUi(() => tcs.SetResult(null), priority, cancellationToken: cancellationToken),
            interval: null,
            name: string.Empty,
            options: TimerOptions.None,
            dispatcher: factory,
            priority: priority);

        timer.Tick += (t) => t.Dispose();
        timer.Start(delay);

        return tcs.Task;
    }

    /// <inheritdoc/>
    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        if (!EnsureNotCancelled<TResult>(cancellationToken, out var tcs))
        {
            return tcs.Task;
        }

        var factory = GetFactoryOrThrow();

        var timer = new DispatcherUnifiedTimer(
            tickAction: () => ExecuteAsync(action, priority, cancellationToken).ContinueOnUi(res => tcs.SetResult(res), priority, cancellationToken: cancellationToken),
            interval: null,
            name: string.Empty,
            options: TimerOptions.None,
            dispatcher: factory,
            priority: priority);

        timer.Tick += (t) => t.Dispose();
        timer.Start(delay);

        return tcs.Task;
    }

    /// <inheritdoc/>
    public override Task ExecuteAsync(Action action, CancellationToken cancellationToken)
        => ExecuteAsync(action, DispatcherPriority.Normal, cancellationToken);

    /// <inheritdoc/>
    public override Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteAsync(action, DispatcherPriority.Normal, cancellationToken);

    /// <inheritdoc/>
    public override void ExecuteSync(Action action, CancellationToken cancellationToken)
        => ExecuteSync(action, DispatcherPriority.Normal, cancellationToken);

    /// <inheritdoc/>
    public override TResult ExecuteSync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteSync(action, DispatcherPriority.Normal, cancellationToken);

    /// <inheritdoc/>
    public override Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, CancellationToken cancellationToken)
        => ExecuteAsyncWithDelay(action, delay, DispatcherPriority.Normal, cancellationToken);

    /// <inheritdoc/>
    public override Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, CancellationToken cancellationToken)
        => ExecuteAsyncWithDelay(action, delay, DispatcherPriority.Normal, cancellationToken);

    /// <inheritdoc/>
    public override bool VerifyThread()
        => GetFactoryOrThrow().CheckAccess();

    private void UpdateIsAvailable(object? sender, EventArgs args)
    {
        var factory = GetFactory();

        IsAvailable = !factory.HasShutdownStarted &&
                      !factory.HasShutdownFinished;
    }

    private void SubscribeOnDispatcherEvents()
    {
        var factory = GetFactory();

        factory.ShutdownStarted -= UpdateIsAvailable;
        factory.ShutdownStarted += UpdateIsAvailable;

        factory.ShutdownFinished -= UpdateIsAvailable;
        factory.ShutdownFinished += UpdateIsAvailable;

        UpdateIsAvailable(null, EventArgs.Empty);
    }

    private sealed class WpfThreadObserver : IWpfThreadObserver
    {
        public WpfThreadObserver(DispatcherExecutor executor)
            => _executor = executor;

        public void Created() { }

        public void Started()
            => _executor.SubscribeOnDispatcherEvents();

        private readonly DispatcherExecutor _executor;
    }
}
