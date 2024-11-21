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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class InteractivityDialogFooter : Control
    {
        #region ReferenceButtonCommand

        public ICommand ReferenceButtonCommand
        {
            get => (ICommand)GetValue(ReferenceButtonCommandProperty);
            set => SetValue(ReferenceButtonCommandProperty, value);
        }

        public static readonly DependencyProperty ReferenceButtonCommandProperty = DependencyProperty.Register(
            nameof(ReferenceButtonCommand),
            typeof(ICommand),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region ReferenceButtonCaption

        public string ReferenceButtonCaption
        {
            get => (string)GetValue(ReferenceButtonCaptionProperty);
            set => SetValue(ReferenceButtonCaptionProperty, value);
        }

        public static readonly DependencyProperty ReferenceButtonCaptionProperty = DependencyProperty.Register(
            nameof(ReferenceButtonCaption),
            typeof(string),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(string)));

        #endregion

        #region ReferenceButtonStyle

        public Style ReferenceButtonStyle
        {
            get => (Style)GetValue(ReferenceButtonStyleProperty);
            set => SetValue(ReferenceButtonStyleProperty, value);
        }

        public static readonly DependencyProperty ReferenceButtonStyleProperty = DependencyProperty.Register(
            nameof(ReferenceButtonStyle),
            typeof(Style),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(Style)));

        #endregion

        #region PrimaryButtonCommand

        public ICommand PrimaryButtonCommand
        {
            get => (ICommand)GetValue(PrimaryButtonCommandProperty);
            set => SetValue(PrimaryButtonCommandProperty, value);
        }

        public static readonly DependencyProperty PrimaryButtonCommandProperty = DependencyProperty.Register(
            nameof(PrimaryButtonCommand),
            typeof(ICommand),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region PrimaryButtonCaption

        public string PrimaryButtonCaption
        {
            get => (string)GetValue(PrimaryButtonCaptionProperty);
            set => SetValue(PrimaryButtonCaptionProperty, value);
        }

        public static readonly DependencyProperty PrimaryButtonCaptionProperty = DependencyProperty.Register(
            nameof(PrimaryButtonCaption),
            typeof(string),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(string)));

        #endregion

        #region PrimaryButtonStyle

        public Style PrimaryButtonStyle
        {
            get => (Style)GetValue(PrimaryButtonStyleProperty);
            set => SetValue(PrimaryButtonStyleProperty, value);
        }

        public static readonly DependencyProperty PrimaryButtonStyleProperty = DependencyProperty.Register(
            nameof(PrimaryButtonStyle),
            typeof(Style),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(Style)));

        #endregion

        #region PrimaryButtonEnabled

        public bool PrimaryButtonEnabled
        {
            get => (bool)GetValue(PrimaryButtonEnabledProperty);
            set => SetValue(PrimaryButtonEnabledProperty, value);
        }

        public static readonly DependencyProperty PrimaryButtonEnabledProperty = DependencyProperty.Register(
            nameof(PrimaryButtonEnabled),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(true));

        #endregion

        #region PrimaryButtonIcon

        public UIKitIcon_16 PrimaryButtonIcon
        {
            get => (UIKitIcon_16)GetValue(PrimaryButtonIconProperty);
            set => SetValue(PrimaryButtonIconProperty, value);
        }

        public static readonly DependencyProperty PrimaryButtonIconProperty = DependencyProperty.Register(
            nameof(PrimaryButtonIcon),
            typeof(UIKitIcon_16),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(UIKitIcon_16)));

        #endregion

        #region PrimaryButtonIconLocation

        public ButtonIconLocation PrimaryButtonIconLocation
        {
            get => (ButtonIconLocation)GetValue(PrimaryButtonIconLocationProperty);
            set => SetValue(PrimaryButtonIconLocationProperty, value);
        }

        public static readonly DependencyProperty PrimaryButtonIconLocationProperty = DependencyProperty.Register(
            nameof(PrimaryButtonIconLocation),
            typeof(ButtonIconLocation),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(ButtonIconLocation)));

        #endregion

        #region SecondaryButtonStyle

        public Style SecondaryButtonStyle
        {
            get => (Style)GetValue(SecondaryButtonStyleProperty);
            set => SetValue(SecondaryButtonStyleProperty, value);
        }

        public static readonly DependencyProperty SecondaryButtonStyleProperty = DependencyProperty.Register(
            nameof(SecondaryButtonStyle),
            typeof(Style),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(Style)));

        #endregion

        #region SecondaryButtonCaption

        public string SecondaryButtonCaption
        {
            get => (string)GetValue(SecondaryButtonCaptionProperty);
            set => SetValue(SecondaryButtonCaptionProperty, value);
        }

        public static readonly DependencyProperty SecondaryButtonCaptionProperty = DependencyProperty.Register(
            nameof(SecondaryButtonCaption),
            typeof(string),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(string)));

        #endregion

        #region SecondaryButtonCommand

        public ICommand SecondaryButtonCommand
        {
            get => (ICommand)GetValue(SecondaryButtonCommandProperty);
            set => SetValue(SecondaryButtonCommandProperty, value);
        }

        public static readonly DependencyProperty SecondaryButtonCommandProperty = DependencyProperty.Register(
            nameof(SecondaryButtonCommand),
            typeof(ICommand),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region SecondaryButtonEnabled

        public bool SecondaryButtonEnabled
        {
            get => (bool)GetValue(SecondaryButtonEnabledProperty);
            set => SetValue(SecondaryButtonEnabledProperty, value);
        }

        public static readonly DependencyProperty SecondaryButtonEnabledProperty = DependencyProperty.Register(
            nameof(SecondaryButtonEnabled),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(true));

        #endregion

        #region SecondaryButtonIcon

        public UIKitIcon_16 SecondaryButtonIcon
        {
            get => (UIKitIcon_16)GetValue(SecondaryButtonIconProperty);
            set => SetValue(SecondaryButtonIconProperty, value);
        }

        public static readonly DependencyProperty SecondaryButtonIconProperty = DependencyProperty.Register(
            nameof(SecondaryButtonIcon),
            typeof(UIKitIcon_16),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(UIKitIcon_16)));

        #endregion

        #region SecondaryButtonIconLocation

        public ButtonIconLocation SecondaryButtonIconLocation
        {
            get => (ButtonIconLocation)GetValue(SecondaryButtonIconLocationProperty);
            set => SetValue(SecondaryButtonIconLocationProperty, value);
        }

        public static readonly DependencyProperty SecondaryButtonIconLocationProperty = DependencyProperty.Register(
            nameof(SecondaryButtonIconLocation),
            typeof(ButtonIconLocation),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(default(ButtonIconLocation)));

        #endregion

        #region IsPrimaryCloseButton

        public bool IsPrimaryCloseButton
        {
            get => (bool)GetValue(IsPrimaryCloseButtonProperty);
            set => SetValue(IsPrimaryCloseButtonProperty, value);
        }

        public static readonly DependencyProperty IsPrimaryCloseButtonProperty = DependencyProperty.Register(
            nameof(IsPrimaryCloseButton),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(true));

        #endregion

        #region IsPrimaryCancelButton

        public bool IsPrimaryCancelButton
        {
            get => (bool)GetValue(IsPrimaryCancelButtonProperty);
            set => SetValue(IsPrimaryCancelButtonProperty, value);
        }

        public static readonly DependencyProperty IsPrimaryCancelButtonProperty = DependencyProperty.Register(
            nameof(IsPrimaryCancelButton),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(false));

        #endregion

        #region IsPrimaryDefaultButton

        public bool IsPrimaryDefaultButton
        {
            get => (bool)GetValue(IsPrimaryDefaultButtonProperty);
            set => SetValue(IsPrimaryDefaultButtonProperty, value);
        }

        public static readonly DependencyProperty IsPrimaryDefaultButtonProperty = DependencyProperty.Register(
            nameof(IsPrimaryDefaultButton),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(true));

        #endregion

        #region IsSecondaryCloseButton

        public bool IsSecondaryCloseButton
        {
            get => (bool)GetValue(IsSecondaryCloseButtonProperty);
            set => SetValue(IsSecondaryCloseButtonProperty, value);
        }

        public static readonly DependencyProperty IsSecondaryCloseButtonProperty = DependencyProperty.Register(
            nameof(IsSecondaryCloseButton),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(true));

        #endregion

        #region IsSecondaryCancelButton

        public bool IsSecondaryCancelButton
        {
            get => (bool)GetValue(IsSecondaryCancelButtonProperty);
            set => SetValue(IsSecondaryCancelButtonProperty, value);
        }

        public static readonly DependencyProperty IsSecondaryCancelButtonProperty = DependencyProperty.Register(
            nameof(IsSecondaryCancelButton),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(true));

        #endregion

        #region IsSecondaryDefaultButton

        public bool IsSecondaryDefaultButton
        {
            get => (bool)GetValue(IsSecondaryDefaultButtonProperty);
            set => SetValue(IsSecondaryDefaultButtonProperty, value);
        }

        public static readonly DependencyProperty IsSecondaryDefaultButtonProperty = DependencyProperty.Register(
            nameof(IsSecondaryDefaultButton),
            typeof(bool),
            typeof(InteractivityDialogFooter),
            new PropertyMetadata(false));

        #endregion
    }
}
