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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors
{
    public static class WindowResizeBehavior
    {
        #region IsWindowResizeLayer

        public static bool GetIsWindowResizeLayer(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsWindowResizeLayerProperty);
        }

        public static void SetIsWindowResizeLayer(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWindowResizeLayerProperty, value);
        }

        public static readonly DependencyProperty IsWindowResizeLayerProperty =
            DependencyProperty.RegisterAttached("IsWindowResizeLayer", typeof(bool), typeof(WindowResizeBehavior),
                new PropertyMetadata(false, OnIsWindowResizeLayerChanged));

        private static void OnIsWindowResizeLayerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as Panel;
            if (panel == null)
            {
                return;
            }

            var isEnable = (bool)e.NewValue;
            if (isEnable)
            {
                panel.WhenLoaded(() => Initialize(panel));
            }
            else
            {
                panel.WhenLoaded(() => Deinitialize(panel));
            }
        }

        #endregion

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WindowResizeBehavior));

        public static void SetIsEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }

        public static bool GetIsEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        #endregion

        private static void Initialize(Panel panel)
        {
            var windowResizeBorder = panel.FindVisualChild<WindowResizeBorder>();
            if (windowResizeBorder == null)
            {
                windowResizeBorder = new WindowResizeBorder(panel);

                panel.Children.Add(windowResizeBorder);
            }
        }

        private static void Deinitialize(Panel panel)
        {
            var windowResizeBorder = panel.FindVisualChild<WindowResizeBorder>();
            if (windowResizeBorder != null)
            {
                panel.Children.Remove(windowResizeBorder);
            }
        }

        private sealed class WindowResizeBorder : Grid
        {
            public WindowResizeBorder(Panel panel)
            {
                Guard.ArgumentIsNotNull(panel);

                SetValue(Grid.ColumnSpanProperty, int.MaxValue);
                SetValue(Grid.RowSpanProperty, int.MaxValue);
                SetValue(Panel.ZIndexProperty, int.MaxValue);

                _window = panel.GetWindow();

                AddRectangles(this);

                SetBinding(IsActiveProperty, new MultiBinding()
                {
                    Bindings =
                    {
                        new Binding() { Path = Window.WindowStateProperty.AsPath(), Source = _window },
                        new Binding() { Path = Window.ResizeModeProperty.AsPath(), Source = _window },
                        new Binding() { Path = WindowResizeBehavior.IsEnabledProperty.AsPath(), Source = _window },
                    },
                    Converter = new DelegateMultiConverter(values =>
                    {
                        var windowState = (WindowState)values[0]!;
                        var resizeMode = (ResizeMode)values[1]!;
                        var isEnabled = (bool)values[2]!;

                        return isEnabled && windowState != WindowState.Maximized && resizeMode == ResizeMode.CanResize;
                    })
                });

                FlowDirection = FlowDirection.LeftToRight;
            }

            #region IsActive

            public bool IsActive
            {
                get { return (bool)GetValue(IsActiveProperty); }
                set { SetValue(IsActiveProperty, value); }
            }

            public static readonly DependencyProperty IsActiveProperty =
                DependencyProperty.Register("IsActive", typeof(bool), typeof(WindowResizeBorder),
                    new PropertyMetadata(false, OnIsActiveChanged));

            private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowResizeBorder)d).UpdateState();
            }

            #endregion

            private void UpdateState()
            {
                foreach (var rectangle in Children.Cast<Rectangle>())
                {
                    rectangle.MouseEnter -= OnMouseEnter;
                    rectangle.MouseLeave -= OnMouseLeave;
                    rectangle.PreviewMouseDown -= OnPreviewMouseDown;

                    if (IsActive)
                    {
                        rectangle.MouseEnter += OnMouseEnter;
                        rectangle.MouseLeave += OnMouseLeave;
                        rectangle.PreviewMouseDown += OnPreviewMouseDown;
                    }
                }

                IsHitTestVisible = IsActive;
            }

            private void OnMouseEnter(object sender, MouseEventArgs e)
            {
                var direction = GetDirection(sender);

                SetCursor(direction);
            }

            private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
                var direction = GetDirection(sender);

                SetCursor(direction);

                var hwnd = _window.GetHandle();
                if (hwnd != IntPtr.Zero)
                {
                    hwnd.ResizeWindow(direction);

                    e.Handled = true;
                }

                ResetCursor();
            }

            private void OnMouseLeave(object sender, MouseEventArgs e)
            {
                ResetCursor();
            }

            private static WindowResizeDirection GetDirection(object sender)
            {
                var rectangle = (Rectangle)sender;

                return (WindowResizeDirection)rectangle.Tag;
            }

            private void ResetCursor()
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                {
                    _window.Cursor = Cursors.Arrow;
                }
            }

            private void SetCursor(WindowResizeDirection direction)
            {
                _window.Cursor = GetResizeCursor(direction);
            }

            private static void AddRectangles(Panel panel)
            {
                var top = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.Top,
                    Width = double.NaN,
                    Height = 5,
                    Margin = new Thickness(10, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Top
                };

                var bottom = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.Bottom,
                    Width = double.NaN,
                    Height = 5,
                    Margin = new Thickness(10, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                var right = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.Right,
                    Width = 5,
                    Height = double.NaN,
                    Margin = new Thickness(0, 10, 0, 10),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Stretch
                };

                var left = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.Left,
                    Width = 5,
                    Height = double.NaN,
                    Margin = new Thickness(0, 10, 0, 10),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Stretch
                };

                var topLeft = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.TopLeft,
                    Width = 10,
                    Height = 10,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };

                var topRight = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.TopRight,
                    Width = 10,
                    Height = 10,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top
                };

                var bottomLeft = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.BottomLeft,
                    Width = 10,
                    Height = 10,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                var bottomRight = new Rectangle
                {
                    Fill = Brushes.Transparent,
                    Tag = WindowResizeDirection.BottomRight,
                    Width = 10,
                    Height = 10,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                panel.Children.Add(top);
                panel.Children.Add(bottom);
                panel.Children.Add(right);
                panel.Children.Add(left);
                panel.Children.Add(topLeft);
                panel.Children.Add(topRight);
                panel.Children.Add(bottomLeft);
                panel.Children.Add(bottomRight);
            }

            private static Cursor GetResizeCursor(WindowResizeDirection direction)
            {
                return direction switch
                {
                    WindowResizeDirection.Top => Cursors.SizeNS,
                    WindowResizeDirection.Bottom => Cursors.SizeNS,
                    WindowResizeDirection.Left => Cursors.SizeWE,
                    WindowResizeDirection.Right => Cursors.SizeWE,
                    WindowResizeDirection.TopLeft => Cursors.SizeNWSE,
                    WindowResizeDirection.TopRight => Cursors.SizeNESW,
                    WindowResizeDirection.BottomLeft => Cursors.SizeNESW,
                    WindowResizeDirection.BottomRight => Cursors.SizeNWSE,
                    _ => Cursors.Arrow,
                };
            }

            private readonly Window _window;
        }
    }
}
