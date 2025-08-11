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
using System.Windows.Controls;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Properties;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public abstract class IconBase<TUIKitIcons> : IconBase where TUIKitIcons : Enum
{
    static IconBase()
    {
        MinHeightProperty.OverrideMetadata(typeof(IconBase<TUIKitIcons>),
            new FrameworkPropertyMetadata(_size.Value, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceMinHeightWidth));

        MinWidthProperty.OverrideMetadata(typeof(IconBase<TUIKitIcons>),
            new FrameworkPropertyMetadata(_size.Value, FrameworkPropertyMetadataOptions.AffectsMeasure, null, CoerceMinHeightWidth));

#if NETFRAMEWORK
        UIKitIconMetadataRegistrar.RegisterMetadata();
#endif
    }

    protected IconBase()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    #region Icon

    public TUIKitIcons Icon
    {
        get => (TUIKitIcons)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(TUIKitIcons),
        typeof(IconBase<TUIKitIcons>),
        new PropertyMetadata(default(TUIKitIcons), OnIconChanged));

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((IconBase<TUIKitIcons>)d).ApplyIcon();

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ApplyIcon();
    }

    protected sealed override void OnIconBrushChanged()
    {
        var uiKitIcon = new UIKitIconBuilder(Icon);
        Guard.IsNotNull(uiKitIcon.IconName);

        var iconMetadata = UIKitIconMetadataStorage.Get(Icon);

        ApplyIconBrush(uiKitIcon.IconName, iconMetadata, IconBrush);
    }

    private void ApplyIcon()
    {
        if (Image is null)
        {
            return;
        }

        var icon = Icon;

        var uiKitIcon = new UIKitIconBuilder(icon);
        Guard.IsNotNull(uiKitIcon.IconName);

        var iconMetadata = UIKitIconMetadataStorage.Get(icon);

        ApplyIconSize(_size.Value);
        ApplyIconBrush(uiKitIcon.IconName, iconMetadata, IconBrush);
        ApplyFlowDirection(uiKitIcon.IconName, iconMetadata);

        IsUnset = uiKitIcon.IconName == UIKitIcons.Unset;

        if (IsUnset)
        {
            Image.ClearValue(Image.SourceProperty);
        }
        else
        {
            var iconBuilder = new UIKitIconBuilder(icon);

            Image.SetValue(Image.SourceProperty, new ImgExtension()
            {
                Key = iconBuilder.IconKey,
                Scope = iconBuilder.IconScope,
                Mode = ImgExtensionMode.SvgImage
            }.ProvideValue(Image, Image.SourceProperty));
        }
    }

    private void ApplyIconSize(double iconSize)
    {
        if (Image is null)
        {
            return;
        }

        Image.SetValue(HeightProperty, iconSize);
        Image.SetValue(WidthProperty, iconSize);
    }

    private void ApplyIconBrush(string iconName, UIKitIconMetadata iconMetadata, Brush? iconBrush)
    {
        if (Image is null)
        {
            return;
        }

        if (iconName == UIKitIcons.Unset || iconMetadata.IsColorfull)
        {
            Image.ClearValue(ImageProps.SvgBrushProperty);
        }
        else
        {
            Image.SetValue(ImageProps.SvgBrushProperty, iconBrush);
        }
    }

    private void ApplyFlowDirection(string iconName, UIKitIconMetadata iconMetadata)
    {
        if (Image is null)
        {
            return;
        }

        if (iconName == UIKitIcons.Unset)
        {
            Image.ClearValue(FlowDirectionProperty);
        }
        else if (iconMetadata.IsAutoRTL)
        {
            Image.SetValue(FlowDirectionProperty, LocalizationManager.DisplayCulture.FlowDirection);
        }
        else
        {
            Image.SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        LocalizationManager.CultureChanged += OnCultureChanged;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        LocalizationManager.CultureChanged -= OnCultureChanged;
    }

    private void OnCultureChanged()
    {
        Executers.InUiSyncOrAsync(ApplyIcon);
    }

    private static double GetSize()
    {
        var iconType = typeof(TUIKitIcons);

        if (iconType == typeof(UIKitIcon_12))
        {
            return 12;
        }
        else if (iconType == typeof(UIKitIcon_16))
        {
            return 16;
        }
        else if (iconType == typeof(UIKitIcon_24))
        {
            return 24;
        }
        else if (iconType == typeof(UIKitIcon_32))
        {
            return 32;
        }
        else if (iconType == typeof(UIKitIcon_48))
        {
            return 48;
        }
        else
        {
            throw new ArgumentException($"Unable to get size for UIKit icon of type '{iconType.Name}'");
        }
    }

    private static object CoerceMinHeightWidth(DependencyObject d, object baseValue)
    {
        var value = (double)baseValue;

        return value < _size.Value
            ? _size.Value
            : value;
    }

    private static readonly Lazy<double> _size = new(GetSize);
}
