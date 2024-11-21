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

using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(CarouselItem))]
    public sealed class Carousel : SelectorList<CarouselItem>
    {
        static Carousel()
        {
            SelectionModeProperty.OverrideMetadata(typeof(Carousel),
                new FrameworkPropertyMetadata(SelectionMode.Single, FrameworkPropertyMetadataOptions.None, null, CoerceSelectionMode));
        }

        private static object CoerceSelectionMode(DependencyObject d, object baseValue)
        {
            return SelectionMode.Single;
        }
    }
}
