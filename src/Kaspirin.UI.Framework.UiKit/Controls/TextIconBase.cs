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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public abstract class TextIconBase<TUIKitIcons, TIconElement> : TextInlineBase
    where TUIKitIcons : Enum
    where TIconElement : IconBase<TUIKitIcons>, new()
{
    protected TextIconBase()
    {
        _iconElement = new TIconElement();
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
        typeof(TextIconBase<TUIKitIcons, TIconElement>),
        new PropertyMetadata(default(TUIKitIcons), OnIconChanged));

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((TextIconBase<TUIKitIcons, TIconElement>)d).OnIconChanged();

    #endregion

    #region IconBrush

    public Brush IconBrush
    {
        get => (Brush)GetValue(IconBrushProperty);
        set => SetValue(IconBrushProperty, value);
    }

    public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register(
        nameof(IconBrush),
        typeof(Brush),
        typeof(TextIconBase<TUIKitIcons, TIconElement>),
        new PropertyMetadata(default(Brush), OnIconBrushChanged));

    private static void OnIconBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((TextIconBase<TUIKitIcons, TIconElement>)d).OnIconBrushChanged();

    #endregion

    protected sealed override UIElement PrepareChildElement(TextBlock parent)
        => _iconElement;

    private void OnIconBrushChanged()
    {
        if (IconBrush == null)
        {
            _iconElement.ClearValue(IconBase<TUIKitIcons>.IconBrushProperty);
        }
        else
        {
            _iconElement.SetValue(IconBase<TUIKitIcons>.IconBrushProperty, IconBrush);
        }
    }

    private void OnIconChanged()
        => _iconElement.SetValue(IconBase<TUIKitIcons>.IconProperty, Icon);

    private readonly TIconElement _iconElement;
}
