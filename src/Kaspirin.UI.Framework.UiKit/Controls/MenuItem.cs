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
using System.Windows.Data;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public class MenuItem : MenuItemBase
{
    public MenuItem()
    {
        Click += OnMenuItemClick;

        SetBinding(MenuItemRoleProperty, new Binding()
        {
            Source = this,
            Mode = BindingMode.OneWay,
            Path = RoleProperty.AsPath(),
            Converter = _roleConverter
        });
    }

    #region Description

    public object Description
    {
        get => (object)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description),
        typeof(object),
        typeof(MenuItem),
        new PropertyMetadata(default(object)));

    #endregion

    #region HasBadge

    public bool HasBadge
    {
        get => (bool)GetValue(HasBadgeProperty);
        set => SetValue(HasBadgeProperty, value);
    }

    public static readonly DependencyProperty HasBadgeProperty = DependencyProperty.Register(
        nameof(HasBadge),
        typeof(bool),
        typeof(MenuItem),
        new PropertyMetadata(default(bool)));

    #endregion

    #region IconBrush

    public Brush? IconBrush
    {
        get => (Brush?)GetValue(IconBrushProperty);
        set => SetValue(IconBrushProperty, value);
    }

    public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register(
        nameof(IconBrush),
        typeof(Brush),
        typeof(MenuItem),
        new PropertyMetadata(default(Brush)));

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
        typeof(MenuItem),
        new PropertyMetadata(default(BadgeType)));

    #endregion

    private void OnMenuItemClick(object sender, RoutedEventArgs e)
    {
        var menuItem = (MenuItem)sender;

        ServiceLocator.GetService<IStatisticsSender>().SendMenuItemClicked(menuItem);

        var contextMenu = this.FindVisualParent<ContextMenu>() ?? this.FindLogicalParent<ContextMenu>();
        var contextMenuHolder = (contextMenu?.PlacementTarget as FrameworkElement)?.TemplatedParent;
        if (contextMenuHolder is ContextMenuSelect contextMenuSelect)
        {
            contextMenuSelect.SetCurrentValue(ContextMenuSelect.SelectedItemProperty, DataContext ?? this);
        }
    }

    private readonly IValueConverter _roleConverter = new DelegateConverter<System.Windows.Controls.MenuItemRole>(r =>
    {
        return (MenuItemRole)r;
    });
}
