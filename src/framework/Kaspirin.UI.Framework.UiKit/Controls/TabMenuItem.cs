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

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class TabMenuItem : SelectorItem
{
    #region Counter

    public int Counter
    {
        get => (int)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }

    public static readonly DependencyProperty CounterProperty = DependencyProperty.Register(
        nameof(Counter),
        typeof(int),
        typeof(TabMenuItem),
        new PropertyMetadata(default(int), null, CoerceCounter));

    private static object CoerceCounter(DependencyObject d, object baseValue)
    {
        var counter = (int)baseValue;
        return counter < 0 ? 0 : counter;
    }

    #endregion

    #region MaxCounter

    public int MaxCounter
    {
        get => (int)GetValue(MaxCounterProperty);
        set => SetValue(MaxCounterProperty, value);
    }

    public static readonly DependencyProperty MaxCounterProperty = DependencyProperty.Register(
        nameof(MaxCounter),
        typeof(int),
        typeof(TabMenuItem),
        new PropertyMetadata(UIKitConstants.TabMenuItemMaxCounter, null, CoerceMaxCounter));

    private static object CoerceMaxCounter(DependencyObject d, object baseValue)
    {
        var counter = (int)baseValue;
        return counter < 1 ? 1 : counter;
    }

    #endregion

    #region Icon

    public UIKitIcon_16 Icon
    {
        get => (UIKitIcon_16)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(UIKitIcon_16),
        typeof(TabMenuItem),
        new PropertyMetadata(default(UIKitIcon_16)));

    #endregion

    #region HasCounter

    public bool HasCounter
    {
        get => (bool)GetValue(HasCounterProperty);
        set => SetValue(HasCounterProperty, value);
    }

    public static readonly DependencyProperty HasCounterProperty = DependencyProperty.Register(
        nameof(HasCounter),
        typeof(bool),
        typeof(TabMenuItem),
        new PropertyMetadata(default(bool)));

    #endregion

    #region HasIcon

    public bool HasIcon
    {
        get => (bool)GetValue(HasIconProperty);
        set => SetValue(HasIconProperty, value);
    }

    public static readonly DependencyProperty HasIconProperty = DependencyProperty.Register(
        nameof(HasIcon),
        typeof(bool),
        typeof(TabMenuItem),
        new PropertyMetadata(default(bool)));

    #endregion
}
