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

#pragma warning disable CA1416 // This call site is reachable on all platforms

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.Storage
{
    /// <summary>
    ///     Monitors changes in the Windows registry.
    /// </summary>
    public sealed class WinRegistryTracker : IRegistryTracker
    {
        /// <inheritdoc cref="IRegistryTracker.TrackChangesAsync" />
        public Task TrackChangesAsync(RegistryHive hive, RegistryView view, string regPath, CancellationToken cancellationToken, Action changed)
        {
            Guard.ArgumentIsNotNullOrEmpty(regPath);
            Guard.ArgumentIsNotNull(changed);

            return Task.Factory.StartNew(() =>
            {
                using var key = RegistryKey.OpenBaseKey(hive, view).OpenSubKey(regPath);

                if (key == null)
                {
                    _tracer.TraceWarning($"Key for '{regPath}' not found");
                    return;
                }

                try
                {
                    _tracer.TraceInformation($"Started tracking '{key}' of registry changes");

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        using var keyChanged = new AutoResetEvent(false);

                        TrackKeyChanges(key, keyChanged);

                        var handlers = new[]
                        {
                            keyChanged,
                            cancellationToken.WaitHandle
                        };

                        if (WaitHandle.WaitAny(handlers) == Array.IndexOf(handlers, keyChanged))
                        {
                            Task.Factory.StartNew(changed);
                        }
                    }
                }
                finally
                {
                    _tracer.TraceInformation($"Stopped tracking '{key}' of registry changes");
                }
            }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private static void TrackKeyChanges(RegistryKey key, WaitHandle waitHandle)
        {
            var regKey = key.Handle.DangerousGetHandle();
            var notification = waitHandle.SafeWaitHandle.DangerousGetHandle();
            var filter = RegNotifyChangeKeyValueFlags.REG_NOTIFY_CHANGE_LAST_SET;

            var resultCode = Advapi32Dll.RegNotifyChangeKeyValue(regKey, watchSubtree: false, filter, notification, isAsynchronous: true);

            if (resultCode != 0)
            {
                throw new SystemException($"Failed to track changes on '{key}'. Win32Error=0x{Marshal.GetLastWin32Error():X}");
            }
        }

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(nameof(WinRegistryTracker));
    }
}
