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

namespace Kaspirin.UI.Framework.Threading.Throttlers;

/// <summary>
///     Interface for the deferred action handler.
/// </summary>
/// <remarks>
///     The execution of an action always takes place one at a time. If a repeated execution request
///     occurs during the execution of an action, such a request will be processed depending on the
///     handler's operating mode (property <see cref="IBaseDeferredAction.Mode" />).
/// </remarks>
public interface IBaseDeferredAction : IDisposable
{
    /// <summary>
    ///     The status of the action.
    /// </summary>
    DeferredActionState State { get; }

    /// <summary>
    ///     The mode of operation of the deferred action handler.
    /// </summary>
    DeferredActionMode Mode { get; }

    /// <summary>
    ///     Additional options.
    /// </summary>
    DeferredActionOptions Options { get; }

    /// <summary>
    ///     Delay before starting an action.
    /// </summary>
    TimeSpan DelayBeforeAction { get; }

    /// <summary>
    ///     The delay after the action is completed.
    /// </summary>
    TimeSpan DelayAfterAction { get; }

    /// <summary>
    ///     The name of the handler for identification.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Cancels the scheduled execution of an action, if possible.
    /// </summary>
    void Cancel();
}
