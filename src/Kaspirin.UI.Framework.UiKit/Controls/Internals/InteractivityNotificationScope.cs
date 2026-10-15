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
using System.Collections.Generic;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal sealed class InteractivityNotificationScope : InteractivityScope<InteractivityNotification>
{
    public static void Register(InteractivityNotification notification, Action? continueCallback)
    {
        new InteractivityNotificationScope().Register(notification, notification.ScopeName, continueCallback);
    }

    public static void Unregister(InteractivityNotification notification, Action? continueCallback)
    {
        new InteractivityNotificationScope().Unregister(notification, notification.ScopeName, continueCallback);
    }

    protected override InteractivityScope<InteractivityNotification> CreateScopeInstance()
    {
        return new InteractivityNotificationScope();
    }

    protected override void OnRegister(InteractivityNotification notification, List<InteractivityNotification> scopedNotifications, Action? continueCallback)
    {
        notification.ShowContent(continueCallback);

        scopedNotifications.SkipLast(notification.ScopeMaxNotificationCount - 1).ForEach(n => n.Close(forced: true));
        scopedNotifications.Add(notification);
    }

    protected override void OnUnregister(InteractivityNotification notification, List<InteractivityNotification> scopedNotifications, Action? continueCallback)
    {
        notification.HideContent(continueCallback);

        scopedNotifications.Remove(notification);
    }
}
