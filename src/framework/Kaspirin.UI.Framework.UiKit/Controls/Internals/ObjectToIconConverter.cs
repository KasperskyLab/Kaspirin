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

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class ObjectToIconConverter : ValueConverterMarkupExtension<ObjectToIconConverter>
    {
        public object? FallbackValue { get; set; }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (targetType.BaseType != typeof(Enum) && value is not Enum)
            {
                return value;
            }

            if (targetType.BaseType == typeof(ValueType) && targetType.IsGenericType && targetType.GetGenericArguments().First().BaseType == typeof(Enum))
            {
                return value;
            }

            if (targetType.BaseType == typeof(Enum) && value is Enum)
            {
                return value;
            }

            return FallbackValue;
        }
    }
}
