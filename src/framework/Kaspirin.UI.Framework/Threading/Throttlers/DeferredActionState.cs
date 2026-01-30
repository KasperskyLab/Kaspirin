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

namespace Kaspirin.UI.Framework.Threading.Throttlers;

/// <summary>
///     The status of the deferred action handler.
/// </summary>
public enum DeferredActionState
{
    /// <summary>
    ///     Inaction.
    /// </summary>
    /// <remarks>
    ///     In this state, you can schedule a new action to be performed.
    /// </remarks>
    Idle,

    /// <summary>
    ///     There is a delay before performing an action.
    /// </summary>
    DelayBeforeAction,

    /// <summary>
    ///     An action is being performed.
    /// </summary>
    ActionRunning,

    /// <summary>
    ///     There is a delay after the action is completed.
    /// </summary>
    DelayAfterAction,

    /// <summary>
    ///     The previous action has been canceled.
    /// </summary>
    /// <remarks>
    ///     In this state, you can schedule a new action to be performed.
    /// </remarks>
    Cancelled,

    /// <summary>
    ///     The handler has been deleted.
    /// </summary>
    /// <remarks>
    ///     The handler object can no longer be used. All attempts to interact with the object in this state are ignored.
    /// </remarks>
    Disposed,
}
