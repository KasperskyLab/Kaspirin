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
///     The mode of operation of the deferred execution handler.
/// </summary>
public enum DeferredActionMode
{
    /// <summary>
    ///     Frequency limitation mode (throttle).
    /// </summary>
    /// <remarks>
    ///     It is used to prevent calls from being too frequent.
    ///     <para /> The principle of operation:
    ///     <br /> - The first call triggers the execution of the action.
    ///     <br /> - Repeated calls are ignored until <see cref="IBaseDeferredAction.State" /> will not switch
    ///     to the state <see cref="DeferredActionState.Idle" />.
    /// </remarks>
    Throttle,

    /// <summary>
    ///     Deferred execution mode (debounce).
    /// </summary>
    /// <remarks>
    ///     It is used to react to the completion of a series of calls.
    ///     <para /> The principle of operation:
    ///     <br /> - The first call starts a timer <see cref="IBaseDeferredAction.DelayBeforeAction" />,
    ///     at the end of which the action is performed.
    ///     <br /> - Repeated calls during the active timer <see cref="IBaseDeferredAction.DelayBeforeAction" /> restart the timer.
    ///     <br /> - Repeated calls after the timer ends <see cref="IBaseDeferredAction.DelayBeforeAction" />
    ///     are ignored until <see cref="IBaseDeferredAction.State" /> will not switch to the state <see cref="DeferredActionState.Idle" />.
    /// </remarks>
    Debounce
}