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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class SelectItem : SelectorItem
    {
        public SelectItem()
        {
            SetBinding(HeaderProperty, new Binding()
            {
                Source = this,
                Path = DataContextProperty.AsPath(),
            });
        }

        #region Icon

        public UIKitIcon_16 Icon
        {
            get => (UIKitIcon_16)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(UIKitIcon_16),
            typeof(SelectItem),
            new PropertyMetadata(default(UIKitIcon_16)));

        #endregion

        #region Image

        public ImageSource? Image
        {
            get => (ImageSource?)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            nameof(Image),
            typeof(ImageSource),
            typeof(SelectItem),
            new PropertyMetadata(default(ImageSource?)));

        #endregion

        #region ImageFlowDirection

        public FlowDirection ImageFlowDirection
        {
            get => (FlowDirection)GetValue(ImageFlowDirectionProperty);
            set => SetValue(ImageFlowDirectionProperty, value);
        }

        public static readonly DependencyProperty ImageFlowDirectionProperty = DependencyProperty.Register(
            nameof(ImageFlowDirection),
            typeof(FlowDirection),
            typeof(SelectItem),
            new PropertyMetadata(default(FlowDirection)));

        #endregion

        #region Header

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SelectItem),
            new PropertyMetadata(string.Empty));

        #endregion

        #region IsPressed

        public bool IsPressed
        {
            get => (bool)GetValue(_isPressedProperty);
            private set => SetValue(_isPressedProperty, value);
        }

        internal static readonly DependencyProperty _isPressedProperty = DependencyProperty.Register(
            nameof(IsPressed),
            typeof(bool),
            typeof(SelectItem),
            new PropertyMetadata(default(bool)));

        #endregion

        #region IsHighlighted

        public bool IsHighlighted
        {
            get => (bool)GetValue(_isHighlightedProperty);
            internal set => SetValue(_isHighlightedProperty, value);
        }

        internal static readonly DependencyProperty _isHighlightedProperty = DependencyProperty.Register(
            nameof(IsHighlighted),
            typeof(bool),
            typeof(SelectItem),
            new PropertyMetadata(default(bool)));

        #endregion

        #region HasRightBar

        public bool HasRightBar
        {
            get => (bool)GetValue(_hasRightBarPropertyKey.DependencyProperty);
            private set => SetValue(_hasRightBarPropertyKey, value);
        }

        private static readonly DependencyPropertyKey _hasRightBarPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(HasRightBar),
            typeof(bool),
            typeof(SelectItem),
            new PropertyMetadata(false));

        public static readonly DependencyProperty HasRightBarProperty = _hasRightBarPropertyKey.DependencyProperty;

        #endregion

        #region RightBar

        public object RightBar
        {
            get => (object)GetValue(RightBarProperty);
            set => SetValue(RightBarProperty, value);
        }

        public static readonly DependencyProperty RightBarProperty = DependencyProperty.Register(
            nameof(RightBar),
            typeof(object),
            typeof(SelectItem),
            new PropertyMetadata(null, OnRightBarChanged));

        private static void OnRightBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SelectItem)d).InvalidateHasRightBar();
        }

        #endregion

        #region RightBarTemplate

        public DataTemplate RightBarTemplate
        {
            get => (DataTemplate)GetValue(RightBarTemplateProperty);
            set => SetValue(RightBarTemplateProperty, value);
        }

        public static readonly DependencyProperty RightBarTemplateProperty = DependencyProperty.Register(
            nameof(RightBarTemplate),
            typeof(DataTemplate),
            typeof(SelectItem),
            new PropertyMetadata(null, OnRightBarTemplateChanged));

        private static void OnRightBarTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SelectItem)d).InvalidateHasRightBar();
        }

        #endregion

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;

            IsPressed = true;

            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;

            IsPressed = false;

            ParentSelect?.NotifySelectItemMouseUp(this);

            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsPressed = false;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            e.Handled = true;

            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                IsPressed = true;
            }

            ParentSelect?.NotifySelectItemMouseEnter(this);

            base.OnMouseEnter(e);
        }

        private void InvalidateHasRightBar()
        {
            HasRightBar = RightBar != null || RightBarTemplate != null;
        }

        private Select? ParentSelect => ItemsControl.ItemsControlFromItemContainer(this) as Select;
    }
}