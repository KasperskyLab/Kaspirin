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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal static class ScrollViewerInternals
    {
        public static string PART_ScrollContentPresenterContainer = "PART_ScrollContentPresenterContainer";

        #region CanMouseWheelScroll

        public static bool GetCanMouseWheelScroll(DependencyObject obj)
            => (bool)obj.GetValue(CanMouseWheelScrollProperty);

        public static void SetCanMouseWheelScroll(DependencyObject obj, bool value)
            => obj.SetValue(CanMouseWheelScrollProperty, value);

        public static readonly DependencyProperty CanMouseWheelScrollProperty = DependencyProperty.RegisterAttached(
            "CanMouseWheelScroll",
            typeof(bool),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(CanMouseWheelScrollProperty), OnCanMouseWheelScrollChanged, true));

        private static void OnCanMouseWheelScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)d;

            scrollViewer.PreviewMouseWheel -= HandleMouseWheelScroll;
            scrollViewer.PreviewMouseWheel += HandleMouseWheelScroll;
        }

        #endregion

        #region MouseWheelScrollOrientation

        public static Orientation GetMouseWheelScrollOrientation(DependencyObject obj)
            => (Orientation)obj.GetValue(MouseWheelScrollOrientationProperty);

        public static void SetMouseWheelScrollOrientation(DependencyObject obj, Orientation value)
            => obj.SetValue(MouseWheelScrollOrientationProperty, value);

        public static readonly DependencyProperty MouseWheelScrollOrientationProperty = DependencyProperty.RegisterAttached(
            "MouseWheelScrollOrientation",
            typeof(Orientation),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(MouseWheelScrollOrientationProperty), OnMouseWheelScrollOrientationChanged, Orientation.Vertical));

        private static void OnMouseWheelScrollOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)d;

            scrollViewer.PreviewMouseWheel -= HandleMouseWheelScroll;
            scrollViewer.PreviewMouseWheel += HandleMouseWheelScroll;
        }

        #endregion

        #region ScrollViewerObserver

        public static bool GetScrollViewerObserver(DependencyObject element)
            => (bool)element.GetValue(ScrollViewerObserverProperty);

        public static void SetScrollViewerObserver(DependencyObject element, bool value)
            => element.SetValue(ScrollViewerObserverProperty, value);

        public static readonly DependencyProperty ScrollViewerObserverProperty = DependencyProperty.RegisterAttached(
            "ScrollViewerObserver",
            typeof(bool),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(ScrollViewerObserverProperty), OnScrollViewerObserverChanged));

        #endregion

        #region IsBorderFadeEnabled

        public static bool GetIsBorderFadeEnabled(DependencyObject obj)
            => (bool)obj.GetValue(IsBorderFadeEnabledProperty);

        public static void SetIsBorderFadeEnabled(DependencyObject obj, bool value)
            => obj.SetValue(IsBorderFadeEnabledProperty, value);

        public static readonly DependencyProperty IsBorderFadeEnabledProperty = DependencyProperty.RegisterAttached(
            "IsBorderFadeEnabled",
            typeof(bool),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(IsBorderFadeEnabledProperty), OnIsBorderFadeEnabledChanged));

        private static void OnIsBorderFadeEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ApplyBorderFade((ScrollViewer)d);

        #endregion

        #region BorderFadeState

        public static ScrollViewerBorderFadeState GetBorderFadeState(DependencyObject obj)
            => (ScrollViewerBorderFadeState)obj.GetValue(BorderFadeStateProperty);

        public static void SetBorderFadeState(DependencyObject obj, ScrollViewerBorderFadeState value)
            => obj.SetValue(_borderFadeStatePropertyKey, value);

        private static readonly DependencyPropertyKey _borderFadeStatePropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "BorderFadeState",
            typeof(ScrollViewerBorderFadeState),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(_borderFadeStatePropertyKey)));

        public static readonly DependencyProperty BorderFadeStateProperty = _borderFadeStatePropertyKey.DependencyProperty;

        #endregion

        #region BorderFadeWidth

        public static double GetBorderFadeWidth(DependencyObject obj)
            => (double)obj.GetValue(BorderFadeWidthProperty);

        public static void SetBorderFadeWidth(DependencyObject obj, double value)
            => obj.SetValue(BorderFadeWidthProperty, value);

        public static readonly DependencyProperty BorderFadeWidthProperty = DependencyProperty.RegisterAttached(
            "BorderFadeWidth",
            typeof(double),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(BorderFadeWidthProperty), defaultValue: UIKitConstants.ScrollViewerBorderFadeLength));

        #endregion

        #region BorderFadeHeight

        public static double GetBorderFadeHeight(DependencyObject obj)
            => (double)obj.GetValue(BorderFadeHeightProperty);

        public static void SetBorderFadeHeight(DependencyObject obj, double value)
            => obj.SetValue(BorderFadeHeightProperty, value);

        public static readonly DependencyProperty BorderFadeHeightProperty = DependencyProperty.RegisterAttached(
            "BorderFadeHeight",
            typeof(double),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(BorderFadeHeightProperty), defaultValue: UIKitConstants.ScrollViewerBorderFadeLength));

        #endregion

        #region BorderFadeGradient

        public static GradientStopCollection GetBorderFadeGradient(DependencyObject obj)
            => (GradientStopCollection)obj.GetValue(BorderFadeGradientProperty);

        public static void SetBorderFadeGradient(DependencyObject obj, GradientStopCollection value)
            => obj.SetValue(BorderFadeGradientProperty, value);

        public static readonly DependencyProperty BorderFadeGradientProperty = DependencyProperty.RegisterAttached(
            "BorderFadeGradient",
            typeof(GradientStopCollection),
            typeof(ScrollViewerInternals),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(BorderFadeGradientProperty), defaultValue: UIKitConstants.ScrollViewerBorderFadeGradient));

        #endregion

        private static void OnScrollViewerObserverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)d;

            scrollViewer.WhenLoaded(() =>
            {
                CreateBorderFadeOpacityMask(scrollViewer);
                ApplyBorderFade(scrollViewer);
            });

            scrollViewer.ScrollChanged += OnScrollChanged;
        }

        private static void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            ApplyBorderFade(scrollViewer);
        }

        private static void ApplyBorderFade(ScrollViewer scrollViewer)
        {
            var isBorderFadeEnabled = GetIsBorderFadeEnabled(scrollViewer);
            if (isBorderFadeEnabled == false)
            {
                SetBorderFadeState(scrollViewer, ScrollViewerBorderFadeState.None);
            }
            else
            {
                var hasTopOffset = scrollViewer.VerticalOffset > 0;
                var hasLeftOffset = scrollViewer.HorizontalOffset > 0;

                var hasBottomOffset = scrollViewer.VerticalOffset + scrollViewer.ViewportHeight < scrollViewer.ExtentHeight;
                var hasRightOffset = scrollViewer.HorizontalOffset + scrollViewer.ViewportWidth < scrollViewer.ExtentWidth;

                var fadeState = ScrollViewerBorderFadeState.None;

                if (hasTopOffset)
                {
                    fadeState |= ScrollViewerBorderFadeState.Top;
                }

                if (hasBottomOffset)
                {
                    fadeState |= ScrollViewerBorderFadeState.Bottom;
                }

                if (hasLeftOffset)
                {
                    fadeState |= ScrollViewerBorderFadeState.Left;
                }

                if (hasRightOffset)
                {
                    fadeState |= ScrollViewerBorderFadeState.Right;
                }

                SetBorderFadeState(scrollViewer, fadeState);
            }
        }

        private static void CreateBorderFadeOpacityMask(ScrollViewer scrollViewer)
        {
            var isBorderFadeEnabled = GetIsBorderFadeEnabled(scrollViewer);
            if (!isBorderFadeEnabled)
            {
                return;
            }

            var fadeLayer = scrollViewer.FindVisualChildren<Panel>().FirstOrDefault(p => p.Name == PART_ScrollContentPresenterContainer);
            if (fadeLayer != null)
            {
                var topLeftMask = CreateRectangle(
                    source: scrollViewer,
                    column: 0,
                    row: 0,
                    gradientBrush: new RadialGradientBrush()
                    {
                        Center = new Point(1, 1),
                        GradientOrigin = new Point(1, 1),
                        RadiusX = 1,
                        RadiusY = 1
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Left | ScrollViewerBorderFadeState.Top)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var topRightMask = CreateRectangle(
                    source: scrollViewer,
                    column: 2,
                    row: 0,
                    gradientBrush: new RadialGradientBrush()
                    {
                        Center = new Point(0, 1),
                        GradientOrigin = new Point(0, 1),
                        RadiusX = 1,
                        RadiusY = 1
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Right | ScrollViewerBorderFadeState.Top)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var bottomLeftMask = CreateRectangle(
                    source: scrollViewer,
                    column: 0,
                    row: 2,
                    gradientBrush: new RadialGradientBrush()
                    {
                        Center = new Point(1, 0),
                        GradientOrigin = new Point(1, 0),
                        RadiusX = 1,
                        RadiusY = 1
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Left | ScrollViewerBorderFadeState.Bottom)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var bottomRightMask = CreateRectangle(
                    source: scrollViewer,
                    column: 2,
                    row: 2,
                    gradientBrush: new RadialGradientBrush()
                    {
                        Center = new Point(0, 0),
                        GradientOrigin = new Point(0, 0),
                        RadiusX = 1,
                        RadiusY = 1
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Right | ScrollViewerBorderFadeState.Bottom)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var leftMask = CreateRectangle(
                    source: scrollViewer,
                    column: 0,
                    row: 1,
                    gradientBrush: new LinearGradientBrush()
                    {
                        EndPoint = new Point(0, 0),
                        StartPoint = new Point(1, 0)
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Left)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var rightMask = CreateRectangle(
                    source: scrollViewer,
                    column: 2,
                    row: 1,
                    gradientBrush: new LinearGradientBrush()
                    {
                        EndPoint = new Point(1, 0),
                        StartPoint = new Point(0, 0)
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Right)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var topMask = CreateRectangle(
                    source: scrollViewer,
                    column: 1,
                    row: 0,
                    gradientBrush: new LinearGradientBrush()
                    {
                        EndPoint = new Point(0, 0),
                        StartPoint = new Point(0, 1)
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Top)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var bottomMask = CreateRectangle(
                    source: scrollViewer,
                    column: 1,
                    row: 2,
                    gradientBrush: new LinearGradientBrush()
                    {
                        EndPoint = new Point(0, 1),
                        StartPoint = new Point(0, 0)
                    },
                    visibilityConverter: new DelegateConverter<ScrollViewerBorderFadeState>(value =>
                    {
                        return EnumOperations.HasFlag(value, ScrollViewerBorderFadeState.Bottom)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    }));

                var middleMask = new Rectangle();
                middleMask.SetValue(Grid.RowProperty, 1);
                middleMask.SetValue(Grid.ColumnProperty, 1);
                middleMask.SetValue(Rectangle.FillProperty, Brushes.Black);

                var opacityMaskGrid = new Grid();
                opacityMaskGrid.SetBinding(Grid.HeightProperty, new Binding() { Source = scrollViewer, Path = ScrollViewer.ActualHeightProperty.AsPath() });
                opacityMaskGrid.SetBinding(Grid.WidthProperty, new Binding() { Source = scrollViewer, Path = ScrollViewer.ActualWidthProperty.AsPath() });
                opacityMaskGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                opacityMaskGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                opacityMaskGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                opacityMaskGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
                opacityMaskGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                opacityMaskGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
                opacityMaskGrid.Children.Add(topLeftMask);
                opacityMaskGrid.Children.Add(topRightMask);
                opacityMaskGrid.Children.Add(bottomLeftMask);
                opacityMaskGrid.Children.Add(bottomRightMask);
                opacityMaskGrid.Children.Add(leftMask);
                opacityMaskGrid.Children.Add(rightMask);
                opacityMaskGrid.Children.Add(topMask);
                opacityMaskGrid.Children.Add(bottomMask);
                opacityMaskGrid.Children.Add(middleMask);

                fadeLayer.OpacityMask = new VisualBrush
                {
                    Visual = opacityMaskGrid
                };
            }
        }

        private static Rectangle CreateRectangle(ScrollViewer source, int column, int row, GradientBrush gradientBrush, IValueConverter visibilityConverter)
        {
            var maskWidth = (double)source.GetValue(BorderFadeWidthProperty);
            var maskHeight = (double)source.GetValue(BorderFadeHeightProperty);
            var gradientStops = ((GradientStopCollection)source.GetValue(BorderFadeGradientProperty)).Clone();

            gradientBrush.GradientStops = gradientStops;

            var rect = new Rectangle();
            rect.SetValue(Grid.ColumnProperty, column);
            rect.SetValue(Grid.RowProperty, row);
            if (column != 1)
            {
                rect.SetValue(Rectangle.WidthProperty, maskWidth);
            }

            if (row != 1)
            {
                rect.SetValue(Rectangle.HeightProperty, maskHeight);
            }

            rect.SetValue(Rectangle.FillProperty, gradientBrush);
            rect.SetBinding(Rectangle.VisibilityProperty, new Binding()
            {
                Source = source,
                Path = _borderFadeStatePropertyKey.DependencyProperty.AsPath(),
                Converter = visibilityConverter
            });

            return rect;
        }

        private static void HandleMouseWheelScroll(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = Guard.EnsureIsInstanceOfType<ScrollViewer>(sender);

            var canMouseWheelScroll = scrollViewer.GetValue<bool>(CanMouseWheelScrollProperty);
            if (canMouseWheelScroll == false)
            {
                e.Handled = true;

                RaiseParentScroll(scrollViewer, e);
            }

            var mouseWheelScrollOrientation = scrollViewer.GetValue<Orientation>(MouseWheelScrollOrientationProperty);
            if (mouseWheelScrollOrientation == Orientation.Horizontal)
            {
                e.Handled = true;

                if (scrollViewer.CanScrollLeft() || scrollViewer.CanScrollRight())
                {
                    const int minHorizontalScrollLine = 2;

                    var lineCount = Math.Max(Math.Abs(e.Delta) / Mouse.MouseWheelDeltaForOneLine, minHorizontalScrollLine);

                    if (e.Delta < 0)
                    {
                        Enumerable.Range(0, lineCount).ForEach(arg => scrollViewer.LineRight());
                    }
                    else
                    {
                        Enumerable.Range(0, lineCount).ForEach(arg => scrollViewer.LineLeft());
                    }
                }
                else
                {
                    RaiseParentScroll(scrollViewer, e);
                }
            }
        }

        private static void RaiseParentScroll(ScrollViewer sender, MouseWheelEventArgs eventArgs)
        {
            if (sender.Parent is UIElement parent)
            {
                parent.RaiseEvent(new MouseWheelEventArgs(eventArgs.MouseDevice, eventArgs.Timestamp, eventArgs.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                });
            }
        }
    }
}
