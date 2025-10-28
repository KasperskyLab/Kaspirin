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
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading;

/// <summary>
///     Executes delegates in the UI thread using the WPF application manager Application.Current.Dispatcher.
/// </summary>
public sealed class WpfUiThreadExecutor : IUiThreadExecutor
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WpfUiThreadExecutor" /> class.
    /// </summary>
    public WpfUiThreadExecutor()
    {
        _tracer = ComponentTracer.Get(ComponentTracers.Threading, this);
    }

    /// <inheritdoc />
    public bool ThrowIfNotAvailable { get; set; } = true;

    /// <inheritdoc />
    public bool IsAvailable => TryGetApplication(out _);

    /// <inheritdoc />
    public bool IsUiThread => GetApplication().CheckAccess();

    /// <inheritdoc />
    public void ExecuteInUiThreadSync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        if (!CheckIsAvailable())
        {
            _tracer.TraceMethodWarning("Cant execute action in UI thread now.");
            return;
        }

        var application = GetApplication();

        if (application.CheckAccess())
        {
            action();
            return;
        }

        application.Dispatcher.Invoke(priority, action);
    }

    /// <inheritdoc />
    public TResult ExecuteInUiThreadSync<TResult>(Func<TResult> action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        if (!CheckIsAvailable())
        {
            _tracer.TraceMethodWarning("Cant execute action in UI thread now.");
            return default!;
        }

        var application = GetApplication();

        if (application.CheckAccess())
        {
            return action();
        }

        return (TResult)application.Dispatcher.Invoke(priority, action);
    }

    /// <inheritdoc />
    public Task ExecuteInUiThreadAsync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        var tcs = new TaskCompletionSource<object>();

        if (!CheckIsAvailable())
        {

#if NETCOREAPP
            return Task.CompletedTask;
#else
            tcs.SetResult(null!);
            return tcs.Task;
#endif
        }

        var application = GetApplication();

#if NETCOREAPP
        return application.Dispatcher.BeginInvoke(priority, action).Task;
#else

        application.Dispatcher.BeginInvoke(priority, (Action)(() =>
        {
            action();
            tcs.SetResult(null!);
        }));

        return tcs.Task;
#endif
    }

    private bool CheckIsAvailable()
    {
        if (!IsAvailable)
        {
            const string err = "Cant execute action in UI thread now.";

            if (ThrowIfNotAvailable)
            {
                throw new InvalidOperationException(err);
            }

            _tracer.TraceMethodWarning("Cant execute action in UI thread now.");
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Gets the current instance of the application from <see cref="Application.Current" />.
    /// </summary>
    /// <returns>
    ///     The current instance of the application.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     It is thrown if the value of <see cref="Application.Current" /> is <see langword="null" />.
    /// </exception>
    private static Application GetApplication()
        => TryGetApplication(out var application)
            ? application
            : throw new InvalidOperationException("Application.Current is null");

    /// <summary>
    ///     Checks the availability of the current application instance <see cref="Application.Current" />.
    /// </summary>
    /// <param name="application">
    ///     The current instance of the application.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if <see cref="Application.Current" /> is not equal to <see langword="null" />,
    ///     otherwise - <see langword="false" />.
    /// </returns>
    private static bool TryGetApplication([NotNullWhen(true)] out Application? application)
        => (application = Application.Current) != null;

    private readonly ComponentTracer _tracer;
}
