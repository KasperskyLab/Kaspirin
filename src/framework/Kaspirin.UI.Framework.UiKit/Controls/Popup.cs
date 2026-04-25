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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Extensions.Internals;

using PopupWpf = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_Popup, Type = typeof(PopupWpf))]
[TemplatePart(Name = PART_PopupArrow, Type = typeof(Image))]
[TemplatePart(Name = PART_PopupRoot, Type = typeof(Border))]
[ContentProperty(nameof(PopupContent))]
public sealed class Popup : Control
{
    public const string PART_Popup = "PART_Popup";
    public const string PART_PopupArrow = "PART_PopupArrow";
    public const string PART_PopupRoot = "PART_PopupRoot";

    public Popup()
    {
        Loaded += PopupControlLoaded;
        Unloaded += PopupControlUnloaded;

        _animationManager = ServiceLocator.GetService<IAnimationManager>();
        _timer = TimerFactory.CreateOnUi(UpdatePopupPositionOnTimer, TimeSpan.FromMilliseconds(250), DispatcherPriority.Render);
    }

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
        typeof(Popup));

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
        typeof(Popup));

    #endregion

    #region PopupTarget

    public UIElement? PopupTarget
    {
        get => (UIElement?)GetValue(PopupTargetProperty);
        set => SetValue(PopupTargetProperty, value);
    }

    public static readonly DependencyProperty PopupTargetProperty = DependencyProperty.Register(
        nameof(PopupTarget),
        typeof(UIElement),
        typeof(Popup),
        new PropertyMetadata(default(UIElement), OnPopupTargetChanged));

    private static void OnPopupTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var popup = (Popup)d;

        popup.InvalidatePopupTarget();
        popup.InvalidatePopup();
    }

    #endregion

    #region PopupHeader

    public object PopupHeader
    {
        get => GetValue(PopupHeaderProperty);
        set => SetValue(PopupHeaderProperty, value);
    }

    public static readonly DependencyProperty PopupHeaderProperty = DependencyProperty.Register(
        nameof(PopupHeader),
        typeof(object),
        typeof(Popup),
        new PropertyMetadata(default(object)));

    #endregion

    #region PopupContent

    public object? PopupContent
    {
        get => GetValue(PopupContentProperty);
        set => SetValue(PopupContentProperty, value);
    }

    public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
        nameof(PopupContent),
        typeof(object),
        typeof(Popup),
        new PropertyMetadata(default(object)));

    #endregion

    #region PopupContentTemplate

    public DataTemplate? PopupContentTemplate
    {
        get => (DataTemplate?)GetValue(PopupContentTemplateProperty);
        set => SetValue(PopupContentTemplateProperty, value);
    }

    public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
        nameof(PopupContentTemplate),
        typeof(DataTemplate),
        typeof(Popup),
        new PropertyMetadata(default(DataTemplate)));

    #endregion

    #region PopupPosition

    public PopupPosition PopupPosition
    {
        get => (PopupPosition)GetValue(PopupPositionProperty);
        set => SetValue(PopupPositionProperty, value);
    }

    public static readonly DependencyProperty PopupPositionProperty = DependencyProperty.Register(
        nameof(PopupPosition),
        typeof(PopupPosition),
        typeof(Popup),
        new PropertyMetadata(PopupPosition.Right, OnPopupPositionChanged));

    private static void OnPopupPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var popup = (Popup)d;

        popup.SetPopupArrowPosition();
        popup.UpdatePopupPosition();
    }

    #endregion

    #region PopupOffset

    public double PopupOffset
    {
        get => (double)GetValue(PopupOffsetProperty);
        set => SetValue(PopupOffsetProperty, value);
    }

    public static readonly DependencyProperty PopupOffsetProperty = DependencyProperty.Register(
        nameof(PopupOffset),
        typeof(double),
        typeof(Popup),
        new PropertyMetadata(default(double), OnPopupOffsetChanged));

    private static void OnPopupOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var popup = (Popup)d;

        popup.UpdatePopupPosition();
    }

    #endregion

    #region PopupOpenBehavior

    public PopupOpenBehavior PopupOpenBehavior
    {
        get => (PopupOpenBehavior)GetValue(PopupOpenBehaviorProperty);
        set => SetValue(PopupOpenBehaviorProperty, value);
    }

    public static readonly DependencyProperty PopupOpenBehaviorProperty = DependencyProperty.Register(
        nameof(PopupOpenBehavior),
        typeof(PopupOpenBehavior),
        typeof(Popup),
        new PropertyMetadata(PopupOpenBehavior.Explicit));

    #endregion

    #region IsPopupOpen

    public bool IsPopupOpen
    {
        get => (bool)GetValue(IsPopupOpenProperty);
        set => SetValue(IsPopupOpenProperty, value);
    }

    public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register(
        nameof(IsPopupOpen),
        typeof(bool),
        typeof(Popup),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region IsPopupStaysOpen

    public bool IsPopupStaysOpen
    {
        get => (bool)GetValue(IsPopupStaysOpenProperty);
        set => SetValue(IsPopupStaysOpenProperty, value);
    }

    public static readonly DependencyProperty IsPopupStaysOpenProperty = DependencyProperty.Register(
        nameof(IsPopupStaysOpen),
        typeof(bool),
        typeof(Popup),
        new PropertyMetadata(true));

    #endregion

    #region IsPopupHitTestVisible

    public bool IsPopupHitTestVisible
    {
        get => (bool)GetValue(IsPopupHitTestVisibleProperty);
        set => SetValue(IsPopupHitTestVisibleProperty, value);
    }

    public static readonly DependencyProperty IsPopupHitTestVisibleProperty = DependencyProperty.Register(
        nameof(IsPopupHitTestVisible),
        typeof(bool),
        typeof(Popup),
        new PropertyMetadata(true));

    #endregion

    #region IsPopupTarget

    public static bool GetIsPopupTarget(DependencyObject obj)
        => (bool)obj.GetValue(IsPopupTargetProperty);

    public static void SetIsPopupTarget(DependencyObject obj, bool value)
        => obj.SetValue(IsPopupTargetProperty, value);

    public static readonly DependencyProperty IsPopupTargetProperty = DependencyProperty.RegisterAttached(
        "IsPopupTarget",
        typeof(bool),
        typeof(Popup),
        new PropertyMetadata(default(bool)));

    #endregion

    public override void OnApplyTemplate()
    {
        _popup = (PopupWpf)GetTemplateChild("PART_Popup");
        _popup.Opened += InvalidatePopup;
        _popup.Opened += OnPopupOpened;
        _popup.Closed += OnPopupClosed;
        // By default popup marshals unhandled input events (mouse, stylus, keyboard) to its PlacementTarget.
        // Mark MouseDown event as handled to suppress it and isolate clicks inside popup and its content.
        _popup.MouseDown += (s, e) => e.Handled = true;
        _popup.Placement = PlacementMode.Custom;
        _popup.CustomPopupPlacementCallback = PopupPlacementCallback;

        _popupArrow = (Image)GetTemplateChild("PART_PopupArrow");
        _popupRoot = (Border)GetTemplateChild("PART_PopupRoot");
    }

    private void PopupControlLoaded(object sender, RoutedEventArgs arg)
    {
        SubscribeToWindowEvents();

        SetPopupEffect();
        SetPopupArrowPosition();

        InvalidatePopupTarget();
        InvalidatePopup();
    }

    private void PopupControlUnloaded(object sender, RoutedEventArgs e)
    {
        UnsubscribeFromParentScrollEvents();
        UnsubscribeFromNotificationLayerEvents();
        UnsubscribeFromWindowEvents();

        _popupTarget = null;
    }

    private void InvalidatePopupIsOpenBinding()
    {
        if (_popup is null)
        {
            return;
        }

        if (_popupTarget is null)
        {
            BindingOperations.ClearBinding(_popup, PopupWpf.IsOpenProperty);
        }
        else
        {
            var binding = new MultiBinding()
            {
                Bindings =
                {
                    new Binding
                    {
                        Path = IsPopupOpenProperty.AsPath(),
                        Source = this
                    },
                    new Binding
                    {
                        Path = UIElement.IsMouseOverProperty.AsPath(),
                        Mode = BindingMode.OneWay,
                        Source = _popupTarget,
                    },
                    new Binding
                    {
                        Path = PopupOpenBehaviorProperty.AsPath(),
                        Source = this,
                    }
                },
                Converter = new DelegateMultiConverter(values =>
                {
                    var isPopupOpen = Guard.EnsureArgumentIsInstanceOfType<bool>(values[0]);
                    var isTargetMouseOver = Guard.EnsureArgumentIsInstanceOfType<bool>(values[1]);
                    var openBehavior = Guard.EnsureArgumentIsInstanceOfType<PopupOpenBehavior>(values[2]);

                    var isOpen = openBehavior switch
                    {
                        PopupOpenBehavior.OnMouseEnter => isTargetMouseOver,
                        PopupOpenBehavior.Explicit => isPopupOpen,
                        _ => throw new UnexpectedValueException(openBehavior)
                    };

                    if (_popup != null)
                    {
                        var isAnimationEnabled = _animationManager.State == AnimationState.Enabled;
                        if (isAnimationEnabled && isOpen)
                        {
                            _popup.PopupAnimation = PopupAnimation.Fade;
                        }
                        else
                        {
                            _popup.PopupAnimation = PopupAnimation.None;
                        }
                    }

                    return isOpen;

                }),
                Mode = BindingMode.OneWay
            };

            BindingOperations.SetBinding(_popup, PopupWpf.IsOpenProperty, binding);
        }
    }

    private void InvalidatePopup(object sender, RoutedEventArgs e)
        => InvalidatePopup();

    private void InvalidatePopup(object sender, ScrollChangedEventArgs e)
        => InvalidatePopup();

    private void InvalidatePopup(object? sender, EventArgs e)
        => InvalidatePopup();

    private void InvalidatePopupTarget()
    {
        if (_popup is null)
        {
            return;
        }

        UnsubscribeFromPopupTargetEvents();

        var targetElement = PopupTarget as FrameworkElement;

        var innerElement = targetElement
            ?.TraverseVisualChildren(child => child is FrameworkElement fe && fe.TemplatedParent is not null)
            .Where(child => GetIsPopupTarget(child))
            .OfType<FrameworkElement>()
            .FirstOrDefault();

        _popupTarget = innerElement ?? targetElement;
        _popup.PlacementTarget = _popupTarget;

        UnsubscribeFromParentScrollEvents();
        UnsubscribeFromNotificationLayerEvents();

        SubscribeToPopupTargetEvents();
        SubscribeToParentScrollEvents();
        SubscribeToNotificationLayerEvents();

        InvalidatePopupIsOpenBinding();
    }

    private void InvalidatePopup()
    {
        UpdatePopupVisibility();
        UpdatePopupPosition();
    }

    private void UpdatePopupVisibility()
    {
        if (_popupRoot is null)
        {
            return;
        }

        var isTopmostPopup = _notificationLayer?.TopmostNotification == _notificationView;
        var isTargetVisible = IsPopupTargetVisible();
        var isWindowForeground = IsWindowForeground();
        var isWindowActive = _currentWindow?.IsActive == true;
        var isWindowNotMinimized = _currentWindow?.WindowState != WindowState.Minimized;

        var canShowPopup = isTargetVisible && isTopmostPopup && isWindowForeground && isWindowActive && isWindowNotMinimized;

        _popupRoot.IsHitTestVisible = canShowPopup;
        _popupRoot.Visibility = canShowPopup
            ? Visibility.Visible
            : Visibility.Hidden;
    }

    private void UpdatePopupPosition()
    {
        if (_popup is null)
        {
            return;
        }

        if (_popupTarget is not null)
        {
            // Need to refresh popup position.
            var offset = _popup.HorizontalOffset;
            _popup.HorizontalOffset = offset + double.Epsilon;
            _popup.HorizontalOffset = offset;
        }
    }

    private void UpdatePopupPositionOnTimer()
        => UpdatePopupPosition();

    private void OnPopupOpened(object? sender, EventArgs e)
        => OpenPopup();

    private void OnPopupClosed(object? sender, EventArgs e)
        => ClosePopup();

    private void OnPopupTargetLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (!IsPopupStaysOpen && !IsPopupContainsFocusedElement())
        {
            ClosePopup();
        }
    }

    private void OpenPopup()
    {
        _timer.Start();

        SetCurrentValue(IsPopupOpenProperty, true);

        RaiseEvent(new RoutedEventArgs(OpenedEvent));
    }

    private void ClosePopup()
    {
        _timer.Stop();

        SetCurrentValue(IsPopupOpenProperty, false);

        RaiseEvent(new RoutedEventArgs(ClosedEvent));
    }

    private bool IsPopupTargetVisible()
    {
        if (_popupTarget?.IsVisible != true)
        {
            return false;
        }

        if (_parentScrolls is not null)
        {
            foreach (var parentScroll in _parentScrolls)
            {
                if (parentScroll.IsVisible is false)
                {
                    return false;
                }

                var scrollViewerRect = new Rect(new Point(0, 0), parentScroll.RenderSize);
                var popupTargetRect = new Rect(new Point(0, 0), _popupTarget.RenderSize);

                var popupTargetOnScrollViewerRect = _popupTarget.TransformToAncestor(parentScroll).TransformBounds(popupTargetRect);

                var isPopupTargetHidden = Rect.Intersect(scrollViewerRect, popupTargetOnScrollViewerRect) == Rect.Empty;
                if (isPopupTargetHidden)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsPopupContainsFocusedElement()
    {
        if (_popupRoot is null)
        {
            return false;
        }

        if (Keyboard.FocusedElement is DependencyObject focusedControl)
        {
            return focusedControl.HasVisualParent(_popupRoot);
        }

        return false;
    }

    private CustomPopupPlacement[] PopupPlacementCallback(Size popupSize, Size targetSize, Point offset)
    {
        var popupFlowDirection = FlowDirection;
        var targetFlowDirection = _popupTarget?.FlowDirection ?? popupFlowDirection;

        var effectivePosition = PopupPosition;

        var placementProvider = new PopupPositionProvider(popupSize, targetSize, targetFlowDirection, PopupOffset, _popupShadowOffset);

        if (popupFlowDirection == FlowDirection.RightToLeft)
        {
            effectivePosition = FlipPosition(PopupPosition);
        }

        return effectivePosition switch
        {
            PopupPosition.Right => placementProvider.LocateRightCenter(),
            PopupPosition.Left => placementProvider.LocateLeftCenter(),
            PopupPosition.Bottom => placementProvider.LocateBottomCenter(),
            PopupPosition.Top => placementProvider.LocateTopCenter(),
            _ => throw new UnexpectedValueException(effectivePosition)
        };
    }

    private void SetPopupEffect()
    {
        if (_popupRoot?.Effect is DropShadowEffect shadow)
        {
            _popupShadowOffset = shadow.GetShadowOffset();
        }
    }

    private void SetPopupArrowPosition()
    {
        if (_popupArrow is not null)
        {
            _popupArrow.SetValue(Grid.ColumnProperty, SelectArrowColumn(PopupPosition));
            _popupArrow.SetValue(Grid.RowProperty, SelectArrowRow(PopupPosition));
        }
    }

    private bool IsWindowForeground()
    {
        var hWnd = _currentWindow?.GetHandle();
        if (hWnd.HasValue && hWnd != IntPtr.Zero)
        {
            return hWnd.Value.IsForeground();
        }

        return false;
    }

    private void SubscribeToWindowEvents()
    {
        var currentWindow = this.GetWindow();
        if (currentWindow is null)
        {
            return;
        }

        _currentWindow = currentWindow;

        _currentWindow.LocationChanged += InvalidatePopup;
        _currentWindow.SizeChanged += InvalidatePopup;
        _currentWindow.Activated += InvalidatePopup;
        _currentWindow.Deactivated += InvalidatePopup;
        _currentWindow.StateChanged += InvalidatePopup;
    }

    private void SubscribeToPopupTargetEvents()
    {
        if (_popupTarget is not null)
        {
            _popupTarget.LostKeyboardFocus += OnPopupTargetLostKeyboardFocus;
        }
    }

    private void SubscribeToParentScrollEvents()
    {
        if (_popupTarget is null)
        {
            return;
        }

        _parentScrolls = _popupTarget.FindVisualParents<ScrollViewer>().ToArray();

        foreach (var scroll in _parentScrolls)
        {
            scroll.ScrollChanged += InvalidatePopup;
        }
    }

    private void SubscribeToNotificationLayerEvents()
    {
        if (_popupTarget is null)
        {
            return;
        }

        _notificationView = _popupTarget.FindVisualParent<NotificationView>() ?? this.FindVisualParent<NotificationView>();

        var notificationLayer = NotificationLayer.FindLayer(_popupTarget, isModal: true);
        if (notificationLayer is not null)
        {
            _notificationLayer = notificationLayer;

            _notificationLayer.IsModalStateChanged += InvalidatePopup;
            _notificationLayer.TopmostNotificationChanged += InvalidatePopup;
        }
    }

    private void UnsubscribeFromWindowEvents()
    {
        if (_currentWindow is null)
        {
            return;
        }

        _currentWindow.LocationChanged -= InvalidatePopup;
        _currentWindow.SizeChanged -= InvalidatePopup;
        _currentWindow.Activated -= InvalidatePopup;
        _currentWindow.Deactivated -= InvalidatePopup;
        _currentWindow.StateChanged -= InvalidatePopup;

        _currentWindow = null;
    }

    private void UnsubscribeFromPopupTargetEvents()
    {
        if (_popupTarget is not null)
        {
            _popupTarget.LostKeyboardFocus -= OnPopupTargetLostKeyboardFocus;
        }
    }

    private void UnsubscribeFromParentScrollEvents()
    {
        if (_parentScrolls is not null)
        {
            foreach (var scroll in _parentScrolls)
            {
                scroll.ScrollChanged -= InvalidatePopup;
            }

            _parentScrolls = null;
        }
    }

    private void UnsubscribeFromNotificationLayerEvents()
    {
        if (_notificationLayer is not null)
        {
            _notificationLayer.IsModalStateChanged -= InvalidatePopup;
            _notificationLayer.TopmostNotificationChanged -= InvalidatePopup;

            _notificationLayer = null;
            _notificationView = null;
        }
    }

    private static int SelectArrowRow(PopupPosition popupPosition)
    {
        return popupPosition switch
        {
            PopupPosition.Bottom => 0,
            PopupPosition.Left or PopupPosition.Right => 1,
            PopupPosition.Top => 2,
            _ => -1,
        };
    }

    private static int SelectArrowColumn(PopupPosition popupPosition)
    {
        return popupPosition switch
        {
            PopupPosition.Right => 0,
            PopupPosition.Top or PopupPosition.Bottom => 1,
            PopupPosition.Left => 2,
            _ => -1,
        };
    }

    private static PopupPosition FlipPosition(PopupPosition popupPosition)
    {
        return popupPosition switch
        {
            PopupPosition.Left => PopupPosition.Right,
            PopupPosition.Right => PopupPosition.Left,
            _ => popupPosition,
        };
    }

    private Window? _currentWindow;
    private PopupWpf? _popup;
    private Image? _popupArrow;
    private Border? _popupRoot;
    private double _popupShadowOffset;
    private NotificationLayer? _notificationLayer;
    private ScrollViewer[]? _parentScrolls;
    private FrameworkElement? _popupTarget;
    private NotificationView? _notificationView;

    private readonly IAnimationManager _animationManager;
    private readonly ITimer _timer;
}
