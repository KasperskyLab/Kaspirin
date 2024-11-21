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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public class CodeInput : TextInputBase
    {
        static CodeInput()
        {
            FlowBehaviorProperty.OverrideMetadata(
                typeof(CodeInput),
                new PropertyMetadata(TextInputFlowBehaviorType.RtlSimulation));
        }

        #region FontMode

        public InputFontMode FontMode
        {
            get { return (InputFontMode)GetValue(FontModeProperty); }
            set { SetValue(FontModeProperty, value); }
        }

        public static readonly DependencyProperty FontModeProperty =
            DependencyProperty.Register("FontMode", typeof(InputFontMode), typeof(CodeInput),
                new PropertyMetadata(InputFontMode.Monospace));

        #endregion

        #region Placeholder

        public TextInputPlaceholder? Placeholder
        {
            get { return (TextInputPlaceholder?)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(TextInputPlaceholder), typeof(CodeInput),
                new PropertyMetadata(null, OnPlaceholderChanged));

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextInputBaseInternals.PlaceholderProperty, e.NewValue);
        }

        #endregion

        #region InputHorizontalAlignment

        public HorizontalAlignment InputHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(InputHorizontalAlignmentProperty); }
            set { SetValue(InputHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty InputHorizontalAlignmentProperty =
            DependencyProperty.Register("InputHorizontalAlignment", typeof(HorizontalAlignment), typeof(CodeInput),
                new PropertyMetadata(HorizontalAlignment.Left, InputHorizontalAlignmentChanged));

        private static void InputHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextInputBaseInternals.InputHorizontalAlignmentProperty, e.NewValue);
        }

        #endregion

        #region InputWidth

        public double InputWidth
        {
            get { return (double)GetValue(InputWidthProperty); }
            set { SetValue(InputWidthProperty, value); }
        }

        public static readonly DependencyProperty InputWidthProperty =
            DependencyProperty.Register("InputWidth", typeof(double), typeof(CodeInput),
                new PropertyMetadata(double.NaN, InputWidthChanged));

        private static void InputWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextInputBaseInternals.InputWidthProperty, e.NewValue);
        }

        #endregion
    }
}
