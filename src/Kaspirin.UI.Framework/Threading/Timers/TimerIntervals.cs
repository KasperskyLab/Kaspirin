// Copyright © 2026 AO Kaspersky Lab.
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
///     The time intervals in milliseconds used to set the timers.
/// </summary>
public enum TimerIntervals
{
    /// <summary>
    ///     The default interval is 1 second.
    /// </summary>
    Default = 1000,

    /// <summary>
    ///     Short interval: 500 milliseconds.
    /// </summary>
    Short = 500,

    /// <summary>
    ///     Average interval: 2 seconds.
    /// </summary>
    Medium = 2000,

    /// <summary>
    ///     Long interval: 5 seconds.
    /// </summary>
    Long = 5000,

    /// <summary>
    ///     Polling interval: 15 seconds.
    /// </summary>
    ShortPolling = 15_000,

    /// <summary>
    ///     Polling interval: 1 minute.
    /// </summary>
    MediumPolling = 60_000,

    /// <summary>
    ///     Polling interval: 15 minutes.
    /// </summary>
    LongPolling = 900_000,

    /// <summary>
    ///     The interval for an hourly survey is 1 hour.
    /// </summary>
    EveryHour = 3_600_000,

    /// <summary>
    ///     The interval for the daily survey is 1 day.
    /// </summary>
    EveryDay = 86_400_000
}
