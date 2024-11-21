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

using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Notifications.Internals
{
    internal sealed class ItemsControlAdorner : NotificationAdorner<ItemsControl>
    {
        public ItemsControlAdorner(ItemsControl panel, NotificationLayer notificationLayer, int maxNotificationCount)
            : base(panel, notificationLayer)
        {
            _maxNotificationCount = maxNotificationCount;
        }

        protected override void AddNotification(ItemsControl element, NotificationView view, out bool canShowAdorner)
        {
            if (element.Items.Count >= _maxNotificationCount)
            {
                if (element.Items[0] is NotificationView notificationView)
                {
                    notificationView.CloseForced();
                }
            }

            element.Items.Add(view);

            canShowAdorner = element.Items.Count == 1;
        }

        protected override void RemoveNotification(ItemsControl element, NotificationView view, out bool canCloseAdorner)
        {
            element.Items.Remove(view);

            canCloseAdorner = element.Items.Count == 0;
        }

        private readonly int _maxNotificationCount;
    }
}
