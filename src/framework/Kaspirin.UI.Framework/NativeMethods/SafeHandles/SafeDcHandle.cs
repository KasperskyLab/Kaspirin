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

#pragma warning disable SYSLIB0004 // The constrained execution region (CER) feature is not supported

using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Kaspirin.UI.Framework.NativeMethods.SafeHandles
{
    /// <summary>
    ///     Device context descriptor (DC).
    /// </summary>
    [SecurityCritical]
    public sealed class SafeDcHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        [SecurityCritical]
        internal SafeDcHandle() : base(true)
        {
        }

        /// <summary>
        ///     Creates a DC descriptor for the device using the specified name.
        /// </summary>
        /// <returns>
        ///     The DC descriptor for the device.
        /// </returns>
        [SecurityCritical]
        public static SafeDcHandle CreateDeviceDc(string deviceName)
        {
            var safeDc = (SafeDcHandle?)null;
            try
            {
                safeDc = Gdi32Dll.CreateDC(deviceName, null, IntPtr.Zero, IntPtr.Zero);
            }
            finally
            {
                if (safeDc != null)
                {
                    safeDc._created = true;
                }
            }

            if (safeDc is { IsInvalid: true })
            {
                safeDc.Dispose();

                throw new InvalidOperationException($"Unable to create a device context for device:{deviceName}.");
            }

            return safeDc;
        }

        /// <summary>
        ///     Provides the number of pixels per logical inch (DPI) along the width of the screen.
        /// </summary>
        /// <returns>
        ///     The DPI value is based on the screen width.
        /// </returns>
        [SecurityCritical]
        public int GetDeviceCapsX()
            => Gdi32Dll.GetDeviceCaps(handle, DeviceCapsItems.LOGPIXELSX);

        /// <summary>
        ///     Provides the number of pixels per logical inch (DPI) along the height of the screen.
        /// </summary>
        /// <returns>
        ///     The DPI value for the height of the screen.
        /// </returns>
        [SecurityCritical]
        public int GetDeviceCapsY()
            => Gdi32Dll.GetDeviceCaps(handle, DeviceCapsItems.LOGPIXELSY);

        /// <summary>
        ///     Provides a DC descriptor for the screen.
        /// </summary>
        /// <returns>
        ///     The DC descriptor for the screen.
        /// </returns>
        [SecurityCritical]
        public static SafeDcHandle GetScreenDc()
            => GetWindowDc(IntPtr.Zero);

        /// <summary>
        ///     Provides a DC descriptor for the specified window.
        /// </summary>
        /// <param name="hwnd">
        ///     The window handle.
        /// </param>
        /// <returns>
        ///     The DC descriptor for the window.
        /// </returns>
        [SecurityCritical]
        public static SafeDcHandle GetWindowDc(IntPtr hwnd)
        {
            var safeDc = (SafeDcHandle?)null;
            try
            {
                safeDc = User32Dll.GetDC(hwnd);
            }
            finally
            {
                if (safeDc != null)
                {
                    safeDc._hwnd = hwnd;
                }
            }

            if (safeDc is { IsInvalid: true })
            {
                safeDc.Dispose();

                throw new InvalidOperationException($"Unable to get a device context for HWND:{hwnd}");
            }

            return safeDc;
        }

        /// <inheritdoc />
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            if (_created)
            {
                return Gdi32Dll.DeleteDC(handle);
            }

            if (!_hwnd.HasValue || _hwnd.Value == IntPtr.Zero)
            {
                return true;
            }

            return User32Dll.ReleaseDC(_hwnd.Value, handle) == 1;
        }

        [SecurityCritical]
        private IntPtr? _hwnd;
        private bool _created;
    }
}
