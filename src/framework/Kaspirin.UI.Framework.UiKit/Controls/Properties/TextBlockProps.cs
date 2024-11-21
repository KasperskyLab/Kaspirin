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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Properties
{
    public static class TextBlockProps
    {
        #region ShowToolTipOnTrim

        public static bool GetShowToolTipOnTrim(DependencyObject obj)
            => (bool)obj.GetValue(ShowToolTipOnTrimProperty);

        public static void SetShowToolTipOnTrim(DependencyObject obj, bool value)
            => obj.SetValue(ShowToolTipOnTrimProperty, value);

        public static readonly DependencyProperty ShowToolTipOnTrimProperty = DependencyProperty.RegisterAttached(
            "ShowToolTipOnTrim",
            typeof(bool),
            typeof(TextBlockProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(TextBlock), nameof(ShowToolTipOnTrimProperty), OnShowToolTipOnTrimChanged));

        private static void OnShowToolTipOnTrimChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => d.SetValue(TextBlockToolTipOnTrimBehavior.IsEnabledProperty, (bool)e.NewValue);

        #endregion

        #region LinesMaxCount

        public static int? GetLinesMaxCount(DependencyObject obj)
            => (int?)obj.GetValue(LinesMaxCountProperty);

        public static void SetLinesMaxCount(DependencyObject obj, int? value)
            => obj.SetValue(LinesMaxCountProperty, value);

        public static readonly DependencyProperty LinesMaxCountProperty = DependencyProperty.RegisterAttached(
            "LinesMaxCount",
            typeof(int?),
            typeof(TextBlockProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(TextBlock), nameof(LinesMaxCountProperty), OnLinesMaxCountChanged));

        private static void OnLinesMaxCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                d.SetValue(TextBlockLinesLimiterBehavior.MaxLinesProperty, (int)e.NewValue);
                d.SetValue(TextBlockLinesLimiterBehavior.IsEnabledProperty, true);
            }
            else
            {
                d.SetValue(TextBlockLinesLimiterBehavior.IsEnabledProperty, false);
                d.SetValue(TextBlockLinesLimiterBehavior.MaxLinesProperty, DependencyProperty.UnsetValue);
            }
        }

        #endregion

        #region FontStyle

        public static UIKitFontStyleSettings? GetFontStyle(DependencyObject obj)
            => (UIKitFontStyleSettings?)obj.GetValue(FontStyleProperty);

        public static void SetFontStyle(DependencyObject obj, UIKitFontStyleSettings? value)
            => obj.SetValue(FontStyleProperty, value);

        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.RegisterAttached(
            "FontStyle",
            typeof(UIKitFontStyleSettings),
            typeof(TextBlockProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(TextBlock), nameof(FontStyleProperty), OnFontStyleChanged));

        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                d.SetValue(TextBlockFontBehavior.StyleProperty, e.NewValue);
                d.SetValue(TextBlockFontBehavior.IsEnabledProperty, true);
            }
            else
            {
                d.SetValue(TextBlockFontBehavior.StyleProperty, DependencyProperty.UnsetValue);

                if (d.GetValue(FontStyleProperty) == null &&
                    d.GetValue(FontBrushProperty) == null)
                {
                    d.SetValue(TextBlockFontBehavior.IsEnabledProperty, false);
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
            typeof(TextBlockProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(TextBlock), nameof(FontBrushProperty), OnFontBrushChanged));

        private static void OnFontBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                d.SetValue(TextBlockFontBehavior.BrushProperty, e.NewValue);
                d.SetValue(TextBlockFontBehavior.IsEnabledProperty, true);
            }
            else
            {
                d.SetValue(TextBlockFontBehavior.BrushProperty, DependencyProperty.UnsetValue);

                if (d.GetValue(FontStyleProperty) == null &&
                    d.GetValue(FontBrushProperty) == null)
                {
                    d.SetValue(TextBlockFontBehavior.IsEnabledProperty, false);
                }
            }
        }

        #endregion
    }
}
