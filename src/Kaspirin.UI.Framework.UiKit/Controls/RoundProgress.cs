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
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class RoundProgress : System.Windows.Controls.ProgressBar
{
    public RoundProgress()
    {
        _anglePropertyPath = new(_angleProperty);

        _storyboard = new();
        _storyboard.SetFrameRate();

        Loaded += OnLoaded;
        ValueChanged += OnValueChanged;
        SizeChanged += OnSizeChanged;
    }

    public override void OnApplyTemplate()
    {
        var animatedIndicator = GetTemplateChild("PART_AnimatedIndicator") as Path;
        if (animatedIndicator != null)
        {
            var animationBinding = new MultiBinding();
            animationBinding.Bindings.Add(new Binding() { Source = this, Path = DiameterProperty.AsPath() });
            animationBinding.Bindings.Add(new Binding() { Source = this, Path = StrokeThicknessProperty.AsPath() });
            animationBinding.Bindings.Add(new Binding() { Source = this, Path = _angleProperty.AsPath() });
            animationBinding.Converter = new ArcConverter();

            animatedIndicator.SetBinding(Path.DataProperty, animationBinding);
            animatedIndicator.SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
        }

        var track = GetTemplateChild("PART_Track") as Path;
        if (track != null)
        {
            var trackBinding = new MultiBinding();
            trackBinding.Bindings.Add(new Binding() { Source = this, Path = DiameterProperty.AsPath() });
            trackBinding.Bindings.Add(new Binding() { Source = this, Path = StrokeThicknessProperty.AsPath() });
            trackBinding.Converter = new ArcConverter();

            track.SetBinding(Path.DataProperty, trackBinding);
        }
    }

    #region ShowValue

    public bool ShowValue
    {
        get => (bool)GetValue(ShowValueProperty);
        set => SetValue(ShowValueProperty, value);
    }

    public static readonly DependencyProperty ShowValueProperty = DependencyProperty.Register(
        nameof(ShowValue),
        typeof(bool),
        typeof(RoundProgress),
        new PropertyMetadata(true));

    #endregion

    #region StrokeThickness

    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
        nameof(StrokeThickness),
        typeof(double),
        typeof(RoundProgress),
        new PropertyMetadata(default(double)));

    #endregion

    #region Angle

    private static readonly DependencyProperty _angleProperty = DependencyProperty.Register(
        "Angle",
        typeof(double),
        typeof(RoundProgress),
        new PropertyMetadata(default(double)));

    #endregion

    #region Diameter

    public double Diameter
    {
        get => (double)GetValue(DiameterProperty);
        set => SetValue(DiameterProperty, value);
    }

    public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register(
        nameof(Diameter),
        typeof(double),
        typeof(RoundProgress),
        new PropertyMetadata(default(double)));

    #endregion

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        ChangeValuePermanent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ChangeValuePermanent();
    }

    private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (e.NewValue.LargerOrNearlyEqual(e.OldValue))
        {
            ChangeValueSmooth();
        }
        else
        {
            ChangeValuePermanent();
        }
    }

    private void ChangeValuePermanent()
    {
        _storyboard.Stop();
        SetValue(_angleProperty, GetAngleForValue(Minimum, Maximum, Value));
        _storyboard.Resume();
    }

    private void ChangeValueSmooth()
    {
        var animation = CreateAnimation();

        _storyboard.Remove();
        _storyboard.Children.Clear();

        _storyboard.Children.Add(animation);
        _storyboard.Begin();
    }

    private DoubleAnimation CreateAnimation()
    {
        var animation = new DoubleAnimation
        {
            From = (double)GetValue(_angleProperty),
            To = GetAngleForValue(Minimum, Maximum, Value),
            FillBehavior = FillBehavior.HoldEnd,
            Duration = _animationDuration.CoerceDuration()
        };

        Storyboard.SetTarget(animation, this);
        Storyboard.SetTargetProperty(animation, _anglePropertyPath);
        animation.Freeze();

        return animation;
    }

    private static double GetAngleForValue(double minValue, double maxValue, double currentValue)
    {
        var percent = (currentValue - minValue) * 100 / (maxValue - minValue);
        var valueInAngle = percent / 100 * 359.999;
        return valueInAngle;
    }

    private sealed class ArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var angle = 360D;
            if (values.Length > 2)
            {
                angle = (double)values[2];
            }

            var origin = (double)values[0] / 2;

            var circleThickness = (double)values[1];
            var circleRadius = new Size(origin - circleThickness / 2, origin - circleThickness / 2);

            var circleStartPosition = GetPointForAngle(origin, circleRadius, 0);
            var circleEndPosition = GetPointForAngle(origin, circleRadius, angle);

            var arcSegment = new ArcSegment
            {
                RotationAngle = 0,
                SweepDirection = SweepDirection.Clockwise,
                IsLargeArc = angle > 180,
                Size = circleRadius,
                Point = circleEndPosition
            };

            var pathFigure = new PathFigure
            {
                StartPoint = circleStartPosition,
            };
            pathFigure.Segments.Add(arcSegment);

            var pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Point GetPointForAngle(double origin, Size radiusInSize, double angle)
        {
            var radius = radiusInSize.Height;

            angle = angle == 360 ? 359.99 : angle;
            var angleInRadians = angle * Math.PI / 180;

            var px = Math.Sin(angleInRadians) * radius + origin;
            var py = -Math.Cos(angleInRadians) * radius + origin;

            return new Point(px, py);
        }
    }

    private readonly PropertyPath _anglePropertyPath;
    private readonly Storyboard _storyboard;

    private static readonly Duration _animationDuration = new(TimeSpan.FromMilliseconds(500));
}
