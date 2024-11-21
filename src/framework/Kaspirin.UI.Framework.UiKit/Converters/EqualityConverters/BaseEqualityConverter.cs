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
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.EqualityConverters
{
    public abstract class BaseEqualityConverter<T> : ValueConverterMarkupExtension<BaseEqualityConverter<T>>
    {
        protected BaseEqualityConverter(T? trueValue, T? falseValue)
        {
            ParameterType = EqualityParameterType.Default;

            True = trueValue;
            False = falseValue;
        }

        public T? True { get; set; }

        public T? False { get; set; }

        public EqualityParameterType ParameterType { get; set; }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => IsEqual(value, parameter) ? True : False;

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => IsEqual(value, True) ? parameter : Binding.DoNothing;

        private bool IsEqual(object? value, object? parameter)
        {
            switch (ParameterType)
            {
                case EqualityParameterType.Default:
                    return Equals(value, parameter);

                case EqualityParameterType.Int32:
                    return Equals(value, System.Convert.ToInt32(parameter));

                case EqualityParameterType.UInt32:
                    return Equals(value, System.Convert.ToUInt32(parameter));

                case EqualityParameterType.UInt64:
                    return Equals(value, System.Convert.ToUInt64(parameter));

                case EqualityParameterType.Double:
                    return System.Convert.ToDouble(value).NearlyEqual(System.Convert.ToDouble(parameter));

                case EqualityParameterType.String:
                    return string.IsNullOrWhiteSpace(value as string) && parameter == null || Equals(value, parameter);

                case EqualityParameterType.Type:
                    if (parameter is not Type targetType)
                    {
                        return false;
                    }

                    return value?.GetType().Equals(targetType) ?? false;

                default:
                    throw new NotSupportedException($"Value {ParameterType} not supported.");
            }
        }
    }
}
