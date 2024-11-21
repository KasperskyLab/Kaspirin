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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class DateTimePopupItemAnimation
    {
        public DateTimePopupItemAnimation(FrameworkElement target, double minOffset, double maxOffset)
        {
            _target = target;
            _minOffset = minOffset;
            _maxOffset = maxOffset;
            _lenght = Math.Abs(_minOffset - _maxOffset);
            _animationRequestTime = DateTime.MinValue;
        }

        public event Action Completed = delegate { };

        public void Requested()
        {
            _animationRequestTime = DateTime.Now;
        }

        public void Start(double currentOffset, double changeDelta)
        {
            var from = currentOffset;
            var to = currentOffset + changeDelta;

            var animation = new DoubleAnimation
            {
                From = from,
                To = to,
                BeginTime = TimeSpan.Zero,
                FillBehavior = FillBehavior.HoldEnd,
                Duration = CalculateDuration(from, to)
            };

            animation.SetValue(Storyboard.TargetPropertyProperty, _animationPath);
            animation.Completed += (s, e) => Completed();
            animation.Freeze();

            _storyboard.Children.Add(animation);
            _storyboard.Begin(_target, HandoffBehavior.Compose);
        }

        public void Hold(double currentOffset)
        {
            _storyboard.Remove(_target);
            _storyboard.Children.Clear();

            _target.BeginAnimation(DateTimePopupItemButton.CurrentOffsetProperty, null);
            _target.SetCurrentValue(DateTimePopupItemButton.CurrentOffsetProperty, currentOffset);
        }

        private Duration CalculateDuration(double from, double to)
        {
            var requestedSpeed = DateTime.Now.Ticks - _animationRequestTime.Ticks;
            var effectiveSpeed = Math.Max(_minAnimationDurationTicks, Math.Min(requestedSpeed, _maxAnimationDurationTicks));

            var duration = TimeSpan.FromTicks((long)(effectiveSpeed * Math.Abs(from - to) / _lenght));

            return duration.CoerceDuration();
        }

        private static readonly long _maxAnimationDurationTicks = TimeSpan.FromMilliseconds(700).Ticks;
        private static readonly long _minAnimationDurationTicks = TimeSpan.FromMilliseconds(100).Ticks;
        private readonly FrameworkElement _target;
        private readonly double _minOffset;
        private readonly double _maxOffset;
        private readonly double _lenght;

        private readonly PropertyPath _animationPath = new(DateTimePopupItemButton.CurrentOffsetProperty);
        private readonly Storyboard _storyboard = new();

        private DateTime _animationRequestTime;
    }
}
