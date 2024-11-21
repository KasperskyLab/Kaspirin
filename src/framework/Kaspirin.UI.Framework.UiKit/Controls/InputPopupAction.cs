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
using System.Windows.Data;
using System.Windows.Markup;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_Popup, Type = typeof(Popup))]
    [ContentProperty(nameof(PopupContent))]
    public sealed class InputPopupAction : InputCommandAction
    {
        public const string PART_Popup = "PART_Popup";

        #region PopupContent

        public object PopupContent
        {
            get => GetValue(PopupContentProperty);
            set => SetValue(PopupContentProperty, value);
        }

        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
            nameof(PopupContent),
            typeof(object),
            typeof(InputPopupAction));

        #endregion

        #region PopupContentTemplate

        public DataTemplate? PopupContentTemplate
        {
            get => (DataTemplate?)GetValue(PopupContentProperty);
            set => SetValue(PopupContentProperty, value);
        }

        public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
            nameof(PopupContentTemplate),
            typeof(DataTemplate),
            typeof(InputPopupAction));

        #endregion

        #region PopupPosition

        public PopupPosition PopupPosition
        {
            get => (PopupPosition)GetValue(PopupPositionProperty);
            set => SetValue(PopupPositionProperty, value);
        }

        public static readonly DependencyProperty PopupPositionProperty = DependencyProperty.Register(
            nameof(PopupPosition),
            typeof(PopupPosition),
            typeof(InputPopupAction),
            new PropertyMetadata(PopupPosition.Bottom));

        #endregion

        #region IsPopupOpen

        public bool IsPopupOpen
        {
            get => (bool)GetValue(IsPopupOpenProperty);
            set => SetValue(IsPopupOpenProperty, value);
        }

        public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register(
            nameof(IsPopupOpen),
            typeof(bool),
            typeof(InputPopupAction),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _popup = (Popup)GetTemplateChild(PART_Popup);

            _popup.SetBinding(Popup.IsPopupOpenProperty, new Binding() { Source = this, Path = IsPopupOpenProperty.AsPath() });

            _popup.Opened += OnPopupOpened;
            _popup.Closed += OnPopupClosed;
        }

        protected override void OnClick()
        {
            base.OnClick();

            IsPopupOpen = !IsPopupOpen;
        }

        private void OnPopupOpened(object? sender, EventArgs e)
            => IsHitTestVisible = false;

        private void OnPopupClosed(object? sender, EventArgs e)
            => IsHitTestVisible = true;

        private Popup? _popup;
    }
}
