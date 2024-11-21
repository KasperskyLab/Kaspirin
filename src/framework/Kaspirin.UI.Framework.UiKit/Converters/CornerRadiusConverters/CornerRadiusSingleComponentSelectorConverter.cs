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

namespace Kaspirin.UI.Framework.UiKit.Converters.CornerRadiusConverters
{
    public sealed class CornerRadiusSingleComponentSelectorConverter : ValueConverterMarkupExtension<CornerRadiusSingleComponentSelectorConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Guard.ArgumentIsNotNull(value);
            Guard.ArgumentIsNotNull(parameter);

            if (value is not CornerRadius cornerRadius || parameter is not CornerRadiusSingleComponent component)
            {
                return DependencyProperty.UnsetValue;
            }

            switch (component)
            {
                case CornerRadiusSingleComponent.TopLeft:
                    return cornerRadius.TopLeft;
                case CornerRadiusSingleComponent.TopRight:
                    return cornerRadius.TopRight;
                case CornerRadiusSingleComponent.BottomRight:
                    return cornerRadius.BottomRight;
                case CornerRadiusSingleComponent.BottomLeft:
                    return cornerRadius.BottomLeft;
                case CornerRadiusSingleComponent.All:
                    Guard.Assert(
                        cornerRadius.TopLeft.NearlyEqual(cornerRadius.TopRight) &&
                        cornerRadius.TopLeft.NearlyEqual(cornerRadius.BottomRight) &&
                        cornerRadius.TopLeft.NearlyEqual(cornerRadius.BottomLeft),
                        $"CornerRadius value with equal components is expected, but got {cornerRadius}");
                    return cornerRadius.TopLeft;
                default:
                    throw new ArgumentOutOfRangeException($"Value {component} not supported.");
            }
        }
    }
}
