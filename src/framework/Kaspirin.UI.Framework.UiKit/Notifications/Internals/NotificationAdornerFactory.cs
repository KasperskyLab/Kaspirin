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

namespace Kaspirin.UI.Framework.UiKit.Notifications.Internals;

internal sealed class NotificationAdornerFactory
{
    public NotificationAdornerFactory(NotificationLayer notificationLayer)
    {
        _notificationLayer = Guard.EnsureArgumentIsNotNull(notificationLayer);
    }

    public INotificationAdorner GetAdorner(NotificationDisplaySettings settings)
    {
        return settings.IsModal ? GetModalAdorner(settings) : GetNonModalAdorner(settings);
    }

    private INotificationAdorner GetModalAdorner(NotificationDisplaySettings settings)
    {
        return FindAdorner<GridAdorner>() ?? CreateModalAdorner(settings);
    }

    private INotificationAdorner GetNonModalAdorner(NotificationDisplaySettings settings)
    {
        return FindAdorner<ItemsControlAdorner>() ?? CreateNonModalAdorner(settings);
    }

    private GridAdorner CreateModalAdorner(NotificationDisplaySettings settings)
    {
        var panel = new Grid { Focusable = false };

        return new GridAdorner(panel, _notificationLayer);
    }

    private ItemsControlAdorner CreateNonModalAdorner(NotificationDisplaySettings settings)
    {
        var stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
        stackPanelFactory.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Bottom);

        var panel = new ItemsControl
        {
            Focusable = false,
            VerticalAlignment = VerticalAlignment.Bottom,
            ItemsPanel = new ItemsPanelTemplate()
            {
                VisualTree = stackPanelFactory,
            }
        };

        return new ItemsControlAdorner(panel, _notificationLayer);
    }

    private TNotificationAdorner? FindAdorner<TNotificationAdorner>()
        where TNotificationAdorner : INotificationAdorner
    {
        var notificationAdorner = _notificationLayer.GetAdornerLayer();
        var contentElement = _notificationLayer.GetContentElement();

        var adorners = notificationAdorner.GetAdorners(contentElement);
        if (adorners != null)
        {
            return adorners.OfType<TNotificationAdorner>().FirstOrDefault();
        }

        return default;
    }

    private readonly NotificationLayer _notificationLayer;
}
