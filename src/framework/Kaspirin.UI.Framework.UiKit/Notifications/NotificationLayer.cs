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
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Kaspirin.UI.Framework.UiKit.Notifications.Internals;

namespace Kaspirin.UI.Framework.UiKit.Notifications;

[TemplatePart(Name = PART_Content, Type = typeof(ContentPresenter))]
public sealed class NotificationLayer : ContentControl
{
    public const string PART_Content = "PART_Content";
    public const string DefaultLayerName = "RootLayer";

    public NotificationLayer()
    {
        _tracer = ComponentTracer.Get(UIKitComponentTracers.Notification, this);
        _adornerFactory = new NotificationAdornerFactory(this);
    }

    public override void OnApplyTemplate()
    {
        _content = Guard.EnsureIsInstanceOfType<ContentPresenter>(GetTemplateChild(PART_Content));
    }

    public NotificationView? TopmostNotification
    {
        get => _topmostNotification;
        internal set
        {
            if (_topmostNotification != value)
            {
                _topmostNotification = value;
                RaiseEvent(new RoutedEventArgs(TopmostNotificationChangedEvent));
            }
        }
    }

    public bool IsModalState
    {
        get => _isModalState;
        private set
        {
            if (_isModalState != value)
            {
                if (value)
                {
                    EnsureNotificationLayerIsLastInVisualTree();
                }

                _isModalState = value;
                RaiseEvent(new RoutedEventArgs(IsModalStateChangedEvent));
            }
        }
    }

    #region TopmostNotificationChanged Event

    public event RoutedEventHandler TopmostNotificationChanged
    {
        add => AddHandler(TopmostNotificationChangedEvent, value);
        remove => RemoveHandler(TopmostNotificationChangedEvent, value);
    }

    public static readonly RoutedEvent TopmostNotificationChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(TopmostNotificationChanged),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(NotificationLayer));

    #endregion

    #region IsModalStateChanged Event

    public event RoutedEventHandler IsModalStateChanged
    {
        add => AddHandler(IsModalStateChangedEvent, value);
        remove => RemoveHandler(IsModalStateChangedEvent, value);
    }

    public static readonly RoutedEvent IsModalStateChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(IsModalStateChanged),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(NotificationLayer));

    #endregion

    #region LayerName

    public string LayerName
    {
        get { return (string)GetValue(LayerNameProperty); }
        set { SetValue(LayerNameProperty, value); }
    }

    public static readonly DependencyProperty LayerNameProperty = DependencyProperty.Register(
        nameof(LayerName),
        typeof(string),
        typeof(NotificationLayer),
        new PropertyMetadata(DefaultLayerName));

    #endregion

    public static NotificationLayer? FindLayer(DependencyObject source, bool isModal, string name = DefaultLayerName)
    {
        var lookupOrigin = Guard.EnsureArgumentIsNotNull(source);
        if (lookupOrigin is Window window)
        {
            var windowContent = LogicalTreeHelper.GetChildren(window)
                .Cast<DependencyObject>()
                .FirstOrDefault();

            if (windowContent is null)
            {
                return isModal
                    ? lookupOrigin.FindVisualChildren<NotificationLayer>().FirstOrDefault()
                    : lookupOrigin.FindVisualChildren<NotificationLayer>().GuardedSingleOrDefault(l => l.LayerName == name);
            }

            lookupOrigin = windowContent;
        }

        return isModal
            ? lookupOrigin.FindVisualParents<NotificationLayer>().LastOrDefault()
            : lookupOrigin.FindVisualParents<NotificationLayer>().GuardedSingleOrDefault(l => l.LayerName == name);
    }

    public override string ToString()
    {
        return $"{nameof(NotificationLayer)} ({LayerName})";
    }

    internal void AddNotification(NotificationView notification, Action? onAdded = null, Action? onRemoved = null)
    {
        var onAddedCallback = () => notification.WhenLoaded(() => onAdded?.Invoke());
        var onRemovedCallback = () => notification.WhenUnloaded(() => onRemoved?.Invoke());

        var isKnownNotification = _visibleNotifications.Any(i => i.Notification == notification) ||
                                  _modalNotificationQueue.Any(i => i.Notification == notification) ||
                                  _modalNotificationStack.Any(i => i.Notification == notification);
        if (isKnownNotification)
        {
            _tracer.TraceWarning($"Notification is already on {this}. {notification}");
            return;
        }

        var notificationSettings = notification.DisplaySettings;

        var notificationAdorner = _adornerFactory.GetAdorner(notificationSettings);
        var notificationInfo = new NotificationViewInfo(notification, notificationAdorner, onAddedCallback, onRemovedCallback);

        if (notificationSettings.DisplayMode == NotificationDisplayMode.Modal)
        {
            var hasAnyModals = _visibleNotifications.Any(n => n.Notification.DisplaySettings.IsModal);
            if (hasAnyModals)
            {
                _modalNotificationQueue.Add(notificationInfo);

                _tracer.TraceInformation($"Notification added notification queue of {this}. {notification}");
                return;
            }
        }
        else if (notificationSettings.DisplayMode == NotificationDisplayMode.ModalTopmost)
        {
            _modalNotificationStack.Add(notificationInfo);
        }

        ProcessAddNotification(notificationInfo);
    }

    internal void RemoveNotification(NotificationView notification)
    {
        var notificationInfo = _visibleNotifications.SingleOrDefault(i => i.Notification == notification) ??
                               _modalNotificationQueue.SingleOrDefault(i => i.Notification == notification) ??
                               _modalNotificationStack.SingleOrDefault(i => i.Notification == notification);

        if (notificationInfo == null)
        {
            _tracer.TraceWarning($"Notification is not found in {this}. {notification}");
            return;
        }

        ProcessRemoveNotification(notificationInfo);
    }

    internal AdornerLayer GetAdornerLayer()
    {
        return AdornerLayer.GetAdornerLayer(_content);
    }

    internal UIElement GetContentElement()
    {
        return Guard.EnsureIsNotNull(_content);
    }

    private void ProcessAddNotification(NotificationViewInfo notificationInfo)
    {
        if (!_visibleNotifications.Contains(notificationInfo))
        {
            _visibleNotifications.Add(notificationInfo);

            var notification = notificationInfo.Notification;
            if (notification.DisplaySettings.IsModal)
            {
                SuppressBackwardInteraction();
            }

            notificationInfo.Adorner.AddNotification(notification);
            notificationInfo.OnAdded.Invoke();

            _tracer.TraceInformation($"Notification added on {this}. {notification}");
        }
    }

    private void ProcessRemoveNotification(NotificationViewInfo notificationInfo)
    {
        if (_modalNotificationQueue.Contains(notificationInfo))
        {
            _modalNotificationQueue.Remove(notificationInfo);
        }

        if (_modalNotificationStack.Contains(notificationInfo))
        {
            _modalNotificationStack.Remove(notificationInfo);
        }

        if (_visibleNotifications.Contains(notificationInfo))
        {
            _visibleNotifications.Remove(notificationInfo);

            var notification = notificationInfo.Notification;
            if (notification.DisplaySettings.IsModal)
            {
                RestoreBackwardInteraction();
            }

            notificationInfo.Adorner.RemoveNotification(notification);
            notificationInfo.OnRemoved.Invoke();

            if (_modalNotificationStack.None())
            {
                var nextModal = _modalNotificationQueue.FirstOrDefault();
                if (nextModal != null)
                {
                    _modalNotificationQueue.Remove(nextModal);

                    ProcessAddNotification(nextModal);
                }
            }

            _tracer.TraceInformation($"Notification removed from {this}. {notification}");
        }
    }

    private void EnsureNotificationLayerIsLastInVisualTree()
    {
        var parentLayers = this.FindVisualParents<NotificationLayer>();
        if (parentLayers.Any())
        {
            Guard.Fail("Modal state can be set only on last NotificationLayer in visual tree");
        }
    }

    private void SuppressBackwardInteraction()
    {
        var visibleModals = _visibleNotifications.Where(n => n.Notification.DisplaySettings.IsModal).ToArray();

        var backwardLayer = visibleModals.Length == 1
            ? (DependencyObject?)_content
            : (DependencyObject?)visibleModals[visibleModals.Length - 2].Notification;

        AccessKeyManager.AddAccessKeyPressedHandler(backwardLayer, HandleAccessKey);
        KeyboardNavigation.SetTabNavigation(backwardLayer, KeyboardNavigationMode.None);
        KeyboardNavigation.SetDirectionalNavigation(backwardLayer, KeyboardNavigationMode.None);

        var isFocusOnNotification = visibleModals.Last().Notification.FindVisualChildren<UIElement>().Any(o => o.IsFocused);
        if (isFocusOnNotification is false)
        {
            InputFocusManager.ClearInputFocus(this);
        }

        IsModalState = visibleModals.Length > 0;
    }

    private void RestoreBackwardInteraction()
    {
        var visibleModals = _visibleNotifications.Where(n => n.Notification.DisplaySettings.IsModal).ToArray();

        var backwardLayer = visibleModals.Length == 0
            ? (DependencyObject?)_content
            : (DependencyObject?)visibleModals.Last().Notification;

        AccessKeyManager.RemoveAccessKeyPressedHandler(backwardLayer, HandleAccessKey);
        KeyboardNavigation.SetTabNavigation(backwardLayer, KeyboardNavigationMode.Continue);
        KeyboardNavigation.SetDirectionalNavigation(backwardLayer, KeyboardNavigationMode.Continue);

        IsModalState = visibleModals.Length > 0;
    }

    private static void HandleAccessKey(object sender, AccessKeyPressedEventArgs e)
    {
        if (!Keyboard.IsKeyDown(Key.F1))
        {
            e.Scope = sender;
            e.Handled = true;
        }
    }

    private sealed class NotificationViewInfo
    {
        public NotificationViewInfo(NotificationView notification, INotificationAdorner adorner, Action onAdded, Action onRemoved)
        {
            Notification = notification;
            Adorner = adorner;
            OnAdded = onAdded;
            OnRemoved = onRemoved;
        }

        public INotificationAdorner Adorner { get; }
        public NotificationView Notification { get; }
        public Action OnAdded { get; }
        public Action OnRemoved { get; }
    }

    private readonly List<NotificationViewInfo> _visibleNotifications = new();
    private readonly List<NotificationViewInfo> _modalNotificationQueue = new();
    private readonly List<NotificationViewInfo> _modalNotificationStack = new();
    private readonly NotificationAdornerFactory _adornerFactory;
    private readonly ComponentTracer _tracer;

    private bool _isModalState;
    private ContentPresenter? _content;
    private NotificationView? _topmostNotification;
}
