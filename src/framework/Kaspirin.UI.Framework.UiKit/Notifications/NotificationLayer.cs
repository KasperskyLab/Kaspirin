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

namespace Kaspirin.UI.Framework.UiKit.Notifications
{
    public sealed class NotificationLayer : ContentControl
    {
        public override void OnApplyTemplate()
        {
            _content = (ContentPresenter)GetTemplateChild("PART_Content");
        }

        public bool IsModalState
        {
            get { return _isModalState; }
            internal set
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

        #region IsModalStateChanged Event

        public static readonly RoutedEvent IsModalStateChangedEvent = EventManager.RegisterRoutedEvent(
            "IsModalStateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationLayer));

        public event RoutedEventHandler IsModalStateChanged
        {
            add { AddHandler(IsModalStateChangedEvent, value); }
            remove { RemoveHandler(IsModalStateChangedEvent, value); }
        }

        #endregion

        #region LayerName

        public string? LayerName
        {
            get { return (string?)GetValue(LayerNameProperty); }
            set { SetValue(LayerNameProperty, value); }
        }

        public static readonly DependencyProperty LayerNameProperty = DependencyProperty.Register(
            "LayerName", typeof(string), typeof(NotificationLayer));

        #endregion

        public static NotificationLayer? FindLayer(DependencyObject source, bool isModal, string? name = null)
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

        internal void AddNotification(NotificationView notification, Action? onAdded = null, Action? onRemoved = null)
        {
            var isKnownNotification = _visibleNotifications.Any(i => i.Notification == notification) ||
                                      _queuedModalNotifications.Any(i => i.Notification == notification);
            if (isKnownNotification)
            {
                _trace.TraceWarning($"Notification {notification} is already on NotificationLayer.");
                return;
            }

            var notificationInfo = new NotificationViewInfo(notification, GetNotificationAdorner(notification), onAdded, onRemoved);
            if (notificationInfo.Notification.IsModal)
            {
                var modalNotification = _visibleNotifications.GuardedSingleOrDefault(i => i.Notification.IsModal);
                if (modalNotification != null)
                {
                    _queuedModalNotifications.Add(notificationInfo);

                    _trace.TraceWarning($"Another modal notification {modalNotification.Notification} with " +
                                        $"state {modalNotification.Notification.State} is already on NotificationLayer.");
                    return;
                }
            }

            ProcessAddNotification(notificationInfo);
        }

        internal void RemoveNotification(NotificationView notification)
        {
            var notificationInfo = _visibleNotifications.SingleOrDefault(i => i.Notification == notification) ??
                                   _queuedModalNotifications.SingleOrDefault(i => i.Notification == notification);

            if (notificationInfo == null)
            {
                _trace.TraceWarning($"Notification {notification} is not found in NotificationLayer.");
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

        private INotificationAdorner GetNotificationAdorner(NotificationView notification)
            => notification.IsModal
                ? NotificationAdornerFactory.CreateContentAdorner(this)
                : NotificationAdornerFactory.FindOrCreateItemsAdorner(this, notification.LocationSettings, notification.MaxNotificationCount);

        private void ProcessAddNotification(NotificationViewInfo notificationInfo)
        {
            var notification = notificationInfo.Notification;

            if (notification.IsModal)
            {
                SuppressBackwardInteraction(notification);
            }

            _visibleNotifications.Add(notificationInfo);

            notificationInfo.Adorner.AddNotification(notification);
            notificationInfo.OnAdded?.Invoke();
        }

        private void ProcessRemoveNotification(NotificationViewInfo notificationInfo)
        {
            var notification = notificationInfo.Notification;

            if (_queuedModalNotifications.Contains(notificationInfo))
            {
                _queuedModalNotifications.Remove(notificationInfo);
            }

            if (_visibleNotifications.Contains(notificationInfo))
            {
                _visibleNotifications.Remove(notificationInfo);

                if (notification.IsModal)
                {
                    RestoreBackwardInteraction();
                }

                notificationInfo.Adorner.RemoveNotification(notification);
                notificationInfo.OnRemoved?.Invoke();

                var nextModal = _queuedModalNotifications.FirstOrDefault();
                if (nextModal != null)
                {
                    _queuedModalNotifications.Remove(nextModal);

                    ProcessAddNotification(nextModal);
                }
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

        private void SuppressBackwardInteraction(NotificationView notification)
        {
            AccessKeyManager.AddAccessKeyPressedHandler(_content, HandleAccessKey);
            KeyboardNavigation.SetTabNavigation(_content, KeyboardNavigationMode.None);
            KeyboardNavigation.SetDirectionalNavigation(_content, KeyboardNavigationMode.None);

            var isFocusOnNotification = notification.FindVisualChildren<UIElement>().Any(o => o.IsFocused);
            if (isFocusOnNotification is false)
            {
                InputFocusManager.ClearInputFocus(this);
            }

            IsModalState = true;
        }

        private void RestoreBackwardInteraction()
        {
            AccessKeyManager.RemoveAccessKeyPressedHandler(_content, HandleAccessKey);
            KeyboardNavigation.SetTabNavigation(_content, KeyboardNavigationMode.Continue);
            KeyboardNavigation.SetDirectionalNavigation(_content, KeyboardNavigationMode.Continue);

            IsModalState = false;
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
            public NotificationViewInfo(NotificationView notification, INotificationAdorner adorner, Action? onAdded, Action? onRemoved)
            {
                Notification = notification;
                Adorner = adorner;
                OnAdded = onAdded;
                OnRemoved = onRemoved;
            }

            public INotificationAdorner Adorner { get; }
            public NotificationView Notification { get; }
            public Action? OnAdded { get; }
            public Action? OnRemoved { get; }
        }

        private readonly List<NotificationViewInfo> _visibleNotifications = new();
        private readonly List<NotificationViewInfo> _queuedModalNotifications = new();

        private bool _isModalState;
        private ContentPresenter? _content;

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Notification);
    }
}
