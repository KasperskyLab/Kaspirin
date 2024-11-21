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

namespace Kaspirin.UI.Framework.UiKit.Notifications
{
    public static class NotificationViewBehavior
    {
        internal static void SetInteractionObject(this NotificationView notificationView, InteractionObject interactionObject)
        {
            Guard.ArgumentIsNotNull(notificationView);
            Guard.ArgumentIsNotNull(interactionObject);

            notificationView.SetValue(_interactionObjectProperty, interactionObject);
        }

        internal static InteractionObject? GetInteractionObject(this NotificationView notificationView)
        {
            Guard.ArgumentIsNotNull(notificationView);

            return notificationView.GetValue(_interactionObjectProperty) as InteractionObject;
        }

        #region CloseFlag

        public static bool GetCloseFlag(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseFlagProperty);
        }

        public static void SetCloseFlag(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseFlagProperty, value);
        }

        public static readonly DependencyProperty CloseFlagProperty =
            DependencyProperty.RegisterAttached("CloseFlag", typeof(bool), typeof(NotificationViewBehavior), new PropertyMetadata(false, CloseOnFlagChagned));

        #endregion

        #region IsCloseButton

        public static bool GetIsCloseButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseButtonProperty);
        }

        public static void SetIsCloseButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseButtonProperty, value);
        }

        public static readonly DependencyProperty IsCloseButtonProperty = DependencyProperty.RegisterAttached(
            "IsCloseButton", typeof(bool), typeof(NotificationViewBehavior), new PropertyMetadata(false, OnIsCloseButtonChanged));

        private static void OnIsCloseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (button != null)
            {
                if ((bool)e.NewValue)
                {
                    button.Click += CloseOnClick;
                }
                else
                {
                    button.Click -= CloseOnClick;
                }
            }
        }

        #endregion

        #region IsConfirmButton

        public static bool GetIsConfirmButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsConfirmButtonProperty);
        }

        public static void SetIsConfirmButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsConfirmButtonProperty, value);
        }

        public static readonly DependencyProperty IsConfirmButtonProperty = DependencyProperty.RegisterAttached(
            "IsConfirmButton", typeof(bool), typeof(NotificationViewBehavior), new PropertyMetadata(false, OnIsConfirmButtonChanged));

        private static void OnIsConfirmButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (button != null)
            {
                if ((bool)e.NewValue)
                {
                    button.Click += CloseAndConfirmOnClick;
                }
                else
                {
                    button.Click -= CloseAndConfirmOnClick;
                }
            }
        }

        #endregion

        private static void CloseAndConfirmOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            CloseNotification((DependencyObject)sender, confirm: true);
        }

        private static void CloseOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            CloseNotification((DependencyObject)sender, confirm: false);
        }

        private static void CloseOnFlagChagned(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is true)
            {
                CloseNotification(d, confirm: false);
            }
        }

        public static void CloseNotification(DependencyObject dependencyObject, bool confirm = false)
        {
            var view = dependencyObject.FindVisualParent<NotificationView>();
            if (view == null)
            {
                return;
            }

            if (confirm && view.GetInteractionObject() is ConfirmationObject confirmation)
            {
                confirmation.IsConfirmed = true;
            }

            view.CloseSmooth();
        }

        private static readonly DependencyProperty _interactionObjectProperty = DependencyProperty.RegisterAttached(
            "InteractionObject", typeof(InteractionObject), typeof(NotificationViewBehavior));
    }
}
