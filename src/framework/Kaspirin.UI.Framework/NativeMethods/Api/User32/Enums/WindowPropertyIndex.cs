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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongw">Learn more</seealso>.
/// </summary>
public enum WindowPropertyIndex : int
{
    /// <summary>
    ///     The GWL_WNDPROC constant.
    /// </summary>
    WndProc = -4,

    /// <summary>
    ///     The GWL_HINSTANCE constant.
    /// </summary>
    HInstance = -6,

    /// <summary>
    ///     The GWL_HWNDPARENT constant.
    /// </summary>
    HWndParent = -8,

    /// <summary>
    ///     The GWL_ID constant.
    /// </summary>
    Id = -12,

    /// <summary>
    ///     The GWL_STYLE constant.
    /// </summary>
    Style = -16,

    /// <summary>
    ///     The GWL_EXSTYLE constant.
    /// </summary>
    ExStyle = -20,

    /// <summary>
    ///     The GWL_USERDATA constant.
    /// </summary>
    UserData = -21,
}