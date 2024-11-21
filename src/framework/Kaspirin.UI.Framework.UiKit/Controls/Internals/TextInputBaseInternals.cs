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

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal static class TextInputBaseInternals
    {
        #region ActionBar

        public static InputActionCollection GetActionBar(DependencyObject obj)
            => (InputActionCollection)obj.GetValue(ActionBarProperty);

        public static void SetActionBar(DependencyObject obj, InputActionCollection value)
            => obj.SetValue(ActionBarProperty, value);

        public static readonly DependencyProperty ActionBarProperty = DependencyProperty.RegisterAttached(
            "ActionBar",
            typeof(InputActionCollection),
            typeof(TextInputBaseInternals));

        #endregion

        #region Placeholder

        public static TextInputPlaceholder? GetPlaceholder(DependencyObject obj)
            => (TextInputPlaceholder?)obj.GetValue(PlaceholderProperty);

        public static void SetPlaceholder(DependencyObject obj, TextInputPlaceholder? value)
            => obj.SetValue(PlaceholderProperty, value);

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached(
            "Placeholder",
            typeof(TextInputPlaceholder),
            typeof(TextInputBaseInternals),
            new PropertyMetadata(null, PlaceholderChanged));

        private static void PlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textInput = (TextInputBase)d;

            textInput.InvalidateText();
            textInput.InvalidatePlaceholderStyle();
            textInput.InvalidateImeState();
        }

        #endregion

        #region InputHorizontalAlignment

        public static HorizontalAlignment GetInputHorizontalAlignment(DependencyObject obj)
            => (HorizontalAlignment)obj.GetValue(InputHorizontalAlignmentProperty);

        public static void SetInputHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
            => obj.SetValue(InputHorizontalAlignmentProperty, value);

        public static readonly DependencyProperty InputHorizontalAlignmentProperty = DependencyProperty.RegisterAttached(
            "InputHorizontalAlignment",
            typeof(HorizontalAlignment),
            typeof(TextInputBaseInternals),
            new PropertyMetadata(HorizontalAlignment.Left));

        #endregion

        #region InputWidth

        public static double GetInputWidth(DependencyObject obj)
            => (double)obj.GetValue(InputWidthProperty);

        public static void SetInputWidth(DependencyObject obj, double value)
            => obj.SetValue(InputWidthProperty, value);

        public static readonly DependencyProperty InputWidthProperty = DependencyProperty.RegisterAttached(
            "InputWidth",
            typeof(double),
            typeof(TextInputBaseInternals),
            new PropertyMetadata(double.NaN));

        #endregion

        #region InputHeight

        public static double GetInputHeight(DependencyObject obj)
            => (double)obj.GetValue(InputHeightProperty);

        public static void SetInputHeight(DependencyObject obj, double value)
            => obj.SetValue(InputHeightProperty, value);

        public static readonly DependencyProperty InputHeightProperty = DependencyProperty.RegisterAttached(
            "InputHeight",
            typeof(double),
            typeof(TextInputBaseInternals),
            new PropertyMetadata(double.NaN));

        #endregion

        #region HasCounter

        public static bool GetHasCounter(DependencyObject obj)
            => (bool)obj.GetValue(HasCounterProperty);

        public static void SetHasCounter(DependencyObject obj, bool value)
            => obj.SetValue(HasCounterProperty, value);

        public static readonly DependencyProperty HasCounterProperty = DependencyProperty.RegisterAttached(
            "HasCounter",
            typeof(bool),
            typeof(TextInputBaseInternals));

        #endregion

        #region IsMultiline

        public static bool GetIsMultiline(DependencyObject obj)
            => (bool)obj.GetValue(IsMultilineProperty);

        public static void SetIsMultiline(DependencyObject obj, bool value)
            => obj.SetValue(IsMultilineProperty, value);

        public static readonly DependencyProperty IsMultilineProperty = DependencyProperty.RegisterAttached(
            "IsMultiline",
            typeof(bool),
            typeof(TextInputBaseInternals));

        #endregion
    }
}
