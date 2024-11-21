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

namespace Kaspirin.UI.Framework.SystemInfo.Internals
{
    internal sealed class SessionProvider : ISessionProvider
    {
        public SessionProvider(ISystemEvents systemEvents)
        {
            Guard.ArgumentIsNotNull(systemEvents);

            systemEvents.SessionSwitch += (s, e) =>
            {
                IsSessionActive = IsActiveSession(e.Reason);

                _trace.TraceInformation($"SessionSwitch {e.Reason}");

                if (IsSessionActive)
                {
                    OnActivateSession.Invoke();
                }
                else
                {
                    OnDeactivateSession.Invoke();
                }
            };
        }

        public event Action OnActivateSession = () => { };

        public event Action OnDeactivateSession = () => { };

        public bool IsSessionActive { get; private set; } = true;

        private static bool IsActiveSession(SessionSwitchReason reason)
        {
            return reason switch
            {
                SessionSwitchReason.ConsoleConnect or
                SessionSwitchReason.RemoteConnect or
                SessionSwitchReason.SessionLogon or
                SessionSwitchReason.SessionUnlock => true,
                _ => false,
            };
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(ComponentTracers.SystemInfo);
    }
}
