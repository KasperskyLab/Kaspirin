// Copyright © 2025 AO Kaspersky Lab.
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

using System.Threading;
using Kaspirin.UI.Framework.UiKit.Palettes;

namespace Kaspirin.UI.Framework.UiKit.@PaletteNamespacePart.Palette;

public enum UIKitPalette
{
    Transparent = 0,
@PaletteItems
}

public enum UIKitPaletteScope
{
    UIKitUnset = 0,
@PaletteItemScopes
}

public sealed class UIKitPaletteExtension : UIKitPaletteExtension<UIKitPalette>
{
    static UIKitPaletteExtension()
    {
        UIKitPaletteMetadataRegistrar.RegisterMetadata();
    }
}

internal static class UIKitPaletteMetadataRegistrar
{
    internal static void RegisterMetadata()
    {
        if (Interlocked.CompareExchange(ref _isRegistered, 1, 0) == 0)
        {
@PaletteMetadataRegistration
        }
    }

    private static int _isRegistered = 0;
}