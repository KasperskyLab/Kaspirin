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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_Panel, Type = typeof(FrameworkElement))]
    public sealed class RoundedPanel : ContentControl
    {
        public const string PART_Panel = "PART_Panel";

        static RoundedPanel()
        {
            BorderThicknessProperty.OverrideMetadata(
                typeof(RoundedPanel),
                new FrameworkPropertyMetadata(default(Thickness), (d, _) => ((RoundedPanel)d).UpdatePanelClip()));
        }

        public RoundedPanel()
        {
            SizeChanged += OnSizeChanged;
            Loaded += OnLoaded;
        }

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(RoundedPanel),
            new PropertyMetadata(default(CornerRadius), OnCornerRadiusChanged));

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((RoundedPanel)d).UpdatePanelClip();

        #endregion

        public override void OnApplyTemplate()
        {
            _panel = GetTemplateChild(PART_Panel) as FrameworkElement;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdatePanelClip();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePanelClip();
        }

        private void UpdatePanelClip()
        {
            if (_panel == null)
            {
                return;
            }

            if (CornerRadius == default)
            {
                _panel.Clip = null;
                return;
            }

            _panel.Clip = GetClipFigure(_panel.ActualWidth, _panel.ActualHeight, CornerRadius, BorderThickness);
        }

        private static Geometry GetClipFigure(double width, double height, CornerRadius cornerRadius, Thickness borderThickness)
        {
            var clipRadius = GetClipRadius(cornerRadius, borderThickness);

            var figure = new PathFigure()
            {
                StartPoint = new Point(0, clipRadius.TopLeft),
                IsClosed = true
            };

            figure.Segments.Add(new ArcSegment()
            {
                Point = new Point(clipRadius.TopLeft, 0),
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(clipRadius.TopLeft, clipRadius.TopLeft)
            });

            figure.Segments.Add(new LineSegment()
            {
                Point = new Point(width - clipRadius.TopRight, 0)
            });

            figure.Segments.Add(new ArcSegment()
            {
                Point = new Point(width, clipRadius.TopRight),
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(clipRadius.TopRight, clipRadius.TopRight)
            });

            figure.Segments.Add(new LineSegment()
            {
                Point = new Point(width, height - clipRadius.BottomRight)
            });

            figure.Segments.Add(new ArcSegment()
            {
                Point = new Point(width - clipRadius.BottomRight, height),
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(clipRadius.BottomRight, clipRadius.BottomRight)
            });

            figure.Segments.Add(new LineSegment()
            {
                Point = new Point(clipRadius.BottomLeft, height)
            });

            figure.Segments.Add(new ArcSegment()
            {
                Point = new Point(0, height - clipRadius.BottomLeft),
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(clipRadius.BottomLeft, clipRadius.BottomLeft)
            });

            figure.Segments.Add(new LineSegment()
            {
                Point = new Point(0, clipRadius.TopLeft)
            });

            var pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(figure);

            return pathGeometry;
        }

        private static CornerRadius GetClipRadius(CornerRadius cornerRadius, Thickness borderThickness)
        {
            var correction = Math.Max(0, borderThickness.Top - 1);

            return new CornerRadius()
            {
                BottomLeft = cornerRadius.BottomLeft + correction,
                BottomRight = cornerRadius.BottomRight + correction,
                TopLeft = cornerRadius.TopLeft + correction,
                TopRight = cornerRadius.TopRight + correction,
            };
        }

        private FrameworkElement? _panel;
    }
}
