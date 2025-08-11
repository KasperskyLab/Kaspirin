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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest">Learn more</seealso>.
/// </summary>
public enum HitTestType : int
{
    /// <summary>
    ///     The HTERROR constant.
    /// </summary>
    Error = -2,

    /// <summary>
    ///     The HTTRANSPARENT constant.
    /// </summary>
    Transparent = -1,

    /// <summary>
    ///     The HTNOWHERE constant.
    /// </summary>
    Nowhere = 0,

    /// <summary>
    ///     The HTCLIENT constant.
    /// </summary>
    Client = 1,

    /// <summary>
    ///     The HTCAPTION constant.
    /// </summary>
    Caption = 2,

    /// <summary>
    ///     The HTSYSMENU constant.
    /// </summary>
    SysMenu = 3,

    /// <summary>
    ///     The HTGROWBOX constant.
    /// </summary>
    GrowBox = 4,

    /// <summary>
    ///     The HTMENU constant.
    /// </summary>
    Menu = 5,

    /// <summary>
    ///     The HTHSCROLL constant.
    /// </summary>
    HScroll = 6,

    /// <summary>
    ///     The HTVSCROLL constant.
    /// </summary>
    VScroll = 7,

    /// <summary>
    ///     The HTMINBUTTON constant.
    /// </summary>
    MinButton = 8,

    /// <summary>
    ///     The HTMAXBUTTON constant.
    /// </summary>
    MaxButton = 9,

    /// <summary>
    ///     The HTLEFT constant.
    /// </summary>
    Left = 10,

    /// <summary>
    ///     The HTRIGHT constant.
    /// </summary>
    Right = 11,

    /// <summary>
    ///     The HTTOP constant.
    /// </summary>
    Top = 12,

    /// <summary>
    ///     The HTTOPLEFT constant.
    /// </summary>
    TopLeft = 13,

    /// <summary>
    ///     The HTTOPRIGHT constant.
    /// </summary>
    TopRight = 14,

    /// <summary>
    ///     The HTBOTTOM constant.
    /// </summary>
    Bottom = 15,

    /// <summary>
    ///     The HTBOTTOMLEFT constant.
    /// </summary>
    BottomLeft = 16,

    /// <summary>
    ///     The HTBOTTOMRIGHT constant.
    /// </summary>
    BottomRight = 17,

    /// <summary>
    ///     The HTBORDER constant.
    /// </summary>
    Border = 18,

    /// <summary>
    ///     The HTOBJECT constant.
    /// </summary>
    Object = 19,

    /// <summary>
    ///     The HTCLOSE constant.
    /// </summary>
    Close = 20,

    /// <summary>
    ///     The HTHELP constant.
    /// </summary>
    Help = 21,
}
