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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes">Learn more</seealso>.
/// </summary>
public enum VirtualKey : ushort
{
    /// <summary>
    ///     The VK_LBUTTON constant.
    /// </summary>
    LButton = 0x01,

    /// <summary>
    ///     The VK_RBUTTON constant.
    /// </summary>
    RButton = 0x02,

    /// <summary>
    ///     The VK_CANCEL constant.
    /// </summary>
    Cancel = 0x03,

    /// <summary>
    ///     The VK_MBUTTON constant.
    /// </summary>
    MButton = 0x04,

    /// <summary>
    ///     The VK_XBUTTON1 constant.
    /// </summary>
    XButton1 = 0x05,

    /// <summary>
    ///     The VK_XBUTTON2 constant.
    /// </summary>
    XButton2 = 0x06,

    /// <summary>
    ///     The VK_BACK constant.
    /// </summary>
    Back = 0x08,

    /// <summary>
    ///     The VK_TAB constant.
    /// </summary>
    Tab = 0x09,

    /// <summary>
    ///     The VK_CLEAR constant.
    /// </summary>
    Clear = 0x0C,

    /// <summary>
    ///     The VK_RETURN constant.
    /// </summary>
    Return = 0x0D,

    /// <summary>
    ///     The VK_SHIFT constant.
    /// </summary>
    Shift = 0x10,

    /// <summary>
    ///     The VK_CONTROL constant.
    /// </summary>
    Control = 0x11,

    /// <summary>
    ///     The VK_MENU constant.
    /// </summary>
    Menu = 0x12,

    /// <summary>
    ///     The VK_PAUSE constant.
    /// </summary>
    Pause = 0x13,

    /// <summary>
    ///     The VK_CAPITAL constant.
    /// </summary>
    Capital = 0x14,

    /// <summary>
    ///     The VK_KANA constant.
    /// </summary>
    Kana = 0x15,

    /// <summary>
    ///     The VK_HANGUL constant.
    /// </summary>
    Hangul = 0x15,

    /// <summary>
    ///     The VK_IME_ON constant.
    /// </summary>
    ImeOn = 0x16,

    /// <summary>
    ///     The VK_JUNJA constant.
    /// </summary>
    Junja = 0x17,

    /// <summary>
    ///     The VK_FINAL constant.
    /// </summary>
    Final = 0x18,

    /// <summary>
    ///     The VK_HANJA constant.
    /// </summary>
    Hanja = 0x19,

    /// <summary>
    ///     The VK_KANJI constant.
    /// </summary>
    Kanji = 0x19,

    /// <summary>
    ///     The VK_IME_OFF constant.
    /// </summary>
    ImeOff = 0x1A,

    /// <summary>
    ///     The VK_ESCAPE constant.
    /// </summary>
    Escape = 0x1B,

    /// <summary>
    ///     The VK_CONVERT constant.
    /// </summary>
    Convert = 0x1C,

    /// <summary>
    ///     The VK_NONCONVERT constant.
    /// </summary>
    NonConvert = 0x1D,

    /// <summary>
    ///     The VK_ACCEPT constant.
    /// </summary>
    Accept = 0x1E,

    /// <summary>
    ///     The VK_MODECHANGE constant.
    /// </summary>
    ModeChange = 0x1F,

    /// <summary>
    ///     The VK_SPACE constant.
    /// </summary>
    Space = 0x20,

    /// <summary>
    ///     The VK_PRIOR constant.
    /// </summary>
    Prior = 0x21,

    /// <summary>
    ///     The VK_NEXT constant.
    /// </summary>
    Next = 0x22,

    /// <summary>
    ///     The VK_END constant.
    /// </summary>
    End = 0x23,

    /// <summary>
    ///     The VK_HOME constant.
    /// </summary>
    Home = 0x24,

    /// <summary>
    ///     The VK_LEFT constant.
    /// </summary>
    Left = 0x25,

    /// <summary>
    ///     The VK_UP constant.
    /// </summary>
    Up = 0x26,

    /// <summary>
    ///     The VK_RIGHT constant.
    /// </summary>
    Right = 0x27,

    /// <summary>
    ///     The VK_DOWN constant.
    /// </summary>
    Down = 0x28,

    /// <summary>
    ///     The VK_SELECT constant.
    /// </summary>
    Select = 0x29,

    /// <summary>
    ///     The VK_PRINT constant.
    /// </summary>
    Print = 0x2A,

    /// <summary>
    ///     The VK_EXECUTE constant.
    /// </summary>
    Execute = 0x2B,

    /// <summary>
    ///     The VK_SNAPSHOT constant.
    /// </summary>
    Snapshot = 0x2C,

    /// <summary>
    ///     The VK_INSERT constant.
    /// </summary>
    Insert = 0x2D,

    /// <summary>
    ///     The VK_DELETE constant.
    /// </summary>
    Delete = 0x2E,

    /// <summary>
    ///     The VK_HELP constant.
    /// </summary>
    Help = 0x2F,

    /// <summary>
    ///     The Key0 constant.
    /// </summary>
    Key0 = 0x30,

    /// <summary>
    ///     The Key1 constant.
    /// </summary>
    Key1 = 0x31,

    /// <summary>
    ///     The Key2 constant.
    /// </summary>
    Key2 = 0x32,

    /// <summary>
    ///     The Key3 constant.
    /// </summary>
    Key3 = 0x33,

    /// <summary>
    ///     The Key4 constant.
    /// </summary>
    Key4 = 0x34,

    /// <summary>
    ///     The Key5 constant.
    /// </summary>
    Key5 = 0x35,

    /// <summary>
    ///     The Key6 constant.
    /// </summary>
    Key6 = 0x36,

    /// <summary>
    ///     The Key7 constant.
    /// </summary>
    Key7 = 0x37,

    /// <summary>
    ///     The Key8 constant.
    /// </summary>
    Key8 = 0x38,

    /// <summary>
    ///     The Key9 constant.
    /// </summary>
    Key9 = 0x39,

    /// <summary>
    ///     The KeyA constant.
    /// </summary>
    KeyA = 0x41,

    /// <summary>
    ///     The KeyB constant.
    /// </summary>
    KeyB = 0x42,

    /// <summary>
    ///     The KeyC constant.
    /// </summary>
    KeyC = 0x43,

    /// <summary>
    ///     The KeyD constant.
    /// </summary>
    KeyD = 0x44,

    /// <summary>
    ///     The KeyE constant.
    /// </summary>
    KeyE = 0x45,

    /// <summary>
    ///     The KeyF constant.
    /// </summary>
    KeyF = 0x46,

    /// <summary>
    ///     The KeyG constant.
    /// </summary>
    KeyG = 0x47,

    /// <summary>
    ///     The KeyH constant.
    /// </summary>
    KeyH = 0x48,

    /// <summary>
    ///     The KeyI constant.
    /// </summary>
    KeyI = 0x49,

    /// <summary>
    ///     The KeyJ constant.
    /// </summary>
    KeyJ = 0x4A,

    /// <summary>
    ///     The KeyK constant.
    /// </summary>
    KeyK = 0x4B,

    /// <summary>
    ///     The KeyL constant.
    /// </summary>
    KeyL = 0x4C,

    /// <summary>
    ///     The KeyM constant.
    /// </summary>
    KeyM = 0x4D,

    /// <summary>
    ///     The KeyN constant.
    /// </summary>
    KeyN = 0x4E,

    /// <summary>
    ///     The KeyO constant.
    /// </summary>
    KeyO = 0x4F,

    /// <summary>
    ///     The KeyP constant.
    /// </summary>
    KeyP = 0x50,

    /// <summary>
    ///     The KeyQ constant.
    /// </summary>
    KeyQ = 0x51,

    /// <summary>
    ///     The KeyR constant.
    /// </summary>
    KeyR = 0x52,

    /// <summary>
    ///     The KeyS constant.
    /// </summary>
    KeyS = 0x53,

    /// <summary>
    ///     The KeyT constant.
    /// </summary>
    KeyT = 0x54,

    /// <summary>
    ///     The KeyU constant.
    /// </summary>
    KeyU = 0x55,

    /// <summary>
    ///     The KeyV constant.
    /// </summary>
    KeyV = 0x56,

    /// <summary>
    ///     The KeyW constant.
    /// </summary>
    KeyW = 0x57,

    /// <summary>
    ///     The KeyX constant.
    /// </summary>
    KeyX = 0x58,

    /// <summary>
    ///     The KeyY constant.
    /// </summary>
    KeyY = 0x59,

    /// <summary>
    ///     The KeyZ constant.
    /// </summary>
    KeyZ = 0x5A,

    /// <summary>
    ///     The VK_LWIN constant.
    /// </summary>
    LWin = 0x5B,

    /// <summary>
    ///     The VK_RWIN constant.
    /// </summary>
    RWin = 0x5C,

    /// <summary>
    ///     The VK_APPS constant.
    /// </summary>
    Apps = 0x5D,

    /// <summary>
    ///     The VK_SLEEP constant.
    /// </summary>
    Sleep = 0x5F,

    /// <summary>
    ///     The VK_NUMPAD0 constant.
    /// </summary>
    Numpad0 = 0x60,

    /// <summary>
    ///     The VK_NUMPAD1 constant.
    /// </summary>
    Numpad1 = 0x61,

    /// <summary>
    ///     The VK_NUMPAD2 constant.
    /// </summary>
    Numpad2 = 0x62,

    /// <summary>
    ///     The VK_NUMPAD3 constant.
    /// </summary>
    Numpad3 = 0x63,

    /// <summary>
    ///     The VK_NUMPAD4 constant.
    /// </summary>
    Numpad4 = 0x64,

    /// <summary>
    ///     The VK_NUMPAD5 constant.
    /// </summary>
    Numpad5 = 0x65,

    /// <summary>
    ///     The VK_NUMPAD6 constant.
    /// </summary>
    Numpad6 = 0x66,

    /// <summary>
    ///     The VK_NUMPAD7 constant.
    /// </summary>
    Numpad7 = 0x67,

    /// <summary>
    ///     The VK_NUMPAD8 constant.
    /// </summary>
    Numpad8 = 0x68,

    /// <summary>
    ///     The VK_NUMPAD9 constant.
    /// </summary>
    Numpad9 = 0x69,

    /// <summary>
    ///     The VK_MULTIPLY constant.
    /// </summary>
    Multiply = 0x6A,

    /// <summary>
    ///     The VK_ADD constant.
    /// </summary>
    Add = 0x6B,

    /// <summary>
    ///     The VK_SEPARATOR constant.
    /// </summary>
    Separator = 0x6C,

    /// <summary>
    ///     The VK_SUBTRACT constant.
    /// </summary>
    Subtract = 0x6D,

    /// <summary>
    ///     The VK_DECIMAL constant.
    /// </summary>
    Decimal = 0x6E,

    /// <summary>
    ///     The VK_DIVIDE constant.
    /// </summary>
    Divide = 0x6F,

    /// <summary>
    ///     The VK_F1 constant.
    /// </summary>
    F1 = 0x70,

    /// <summary>
    ///     The VK_F2 constant.
    /// </summary>
    F2 = 0x71,

    /// <summary>
    ///     The VK_F3 constant.
    /// </summary>
    F3 = 0x72,

    /// <summary>
    ///     The VK_F4 constant.
    /// </summary>
    F4 = 0x73,

    /// <summary>
    ///     The VK_F5 constant.
    /// </summary>
    F5 = 0x74,

    /// <summary>
    ///     The VK_F6 constant.
    /// </summary>
    F6 = 0x75,

    /// <summary>
    ///     The VK_F7 constant.
    /// </summary>
    F7 = 0x76,

    /// <summary>
    ///     The VK_F8 constant.
    /// </summary>
    F8 = 0x77,

    /// <summary>
    ///     The VK_F9 constant.
    /// </summary>
    F9 = 0x78,

    /// <summary>
    ///     The VK_F10 constant.
    /// </summary>
    F10 = 0x79,

    /// <summary>
    ///     The VK_F11 constant.
    /// </summary>
    F11 = 0x7A,

    /// <summary>
    ///     The VK_F12 constant.
    /// </summary>
    F12 = 0x7B,

    /// <summary>
    ///     The VK_F13 constant.
    /// </summary>
    F13 = 0x7C,

    /// <summary>
    ///     The VK_F14 constant.
    /// </summary>
    F14 = 0x7D,

    /// <summary>
    ///     The VK_F15 constant.
    /// </summary>
    F15 = 0x7E,

    /// <summary>
    ///     The VK_F16 constant.
    /// </summary>
    F16 = 0x7F,

    /// <summary>
    ///     The VK_F17 constant.
    /// </summary>
    F17 = 0x80,

    /// <summary>
    ///     The VK_F18 constant.
    /// </summary>
    F18 = 0x81,

    /// <summary>
    ///     The VK_F19 constant.
    /// </summary>
    F19 = 0x82,

    /// <summary>
    ///     The VK_F20 constant.
    /// </summary>
    F20 = 0x83,

    /// <summary>
    ///     The VK_F21 constant.
    /// </summary>
    F21 = 0x84,

    /// <summary>
    ///     The VK_F22 constant.
    /// </summary>
    F22 = 0x85,

    /// <summary>
    ///     The VK_F23 constant.
    /// </summary>
    F23 = 0x86,

    /// <summary>
    ///     The VK_F24 constant.
    /// </summary>
    F24 = 0x87,

    /// <summary>
    ///     The VK_NUMLOCK constant.
    /// </summary>
    NumLock = 0x90,

    /// <summary>
    ///     The VK_SCROLL constant.
    /// </summary>
    Scroll = 0x91,

    /// <summary>
    ///     The VK_LSHIFT constant.
    /// </summary>
    LShift = 0xA0,

    /// <summary>
    ///     The VK_RSHIFT constant.
    /// </summary>
    RShift = 0xA1,

    /// <summary>
    ///     The VK_LCONTROL constant.
    /// </summary>
    LControl = 0xA2,

    /// <summary>
    ///     The VK_RCONTROL constant.
    /// </summary>
    RControl = 0xA3,

    /// <summary>
    ///     The VK_LMENU constant.
    /// </summary>
    LMenu = 0xA4,

    /// <summary>
    ///     The VK_RMENU constant.
    /// </summary>
    RMenu = 0xA5,

    /// <summary>
    ///     The VK_BROWSER_BACK constant.
    /// </summary>
    BrowserBack = 0xA6,

    /// <summary>
    ///     The VK_BROWSER_FORWARD constant.
    /// </summary>
    BrowserForward = 0xA7,

    /// <summary>
    ///     The VK_BROWSER_REFRESH constant.
    /// </summary>
    BrowserRefresh = 0xA8,

    /// <summary>
    ///     The VK_BROWSER_STOP constant.
    /// </summary>
    BrowserStop = 0xA9,

    /// <summary>
    ///     The VK_BROWSER_SEARCH constant.
    /// </summary>
    BrowserSearch = 0xAA,

    /// <summary>
    ///     The VK_BROWSER_FAVORITES constant.
    /// </summary>
    BrowserFavorites = 0xAB,

    /// <summary>
    ///     The VK_BROWSER_HOME constant.
    /// </summary>
    BrowserHome = 0xAC,

    /// <summary>
    ///     The VK_VOLUME_MUTE constant.
    /// </summary>
    VolumeMute = 0xAD,

    /// <summary>
    ///     The VK_VOLUME_DOWN constant.
    /// </summary>
    VolumeDown = 0xAE,

    /// <summary>
    ///     The VK_VOLUME_UP constant.
    /// </summary>
    VolumeUp = 0xAF,

    /// <summary>
    ///     The VK_MEDIA_NEXT_TRACK constant.
    /// </summary>
    MediaNextTrack = 0xB0,

    /// <summary>
    ///     The VK_MEDIA_PREV_TRACK constant.
    /// </summary>
    MediaPrevTrack = 0xB1,

    /// <summary>
    ///     The VK_MEDIA_STOP constant.
    /// </summary>
    MediaStop = 0xB2,

    /// <summary>
    ///     The VK_MEDIA_PLAY_PAUSE constant.
    /// </summary>
    MediaPlayPause = 0xB3,

    /// <summary>
    ///     The VK_LAUNCH_MAIL constant.
    /// </summary>
    LaunchMail = 0xB4,

    /// <summary>
    ///     The VK_LAUNCH_MEDIA_SELECT constant.
    /// </summary>
    LaunchMediaSelect = 0xB5,

    /// <summary>
    ///     The VK_LAUNCH_APP1 constant.
    /// </summary>
    LaunchApp1 = 0xB6,

    /// <summary>
    ///     The VK_LAUNCH_APP2 constant.
    /// </summary>
    LaunchApp2 = 0xB7,

    /// <summary>
    ///     The VK_OEM_1 constant.
    /// </summary>
    Oem1 = 0xBA,

    /// <summary>
    ///     The VK_OEM_PLUS constant.
    /// </summary>
    OemPlus = 0xBB,

    /// <summary>
    ///     The VK_OEM_COMMA constant.
    /// </summary>
    OemComma = 0xBC,

    /// <summary>
    ///     The VK_OEM_MINUS constant.
    /// </summary>
    OemMinus = 0xBD,

    /// <summary>
    ///     The VK_OEM_PERIOD constant.
    /// </summary>
    OemPeriod = 0xBE,

    /// <summary>
    ///     The VK_OEM_2 constant.
    /// </summary>
    Oem2 = 0xBF,

    /// <summary>
    ///     The VK_OEM_3 constant.
    /// </summary>
    Oem3 = 0xC0,

    /// <summary>
    ///     The VK_OEM_4 constant.
    /// </summary>
    Oem4 = 0xDB,

    /// <summary>
    ///     The VK_OEM_5 constant.
    /// </summary>
    Oem5 = 0xDC,

    /// <summary>
    ///     The VK_OEM_6 constant.
    /// </summary>
    Oem6 = 0xDD,

    /// <summary>
    ///     The VK_OEM_7 constant.
    /// </summary>
    Oem7 = 0xDE,

    /// <summary>
    ///     The VK_OEM_8 constant.
    /// </summary>
    Oem8 = 0xDF,

    /// <summary>
    ///     The VK_OEM_102 constant.
    /// </summary>
    Oem102 = 0xE2,

    /// <summary>
    ///     The VK_PROCESSKEY constant.
    /// </summary>
    ProcessKey = 0xE5,

    /// <summary>
    ///     The VK_PACKET constant.
    /// </summary>
    Packet = 0xE7,

    /// <summary>
    ///     The VK_ATTN constant.
    /// </summary>
    Attn = 0xF6,

    /// <summary>
    ///     The VK_CRSEL constant.
    /// </summary>
    CrSel = 0xF7,

    /// <summary>
    ///     The VK_EXSEL constant.
    /// </summary>
    ExSel = 0xF8,

    /// <summary>
    ///     The VK_EREOF constant.
    /// </summary>
    ErEof = 0xF9,

    /// <summary>
    ///     The VK_PLAY constant.
    /// </summary>
    Play = 0xFA,

    /// <summary>
    ///     The VK_ZOOM constant.
    /// </summary>
    Zoom = 0xFB,

    /// <summary>
    ///     The VK_NONAME constant.
    /// </summary>
    NoName = 0xFC,

    /// <summary>
    ///     The VK_PA1 constant.
    /// </summary>
    Pa1 = 0xFD,

    /// <summary>
    ///     The VK_OEM_CLEAR constant.
    /// </summary>
    OemClear = 0xFE,
}