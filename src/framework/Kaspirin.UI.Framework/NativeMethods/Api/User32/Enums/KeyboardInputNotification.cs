// Copyright © 2026 AO Kaspersky Lab.
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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/inputdev/keyboard-input-notifications">Learn more</seealso>.
/// </summary>
public enum KeyboardInputNotification : uint
{
    /// <summary>
    ///     The WM_ACTIVATE constant.
    /// </summary>
    Activate = 0x0006,

    /// <summary>
    ///     The WM_APPCOMMAND constant.
    /// </summary>
    AppCommand = 0x0319,

    /// <summary>
    ///     The WM_CHAR constant.
    /// </summary>
    Char = 0x0102,

    /// <summary>
    ///     The WM_DEADCHAR constant.
    /// </summary>
    DeadChar = 0x0103,

    /// <summary>
    ///     The WM_HOTKEY constant.
    /// </summary>
    HotKey = 0x0312,

    /// <summary>
    ///     The WM_KEYDOWN constant.
    /// </summary>
    KeyDown = 0x0100,

    /// <summary>
    ///     The WM_KEYUP constant.
    /// </summary>
    KeyUp = 0x0101,

    /// <summary>
    ///     The WM_KILLFOCUS constant.
    /// </summary>
    KillFocus = 0x0008,

    /// <summary>
    ///     The WM_SETFOCUS constant.
    /// </summary>
    SetFocus = 0x0007,

    /// <summary>
    ///     The WM_SYSDEADCHAR constant.
    /// </summary>
    SysDeadChar = 0x0107,

    /// <summary>
    ///     The WM_SYSKEYDOWN constant.
    /// </summary>
    SysKeyDown = 0x0104,

    /// <summary>
    ///     The WM_SYSKEYUP constant.
    /// </summary>
    SysKeyUp = 0x0105,

    /// <summary>
    ///     The WM_UNICHAR constant.
    /// </summary>
    Unichar = 0x0109,
}
