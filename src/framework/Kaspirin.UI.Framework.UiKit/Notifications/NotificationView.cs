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
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Notifications
{
    public sealed class NotificationView : ContentControl
    {
        internal NotificationView(
            FrameworkElement associatedObject,
            object content,
            NotificationLocationSettings? locationSettings = null,
            bool isModal = true,
            int maxNotificationCount = default)
        {
            Guard.ArgumentIsNotNull(associatedObject);
            Guard.ArgumentIsNotNull(content);

            _deferredClose = new DeferredAction(CloseSmooth);
            _associatedObjectType = associatedObject.GetType().Name;
            _contentType = content.GetType().Name;

            AssociatedObject = associatedObject;
            Content = content;
            IsModal = isModal;
            MaxNotificationCount = isModal || maxNotificationCount <= 0 ? 1 : maxNotificationCount;
            Opacity = 0;
            LocationSettings = locationSettings;
            State = NotificationViewState.Initial;

            Loaded += (sender, args) =>
            {
                var backgroundPresenter = GetTemplateChild("PART_BackgroundPresenter") as Border;
                if (backgroundPresenter != null)
                {
                    backgroundPresenter.Background = IsModal ? Brushes.Transparent : null;
                    backgroundPresenter.SetBinding(ClipProperty, new Binding() { Source = this, Path = BackgroundClipProperty.AsPath() });
                }

                if (AutoCloseTimeout != TimeSpan.Zero && !IsModal)
                {
                    Opened += (o, eventArgs) =>
                    {
                        if (!IsMouseOver)
                        {
                            StartAutoClose();
                        }
                    };
                    MouseLeave += (o, eventArgs) => StartAutoClose();
                    MouseEnter += (o, eventArgs) => StopAutoClose();
                }

                _tracer.TraceInformation($"Notification {this} loaded");
            };

            Unloaded += (sender, args) =>
            {
                Content = null;
                ContentTemplate = null;
            };

            Opening += (sender, args) =>
            {
                NotificationLayer = GetNotificationLayer();
                NotificationLayer.AddNotification(this, onAdded: LaunchOpeningAnimation);

                _tracer.TraceInformation($"Notification {this} opening");
            };

            Opened += (sender, args) =>
            {
                _tracer.TraceInformation($"Notification {this} opened");
            };

            Closing += (sender, args) =>
            {
                LaunchClosingAnimation();

                _tracer.TraceInformation($"Notification {this} closing");
            };

            Closed += (sender, args) =>
            {
                NotificationLayer?.RemoveNotification(this);
                NotificationLayer = null;

                _tracer.TraceInformation($"Notification {this} closed");
            };
        }

        #region BackgroundClip

        public Geometry? BackgroundClip
        {
            get { return (Geometry)GetValue(BackgroundClipProperty); }
            set { SetValue(BackgroundClipProperty, value); }
        }

        public static readonly DependencyProperty BackgroundClipProperty =
            DependencyProperty.Register("BackgroundClip", typeof(Geometry), typeof(NotificationView));

        #endregion

        #region AnimationDuration

        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            "AnimationDuration", typeof(TimeSpan), typeof(NotificationView), new PropertyMetadata(TimeSpan.FromMilliseconds(200)));

        #endregion

        #region AutoCloseTimeout

        public TimeSpan AutoCloseTimeout
        {
            get { return (TimeSpan)GetValue(AutoCloseTimeoutProperty); }
            set { SetValue(AutoCloseTimeoutProperty, value); }
        }

        public static readonly DependencyProperty AutoCloseTimeoutProperty = DependencyProperty.Register(
            "AutoCloseTimeout", typeof(TimeSpan), typeof(NotificationView), new PropertyMetadata(TimeSpan.Zero));

        #endregion

        #region Closed Event

        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
            "Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationView));

        #endregion

        #region Closing Event

        public event RoutedEventHandler Closing
        {
            add { AddHandler(ClosingEvent, value); }
            remove { RemoveHandler(ClosingEvent, value); }
        }

        public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent(
            "Closing", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationView));

        #endregion

        #region Opened Event

        public event RoutedEventHandler Opened
        {
            add { AddHandler(OpenedEvent, value); }
            remove { RemoveHandler(OpenedEvent, value); }
        }

        public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent(
            "Opened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationView));

        #endregion

        #region Opening Event

        public event RoutedEventHandler Opening
        {
            add { AddHandler(OpeningEvent, value); }
            remove { RemoveHandler(OpeningEvent, value); }
        }

        public static readonly RoutedEvent OpeningEvent = EventManager.RegisterRoutedEvent(
            "Opening", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationView));

        #endregion

        public FrameworkElement AssociatedObject { get; }

        public bool IsModal { get; }

        public string? NotificationLayerName { get; set; }

        public int MaxNotificationCount { get; }

        public NotificationLayer? NotificationLayer { get; private set; }

        public NotificationLocationSettings? LocationSettings { get; }

        public NotificationViewState State
        {
            get => _state;
            private set
            {
                if (_state != value)
                {
                    _tracer.TraceInformation($"State changed from {_state} to {value} in notification {this}");
                    _state = value;
                }
            }
        }

        public bool IsOpening => State == NotificationViewState.Opening;

        public bool IsOpened => State == NotificationViewState.Opened;

        public bool IsClosing => State == NotificationViewState.Closing;

        public bool IsClosed => State == NotificationViewState.Closed;

        public override string ToString() => $"{_contentType} for {_associatedObjectType}";

        public void Show()
        {
            AssociatedObject.WhenLoaded(() =>
            {
                if (State.NotIn(NotificationViewState.Initial))
                {
                    TraceMethodSkip();
                    return;
                }

                if (EnsureCanShow())
                {
                    State = NotificationViewState.Opening;

                    RaiseEvent(new RoutedEventArgs(OpeningEvent));
                }
                else
                {
                    State = NotificationViewState.Error;
                }
            });
        }

        public void CloseSmooth()
        {
            if (State.In(NotificationViewState.Closing,
                         NotificationViewState.Closed))
            {
                TraceMethodSkip();
                return;
            }

            State = NotificationViewState.Closing;

            RaiseEvent(new RoutedEventArgs(ClosingEvent));
        }

        public void CloseForced()
        {
            if (State.In(NotificationViewState.Closed))
            {
                TraceMethodSkip();
                return;
            }

            State = NotificationViewState.Closed;

            RaiseEvent(new RoutedEventArgs(ClosedEvent));
        }

        private void ClosingCompleted(object? sender, EventArgs e)
        {
            if (State.In(NotificationViewState.Closed))
            {
                TraceMethodSkip();
                return;
            }

            State = NotificationViewState.Closed;

            RaiseEvent(new RoutedEventArgs(ClosedEvent));
        }

        private void OpeningCompleted(object? sender, EventArgs e)
        {
            if (State.NotIn(NotificationViewState.Opening))
            {
                TraceMethodSkip();
                return;
            }

            State = NotificationViewState.Opened;

            RaiseEvent(new RoutedEventArgs(OpenedEvent));
        }

        private void LaunchOpeningAnimation()
        {
            LaunchOpacityAnimation(1, OpeningCompleted);
        }

        private void LaunchClosingAnimation()
        {
            LaunchOpacityAnimation(0, ClosingCompleted);
        }

        private void LaunchOpacityAnimation(double newOpacity, EventHandler onCompletedAction)
        {
            _storyboard.Remove();
            _storyboard.Children.Clear();

            if (Opacity.NotNearlyEqual(newOpacity))
            {
                var animation = new DoubleAnimation
                {
                    To = newOpacity,
                    FillBehavior = FillBehavior.HoldEnd,
                    Duration = AnimationDuration.CoerceDuration()
                };

                animation.Completed += onCompletedAction;
                animation.SetValue(Storyboard.TargetProperty, this);
                animation.SetValue(Storyboard.TargetPropertyProperty, _opacityPropertyPath);
                animation.Freeze();

                _storyboard.Children.Add(animation);
                _storyboard.Begin();
            }
            else
            {
                Executers.InUiAsync(() => onCompletedAction.Invoke(this, EventArgs.Empty));
            }
        }

        private void StartAutoClose()
        {
            _deferredClose.Execute(AutoCloseTimeout);
        }

        private void StopAutoClose()
        {
            _deferredClose.Cancel();
        }

        private bool EnsureCanShow()
        {
            var layerDescription = string.IsNullOrEmpty(NotificationLayerName)
                ? nameof(NotificationLayer)
                : $"{nameof(NotificationLayer)} with name '{NotificationLayerName}'";

            var notificationLayer = TryGetNotificationLayer();
            if (notificationLayer == null)
            {
#if DEBUG
                Guard.Fail($"Failed to show notification {this}. Unable to provide {layerDescription}.");
#else
                _tracer.TraceError($"Failed to show notification {this}. Unable to provide {layerDescription}.");
#endif
                return false;
            }

            return true;
        }

        private NotificationLayer? TryGetNotificationLayer()
        {
            return NotificationLayer.FindLayer(AssociatedObject, IsModal, NotificationLayerName);
        }

        private NotificationLayer GetNotificationLayer()
        {
            return Guard.EnsureIsNotNull(TryGetNotificationLayer());
        }

        private void TraceMethodSkip([CallerMemberName] string? methodName = default)
        {
            _tracer.TraceInformation($"Skip '{methodName}' execution. Notification {this} is in state: {State}");
        }

        private NotificationViewState _state;

        private readonly DeferredAction _deferredClose;
        private readonly string _contentType;
        private readonly string _associatedObjectType;
        private readonly Storyboard _storyboard = new();

        private static readonly PropertyPath _opacityPropertyPath = new(OpacityProperty);
        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Notification);
    }
}