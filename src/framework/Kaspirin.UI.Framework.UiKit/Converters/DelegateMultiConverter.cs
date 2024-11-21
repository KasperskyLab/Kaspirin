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
    ///     Provides an implementation of <see cref="IMultiValueConverter" />, which uses arbitrary logic to convert values.
    /// </summary>
    public sealed class DelegateMultiConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Creates an object <see cref="DelegateMultiConverter" />.
        /// </summary>
        /// <param name="convert">
        ///     The delegate for the <see cref="Convert" /> method.
        /// </param>
        /// <param name="convertBack">
        ///     The delegate for the <see cref="ConvertBack" /> method.
        /// </param>
        public DelegateMultiConverter(Func<object?[], object?> convert, Func<object?, object?[]>? convertBack = null)
        {
            _convert = Guard.EnsureArgumentIsNotNull(convert);
            _convertBack = convertBack;
        }

        /// <summary>
        ///     Converts <paramref name="values" /> using the delegate provided in the constructor.
        /// </summary>
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
        ///     The result of the delegate call.
        /// </returns>
        public object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
            => _convert.Invoke(values);

        /// <summary>
        ///     Performs the reverse conversion of <paramref name="value" /> using the delegate provided in the constructor.
        /// </summary>
        /// <param name="value">
        ///     The converted value.
        /// </param>
        /// <param name="targetTypes">
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
        public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
            => _convertBack?.Invoke(value);

        private readonly Func<object?[], object?> _convert;
        private readonly Func<object?, object?[]>? _convertBack;
    }
}
