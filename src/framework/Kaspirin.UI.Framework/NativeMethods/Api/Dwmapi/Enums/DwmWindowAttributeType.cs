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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Dwmapi.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute">Learn more</seealso>.
    /// </summary>
    public enum DwmWindowAttributeType : uint
    {
        DWMWA_NCRENDERING_ENABLED = 1,
        DWMWA_NCRENDERING_POLICY = 2,
        DWMWA_TRANSITIONS_FORCEDISABLED = 3,
        DWMWA_ALLOW_NCPAINT = 4,
        DWMWA_CAPTION_BUTTON_BOUNDS = 5,
        DWMWA_NONCLIENT_RTL_LAYOUT = 6,
        DWMWA_FORCE_ICONIC_REPRESENTATION = 7,
        DWMWA_FLIP3D_POLICY = 8,
        DWMWA_EXTENDED_FRAME_BOUNDS = 9,
        DWMWA_HAS_ICONIC_BITMAP = 10,
        DWMWA_DISALLOW_PEEK = 11,
        DWMWA_EXCLUDED_FROM_PEEK = 12,
        DWMWA_CLOAK = 13,
        DWMWA_CLOAKED = 14,
        DWMWA_FREEZE_REPRESENTATION = 15,
        DWMWA_PASSIVE_UPDATE_MODE = 16,
        DWMWA_USE_HOSTBACKDROPBRUSH = 17,
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,
        DWMWA_BORDER_COLOR = 34,
        DWMWA_CAPTION_COLOR = 35,
        DWMWA_TEXT_COLOR = 36,
        DWMWA_VISIBLE_FRAME_BORDER_THICKNESS = 37,
        DWMWA_SYSTEMBACKDROP_TYPE = 38,
        DWMWA_LAST = 39,
    }
}
