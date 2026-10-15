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


using System;

namespace Kaspirin.UI.Framework.Threading.Timers;

/// <summary>
///     Provides access to the parameter store with time intervals for timers.
/// </summary>
public sealed class TimerIntervalParameterStorage : IMutableParameterStorage<TimerIntervals>
{
    /// <inheritdoc cref="IMutableParameterStorage.ParameterChanged"/>
    public event Action<Enum> ParameterChanged = id => { };

    /// <inheritdoc cref="IMutableParameterStorage.GetParameter"/>
    public object GetParameter(Enum parameterId)
        => TimeSpan.FromMilliseconds((int)Guard.EnsureArgumentIsInstanceOfType<TimerIntervals>(parameterId));
}
