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
    public class TextArea : TextInputBase
    {
        public TextArea()
        {
            SetValue(TextInputBaseInternals.IsMultilineProperty, true);
        }

        #region Placeholder

        public string? Placeholder
        {
            get { return (string?)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextArea),
                new PropertyMetadata(null, OnPlaceholderChanged));

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var stringValue = e.NewValue as string ?? string.Empty;

            d.SetValue(TextInputBaseInternals.PlaceholderProperty, new TextInputStringPlaceholder(stringValue));
        }

        #endregion

        #region HasCounter

        public bool HasCounter
        {
            get { return (bool)GetValue(HasCounterProperty); }
            set { SetValue(HasCounterProperty, value); }
        }

        public static readonly DependencyProperty HasCounterProperty =
            DependencyProperty.Register("HasCounter", typeof(bool), typeof(TextArea),
                new PropertyMetadata(false, OnHasCounterChanged));

        private static void OnHasCounterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextInputBaseInternals.HasCounterProperty, e.NewValue);
        }

        #endregion

        #region InputHeight

        public double InputHeight
        {
            get { return (double)GetValue(InputHeightProperty); }
            set { SetValue(InputHeightProperty, value); }
        }

        public static readonly DependencyProperty InputHeightProperty =
            DependencyProperty.Register("InputHeight", typeof(double), typeof(TextArea)
                , new PropertyMetadata(double.NaN, InputHeightChanged));

        private static void InputHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextInputBaseInternals.InputHeightProperty, e.NewValue);
        }

        #endregion
    }
}
