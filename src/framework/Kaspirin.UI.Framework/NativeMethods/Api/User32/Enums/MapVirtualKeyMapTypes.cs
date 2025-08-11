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

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyw">Learn more</seealso>.
/// </summary>
public enum MapVirtualKeyMapTypes : uint
{
    /// <summary>
    ///     The MAPVK_VK_TO_VSC constant.
    /// </summary>
    VkToVsc = 0x00,

    /// <summary>
    ///     The MAPVK_VSC_TO_VK constant.
    /// </summary>
    VscToVk = 0x01,

    /// <summary>
    ///     The MAPVK_VK_TO_CHAR constant.
    /// </summary>
    VkToChar = 0x02,

    /// <summary>
    ///     The MAPVK_VSC_TO_VK_EX constant.
    /// </summary>
    VscToVkEx = 0x03,

    /// <summary>
    ///     The MAPVK_VK_TO_VSC_EX constant.
    /// </summary>
    VkToVscEx = 0x04,
}