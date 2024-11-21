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
    internal sealed class ContentControlAdorner : NotificationAdorner<ContentControl>
    {
        public ContentControlAdorner(ContentControl panel, NotificationLayer notificationLayer)
            : base(panel, notificationLayer)
        {
        }

        protected override void AddNotification(ContentControl element, NotificationView view, out bool canShowAdorner)
        {
            element.Content = view;

            canShowAdorner = true;
        }

        protected override void RemoveNotification(ContentControl element, NotificationView view, out bool canCloseAdorner)
        {
            element.Content = null;

            canCloseAdorner = true;
        }
    }
}
