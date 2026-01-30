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
///     Additional options for configuring the behavior of the deferred action handler <see cref="IBaseDeferredAction" />.
/// </summary>
[Flags]
public enum DeferredActionOptions
{
    /// <summary>
    ///     Do not apply additional options.
    /// </summary>
    None = 0x0000,

    /// <summary>
    ///     Store a weak reference to the delegate of the action being performed.
    /// </summary>
    /// <remarks>
    ///     If the reference to the action delegate is collected by the garbage collector, the deferred
    ///     action will be automatically canceled, and the state is <see cref="IBaseDeferredAction.State" />
    ///     will take the value <see cref="DeferredActionState.Disposed" />.
    /// </remarks>
    Weak = 0x0001,

    /// <summary>
    ///     Perform the last deferred action that was skipped.
    /// </summary>
    /// <remarks>
    ///     If the flag is set, then after the end of the current action, a new action will be scheduled
    ///     with the last argument of the missed action <see cref="IDeferredAction{TArg}.Execute(TArg)" />, if there were such calls.
    /// </remarks>
    RunLastSkippedAction = 0x0002,

    /// <summary>
    ///     Enable logging of the action.
    /// </summary>
    EnableTraces = 0x0004,
}
