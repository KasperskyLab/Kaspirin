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
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Converters.EnumerableConverters;

public sealed class EnumerableToStringJoinConverter : ValueConverterMarkupExtension<EnumerableToStringJoinConverter>
{
    public LocExtension Separator { get; set; } = new("EnumerableToStringJoinConverter_Separator", UIKitConstants.LocalizationScope);

    public LocExtension? EmptyCollection { get; set; }

    public ResourceConverterExtension? ItemConverter { get; set; }

    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return DependencyProperty.UnsetValue;
        }

        var enumerable = Guard.EnsureArgumentIsInstanceOfType<IEnumerable>(value)
            .Cast<object?>()
            .ToArray()
            .AsEnumerable();

        if (enumerable.None() && EmptyCollection is not null)
        {
            return EmptyCollection.ProvideConstantStringValue();
        }

        if (ItemConverter is not null)
        {
            enumerable = enumerable.Select(e =>
            {
                var convertedValue = ItemConverter.GetResource(e);
                var locExtension = convertedValue as LocExtension;
                return locExtension?.ProvideConstantStringValue() ?? convertedValue;
            });
        }

        return string.Join(Separator.ProvideConstantStringValue(), enumerable);
    }
}
