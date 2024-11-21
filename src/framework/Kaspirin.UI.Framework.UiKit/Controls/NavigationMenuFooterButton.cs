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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System.Windows;
using System.Windows.Data;
using WpfContextMenu = System.Windows.Controls.ContextMenu;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class NavigationMenuFooterButton : NavigationMenuButtonBase
    {
        static NavigationMenuFooterButton()
        {
            ContextMenuProperty.OverrideMetadata(
                typeof(NavigationMenuFooterButton),
                new FrameworkPropertyMetadata(null, OnContextMenuChanged));
        }

        public NavigationMenuFooterButton()
        {
            ContextMenuOpening += (s, e) =>
            {
                // Mark event as handled to suppress opening context menu on right mouse button click.
                e.Handled = true;
            };

            Loaded += (sender, args) =>
            {
                DetachContextMenu(ContextMenu);
                AttachContextMenu(ContextMenu);
            };

            Unloaded += (sender, args) => CloseContextMenu();
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (ContextMenu is not null)
            {
                ContextMenu.IsOpen = true;
            }
        }

        private void AttachContextMenu(WpfContextMenu? contextMenu)
        {
            if (contextMenu is not null)
            {
                contextMenu.PlacementTarget = this;

                SetBinding(IsSelectedProperty, new Binding()
                {
                    Source = contextMenu,
                    Path = WpfContextMenu.IsOpenProperty.AsPath()
                });

                contextMenu.Opened += OnContextMenuOpened;
                contextMenu.Closed += OnContextMenuClosed;
            }
        }

        private void DetachContextMenu(WpfContextMenu? contextMenu)
        {
            if (contextMenu is not null)
            {
                contextMenu.ClearValue(WpfContextMenu.PlacementTargetProperty);
                contextMenu.ClearValue(WpfContextMenu.IsOpenProperty);

                contextMenu.Opened -= OnContextMenuOpened;
                contextMenu.Closed -= OnContextMenuClosed;
            }
        }

        private void OnContextMenuOpened(object sender, RoutedEventArgs e)
            => SetHitTestVisible(false);

        private void OnContextMenuClosed(object sender, RoutedEventArgs e)
            => SetHitTestVisible(true);

        private static void OnContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var navigationMenuFooterButton = (NavigationMenuFooterButton)d;

            navigationMenuFooterButton.DetachContextMenu((ContextMenu?)e.OldValue);
            navigationMenuFooterButton.AttachContextMenu((ContextMenu?)e.NewValue);

            navigationMenuFooterButton.SetHitTestVisible(true);
        }

        private void CloseContextMenu()
        {
            if (ContextMenu is not null)
            {
                SetHitTestVisible(true);

                ContextMenu.IsOpen = false;
                ContextMenu.Opened -= OnContextMenuOpened;
                ContextMenu.Closed -= OnContextMenuClosed;
            }
        }

        private void SetHitTestVisible(bool value)
            => IsHitTestVisible = value;
    }
}
