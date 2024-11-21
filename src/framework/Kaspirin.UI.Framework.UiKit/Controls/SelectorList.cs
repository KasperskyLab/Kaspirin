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
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public abstract class SelectorList<TListItem> : ListBox where TListItem : SelectorItem, new()
    {
        #region ItemElement

        public DataTemplate ItemElement
        {
            get { return (DataTemplate)GetValue(ItemElementProperty); }
            set { SetValue(ItemElementProperty, value); }
        }

        public static readonly DependencyProperty ItemElementProperty =
            UIKitItemsControlHelper.ItemElementProperty.AddOwner(typeof(TListItem));

        #endregion

        #region ItemElementSelector

        public DataTemplateSelector ItemElementSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemElementSelectorProperty); }
            set { SetValue(ItemElementSelectorProperty, value); }
        }

        public static readonly DependencyProperty ItemElementSelectorProperty =
            UIKitItemsControlHelper.ItemElementSelectorProperty.AddOwner(typeof(TListItem));

        #endregion

        public new void ScrollIntoView(object item)
        {
            if (item == null)
            {
                return;
            }

            var container = ItemContainerGenerator.ContainerFromItem(item) as SelectorItem;
            if (container != null)
            {
                var fadeLine = container.FindVisualParent<FadeLine>();
                if (fadeLine != null)
                {
                    fadeLine.ScrollIntoView(container);

                    return;
                }
            }

            base.ScrollIntoView(item);
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            _processingItem = item;

            return UIKitItemsControlHelper.IsItemContainer<TListItem>(item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var processingItem = _processingItem;
            _processingItem = null;

            return UIKitItemsControlHelper.CreateItemContainer<TListItem>(this, processingItem);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            element = UIKitItemsControlHelper.IsSimpleItemContainer(element)
                ? element
                : new TListItem();

            base.PrepareContainerForItemOverride(element, item);
        }

        private object? _processingItem;
    }
}
