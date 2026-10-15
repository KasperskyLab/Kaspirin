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
///     <seealso href="https://learn.microsoft.com/en-us/Windows/win32/api/winuser/nf-winuser-systemparametersinfow">Learn more</seealso>.
/// </summary>
public enum SpiType : uint
{
    /// <summary>
    ///     The SPI_GETBEEP constant.
    /// </summary>
    GetBeep = 0x0001,

    /// <summary>
    ///     The SPI_SETBEEP constant.
    /// </summary>
    SetBeep = 0x0002,

    /// <summary>
    ///     The SPI_GETMOUSE constant.
    /// </summary>
    GetMouse = 0x0003,

    /// <summary>
    ///     The SPI_SETMOUSE constant.
    /// </summary>
    SetMouse = 0x0004,

    /// <summary>
    ///     The SPI_GETBORDER constant.
    /// </summary>
    GetBorder = 0x0005,

    /// <summary>
    ///     The SPI_SETBORDER constant.
    /// </summary>
    SetBorder = 0x0006,

    /// <summary>
    ///     The SPI_GETKEYBOARDSPEED constant.
    /// </summary>
    GetKeyboardSpeed = 0x000A,

    /// <summary>
    ///     The SPI_SETKEYBOARDSPEED constant.
    /// </summary>
    SetKeyboardSpeed = 0x000B,

    /// <summary>
    ///     The SPI_LANGDRIVER constant.
    /// </summary>
    LangDriver = 0x000C,

    /// <summary>
    ///     The SPI_ICONHORIZONTALSPACING constant.
    /// </summary>
    IconHorizontalSpacing = 0x000D,

    /// <summary>
    ///     The SPI_GETSCREENSAVETIMEOUT constant.
    /// </summary>
    GetScreenSaveTimeout = 0x000E,

    /// <summary>
    ///     The SPI_SETSCREENSAVETIMEOUT constant.
    /// </summary>
    SetScreenSaveTimeout = 0x000F,

    /// <summary>
    ///     The SPI_GETSCREENSAVEACTIVE constant.
    /// </summary>
    GetScreenSaveActive = 0x0010,

    /// <summary>
    ///     The SPI_SETSCREENSAVEACTIVE constant.
    /// </summary>
    SetScreenSaveActive = 0x0011,

    /// <summary>
    ///     The SPI_GETGRIDGRANULARITY constant.
    /// </summary>
    GetGridGranularity = 0x0012,

    /// <summary>
    ///     The SPI_SETGRIDGRANULARITY constant.
    /// </summary>
    SetGridGranularity = 0x0013,

    /// <summary>
    ///     The SPI_SETDESKWALLPAPER constant.
    /// </summary>
    SetDeskWallpaper = 0x0014,

    /// <summary>
    ///     The SPI_SETDESKPATTERN constant.
    /// </summary>
    SetDeskPattern = 0x0015,

    /// <summary>
    ///     The SPI_GETKEYBOARDDELAY constant.
    /// </summary>
    GetKeyboardDelay = 0x0016,

    /// <summary>
    ///     The SPI_SETKEYBOARDDELAY constant.
    /// </summary>
    SetKeyboardDelay = 0x0017,

    /// <summary>
    ///     The SPI_ICONVERTICALSPACING constant.
    /// </summary>
    IconVerticalSpacing = 0x0018,

    /// <summary>
    ///     The SPI_GETICONTITLEWRAP constant.
    /// </summary>
    GetIconTitleWrap = 0x0019,

    /// <summary>
    ///     The SPI_SETICONTITLEWRAP constant.
    /// </summary>
    SetIconTitleWrap = 0x001A,

    /// <summary>
    ///     The SPI_GETMENUDROPALIGNMENT constant.
    /// </summary>
    GetMenuDropAlignment = 0x001B,

    /// <summary>
    ///     The SPI_SETMENUDROPALIGNMENT constant.
    /// </summary>
    SetMenuDropAlignment = 0x001C,

    /// <summary>
    ///     The SPI_SETDOUBLECLKWIDTH constant.
    /// </summary>
    SetDoubleClkWidth = 0x001D,

    /// <summary>
    ///     The SPI_SETDOUBLECLKHEIGHT constant.
    /// </summary>
    SetDoubleClkHeight = 0x001E,

    /// <summary>
    ///     The SPI_GETICONTITLELOGFONT constant.
    /// </summary>
    GetIconTitleLogFont = 0x001F,

    /// <summary>
    ///     The SPI_SETDOUBLECLICKTIME constant.
    /// </summary>
    SetDoubleClickTime = 0x0020,

    /// <summary>
    ///     The SPI_SETMOUSEBUTTONSWAP constant.
    /// </summary>
    SetMouseButtonSwap = 0x0021,

    /// <summary>
    ///     The SPI_SETICONTITLELOGFONT constant.
    /// </summary>
    SetIconTitleLogFont = 0x0022,

    /// <summary>
    ///     The SPI_GETFASTTASKSWITCH constant.
    /// </summary>
    GetFastTaskSwitch = 0x0023,

    /// <summary>
    ///     The SPI_SETFASTTASKSWITCH constant.
    /// </summary>
    SetFastTaskSwitch = 0x0024,

    /// <summary>
    ///     The SPI_SETDRAGFULLWINDOWS constant.
    /// </summary>
    SetDragFullWindows = 0x0025,

    /// <summary>
    ///     The SPI_GETDRAGFULLWINDOWS constant.
    /// </summary>
    GetDagFullWindows = 0x0026,

    /// <summary>
    ///     The SPI_GETNONCLIENTMETRICS constant.
    /// </summary>
    GetNonClientMetrics = 0x0029,

    /// <summary>
    ///     The SPI_SETNONCLIENTMETRICS constant.
    /// </summary>
    SetNonClientMetrics = 0x002A,

    /// <summary>
    ///     The SPI_GETMINIMIZEDMETRICS constant.
    /// </summary>
    GetMinimizedMetrics = 0x002B,

    /// <summary>
    ///     The SPI_SETMINIMIZEDMETRICS constant.
    /// </summary>
    SetMinimizedMetrics = 0x002C,

    /// <summary>
    ///     The SPI_GETICONMETRICS constant.
    /// </summary>
    GetIconMetrics = 0x002D,

    /// <summary>
    ///     The SPI_SETICONMETRICS constant.
    /// </summary>
    SetIconMetrics = 0x002E,

    /// <summary>
    ///     The SPI_SETWORKAREA constant.
    /// </summary>
    SetWorkArea = 0x002F,

    /// <summary>
    ///     The SPI_GETWORKAREA constant.
    /// </summary>
    GetWorkArea = 0x0030,

    /// <summary>
    ///     The SPI_SETPENWINDOWS constant.
    /// </summary>
    SetPenWindows = 0x0031,

    /// <summary>
    ///     The SPI_GETHIGHCONTRAST constant.
    /// </summary>
    GetHighContrast = 0x0042,

    /// <summary>
    ///     The SPI_SETHIGHCONTRAST constant.
    /// </summary>
    SetHighContrast = 0x0043,

    /// <summary>
    ///     The SPI_GETKEYBOARDPREF constant.
    /// </summary>
    GetKeyboardPref = 0x0044,

    /// <summary>
    ///     The SPI_SETKEYBOARDPREF constant.
    /// </summary>
    SetKeyboardPref = 0x0045,

    /// <summary>
    ///     The SPI_GETSCREENREADER constant.
    /// </summary>
    GetScreenReader = 0x0046,

    /// <summary>
    ///     The SPI_SETSCREENREADER constant.
    /// </summary>
    SetScreenReader = 0x0047,

    /// <summary>
    ///     The SPI_GETANIMATION constant.
    /// </summary>
    GetAnimation = 0x0048,

    /// <summary>
    ///     The SPI_SETANIMATION constant.
    /// </summary>
    SetAnimation = 0x0049,

    /// <summary>
    ///     The SPI_GETFONTSMOOTHING constant.
    /// </summary>
    GetFontSmoothing = 0x004A,

    /// <summary>
    ///     The SPI_SETFONTSMOOTHING constant.
    /// </summary>
    SetFontSmoothing = 0x004B,

    /// <summary>
    ///     The SPI_SETDRAGWIDTH constant.
    /// </summary>
    SetDragWidth = 0x004C,

    /// <summary>
    ///     The SPI_SETDRAGHEIGHT constant.
    /// </summary>
    SetDragHeight = 0x004D,

    /// <summary>
    ///     The SPI_SETHANDHELD constant.
    /// </summary>
    SetHandheld = 0x004E,

    /// <summary>
    ///     The SPI_GETLOWPOWERTIMEOUT constant.
    /// </summary>
    GetLowPowerTimeout = 0x004F,

    /// <summary>
    ///     The SPI_GETPOWEROFFTIMEOUT constant.
    /// </summary>
    GetPowerOffTimeout = 0x0050,

    /// <summary>
    ///     The SPI_SETLOWPOWERTIMEOUT constant.
    /// </summary>
    SetLowPowerTimeout = 0x0051,

    /// <summary>
    ///     The SPI_SETPOWEROFFTIMEOUT constant.
    /// </summary>
    SetPowerOffTimeout = 0x0052,

    /// <summary>
    ///     The SPI_GETLOWPOWERACTIVE constant.
    /// </summary>
    GetLowPowerActive = 0x0053,

    /// <summary>
    ///     The SPI_GETPOWEROFFACTIVE constant.
    /// </summary>
    GetPowerOffActive = 0x0054,

    /// <summary>
    ///     The SPI_SETLOWPOWERACTIVE constant.
    /// </summary>
    SetLowPowerActive = 0x0055,

    /// <summary>
    ///     The SPI_SETPOWEROFFACTIVE constant.
    /// </summary>
    SetPowerOffActive = 0x0056,

    /// <summary>
    ///     The SPI_SETCURSORS constant.
    /// </summary>
    SetCursors = 0x0057,

    /// <summary>
    ///     The SPI_SETICONS constant.
    /// </summary>
    SetIcons = 0x0058,

    /// <summary>
    ///     The SPI_GETDEFAULTINPUTLANG constant.
    /// </summary>
    GetDefaultInputLang = 0x0059,

    /// <summary>
    ///     The SPI_SETDEFAULTINPUTLANG constant.
    /// </summary>
    SetDefaultInputLang = 0x005A,

    /// <summary>
    ///     The SPI_SETLANGTOGGLE constant.
    /// </summary>
    SetLangToggle = 0x005B,

    /// <summary>
    ///     The SPI_GETWINDOWSEXTENSION constant.
    /// </summary>
    GetWindowsExtension = 0x005C,

    /// <summary>
    ///     The SPI_SETMOUSETRAILS constant.
    /// </summary>
    SetMouseTrails = 0x005D,

    /// <summary>
    ///     The SPI_GETMOUSETRAILS constant.
    /// </summary>
    GetMouseTrails = 0x005E,

    /// <summary>
    ///     The SPI_SETSCREENSAVERRUNNING constant.
    /// </summary>
    SetScreensaverRunning = 0x0061,

    /// <summary>
    ///     The SPI_SCREENSAVERRUNNING constant.
    /// </summary>
    ScreensaverRunning = SetScreensaverRunning,

    /// <summary>
    ///     The SPI_GETFILTERKEYS constant.
    /// </summary>
    GetFilterKeys = 0x0032,

    /// <summary>
    ///     The SPI_SETFILTERKEYS constant.
    /// </summary>
    SetFilterKeys = 0x0033,

    /// <summary>
    ///     The SPI_GETTOGGLEKEYS constant.
    /// </summary>
    GetToggleKeys = 0x0034,

    /// <summary>
    ///     The SPI_SETTOGGLEKEYS constant.
    /// </summary>
    SetToggleKeys = 0x0035,

    /// <summary>
    ///     The SPI_GETMOUSEKEYS constant.
    /// </summary>
    GetMouseKeys = 0x0036,

    /// <summary>
    ///     The SPI_SETMOUSEKEYS constant.
    /// </summary>
    SetMouseKeys = 0x0037,

    /// <summary>
    ///     The SPI_GETSHOWSOUNDS constant.
    /// </summary>
    GetShowSounds = 0x0038,

    /// <summary>
    ///     The SPI_SETSHOWSOUNDS constant.
    /// </summary>
    SetShowSounds = 0x0039,

    /// <summary>
    ///     The SPI_GETSTICKYKEYS constant.
    /// </summary>
    GetStickyKeys = 0x003A,

    /// <summary>
    ///     The SPI_SETSTICKYKEYS constant.
    /// </summary>
    SetStickyKeys = 0x003B,

    /// <summary>
    ///     The SPI_GETACCESSTIMEOUT constant.
    /// </summary>
    GetAccessTimeout = 0x003C,

    /// <summary>
    ///     The SPI_SETACCESSTIMEOUT constant.
    /// </summary>
    SetAccessTimeout = 0x003D,

    /// <summary>
    ///     The SPI_GETSERIALKEYS constant.
    /// </summary>
    GetSerialKeys = 0x003E,

    /// <summary>
    ///     The SPI_SETSERIALKEYS constant.
    /// </summary>
    SetSerialKeys = 0x003F,

    /// <summary>
    ///     The SPI_GETSOUNDSENTRY constant.
    /// </summary>
    GetSoundsEntry = 0x0040,

    /// <summary>
    ///     The SPI_SETSOUNDSENTRY constant.
    /// </summary>
    SetSoundsEntry = 0x0041,

    /// <summary>
    ///     The SPI_GETSNAPTODEFBUTTON constant.
    /// </summary>
    GetSnapToDefButton = 0x005F,

    /// <summary>
    ///     The SPI_SETSNAPTODEFBUTTON constant.
    /// </summary>
    SetSnapToDefButton = 0x0060,

    /// <summary>
    ///     The SPI_GETMOUSEHOVERWIDTH constant.
    /// </summary>
    GetMouseHoverWidth = 0x0062,

    /// <summary>
    ///     The SPI_SETMOUSEHOVERWIDTH constant.
    /// </summary>
    SetMouseHoverWidth = 0x0063,

    /// <summary>
    ///     The SPI_GETMOUSEHOVERHEIGHT constant.
    /// </summary>
    GetMouseHoverHeight = 0x0064,

    /// <summary>
    ///     The SPI_SETMOUSEHOVERHEIGHT constant.
    /// </summary>
    SetMouseHoverHeight = 0x0065,

    /// <summary>
    ///     The SPI_GETMOUSEHOVERTIME constant.
    /// </summary>
    GetMouseHoverTime = 0x0066,

    /// <summary>
    ///     The SPI_SETMOUSEHOVERTIME constant.
    /// </summary>
    SetMouseHoverTime = 0x0067,

    /// <summary>
    ///     The SPI_GETWHEELSCROLLLINES constant.
    /// </summary>
    GetWheelScrollLines = 0x0068,

    /// <summary>
    ///     The SPI_SETWHEELSCROLLLINES constant.
    /// </summary>
    SetWheelScrollLines = 0x0069,

    /// <summary>
    ///     The SPI_GETMENUSHOWDELAY constant.
    /// </summary>
    GetMenuShowDelay = 0x006A,

    /// <summary>
    ///     The SPI_SETMENUSHOWDELAY constant.
    /// </summary>
    SetMenuShowDelay = 0x006B,

    /// <summary>
    ///     The SPI_GETSHOWIMEUI constant.
    /// </summary>
    GetShowImeUi = 0x006E,

    /// <summary>
    ///     The SPI_SETSHOWIMEUI constant.
    /// </summary>
    SetShowImeUi = 0x006F,

    /// <summary>
    ///     The SPI_GETMOUSESPEED constant.
    /// </summary>
    GetMouseSpeed = 0x0070,

    /// <summary>
    ///     The SPI_SETMOUSESPEED constant.
    /// </summary>
    SetMouseSpeed = 0x0071,

    /// <summary>
    ///     The SPI_GETSCREENSAVERRUNNING constant.
    /// </summary>
    GetScreensaverRunning = 0x0072,

    /// <summary>
    ///     The SPI_GETDESKWALLPAPER constant.
    /// </summary>
    GetDeskWallpaper = 0x0073,

    /// <summary>
    ///     The SPI_GETACTIVEWINDOWTRACKING constant.
    /// </summary>
    GetActiveWindowTracking = 0x1000,

    /// <summary>
    ///     The SPI_SETACTIVEWINDOWTRACKING constant.
    /// </summary>
    SetActiveWindowTracking = 0x1001,

    /// <summary>
    ///     The SPI_GETMENUANIMATION constant.
    /// </summary>
    GetMenuAnimation = 0x1002,

    /// <summary>
    ///     The SPI_SETMENUANIMATION constant.
    /// </summary>
    SetMenuAnimation = 0x1003,

    /// <summary>
    ///     The SPI_GETCOMBOBOXANIMATION constant.
    /// </summary>
    GetComboBoxAnimation = 0x1004,

    /// <summary>
    ///     The SPI_SETCOMBOBOXANIMATION constant.
    /// </summary>
    SetComboBoxAnimation = 0x1005,

    /// <summary>
    ///     The SPI_GETLISTBOXSMOOTHSCROLLING constant.
    /// </summary>
    GetListBoxSmoothScrolling = 0x1006,

    /// <summary>
    ///     The SPI_SETLISTBOXSMOOTHSCROLLING constant.
    /// </summary>
    SetListBoxSmoothScrolling = 0x1007,

    /// <summary>
    ///     The SPI_GETGRADIENTCAPTIONS constant.
    /// </summary>
    GetGradientCaptions = 0x1008,

    /// <summary>
    ///     The SPI_SETGRADIENTCAPTIONS constant.
    /// </summary>
    SetGradientCaptions = 0x1009,

    /// <summary>
    ///     The SPI_GETKEYBOARDCUES constant.
    /// </summary>
    GetKeyboardCues = 0x100A,

    /// <summary>
    ///     The SPI_SETKEYBOARDCUES constant.
    /// </summary>
    SetKeyboardCues = 0x100B,

    /// <summary>
    ///     The SPI_GETMENUUNDERLINES constant.
    /// </summary>
    GetMenuUnderLines = GetKeyboardCues,

    /// <summary>
    ///     The SPI_SETMENUUNDERLINES constant.
    /// </summary>
    SetMenuUnderLines = SetKeyboardCues,

    /// <summary>
    ///     The SPI_GETACTIVEWNDTRKZORDER constant.
    /// </summary>
    GetActiveWndTrkZOrder = 0x100C,

    /// <summary>
    ///     The SPI_SETACTIVEWNDTRKZORDER constant.
    /// </summary>
    SetActiveWndTrkZOrder = 0x100D,

    /// <summary>
    ///     The SPI_GETHOTTRACKING constant.
    /// </summary>
    GetHotTracking = 0x100E,

    /// <summary>
    ///     The SPI_SETHOTTRACKING constant.
    /// </summary>
    SetHotTracking = 0x100F,

    /// <summary>
    ///     The SPI_GETMENUFADE constant.
    /// </summary>
    GetMenuFade = 0x1012,

    /// <summary>
    ///     The SPI_SETMENUFADE constant.
    /// </summary>
    SetMenuFade = 0x1013,

    /// <summary>
    ///     The SPI_GETSELECTIONFADE constant.
    /// </summary>
    GetSelectionFade = 0x1014,

    /// <summary>
    ///     The SPI_SETSELECTIONFADE constant.
    /// </summary>
    SetSelectionFade = 0x1015,

    /// <summary>
    ///     The SPI_GETTOOLTIPANIMATION constant.
    /// </summary>
    GetTooltipAnimation = 0x1016,

    /// <summary>
    ///     The SPI_SETTOOLTIPANIMATION constant.
    /// </summary>
    SetTooltipAnimation = 0x1017,

    /// <summary>
    ///     The SPI_GETTOOLTIPFADE constant.
    /// </summary>
    GetTooltipFade = 0x1018,

    /// <summary>
    ///     The SPI_SETTOOLTIPFADE constant.
    /// </summary>
    SetTooltipFade = 0x1019,

    /// <summary>
    ///     The SPI_GETCURSORSHADOW constant.
    /// </summary>
    GetCursorShadow = 0x101A,

    /// <summary>
    ///     The SPI_SETCURSORSHADOW constant.
    /// </summary>
    SetCursorShadow = 0x101B,

    /// <summary>
    ///     The SPI_GETMOUSESONAR constant.
    /// </summary>
    GetMouseSonar = 0x101C,

    /// <summary>
    ///     The SPI_SETMOUSESONAR constant.
    /// </summary>
    SetMouseSonar = 0x101D,

    /// <summary>
    ///     The SPI_GETMOUSECLICKLOCK constant.
    /// </summary>
    GetMouseClickLock = 0x101E,

    /// <summary>
    ///     The SPI_SETMOUSECLICKLOCK constant.
    /// </summary>
    SetMouseClickLock = 0x101F,

    /// <summary>
    ///     The SPI_GETMOUSEVANISH constant.
    /// </summary>
    GetMouseVanish = 0x1020,

    /// <summary>
    ///     The SPI_SETMOUSEVANISH constant.
    /// </summary>
    SetMouseVanish = 0x1021,

    /// <summary>
    ///     The SPI_GETFLATMENU constant.
    /// </summary>
    GetFlatMenu = 0x1022,

    /// <summary>
    ///     The SPI_SETFLATMENU constant.
    /// </summary>
    SetFlatMenu = 0x1023,

    /// <summary>
    ///     The SPI_GETDROPSHADOW constant.
    /// </summary>
    GetDropShadow = 0x1024,

    /// <summary>
    ///     The SPI_SETDROPSHADOW constant.
    /// </summary>
    SetDropShadow = 0x1025,

    /// <summary>
    ///     The SPI_GETBLOCKSENDINPUTRESETS constant.
    /// </summary>
    GetBlockSendInputResets = 0x1026,

    /// <summary>
    ///     The SPI_SETBLOCKSENDINPUTRESETS constant.
    /// </summary>
    SetBlockSendInputResets = 0x1027,

    /// <summary>
    ///     The SPI_GETUIEFFECTS constant.
    /// </summary>
    GetUiEffects = 0x103E,

    /// <summary>
    ///     The SPI_SETUIEFFECTS constant.
    /// </summary>
    SetUiEffects = 0x103F,

    /// <summary>
    ///     The SPI_GETCLIENTAREAANIMATION constant.
    /// </summary>
    GetClientAreaAnimation = 0x1042,

    /// <summary>
    ///     The SPI_GETFOREGROUNDLOCKTIMEOUT constant.
    /// </summary>
    GetForegroundLockTimeout = 0x2000,

    /// <summary>
    ///     The SPI_SETFOREGROUNDLOCKTIMEOUT constant.
    /// </summary>
    SetForegroundLockTimeout = 0x2001,

    /// <summary>
    ///     The SPI_GETACTIVEWNDTRKTIMEOUT constant.
    /// </summary>
    GetActiveWndTrkTimeout = 0x2002,

    /// <summary>
    ///     The SPI_SETACTIVEWNDTRKTIMEOUT constant.
    /// </summary>
    SetActiveWndTrkTimeout = 0x2003,

    /// <summary>
    ///     The SPI_GETFOREGROUNDFLASHCOUNT constant.
    /// </summary>
    GetForegroundFlashCount = 0x2004,

    /// <summary>
    ///     The SPI_SETFOREGROUNDFLASHCOUNT constant.
    /// </summary>
    SetForegroundFlashCount = 0x2005,

    /// <summary>
    ///     The SPI_GETCARETWIDTH constant.
    /// </summary>
    GetCaretWidth = 0x2006,

    /// <summary>
    ///     The SPI_SETCARETWIDTH constant.
    /// </summary>
    SetCaretWidth = 0x2007,

    /// <summary>
    ///     The SPI_GETMOUSECLICKLOCKTIME constant.
    /// </summary>
    GetMouseClickLockTime = 0x2008,

    /// <summary>
    ///     The SPI_SETMOUSECLICKLOCKTIME constant.
    /// </summary>
    SetMouseClickLockTime = 0x2009,

    /// <summary>
    ///     The SPI_GETFONTSMOOTHINGTYPE constant.
    /// </summary>
    GetFontSmoothingType = 0x200A,

    /// <summary>
    ///     The SPI_SETFONTSMOOTHINGTYPE constant.
    /// </summary>
    SetFontSmoothingType = 0x200B,

    /// <summary>
    ///     The SPI_GETFONTSMOOTHINGCONTRAST constant.
    /// </summary>
    GetFontSmoothingContrast = 0x200C,

    /// <summary>
    ///     The SPI_SETFONTSMOOTHINGCONTRAST constant.
    /// </summary>
    SetFontSmoothingContrast = 0x200D,

    /// <summary>
    ///     The SPI_GETFOCUSBORDERWIDTH constant.
    /// </summary>
    GetFocusBorderWidth = 0x200E,

    /// <summary>
    ///     The SPI_SETFOCUSBORDERWIDTH constant.
    /// </summary>
    SetFocusBorderWidth = 0x200F,

    /// <summary>
    ///     The SPI_GETFOCUSBORDERHEIGHT constant.
    /// </summary>
    GetFocusBorderHeight = 0x2010,

    /// <summary>
    ///     The SPI_SETFOCUSBORDERHEIGHT constant.
    /// </summary>
    SetFocusBorderHeight = 0x2011,

    /// <summary>
    ///     The SPI_GETFONTSMOOTHINGORIENTATION constant.
    /// </summary>
    GetFontSmoothingOrientation = 0x2012,

    /// <summary>
    ///     The SPI_SETFONTSMOOTHINGORIENTATION constant.
    /// </summary>
    SetFontSmoothingOrientation = 0x2013,
}
