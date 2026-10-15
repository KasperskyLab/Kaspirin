// Copyright © 2025 AO Kaspersky Lab.
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
using System.Windows.Documents;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Properties;

public static class RunProps
{
    #region FontStyle

    public static UIKitFontStyleSettings? GetFontStyle(DependencyObject obj)
        => (UIKitFontStyleSettings?)obj.GetValue(FontStyleProperty);

    public static void SetFontStyle(DependencyObject obj, UIKitFontStyleSettings? value)
        => obj.SetValue(FontStyleProperty, value);

    public static readonly DependencyProperty FontStyleProperty = DependencyProperty.RegisterAttached(
        "FontStyle",
        typeof(UIKitFontStyleSettings),
        typeof(RunProps),
        UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(Run), nameof(FontStyleProperty), OnFontStyleChanged));

    private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != null)
        {
            d.SetValue(FrameworkContentElementFontBehavior.StyleProperty, e.NewValue);
            d.SetValue(FrameworkContentElementFontBehavior.IsEnabledProperty, true);
        }
        else
        {
            d.SetValue(FrameworkContentElementFontBehavior.StyleProperty, DependencyProperty.UnsetValue);

            if (d.GetValue(FontStyleProperty) == null &&
                d.GetValue(FontBrushProperty) == null)
            {
                d.SetValue(FrameworkContentElementFontBehavior.IsEnabledProperty, false);
            }
        }
    }

    #endregion

    #region FontBrush

    public static UIKitFontBrushSettings GetFontBrush(DependencyObject obj)
        => (UIKitFontBrushSettings)obj.GetValue(FontBrushProperty);

    public static void SetFontBrush(DependencyObject obj, UIKitFontBrushSettings value)
        => obj.SetValue(FontBrushProperty, value);

    public static readonly DependencyProperty FontBrushProperty = DependencyProperty.RegisterAttached(
        "FontBrush",
        typeof(UIKitFontBrushSettings),
        typeof(RunProps),
        UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(Run), nameof(FontBrushProperty), OnFontBrushChanged));

    private static void OnFontBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != null)
        {
            d.SetValue(FrameworkContentElementFontBehavior.BrushProperty, e.NewValue);
            d.SetValue(FrameworkContentElementFontBehavior.IsEnabledProperty, true);
        }
        else
        {
            d.SetValue(FrameworkContentElementFontBehavior.BrushProperty, DependencyProperty.UnsetValue);

            if (d.GetValue(FontStyleProperty) == null &&
                d.GetValue(FontBrushProperty) == null)
            {
                d.SetValue(FrameworkContentElementFontBehavior.IsEnabledProperty, false);
            }
        }
    }

    #endregion
}
