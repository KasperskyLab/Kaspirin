// Copyright © 2024 AO Kaspersky Lab.
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
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.SystemInfo
{
    /// <summary>
    ///     Interface for working with static events and methods from <see cref="SystemEvents" />.
    /// </summary>
    public interface ISystemEvents
    {
        /// <inheritdoc cref="SystemEvents.PaletteChanged"/>
        event EventHandler PaletteChanged;

        /// <inheritdoc cref="SystemEvents.TimerElapsed"/>
        event TimerElapsedEventHandler TimerElapsed;

        /// <inheritdoc cref="SystemEvents.TimeChanged"/>
        event EventHandler TimeChanged;

        /// <inheritdoc cref="SystemEvents.SessionSwitch"/>
        event SessionSwitchEventHandler SessionSwitch;

        /// <inheritdoc cref="SystemEvents.SessionEnding"/>
        event SessionEndingEventHandler SessionEnding;

        /// <inheritdoc cref="SystemEvents.SessionEnded"/>
        event SessionEndedEventHandler SessionEnded;

        /// <inheritdoc cref="SystemEvents.PowerModeChanged"/>
        event PowerModeChangedEventHandler PowerModeChanged;

        /// <inheritdoc cref="SystemEvents.UserPreferenceChanged"/>
        event UserPreferenceChangedEventHandler UserPreferenceChanged;

        /// <inheritdoc cref="SystemEvents.UserPreferenceChanging"/>
        event UserPreferenceChangingEventHandler UserPreferenceChanging;

        /// <inheritdoc cref="SystemEvents.InstalledFontsChanged"/>
        event EventHandler InstalledFontsChanged;

        /// <inheritdoc cref="SystemEvents.EventsThreadShutdown"/>
        event EventHandler EventsThreadShutdown;

        /// <inheritdoc cref="SystemEvents.DisplaySettingsChanged"/>
        event EventHandler DisplaySettingsChanged;

        /// <inheritdoc cref="SystemEvents.DisplaySettingsChanging"/>
        event EventHandler DisplaySettingsChanging;

        /// <inheritdoc cref="SystemEvents.CreateTimer"/>
        IntPtr CreateTimer(int interval);

        /// <inheritdoc cref="SystemEvents.InvokeOnEventsThread"/>
        void InvokeOnEventsThread(Delegate method);

        /// <inheritdoc cref="SystemEvents.KillTimer"/>
        void KillTimer(IntPtr timerId);
    }
}
