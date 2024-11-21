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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public class BigButton : ButtonBase
    {
        #region IconLocation

        public ButtonIconLocation IconLocation
        {
            get { return (ButtonIconLocation)GetValue(IconLocationProperty); }
            set { SetValue(IconLocationProperty, value); }
        }

        public static readonly DependencyProperty IconLocationProperty =
            DependencyProperty.Register("IconLocation", typeof(ButtonIconLocation), typeof(BigButton),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(BigButton), nameof(IconLocationProperty), OnIconOrLocationChanged));

        #endregion

        #region Icon

        public UIKitIcon_24 Icon
        {
            get { return (UIKitIcon_24)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(UIKitIcon_24), typeof(BigButton),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(BigButton), nameof(IconProperty), OnIconOrLocationChanged));

        #endregion

        #region IsBusy

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BigButton),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(BigButton), nameof(IsBusyProperty), OnIsBusyChanged));

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ButtonBaseInternals.IsBusyProperty, e.NewValue);
        }

        #endregion

        private static void OnIconOrLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var icon = (UIKitIcon_24)d.GetValue(IconProperty);
            var location = (ButtonIconLocation)d.GetValue(IconLocationProperty);

            if (location == ButtonIconLocation.Left)
            {
                d.SetValue(ButtonBaseInternals.LeftIcon24Property, icon);
                d.SetValue(ButtonBaseInternals.RightIcon24Property, UIKitIcon_24.UIKitUnset);
            }
            else
            {
                d.SetValue(ButtonBaseInternals.LeftIcon24Property, UIKitIcon_24.UIKitUnset);
                d.SetValue(ButtonBaseInternals.RightIcon24Property, icon);
            }
        }
    }
}
