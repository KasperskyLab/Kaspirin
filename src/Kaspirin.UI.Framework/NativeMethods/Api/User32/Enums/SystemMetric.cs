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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics">Learn more</seealso>.
/// </summary>
public enum SystemMetric : uint
{
    /// <summary>
    ///     The SM_CXSCREEN constant.
    /// </summary>
    CxScreen = 0,

    /// <summary>
    ///     The SM_CYSCREEN constant.
    /// </summary>
    CyScreen = 1,

    /// <summary>
    ///     The SM_CXVSCROLL constant.
    /// </summary>
    CxVScroll = 2,

    /// <summary>
    ///     The SM_CYHSCROLL constant.
    /// </summary>
    CyHScroll = 3,

    /// <summary>
    ///     The SM_CYCAPTION constant.
    /// </summary>
    CyCaption = 4,

    /// <summary>
    ///     The SM_CXBORDER constant.
    /// </summary>
    CxBorder = 5,

    /// <summary>
    ///     The SM_CYBORDER constant.
    /// </summary>
    CyBorder = 6,

    /// <summary>
    ///     The SM_CXDLGFRAME constant.
    /// </summary>
    CxDlgFrame = 7,

    /// <summary>
    ///     The SM_CYDLGFRAME constant.
    /// </summary>
    CyDlgFrame = 8,

    /// <summary>
    ///     The SM_CYVTHUMB constant.
    /// </summary>
    CyVThumb = 9,

    /// <summary>
    ///     The SM_CXHTHUMB constant.
    /// </summary>
    CxHThumb = 10,

    /// <summary>
    ///     The SM_CXICON constant.
    /// </summary>
    CxIcon = 11,

    /// <summary>
    ///     The SM_CYICON constant.
    /// </summary>
    CyIcon = 12,

    /// <summary>
    ///     The SM_CXCURSOR constant.
    /// </summary>
    CxCursor = 13,

    /// <summary>
    ///     The SM_CYCURSOR constant.
    /// </summary>
    CyCursor = 14,

    /// <summary>
    ///     The SM_CYMENU constant.
    /// </summary>
    CyMenu = 15,

    /// <summary>
    ///     The SM_CXFULLSCREEN constant.
    /// </summary>
    CxFullScreen = 16,

    /// <summary>
    ///     The SM_CYFULLSCREEN constant.
    /// </summary>
    CyFullScreen = 17,

    /// <summary>
    ///     The SM_CYKANJIWINDOW constant.
    /// </summary>
    CyKanjiWindow = 18,

    /// <summary>
    ///     The SM_MOUSEPRESENT constant.
    /// </summary>
    MousePresent = 19,

    /// <summary>
    ///     The SM_CYVSCROLL constant.
    /// </summary>
    CyVScroll = 20,

    /// <summary>
    ///     The SM_CXHSCROLL constant.
    /// </summary>
    CxHScroll = 21,

    /// <summary>
    ///     The SM_DEBUG constant.
    /// </summary>
    Debug = 22,

    /// <summary>
    ///     The SM_SWAPBUTTON constant.
    /// </summary>
    SwapButton = 23,

    /// <summary>
    ///     The SM_RESERVED1 constant.
    /// </summary>
    Reserved1 = 24,

    /// <summary>
    ///     The SM_RESERVED2 constant.
    /// </summary>
    Reserved2 = 25,

    /// <summary>
    ///     The SM_RESERVED3 constant.
    /// </summary>
    Reserved3 = 26,

    /// <summary>
    ///     The SM_RESERVED4 constant.
    /// </summary>
    Reserved4 = 27,

    /// <summary>
    ///     The SM_CXMIN constant.
    /// </summary>
    CxMin = 28,

    /// <summary>
    ///     The SM_CYMIN constant.
    /// </summary>
    CyMin = 29,

    /// <summary>
    ///     The SM_CXSIZE constant.
    /// </summary>
    CxSize = 30,

    /// <summary>
    ///     The SM_CYSIZE constant.
    /// </summary>
    CySize = 31,

    /// <summary>
    ///     The SM_CXFRAME constant.
    /// </summary>
    CxFrame = 32,

    /// <summary>
    ///     The SM_CYFRAME constant.
    /// </summary>
    CyFrame = 33,

    /// <summary>
    ///     The SM_CXMINTRACK constant.
    /// </summary>
    CxMinTrack = 34,

    /// <summary>
    ///     The SM_CYMINTRACK constant.
    /// </summary>
    CyMinTrack = 35,

    /// <summary>
    ///     The SM_CXDOUBLECLK constant.
    /// </summary>
    CxDoubleClk = 36,

    /// <summary>
    ///     The SM_CYDOUBLECLK constant.
    /// </summary>
    CyDoubleClk = 37,

    /// <summary>
    ///     The SM_CXICONSPACING constant.
    /// </summary>
    CxIconSpacing = 38,

    /// <summary>
    ///     The SM_CYICONSPACING constant.
    /// </summary>
    CyIconSpacing = 39,

    /// <summary>
    ///     The SM_MENUDROPALIGNMENT constant.
    /// </summary>
    MenuDropAlignment = 40,

    /// <summary>
    ///     The SM_PENWINDOWS constant.
    /// </summary>
    PenWindows = 41,

    /// <summary>
    ///     The SM_DBCSENABLED constant.
    /// </summary>
    DbcsEnabled = 42,

    /// <summary>
    ///     The SM_CMOUSEBUTTONS constant.
    /// </summary>
    CMouseButtons = 43,

    /// <summary>
    ///     The SM_SECURE constant.
    /// </summary>
    Secure = 44,

    /// <summary>
    ///     The SM_CXEDGE constant.
    /// </summary>
    CxEdge = 45,

    /// <summary>
    ///     The SM_CYEDGE constant.
    /// </summary>
    CyEdge = 46,

    /// <summary>
    ///     The SM_CXMINSPACING constant.
    /// </summary>
    CxMinSpacing = 47,

    /// <summary>
    ///     The SM_CYMINSPACING constant.
    /// </summary>
    CyMinSpacing = 48,

    /// <summary>
    ///     The SM_CXSMICON constant.
    /// </summary>
    CxSmIcon = 49,

    /// <summary>
    ///     The SM_CYSMICON constant.
    /// </summary>
    CySmIcon = 50,

    /// <summary>
    ///     The SM_CYSMCAPTION constant.
    /// </summary>
    CySmCaption = 51,

    /// <summary>
    ///     The SM_CXSMSIZE constant.
    /// </summary>
    CxSmSize = 52,

    /// <summary>
    ///     The SM_CYSMSIZE constant.
    /// </summary>
    CySmSize = 53,

    /// <summary>
    ///     The SM_CXMENUSIZE constant.
    /// </summary>
    CxMenuSize = 54,

    /// <summary>
    ///     The SM_CYMENUSIZE constant.
    /// </summary>
    CyMenuSize = 55,

    /// <summary>
    ///     The SM_ARRANGE constant.
    /// </summary>
    Arrange = 56,

    /// <summary>
    ///     The SM_CXMINIMIZED constant.
    /// </summary>
    CxMinimized = 57,

    /// <summary>
    ///     The SM_CYMINIMIZED constant.
    /// </summary>
    CyMinimized = 58,

    /// <summary>
    ///     The SM_CXMAXTRACK constant.
    /// </summary>
    CxMaxTrack = 59,

    /// <summary>
    ///     The SM_CYMAXTRACK constant.
    /// </summary>
    CyMaxTrack = 60,

    /// <summary>
    ///     The SM_CXMAXIMIZED constant.
    /// </summary>
    CxMaximized = 61,

    /// <summary>
    ///     The SM_CYMAXIMIZED constant.
    /// </summary>
    CyMaximized = 62,

    /// <summary>
    ///     The SM_NETWORK constant.
    /// </summary>
    Network = 63,

    /// <summary>
    ///     The SM_CLEANBOOT constant.
    /// </summary>
    CleanBoot = 67,

    /// <summary>
    ///     The SM_CXDRAG constant.
    /// </summary>
    CxDrag = 68,

    /// <summary>
    ///     The SM_CYDRAG constant.
    /// </summary>
    CyDrag = 69,

    /// <summary>
    ///     The SM_SHOWSOUNDS constant.
    /// </summary>
    ShowSounds = 70,

    /// <summary>
    ///     The SM_CXMENUCHECK constant.
    /// </summary>
    CxMenuCheck = 71,

    /// <summary>
    ///     The SM_CYMENUCHECK constant.
    /// </summary>
    CyMenuCheck = 72,

    /// <summary>
    ///     The SM_SLOWMACHINE constant.
    /// </summary>
    SlowMachine = 73,

    /// <summary>
    ///     The SM_MIDEASTENABLED constant.
    /// </summary>
    MideastEnabled = 74,

    /// <summary>
    ///     The SM_MOUSEWHEELPRESENT constant.
    /// </summary>
    MouseWheelPresent = 75,

    /// <summary>
    ///     The SM_XVIRTUALSCREEN constant.
    /// </summary>
    XVirtualScreen = 76,

    /// <summary>
    ///     The SM_YVIRTUALSCREEN constant.
    /// </summary>
    YVirtualScreen = 77,

    /// <summary>
    ///     The SM_CXVIRTUALSCREEN constant.
    /// </summary>
    CxVirtualScreen = 78,

    /// <summary>
    ///     The SM_CYVIRTUALSCREEN constant.
    /// </summary>
    CyVirtualScreen = 79,

    /// <summary>
    ///     The SM_CMONITORS constant.
    /// </summary>
    CMonitors = 80,

    /// <summary>
    ///     The SM_SAMEDISPLAYFORMAT constant.
    /// </summary>
    SameDisplayFormat = 81,

    /// <summary>
    ///     The SM_IMMENABLED constant.
    /// </summary>
    ImmEnabled = 82,

    /// <summary>
    ///     The SM_CXFOCUSBORDER constant.
    /// </summary>
    CxFocusBorder = 83,

    /// <summary>
    ///     The SM_CYFOCUSBORDER constant.
    /// </summary>
    CyFocusBorder = 84,

    /// <summary>
    ///     The SM_TABLETPC constant.
    /// </summary>
    TabletPc = 86,

    /// <summary>
    ///     The SM_MEDIACENTER constant.
    /// </summary>
    MediaCenter = 87,

    /// <summary>
    ///     The SM_STARTER constant.
    /// </summary>
    Starter = 88,

    /// <summary>
    ///     The SM_SERVERR2 constant.
    /// </summary>
    ServerR2 = 89,

    /// <summary>
    ///     The SM_MOUSEHORIZONTALWHEELPRESENT constant.
    /// </summary>
    MouseHorizontalWheelPresent = 91,

    /// <summary>
    ///     The SM_CXPADDEDBORDER constant.
    /// </summary>
    CxPaddedBorder = 92,

    /// <summary>
    ///     The SM_DIGITIZER constant.
    /// </summary>
    Digitizer = 94,

    /// <summary>
    ///     The SM_MAXIMUMTOUCHES constant.
    /// </summary>
    MaximumTouches = 95,

    /// <summary>
    ///     The SM_REMOTESESSION constant.
    /// </summary>
    RemoteSession = 0x1000,

    /// <summary>
    ///     The SM_SHUTTINGDOWN constant.
    /// </summary>
    ShuttingDown = 0x2000,

    /// <summary>
    ///     The SM_REMOTECONTROL constant.
    /// </summary>
    RemoteControl = 0x2001,

    /// <summary>
    ///     The SM_CONVERTIBLESLATEMODE constant.
    /// </summary>
    ConvertiblesLateMode = 0x2003,

    /// <summary>
    ///     The SM_SYSTEMDOCKED constant.
    /// </summary>
    SystemDocked = 0x2004,
}