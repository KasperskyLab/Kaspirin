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
using System.Collections.Generic;

namespace Kaspirin.UI.Framework.UiKit.Icons
{
    internal sealed class UIKitIconMetadataStorage
    {
        public static void Register(Enum icon, bool isAutoRTL, bool isColorfull)
        {
            Guard.Argument(
                !_storage.ContainsKey(icon),
                $"Duplicate registration of '{icon.GetType().Name}.{icon}' icon metadata");

            _storage.Add(icon, new UIKitIconMetadata(isAutoRTL, isColorfull));
        }

        public static UIKitIconMetadata Get(Enum icon)
        {
            if (_storage.TryGetValue(icon, out var metainfo))
            {
                return metainfo;
            }

            return _default;
        }

        private static readonly IDictionary<Enum, UIKitIconMetadata> _storage = new Dictionary<Enum, UIKitIconMetadata>();
        private static readonly UIKitIconMetadata _default = new(false, false);
    }
}
