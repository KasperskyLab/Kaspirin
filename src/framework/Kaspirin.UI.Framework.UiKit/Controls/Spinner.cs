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
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_LoaderIcon, Type = typeof(Image))]
    public class Spinner : Control
    {
        public const string PART_LoaderIcon = "PART_LoaderIcon";

        public Spinner()
        {
            var animationSettings = ServiceLocator.Instance.GetService<IAnimationSettingsProvider>();
            var frameRate = animationSettings.GetDesiredFrameRate(AnimationRenderQuality.Low);
            if (frameRate == null || frameRate <= 0)
            {
                throw new Exception(
                    $"Unexpected value provided from {nameof(IAnimationSettingsProvider)}.{nameof(IAnimationSettingsProvider.GetDesiredFrameRate)}. " +
                    $"FrameRate must be greater than zero.");
            }

            _frameRate = frameRate.Value;
            _animationTimerInterval = TimeSpan.FromSeconds(1D / _frameRate);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild(PART_LoaderIcon) is not Image loaderIcon)
            {
                return;
            }

            _rotateTransform = new RotateTransform();
            loaderIcon.RenderTransform = _rotateTransform;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (!ReferenceEquals(e.Property, IsVisibleProperty))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                SetDispatcherTimer();
            }
            else
            {
                StopDispatcherTimer();
            }
        }

        private void SetDispatcherTimer()
        {
            if (_animationTimer is null)
            {
                _animationTimer = new DispatcherTimer(DispatcherPriority.Render) { Interval = _animationTimerInterval };
                _animationTimer.Start();
            }

            _animationTimer.Tick -= OnAnimationTimerTick;
            _animationTimer.Tick += OnAnimationTimerTick;
            _activeSpinnersCount++;
        }

        private void StopDispatcherTimer()
        {
            if (_animationTimer == null)
            {
                return;
            }

            _activeSpinnersCount--;
            _animationTimer.Tick -= OnAnimationTimerTick;
            if (_animationTimer.IsEnabled && _activeSpinnersCount == 0)
            {
                _animationTimer.Stop();
                _animationTimer = null;
            }
        }

        private void OnAnimationTimerTick(object? sender, EventArgs e)
        {
            if (_rotateTransform == null)
            {
                return;
            }

            _spinCount = ++_spinCount % _frameRate;
            _rotateTransform.Angle = 360D / _frameRate * _spinCount;
        }

        private RotateTransform? _rotateTransform;

        private int _spinCount;

        private readonly int _frameRate;
        private readonly TimeSpan _animationTimerInterval;

        private static DispatcherTimer? _animationTimer;
        private static int _activeSpinnersCount = 0;
    }
}
