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

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal abstract class FontBehavior<TElement, TBehavior> : Behavior<TElement, TBehavior>
    where TElement : DependencyObject
    where TBehavior : Behavior<TElement>, new()
{
    #region Style

    public static UIKitFontStyleSettings GetStyle(DependencyObject obj)
        => (UIKitFontStyleSettings)obj.GetValue(StyleProperty);

    public static void SetStyle(DependencyObject obj, UIKitFontStyleSettings value)
        => obj.SetValue(StyleProperty, value);

    public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
        "Style",
        typeof(UIKitFontStyleSettings),
        typeof(FontBehavior<TElement, TBehavior>),
        new PropertyMetadata(default(UIKitFontStyleSettings), OnStyleChanged));

    private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement frameworkElement && GetIsEnabled(frameworkElement))
        {
            var behavior = GetBehavior(frameworkElement);

            Guard.EnsureIsInstanceOfType<FontBehavior<TElement, TBehavior>>(behavior).UpdateTextBlock();
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
        typeof(FontBehavior<TElement, TBehavior>),
        new PropertyMetadata(default(UIKitFontBrushSettings), OnBrushChanged));

    private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement frameworkElement && GetIsEnabled(frameworkElement))
        {
            var behavior = GetBehavior(frameworkElement);

            Guard.EnsureIsInstanceOfType<FontBehavior<TElement, TBehavior>>(behavior).UpdateTextBlock();
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
            var frameworkObject = new FrameworkObject(AssociatedObject);

            var styleSettings = GetStyle(AssociatedObject);
            if (styleSettings != null && !string.IsNullOrEmpty(styleSettings.Style))
            {

                var fontStyle = Guard.EnsureIsInstanceOfType<Style>(frameworkObject.FindResource(styleSettings.Style));

                SetValueOrBinding(frameworkObject, TextBlock.LineHeightProperty, GetPropertyValueFromStyle(TextBlock.LineHeightProperty, fontStyle));
                SetValueOrBinding(frameworkObject, TextBlock.LineStackingStrategyProperty, GetPropertyValueFromStyle(TextBlock.LineStackingStrategyProperty, fontStyle));
                SetValueOrBinding(frameworkObject, TextBlock.FontFamilyProperty, GetPropertyValueFromStyle(TextBlock.FontFamilyProperty, fontStyle));
                SetValueOrBinding(frameworkObject, TextBlock.FontSizeProperty, GetPropertyValueFromStyle(TextBlock.FontSizeProperty, fontStyle));
                SetValueOrBinding(frameworkObject, TextBlock.FontStyleProperty, GetPropertyValueFromStyle(TextBlock.FontStyleProperty, fontStyle));
                SetValueOrBinding(frameworkObject, TextBlock.FontWeightProperty, GetPropertyValueFromStyle(TextBlock.FontWeightProperty, fontStyle));

                if (AssociatedObject is TextBlock)
                {
                    SetValueOrBinding(frameworkObject, AccessibilityProperties.FontPriorityProperty, GetPropertyValueFromStyle(AccessibilityProperties.FontPriorityProperty, fontStyle));
                    SetValueOrBinding(frameworkObject, AccessibilityProperties._isHeaderProperty, GetPropertyValueFromStyle(AccessibilityProperties._isHeaderProperty, fontStyle));
                }
            }

            var brushSettings = GetBrush(AssociatedObject);
            if (brushSettings != null && !string.IsNullOrEmpty(brushSettings.Brush))
            {
                var fontBrush = new FontBrushExtension(brushSettings.Brush, UIKitConstants.PaletteScope);

                SetValueOrBinding(frameworkObject, TextBlock.ForegroundProperty, fontBrush.ProvideValue(AssociatedObject, TextBlock.ForegroundProperty));
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

        var frameworkObject = new FrameworkObject(AssociatedObject);

        _originalLineHeight = frameworkObject.GetValue<double>(TextBlock.LineHeightProperty);
        _originalLineStackingStrategy = frameworkObject.GetValue<LineStackingStrategy>(TextBlock.LineStackingStrategyProperty);
        _originalFontFamily = frameworkObject.GetValue<FontFamily>(TextBlock.FontFamilyProperty);
        _originalFontSize = frameworkObject.GetValue<double>(TextBlock.FontSizeProperty);
        _originalFontStyle = frameworkObject.GetValue<FontStyle>(TextBlock.FontStyleProperty);
        _originalFontWeight = frameworkObject.GetValue<FontWeight>(TextBlock.FontWeightProperty);
        _originalForeground = frameworkObject.GetValue<Brush>(TextBlock.ForegroundProperty);

        if (AssociatedObject is TextBlock)
        {
            _originalFontPriority = frameworkObject.GetValue<int>(AccessibilityProperties.FontPriorityProperty);
            _originalIsHeader = frameworkObject.GetValue<bool>(AccessibilityProperties._isHeaderProperty);
        }
    }

    private void ResetToOriginalValues()
    {
        Guard.IsNotNull(AssociatedObject);

        var frameworkObject = new FrameworkObject(AssociatedObject);

        frameworkObject.SetValue(TextBlock.LineHeightProperty, _originalLineHeight);
        frameworkObject.SetValue(TextBlock.LineStackingStrategyProperty, _originalLineStackingStrategy);
        frameworkObject.SetValue(TextElement.FontFamilyProperty, _originalFontFamily);
        frameworkObject.SetValue(TextElement.FontSizeProperty, _originalFontSize);
        frameworkObject.SetValue(TextElement.FontStyleProperty, _originalFontStyle);
        frameworkObject.SetValue(TextElement.FontWeightProperty, _originalFontWeight);
        frameworkObject.SetValue(TextElement.ForegroundProperty, _originalForeground);

        if (AssociatedObject is TextBlock)
        {
            frameworkObject.SetValue(AccessibilityProperties.FontPriorityProperty, _originalFontPriority);
            frameworkObject.SetValue(AccessibilityProperties._isHeaderProperty, _originalIsHeader);
        }
    }

    private static void SetValueOrBinding(FrameworkObject frameworkObject, DependencyProperty dependencyProperty, object? value)
    {
        if (value is BindingBase binding)
        {
            frameworkObject.SetBinding(dependencyProperty, binding);
        }
        else
        {
            frameworkObject.SetValue(dependencyProperty, value);
        }
    }

    private sealed class FontBrushExtension : BaseLocalizationMarkupExtension
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
            return LocalizationManager.GetLocalizer<IXamlLocalizer>(Scope);
        }
    }

    private double _originalLineHeight;
    private LineStackingStrategy _originalLineStackingStrategy;
    private FontFamily? _originalFontFamily;
    private double _originalFontSize;
    private Brush? _originalForeground;
    private FontStyle _originalFontStyle;
    private FontWeight _originalFontWeight;
    private int? _originalFontPriority;
    private bool? _originalIsHeader;
}
