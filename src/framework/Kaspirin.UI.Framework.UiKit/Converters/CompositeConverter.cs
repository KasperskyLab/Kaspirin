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

namespace Kaspirin.UI.Framework.UiKit.Converters
{
    /// <summary>
    ///     Provides an implementation of <see cref="IValueConverter" /> that performs sequential conversion
    ///     of values through several <see cref="IValueConverter" />.
    /// </summary>
    public sealed class CompositeConverter : IValueConverter
    {
        /// <summary>
        ///     Creates an object <see cref="CompositeConverter" />.
        /// </summary>
        /// <param name="converters">
        ///     Converters involved in sequential conversion.
        /// </param>
        public CompositeConverter(params IValueConverter[] converters)
        {
            _converters = Guard.EnsureArgumentIsNotNull(converters);
        }

        /// <summary>
        ///     Converts <paramref name="value" />, sequentially transferring the conversion result between converters.
        /// </summary>
        /// <param name="value">
        ///     The converted value.
        /// </param>
        /// <param name="targetType">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <param name="parameter">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <param name="culture">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <returns>
        ///     The result of the conversion of the last converter.
        /// </returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => _converters.WhereNotNull().Aggregate(value, (v, converter) => converter.Convert(v, targetType, parameter, culture));

        /// <summary>
        ///     Performs reverse conversion <paramref name="value" />, sequentially transmitting the conversion result between converters.
        /// </summary>
        /// <remarks>
        ///     In reverse conversion, the converters are called in reverse order.
        /// </remarks>
        /// <param name="value">
        ///     The converted value.
        /// </param>
        /// <param name="targetType">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <param name="parameter">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <param name="culture">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <returns>
        ///     The result of the reverse conversion of the first converter.
        /// </returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => _converters.WhereNotNull().Reverse().Aggregate(value, (v, converter) => converter.ConvertBack(v, targetType, parameter, culture));

        private readonly IValueConverter[] _converters;
    }
}
