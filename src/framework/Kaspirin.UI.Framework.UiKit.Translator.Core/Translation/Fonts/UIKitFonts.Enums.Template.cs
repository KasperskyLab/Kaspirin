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

@FileComment
using System;
using Kaspirin.UI.Framework.UiKit.@PaletteNamespacePart.Palette;

namespace Kaspirin.UI.Framework.UiKit.@FontNamespacePart.Fonts
{
    public sealed class UIKitFontStorage
    {
        public static string Map(UIKitFontStyle id)
            => $"@FontStyleIdPrefix{id}";

        public static string Map(UIKitFontBrush id)
        {
            var paletteIdString = $"@FontBrushPrefix{id}";

            var paletteId = (UIKitPaletteStorage.UIKitPalette)Enum.Parse(typeof(UIKitPaletteStorage.UIKitPalette), paletteIdString);

            return UIKitPaletteStorage.Map(paletteId);
        }

        public enum UIKitFontBrush
        {
@FontBrushes
        }

        public enum UIKitFontStyle
        {
@FontStyles
        }
    }
}
