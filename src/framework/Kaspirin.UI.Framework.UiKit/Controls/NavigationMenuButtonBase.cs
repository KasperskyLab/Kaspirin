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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public abstract class NavigationMenuButtonBase : Button
    {
        static NavigationMenuButtonBase()
        {
            DataContextProperty.OverrideMetadata(
                typeof(NavigationMenuButtonBase),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnDataContextChanged)));
        }

        #region IsSelected

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(NavigationMenuButtonBase), new PropertyMetadata(false));

        #endregion

        #region BadgeType

        public BadgeType BadgeType
        {
            get { return (BadgeType)GetValue(BadgeTypeProperty); }
            set { SetValue(BadgeTypeProperty, value); }
        }
        public static readonly DependencyProperty BadgeTypeProperty =
            DependencyProperty.Register("BadgeType", typeof(BadgeType), typeof(NavigationMenuButtonBase));

        #endregion

        #region ShowBadge

        public bool ShowBadge
        {
            get { return (bool)GetValue(ShowBadgeProperty); }
            set { SetValue(ShowBadgeProperty, value); }
        }
        public static readonly DependencyProperty ShowBadgeProperty =
            DependencyProperty.Register("ShowBadge", typeof(bool), typeof(NavigationMenuButtonBase));

        #endregion

        #region Icon

        public UIKitIcon_24 Icon
        {
            get { return (UIKitIcon_24)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(UIKitIcon_24), typeof(NavigationMenuButtonBase));

        #endregion

        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NavigationMenuButtonBase)d).UpdateDataContextBindings();
        }

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
    }
}
