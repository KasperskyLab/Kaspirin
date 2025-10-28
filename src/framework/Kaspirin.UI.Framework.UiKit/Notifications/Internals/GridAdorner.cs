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

using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Notifications.Internals;

internal sealed class GridAdorner : NotificationAdorner<Grid>
{
    public GridAdorner(Grid panel, NotificationLayer notificationLayer)
        : base(panel, notificationLayer)
    {
    }

    protected override void AddNotification(Grid element, NotificationView view)
        => element.Children.Add(view);

    protected override void RemoveNotification(Grid element, NotificationView view)
        => element.Children.Remove(view);
}
