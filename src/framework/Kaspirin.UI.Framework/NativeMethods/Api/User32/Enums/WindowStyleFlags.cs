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
#pragma warning disable KCAIDE0002 // Enum has incorrect suffix

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/winmsg/window-styles">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum WindowStyleFlags : uint
    {
        WS_BORDER = 0x800000,
        WS_CAPTION = 0xc00000,
        WS_CHILD = 0x40000000,
        WS_CHILDWINDOW = WS_CHILD,
        WS_CLIPCHILDREN = 0x2000000,
        WS_CLIPSIBLINGS = 0x4000000,
        WS_DISABLED = 0x8000000,
        WS_DLGFRAME = 0x400000,
        WS_GROUP = 0x20000,
        WS_HSCROLL = 0x100000,
        WS_ICONIC = WS_MINIMIZE,
        WS_MAXIMIZE = 0x1000000,
        WS_MAXIMIZEBOX = 0x10000,
        WS_MINIMIZE = 0x20000000,
        WS_MINIMIZEBOX = 0x20000,
        WS_OVERLAPPED = 0x0,
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEBOX | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_POPUP = 0x80000000,
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
        WS_SIZEBOX = 0x40000,
        WS_SYSMENU = 0x80000,
        WS_TABSTOP = 0x10000,
        WS_THICKFRAME = WS_SIZEBOX,
        WS_TILED = WS_OVERLAPPED,
        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
        WS_VISIBLE = 0x10000000,
        WS_VSCROLL = 0x200000
    }
}
