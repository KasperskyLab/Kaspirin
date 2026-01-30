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

namespace Kaspirin.UI.Framework.Threading.Timers;

/// <summary>
///     Timer status.
/// </summary>
public enum TimerState
{
    /// <summary>
    ///     The timer has not started yet.
    /// </summary>
    NeverRun,

    /// <summary>
    ///     The timer is running.
    /// </summary>
    Running,

    /// <summary>
    ///     The timer is stopped.
    /// </summary>
    Stopped,

    /// <summary>
    ///     The timer has been deleted.
    /// </summary>
    /// <remarks>
    ///     The timer object can no longer be used. All attempts to interact with the object in this state are ignored.
    /// </remarks>
    Disposed,
}
