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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/ne-shobjidl_core-sigdn">Learn more</seealso>.
/// </summary>
public enum SiGetDisplayName : uint
{
    /// <summary>
    ///     The SIGDN_NORMALDISPLAY constant.
    /// </summary>
    NormalDisplay = 0x00000000,

    /// <summary>
    ///     The SIGDN_PARENTRELATIVEPARSING constant.
    /// </summary>
    ParentRelativeParsing = 0x80018001,

    /// <summary>
    ///     The SIGDN_DESKTOPABSOLUTEPARSING constant.
    /// </summary>
    DesktopAbsoluteParsing = 0x80028000,

    /// <summary>
    ///     The SIGDN_PARENTRELATIVEEDITING constant.
    /// </summary>
    ParentRelativeEditing = 0x80031001,

    /// <summary>
    ///     The SIGDN_DESKTOPABSOLUTEEDITING constant.
    /// </summary>
    DesktopAbsoluteEditing = 0x8004c000,

    /// <summary>
    ///     The SIGDN_FILESYSPATH constant.
    /// </summary>
    FileSysPath = 0x80058000,

    /// <summary>
    ///     The SIGDN_URL constant.
    /// </summary>
    Url = 0x80068000,

    /// <summary>
    ///     The SIGDN_PARENTRELATIVEFORADDRESSBAR constant.
    /// </summary>
    ParentRelativeForAddressBar = 0x8007c001,

    /// <summary>
    ///     The SIGDN_PARENTRELATIVE constant.
    /// </summary>
    ParentRelative = 0x80080001,

    /// <summary>
    ///     The SIGDN_PARENTRELATIVEFORUI constant.
    /// </summary>
    ParentRelativeForUi = 0x80094001,
}
