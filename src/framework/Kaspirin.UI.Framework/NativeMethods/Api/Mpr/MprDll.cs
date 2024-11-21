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

using System.Runtime.InteropServices;
using System.Text;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Mpr
{
    /// <summary>
    ///     Provides API methods for functions from mpr.dll .
    /// </summary>
    public static class MprDll
    {
        #region winnetwk.h

        /// <summary>
        ///     The WNetGetUniversalName API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnetwk/nf-winnetwk-wnetgetuniversalnamew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = false, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern int WNetGetUniversalName(
            string lpLocalPath,
            [MarshalAs(UnmanagedType.U4)] InfoLevel infoLevel,
            StringBuilder lpBuffer,
            [MarshalAs(UnmanagedType.U4)] ref int lpBufferSize);

        #endregion

        private const string DllName = "mpr.dll";
    }
}

