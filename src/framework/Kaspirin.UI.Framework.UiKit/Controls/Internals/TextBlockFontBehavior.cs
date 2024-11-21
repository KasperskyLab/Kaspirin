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

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class TextBlockFontBehavior : Behavior<TextBlock, TextBlockFontBehavior>
    {
        #region Style

        public static UIKitFontStyleSettings GetStyle(DependencyObject obj)
            => (UIKitFontStyleSettings)obj.GetValue(StyleProperty);

        public static void SetStyle(DependencyObject obj, UIKitFontStyleSettings value)
            => obj.SetValue(StyleProperty, value);

        public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
            "Style",
            typeof(UIKitFontStyleSettings),
            typeof(TextBlockFontBehavior),
            new PropertyMetadata(default(UIKitFontStyleSettings), OnStyleChanged));

        private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBlock textBlock && GetIsEnabled(textBlock))
            {
                GetBehavior(textBlock).UpdateTextBlock();
            }
        }

        #endregion

        #region Brush

        public static UIKitFontBrushSettings GetBrush(DependencyObject obj)
            => (UIKitFontBrushSettings)obj.GetValue(BrushProperty);

        public static void SetBrush(DependencyObject obj, UIKitFontBrushSettings value)
            => obj.SetValue(BrushProperty, value);

        public static readonly DependencyProperty BrushProperty = DependencyProperty.RegisterAttached(
            "Brush",
            typeof(UIKitFontBrushSettings),
            typeof(TextBlockFontBehavior),
            new PropertyMetadata(default(UIKitFontBrushSettings), OnBrushChanged));

        private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBlock textBlock && GetIsEnabled(textBlock))
            {
                GetBehavior(textBlock).UpdateTextBlock();
            }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            SaveOriginalValues();

            UpdateTextBlock();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            ResetToOriginalValues();
        }

        private void UpdateTextBlock()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.WhenInitialized(() =>
            {
                var styleSettings = GetStyle(AssociatedObject);
                if (styleSettings != null && !string.IsNullOrEmpty(styleSettings.Style))
                {
                    var fontStyle = Guard.EnsureIsInstanceOfType<Style>(AssociatedObject.FindResource(styleSettings.Style));

                    SetValueOrBinding(AssociatedObject, TextBlock.LineHeightProperty, GetPropertyValueFromStyle(TextBlock.LineHeightProperty, fontStyle));
                    SetValueOrBinding(AssociatedObject, TextBlock.LineStackingStrategyProperty, GetPropertyValueFromStyle(TextBlock.LineStackingStrategyProperty, fontStyle));
                    SetValueOrBinding(AssociatedObject, TextBlock.FontFamilyProperty, GetPropertyValueFromStyle(TextBlock.FontFamilyProperty, fontStyle));
                    SetValueOrBinding(AssociatedObject, TextBlock.FontSizeProperty, GetPropertyValueFromStyle(TextBlock.FontSizeProperty, fontStyle));
                    SetValueOrBinding(AssociatedObject, TextBlock.FontStyleProperty, GetPropertyValueFromStyle(TextBlock.FontStyleProperty, fontStyle));
                    SetValueOrBinding(AssociatedObject, TextBlock.FontWeightProperty, GetPropertyValueFromStyle(TextBlock.FontWeightProperty, fontStyle));
                }

                var brushSettings = GetBrush(AssociatedObject);
                if (brushSettings != null && !string.IsNullOrEmpty(brushSettings.Brush))
                {
                    var fontBrush = new FontBrushExtension(brushSettings.Brush, UIKitConstants.PaletteScope);

                    SetValueOrBinding(AssociatedObject, TextBlock.ForegroundProperty, fontBrush.ProvideValue(AssociatedObject, TextBlock.ForegroundProperty));
                }

                if (styleSettings == null &&
                    brushSettings == null)
                {
                    ResetToOriginalValues();
                }
            });
        }

        private object? GetPropertyValueFromStyle(DependencyProperty property, Style style)
        {
            return style.Setters.OfType<Setter>().GuardedSingle(s => s.Property == property).Value;
        }

        private void SaveOriginalValues()
        {
            Guard.IsNotNull(AssociatedObject);

            _originalLineHeight = AssociatedObject.LineHeight;
            _originalLineStackingStrategy = AssociatedObject.LineStackingStrategy;
            _originalFontFamily = AssociatedObject.FontFamily;
            _originalFontSize = AssociatedObject.FontSize;
            _originalFontStyle = AssociatedObject.FontStyle;
            _originalFontWeight = AssociatedObject.FontWeight;
            _originalForeground = AssociatedObject.Foreground;
        }

        private void ResetToOriginalValues()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.LineHeight = _originalLineHeight;
            AssociatedObject.LineStackingStrategy = _originalLineStackingStrategy;
            AssociatedObject.FontFamily = _originalFontFamily;
            AssociatedObject.FontSize = _originalFontSize;
            AssociatedObject.FontStyle = _originalFontStyle;
            AssociatedObject.FontWeight = _originalFontWeight;
            AssociatedObject.Foreground = _originalForeground;
        }

        public static void SetValueOrBinding(FrameworkElement frameworkElement, DependencyProperty dependencyProperty, object? value)
        {
            Guard.ArgumentIsNotNull(frameworkElement);
            Guard.ArgumentIsNotNull(dependencyProperty);

            if (value is BindingBase binding)
            {
                frameworkElement.SetBinding(dependencyProperty, binding);
            }
            else
            {
                frameworkElement.SetValue(dependencyProperty, value);
            }
        }

        private sealed class FontBrushExtension : LocalizationMarkupBase
        {
            public FontBrushExtension(string key, string scope) : base(key, scope)
            {
            }

            protected override object? ProvideValue()
            {
                var resource = ProvideLocalizer<IXamlLocalizer>().GetResource(Key);
                if (resource is Color paletteColor)
                {
                    return new SolidColorBrush(paletteColor).GetAsFrozen();
                }

                return resource;
            }

            protected override ILocalizer PrepareLocalizer()
            {
                return LocalizationManager.Current.LocalizerFactory.Resolve<IXamlLocalizer>(Scope);
            }
        }

        private double _originalLineHeight;
        private LineStackingStrategy _originalLineStackingStrategy;
        private FontFamily? _originalFontFamily;
        private double _originalFontSize;
        private FontStyle _originalFontStyle;
        private FontWeight _originalFontWeight;
        private Brush? _originalForeground;
    }
}
