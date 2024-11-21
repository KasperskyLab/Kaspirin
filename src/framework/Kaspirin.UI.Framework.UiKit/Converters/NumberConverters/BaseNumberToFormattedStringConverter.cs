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
    public abstract class BaseNumberToFormattedStringConverter : ValueConverterMarkupExtension<BaseNumberToFormattedStringConverter>
    {
        private static bool ValueIsValid(object? value)
        {
            return value switch
            {
                null => false,
                _ => value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal
            };
        }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return ValueIsValid(value) ? Convert(value!) : DependencyProperty.UnsetValue;
        }

        public abstract object Convert(object value);
    }
}
