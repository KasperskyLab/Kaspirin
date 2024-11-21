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
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Converters.BaseMarkupExtensions
{
    /// <summary>
    ///     Provides a basic implementation of <see cref="IMultiValueConverter" /> that can be used in
    ///     XAML files as a markup extension.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the class with the conversion logic.
    /// </typeparam>
    public abstract class MultiValueConverterMarkupExtension<T> : MarkupExtension, IMultiValueConverter
        where T : MultiValueConverterMarkupExtension<T>
    {
        /// <inheritdoc cref = "MarkupExtension.ProvideValue(IServiceProvider)" />
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
            => Guard.EnsureIsInstanceOfType<T>(this);

        /// <inheritdoc cref = "IMultiValueConverter.Convert(object[], Type, object, CultureInfo)" />
        public abstract object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture);

        /// <inheritdoc cref = "IMultiValueConverter.ConvertBack(object, Type[], object, CultureInfo)" />
        public virtual object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}