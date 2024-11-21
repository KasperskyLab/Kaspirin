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

namespace Kaspirin.UI.Framework.UiKit.Illustrations
{
    internal sealed class UIKitIllustrationMetadata
    {
        public UIKitIllustrationMetadata(bool isAutoRTL, double height, double width)
        {
            Guard.Argument(height > 0);
            Guard.Argument(width > 0);

            IsAutoRTL = isAutoRTL;
            Height = height;
            Width = width;
        }

        public bool IsAutoRTL { get; }

        public double Height { get; }

        public double Width { get; }
    }
}
