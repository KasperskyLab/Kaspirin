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

namespace Kaspirin.UI.Framework.NativeMethods.Common;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues">Learn more</seealso>.
/// </summary>
public enum WindowMessage : uint
{
    /// <summary>
    ///     The WM_CREATE constant.
    /// </summary>
    Create = 0x0001,

    /// <summary>
    ///     The WM_DESTROY constant.
    /// </summary>
    Destroy = 0x0002,

    /// <summary>
    ///     The WM_MOVE constant.
    /// </summary>
    Move = 0x0003,

    /// <summary>
    ///     The WM_SIZE constant.
    /// </summary>
    Size = 0x0005,

    /// <summary>
    ///     The WM_ACTIVATE constant.
    /// </summary>
    Activate = 0x0006,

    /// <summary>
    ///     The WM_SETFOCUS constant.
    /// </summary>
    SetFocus = 0x0007,

    /// <summary>
    ///     The WM_KILLFOCUS constant.
    /// </summary>
    KillFocus = 0x0008,

    /// <summary>
    ///     The WM_GETTEXT constant.
    /// </summary>
    GetText = 0x000D,

    /// <summary>
    ///     The WM_PAINT constant.
    /// </summary>
    Paint = 0x000F,

    /// <summary>
    ///     The WM_CLOSE constant.
    /// </summary>
    Close = 0x0010,

    /// <summary>
    ///     The WM_QUERYENDSESSION constant.
    /// </summary>
    QueryEndSession = 0x0011,

    /// <summary>
    ///     The WM_QUIT constant.
    /// </summary>
    Quit = 0x0012,

    /// <summary>
    ///     The WM_ERASEBKGND constant.
    /// </summary>
    EraseBkgnd = 0x0014,

    /// <summary>
    ///     The WM_SYSCOLORCHANGE constant.
    /// </summary>
    SysColorChange = 0x0015,

    /// <summary>
    ///     The WM_ENDSESSION constant.
    /// </summary>
    EndSession = 0x0016,

    /// <summary>
    ///     The WM_SHOWWINDOW constant.
    /// </summary>
    ShowWindow = 0x0018,

    /// <summary>
    ///     The WM_MOUSEACTIVATE constant.
    /// </summary>
    MouseActivate = 0x0021,

    /// <summary>
    ///     The WM_GETMINMAXINFO constant.
    /// </summary>
    GetMinMaxInfo = 0x0024,

    /// <summary>
    ///     The WM_WINDOWPOSCHANGING constant.
    /// </summary>
    WindowPosChanging = 0x0046,

    /// <summary>
    ///     The WM_WINDOWPOSCHANGED constant.
    /// </summary>
    WindowPosChanged = 0x0047,

    /// <summary>
    ///     The WM_DISPLAYCHANGE constant.
    /// </summary>
    DisplayChange = 0x007E,

    /// <summary>
    ///     The WM_GETICON constant.
    /// </summary>
    GetIcon = 0x007F,

    /// <summary>
    ///     The WM_NCCALCSIZE constant.
    /// </summary>
    NcCalcSize = 0x0083,

    /// <summary>
    ///     The WM_NCLBUTTONDOWN constant.
    /// </summary>
    NclButtonDown = 0x00A1,

    /// <summary>
    ///     The WM_NCLBUTTONDBLCLK constant.
    /// </summary>
    NclButtonDblClk = 0x00A3,

    /// <summary>
    ///     The WM_INITDIALOG constant.
    /// </summary>
    InitDialog = 0x0110,

    /// <summary>
    ///     The WM_SYSCOMMAND constant.
    /// </summary>
    SysCommand = 0x0112,

    /// <summary>
    ///     The WM_CHANGEUISTATE constant.
    /// </summary>
    ChangeUiState = 0x0127,

    /// <summary>
    ///     The WM_CTLCOLORMSGBOX constant.
    /// </summary>
    CtlColorMsgBox = 0x0132,

    /// <summary>
    ///     The WM_SIZING constant.
    /// </summary>
    Sizing = 0x0214,

    /// <summary>
    ///     The WM_MOVING constant.
    /// </summary>
    Moving = 0x0216,

    /// <summary>
    ///     The WM_ENTERSIZEMOVE constant.
    /// </summary>
    EnterSizeMove = 0x0231,

    /// <summary>
    ///     The WM_EXITSIZEMOVE constant.
    /// </summary>
    ExitSizeMove = 0x0232,

    /// <summary>
    ///     The WM_IME_SETCONTEXT constant.
    /// </summary>
    ImeSetContext = 0x0281,

    /// <summary>
    ///     The WM_IME_NOTIFY constant.
    /// </summary>
    ImeNotify = 0x0282,

    /// <summary>
    ///     The WM_DPICHANGED constant.
    /// </summary>
    DpiChanged = 0x02E0,

    /// <summary>
    ///     The WM_HOTKEY constant.
    /// </summary>
    Hotkey = 0x0312,

    /// <summary>
    ///     The WM_USER constant.
    /// </summary>
    User = 0x0400,

    /// <summary>
    ///     The WM_CREATETIMER constant.
    /// </summary>
    CreateTimer = User + 1,

    /// <summary>
    ///     The WM_KILLTIMER constant.
    /// </summary>
    KillTimer = User + 2,

    /// <summary>
    ///     The WM_REFLECT constant.
    /// </summary>
    Reflect = User + 0x1C00,

    /// <summary>
    ///     The WM_MOUSEWHEEL constant.
    /// </summary>
    MouseWheel = 0x020A,

    /// <summary>
    ///     The WM_LBUTTONDOWN constant.
    /// </summary>
    LButtonDown = 0x0201,

    /// <summary>
    ///     The WM_LBUTTONUP constant.
    /// </summary>
    LButtonUp = 0x0202,

    /// <summary>
    ///     The WM_LBUTTONDBLCLK constant.
    /// </summary>
    LButtonDblClk = 0x0203,

    /// <summary>
    ///     The WM_RBUTTONDOWN constant.
    /// </summary>
    RButtonDown = 0x0204,

    /// <summary>
    ///     The WM_RBUTTONUP constant.
    /// </summary>
    RButtonUp = 0x0205,

    /// <summary>
    ///     The WM_RBUTTONDBLCLK constant.
    /// </summary>
    RButtonDblClk = 0x0206,

    /// <summary>
    ///     The WM_MBUTTONDOWN constant.
    /// </summary>
    MButtonDown = 0x0207,

    /// <summary>
    ///     The WM_MBUTTONUP constant.
    /// </summary>
    MButtonUp = 0x0208,

    /// <summary>
    ///     The WM_MBUTTONDBLCLK constant.
    /// </summary>
    MButtonDblClk = 0x0209,
}
