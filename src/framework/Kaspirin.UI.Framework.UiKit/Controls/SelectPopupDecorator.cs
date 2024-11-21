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
using System.Windows.Input;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using WpfPopup = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class SelectPopupDecorator : PopupDecorator
    {
        static SelectPopupDecorator()
        {
            MinHeightProperty.OverrideMetadata(typeof(SelectPopupDecorator), new FrameworkPropertyMetadata(UIKitConstants.SelectPopupDecoratorMinHeight));
        }

        public SelectPopupDecorator()
        {
            Loaded += OnLoaded;
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            var isPopupSource = e.Source == this;
            if (isPopupSource)
            {
                e.Handled = true;
            }

            base.OnPreviewMouseWheel(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            var isPopupSource = e.Source == this;
            if (isPopupSource)
            {
                e.Handled = true;
            }

            base.OnPreviewMouseDown(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var rootPopup = this.FindLogicalParent<WpfPopup>();

            rootPopup?.WhenOpened(() =>
            {
                SetPopupMinWidth(rootPopup);
                SetPopupMaxHeight(rootPopup);
            });
        }

        private void SetPopupMinWidth(WpfPopup popup)
        {
            popup.SetBinding(WpfPopup.MinWidthProperty, new Binding()
            {
                Source = popup.PlacementTarget,
                Path = Control.ActualWidthProperty.AsPath(),
                Converter = new DelegateConverter<double>(value => ShadowOffset * 2 + value)
            });
        }

        private void SetPopupMaxHeight(WpfPopup popup)
        {
            popup.SetBinding(WpfPopup.MaxHeightProperty, new Binding()
            {
                Source = this,
                Path = Control.MaxHeightProperty.AsPath(),
                Converter = new DelegateConverter<double>(value => CoercePopupMaxHeight(value))
            });
        }

        private double CoercePopupMaxHeight(double popupMaxHeight)
        {
            var popupItemHeight = this.FindVisualChild<SelectItem>()?.ActualHeight ?? 0;
            if (popupItemHeight <= 0)
            {
                return popupMaxHeight;
            }

            var offsetCorrection = Padding.Top + Padding.Bottom + ShadowOffset * 2;

            var scrollableHeight = popupMaxHeight - offsetCorrection;

            var popupItemsCount = Math.Max(Math.Truncate(scrollableHeight / popupItemHeight), 1);

            var coercedMaxHeight = popupItemsCount * popupItemHeight + offsetCorrection;

            return coercedMaxHeight;
        }
    }
}
