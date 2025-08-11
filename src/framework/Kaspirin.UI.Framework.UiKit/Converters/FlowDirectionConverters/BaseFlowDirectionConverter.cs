// Copyright © 2025 AO Kaspersky Lab.
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
using System.Windows;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.FlowDirectionConverters;

/// <summary>
///     Provides a basic implementation <see cref="IValueConverter" /> in which the converted value
///     can take the values <see cref="FlowDirection.LeftToRight" /> or <see cref="FlowDirection.RightToLeft" />,
///     and the result is determined by the properties <see cref="LeftToRight" /> and <see cref="RightToLeft" />.
/// </summary>
/// <typeparam name="T">
///     The type of conversion result.
/// </typeparam>
public abstract class BaseFlowDirectionConverter<T> : ValueConverterMarkupExtension<BaseFlowDirectionConverter<T>>
{
    /// <summary>
    ///     Creates an object <see cref="BaseFlowDirectionConverter{T}" />.
    /// </summary>
    /// <param name="ltrValue">
    ///     The default result when converting the value is <see cref="FlowDirection.LeftToRight" />.
    /// </param>
    /// <param name="rtlValue">
    ///     The default result when converting the value is <see cref="FlowDirection.RightToLeft" />.
    /// </param>
    protected BaseFlowDirectionConverter(T? ltrValue, T? rtlValue)
    {
        LeftToRight = ltrValue;
        RightToLeft = rtlValue;
    }

    /// <summary>
    ///     The result of the value conversion <see cref="FlowDirection.LeftToRight" />.
    /// </summary>
    public T? LeftToRight { get; set; }

    /// <summary>
    ///     The result of converting the value <see cref="FlowDirection.RightToLeft" />.
    /// </summary>
    public T? RightToLeft { get; set; }

    /// <summary>
    ///     Converts <paramref name="value" /> to property values <see cref="LeftToRight" /> or <see cref="RightToLeft" />.
    /// </summary>
    /// <remarks>
    ///     The <paramref name="value" /> value must be of type <see cref="FlowDirection" />.
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
    ///     Returns <see cref="LeftToRight" /> if <paramref name="value" /> is <see cref="FlowDirection.LeftToRight" />,
    ///     otherwise <see cref="RightToLeft" />.
    /// </returns>
    public sealed override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Guard.EnsureIsInstanceOfType<FlowDirection>(value) == FlowDirection.LeftToRight
        ? LeftToRight
        : RightToLeft;

    /// <summary>
    ///     Performs a reverse conversion of <paramref name="value" /> to <see cref="FlowDirection.LeftToRight" />
    ///     or <see cref="FlowDirection.RightToLeft" />.
    /// </summary>
    /// <remarks>
    ///     For comparison, <paramref name="value" /> is used <see cref="EqualityComparer{T}.Default" />.
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
    ///     Returns <see cref="FlowDirection.LeftToRight" /> if <paramref name="value" /> is <see cref="LeftToRight" />,
    ///     or <see cref="FlowDirection.RightToLeft" /> with <paramref name="value" /> equal to <see cref="RightToLeft" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     It is thrown if <paramref name="value" /> is not equal to <see cref="RightToLeft" /> and <see cref="LeftToRight" />.
    /// </exception>
    public sealed override object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isLTRValue = value is T && EqualityComparer<T>.Default.Equals((T)value, LeftToRight!);
        if (isLTRValue)
        {
            return FlowDirection.LeftToRight;
        }

        var isRTLValue = value is T && EqualityComparer<T>.Default.Equals((T)value, RightToLeft!);
        if (isRTLValue)
        {
            return FlowDirection.RightToLeft;
        }

        throw new InvalidOperationException($"Failed to convert back value {value}.");
    }
}
