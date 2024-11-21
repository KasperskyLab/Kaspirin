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

#pragma warning disable  CA1416 // This call site is reachable on all platforms

using System;
using Microsoft.Win32;
using WpfSystemEvents = Microsoft.Win32.SystemEvents;

namespace Kaspirin.UI.Framework.SystemInfo.Internals
{
    internal sealed class SystemEvents : ISystemEvents
    {
        #region ISystemEvents

        public event EventHandler PaletteChanged
        {
            add { InUi(() => WpfSystemEvents.PaletteChanged += value); }
            remove { WpfSystemEvents.PaletteChanged -= value; }
        }

        public event TimerElapsedEventHandler TimerElapsed
        {
            add { InUi(() => WpfSystemEvents.TimerElapsed += value); }
            remove { WpfSystemEvents.TimerElapsed -= value; }
        }

        public event EventHandler TimeChanged
        {
            add { InUi(() => WpfSystemEvents.TimeChanged += value); }
            remove { WpfSystemEvents.TimeChanged -= value; }
        }

        public event SessionSwitchEventHandler SessionSwitch
        {
            add { InUi(() => WpfSystemEvents.SessionSwitch += value); }
            remove { WpfSystemEvents.SessionSwitch -= value; }
        }

        public event SessionEndingEventHandler SessionEnding
        {
            add { InUi(() => WpfSystemEvents.SessionEnding += value); }
            remove { WpfSystemEvents.SessionEnding -= value; }
        }

        public event SessionEndedEventHandler SessionEnded
        {
            add { InUi(() => WpfSystemEvents.SessionEnded += value); }
            remove { WpfSystemEvents.SessionEnded -= value; }
        }

        public event PowerModeChangedEventHandler PowerModeChanged
        {
            add { InUi(() => WpfSystemEvents.PowerModeChanged += value); }
            remove { WpfSystemEvents.PowerModeChanged -= value; }
        }

        public event UserPreferenceChangedEventHandler UserPreferenceChanged
        {
            add { InUi(() => WpfSystemEvents.UserPreferenceChanged += value); }
            remove { WpfSystemEvents.UserPreferenceChanged -= value; }
        }

        public event UserPreferenceChangingEventHandler UserPreferenceChanging
        {
            add { InUi(() => WpfSystemEvents.UserPreferenceChanging += value); }
            remove { WpfSystemEvents.UserPreferenceChanging -= value; }
        }

        public event EventHandler InstalledFontsChanged
        {
            add { InUi(() => WpfSystemEvents.InstalledFontsChanged += value); }
            remove { WpfSystemEvents.InstalledFontsChanged -= value; }
        }

        public event EventHandler EventsThreadShutdown
        {
            add { InUi(() => WpfSystemEvents.EventsThreadShutdown += value); }
            remove { WpfSystemEvents.EventsThreadShutdown -= value; }
        }

        public event EventHandler DisplaySettingsChanged
        {
            add { InUi(() => WpfSystemEvents.DisplaySettingsChanged += value); }
            remove { WpfSystemEvents.DisplaySettingsChanged -= value; }
        }

        public event EventHandler DisplaySettingsChanging
        {
            add { InUi(() => WpfSystemEvents.DisplaySettingsChanging += value); }
            remove { WpfSystemEvents.DisplaySettingsChanging -= value; }
        }

        public IntPtr CreateTimer(int interval) => WpfSystemEvents.CreateTimer(interval);
        public void InvokeOnEventsThread(Delegate method) => WpfSystemEvents.InvokeOnEventsThread(method);
        public void KillTimer(IntPtr timerId) => WpfSystemEvents.KillTimer(timerId);

        #endregion

        private static void InUi(Action action)
        {
            Executers.InUiAsync(action);
        }
    }
}
