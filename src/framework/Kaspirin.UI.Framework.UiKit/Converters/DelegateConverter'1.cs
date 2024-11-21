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
    ///     Provides an implementation of <see cref="IValueConverter" />, which uses arbitrary logic to
    ///     convert values of the type <typeparamref name="TValue" />.
    /// </summary>
    /// <typeparam name="TValue">
    ///     The type of values to convert.
    /// </typeparam>
    public sealed class DelegateConverter<TValue> : IValueConverter
    {
        /// <summary>
        ///     Creates an object <see cref="DelegateConverter{TValue}" />.
        /// </summary>
        /// <param name="convert">
        ///     The delegate for the <see cref="Convert" /> method.
        /// </param>
        /// <param name="convertBack">
        ///     The delegate for the <see cref="ConvertBack" /> method.
        /// </param>
        public DelegateConverter(Func<TValue?, object?> convert, Func<object?, TValue?>? convertBack = null)
        {
            _convert = Guard.EnsureArgumentIsNotNull(convert);
            _convertBack = convertBack;
        }

        /// <summary>
        ///     Converts <paramref name="value" /> using the delegate provided in the constructor.
        /// </summary>
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
        ///     The result of the delegate call.
        /// </returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) 
            => _convert.Invoke(value != null ? (TValue)value : default);

        /// <summary>
        ///     Performs the reverse conversion of <paramref name="value" /> using the delegate provided in the constructor.
        /// </summary>
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
        ///     The result of calling the delegate or <see langword="null" /> if the delegate for reverse conversion is not specified.
        /// </returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (_convertBack == null)
            {
                return null;
            }

            return _convertBack.Invoke(value);
        }

        private readonly Func<TValue?, object?> _convert;
        private readonly Func<object?, TValue?>? _convertBack;
    }
}
