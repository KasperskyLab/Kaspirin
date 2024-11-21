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
using System.Windows.Media.Animation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = "PART_AnimatedIndicator", Type = typeof(FrameworkElement))]
    public class ProgressBar : System.Windows.Controls.ProgressBar
    {
        public ProgressBar()
        {
            _storyboard = new();
            _storyboard.SetFrameRate();

            Loaded += OnLoaded;
            ValueChanged += OnValueChanged;
            SizeChanged += OnSizeChanged;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate(); //Apply PART_GlowRect, PART_Track, PART_Indicator here

            _animatedIndicator = GetTemplateChild("PART_AnimatedIndicator") as FrameworkElement;
        }

        #region State

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(string), typeof(ProgressBar));

        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        #endregion

        #region Estimation

        public static readonly DependencyProperty EstimationProperty =
            DependencyProperty.Register("Estimation", typeof(string), typeof(ProgressBar));

        public string Estimation
        {
            get { return (string)GetValue(EstimationProperty); }
            set { SetValue(EstimationProperty, value); }
        }

        #endregion

        #region ShowHighlight
        public bool ShowHighlight
        {
            get { return (bool)GetValue(ShowHighlightProperty); }
            set { SetValue(ShowHighlightProperty, value); }
        }

        public static readonly DependencyProperty ShowHighlightProperty =
            DependencyProperty.Register("ShowHighlight", typeof(bool), typeof(ProgressBar), new PropertyMetadata(true));
        #endregion

        #region ShowValue

        public static readonly DependencyProperty ShowValueProperty =
            DependencyProperty.Register("ShowValue", typeof(bool), typeof(ProgressBar), new PropertyMetadata(true));

        public bool ShowValue
        {
            get { return (bool)GetValue(ShowValueProperty); }
            set { SetValue(ShowValueProperty, value); }
        }

        #endregion

        #region ShowState

        public static readonly DependencyProperty ShowStateProperty =
            DependencyProperty.Register("ShowState", typeof(bool), typeof(ProgressBar), new PropertyMetadata(true));

        public bool ShowState
        {
            get { return (bool)GetValue(ShowStateProperty); }
            set { SetValue(ShowStateProperty, value); }
        }

        #endregion

        #region ShowEstimation

        public static readonly DependencyProperty ShowEstimationProperty =
            DependencyProperty.Register("ShowEstimation", typeof(bool), typeof(ProgressBar));

        public bool ShowEstimation
        {
            get { return (bool)GetValue(ShowEstimationProperty); }
            set { SetValue(ShowEstimationProperty, value); }
        }

        #endregion

        #region CanRollback
        public bool CanRollback
        {
            get { return (bool)GetValue(CanRollbackProperty); }
            set { SetValue(CanRollbackProperty, value); }
        }

        public static readonly DependencyProperty CanRollbackProperty =
            DependencyProperty.Register("CanRollback", typeof(bool), typeof(ProgressBar), new PropertyMetadata(false));
        #endregion

        #region Type

        public ProgressBarType Type
        {
            get { return (ProgressBarType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ProgressBarType), typeof(ProgressBar), new PropertyMetadata(ProgressBarType.Positive));

        #endregion

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_animatedIndicator != null)
            {
                ChangeValuePermanent();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ChangeValuePermanent();
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue.LargerOrNearlyEqual(e.OldValue) || CanRollback)
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
            if (_animatedIndicator != null)
            {
                _storyboard.Stop();
                _animatedIndicator.Width = ActualWidth / Maximum * Value;
                _storyboard.Resume();
            }
        }

        private void ChangeValueSmooth()
        {
            if (_animatedIndicator != null)
            {
                var animation = CreateAnimation(_animatedIndicator);

                _storyboard.Remove();
                _storyboard.Children.Clear();

                _storyboard.Children.Add(animation);
                _storyboard.Begin();
            }
        }

        private DoubleAnimation CreateAnimation(FrameworkElement animatedIndicator)
        {
            var animation = new DoubleAnimation
            {
                From = animatedIndicator.ActualWidth,
                To = ActualWidth / Maximum * Value,
                FillBehavior = FillBehavior.HoldEnd,
                Duration = _animationDuration.CoerceDuration()
            };

            animation.SetValue(Storyboard.TargetProperty, animatedIndicator);
            animation.SetValue(Storyboard.TargetPropertyProperty, WidthProperty.AsPath());
            animation.Freeze();

            return animation;
        }

        private readonly Storyboard _storyboard;

        private FrameworkElement? _animatedIndicator;

        private static readonly Duration _animationDuration = new(TimeSpan.FromMilliseconds(500));
    }
}
