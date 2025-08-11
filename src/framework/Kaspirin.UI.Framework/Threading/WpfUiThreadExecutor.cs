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
public class WpfUiThreadExecutor : IUiThreadExecutor
{
    /// <inheritdoc />
    public virtual bool CanExecuteInUiThread => TryGetApplication(out _);

    /// <inheritdoc />
    public virtual bool IsUiThread => GetApplication().CheckAccess();

    /// <inheritdoc />
    public virtual void ExecuteInUiThreadSync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        var application = GetApplication();

        if (application.CheckAccess())
        {
            action();
            return;
        }

        application.Dispatcher.Invoke(priority, action);
    }

    /// <inheritdoc />
    public virtual TResult ExecuteInUiThreadSync<TResult>(Func<TResult> action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        var application = GetApplication();

        if (application.CheckAccess())
        {
            return action();
        }

        return (TResult)application.Dispatcher.Invoke(priority, action);
    }

    /// <inheritdoc />
    public virtual Task ExecuteInUiThreadAsync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        var application = GetApplication();

#if NETCOREAPP
        return application.Dispatcher.BeginInvoke(priority, action).Task;
#else
        var tcs = new TaskCompletionSource<object>();
        application.Dispatcher.BeginInvoke(priority, (Action)(() =>
        {
            action();
            tcs.SetResult(null!);
        }));

        return tcs.Task;
#endif
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
    protected static Application GetApplication()
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
    protected static bool TryGetApplication([NotNullWhen(true)] out Application? application)
        => (application = Application.Current) != null;
}
