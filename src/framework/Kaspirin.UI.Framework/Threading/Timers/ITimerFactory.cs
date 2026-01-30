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

namespace Kaspirin.UI.Framework.Threading.Timers;

/// <summary>
///     Factory interface for creating timers.
/// </summary>
public interface ITimerFactory
{
    /// <summary>
    ///     Creates a timer with an indication of the action, interval, and parameters, where each tick
    ///     is executed in the pool thread.
    /// </summary>
    /// <param name="onTimer">
    ///     The action performed at each tick of the timer.
    /// </param>
    /// <param name="interval">
    ///     The interval between timer ticks.
    ///     <br /> The value <see langword="null" /> indicates that the timer should not be repeated.
    /// </param>
    /// <param name="name">
    ///     The name of the timer for identification.
    /// </param>
    /// <param name="options">
    ///     Additional timer options.
    /// </param>
    /// <returns>
    ///     The created timer is in the stopped state.
    /// </returns>
    ITimer CreateOnTp(
        Action onTimer,
        TimeSpan? interval,
        string name,
        TimerOptions options);

    /// <summary>
    ///     Creates a timer with an indication of the action, interval, and parameters, where each tick is executed in the UI thread.
    /// </summary>
    /// <param name="onTimer">
    ///     The action performed at each tick of the timer.
    /// </param>
    /// <param name="interval">
    ///     The interval between timer ticks.
    ///     <br /> The value <see langword="null" /> indicates that the timer should not be repeated.
    /// </param>
    /// <param name="name">
    ///     The name of the timer for identification.
    /// </param>
    /// <param name="options">
    ///     Additional timer options.
    /// </param>
    /// <param name="priority">
    ///     The priority of the timer in the flow manager.
    /// </param>
    /// <returns>
    ///     The created timer is in the stopped state.
    /// </returns>
    ITimer CreateOnUi(
        Action onTimer,
        TimeSpan? interval,
        string name,
        TimerOptions options,
        DispatcherPriority priority);
}
