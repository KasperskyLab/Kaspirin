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

namespace Kaspirin.UI.Framework.UiKit
{
    internal static class UIKitItemsControlHelper
    {
        #region IsGeneratedItemContainer

        public static readonly DependencyProperty IsGeneratedItemContainerProperty =
            DependencyProperty.RegisterAttached("IsGeneratedItemContainer", typeof(bool), typeof(UIKitItemsControlHelper));

        #endregion

        #region ItemElement

        public static readonly DependencyProperty ItemElementProperty =
            DependencyProperty.Register("ItemElement", typeof(DataTemplate), typeof(UIKitItemsControlHelper));

        #endregion

        #region ItemElementSelector

        public static readonly DependencyProperty ItemElementSelectorProperty =
            DependencyProperty.Register("ItemElementSelector", typeof(DataTemplateSelector), typeof(UIKitItemsControlHelper));

        #endregion

        public static bool IsItemContainer<TControl>(object item) where TControl : Control
        {
            return item is TControl;
        }

        public static bool IsSimpleItemContainer(DependencyObject item)
        {
            Guard.ArgumentIsNotNull(item);

            return item.GetValue(IsGeneratedItemContainerProperty) is false;
        }

        public static TControl CreateItemContainer<TControl>(ItemsControl itemsControl, object? item) where TControl : Control, new()
        {
            Guard.ArgumentIsNotNull(itemsControl);

            if (itemsControl.GetValue(ItemElementProperty) is DataTemplate dataTemplate)
            {
                if (dataTemplate.LoadContent() is TControl itemContainer)
                {
                    itemContainer.SetValue(IsGeneratedItemContainerProperty, true);

                    return itemContainer;
                }
                else
                {
                    throw new Exception($"{nameof(ItemElementProperty)} must contain DataTemplate with {typeof(TControl).Name} in root");
                }
            }

            if (itemsControl.GetValue(ItemElementSelectorProperty) is DataTemplateSelector dataTemplateSelector && item != null)
            {
                if (dataTemplateSelector.SelectTemplate(item, null)?.LoadContent() is TControl itemContainer)
                {
                    itemContainer.SetValue(IsGeneratedItemContainerProperty, true);

                    return itemContainer;
                }
                else
                {
                    throw new Exception($"{nameof(ItemElementSelectorProperty)} must provide DataTemplates with {typeof(TControl).Name} in root");
                }
            }

            return new TControl();
        }
    }
}
