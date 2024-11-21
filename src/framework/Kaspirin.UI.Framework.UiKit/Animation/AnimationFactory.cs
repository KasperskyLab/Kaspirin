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
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Animation
{
    internal static class AnimationFactory
    {
        internal static AnimationTimeline CreateAnimation(
            DependencyObject targetDependencyObject,
            DependencyProperty targetDependencyProperty,
            int? desiredFrameRate,
            AnimationProperties properties,
            object value)
        {
            Guard.ArgumentIsNotNull(targetDependencyObject);
            Guard.ArgumentIsNotNull(targetDependencyProperty);
            Guard.ArgumentIsNotNull(properties);
            Guard.ArgumentIsNotNull(value);

            AnimationTimeline animation;
            PropertyPath propertyPath;

            switch (value)
            {
                case int intValue:
                    animation = CreateIntAnimation(properties, intValue);
                    propertyPath = GetAnimationPropertyPath<int>(targetDependencyProperty);
                    break;

                case double doubleValue:
                    animation = CreateDoubleAnimation(properties, doubleValue);
                    propertyPath = GetAnimationPropertyPath<double>(targetDependencyProperty);
                    break;

                case SolidColorBrush solidColorBrushValue:
                    animation = CreateColorAnimation(properties, solidColorBrushValue.Clone().Color);
                    propertyPath = GetAnimationPropertyPath<SolidColorBrush>(targetDependencyProperty);
                    break;

                case Thickness thicknessValue:
                    animation = CreateThicknessAnimation(properties, thicknessValue);
                    propertyPath = GetAnimationPropertyPath<Thickness>(targetDependencyProperty);
                    break;

                default:
                    throw new NotImplementedException($"Unable to create animation to value '{value}' ({value.GetType().Name})");
            }

            animation.Duration = animation.Duration.CoerceDuration();

            animation.SetValue(Storyboard.TargetProperty, targetDependencyObject);
            animation.SetValue(Storyboard.TargetPropertyProperty, propertyPath);
            animation.SetValue(Timeline.DesiredFrameRateProperty, desiredFrameRate);

            animation.Freeze();

            return animation;
        }

        private static Int32Animation CreateIntAnimation(AnimationProperties properties, int value)
            => new()
            {
                BeginTime = properties.Delay,
                Duration = properties.Duration,
                EasingFunction = properties.Easing,
                To = value
            };

        private static DoubleAnimation CreateDoubleAnimation(AnimationProperties properties, double value)
            => new()
            {
                BeginTime = properties.Delay,
                Duration = properties.Duration,
                EasingFunction = properties.Easing,
                To = value
            };

        private static ColorAnimation CreateColorAnimation(AnimationProperties properties, Color value)
            => new()
            {
                BeginTime = properties.Delay,
                Duration = properties.Duration,
                EasingFunction = properties.Easing,
                To = value
            };

        private static ThicknessAnimation CreateThicknessAnimation(AnimationProperties properties, Thickness value)
            => new()
            {
                BeginTime = properties.Delay,
                Duration = properties.Duration,
                EasingFunction = properties.Easing,
                To = value
            };

        private static PropertyPath GetAnimationPropertyPath<TValue>(DependencyProperty dependencyProperty)
        {
            if (typeof(TValue) == typeof(SolidColorBrush))
            {
                return new PropertyPath($"(0).(1)", dependencyProperty, SolidColorBrush.ColorProperty);
            }
            else
            {
                return new PropertyPath(dependencyProperty);
            }
        }
    }
}
