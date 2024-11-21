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

namespace Kaspirin.UI.Framework.UiKit.Converters
{
    /// <summary>
    ///     Provides an implementation of <see cref="IMultiValueConverter" /> that performs conversion
    ///     via <see cref="IMultiValueConverter" /> and sequential call of conversions <see cref="IValueConverter" />.
    /// </summary>
    public sealed class CompositeMultiConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Creates an object <see cref="CompositeMultiConverter" />.
        /// </summary>
        /// <param name="multiValueConverter">
        ///     A multiconverter involved in sequential conversion.
        /// </param>
        /// <param name="converters">
        ///     Converters involved in sequential conversion.
        /// </param>
        public CompositeMultiConverter(IMultiValueConverter multiValueConverter, params IValueConverter[] converters)
        {
            _multiValueConverter = Guard.EnsureArgumentIsNotNull(multiValueConverter);
            _compositeConverter = new CompositeConverter(Guard.EnsureArgumentIsNotNull(converters));
        }

        /// <summary>
        ///     Converts <paramref name="values" />, sequentially transmitting the conversion result between converters.
        /// </summary>
        /// <remarks>
        ///     The multiconverter is called first. Then the result is sequentially transmitted between the converters.
        /// </remarks>
        /// <param name="values">
        ///     Converted values.
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
        public object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            var convertedValue = _multiValueConverter.Convert(values, targetType, parameter, culture);

            return _compositeConverter.Convert(convertedValue, targetType, parameter, culture);
        }

        /// <summary>
        ///     Performs reverse conversion <paramref name="value" />, sequentially transmitting the conversion result between converters.
        /// </summary>
        /// <remarks>
        ///     The converters are called first sequentially and in reverse order. Then the result is transmitted to the multiconverter.
        /// </remarks>
        /// <param name="value">
        ///     The converted value.
        /// </param>
        /// <param name="targetTypes">
        ///     It is transmitted to the multiconverter involved in the conversion. <see cref="object" /> is passed to the converters.
        /// </param>
        /// <param name="parameter">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <param name="culture">
        ///     It is transmitted to the converters involved in the conversion.
        /// </param>
        /// <returns>
        ///     The result of the reverse conversion of the multiconverter.
        /// </returns>
        public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        {
            var convertedValue = _compositeConverter.Convert(value, typeof(object), parameter, culture);

            return _multiValueConverter.ConvertBack(convertedValue, targetTypes, parameter, culture);
        }

        private readonly IMultiValueConverter _multiValueConverter;
        private readonly CompositeConverter _compositeConverter;
    }
}
