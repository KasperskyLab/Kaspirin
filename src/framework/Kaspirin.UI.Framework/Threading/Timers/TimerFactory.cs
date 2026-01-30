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
using Kaspirin.UI.Framework.Threading.Timers.Internals;

namespace Kaspirin.UI.Framework.Threading.Timers;

/// <summary>
///     A factory for creating timers.
/// </summary>
public static class TimerFactory
{
    /// <summary>
    ///     The factory object used by the methods in <see cref="TimerFactory" />.
    /// </summary>
    public static ITimerFactory Implementation { get; set; } = new TimerFactoryImpl();

    /// <inheritdoc cref="ITimerFactory.CreateOnTp" />
    public static ITimer CreateOnTp(
        Action onTimer,
        TimeSpan? interval = null,
        TimerOptions options = TimerOptions.None,
        string? name = null)
    {
        name ??= string.Empty;

        return Implementation.CreateOnTp(onTimer, interval, name, options);
    }

    /// <inheritdoc cref="ITimerFactory.CreateOnUi" />
    public static ITimer CreateOnUi(
        Action onTimer,
        TimeSpan? interval = null,
        DispatcherPriority priority = DispatcherPriority.Background,
        TimerOptions options = TimerOptions.None,
        string? name = null)
    {
        name ??= string.Empty;

        return Implementation.CreateOnUi(onTimer, interval, name, options, priority);
    }

    private sealed class TimerFactoryImpl : ITimerFactory
    {
        public ITimer CreateOnTp(Action onTimer, TimeSpan? interval, string name, TimerOptions options)
        {
            return new ThreadPoolUnifiedTimer(onTimer, interval, name, options);
        }

        public ITimer CreateOnUi(Action onTimer, TimeSpan? interval, string name, TimerOptions options, DispatcherPriority priority)
        {
            var dispatcher = Guard.EnsureIsNotNull(WpfThread.Current?.CurrentDispatcher);

            return new DispatcherUnifiedTimer(onTimer, interval, name, options, dispatcher, priority);
        }
    }
}
