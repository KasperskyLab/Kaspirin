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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public class ContextMenu : System.Windows.Controls.ContextMenu
    {
        #region OnCloseCommand

        public ICommand OnCloseCommand
        {
            get { return (ICommand)GetValue(OnCloseCommandProperty); }
            set { SetValue(OnCloseCommandProperty, value); }
        }

        public static readonly DependencyProperty OnCloseCommandProperty =
            DependencyProperty.Register("OnCloseCommand", typeof(ICommand), typeof(ContextMenu));

        #endregion

        #region OnOpenCommand

        public ICommand OnOpenCommand
        {
            get { return (ICommand)GetValue(OnOpenCommandProperty); }
            set { SetValue(OnOpenCommandProperty, value); }
        }

        public static readonly DependencyProperty OnOpenCommandProperty =
            DependencyProperty.Register("OnOpenCommand", typeof(ICommand), typeof(ContextMenu));

        #endregion

        #region ItemElement

        public DataTemplate ItemElement
        {
            get { return (DataTemplate)GetValue(ItemElementProperty); }
            set { SetValue(ItemElementProperty, value); }
        }

        public static readonly DependencyProperty ItemElementProperty =
            UIKitItemsControlHelper.ItemElementProperty.AddOwner(typeof(ContextMenu));

        #endregion

        #region ItemElementSelector

        public DataTemplateSelector ItemElementSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemElementSelectorProperty); }
            set { SetValue(ItemElementSelectorProperty, value); }
        }
        public static readonly DependencyProperty ItemElementSelectorProperty =
            UIKitItemsControlHelper.ItemElementSelectorProperty.AddOwner(typeof(ContextMenu));

        #endregion

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            _processingItem = item;

            return UIKitItemsControlHelper.IsItemContainer<MenuItemBase>(item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var processingItem = _processingItem;
            _processingItem = null;

            return UIKitItemsControlHelper.CreateItemContainer<MenuItemBase>(this, processingItem);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (UIKitItemsControlHelper.IsSimpleItemContainer(element))
            {
                base.PrepareContainerForItemOverride(element, item);
            }
        }

        protected override void OnOpened(RoutedEventArgs e)
        {
            base.OnOpened(e);

            var currentWindow = this.GetWindow();
            if (currentWindow is not null)
            {
                currentWindow.SizeChanged += InvalidateContextMenuPosition;
                currentWindow.LocationChanged += InvalidateContextMenuPosition;
            }

            var command = OnOpenCommand;
            if (command is not null && command.CanExecute(default))
            {
                command.Execute(default);
            }
        }

        protected override void OnClosed(RoutedEventArgs e)
        {
            base.OnClosed(e);

            var currentWindow = this.GetWindow();
            if (currentWindow is not null)
            {
                currentWindow.SizeChanged -= InvalidateContextMenuPosition;
                currentWindow.LocationChanged -= InvalidateContextMenuPosition;
            }

            var command = OnCloseCommand;
            if (command is not null && command.CanExecute(default))
            {
                command.Execute(default);
            }
        }

        private void InvalidateContextMenuPosition(object? sender, EventArgs eventArgs)
        {
            UpdateContextMenuPosition();
        }

        private void UpdateContextMenuPosition()
        {
            //need to refresh contextmenu position
            var offset = HorizontalOffset;
            HorizontalOffset = offset + 1;
            HorizontalOffset = offset;
        }

        private object? _processingItem;
    }
}
