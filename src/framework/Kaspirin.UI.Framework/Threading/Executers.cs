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
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading;

/// <summary>
///     Executes delegates in the UI thread.
/// </summary>
public static class Executers
{
    /// <summary>
    ///     The logic responsible for executing delegates.
    /// </summary>
    public static IUiThreadExecutor Implementation { get; set; } = new WpfUiThreadExecutor();

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
    ///     Priority of execution.
    /// </param>
    public static void InUiSyncOrAsync(this Action action, bool sync, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        if (sync)
        {
            InUiSync(action, priority);
        }
        else
        {
            InUiAsync(action, priority);
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
    ///     Priority of execution.
    /// </param>
    public static void InUiSyncOrAsync(this Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        var needExecuteSync = Implementation.IsUiThread;

        InUiSyncOrAsync(action, needExecuteSync, priority);
    }

    /// <summary>
    ///     The <paramref name="action" /> delegate executes synchronously in the UI thread.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Priority of execution.
    /// </param>
    public static void InUiSync(this Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        Guard.ArgumentIsNotNull(action);

        Implementation.ExecuteInUiThreadSync(action, priority);
    }

    /// <summary>
    ///     Synchronously executes the <paramref name="action" /> delegate in the UI thread and returns the result.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Priority of execution.
    /// </param>
    /// <returns>
    ///     The result of executing <paramref name="action" />.
    /// </returns>
    public static TResult InUiSync<TResult>(this Func<TResult> action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        Guard.ArgumentIsNotNull(action);

        return Implementation.ExecuteInUiThreadSync(action, priority);
    }

    /// <summary>
    ///     Asynchronously executes the <paramref name="action" /> delegate in the UI thread.
    /// </summary>
    /// <param name="action">
    ///     A delegate to execute.
    /// </param>
    /// <param name="priority">
    ///     Priority of execution.
    /// </param>
    /// <returns>
    ///     The task in which the delegate will be executed.
    /// </returns>
    public static Task InUiAsync(this Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        Guard.ArgumentIsNotNull(action);

        return Implementation.ExecuteInUiThreadAsync(action, priority);
    }
}