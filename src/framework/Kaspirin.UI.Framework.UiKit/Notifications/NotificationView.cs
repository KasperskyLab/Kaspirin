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

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Notifications;

[TemplatePart(Name = PART_ContentPresenter, Type = typeof(ContentPresenter))]
[TemplatePart(Name = PART_BackgroundPresenter, Type = typeof(Border))]
public sealed class NotificationView : ContentControl
{
    public const string PART_ContentPresenter = "PART_ContentPresenter";
    public const string PART_BackgroundPresenter = "PART_BackgroundPresenter";

    internal NotificationView(
        FrameworkElement associatedObject,
        object content,
        NotificationDisplaySettings? displaySettings = null)
    {
        Guard.ArgumentIsNotNull(associatedObject);
        Guard.ArgumentIsNotNull(content);

        _stateStack = new();
        _tracer = ComponentTracer.Get(UIKitComponentTracers.Notification, this);
        _associatedObjectType = associatedObject.GetType().Name;
        _contentType = content.GetType().Name;
        _statisticsSender = ServiceLocator.GetService<IStatisticsSender>();

        AssociatedObject = associatedObject;
        Content = content;
        DisplaySettings = displaySettings ?? new();
        State = NotificationViewState.Initial;

        DispositionContext.Initialize(this);

        Loaded += (sender, args) =>
        {
            var backgroundPresenter = GetTemplateChild(PART_BackgroundPresenter) as Border;
            if (backgroundPresenter != null)
            {
                backgroundPresenter.Background = DisplaySettings.IsModal ? Brushes.Transparent : null;
                backgroundPresenter.SetBinding(ClipProperty, new Binding() { Source = this, Path = BackgroundClipProperty.AsPath() });
            }

            _tracer.TraceInformation($"Notification loaded. {this}");
        };

        Unloaded += (sender, args) =>
        {
            _tracer.TraceInformation($"Notification unloaded. {this}");
        };

        Pending += (sender, args) =>
        {
            NotificationLayer = GetNotificationLayer();
            NotificationLayer.AddNotification(this, onAdded: PendingCompleted);
        };

        Opening += (sender, args) =>
        {
            LaunchOpeningAnimation();
        };

        Opened += (sender, args) =>
        {
            _statisticsSender.SendNotificationViewShown(this);
        };

        Closing += (sender, args) =>
        {
            LaunchClosingAnimation();
        };

        Closed += (sender, args) =>
        {
            NotificationLayer?.RemoveNotification(this);
            NotificationLayer = null;

            DispositionContext.Dispose(this);
        };
    }

    #region BackgroundClip

    public Geometry? BackgroundClip
    {
        get => (Geometry)GetValue(BackgroundClipProperty);
        set => SetValue(BackgroundClipProperty, value);
    }

    public static readonly DependencyProperty BackgroundClipProperty = DependencyProperty.Register(
        nameof(BackgroundClip),
        typeof(Geometry),
        typeof(NotificationView),
        new PropertyMetadata(default(Geometry)));

    #endregion

    #region Closed Event

    public event RoutedEventHandler Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        nameof(Closed),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(NotificationView));

    #endregion

    #region Closing Event

    public event RoutedEventHandler Closing
    {
        add => AddHandler(ClosingEvent, value);
        remove => RemoveHandler(ClosingEvent, value);
    }

    public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent(
        nameof(Closing),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(NotificationView));

    #endregion

    #region Opened Event

    public event RoutedEventHandler Opened
    {
        add => AddHandler(OpenedEvent, value);
        remove => RemoveHandler(OpenedEvent, value);
    }

    public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent(
        nameof(Opened),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(NotificationView));

    #endregion

    #region Opening Event

    public event RoutedEventHandler Opening
    {
        add => AddHandler(OpeningEvent, value);
        remove => RemoveHandler(OpeningEvent, value);
    }

    public static readonly RoutedEvent OpeningEvent = EventManager.RegisterRoutedEvent(
        nameof(Opening),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(NotificationView));

    #endregion

    #region Pending Event

    public event RoutedEventHandler Pending
    {
        add => AddHandler(PendingEvent, value);
        remove => RemoveHandler(PendingEvent, value);
    }

    public static readonly RoutedEvent PendingEvent = EventManager.RegisterRoutedEvent(
        nameof(Pending),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(NotificationView));

    #endregion

    public FrameworkElement? ContentView => GetContentView();

    public FrameworkElement AssociatedObject { get; }

    public NotificationLayer? NotificationLayer { get; private set; }

    public NotificationDisplaySettings DisplaySettings { get; }

    public NotificationViewState State
    {
        get => _state;
        private set
        {
            if (_state != value)
            {
                _tracer.TraceInformation($"State changed from {_state} to {value}. {this}");
                _stateStack.Push(value);
                _state = value;
            }
        }
    }

    public bool IsOpening => State == NotificationViewState.Opening;

    public bool IsOpened => State == NotificationViewState.Opened;

    public bool IsClosing => State == NotificationViewState.Closing;

    public bool IsClosed => State == NotificationViewState.Closed;

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"{nameof(NotificationView)} [");

        if (CheckAccess())
        {
            sb.Append($"ContentView:{ContentView?.GetType().Name ?? "<null>"}; ");
        }
        else
        {
            sb.Append(base.ToString());
        }

        sb.Append($"ContentType:{_contentType}; ");
        sb.Append($"AssociatedObjectType:{_associatedObjectType}");
        sb.Append(']');

        return sb.ToString();
    }

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
                State = NotificationViewState.Pending;

                RaiseEvent(new RoutedEventArgs(PendingEvent));
            }
            else
            {
                State = NotificationViewState.Error;
            }
        });
    }

    public void Close()
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

    internal void CloseForced()
    {
        if (State.In(NotificationViewState.Closed))
        {
            TraceMethodSkip();
            return;
        }

        State = NotificationViewState.Closed;

        RaiseEvent(new RoutedEventArgs(ClosedEvent));
    }

    private FrameworkElement? GetContentView()
    {
        var contentPresenter = GetTemplateChild("PART_ContentPresenter") as ContentPresenter;
        if (contentPresenter != null)
        {
            var hasChildren = VisualTreeHelper.GetChildrenCount(contentPresenter) > 0;
            if (hasChildren)
            {
                return VisualTreeHelper.GetChild(contentPresenter, 0) as FrameworkElement;
            }
        }

        return null;
    }

    private void PendingCompleted()
    {
        if (State.NotIn(NotificationViewState.Pending))
        {
            TraceMethodSkip();
            return;
        }

        State = NotificationViewState.Opening;

        RaiseEvent(new RoutedEventArgs(OpeningEvent));
    }

    private void ClosingCompleted()
    {
        if (State.In(NotificationViewState.Closed))
        {
            TraceMethodSkip();
            return;
        }

        State = NotificationViewState.Closed;

        RaiseEvent(new RoutedEventArgs(ClosedEvent));
    }

    private void OpeningCompleted()
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
        if (State.NotIn(NotificationViewState.Opening))
        {
            return;
        }

        var contentIsReady = EnsureContentViewReady();
        if (contentIsReady is false)
        {
            State = NotificationViewState.Error;
            return;
        }

        var animatableChild = GetFirstAnimatableChild();
        if (animatableChild != null)
        {
            _tracer.TraceMethodInfo($"Animatable child found: {animatableChild}. {this}");

            animatableChild.OnOpening(completedCallback: OpeningCompleted);
        }
        else
        {
            _tracer.TraceMethodWarning($"Animatable child not found. Skip animation. {this}");

            OpeningCompleted();
        }
    }

    private void LaunchClosingAnimation()
    {
        if (State.NotIn(NotificationViewState.Closing))
        {
            return;
        }

        var neverOpened = !_stateStack.Contains(NotificationViewState.Opening);
        if (neverOpened)
        {
            _tracer.TraceMethodInfo($"Notification has not been opened before. Skip animation. {this}");

            ClosingCompleted();
            return;
        }

        var contentIsReady = EnsureContentViewReady();
        if (contentIsReady is false)
        {
            State = NotificationViewState.Error;
            return;
        }

        var animatableChild = GetFirstAnimatableChild();
        if (animatableChild != null)
        {
            _tracer.TraceMethodInfo($"Animatable child found: {animatableChild}. {this}");

            animatableChild.OnClosing(completedCallback: ClosingCompleted);
        }
        else
        {
            _tracer.TraceMethodWarning($"Animatable child not found. Skip animation. {this}");

            ClosingCompleted();
        }
    }

    private bool EnsureCanShow()
    {
        var layerDescription = $"{nameof(NotificationLayer)} with name '{DisplaySettings.LayerName}'";

        var notificationLayer = TryGetNotificationLayer();
        if (notificationLayer == null)
        {
#if DEBUG
            Guard.Fail($"Failed to show notification. Unable to provide {layerDescription}. {this}");
#else
            _tracer.TraceError($"Failed to show notification {this}. Unable to provide {layerDescription}. {this}");
#endif
            return false;
        }

        return true;
    }

    private NotificationLayer? TryGetNotificationLayer()
    {
        return NotificationLayer.FindLayer(AssociatedObject, DisplaySettings.IsModal, DisplaySettings.LayerName);
    }

    private NotificationLayer GetNotificationLayer()
    {
        return Guard.EnsureIsNotNull(TryGetNotificationLayer());
    }

    private INotificationAnimatable? GetFirstAnimatableChild()
    {
        return this
            .FindVisualChildren<FrameworkElement>(c => c is INotificationAnimatable && c.IsVisible)
            .OfType<INotificationAnimatable>()
            .FirstOrDefault();
    }

    private bool EnsureContentViewReady()
    {
        if (ContentView != null)
        {
            return true;
        }

        if (this.GetWindow() == null)
        {
            _tracer.TraceWarning($"{nameof(ContentView)} is not ready because of notification is detached from visual tree. {this}");

            return false;
        }

        var isApplied = ApplyTemplate();
        if (isApplied)
        {
            UpdateLayout();
        }

        if (ContentView is null)
        {
#if DEBUG
            Guard.Fail($"{nameof(ContentView)} is not ready. {this}");
#else
            _tracer.TraceError($"{nameof(ContentView)} is not ready. {this}");
#endif
            return false;
        }

        return true;
    }

    private void TraceMethodSkip([CallerMemberName] string? methodName = default)
    {
        _tracer.TraceInformation($"Skip '{methodName}' execution. Current state: {State}. {this}");
    }

    private NotificationViewState _state;

    private readonly string _contentType;
    private readonly Stack<NotificationViewState> _stateStack;
    private readonly IStatisticsSender _statisticsSender;
    private readonly string _associatedObjectType;
    private readonly ComponentTracer _tracer;
}