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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable KCAIDE0002 // Enum has incorrect suffix

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Cryptui.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/cryptuiapi/ns-cryptuiapi-cryptui_viewcertificate_structa">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum CryptUiViewFlags : uint
    {
        None = 0,
        CRYPTUI_HIDE_HIERARCHYPAGE = 0x00000001,
        CRYPTUI_HIDE_DETAILPAGE = 0x00000002,
        CRYPTUI_DISABLE_EDITPROPERTIES = 0x00000004,
        CRYPTUI_ENABLE_EDITPROPERTIES = 0x00000008,
        CRYPTUI_DISABLE_ADDTOSTORE = 0x00000010,
        CRYPTUI_ENABLE_ADDTOSTORE = 0x00000020,
        CRYPTUI_ACCEPT_DECLINE_STYLE = 0x00000040,
        CRYPTUI_IGNORE_UNTRUSTED_ROOT = 0x00000080,
        CRYPTUI_DONT_OPEN_STORES = 0x00000100,
        CRYPTUI_ONLY_OPEN_ROOT_STORE = 0x00000200,
        CRYPTUI_WARN_UNTRUSTED_ROOT = 0x00000400,
        CRYPTUI_ENABLE_REVOCATION_CHECKING = 0x00000800,
        CRYPTUI_WARN_REMOTE_TRUST = 0x00001000,
        CRYPTUI_DISABLE_EXPORT = 0x00002000,
        CRYPTUI_ENABLE_REVOCATION_CHECK_END_CERT = 0x00004000,
        CRYPTUI_ENABLE_REVOCATION_CHECK_CHAIN = 0x00008000,
        CRYPTUI_ENABLE_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = CRYPTUI_ENABLE_REVOCATION_CHECKING,
        CRYPTUI_DISABLE_HTMLLINK = 0x00010000,
        CRYPTUI_DISABLE_ISSUERSTATEMENT = 0x00020000,
        CRYPTUI_CACHE_ONLY_URL_RETRIEVAL = 0x00040000,
    }
}
