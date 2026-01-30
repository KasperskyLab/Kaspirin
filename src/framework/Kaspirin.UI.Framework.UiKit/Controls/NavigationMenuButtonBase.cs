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
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_Marker, Type = typeof(Badge))]
public abstract class NavigationMenuButtonBase : Button
{
    public const string PART_Marker = "PART_Marker";

    static NavigationMenuButtonBase()
    {
        DataContextProperty.OverrideMetadata(
            typeof(NavigationMenuButtonBase),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnDataContextChanged)));
    }

    public override void OnApplyTemplate()
    {
        ButtonBadge = Guard.EnsureIsInstanceOfType<Badge>(GetTemplateChild(PART_Marker));
    }

    internal Badge? ButtonBadge { get; set; }

    protected NavigationMenuButtonBase()
    {
        Click += NavigateOnClick;
    }

    #region IsSelected

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected),
        typeof(bool),
        typeof(NavigationMenuButtonBase),
        new PropertyMetadata(default(bool)));

    #endregion

    #region BadgeType

    public BadgeType BadgeType
    {
        get => (BadgeType)GetValue(BadgeTypeProperty);
        set => SetValue(BadgeTypeProperty, value);
    }
    public static readonly DependencyProperty BadgeTypeProperty = DependencyProperty.Register(
        nameof(BadgeType),
        typeof(BadgeType),
        typeof(NavigationMenuButtonBase),
        new PropertyMetadata(default(BadgeType)));

    #endregion

    #region ShowBadge

    public bool ShowBadge
    {
        get => (bool)GetValue(ShowBadgeProperty);
        set => SetValue(ShowBadgeProperty, value);
    }
    public static readonly DependencyProperty ShowBadgeProperty = DependencyProperty.Register(
        nameof(ShowBadge),
        typeof(bool),
        typeof(NavigationMenuButtonBase),
        new PropertyMetadata(default(bool)));

    #endregion

    #region Icon

    public UIKitIcon_24 Icon
    {
        get => (UIKitIcon_24)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(UIKitIcon_24),
        typeof(NavigationMenuButtonBase),
        new PropertyMetadata(default(UIKitIcon_24)));

    #endregion

    private void UpdateDataContextBindings()
    {
        if (DataContext is INavigationItem navigationItem)
        {
            SetBinding(IsSelectedProperty, new Binding()
            {
                Source = navigationItem,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath(nameof(navigationItem.IsSelected))
            });
            SetBinding(IsEnabledProperty, new Binding()
            {
                Source = navigationItem,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath(nameof(navigationItem.IsEnabled))
            });
            SetBinding(VisibilityProperty, new Binding()
            {
                Source = navigationItem,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath(nameof(navigationItem.IsVisible)),
                Converter = new BooleanToVisibilityConverter()
            });
        }
        else
        {
            ClearValue(IsSelectedProperty);
            ClearValue(IsEnabledProperty);
            ClearValue(VisibilityProperty);
        }
    }

    private void NavigateOnClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is INavigationItem navigationItem)
        {
            navigationItem.Navigate();
        }
    }

    private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((NavigationMenuButtonBase)d).UpdateDataContextBindings();
    }
}
