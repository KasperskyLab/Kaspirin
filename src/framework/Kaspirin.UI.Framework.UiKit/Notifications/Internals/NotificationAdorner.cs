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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Notifications.Internals;

internal abstract class NotificationAdorner<TElement> : Adorner, INotificationAdorner
    where TElement : FrameworkElement
{
    public NotificationAdorner(TElement element, NotificationLayer notificationLayer)
        : base(Guard.EnsureArgumentIsNotNull(notificationLayer).GetContentElement())
    {
        _element = Guard.EnsureArgumentIsNotNull(element);
        _notificationLayer = Guard.EnsureArgumentIsNotNull(notificationLayer);

        _visualCollection = new VisualCollection(this) { _element };
        _notifications = new();

        _element.SetBinding(WidthProperty, new Binding
        {
            Source = _notificationLayer,
            Mode = BindingMode.OneWay,
            Path = ActualWidthProperty.AsPath()
        });
        _element.SetBinding(HeightProperty, new Binding
        {
            Source = _notificationLayer,
            Mode = BindingMode.OneWay,
            Path = ActualHeightProperty.AsPath()
        });
        Unloaded += (sender, args) =>
        {
            _visualCollection.Clear();
            _element = null;
        };
    }

    public void AddNotification(NotificationView view)
    {
        if (_element != null)
        {
            _notifications.Add(view);
            _notificationLayer.TopmostNotification = _notifications.LastOrDefault(n => n.DisplaySettings.IsModal);

            AddNotification(_element, view);

            if (_notifications.Count == 1)
            {
                ShowAdorner();
            }
        }
    }

    public void RemoveNotification(NotificationView view)
    {
        if (_element != null)
        {
            _notifications.Remove(view);
            _notificationLayer.TopmostNotification = _notifications.LastOrDefault(n => n.DisplaySettings.IsModal);

            RemoveNotification(_element, view);

            if (_notifications.Count == 0)
            {
                CloseAdorner();
            }
        }
    }

    protected abstract void AddNotification(TElement element, NotificationView view);

    protected abstract void RemoveNotification(TElement element, NotificationView view);

    protected override Size MeasureOverride(Size constraint)
    {
        var baseResult = base.MeasureOverride(constraint);
        if (_element == null)
        {
            return baseResult;
        }

        _element.Measure(constraint);
        return _element.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var baseResult = base.ArrangeOverride(finalSize);
        if (_element == null)
        {
            return baseResult;
        }

        _element.Arrange(new Rect(finalSize));
        return finalSize;
    }

    protected override Visual GetVisualChild(int index)
    {
        return _visualCollection[index];
    }

    protected override int VisualChildrenCount => _visualCollection.Count;

    private void CloseAdorner()
    {
        GetAdornerLayer().Remove(this);
    }

    private void ShowAdorner()
    {
        GetAdornerLayer().Add(this);
    }

    private AdornerLayer GetAdornerLayer()
        => _notificationLayer.GetAdornerLayer()
            ?? throw new InvalidOperationException($"Unable to provide {nameof(AdornerLayer)} for element");

    private TElement? _element;

    private readonly VisualCollection _visualCollection;
    private readonly NotificationLayer _notificationLayer;
    private readonly List<NotificationView> _notifications;
}
