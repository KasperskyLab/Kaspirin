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
using System.Windows.Controls.Primitives;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Properties
{
    public static class ToggleButtonProps
    {
        #region IconLocation

        public static ButtonIconLocation GetIconLocation(DependencyObject obj)
        {
            return (ButtonIconLocation)obj.GetValue(IconLocationProperty);
        }

        public static void SetIconLocation(DependencyObject obj, ButtonIconLocation value)
        {
            obj.SetValue(IconLocationProperty, value);
        }

        public static readonly DependencyProperty IconLocationProperty =
            DependencyProperty.RegisterAttached("IconLocation", typeof(ButtonIconLocation), typeof(ToggleButtonProps),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ToggleButton), nameof(IconLocationProperty), OnIconOrLocationChanged));

        #endregion

        #region Icon

        public static UIKitIcon_16 GetIcon(DependencyObject obj)
        {
            return (UIKitIcon_16)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, UIKitIcon_16 value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached("Icon", typeof(UIKitIcon_16), typeof(ToggleButtonProps),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ToggleButton), nameof(IconProperty), OnIconOrLocationChanged));

        #endregion

        private static void OnIconOrLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var icon = (UIKitIcon_16)d.GetValue(IconProperty);
            var location = (ButtonIconLocation)d.GetValue(IconLocationProperty);

            if (location == ButtonIconLocation.Left)
            {
                d.SetValue(ButtonBaseInternals.LeftIcon16Property, icon);
                d.SetValue(ButtonBaseInternals.RightIcon16Property, UIKitIcon_16.UIKitUnset);
            }
            else
            {
                d.SetValue(ButtonBaseInternals.LeftIcon16Property, UIKitIcon_16.UIKitUnset);
                d.SetValue(ButtonBaseInternals.RightIcon16Property, icon);
            }
        }
    }
}
