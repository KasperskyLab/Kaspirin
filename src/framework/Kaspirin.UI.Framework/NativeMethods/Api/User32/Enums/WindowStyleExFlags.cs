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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles">Learn more</seealso>.
/// </summary>
[Flags]
public enum WindowStyleExFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The WS_EX_DLGMODALFRAME constant.
    /// </summary>
    DlgModalFrame = 0x00000001,

    /// <summary>
    ///     The WS_EX_NOPARENTNOTIFY constant.
    /// </summary>
    NoParentNotify = 0x00000004,

    /// <summary>
    ///     The WS_EX_TOPMOST constant.
    /// </summary>
    Topmost = 0x00000008,

    /// <summary>
    ///     The WS_EX_ACCEPTFILES constant.
    /// </summary>
    AcceptFiles = 0x00000010,

    /// <summary>
    ///     The WS_EX_TRANSPARENT constant.
    /// </summary>
    Transparent = 0x00000020,

    /// <summary>
    ///     The WS_EX_MDICHILD constant.
    /// </summary>
    MdiChild = 0x00000040,

    /// <summary>
    ///     The WS_EX_TOOLWINDOW constant.
    /// </summary>
    ToolWindow = 0x00000080,

    /// <summary>
    ///     The WS_EX_WINDOWEDGE constant.
    /// </summary>
    WindowEdge = 0x00000100,

    /// <summary>
    ///     The WS_EX_CLIENTEDGE constant.
    /// </summary>
    ClientEdge = 0x00000200,

    /// <summary>
    ///     The WS_EX_CONTEXTHELP constant.
    /// </summary>
    ContextHelp = 0x00000400,

    /// <summary>
    ///     The WS_EX_RIGHT constant.
    /// </summary>
    Right = 0x00001000,

    /// <summary>
    ///     The WS_EX_LEFT constant.
    /// </summary>
    Left = 0x00000000,

    /// <summary>
    ///     The WS_EX_RTLREADING constant.
    /// </summary>
    RtlReading = 0x00002000,

    /// <summary>
    ///     The WS_EX_LTRREADING constant.
    /// </summary>
    LtrReading = 0x00000000,

    /// <summary>
    ///     The WS_EX_LEFTSCROLLBAR constant.
    /// </summary>
    LeftScrollbar = 0x00004000,

    /// <summary>
    ///     The WS_EX_RIGHTSCROLLBAR constant.
    /// </summary>
    RightScrollbar = 0x00000000,

    /// <summary>
    ///     The WS_EX_CONTROLPARENT constant.
    /// </summary>
    ControlParent = 0x00010000,

    /// <summary>
    ///     The WS_EX_STATICEDGE constant.
    /// </summary>
    StaticEdge = 0x00020000,

    /// <summary>
    ///     The WS_EX_APPWINDOW constant.
    /// </summary>
    AppWindow = 0x00040000,

    /// <summary>
    ///     The WS_EX_LAYERED constant.
    /// </summary>
    Layered = 0x00080000,

    /// <summary>
    ///     The WS_EX_NOINHERITLAYOUT constant.
    /// </summary>
    NoInheritLayout = 0x00100000,

    /// <summary>
    ///     The WS_EX_LAYOUTRTL constant.
    /// </summary>
    LayoutRtl = 0x00400000,

    /// <summary>
    ///     The WS_EX_COMPOSITED constant.
    /// </summary>
    Composited = 0x02000000,

    /// <summary>
    ///     The WS_EX_NOACTIVATE constant.
    /// </summary>
    NoActivate = 0x08000000,

    /// <summary>
    ///     The WS_EX_NOREDIRECTIONBITMAP constant.
    /// </summary>
    NoRedirectionBitmap = 0x00200000,

    /// <summary>
    ///     The WS_EX_OVERLAPPEDWINDOW constant.
    /// </summary>
    OverlappedWindow = ClientEdge | WindowEdge,

    /// <summary>
    ///     The WS_EX_PALETTEWINDOW constant.
    /// </summary>
    PaletteWindow = WindowEdge | ToolWindow | Topmost,
}