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

namespace Kaspirin.UI.Framework.UiKit.Illustrations
{
    internal sealed class UIKitIllustrationMetadataStorage
    {
        public static void Register(Enum illustration, bool isAutoRTL, double height, double width)
        {
            Guard.Argument(
                !_storage.ContainsKey(illustration),
                $"Duplicate registration of '{illustration.GetType().Name}.{illustration}' illustration metadata");

            _storage.Add(illustration, new UIKitIllustrationMetadata(isAutoRTL, height, width));
        }

        public static UIKitIllustrationMetadata Get(Enum illustration)
        {
            Guard.Assert(
                _storage.TryGetValue(illustration, out var metainfo),
                $"Unable to get '{illustration.GetType().Name}.{illustration}' illustration metadata");

            return metainfo;
        }

        private static readonly IDictionary<Enum, UIKitIllustrationMetadata> _storage = new Dictionary<Enum, UIKitIllustrationMetadata>();
    }
}
