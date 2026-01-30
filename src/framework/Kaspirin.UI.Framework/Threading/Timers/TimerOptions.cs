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

namespace Kaspirin.UI.Framework.Threading.Timers;

/// <summary>
///     Additional options for configuring timer behavior <see cref="ITimer" />.
/// </summary>
[Flags]
public enum TimerOptions
{
    /// <summary>
    ///     Do not apply additional options.
    /// </summary>
    None = 0x0000,

    /// <summary>
    ///     Store a weak reference to the delegate of the action performed on each tick.
    /// </summary>
    /// <remarks>
    ///     If the reference to the action delegate is collected by the garbage collector, the timer will automatically stop.
    /// </remarks>
    Weak = 0x0001,

    /// <summary>
    ///     Enable timer logging.
    /// </summary>
    EnableTraces = 0x0002,
}
