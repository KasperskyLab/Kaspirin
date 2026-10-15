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
using System.Threading;

namespace Kaspirin.UI.Framework.Threading.Timers;

/// <summary>
///     A timer interface with the ability to manage its lifecycle and track its execution.
/// </summary>
public interface ITimer : IDisposable
{
    /// <summary>
    ///     An event that occurs when the timer's <see cref="State" /> state changes.
    /// </summary>
    event Action<ITimer> StateChanged;

    /// <summary>
    ///     An event that occurs at each tick of the timer.
    /// </summary>
    event Action<ITimer> Tick;

    /// <summary>
    ///     An object waiting for actions to be performed when the timer is ticking. The signal is set
    ///     when all the actions of the last tick are completed.
    /// </summary>
    WaitHandle TickExecutionEvent { get; }

    /// <summary>
    ///     Gets or sets the interval between timer ticks.
    /// </summary>
    /// <remarks>
    ///     The value <see langword="null" /> indicates that the timer should not be repeated. Setting
    ///     the <see langword="null" /> value for a running timer stops it.
    /// </remarks>
    TimeSpan? Interval { get; set; }

    /// <summary>
    ///     Retrieves or sets a user-defined data object for the timer.
    /// </summary>
    object? Tag { get; set; }

    /// <summary>
    ///     Additional timer options.
    /// </summary>
    TimerOptions Options { get; }

    /// <summary>
    ///     Timer status.
    /// </summary>
    TimerState State { get; }

    /// <summary>
    ///     Gets a value indicating whether the timer is running.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    ///     The name of the timer for identification.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Starts the timer.
    /// </summary>
    /// <param name="delay">
    ///     The delay before the first tick.
    /// </param>
    /// <remarks>
    ///     If the delay <paramref name="delay" /> is not specified, the first tick will occur immediately after the method is called.
    /// </remarks>
    /// <returns>
    ///     Returns <see langword="true" /> if the timer is successfully started, otherwise - <see langword="false" />.
    /// </returns>
    bool Start(TimeSpan? delay = null);

    /// <summary>
    ///     Restarts the timer, or starts it if it is not running.
    /// </summary>
    /// <param name="delay">
    ///     The delay before the first tick.
    /// </param>
    /// <remarks>
    ///     If the delay <paramref name="delay" /> is not specified, the first tick will occur immediately after the method is called.
    /// </remarks>
    /// <returns>
    ///     Returns <see langword="true" /> if the timer has been successfully restarted, otherwise - <see langword="false" />.
    /// </returns>
    bool Restart(TimeSpan? delay = null);

    /// <summary>
    ///     Stops the timer, preventing further ticks.
    /// </summary>
    /// <remarks>
    ///     A stopped timer can be restarted.
    /// </remarks>
    /// <returns>
    ///     Returns <see langword="true" /> if the timer is successfully stopped, otherwise - <see langword="false" />.
    /// </returns>
    bool Stop();
}
