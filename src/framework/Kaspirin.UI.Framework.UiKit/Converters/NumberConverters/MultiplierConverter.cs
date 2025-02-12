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

namespace Kaspirin.UI.Framework.UiKit.Converters.NumberConverters
{
    public sealed class MultiplierConverter : ValueConverterMarkupExtension<MultiplierConverter>
    {
        public double Factor { get; set; } = 1;

        public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                null or not IConvertible => DependencyProperty.UnsetValue,
                _ => System.Convert.ToDouble(value) * Factor
            };
        }
    }
}
