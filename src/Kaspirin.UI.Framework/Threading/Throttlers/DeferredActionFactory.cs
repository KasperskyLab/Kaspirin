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
using System.Windows.Threading;
using Kaspirin.UI.Framework.Threading.Throttlers.Internals;

namespace Kaspirin.UI.Framework.Threading.Throttlers;

/// <summary>
///     A factory for creating deferred action handlers.
/// </summary>
public static class DeferredActionFactory
{
    /// <summary>
    ///     The factory object used by the methods in <see cref="DeferredActionFactory" />.
    /// </summary>
    public static IDeferredActionFactory Implementation { get; set; } = new DeferredActionFactoryImpl();

    /// <summary>
    ///     Creates an action handler in deferred execution mode (debounce).
    /// </summary>
    /// <remarks>
    ///     The action is performed in the pool thread.
    /// </remarks>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Debounce mode.
    /// </returns>
    public static IDeferredAction CreateDebouncerOnTp(
        Action action,
        TimeSpan delayBeforeAction,
        DeferredActionOptions options = DeferredActionOptions.None,
        IThreadPoolExecutor? executor = null,
        string? name = null)
    {
        executor ??= Executers.ThreadPoolExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnTp(
            action: action,
            delayBeforeAction: delayBeforeAction,
            delayAfterAction: TimeSpan.Zero,
            name: name,
            mode: DeferredActionMode.Debounce,
            options: options,
            executor: executor);
    }

    /// <summary>
    ///     Creates an action handler in deferred execution mode (debounce).
    /// </summary>
    /// <remarks>
    ///     The action is performed in the UI thread.
    /// </remarks>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Debounce mode.
    /// </returns>
    public static IDeferredAction CreateDebouncerOnUi(
        Action action,
        TimeSpan delayBeforeAction,
        DeferredActionOptions options = DeferredActionOptions.None,
        DispatcherPriority priority = DispatcherPriority.Normal,
        IDispatcherExecutor? executor = null,
        string? name = null)
    {
        executor ??= Executers.DispatcherExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnUi(
            action: action,
            delayBeforeAction: delayBeforeAction,
            delayAfterAction: TimeSpan.Zero,
            name: name,
            mode: DeferredActionMode.Debounce,
            options: options,
            priority: priority,
            executor: executor);
    }

    /// <summary>
    ///     Creates an action handler in the throttle mode.
    /// </summary>
    /// <remarks>
    ///     The action is performed in the pool thread.
    /// </remarks>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Throttle mode.
    /// </returns>
    public static IDeferredAction CreateThrottlerOnTp(
        Action action,
        TimeSpan? delayAfterAction = null,
        DeferredActionOptions options = DeferredActionOptions.None,
        IThreadPoolExecutor? executor = null,
        string? name = null)
    {
        delayAfterAction ??= TimeSpan.Zero;
        executor ??= Executers.ThreadPoolExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnTp(
            action: action,
            delayBeforeAction: TimeSpan.Zero,
            delayAfterAction: delayAfterAction.Value,
            name: name,
            mode: DeferredActionMode.Throttle,
            options: options,
            executor: executor);
    }

    /// <summary>
    ///     Creates an action handler in the throttle mode.
    /// </summary>
    /// <remarks>
    ///     The action is performed in the UI thread.
    /// </remarks>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Throttle mode.
    /// </returns>
    public static IDeferredAction CreateThrottlerOnUi(
        Action action,
        TimeSpan? delayAfterAction = null,
        DeferredActionOptions options = DeferredActionOptions.None,
        DispatcherPriority priority = DispatcherPriority.Normal,
        IDispatcherExecutor? executor = null,
        string? name = null)
    {
        delayAfterAction ??= TimeSpan.Zero;
        executor ??= Executers.DispatcherExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnUi(
            action: action,
            delayBeforeAction: TimeSpan.Zero,
            delayAfterAction: delayAfterAction.Value,
            name: name,
            mode: DeferredActionMode.Throttle,
            options: options,
            priority: priority,
            executor: executor);
    }

    /// <summary>
    ///     Creates an action handler with a parameter in deferred execution mode (debounce).
    /// </summary>
    /// <remarks>
    ///     The action is performed in the pool thread.
    /// </remarks>
    /// <typeparam name="T">
    ///     The type of the action parameter.
    /// </typeparam>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Debounce mode.
    /// </returns>
    public static IDeferredAction<T> CreateDebouncerOnTp<T>(
        Action<T?> action,
        TimeSpan delayBeforeAction,
        DeferredActionOptions options = DeferredActionOptions.None,
        IThreadPoolExecutor? executor = null,
        string? name = null)
    {
        executor ??= Executers.ThreadPoolExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnTp(
            action: action,
            delayBeforeAction: delayBeforeAction,
            delayAfterAction: TimeSpan.Zero,
            name: name,
            mode: DeferredActionMode.Debounce,
            options: options,
            executor: executor);
    }

    /// <summary>
    ///     Creates an action handler with a parameter in deferred execution mode (debounce).
    /// </summary>
    /// <remarks>
    ///     The action is performed in the UI thread.
    /// </remarks>
    /// <typeparam name="T">
    ///     The type of the action parameter.
    /// </typeparam>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Debounce mode.
    /// </returns>
    public static IDeferredAction<T> CreateDebouncerOnUi<T>(
        Action<T?> action,
        TimeSpan delayBeforeAction,
        DeferredActionOptions options = DeferredActionOptions.None,
        DispatcherPriority priority = DispatcherPriority.Normal,
        IDispatcherExecutor? executor = null,
        string? name = null)
    {
        executor ??= Executers.DispatcherExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnUi(
            action: action,
            delayBeforeAction: delayBeforeAction,
            delayAfterAction: TimeSpan.Zero,
            name: name,
            mode: DeferredActionMode.Debounce,
            options: options,
            priority: priority,
            executor: executor);
    }

    /// <summary>
    ///     Creates an action handler with a parameter in the throttle mode.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the action parameter.
    /// </typeparam>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Throttle mode.
    /// </returns>
    public static IDeferredAction<T> CreateThrottlerOnTp<T>(
        Action<T?> action,
        TimeSpan? delayAfterAction = null,
        DeferredActionOptions options = DeferredActionOptions.None,
        IThreadPoolExecutor? executor = null,
        string? name = null)
    {
        delayAfterAction ??= TimeSpan.Zero;
        executor ??= Executers.ThreadPoolExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnTp(
            action: action,
            delayBeforeAction: TimeSpan.Zero,
            delayAfterAction: delayAfterAction.Value,
            name: name,
            mode: DeferredActionMode.Throttle,
            options: options,
            executor: executor);
    }

    /// <summary>
    ///     Creates an action handler with a parameter in the throttle mode.
    /// </summary>
    /// <remarks>
    ///     The action is performed in the UI thread.
    /// </remarks>
    /// <typeparam name="T">
    ///     The type of the action parameter.
    /// </typeparam>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="options">
    ///     Additional options for customizing behavior.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <returns>
    ///     Handler for deferred execution in Throttle mode.
    /// </returns>
    public static IDeferredAction<T> CreateThrottlerOnUi<T>(
        Action<T?> action,
        TimeSpan? delayAfterAction = null,
        DeferredActionOptions options = DeferredActionOptions.None,
        DispatcherPriority priority = DispatcherPriority.Normal,
        IDispatcherExecutor? executor = null,
        string? name = null)
    {
        delayAfterAction ??= TimeSpan.Zero;
        executor ??= Executers.DispatcherExecutor;
        name ??= string.Empty;

        return Implementation.CreateOnUi(
            action: action,
            delayBeforeAction: TimeSpan.Zero,
            delayAfterAction: delayAfterAction.Value,
            name: name,
            mode: DeferredActionMode.Throttle,
            options: options,
            priority: priority,
            executor: executor);
    }

    private sealed class DeferredActionFactoryImpl : IDeferredActionFactory
    {
        public IDeferredAction CreateOnTp(
            Action action,
            TimeSpan delayBeforeAction,
            TimeSpan delayAfterAction,
            string name,
            DeferredActionMode mode,
            DeferredActionOptions options,
            IThreadPoolExecutor executor)
        {
            var parameters = new DeferredActionParameters(
                action: action,
                delayAfterAction: delayAfterAction,
                delayBeforeAction: delayBeforeAction,
                name: name,
                options: options,
                mode: mode);

            return new ThreadPoolDeferredAction(parameters, executor);
        }

        public IDeferredAction CreateOnUi(
            Action action,
            TimeSpan delayBeforeAction,
            TimeSpan delayAfterAction,
            string name,
            DeferredActionMode mode,
            DeferredActionOptions options,
            DispatcherPriority priority,
            IDispatcherExecutor executor)
        {
            var parameters = new DeferredActionParameters(
                action: action,
                delayAfterAction: delayAfterAction,
                delayBeforeAction: delayBeforeAction,
                name: name,
                options: options,
                mode: mode);

            return new DispatcherDeferredAction(parameters, priority, executor);
        }

        public IDeferredAction<T> CreateOnTp<T>(
            Action<T?> action,
            TimeSpan delayBeforeAction,
            TimeSpan delayAfterAction,
            string name,
            DeferredActionMode mode,
            DeferredActionOptions options,
            IThreadPoolExecutor executor)
        {
            var parameters = new DeferredActionParameters<T>(
                action: action,
                delayAfterAction: delayAfterAction,
                delayBeforeAction: delayBeforeAction,
                name: name,
                options: options,
                mode: mode);

            return new ThreadPoolDeferredAction<T>(parameters, executor);
        }

        public IDeferredAction<T> CreateOnUi<T>(
            Action<T?> action,
            TimeSpan delayBeforeAction,
            TimeSpan delayAfterAction,
            string name,
            DeferredActionMode mode,
            DeferredActionOptions options,
            DispatcherPriority priority,
            IDispatcherExecutor executor)
        {
            var parameters = new DeferredActionParameters<T>(
                action: action,
                delayAfterAction: delayAfterAction,
                delayBeforeAction: delayBeforeAction,
                name: name,
                options: options,
                mode: mode);

            return new DispatcherDeferredAction<T>(parameters, priority, executor);
        }
    }
}
