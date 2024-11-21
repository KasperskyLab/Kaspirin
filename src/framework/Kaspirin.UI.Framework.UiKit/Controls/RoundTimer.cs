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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class RoundTimer : Control
    {
        public RoundTimer()
        {
            _storyboard = new();
            _storyboard.SetFrameRate();

            Loaded += OnLoaded;
            IsEnabledChanged += OnIsEnabledChanged;
        }

        public override void OnApplyTemplate()
        {
            var animatedIndicator = GetTemplateChild("PART_AnimatedIndicator") as Path;
            if (animatedIndicator != null)
            {
                var animationBinding = new MultiBinding();
                animationBinding.Bindings.Add(new Binding() { Source = this, Path = DiameterProperty.AsPath() });
                animationBinding.Bindings.Add(new Binding() { Source = this, Path = DurationProperty.AsPath() });
                animationBinding.Bindings.Add(new Binding() { Source = this, Path = _remainingTicksProperty.AsPath() });
                animationBinding.Converter = new PieConverter(this);

                animatedIndicator.SetBinding(Path.DataProperty, animationBinding);
                animatedIndicator.SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
            }
        }

        #region Type

        public RoundTimerType Type
        {
            get { return (RoundTimerType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(RoundTimerType), typeof(RoundTimer), new PropertyMetadata(RoundTimerType.Positive));

        #endregion

        #region IsPaused

        public bool IsPaused
        {
            get { return (bool)GetValue(IsPausedProperty); }
            set { SetValue(IsPausedProperty, value); }
        }

        public static readonly DependencyProperty IsPausedProperty =
            DependencyProperty.Register("IsPaused", typeof(bool), typeof(RoundTimer),
                new PropertyMetadata(false, OnIsPausedChanged));

        private static void OnIsPausedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RoundTimer)d).InvalidateAnimation();
        }

        #endregion

        #region Duration

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(RoundTimer),
                new PropertyMetadata(TimeSpan.FromSeconds(30), OnDurationChanged, CoerceDuration));

        private static void OnDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RoundTimer)d).StartTimer();
        }

        private static object CoerceDuration(DependencyObject d, object baseValue)
        {
            var roundTimer = (RoundTimer)d;
            var duration = (TimeSpan)baseValue;

            if (roundTimer.Remaining > duration)
            {
                roundTimer.Remaining = duration;
            }

            return duration;
        }

        #endregion

        #region Remaining

        public TimeSpan Remaining
        {
            get { return (TimeSpan)GetValue(RemainingProperty); }
            set { SetValue(RemainingProperty, value); }
        }

        public static readonly DependencyProperty RemainingProperty =
            DependencyProperty.Register("Remaining", typeof(TimeSpan), typeof(RoundTimer),
                new FrameworkPropertyMetadata(TimeSpan.FromSeconds(30), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRemainingChanged, CoerceRemaining));

        private static void OnRemainingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RoundTimer)d).StartTimer();
        }

        private static object CoerceRemaining(DependencyObject d, object baseValue)
        {
            var roundTimer = (RoundTimer)d;

            var duration = roundTimer.Duration;
            var remaining = (TimeSpan)baseValue;

            return TimeSpan.FromTicks(Math.Min(duration.Ticks, remaining.Ticks));
        }

        #endregion

        #region RemainingTicks

        private static readonly DependencyProperty _remainingTicksProperty = DependencyProperty.Register("RemainingTicks", typeof(long), typeof(RoundProgress));

        #endregion

        #region Diameter

        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(double), typeof(RoundTimer));

        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        #endregion

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StartTimer();
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            InvalidateAnimation();
        }

        private void InvalidateAnimation()
        {
            if (_isAnimationUpdate)
            {
                return;
            }

            if (IsEnabled && !IsPaused)
            {
                ResumeTimer();
            }
            else
            {
                PauseTimer();
            }
        }

        private void StartTimer()
        {
            if (_isAnimationUpdate)
            {
                return;
            }

            if (IsLoaded)
            {
                var animation = CreateAnimation();

                _storyboard.Remove();
                _storyboard.Children.Clear();

                _storyboard.Children.Add(animation);
                _storyboard.Begin();

                Executers.InUiAsync(InvalidateAnimation, System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private void PauseTimer()
        {
            if (_storyboard.GetIsPaused() == false)
            {
                _storyboard.Pause();
            }
        }

        private void ResumeTimer()
        {
            if (_storyboard.GetIsPaused())
            {
                _storyboard.Resume();
            }
        }

        private Int64Animation CreateAnimation()
        {
            var animation = new Int64Animation
            {
                From = Remaining.Ticks,
                To = 0,
                FillBehavior = FillBehavior.HoldEnd,
                Duration = new Duration(Remaining)
            };

            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, _remainingTicksPropertyPath);
            animation.Freeze();

            return animation;
        }

        private readonly PropertyPath _remainingTicksPropertyPath = new(_remainingTicksProperty);
        private readonly Storyboard _storyboard = new();
        private bool _isAnimationUpdate;

        private sealed class PieConverter : IMultiValueConverter
        {
            public PieConverter(RoundTimer roundTimer)
            {
                _roundTimer = roundTimer;
            }

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                var origin = (double)values[0] / 2;
                var duration = (TimeSpan)values[1];
                var remainingTicks = (long)values[2];

                var angle = remainingTicks * 100.0 / duration.Ticks / 100.0 * 359.999;

                var circleRadius = new Size(origin, origin);
                var circleCenter = new Point(origin, origin);

                var circleStartPosition = PieConverter.GetPointForAngle(origin, circleRadius, 0);
                var circleEndPosition = PieConverter.GetPointForAngle(origin, circleRadius, angle);

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
                    IsClosed = false,
                    StartPoint = circleCenter,
                    Segments = new PathSegmentCollection()
                    {
                        new LineSegment(circleStartPosition, true),
                        arcSegment,
                        new LineSegment(circleCenter, true)
                    },
                };

                var pathGeometry = new PathGeometry();
                pathGeometry.Figures.Add(pathFigure);
                pathGeometry.Transform = new ScaleTransform(-1, 1, circleRadius.Width, 0);

                if (_roundTimer.IsLoaded)
                {
                    _roundTimer._isAnimationUpdate = true;
                    _roundTimer.Remaining = TimeSpan.FromTicks(remainingTicks);
                    _roundTimer._isAnimationUpdate = false;
                }

                return pathGeometry;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            private static Point GetPointForAngle(double origin, Size radiusInSize, double angle)
            {
                var radius = radiusInSize.Height;

                angle = angle == 360 ? 359.99 : angle;
                var angleInRadians = angle * Math.PI / 180;

                var px = Math.Sin(angleInRadians) * radius + origin;
                var py = -Math.Cos(angleInRadians) * radius + origin;

                return new Point(px, py);
            }

            private readonly RoundTimer _roundTimer;
        }
    }
}
