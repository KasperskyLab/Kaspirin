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
using System.Windows.Markup;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Extensions.Internals;

using PopupWpf = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
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

            _timer = WeakDispatcherTimer.Create(UpdatePopupPositionOnTimer, TimeSpan.FromMilliseconds(250), DispatcherPriority.Render);
        }

        public event EventHandler Opened = (_, _) => { };

        public event EventHandler Closed = (_, _) => { };

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
            typeof(Popup));

        #endregion

        #region PopupContent

        public object PopupContent
        {
            get => GetValue(PopupContentProperty);
            set => SetValue(PopupContentProperty, value);
        }

        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
            nameof(PopupContent),
            typeof(object),
            typeof(Popup));

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
            typeof(Popup));

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
            new PropertyMetadata(0.0, OnPopupOffsetChanged));

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
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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
            typeof(Popup));

        #endregion

        public override void OnApplyTemplate()
        {
            _popup = (PopupWpf)GetTemplateChild("PART_Popup");
            _popup.Opened += InvalidatePopup;
            _popup.Opened += OnPopupOpened;
            _popup.Closed += OnPopupClosed;
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

                        return openBehavior switch
                        {
                            PopupOpenBehavior.OnMouseEnter => isTargetMouseOver,
                            PopupOpenBehavior.Explicit => isPopupOpen,
                            _ => throw new ArgumentOutOfRangeException(nameof(PopupOpenBehavior)),
                        };
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

            var targetElement = PopupTarget as FrameworkElement;

            var innerElement = targetElement
                ?.TraverseVisualChildren(child => child is FrameworkElement fe && fe.TemplatedParent is not null && GetIsPopupTarget(fe))
                .OfType<FrameworkElement>()
                .FirstOrDefault();

            _popupTarget = innerElement ?? targetElement;
            _popup.PlacementTarget = _popupTarget;

            UnsubscribeFromParentScrollEvents();
            UnsubscribeFromNotificationLayerEvents();

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

            var isTargetVisible = IsPopupTargetVisible();
            var isNotModalState = _notificationLayer?.IsModalState != true;
            var isWindowActive = _currentWindow?.IsActive == true;
            var isWindowNotMinimized = _currentWindow?.WindowState != WindowState.Minimized;

            _popupRoot.Visibility = isTargetVisible && isNotModalState && isWindowActive && isWindowNotMinimized
                ? Visibility.Visible
                : Visibility.Collapsed;
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

        private void UpdatePopupPositionOnTimer(object? sender, EventArgs e)
            => UpdatePopupPosition();

        private void OnPopupOpened(object? sender, EventArgs e)
        {
            _timer.Start();

            SetCurrentValue(IsPopupOpenProperty, true);

            Opened(this, EventArgs.Empty);
        }

        private void OnPopupClosed(object? sender, EventArgs e)
        {
            _timer.Stop();

            SetCurrentValue(IsPopupOpenProperty, false);

            Closed(this, EventArgs.Empty);
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

        private CustomPopupPlacement[] PopupPlacementCallback(Size popupSize, Size targetSize, Point offset)
        {
            var effectivePosition = PopupPosition;

            var placementProvider = new PopupPositionProvider(popupSize, targetSize, FlowDirection, PopupOffset, _popupShadowOffset);

            if (FlowDirection == FlowDirection.RightToLeft)
            {
                effectivePosition = FlipPosition(PopupPosition);
            }

            return effectivePosition switch
            {
                PopupPosition.Right => placementProvider.LocateRightCenter(),
                PopupPosition.Left => placementProvider.LocateLeftCenter(),
                PopupPosition.Bottom => placementProvider.LocateBottomCenter(),
                PopupPosition.Top => placementProvider.LocateTopCenter(),
                _ => throw new ArgumentOutOfRangeException(nameof(PopupPosition)),
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

            var isNotNotification = _popupTarget.FindVisualParent<NotificationView>() is null;
            if (isNotNotification)
            {
                var notificationLayer = NotificationLayer.FindLayer(_popupTarget, isModal: true);
                if (notificationLayer is not null)
                {
                    _notificationLayer = notificationLayer;

                    _notificationLayer.IsModalStateChanged += InvalidatePopup;
                }
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

                _notificationLayer = null;
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

        private readonly DispatcherTimer _timer;
    }
}
