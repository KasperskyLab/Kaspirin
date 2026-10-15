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

namespace Kaspirin.UI.Framework.Threading.Throttlers;

/// <summary>
///     Factory interface for creating deferred action handlers.
/// </summary>
public interface IDeferredActionFactory
{
    /// <summary>
    ///     Creates a handler for deferred action execution in the pool thread.
    /// </summary>
    /// <param name="action">
    ///     The action that will be performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <param name="mode">
    ///     The mode of action execution.
    /// </param>
    /// <param name="options">
    ///     Additional options.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <returns>
    ///     Handler for deferred action execution.
    /// </returns>
    IDeferredAction CreateOnTp(
        Action action,
        TimeSpan delayBeforeAction,
        TimeSpan delayAfterAction,
        string name,
        DeferredActionMode mode,
        DeferredActionOptions options,
        IThreadPoolExecutor executor);

    /// <summary>
    ///     Creates a deferred action handler for execution in the UI thread.
    /// </summary>
    /// <param name="action">
    ///     The action that will be performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <param name="mode">
    ///     The mode of action execution.
    /// </param>
    /// <param name="options">
    ///     Additional options.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <returns>
    ///     Handler for deferred action execution.
    /// </returns>
    IDeferredAction CreateOnUi(
        Action action,
        TimeSpan delayBeforeAction,
        TimeSpan delayAfterAction,
        string name,
        DeferredActionMode mode,
        DeferredActionOptions options,
        DispatcherPriority priority,
        IDispatcherExecutor executor);

    /// <summary>
    ///     Creates a deferred action handler with a parameter to be executed in the pool thread.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the action parameter.
    /// </typeparam>
    /// <param name="action">
    ///     The action that will be performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <param name="mode">
    ///     The mode of action execution.
    /// </param>
    /// <param name="options">
    ///     Additional options.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <returns>
    ///     Handler for the deferred execution of an action with a parameter.
    /// </returns>
    IDeferredAction<T> CreateOnTp<T>(
        Action<T?> action,
        TimeSpan delayBeforeAction,
        TimeSpan delayAfterAction,
        string name,
        DeferredActionMode mode,
        DeferredActionOptions options,
        IThreadPoolExecutor executor);

    /// <summary>
    ///     Creates a handler for the deferred execution of an action with a parameter to be executed in the UI thread.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the action parameter.
    /// </typeparam>
    /// <param name="action">
    ///     The action that will be performed.
    /// </param>
    /// <param name="delayBeforeAction">
    ///     Delay before performing an action.
    /// </param>
    /// <param name="delayAfterAction">
    ///     The delay after the action is completed.
    /// </param>
    /// <param name="name">
    ///     The name of the handler for identification.
    /// </param>
    /// <param name="mode">
    ///     The mode of action execution.
    /// </param>
    /// <param name="options">
    ///     Additional options.
    /// </param>
    /// <param name="priority">
    ///     Execution priority in the thread manager.
    /// </param>
    /// <param name="executor">
    ///     The performer of the action.
    /// </param>
    /// <returns>
    ///     Handler for the deferred execution of an action with a parameter.
    /// </returns>
    IDeferredAction<T> CreateOnUi<T>(
        Action<T?> action,
        TimeSpan delayBeforeAction,
        TimeSpan delayAfterAction,
        string name,
        DeferredActionMode mode,
        DeferredActionOptions options,
        DispatcherPriority priority,
        IDispatcherExecutor executor);
}