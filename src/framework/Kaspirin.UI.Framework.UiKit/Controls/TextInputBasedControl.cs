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
using System.Windows.Controls;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_TextInput, Type = typeof(TextInput))]
    public abstract class TextInputBasedControl : Control
    {
        public const string PART_TextInput = "PART_TextInput";

        #region Caption

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(TextInputBasedControl));

        #endregion

        #region FontMode

        public InputFontMode FontMode
        {
            get { return (InputFontMode)GetValue(FontModeProperty); }
            set { SetValue(FontModeProperty, value); }
        }

        public static readonly DependencyProperty FontModeProperty =
            DependencyProperty.Register("FontMode", typeof(InputFontMode), typeof(TextInputBasedControl),
                new PropertyMetadata(InputFontMode.Regular));

        #endregion

        #region InputHorizontalAlignment

        public HorizontalAlignment InputHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(InputHorizontalAlignmentProperty); }
            set { SetValue(InputHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty InputHorizontalAlignmentProperty =
            DependencyProperty.Register("InputHorizontalAlignment", typeof(HorizontalAlignment), typeof(TextInputBasedControl),
                new PropertyMetadata(HorizontalAlignment.Left));

        #endregion

        #region InputWidth

        public double InputWidth
        {
            get { return (double)GetValue(InputWidthProperty); }
            set { SetValue(InputWidthProperty, value); }
        }

        public static readonly DependencyProperty InputWidthProperty =
            DependencyProperty.Register("InputWidth", typeof(double), typeof(TextInputBasedControl),
                new PropertyMetadata(double.NaN));

        #endregion

        #region InvalidStatePopupContent

        public string InvalidStatePopupContent
        {
            get { return (string)GetValue(InvalidStatePopupContentProperty); }
            set { SetValue(InvalidStatePopupContentProperty, value); }
        }

        public static readonly DependencyProperty InvalidStatePopupContentProperty =
            DependencyProperty.Register("InvalidStatePopupContent", typeof(string), typeof(TextInputBasedControl));

        #endregion

        #region InvalidStatePopupPosition

        public PopupPosition InvalidStatePopupPosition
        {
            get { return (PopupPosition)GetValue(InvalidStatePopupPositionProperty); }
            set { SetValue(InvalidStatePopupPositionProperty, value); }
        }

        public static readonly DependencyProperty InvalidStatePopupPositionProperty =
            DependencyProperty.Register("InvalidStatePopupPosition", typeof(PopupPosition), typeof(TextInputBasedControl),
                new PropertyMetadata(PopupPosition.Right));

        #endregion

        #region IsInvalidState

        public bool IsInvalidState
        {
            get { return (bool)GetValue(IsInvalidStateProperty); }
            set { SetValue(IsInvalidStateProperty, value); }
        }

        public static readonly DependencyProperty IsInvalidStateProperty =
            DependencyProperty.Register("IsInvalidState", typeof(bool), typeof(TextInputBasedControl));

        #endregion

        #region IsReadOnly

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(TextInputBasedControl));

        #endregion

        #region Label

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(TextInputBasedControl));

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textInput = (TextInput)GetTemplateChild(PART_TextInput);
            _textInput.SetBinding(Controls.TextInput.CaptionProperty, new Binding() { Source = this, Path = CaptionProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.FontModeProperty, new Binding() { Source = this, Path = FontModeProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.InputHorizontalAlignmentProperty, new Binding() { Source = this, Path = InputHorizontalAlignmentProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.InputWidthProperty, new Binding() { Source = this, Path = InputWidthProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.InvalidStatePopupContentProperty, new Binding() { Source = this, Path = InvalidStatePopupContentProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.InvalidStatePopupPositionProperty, new Binding() { Source = this, Path = InvalidStatePopupPositionProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.IsInvalidStateProperty, new Binding() { Source = this, Path = IsInvalidStateProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.IsReadOnlyProperty, new Binding() { Source = this, Path = IsReadOnlyProperty.AsPath() });
            _textInput.SetBinding(Controls.TextInput.LabelProperty, new Binding() { Source = this, Path = LabelProperty.AsPath() });
        }

        private TextInput? _textInput;
    }
}
