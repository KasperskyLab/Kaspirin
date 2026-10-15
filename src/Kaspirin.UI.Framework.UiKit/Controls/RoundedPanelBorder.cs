// Copyright © 2026 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Controls;

internal sealed class RoundedPanelBorder : Border
{
    #region BorderDashArray

    public DoubleCollection? BorderDashArray
    {
        get => (DoubleCollection?)GetValue(BorderDashArrayProperty);
        set => SetValue(BorderDashArrayProperty, value);
    }

    public static readonly DependencyProperty BorderDashArrayProperty = DependencyProperty.Register(
        nameof(BorderDashArray),
        typeof(DoubleCollection),
        typeof(RoundedPanelBorder),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    #endregion

    protected override void OnRender(DrawingContext dc)
    {
        var dashArray = BorderDashArray;
        if (dashArray == null || dashArray.Count == 0)
        {
            base.OnRender(dc);
            return;
        }

        var thickness = BorderThickness.Left;

        if (Background != null)
        {
            dc.DrawGeometry(Background, null, CreateRoundedRectangleGeometry(RenderSize, CornerRadius, thickness));
        }

        if (BorderBrush == null || thickness <= 0)
        {
            return;
        }

        var pen = new Pen(BorderBrush, thickness)
        {
            DashStyle = new DashStyle(dashArray, 0)
        };

        if (pen.CanFreeze)
        {
            pen.Freeze();
        }

        dc.DrawGeometry(null, pen, CreateRoundedRectangleGeometry(RenderSize, CornerRadius, thickness / 2));
    }

    private static Geometry? CreateRoundedRectangleGeometry(Size size, CornerRadius cornerRadius, double inset)
    {
        var left = inset;
        var top = inset;
        var right = size.Width - inset;
        var bottom = size.Height - inset;

        if (right <= left || bottom <= top)
        {
            return null;
        }

        var topLeft = Math.Max(0, cornerRadius.TopLeft - inset);
        var topRight = Math.Max(0, cornerRadius.TopRight - inset);
        var bottomRight = Math.Max(0, cornerRadius.BottomRight - inset);
        var bottomLeft = Math.Max(0, cornerRadius.BottomLeft - inset);

        var figure = new PathFigure()
        {
            StartPoint = new Point(left, top + topLeft),
            IsClosed = true
        };

        figure.Segments.Add(new ArcSegment()
        {
            Point = new Point(left + topLeft, top),
            SweepDirection = SweepDirection.Clockwise,
            Size = new Size(topLeft, topLeft)
        });

        figure.Segments.Add(new LineSegment()
        {
            Point = new Point(right - topRight, top)
        });

        figure.Segments.Add(new ArcSegment()
        {
            Point = new Point(right, top + topRight),
            SweepDirection = SweepDirection.Clockwise,
            Size = new Size(topRight, topRight)
        });

        figure.Segments.Add(new LineSegment()
        {
            Point = new Point(right, bottom - bottomRight)
        });

        figure.Segments.Add(new ArcSegment()
        {
            Point = new Point(right - bottomRight, bottom),
            SweepDirection = SweepDirection.Clockwise,
            Size = new Size(bottomRight, bottomRight)
        });

        figure.Segments.Add(new LineSegment()
        {
            Point = new Point(left + bottomLeft, bottom)
        });

        figure.Segments.Add(new ArcSegment()
        {
            Point = new Point(left, bottom - bottomLeft),
            SweepDirection = SweepDirection.Clockwise,
            Size = new Size(bottomLeft, bottomLeft)
        });

        var pathGeometry = new PathGeometry();
        pathGeometry.Figures.Add(figure);
        pathGeometry.Freeze();

        return pathGeometry;
    }
}
