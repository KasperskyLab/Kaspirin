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
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions.DataBinding
{
    public sealed class ConditionalBindingExtension : MarkupExtension
    {
        public BindingBase? Condition { get; set; }

        public BindingBase? True { get; set; }

        public BindingBase? False { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Condition is null || True is null || False is null)
            {
                return DependencyProperty.UnsetValue;
            }

            var multibinding = new MultiBinding
            {
                Converter = new ConditionalBindingConverter()
            };

            multibinding.Bindings.Add(Condition);
            multibinding.Bindings.Add(True);
            multibinding.Bindings.Add(False);

            return multibinding.ProvideValue(serviceProvider);
        }

        private sealed class ConditionalBindingConverter : MultiValueConverterMarkupExtension<ConditionalBindingConverter>
        {
            public override object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
            {
                if (values?.Length != 3 || values[0] is not bool condition)
                {
                    return DependencyProperty.UnsetValue;
                }

                var trueBinding = values[1];
                var falseBinding = values[2];

                return condition
                    ? trueBinding
                    : falseBinding;
            }
        }
    }
}
