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

using System;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Notifications;

public static class NotificationLauncher
{
    public static NotificationView Create(
        FrameworkElement associatedObject,
        object? content = null,
        DataTemplate? contentTemplate = null,
        NotificationDisplaySettings? displaySettings = null,
        Action<NotificationView>? onClosed = null,
        Action<NotificationView>? onOpened = null)
    {
        Guard.AssertIsUiThread();
        Guard.ArgumentIsNotNull(associatedObject);

        if (content == null)
        {
            content = associatedObject.DataContext;
        }
        else if (content is FrameworkElement fe && fe.DataContext == null)
        {
            fe.DataContext = associatedObject.DataContext;
        }

        var view = new NotificationView(associatedObject, content, displaySettings)
        {
            ContentTemplate = contentTemplate,
        };

        if (onOpened != null)
        {
            view.Opened += (s, e) => onOpened.Invoke(view);
        }

        if (onClosed != null)
        {
            view.Closed += (s, e) => onClosed.Invoke(view);
        }

        return view;
    }

    public static NotificationView Show(
        FrameworkElement associatedObject,
        object? content = null,
        DataTemplate? contentTemplate = null,
        NotificationDisplaySettings? displaySettings = null,
        Action<NotificationView>? onClosed = null,
        Action<NotificationView>? onOpened = null)
    {
        var view = Create(
            associatedObject,
            content,
            contentTemplate,
            displaySettings,
            onClosed,
            onOpened);

        view.Show();

        return view;
    }
}