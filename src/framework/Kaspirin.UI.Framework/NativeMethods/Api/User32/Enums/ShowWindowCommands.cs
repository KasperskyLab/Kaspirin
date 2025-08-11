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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow">Learn more</seealso>.
/// </summary>
public enum ShowWindowCommands : uint
{
    /// <summary>
    ///     The SW_HIDE constant.
    /// </summary>
    Hide = 0,

    /// <summary>
    ///     The SW_NORMAL constant.
    /// </summary>
    Normal = 1,

    /// <summary>
    ///     The SW_SHOWMINIMIZED constant.
    /// </summary>
    ShowMinimized = 2,

    /// <summary>
    ///     The SW_SHOWMAXIMIZED constant.
    /// </summary>
    ShowMaximized = 3,

    /// <summary>
    ///     The SW_SHOWNOACTIVATE constant.
    /// </summary>
    ShowNoActivate = 4,

    /// <summary>
    ///     The SW_SHOW constant.
    /// </summary>
    Show = 5,

    /// <summary>
    ///     The SW_MINIMIZE constant.
    /// </summary>
    Minimize = 6,

    /// <summary>
    ///     The SW_SHOWMINNOACTIVE constant.
    /// </summary>
    ShowMinNoActive = 7,

    /// <summary>
    ///     The SW_SHOWNA constant.
    /// </summary>
    ShowNA = 8,

    /// <summary>
    ///     The SW_RESTORE constant.
    /// </summary>
    Restore = 9,

    /// <summary>
    ///     The SW_SHOWDEFAULT constant.
    /// </summary>
    ShowDefault = 10,

    /// <summary>
    ///     The SW_FORCEMINIMIZE constant.
    /// </summary>
    ForceMinimize = 11,
}