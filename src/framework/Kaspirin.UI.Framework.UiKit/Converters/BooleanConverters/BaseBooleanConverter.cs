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
using System.Globalization;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.BaseConverters
{
    /// <summary>
    ///     Provides a basic implementation of <see cref="IValueConverter" />, in which the converted value
    ///     can take the values <see langword="true" /> or <see langword="false" />, and the result is
    ///     determined by the properties <see cref="True" /> and <see cref="False" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of conversion result.
    /// </typeparam>
    public abstract class BaseBooleanConverter<T> : ValueConverterMarkupExtension<BaseBooleanConverter<T>>
    {
        /// <summary>
        ///     Creates an object <see cref="BaseBooleanConverter{T}" />.
        /// </summary>
        /// <param name="trueValue">
        ///     The default result when converting the value <see langword="true" />.
        /// </param>
        /// <param name="falseValue">
        ///     The default result when converting the value <see langword="false" />.
        /// </param>
        protected BaseBooleanConverter(T? trueValue, T? falseValue)
        {
            True = trueValue;
            False = falseValue;
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
        ///     Converts <paramref name="value" /> to property values <see cref="True" /> or <see cref="False" />.
        /// </summary>
        /// <remarks>
        ///     The value of <paramref name="value" /> is converted to <see cref="bool" /> by calling <see cref="Convert.ToBoolean(object)" />.
        /// </remarks>
        /// <param name="value">
        ///     The converted value.
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
        ///     Returns <see langword="true" /> when <paramref name="value" /> is <see langword="true" />,
        ///     otherwise <see cref="False" />.
        /// </returns>
        public sealed override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => System.Convert.ToBoolean(value as IConvertible) ? True : False;

        /// <summary>
        ///     Performs the reverse conversion of <paramref name="value" /> to <see langword="true" /> or <see langword="false" />,
        ///     comparing <paramref name="value" /> with <see cref="True" />.
        /// </summary>
        /// <remarks>
        ///     To compare <paramref name="value" /> with <see cref="True" />, <see cref="EqualityComparer{T}.Default" /> is used.
        /// </remarks>
        /// <param name="value">
        ///     The converted value.
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
        ///     Returns <see langword="true" /> when <paramref name="value" /> is <see cref="True" />, otherwise <see langword="false" />.
        /// </returns>
        public sealed override object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => value is T && EqualityComparer<T>.Default.Equals((T)value, True);
    }
}
