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

using System;
using System.Collections.Generic;

namespace Kaspirin.UI.Framework.UiKit.Palettes;

internal sealed class UIKitPaletteMetadataStorage
{
    public static void Register(Enum color, Enum scope, string resourceId)
    {
        Guard.Argument(
            !_storage.ContainsKey(color),
            $"Duplicate registration of '{color.GetType().Name}.{color}' color metadata");

        _storage.Add(color, new UIKitPaletteMetadata(scope, resourceId));
    }

    public static UIKitPaletteMetadata Get(Enum color)
    {
        if (_storage.TryGetValue(color, out var data))
        {
            return data;
        }

        throw new InvalidOperationException($"Color '{color.GetType().Name}.{color}' is not registered in storage.");
    }

    private static readonly IDictionary<Enum, UIKitPaletteMetadata> _storage = new Dictionary<Enum, UIKitPaletteMetadata>();
}
