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

#pragma warning disable CA1401 // P/Invokes should not be visible

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Gdi32
{
    /// <summary>
    ///     Provides API methods for functions from gdi32.dll .
    /// </summary>
    public static class Gdi32Dll
    {
        #region wingdi.h

        /// <summary>
        ///     The CreateDC API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-createdcw">Learn more</seealso>.
        /// </summary>
        [SecurityCritical]
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern SafeDcHandle CreateDC(
            [MarshalAs(UnmanagedType.LPWStr)] string driver,
            [MarshalAs(UnmanagedType.LPWStr)] string? device,
            IntPtr output,
            IntPtr initData);

        /// <summary>
        ///     The DeleteDC API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-deletedc">Learn more</seealso>.
        /// </summary>
        [SecurityCritical]
        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC(
            IntPtr hDc);

        /// <summary>
        ///     The GetDeviceCaps API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getdevicecaps">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int GetDeviceCaps(
            IntPtr hDc,
            DeviceCapsItems index);

        /// <summary>
        ///     The DeleteObject API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-deleteobject">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(
            [In] IntPtr hObject);

        #endregion

        private const string DllName = "gdi32.dll";
    }
}