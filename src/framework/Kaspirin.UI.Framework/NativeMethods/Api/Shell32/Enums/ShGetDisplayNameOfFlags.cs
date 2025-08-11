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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/ne-shobjidl_core-_shgdnf">Learn more</seealso>.
/// </summary>
[Flags]
public enum ShGetDisplayNameOfFlags : uint
{
    /// <summary>
    ///     The SHGDN_NORMAL constant.
    /// </summary>
    Normal = 0x0000,

    /// <summary>
    ///     The SHGDN_INFOLDER constant.
    /// </summary>
    InFolder = 0x0001,

    /// <summary>
    ///     The SHGDN_FOREDITING constant.
    /// </summary>
    ForEditing = 0x1000,

    /// <summary>
    ///     The SHGDN_FORADDRESSBAR constant.
    /// </summary>
    ForAddressBar = 0x4000,

    /// <summary>
    ///     The SHGDN_FORPARSING constant.
    /// </summary>
    ForParsing = 0x8000,
}