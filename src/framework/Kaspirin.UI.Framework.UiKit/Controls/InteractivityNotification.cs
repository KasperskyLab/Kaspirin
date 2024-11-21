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
    public sealed class InteractivityNotification : ContentControl
    {
        #region Type

        public InteractivityNotificationType Type
        {
            get { return (InteractivityNotificationType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(InteractivityNotificationType), typeof(InteractivityNotification), new PropertyMetadata(InteractivityNotificationType.Neutral));

        #endregion

        #region Icon

        public UIKitIcon_24 Icon
        {
            get { return (UIKitIcon_24)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(UIKitIcon_24), typeof(InteractivityNotification));

        #endregion

        #region ActionCaption

        public string? ActionCaption
        {
            get { return (string?)GetValue(ActionCaptionProperty); }
            set { SetValue(ActionCaptionProperty, value); }
        }

        public static readonly DependencyProperty ActionCaptionProperty = DependencyProperty.Register(
            "ActionCaption", typeof(string), typeof(InteractivityNotification));

        #endregion

        #region ActionCommand

        public ICommand ActionCommand
        {
            get { return (ICommand)GetValue(ActionCommandProperty); }
            set { SetValue(ActionCommandProperty, value); }
        }

        public static readonly DependencyProperty ActionCommandProperty = DependencyProperty.Register(
            "ActionCommand", typeof(ICommand), typeof(InteractivityNotification));

        #endregion

        #region HasCloseButton

        public bool HasCloseButton
        {
            get { return (bool)GetValue(HasCloseButtonProperty); }
            set { SetValue(HasCloseButtonProperty, value); }
        }

        public static readonly DependencyProperty HasCloseButtonProperty = DependencyProperty.Register(
            "HasCloseButton", typeof(bool), typeof(InteractivityNotification), new PropertyMetadata(true));

        #endregion

        #region CloseButtonCommand

        public ICommand CloseButtonCommand
        {
            get { return (ICommand)GetValue(CloseButtonCommandProperty); }
            set { SetValue(CloseButtonCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonCommandProperty = DependencyProperty.Register(
            "CloseButtonCommand", typeof(ICommand), typeof(InteractivityNotification));

        #endregion
    }
}
