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
using System.Windows.Markup;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[ContentProperty(nameof(ActionBar))]
public sealed class TextInput : TextInputBase
{
    #region ActionBar

    public InputActionCollection ActionBar
    {
        get => (InputActionCollection)GetValue(ActionBarProperty);
        set => SetValue(ActionBarProperty, value);
    }

    public static readonly DependencyProperty ActionBarProperty = DependencyProperty.Register(
        nameof(ActionBar),
        typeof(InputActionCollection),
        typeof(TextInput),
        new PropertyMetadata(default(InputActionCollection), ActionBarChanged));

    private static void ActionBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(TextInputBaseInternals.ActionBarProperty, e.NewValue);
    }

    #endregion

    #region FontMode

    public InputFontMode FontMode
    {
        get => (InputFontMode)GetValue(FontModeProperty);
        set => SetValue(FontModeProperty, value);
    }

    public static readonly DependencyProperty FontModeProperty = DependencyProperty.Register(
        nameof(FontMode),
        typeof(InputFontMode),
        typeof(TextInput),
        new PropertyMetadata(InputFontMode.Regular));

    #endregion

    #region Placeholder

    public TextInputPlaceholder? Placeholder
    {
        get => (TextInputPlaceholder?)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder),
        typeof(TextInputPlaceholder),
        typeof(TextInput),
        new PropertyMetadata(default(TextInputPlaceholder), OnPlaceholderChanged));

    private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(TextInputBaseInternals.PlaceholderProperty, e.NewValue);
    }

    #endregion

    #region InputHorizontalAlignment

    public HorizontalAlignment InputHorizontalAlignment
    {
        get => (HorizontalAlignment)GetValue(InputHorizontalAlignmentProperty);
        set => SetValue(InputHorizontalAlignmentProperty, value);
    }

    public static readonly DependencyProperty InputHorizontalAlignmentProperty = DependencyProperty.Register(
        nameof(InputHorizontalAlignment),
        typeof(HorizontalAlignment),
        typeof(TextInput),
        new PropertyMetadata(HorizontalAlignment.Left, InputHorizontalAlignmentChanged));

    private static void InputHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(TextInputBaseInternals.InputHorizontalAlignmentProperty, e.NewValue);
    }

    #endregion

    #region InputWidth

    public double InputWidth
    {
        get => (double)GetValue(InputWidthProperty);
        set => SetValue(InputWidthProperty, value);
    }

    public static readonly DependencyProperty InputWidthProperty = DependencyProperty.Register(
        nameof(InputWidth),
        typeof(double),
        typeof(TextInput),
        new PropertyMetadata(double.NaN, InputWidthChanged));

    private static void InputWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(TextInputBaseInternals.InputWidthProperty, e.NewValue);
    }

    #endregion
}
