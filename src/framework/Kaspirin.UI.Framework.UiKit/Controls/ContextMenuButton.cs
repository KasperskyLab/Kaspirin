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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using WpfContextMenu = System.Windows.Controls.ContextMenu;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_ToggleButton, Type = typeof(ToggleButton))]
    public class ContextMenuButton : ContentControl
    {
        public const string PART_ToggleButton = "PART_ToggleButton";

        static ContextMenuButton()
        {
            ContextMenuProperty.OverrideMetadata(
                typeof(ContextMenuButton),
                new FrameworkPropertyMetadata(null, OnContextMenuChanged));
        }

        public ContextMenuButton()
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

        public override void OnApplyTemplate()
        {
            _toggleButton = GetTemplateChild(PART_ToggleButton) as ToggleButton;
        }

        #region Icon

        public UIKitIcon_16 Icon
        {
            get { return (UIKitIcon_16)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(UIKitIcon_16), typeof(ContextMenuButton));

        #endregion

        #region IconMenuOpened

        public UIKitIcon_16 IconMenuOpened
        {
            get { return (UIKitIcon_16)GetValue(IconMenuOpenedProperty); }
            set { SetValue(IconMenuOpenedProperty, value); }
        }

        public static readonly DependencyProperty IconMenuOpenedProperty = DependencyProperty.Register(
            "IconMenuOpened", typeof(UIKitIcon_16), typeof(ContextMenuButton));

        #endregion

        #region IconMenuClosed

        public UIKitIcon_16 IconMenuClosed
        {
            get { return (UIKitIcon_16)GetValue(IconMenuClosedProperty); }
            set { SetValue(IconMenuClosedProperty, value); }
        }

        public static readonly DependencyProperty IconMenuClosedProperty = DependencyProperty.Register(
            "IconMenuClosed", typeof(UIKitIcon_16), typeof(ContextMenuButton));

        #endregion

        private void AttachContextMenu(WpfContextMenu? contextMenu)
        {
            if (contextMenu is not null)
            {
                ApplyTemplate();

                if (_toggleButton is not null)
                {
                    contextMenu.PlacementTarget = _toggleButton;

                    contextMenu.SetBinding(WpfContextMenu.IsOpenProperty, new Binding()
                    {
                        Source = _toggleButton,
                        Path = ToggleButton.IsCheckedProperty.AsPath(),
                        Mode = BindingMode.TwoWay
                    });
                }

                contextMenu.Opened += OnContextMenuOpened;
                contextMenu.Closed += OnContextMenuClosed;
            }
        }

        private void DetachContextMenu(WpfContextMenu? contextMenu)
        {
            if (contextMenu is not null)
            {
                ApplyTemplate();

                if (_toggleButton is not null)
                {
                    contextMenu.ClearValue(WpfContextMenu.PlacementTargetProperty);
                    contextMenu.ClearValue(WpfContextMenu.IsOpenProperty);
                }

                contextMenu.Opened -= OnContextMenuOpened;
                contextMenu.Closed -= OnContextMenuClosed;
            }
        }

        private void OnContextMenuOpened(object sender, RoutedEventArgs e)
            => SetToggleHitTestVisible(false);

        private void OnContextMenuClosed(object sender, RoutedEventArgs e)
            => SetToggleHitTestVisible(true);

        private static void OnContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contextMenuButton = (ContextMenuButton)d;

            contextMenuButton.DetachContextMenu((ContextMenu?)e.OldValue);
            contextMenuButton.AttachContextMenu((ContextMenu?)e.NewValue);

            contextMenuButton.SetToggleHitTestVisible(true);
        }

        private void CloseContextMenu()
        {
            if (ContextMenu is not null)
            {
                SetToggleHitTestVisible(true);

                ContextMenu.IsOpen = false;
                ContextMenu.Opened -= OnContextMenuOpened;
                ContextMenu.Closed -= OnContextMenuClosed;
            }
        }

        private void SetToggleHitTestVisible(bool value)
        {
            if (_toggleButton is not null)
            {
                _toggleButton.IsHitTestVisible = value;
            }
        }

        private ToggleButton? _toggleButton;
    }
}
