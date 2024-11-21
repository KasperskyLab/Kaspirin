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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Kaspirin.UI.Framework.NativeMethods.Common
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues">Learn more</seealso>.
    /// </summary>
    public enum WindowMessage : uint
    {
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_GETTEXT = 0x000D,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_MOUSEACTIVATE = 0x0021,
        WM_GETMINMAXINFO = 0x0024,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_NCCALCSIZE = 0x0083,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_INITDIALOG = 0x0110,
        WM_SYSCOMMAND = 0x0112,
        WM_CHANGEUISTATE = 0x0127,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_SIZING = 0x0214,
        WM_MOVING = 0x0216,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_DPICHANGED = 0x02E0,
        WM_HOTKEY = 0x0312,
        WM_USER = 0x0400,
        WM_CREATETIMER = WM_USER + 1,
        WM_KILLTIMER = WM_USER + 2,
        WM_REFLECT = WM_USER + 0x1C00,
        WM_MOUSEWHEEL = 0x020A,
    }
}
