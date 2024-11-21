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
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_ScrollViewer, Type = typeof(ScrollViewer))]
    public sealed class VirtualizedItemsControl : ItemsControl
    {
        public const string PART_ScrollViewer = "PART_ScrollViewer";

        #region VirtualizationThreshold

        public int VirtualizationThreshold
        {
            get => (int)GetValue(VirtualizationThresholdProperty);
            set => SetValue(VirtualizationThresholdProperty, value);
        }

        public static readonly DependencyProperty VirtualizationThresholdProperty = DependencyProperty.Register(
            nameof(VirtualizationThreshold),
            typeof(int),
            typeof(VirtualizedItemsControl),
            new PropertyMetadata(default(int)));

        #endregion

        public override void OnApplyTemplate()
        {
            _scrollViewer = Guard.EnsureIsInstanceOfType<ScrollViewer>(GetTemplateChild(PART_ScrollViewer));
            _scrollViewer.SetBinding(ScrollViewer.CanContentScrollProperty, new MultiBinding()
            {
                Bindings =
                {
                    new Binding() { Source = this, Path = VirtualizationThresholdProperty.AsPath() },
                    new Binding() { Source = this, Path = _itemsCountPath }
                },
                Converter = new DelegateMultiConverter(values =>
                {
                    var threshold = Guard.EnsureIsInstanceOfType<int>(values[0]);
                    var itemsCount = Guard.EnsureIsInstanceOfType<int>(values[1]);

                    return itemsCount >= threshold;
                })
            });
        }

        public void ScrollIntoView(object item)
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                BringItemIntoView(item);
            }
            else
            {
                this.WhenLoaded(() => BringItemIntoView(item));
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (constraint.Height == double.PositiveInfinity && !IsLoaded)
            {
                constraint.Height = 0;

                this.WhenLoaded(InvalidateMeasure);
            }

            if (constraint.Height == double.PositiveInfinity && IsLoaded)
            {
                Guard.Fail($"Virtualization error. Unexpected control height constraint: {constraint.Height}");
            }

            return base.MeasureOverride(constraint);
        }

        private void BringItemIntoView(object item)
        {
            _onBringItemIntoViewMethod.Invoke(this, new[] { item });
        }

        private static MethodInfo GetOnBringItemIntoViewMethod()
            => Guard.EnsureIsNotNull(typeof(ItemsControl).GetMethod(
                name: "OnBringItemIntoView",
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: Type.DefaultBinder,
                types: new Type[] { typeof(object) },
                modifiers: null));

        private static readonly MethodInfo _onBringItemIntoViewMethod = GetOnBringItemIntoViewMethod();

        private static readonly PropertyPath _itemsCountPath = new("Items.Count");

        private ScrollViewer? _scrollViewer;
    }
}
