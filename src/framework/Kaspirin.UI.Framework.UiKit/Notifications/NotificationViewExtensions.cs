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

using System;
using Kaspirin.UI.Framework.Services.Internals;

namespace Kaspirin.UI.Framework.UiKit.Notifications
{
    public static class NotificationViewExtensions
    {
        public static void WhenOpened(this NotificationView notificationView, Action action)
        {
            EventSubscriber.OnceOrNow(
                source: notificationView,
                condition: notificationView => notificationView.IsOpened,
                subscribeCallback: eh => notificationView.Opened += eh,
                unsubscribeCallback: eh => notificationView.Opened -= eh,
                action: action);
        }

        public static void WhenClosed(this NotificationView notificationView, Action action)
        {
            EventSubscriber.OnceOrNow(
                source: notificationView,
                condition: notificationView => notificationView.IsClosed,
                subscribeCallback: eh => notificationView.Closed += eh,
                unsubscribeCallback: eh => notificationView.Closed -= eh,
                action: action);
        }
    }
}
