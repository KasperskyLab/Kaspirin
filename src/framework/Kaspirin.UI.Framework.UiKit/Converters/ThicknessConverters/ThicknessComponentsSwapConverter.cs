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
    public sealed class ThicknessComponentsSwapConverter : ValueConverterMarkupExtension<ThicknessComponentsSwapConverter>
    {
        public ThicknessComponentsSwapMode Mode { get; set; } = ThicknessComponentsSwapMode.All;

        public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Guard.ArgumentIsNotNull(value);

            if (value is not Thickness thickness)
            {
                return DependencyProperty.UnsetValue;
            }

            var (left, top, right, bottom) = (thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);

            return Mode switch
            {
                ThicknessComponentsSwapMode.Horizontal => new Thickness(left: right, top, right: left, bottom),
                ThicknessComponentsSwapMode.Vertical => new Thickness(left, top: bottom, right, bottom: top),
                ThicknessComponentsSwapMode.All => new Thickness(left: right, top: bottom, right: left, bottom: top),
                _ => throw new ArgumentOutOfRangeException($"Mode {Mode} not supported."),
            };
        }
    }
}
