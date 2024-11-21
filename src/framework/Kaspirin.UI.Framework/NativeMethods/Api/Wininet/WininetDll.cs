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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Wininet
{
    /// <summary>
    ///     Provides API methods for functions from wininet.dll .
    /// </summary>
    public static class WininetDll
    {
        #region wininet.h

        /// <summary>
        ///     The InternetSetCookieEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wininet/nf-wininet-internetsetcookieexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true, EntryPoint = "InternetSetCookieExW")]
        [SuppressUnmanagedCodeSecurity]
        [SecurityCritical]
        public static extern uint InternetSetCookieEx(
            [In] string url,
            [In] string cookieName,
            [In] string cookieData,
            InternetCookieFlags flags,
            [In] string pHeader);

        /// <summary>
        ///     The InternetOpen API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wininet/nf-wininet-internetopena">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr InternetOpen(
            string agentName,
            InternetOpenType accessType,
            string proxyName,
            string proxyBypass,
            InternetOpenFlags flags);

        /// <summary>
        ///     The InternetCloseHandle API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wininet/nf-wininet-internetclosehandle">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InternetCloseHandle(
            IntPtr handle);

        /// <summary>
        ///     The InternetSetOption API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wininet/nf-wininet-internetsetoptionw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool InternetSetOption(
            IntPtr handle,
            InternetOptions option,
            IntPtr buffer,
            int bufferSize);

        #endregion

        private const string DllName = "wininet.dll";
    }
}
