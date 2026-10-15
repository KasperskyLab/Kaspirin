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

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Cryptui.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/cryptuiapi/ns-cryptuiapi-cryptui_viewcertificate_structa">Learn more</seealso>.
/// </summary>
[Flags]
public enum CryptUiViewFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The CRYPTUI_HIDE_HIERARCHYPAGE constant.
    /// </summary>
    HideHierarchyPage = 0x00000001,

    /// <summary>
    ///     The CRYPTUI_HIDE_DETAILPAGE constant.
    /// </summary>
    HideDetailPage = 0x00000002,

    /// <summary>
    ///     The CRYPTUI_DISABLE_EDITPROPERTIES constant.
    /// </summary>
    DisableEditProperties = 0x00000004,

    /// <summary>
    ///     The CRYPTUI_ENABLE_EDITPROPERTIES constant.
    /// </summary>
    EnableEditProperties = 0x00000008,

    /// <summary>
    ///     The CRYPTUI_DISABLE_ADDTOSTORE constant.
    /// </summary>
    DisableAddToStore = 0x00000010,

    /// <summary>
    ///     The CRYPTUI_ENABLE_ADDTOSTORE constant.
    /// </summary>
    EnableAddToStore = 0x00000020,

    /// <summary>
    ///     The CRYPTUI_ACCEPT_DECLINE_STYLE constant.
    /// </summary>
    AcceptDeclineStyle = 0x00000040,

    /// <summary>
    ///     The CRYPTUI_IGNORE_UNTRUSTED_ROOT constant.
    /// </summary>
    IgnoreUntrustedRoot = 0x00000080,

    /// <summary>
    ///     The CRYPTUI_DONT_OPEN_STORES constant.
    /// </summary>
    DontOpenStores = 0x00000100,

    /// <summary>
    ///     The CRYPTUI_ONLY_OPEN_ROOT_STORE constant.
    /// </summary>
    OnlyOpenRootStore = 0x00000200,

    /// <summary>
    ///     The CRYPTUI_WARN_UNTRUSTED_ROOT constant.
    /// </summary>
    WarnUntrustedRoot = 0x00000400,

    /// <summary>
    ///     The CRYPTUI_ENABLE_REVOCATION_CHECKING constant.
    /// </summary>
    EnableRevocationChecking = 0x00000800,

    /// <summary>
    ///     The CRYPTUI_WARN_REMOTE_TRUST constant.
    /// </summary>
    WarnRemoteTrust = 0x00001000,

    /// <summary>
    ///     The CRYPTUI_DISABLE_EXPORT constant.
    /// </summary>
    DisableExport = 0x00002000,

    /// <summary>
    ///     The CRYPTUI_ENABLE_REVOCATION_CHECK_END_CERT constant.
    /// </summary>
    EnableRevocationCheckEndCert = 0x00004000,

    /// <summary>
    ///     The CRYPTUI_ENABLE_REVOCATION_CHECK_CHAIN constant.
    /// </summary>
    EnableRevocationCheckChain = 0x00008000,

    /// <summary>
    ///     The CRYPTUI_ENABLE_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT constant.
    /// </summary>
    EnableRevocationCheckChainExcludeRoot = EnableRevocationChecking,

    /// <summary>
    ///     The CRYPTUI_DISABLE_HTMLLINK constant.
    /// </summary>
    DisableHtmlLink = 0x00010000,

    /// <summary>
    ///     The CRYPTUI_DISABLE_ISSUERSTATEMENT constant.
    /// </summary>
    DisableIssuerStatement = 0x00020000,

    /// <summary>
    ///     The CRYPTUI_CACHE_ONLY_URL_RETRIEVAL constant.
    /// </summary>
    CacheOnlyUrlRetrieval = 0x00040000,
}
