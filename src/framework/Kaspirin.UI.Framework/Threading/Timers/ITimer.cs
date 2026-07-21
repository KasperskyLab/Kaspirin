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
///     A timer interface with the ability to manage its lifecycle and track its execution.
/// </summary>
public interface ITimer : IDisposable
{
    /// <summary>
    ///     The event that occurs when the timer starts.
    /// </summary>
    event Action<ITimer> Started;

    /// <summary>
    ///     The event that occurs when the timer stops.
    /// </summary>
    event Action<ITimer> Stopped;

    /// <summary>
    ///     An event that occurs at each tick of the timer.
    /// </summary>
    event Action<ITimer> Tick;

    /// <summary>
    ///     Gets or sets the interval between timer ticks.
    /// </summary>
    /// <remarks>
    ///     The value <see langword="null" /> indicates that the timer should not be repeated.
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
    /// <remarks>
    ///     The first timer tick will occur immediately after the method is called.
    /// </remarks>
    /// <returns>
    ///     Returns <see langword="true" /> if the timer is successfully started, otherwise - <see langword="false" />.
    /// </returns>
    bool Start();

    /// <summary>
    ///     Starts the timer using <paramref name="delay" /> as the delay before the first tick.
    /// </summary>
    /// <param name="delay">
    ///     The delay before the first tick.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the timer is successfully started, otherwise - <see langword="false" />.
    /// </returns>
    bool StartAfterDelay(TimeSpan delay);

    /// <summary>
    ///     Starts the timer using <see cref="Interval" /> as the delay before the first tick.
    /// </summary>
    /// <returns>
    ///     Returns <see langword="true" /> if the timer is successfully started, otherwise - <see langword="false" />.
    /// </returns>
    bool StartAfterInterval();

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
