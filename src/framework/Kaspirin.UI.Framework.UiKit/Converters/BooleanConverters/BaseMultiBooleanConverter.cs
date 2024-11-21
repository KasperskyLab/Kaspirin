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
using System.Linq;
using System.Windows.Data;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Converters.BooleanConverters
{
    /// <summary>
    ///     Provides a basic implementation of <see cref="IMultiValueConverter" />, in which the converted
    ///     value can take the values <see langword="true" /> or <see langword="false" />, and the result
    ///     is determined by the properties <see cref="True" /> and <see cref="False" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of conversion result.
    /// </typeparam>
    public abstract class BaseMultiBooleanConverter<T> : MultiValueConverterMarkupExtension<BaseMultiBooleanConverter<T>>
    {
        /// <summary>
        ///     Creates an object <see cref="BaseMultiBooleanConverter{T}" />.
        /// </summary>
        /// <param name="trueValue">
        ///     The default result when converting the value <see langword="true" />.
        /// </param>
        /// <param name="falseValue">
        ///     The default result when converting the value <see langword="false" />.
        /// </param>
        protected BaseMultiBooleanConverter(T? trueValue, T? falseValue)
        {
            True = trueValue;
            False = falseValue;

            Operation = MultiBooleanOperation.And;
        }

        /// <summary>
        ///     The result of converting the value <see langword="true" />.
        /// </summary>
        public T? True { get; set; }

        /// <summary>
        ///     The result of converting the value <see langword="false" />.
        /// </summary>
        public T? False { get; set; }

        /// <summary>
        ///     The logical operation involved in the conversion.
        /// </summary>
        public MultiBooleanOperation Operation { get; set; }

        /// <summary>
        ///     Converts <paramref name="values" /> to property values <see cref="True" /> or <see cref="False" />.
        /// </summary>
        /// <remarks>
        ///     Values from <paramref name="values" /> are converted to <see langword="bool" /> by calling <see cref="System.Convert.ToBoolean(object)" />.
        /// </remarks>
        /// <param name="values">
        ///     Converted values.
        /// </param>
        /// <param name="targetType">
        ///     Not used.
        /// </param>
        /// <param name="parameter">
        ///     Not used.
        /// </param>
        /// <param name="culture">
        ///     Not used.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the result of logical operations <see cref="Operation" />
        ///     with all values <paramref name="values" /> is <see langword="true" />, otherwise <see cref="False" />.
        /// </returns>
        public override object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            Guard.ArgumentIsNotNull(values);

            var boolValues = values.Where(v => v != DependencyProperty.UnsetValue).Select(System.Convert.ToBoolean);

            var result = Operation switch
            {
                MultiBooleanOperation.And => boolValues.All(v => v),
                MultiBooleanOperation.Or => boolValues.Any(v => v),
                _ => throw new NotSupportedException($"Value {Operation} not supported.")
            };

            return result ? True : False;
        }
    }
}
