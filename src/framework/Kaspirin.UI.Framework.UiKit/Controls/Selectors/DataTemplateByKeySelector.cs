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

using System.Windows;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls.Selectors
{
    public sealed class DataTemplateByKeySelector : DataTemplateSelectorBase
    {
        public Binding Source { get; set; } = new Binding();

        protected override object? GetDataTemplateKey(object item)
        {
            Guard.IsNotNull(Source);

            return new ValueProvider(item, Source).GetValue();
        }

        private sealed class ValueProvider : FrameworkElement
        {
            public ValueProvider(object dataContext, Binding binding)
            {
                binding = binding.Clone();
                binding.Source ??= dataContext;

                SetBinding(_valueProperty, binding);
            }

            public object? GetValue()
            {
                return GetValue(_valueProperty);
            }

            private static readonly DependencyProperty _valueProperty =
                DependencyProperty.Register("Value", typeof(object), typeof(ValueProvider));
        }
    }
}