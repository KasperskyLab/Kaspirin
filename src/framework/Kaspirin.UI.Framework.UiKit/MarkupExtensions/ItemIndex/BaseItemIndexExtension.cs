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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions.ItemIndex
{
    public abstract class BaseItemIndexExtension<T> : ExtendedMarkupExtension
    {
        protected BaseItemIndexExtension(T? trueValue, T? falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T? True { get; set; }

        public T? False { get; set; }

        public MarkupExtension? TrueSource { get; set; }

        public MarkupExtension? FalseSource { get; set; }

        public ItemIndexConditionType? Condition { get; set; }

        public int ConditionParameter { get; set; }

        public MarkupExtension? ConditionParameterSource { get; set; }

        protected override object? ProvideValue(IServiceProvider? serviceProvider, TargetType valueType)
            => CreateItemToIndexBinding(serviceProvider).ProvideValue(serviceProvider);

        private MultiBinding CreateItemToIndexBinding(IServiceProvider? serviceProvider)
        {
            Guard.IsNotNull(Condition);

            return new MultiBinding()
            {
                Bindings =
                {
                    new Binding()
                    {
                        RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self }
                    },
                    new Binding()
                    {
                        RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(ItemsControl) }
                    },
                    ConditionParameterSource?.ExpandTo<Binding>(serviceProvider) ?? new Binding()
                    {
                        Source = ConditionParameter
                    },
                    TrueSource?.ExpandTo<Binding>(serviceProvider) ?? new Binding()
                    {
                        Source = True
                    },
                    FalseSource?.ExpandTo<Binding>(serviceProvider) ?? new Binding()
                    {
                        Source = False
                    },
                    // Binding required to track changes of items count.
                    new Binding()
                    {
                        Path = new PropertyPath($"{nameof(ItemsControl.Items)}.{nameof(ItemCollection.Count)}"),
                        RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(ItemsControl) }
                    },
                },
                Converter = new DelegateMultiConverter(values =>
                {
                    var item = Guard.EnsureArgumentIsInstanceOfType<DependencyObject>(values[0]).GetDataContext();
                    var itemsControl = Guard.EnsureArgumentIsInstanceOfType<ItemsControl>(values[1]);
                    var index = Guard.EnsureArgumentIsInstanceOfType<int>(values[2]);
                    var trueValue = Guard.EnsureArgumentIsInstanceOfType<T?>(values[3]);
                    var falseValue = Guard.EnsureArgumentIsInstanceOfType<T?>(values[4]);

                    var currentIndex = itemsControl.Items.IndexOf(item);
                    if (currentIndex != -1)
                    {
                        return GetResultValue(trueValue, falseValue, Condition, index, currentIndex, 0, itemsControl.Items.Count - 1);
                    }

                    return DependencyProperty.UnsetValue;
                })
            };
        }

        private static T? GetResultValue(T? trueValue, T? falseFalue, ItemIndexConditionType? conditionType, int expectedIndex, int currentIndex, int minIndex, int maxIndex)
        {
            var condition = conditionType switch
            {
                ItemIndexConditionType.First => currentIndex == minIndex,
                ItemIndexConditionType.Last => currentIndex == maxIndex,
                ItemIndexConditionType.Equals => currentIndex == expectedIndex,
                ItemIndexConditionType.MoreThan => currentIndex > expectedIndex,
                ItemIndexConditionType.MoreThanOrEquals => currentIndex >= expectedIndex,
                ItemIndexConditionType.LessThan => currentIndex < expectedIndex,
                ItemIndexConditionType.LessThanOrEquals => currentIndex <= expectedIndex,
                _ => throw new InvalidOperationException($"{conditionType} is not supported"),
            };

            return condition ? trueValue : falseFalue;
        }
    }
}