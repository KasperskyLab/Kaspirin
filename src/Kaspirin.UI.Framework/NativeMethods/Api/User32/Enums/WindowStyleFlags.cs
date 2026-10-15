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


using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/winmsg/window-styles">Learn more</seealso>.
/// </summary>
[Flags]
public enum WindowStyleFlags : uint
{
    /// <summary>
    ///     The WS_BORDER constant.
    /// </summary>
    Border = 0x800000,

    /// <summary>
    ///     The WS_CAPTION constant.
    /// </summary>
    Caption = 0xc00000,

    /// <summary>
    ///     The WS_CHILD constant.
    /// </summary>
    Child = 0x40000000,

    /// <summary>
    ///     The WS_CHILDWINDOW constant.
    /// </summary>
    ChildWindow = Child,

    /// <summary>
    ///     The WS_CLIPCHILDREN constant.
    /// </summary>
    ClipChildren = 0x2000000,

    /// <summary>
    ///     The WS_CLIPSIBLINGS constant.
    /// </summary>
    ClipSiblings = 0x4000000,

    /// <summary>
    ///     The WS_DISABLED constant.
    /// </summary>
    Disabled = 0x8000000,

    /// <summary>
    ///     The WS_DLGFRAME constant.
    /// </summary>
    DlgFrame = 0x400000,

    /// <summary>
    ///     The WS_GROUP constant.
    /// </summary>
    Group = 0x20000,

    /// <summary>
    ///     The WS_HSCROLL constant.
    /// </summary>
    HScroll = 0x100000,

    /// <summary>
    ///     The WS_ICONIC constant.
    /// </summary>
    Iconic = Minimize,

    /// <summary>
    ///     The WS_MAXIMIZE constant.
    /// </summary>
    Maximize = 0x1000000,

    /// <summary>
    ///     The WS_MAXIMIZEBOX constant.
    /// </summary>
    MaximizeBox = 0x10000,

    /// <summary>
    ///     The WS_MINIMIZE constant.
    /// </summary>
    Minimize = 0x20000000,

    /// <summary>
    ///     The WS_MINIMIZEBOX constant.
    /// </summary>
    MinimizeBox = 0x20000,

    /// <summary>
    ///     The WS_OVERLAPPED constant.
    /// </summary>
    Overlapped = 0x0,

    /// <summary>
    ///     The WS_OVERLAPPEDWINDOW constant.
    /// </summary>
    OverlappedWindow = Overlapped | Caption | SysMenu | SizeBox | MinimizeBox | MaximizeBox,

    /// <summary>
    ///     The WS_POPUP constant.
    /// </summary>
    Popup = 0x80000000,

    /// <summary>
    ///     The WS_POPUPWINDOW constant.
    /// </summary>
    PopupWindow = Popup | Border | SysMenu,

    /// <summary>
    ///     The WS_SIZEBOX constant.
    /// </summary>
    SizeBox = 0x40000,

    /// <summary>
    ///     The WS_SYSMENU constant.
    /// </summary>
    SysMenu = 0x80000,

    /// <summary>
    ///     The WS_TABSTOP constant.
    /// </summary>
    TabStop = 0x10000,

    /// <summary>
    ///     The WS_THICKFRAME constant.
    /// </summary>
    ThickFrame = SizeBox,

    /// <summary>
    ///     The WS_TILED constant.
    /// </summary>
    Tiled = Overlapped,

    /// <summary>
    ///     The WS_TILEDWINDOW constant.
    /// </summary>
    TiledWindow = OverlappedWindow,

    /// <summary>
    ///     The WS_VISIBLE constant.
    /// </summary>
    Visible = 0x10000000,

    /// <summary>
    ///     The WS_VSCROLL constant.
    /// </summary>
    VScroll = 0x200000,
}
