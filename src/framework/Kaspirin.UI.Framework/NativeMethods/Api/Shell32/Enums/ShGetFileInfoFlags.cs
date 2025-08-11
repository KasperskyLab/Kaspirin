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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetfileinfow">Learn more</seealso>.
/// </summary>
[Flags]
public enum ShGetFileInfoFlags : uint
{
    /// <summary>
    ///     The SHGFI_LARGEICON constant.
    /// </summary>
    LargeIcon = 0x000000000,

    /// <summary>
    ///     The SHGFI_SMALLICON constant.
    /// </summary>
    SmallIcon = 0x000000001,

    /// <summary>
    ///     The SHGFI_OPENICON constant.
    /// </summary>
    OpenIcon = 0x000000002,

    /// <summary>
    ///     The SHGFI_SHELLICONSIZE constant.
    /// </summary>
    ShellIconSize = 0x000000004,

    /// <summary>
    ///     The SHGFI_PIDL constant.
    /// </summary>
    Pidl = 0x000000008,

    /// <summary>
    ///     The SHGFI_USEFILEATTRIBUTES constant.
    /// </summary>
    UseFileAttributes = 0x000000010,

    /// <summary>
    ///     The SHGFI_ADDOVERLAYS constant.
    /// </summary>
    AddOverlays = 0x000000020,

    /// <summary>
    ///     The SHGFI_OVERLAYINDEX constant.
    /// </summary>
    OverlayIndex = 0x000000040,

    /// <summary>
    ///     The SHGFI_ICON constant.
    /// </summary>
    Icon = 0x000000100,

    /// <summary>
    ///     The SHGFI_DISPLAYNAME constant.
    /// </summary>
    DisplayName = 0x000000200,

    /// <summary>
    ///     The SHGFI_TYPENAME constant.
    /// </summary>
    TypeName = 0x000000400,

    /// <summary>
    ///     The SHGFI_ATTRIBUTES constant.
    /// </summary>
    Attributes = 0x000000800,

    /// <summary>
    ///     The SHGFI_ICONLOCATION constant.
    /// </summary>
    IconLocation = 0x000001000,

    /// <summary>
    ///     The SHGFI_EXETYPE constant.
    /// </summary>
    ExeType = 0x000002000,

    /// <summary>
    ///     The SHGFI_SYSICONINDEX constant.
    /// </summary>
    SysIconIndex = 0x000004000,

    /// <summary>
    ///     The SHGFI_LINKOVERLAY constant.
    /// </summary>
    LinkOverlay = 0x000008000,

    /// <summary>
    ///     The SHGFI_SELECTED constant.
    /// </summary>
    Selected = 0x000010000,

    /// <summary>
    ///     The SHGFI_ATTR_SPECIFIED constant.
    /// </summary>
    AttrSpecified = 0x000020000,
}