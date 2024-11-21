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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Dwmapi
{
    /// <summary>
    ///     Provides API methods for functions from dwmapi.dll .
    /// </summary>
    public static partial class DwmapiDll
    {
        #region dwmapi.h

        /// <summary>
        ///     The DwmSetWindowAttribute API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(
            IntPtr hWnd,
            DwmWindowAttributeType attr,
            IntPtr attrValue,
            int attrSize);

        /// <summary>
        ///     The DwmGetWindowAttribute API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, PreserveSig = true)]
        public static extern int DwmGetWindowAttribute(
            IntPtr hWnd,
            DwmWindowAttributeType attr,
            out bool attrValue,
            int attrSize);

        /// <summary>
        ///     The DwmGetWindowAttribute API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, PreserveSig = true)]
        public static extern int DwmGetWindowAttribute(
            IntPtr hWnd,
            DwmWindowAttributeType attr,
            out NativeRectangle attrValue,
            int attrSize);

        #endregion

        private const string DllName = "dwmapi.dll";
    }
}
