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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw">Learn more</seealso>.
/// </summary>
public enum HookType : int
{
    /// <summary>
    ///     The WH_MSGFILTER constant.
    /// </summary>
    MsgFilter = -1,

    /// <summary>
    ///     The WH_JOURNALRECORD constant.
    /// </summary>
    JournalRecord = 0,

    /// <summary>
    ///     The WH_JOURNALPLAYBACK constant.
    /// </summary>
    JournalPlayback = 1,

    /// <summary>
    ///     The WH_KEYBOARD constant.
    /// </summary>
    Keyboard = 2,

    /// <summary>
    ///     The WH_GETMESSAGE constant.
    /// </summary>
    GetMessage = 3,

    /// <summary>
    ///     The WH_CALLWNDPROC constant.
    /// </summary>
    CallWndProc = 4,

    /// <summary>
    ///     The WH_CBT constant.
    /// </summary>
    Cbt = 5,

    /// <summary>
    ///     The WH_SYSMSGFILTER constant.
    /// </summary>
    SysMsgFilter = 6,

    /// <summary>
    ///     The WH_MOUSE constant.
    /// </summary>
    Mouse = 7,

    /// <summary>
    ///     The WH_HARDWARE constant.
    /// </summary>
    Hardware = 8,

    /// <summary>
    ///     The WH_DEBUG constant.
    /// </summary>
    Debug = 9,

    /// <summary>
    ///     The WH_SHELL constant.
    /// </summary>
    Shell = 10,

    /// <summary>
    ///     The WH_FOREGROUNDIDLE constant.
    /// </summary>
    ForegroundIdle = 11,

    /// <summary>
    ///     The WH_CALLWNDPROCRET constant.
    /// </summary>
    CallWndProcRet = 12,

    /// <summary>
    ///     The WH_KEYBOARD_LL constant.
    /// </summary>
    KeyboardLl = 13,

    /// <summary>
    ///     The WH_MOUSE_LL constant.
    /// </summary>
    MouseLl = 14,
}
