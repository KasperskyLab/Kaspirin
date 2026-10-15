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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow">Learn more</seealso>.
/// </summary>
public enum GetWindowCommands : uint
{
    /// <summary>
    ///     The GW_HWNDFIRST constant.
    /// </summary>
    HWndFirst = 0,

    /// <summary>
    ///     The GW_HWNDLAST constant.
    /// </summary>
    HWndLast = 1,

    /// <summary>
    ///     The GW_HWNDNEXT constant.
    /// </summary>
    HWndNext = 2,

    /// <summary>
    ///     The GW_HWNDPREV constant.
    /// </summary>
    HWndPrev = 3,

    /// <summary>
    ///     The GW_OWNER constant.
    /// </summary>
    Owner = 4,

    /// <summary>
    ///     The GW_CHILD constant.
    /// </summary>
    Child = 5,

    /// <summary>
    ///     The GW_ENABLEDPOPUP constant.
    /// </summary>
    EnabledPopup = 6,
}

