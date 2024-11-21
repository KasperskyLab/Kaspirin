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

using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Notifications.Internals
{
    internal static class NotificationAdornerFactory
    {
        public static INotificationAdorner CreateContentAdorner(NotificationLayer notificationLayer)
        {
            Guard.ArgumentIsNotNull(notificationLayer);

            var panel = new ContentControl { Focusable = false };

            return new ContentControlAdorner(panel, notificationLayer);
        }

        public static INotificationAdorner FindOrCreateItemsAdorner(
            NotificationLayer notificationLayer,
            NotificationLocationSettings? locationSettings,
            int maxNotificationCount)
        {
            Guard.ArgumentIsNotNull(notificationLayer);

            return FindItemsAdorner(notificationLayer)
                ?? CreateItemsAdorner(notificationLayer, locationSettings, maxNotificationCount);
        }

        private static ItemsControlAdorner? FindItemsAdorner(NotificationLayer notificationLayer)
        {
            var notificationAdorner = notificationLayer.GetAdornerLayer();
            var contentElement = notificationLayer.GetContentElement();

            var adorners = notificationAdorner.GetAdorners(contentElement);
            if (adorners != null)
            {
                return adorners.OfType<ItemsControlAdorner>().FirstOrDefault();
            }

            return null;
        }

        private static ItemsControlAdorner CreateItemsAdorner(
            NotificationLayer notificationLayer,
            NotificationLocationSettings? locationSettings,
            int maxNotificationCount)
        {
            locationSettings ??= NotificationLocationSettings.Default;

            var stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(
                FrameworkElement.VerticalAlignmentProperty,
                locationSettings.ModallessViewVerticalAlignment);

            var panel = new ItemsControl
            {
                Focusable = false,
                VerticalAlignment = locationSettings.ModallessViewVerticalAlignment,
                ItemsPanel = new ItemsPanelTemplate()
                {
                    VisualTree = stackPanelFactory,
                }
            };

            return new ItemsControlAdorner(panel, notificationLayer, maxNotificationCount);
        }
    }
}
