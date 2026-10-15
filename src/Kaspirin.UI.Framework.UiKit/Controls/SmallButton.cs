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

using System.Windows;
using System.Windows.Controls;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class SmallButton : Button
{
    #region IconLocation

    public ButtonIconLocation IconLocation
    {
        get => (ButtonIconLocation)GetValue(IconLocationProperty);
        set => SetValue(IconLocationProperty, value);
    }

    public static readonly DependencyProperty IconLocationProperty = DependencyProperty.Register(
        nameof(IconLocation),
        typeof(ButtonIconLocation),
        typeof(SmallButton),
        UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(SmallButton), nameof(IconLocationProperty), OnIconOrLocationChanged));

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
        typeof(SmallButton),
        UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(SmallButton), nameof(IconProperty), OnIconOrLocationChanged));

    #endregion

    #region IsBusy

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
        nameof(IsBusy),
        typeof(bool),
        typeof(SmallButton),
        UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(SmallButton), nameof(IsBusyProperty), OnIsBusyChanged));

    private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => d.SetValue(ButtonBaseInternals.IsBusyProperty, e.NewValue);

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