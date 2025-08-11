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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public abstract class TextIconButtonBase<TUIKitIcons, TIconButtonElement> : TextInlineBase
    where TUIKitIcons : Enum
    where TIconButtonElement : IconButtonBase, new()
{
    protected TextIconButtonBase()
    {
        IconButtonElement = new TIconButtonElement();
        IconButtonElement.SetBinding(IconButtonBase.CommandProperty, new Binding { Source = this, Path = CommandProperty.AsPath() });
        IconButtonElement.SetBinding(IconButtonBase.CommandParameterProperty, new Binding { Source = this, Path = CommandParameterProperty.AsPath() });
    }

    #region Command

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(TextIconButtonBase<TUIKitIcons, TIconButtonElement>),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region CommandParameter

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        nameof(CommandParameter),
        typeof(object),
        typeof(TextIconButtonBase<TUIKitIcons, TIconButtonElement>),
        new PropertyMetadata(default(object)));

    #endregion

    #region Icon

    public TUIKitIcons Icon
    {
        get => (TUIKitIcons)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(TUIKitIcons),
        typeof(TextIconButtonBase<TUIKitIcons, TIconButtonElement>),
        new PropertyMetadata(default(TUIKitIcons), OnIconChanged));

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((TextIconButtonBase<TUIKitIcons, TIconButtonElement>)d).OnIconChanged();

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
        typeof(TextIconButtonBase<TUIKitIcons, TIconButtonElement>),
        new PropertyMetadata(default(Brush), OnIconBrushChanged));

    private static void OnIconBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((TextIconButtonBase<TUIKitIcons, TIconButtonElement>)d).OnIconBrushChanged();

    #endregion

    protected TIconButtonElement IconButtonElement { get; }

    protected sealed override UIElement PrepareChildElement(TextBlock parent)
        => IconButtonElement;

    protected abstract void OnIconChanged();

    private void OnIconBrushChanged()
    {
        if (IconBrush == null)
        {
            IconButtonElement.ClearValue(IconButtonBase.IconBrushProperty);
        }
        else
        {
            IconButtonElement.SetValue(IconButtonBase.IconBrushProperty, IconBrush);
        }
    }
}
