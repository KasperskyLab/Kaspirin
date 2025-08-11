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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Dwmapi.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute">Learn more</seealso>.
/// </summary>
public enum DwmWindowAttributeType : uint
{
    /// <summary>
    ///     The DWMWA_NCRENDERING_ENABLED constant.
    /// </summary>
    NcRenderingEnabled = 1,

    /// <summary>
    ///     The DWMWA_NCRENDERING_POLICY constant.
    /// </summary>
    NcRenderingPolicy = 2,

    /// <summary>
    ///     The DWMWA_TRANSITIONS_FORCEDISABLED constant.
    /// </summary>
    TransitionsForceDisabled = 3,

    /// <summary>
    ///     The DWMWA_ALLOW_NCPAINT constant.
    /// </summary>
    AllowNcPaint = 4,

    /// <summary>
    ///     The DWMWA_CAPTION_BUTTON_BOUNDS constant.
    /// </summary>
    CaptionButtonBounds = 5,

    /// <summary>
    ///     The DWMWA_NONCLIENT_RTL_LAYOUT constant.
    /// </summary>
    NonclientRtlLayout = 6,

    /// <summary>
    ///     The DWMWA_FORCE_ICONIC_REPRESENTATION constant.
    /// </summary>
    ForceIconicRepresentation = 7,

    /// <summary>
    ///     The DWMWA_FLIP3D_POLICY constant.
    /// </summary>
    Flip3dPolicy = 8,

    /// <summary>
    ///     The DWMWA_EXTENDED_FRAME_BOUNDS constant.
    /// </summary>
    ExtendedFrameBounds = 9,

    /// <summary>
    ///     The DWMWA_HAS_ICONIC_BITMAP constant.
    /// </summary>
    HasIconicBitmap = 10,

    /// <summary>
    ///     The DWMWA_DISALLOW_PEEK constant.
    /// </summary>
    DisallowPeek = 11,

    /// <summary>
    ///     The DWMWA_EXCLUDED_FROM_PEEK constant.
    /// </summary>
    ExcludedFromPeek = 12,

    /// <summary>
    ///     The DWMWA_CLOAK constant.
    /// </summary>
    Cloak = 13,

    /// <summary>
    ///     The DWMWA_CLOAKED constant.
    /// </summary>
    Cloaked = 14,

    /// <summary>
    ///     The DWMWA_FREEZE_REPRESENTATION constant.
    /// </summary>
    FreezeRepresentation = 15,

    /// <summary>
    ///     The DWMWA_PASSIVE_UPDATE_MODE constant.
    /// </summary>
    PassiveUpdateMode = 16,

    /// <summary>
    ///     The DWMWA_USE_HOSTBACKDROPBRUSH constant.
    /// </summary>
    UseHostBackDropBrush = 17,

    /// <summary>
    ///     The DWMWA_USE_IMMERSIVE_DARK_MODE constant.
    /// </summary>
    UseImmersiveDarkMode = 20,

    /// <summary>
    ///     The DWMWA_WINDOW_CORNER_PREFERENCE constant.
    /// </summary>
    WindowCornerPreference = 33,

    /// <summary>
    ///     The DWMWA_BORDER_COLOR constant.
    /// </summary>
    BorderColor = 34,

    /// <summary>
    ///     The DWMWA_CAPTION_COLOR constant.
    /// </summary>
    CaptionColor = 35,

    /// <summary>
    ///     The DWMWA_TEXT_COLOR constant.
    /// </summary>
    TextColor = 36,

    /// <summary>
    ///     The DWMWA_VISIBLE_FRAME_BORDER_THICKNESS constant.
    /// </summary>
    VisibleFrameBorderThickness = 37,

    /// <summary>
    ///     The DWMWA_SYSTEMBACKDROP_TYPE constant.
    /// </summary>
    SystemBackDropType = 38,

    /// <summary>
    ///     The DWMWA_LAST constant.
    /// </summary>
    Last = 39,
}
