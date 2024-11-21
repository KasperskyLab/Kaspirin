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
using System.Globalization;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Converters.ThicknessConverters
{
    public sealed class ThicknessComponentsSelectorConverter : ValueConverterMarkupExtension<ThicknessComponentsSelectorConverter>
    {
        public bool InvertComponents { get; set; }

        public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Guard.ArgumentIsNotNull(value);
            Guard.ArgumentIsNotNull(parameter);

            if (value is not Thickness thickness || parameter is not ThicknessComponents components)
            {
                return DependencyProperty.UnsetValue;
            }

            var multiplier = InvertComponents ? -1 : 1;

            var left = components is ThicknessComponents.Left or ThicknessComponents.Horizontal or ThicknessComponents.All
                ? multiplier * thickness.Left
                : 0;

            var top = components is ThicknessComponents.Top or ThicknessComponents.Vertical or ThicknessComponents.All
                ? multiplier * thickness.Top
                : 0;

            var right = components is ThicknessComponents.Right or ThicknessComponents.Horizontal or ThicknessComponents.All
                ? multiplier * thickness.Right
                : 0;

            var bottom = components is ThicknessComponents.Bottom or ThicknessComponents.Vertical or ThicknessComponents.All
                ? multiplier * thickness.Bottom
                : 0;

            return new Thickness(left, top, right, bottom);
        }
    }
}
