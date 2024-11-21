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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Cryptui
{
    /// <summary>
    ///     Provides API methods for functions from cryptui.dll .
    /// </summary>
    public static class CryptuiDll
    {
        #region cryptuiapi.h

        /// <summary>
        ///     The CryptUIDlgViewCertificate API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/cryptuiapi/nf-cryptuiapi-cryptuidlgviewcertificatew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CryptUIDlgViewCertificate(
            ref CryptUiViewCertificateStruct cryptUiViewCertificateStruct,
            ref bool areAnyCertificatePropertiesChanged
        );

        #endregion

        private const string DllName = "cryptui.dll";
    }
}