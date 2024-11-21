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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Kaspirin.UI.Framework.Mvvm;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_OverlayBackground, Type = typeof(Border))]
    [TemplatePart(Name = PART_OverlayCutDecoratorsContainer, Type = typeof(Canvas))]
    public sealed class InteractivityOverlay : ContentControl
    {
        public const string PART_OverlayBackground = "PART_OverlayBackground";
        public const string PART_OverlayCutDecoratorsContainer = "PART_OverlayCutDecoratorsContainer";

        public InteractivityOverlay()
        {
            this.WhenLoaded(OnLoaded);
            this.WhenUnloaded(OnUnloaded);
        }

        #region ClipTargetId

        public Enum? ClipTargetId
        {
            get { return (Enum)GetValue(ClipTargetIdProperty); }
            set { SetValue(ClipTargetIdProperty, value); }
        }

        public static readonly DependencyProperty ClipTargetIdProperty =
            DependencyProperty.Register("ClipTargetId", typeof(Enum), typeof(InteractivityOverlay),
                new PropertyMetadata(null, OnClipTargetIdChanged));

        private static void OnClipTargetIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((InteractivityOverlay)d).UpdateClipArea();
        }

        #endregion

        #region OverlayBehavior

        public InteractivityOverlayBehavior OverlayBehavior
        {
            get { return (InteractivityOverlayBehavior)GetValue(OverlayBehaviorProperty); }
            set { SetValue(OverlayBehaviorProperty, value); }
        }

        public static readonly DependencyProperty OverlayBehaviorProperty =
            DependencyProperty.Register("OverlayBehavior", typeof(InteractivityOverlayBehavior), typeof(InteractivityOverlay),
                new PropertyMetadata(InteractivityOverlayBehavior.DragWindow));

        #endregion

        #region OverlayCommand

        public ICommand OverlayCommand
        {
            get { return (ICommand)GetValue(OverlayCommandProperty); }
            set { SetValue(OverlayCommandProperty, value); }
        }

        public static readonly DependencyProperty OverlayCommandProperty =
            DependencyProperty.Register("OverlayCommand", typeof(ICommand), typeof(InteractivityOverlay));

        #endregion

        #region Public attached properties

        #region OverlayCuts

        public static readonly DependencyProperty OverlayCutsProperty =
            DependencyProperty.RegisterAttached("OverlayCuts", typeof(InteractivityOverlayCutCollection), typeof(InteractivityOverlay),
                new PropertyMetadata(null, OnOverlayCutsChanged));

        public static InteractivityOverlayCutCollection? GetOverlayCuts(DependencyObject element)
            => (InteractivityOverlayCutCollection?)element.GetValue(OverlayCutsProperty);

        public static void SetOverlayCuts(DependencyObject element, InteractivityOverlayCutCollection? value)
            => element.SetValue(OverlayCutsProperty, value);

        private static void OnOverlayCutsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => CacheClipTargetElement(d);

        #endregion

        #endregion

        #region Private attached properties

        #region OverlayCut

        private static readonly DependencyPropertyKey _overlayCutPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("OverlayCut", typeof(InteractivityOverlayCut), typeof(InteractivityOverlay),
                new PropertyMetadata(default(InteractivityOverlayCut)));

        private static InteractivityOverlayCut? GetOverlayCut(DependencyObject element)
            => (InteractivityOverlayCut?)element.GetValue(_overlayCutPropertyKey.DependencyProperty);

        private static void SetOverlayCut(DependencyObject element, InteractivityOverlayCut? value)
            => element.SetValue(_overlayCutPropertyKey, value);

        #endregion

        #region OverlayCutDecoratorTarget

        private static readonly DependencyPropertyKey _overlayCutDecoratorTargetPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("OverlayCutDecoratorTarget", typeof(FrameworkElement), typeof(InteractivityOverlay),
                new PropertyMetadata(default(FrameworkElement)));

        private static FrameworkElement? GetOverlayCutDecoratorTarget(DependencyObject element)
            => (FrameworkElement?)element.GetValue(_overlayCutDecoratorTargetPropertyKey.DependencyProperty);

        private static void SetOverlayCutDecoratorTarget(DependencyObject element, FrameworkElement? value)
            => element.SetValue(_overlayCutDecoratorTargetPropertyKey, value);

        #endregion

        #endregion

        public override void OnApplyTemplate()
        {
            _overlayCutDecoratorsContainer = Guard.EnsureIsInstanceOfType<Canvas>(GetTemplateChild(PART_OverlayCutDecoratorsContainer));

            _overlayBackground = Guard.EnsureIsInstanceOfType<Border>(GetTemplateChild(PART_OverlayBackground));
            _overlayBackground.MouseLeftButtonDown += OnBackgroundPressed;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            _overlayBounds = arrangeBounds;

            UpdateClipArea();

            return base.ArrangeOverride(arrangeBounds);
        }

        private void UpdateClipArea()
        {
            var clipTargetElements = GetClipTargetElements();
            if (clipTargetElements.Count == 1)
            {
                var element = clipTargetElements.Single().Element;
                var elementIsVisisble = element.FindVisualParents<ScrollViewer>().All(s => s.IsInViewport(element, isPartiallyVisible: false));

                if (elementIsVisisble == false)
                {
                    BringIntoViewOnTop(element);
                    Executers.InUiAsync(() => ShowClipArea(clipTargetElements), DispatcherPriority.Loaded);

                    return;
                }
            }

            ShowClipArea(clipTargetElements);
        }

        private void ShowClipArea(IEnumerable<InteractivityOverlayCutPair> clipTargetElements)
        {
            if (_parentNotificationView == null ||
                _parentNotificationLayer == null ||
                _overlayBackground == null ||
                _overlayCutDecoratorsContainer == null)
            {
                return;
            }

            var contentGeometry = GetElementGeometry(_overlayBounds);

            var overlayClip = contentGeometry;
            var backgroundClip = contentGeometry;

            ClearOverlayCutDecorators();
            ClearElementsTracking();

            foreach (var item in clipTargetElements)
            {
                var element = item.Element;
                var overlayCut = item.OverlayCut;

                EnableElementTracking(element);

                if (!element.IsVisible)
                {
                    continue;
                }

                var overlayCutRect = GetOverlayCutRect(_parentNotificationLayer, element, overlayCut.ClipExtent);
                overlayClip = ExcludeRect(overlayClip, overlayCutRect, overlayCut.ClipCornerRadius);

                if (overlayCut.AllowsInteraction)
                {
                    backgroundClip = ExcludeRect(backgroundClip, overlayCutRect, overlayCut.ClipCornerRadius);
                }

                if (overlayCut.Decorator != null ||
                    overlayCut.DecoratorTemplate != null)
                {
                    var decoratorPresenter = CreateDecorator(item);

                    _overlayCutDecoratorsContainer.Children.Add(decoratorPresenter);
                }
            }

            _overlayBackground.Clip = overlayClip;
            _parentNotificationView.BackgroundClip = backgroundClip;
        }

        private IList<InteractivityOverlayCutPair> GetClipTargetElements()
        {
            if (_parentNotificationLayer == null || ClipTargetId == null)
            {
                return _emptyPairs;
            }

            return GetCachedClipTargetElements()
                .Where(element => element.FindVisualParent<NotificationLayer>() == _parentNotificationLayer)
                .Select(element => GetInteractivityOverlayCutPair(element, ClipTargetId))
                .WhereNotNull()
                .ToList();
        }

        private void TryCloseOverlayOnMouseClick(MouseEventArgs e)
        {
            var shouldCloseOverlay = GetClipTargetElements()
                .Where(pair => pair.OverlayCut.CloseOnMouseClick)
                .Any(pair => IsMouseEventTarget(pair, e));

            if (shouldCloseOverlay)
            {
                CloseInteraction();
            }
        }

        private void TryCloseOverlayOnMouseWheel(MouseEventArgs e)
        {
            var shouldCloseOverlay = GetClipTargetElements()
                .Where(pair => pair.OverlayCut.CloseOnMouseWheel)
                .Any(pair => IsMouseEventTarget(pair, e));

            if (shouldCloseOverlay)
            {
                CloseInteraction();
            }
        }

        private void OnLoaded()
        {
            var parentNotificationView = this.FindVisualParent<NotificationView>();
            if (parentNotificationView != null)
            {
                _parentNotificationView = parentNotificationView;
                _parentNotificationLayer = Guard.EnsureIsNotNull(_parentNotificationView.NotificationLayer);

                _parentNotificationLayer.PreviewMouseDown += OnNotificationLayerPreviewMouseDown;
                _parentNotificationLayer.PreviewMouseWheel += OnNotificationLayerPreviewMouseWheel;
            }

            UpdateClipArea();
        }

        private void OnUnloaded()
        {
            if (_parentNotificationLayer != null)
            {
                _parentNotificationLayer.PreviewMouseDown -= OnNotificationLayerPreviewMouseDown;
                _parentNotificationLayer.PreviewMouseWheel -= OnNotificationLayerPreviewMouseWheel;
            }
        }

        private void OnNotificationLayerPreviewMouseDown(object? sender, MouseButtonEventArgs e)
            => TryCloseOverlayOnMouseClick(e);

        private void OnNotificationLayerPreviewMouseWheel(object? sender, MouseWheelEventArgs e)
            => TryCloseOverlayOnMouseWheel(e);

        private void OnElementSizeChanged(object sender, SizeChangedEventArgs e) => UpdateClipArea();

        private void OnElementIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => UpdateClipArea();

        private void OnBackgroundPressed(object sender, MouseButtonEventArgs e)
        {
            switch (OverlayBehavior)
            {
                case InteractivityOverlayBehavior.DragWindow:
                    DragWindow();
                    break;
                case InteractivityOverlayBehavior.CloseInteraction:
                    CloseInteraction();
                    break;
                case InteractivityOverlayBehavior.ExecuteCommand:
                    ExecuteCommand();
                    break;
                case InteractivityOverlayBehavior.None:
                default:
                    break;
            }
        }

        private void ExecuteCommand()
            => CommandHelper.ExecuteCommand(OverlayCommand);

        private void CloseInteraction()
            => _parentNotificationView?.CloseSmooth();

        private void DragWindow()
        {
            var parentWindow = this.GetWindow();
            if (parentWindow != null)
            {
                var hwnd = parentWindow.GetHandle();
                if (hwnd != IntPtr.Zero)
                {
                    hwnd.DragWindow();
                }
            }
        }

        private void EnableElementTracking(FrameworkElement element)
        {
            element.SizeChanged -= OnElementSizeChanged;
            element.SizeChanged += OnElementSizeChanged;

            element.IsVisibleChanged -= OnElementIsVisibleChanged;
            element.IsVisibleChanged += OnElementIsVisibleChanged;

            _sizeAndVisibilityChangeTrackedElements.Add(element);
        }

        private void ClearElementsTracking()
        {
            _sizeAndVisibilityChangeTrackedElements.ForEach(element =>
            {
                element.SizeChanged -= OnElementSizeChanged;
                element.IsVisibleChanged -= OnElementIsVisibleChanged;
            });

            _sizeAndVisibilityChangeTrackedElements.Clear();
        }

        private void ClearOverlayCutDecorators()
        {
            if (_overlayCutDecoratorsContainer is null)
            {
                return;
            }

            _overlayCutDecoratorsContainer.Children.ForEachNonGeneric(child =>
            {
                if (child is ContentPresenter decoratorPresenter)
                {
                    decoratorPresenter.SizeChanged -= OnDecoratorPresenterSizeChanged;
                }
            });

            _overlayCutDecoratorsContainer.Children.Clear();
        }

        private static void BringIntoViewOnTop(FrameworkElement frameworkElement)
        {
            var elementTopMargin = frameworkElement.Margin.Top;
            var elementTopOffset = elementTopMargin > 0 ? -elementTopMargin : 0;

            var verticalOffset = new Rect(0, elementTopOffset, 1, 1);

            frameworkElement.BringIntoView(verticalOffset);
        }

        private static UIElement CreateDecorator(InteractivityOverlayCutPair item)
        {
            var element = item.Element;
            var overlayCut = item.OverlayCut;

            var decoratorPresenter = new ContentPresenter()
            {
                Content = overlayCut.Decorator,
                ContentTemplate = overlayCut.DecoratorTemplate,
            };

            SetOverlayCut(decoratorPresenter, overlayCut);
            SetOverlayCutDecoratorTarget(decoratorPresenter, element);

            decoratorPresenter.SizeChanged += OnDecoratorPresenterSizeChanged;

            return decoratorPresenter;
        }

        private static InteractivityOverlayCutPair? GetInteractivityOverlayCutPair(FrameworkElement element, Enum overlayContentKey)
        {
            var overlayCuts = element.GetValue(OverlayCutsProperty) as InteractivityOverlayCutCollection;
            if (overlayCuts is not null)
            {
                if (overlayCuts.TryGetValue(overlayContentKey, out var overlayCut))
                {
                    return new InteractivityOverlayCutPair(element, overlayCut);
                }
            }

            return null;
        }

        private static Rect GetOverlayCutRect(NotificationLayer notificationLayer, FrameworkElement target, Thickness extent)
        {
            var bounds = GetVisibleBounds(notificationLayer, target);

            var transform = target.TransformToAncestor(notificationLayer);
            var topLeft = transform.Transform(bounds.TopLeft);
            var bottomRight = transform.Transform(bounds.BottomRight);
            var rect = new Rect(topLeft, bottomRight);

            const double epsilon = 1e-4;
            var leftExtent = bounds.X.NearlyZero(epsilon) ? extent.Left : 0;
            var topExtent = bounds.Y.NearlyZero(epsilon) ? extent.Top : 0;
            var rightExtent = (bounds.X + bounds.Width).NearlyEqual(target.ActualWidth, epsilon) ? extent.Right : 0;
            var bottomExtent = (bounds.Y + bounds.Height).NearlyEqual(target.ActualHeight, epsilon) ? extent.Bottom : 0;

            var cutX = rect.X - leftExtent;
            var cutY = rect.Y - topExtent;
            var cutWidth = Math.Max(0, rect.Width + leftExtent + rightExtent);
            var cutHeight = Math.Max(0, rect.Height + topExtent + bottomExtent);

            return new Rect(cutX, cutY, cutWidth, cutHeight);
        }

        private static void OnDecoratorPresenterSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var decoratorPresenter = Guard.EnsureIsInstanceOfType<ContentPresenter>(sender);

            var overlayCut = GetOverlayCut(decoratorPresenter);
            var overlayCutDecoratorTarget = GetOverlayCutDecoratorTarget(decoratorPresenter);

            if (overlayCut is not null && overlayCutDecoratorTarget is not null)
            {
                var overlayCutDecoratorsContainer = decoratorPresenter.FindLogicalParent<Canvas>();
                var overlayCutDecoratorTargetLocation = overlayCutDecoratorTarget.TranslatePoint(new Point(), overlayCutDecoratorsContainer);

                var overlayCutDecoratorLocation = GetOverlayCutDecoratorLocation(
                    overlayCutDecoratorTargetLocation,
                    overlayCutDecoratorTarget.ActualHeight,
                    overlayCutDecoratorTarget.ActualWidth,
                    overlayCut.ClipExtent,
                    overlayCut.DecoratorPosition,
                    decoratorPresenter.ActualHeight,
                    decoratorPresenter.ActualWidth,
                    overlayCut.DecoratorHorizontalOffset,
                    overlayCut.DecoratorVerticalOffset);

                Canvas.SetLeft(decoratorPresenter, overlayCutDecoratorLocation.X);
                Canvas.SetTop(decoratorPresenter, overlayCutDecoratorLocation.Y);
            }
        }

        private static Geometry GetElementGeometry(Size elementSize)
            => (Geometry)new RectangleGeometry(new Rect(new Point(0, 0), elementSize)).GetAsFrozen();

        private static Geometry ExcludeRect(Geometry geometry, Rect rect, double cornerRadius)
            => ExcludeGeometry(geometry, new RectangleGeometry(rect, cornerRadius, cornerRadius));

        private static Geometry ExcludeGeometry(Geometry geometry, Geometry excludedGeometry)
            => (Geometry)new CombinedGeometry
            {
                Geometry1 = geometry,
                Geometry2 = excludedGeometry,
                GeometryCombineMode = GeometryCombineMode.Exclude
            }.GetAsFrozen();

        private static Rect GetVisibleBounds(NotificationLayer notificationLayer, FrameworkElement element)
        {
            var currentBounds = GetWindowRelatedBounds(notificationLayer, element);

            var container = VisualTreeHelper.GetParent(element) as FrameworkElement;
            while (container is not null)
            {
                var containerBounds = GetWindowRelatedBounds(notificationLayer, container);
                currentBounds.Intersect(containerBounds);

                container = VisualTreeHelper.GetParent(container) as FrameworkElement;
            }

            return new Rect(
                notificationLayer.TranslatePoint(currentBounds.TopLeft, element),
                notificationLayer.TranslatePoint(currentBounds.BottomRight, element));
        }

        private static Rect GetWindowRelatedBounds(NotificationLayer notificationLayer, FrameworkElement element)
        {
            var topLeft = element.TranslatePoint(new Point(0, 0), notificationLayer);
            if (element.FlowDirection == FlowDirection.RightToLeft)
            {
                topLeft.Offset(-element.ActualWidth, 0);
            }

            return new Rect(topLeft, element.RenderSize);
        }

        private static Point GetOverlayCutDecoratorLocation(
            Point decoratorTargetLocation,
            double decoratorTargetHeight,
            double decoratorTargetWidth,
            Thickness decoratorTargetClipExtent,
            InteractivityOverlayCutDecoratorPosition decoratorPosition,
            double decoratorHeight,
            double decoratorWidth,
            double horizontalOffset,
            double verticalOffset)
        {
            var xLeft = decoratorTargetLocation.X - decoratorTargetClipExtent.Left - decoratorWidth + horizontalOffset;
            var xCenter = decoratorTargetLocation.X + (decoratorTargetWidth - decoratorWidth) / 2d + horizontalOffset;
            var xRight = decoratorTargetLocation.X + decoratorTargetClipExtent.Right + decoratorTargetWidth + horizontalOffset;

            var yTop = decoratorTargetLocation.Y - decoratorTargetClipExtent.Top - decoratorHeight + verticalOffset;
            var yCenter = decoratorTargetLocation.Y + (decoratorTargetHeight - decoratorHeight) / 2d + verticalOffset;
            var yBottom = decoratorTargetLocation.Y + decoratorTargetClipExtent.Bottom + decoratorTargetHeight + verticalOffset;

            return decoratorPosition switch
            {
                InteractivityOverlayCutDecoratorPosition.TopLeft => new Point(xLeft, yTop),
                InteractivityOverlayCutDecoratorPosition.TopCenter => new Point(xCenter, yTop),
                InteractivityOverlayCutDecoratorPosition.TopRight => new Point(xRight, yTop),
                InteractivityOverlayCutDecoratorPosition.RightCenter => new Point(xRight, yCenter),
                InteractivityOverlayCutDecoratorPosition.BottomRight => new Point(xRight, yBottom),
                InteractivityOverlayCutDecoratorPosition.BottomCenter => new Point(xCenter, yBottom),
                InteractivityOverlayCutDecoratorPosition.BottomLeft => new Point(xLeft, yBottom),
                InteractivityOverlayCutDecoratorPosition.LeftCenter => new Point(xLeft, yCenter),
                _ => throw new ArgumentOutOfRangeException(nameof(decoratorPosition), decoratorPosition, "Unsupported value of overlay cut decorator position")
            };
        }

        private static bool IsMouseEventTarget(InteractivityOverlayCutPair pair, MouseEventArgs args)
        {
            var mousePosition = args.GetPosition(pair.Element);

            var margin = pair.OverlayCut.ClipExtent;
            mousePosition.X += margin.Left;
            mousePosition.Y += margin.Top;

            var actualWidth = pair.Element.ActualWidth + margin.Left + margin.Right;
            var actualHeight = pair.Element.ActualHeight + margin.Top + margin.Bottom;

            return mousePosition is { X: >= 0, Y: >= 0 }
                && mousePosition.X <= actualWidth && mousePosition.Y <= actualHeight;
        }

        private static void CacheClipTargetElement(DependencyObject dependencyObject)
        {
            if (dependencyObject is FrameworkElement)
            {
                _clipTargetElements.Add(new WeakReference(dependencyObject));
                _clipTargetElements.Where(wr => !wr.IsAlive).ToList().ForEach(wr => _clipTargetElements.Remove(wr));
            }
        }

        private static IList<FrameworkElement> GetCachedClipTargetElements()
        {
            return _clipTargetElements.Select(wr => wr.Target).WhereNotNull().OfType<FrameworkElement>().ToList();
        }

        private sealed class InteractivityOverlayCutPair
        {
            public InteractivityOverlayCutPair(FrameworkElement element, InteractivityOverlayCut overlayCut)
            {
                Element = element;
                OverlayCut = overlayCut;
            }

            public FrameworkElement Element { get; }

            public InteractivityOverlayCut OverlayCut { get; }
        }

        private readonly IList<FrameworkElement> _sizeAndVisibilityChangeTrackedElements = new List<FrameworkElement>();

        private Size _overlayBounds;
        private Border? _overlayBackground;
        private Canvas? _overlayCutDecoratorsContainer;
        private NotificationView? _parentNotificationView;
        private NotificationLayer? _parentNotificationLayer;

        private static readonly IList<InteractivityOverlayCutPair> _emptyPairs = new List<InteractivityOverlayCutPair>();
        private static readonly IList<WeakReference> _clipTargetElements = new List<WeakReference>();
        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Interactivity);
    }
}
