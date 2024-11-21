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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Version
{
    /// <summary>
    ///     Provides API methods for functions from version.dll .
    /// </summary>
    public static class VersionDll
    {
        #region winver.h

        /// <summary>
        ///     The GetFileVersionInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winver/nf-winver-getfileversioninfow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        public static extern bool GetFileVersionInfo(
            string filename,
            int handle,
            int size,
            HandleRef infoBuffer);

        /// <summary>
        ///     The GetFileVersionInfoSize API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winver/nf-winver-getfileversioninfosizew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        public static extern int GetFileVersionInfoSize(
            string fileName,
            out int handle);

        /// <summary>
        ///     The GetFileVersionInfoSize API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winver/nf-winver-verqueryvaluew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, BestFitMapping = false)]
        public static extern bool VerQueryValue(
            HandleRef info,
            string subBlock,
            out IntPtr value,
            out uint length);

        #endregion

        private const string DllName = "version.dll";
    }
}
