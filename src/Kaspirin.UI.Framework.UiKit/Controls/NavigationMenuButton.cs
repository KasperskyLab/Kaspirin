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
using System.Windows.Automation.Peers;
using Kaspirin.UI.Framework.UiKit.Controls.Automation;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = "PART_Container", Type = typeof(FrameworkElement))]
public sealed class NavigationMenuButton : NavigationMenuButtonBase
{
    #region Caption

    public object Caption
    {
        get => GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }

    public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
        nameof(Caption),
        typeof(object),
        typeof(NavigationMenuButton),
        new PropertyMetadata(default(object)));

    #endregion

    #region Counter

    public int Counter
    {
        get => (int)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }

    public static readonly DependencyProperty CounterProperty = DependencyProperty.Register(
        nameof(Counter),
        typeof(int),
        typeof(NavigationMenuButton),
        new PropertyMetadata(default(int)));

    #endregion

    #region Description

    public object Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description),
        typeof(object),
        typeof(NavigationMenuButton),
        new PropertyMetadata(default(object)));

    #endregion

    #region Level

    public uint Level
    {
        get => (uint)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly DependencyProperty LevelProperty = DependencyProperty.Register(
        nameof(Level),
        typeof(uint),
        typeof(NavigationMenuButton),
        new PropertyMetadata(default(uint)));

    #endregion

    #region ShowCounter

    public bool ShowCounter
    {
        get => (bool)GetValue(ShowCounterProperty);
        set => SetValue(ShowCounterProperty, value);
    }

    public static readonly DependencyProperty ShowCounterProperty = DependencyProperty.Register(
        nameof(ShowCounter),
        typeof(bool),
        typeof(NavigationMenuButton),
        new PropertyMetadata(default(bool)));

    #endregion

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new NavigationMenuButtonAutomationPeer(this);
    }
}