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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace Kaspirin.UI.Framework.UiKit.Animation.Internals
{
    internal sealed class AnimatedBindingFactory
    {
        public AnimatedBindingFactory(IAnimationSettingsProvider animationSettingsProvider)
        {
            _animationSettingsProvider = animationSettingsProvider;
        }

        public BindingBase CreateBinding(
            BindingBase source,
            DependencyObject targetObject,
            DependencyProperty targetProperty,
            AnimationProperties? properties = null,
            Action? onCompletedCallback = null)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(targetObject);
            Guard.ArgumentIsNotNull(targetProperty);

            return source switch
            {
                MultiBinding multiBinding => CreateMultiBinding(targetObject, targetProperty, multiBinding, properties, onCompletedCallback),
                Binding singleBinding => CreateSingleBinding(targetObject, targetProperty, singleBinding, properties, onCompletedCallback),
                _ => throw new InvalidOperationException($"{source.GetType().Name} is not supported"),
            };
        }

        private MultiBinding CreateMultiBinding(
            DependencyObject targetObject,
            DependencyProperty targetProperty,
            MultiBinding multiBindingSource,
            AnimationProperties? properties,
            Action? onCompletedCallback)
        {
            var binding = new MultiBinding();

            multiBindingSource.Bindings.OfType<Binding>().ForEach(b => binding.Bindings.Add(b.Clone()));

            binding.Converter = new CompositeMultiConverter(
                multiBindingSource.Converter,
                new DelegateConverter(value => Convert(targetObject, targetProperty, value, properties, onCompletedCallback)));

            return binding;
        }

        private Binding CreateSingleBinding(
            DependencyObject targetObject,
            DependencyProperty targetProperty,
            Binding singleBindingSource,
            AnimationProperties? properties,
            Action? onCompletedCallback)
        {
            var binding = singleBindingSource.Clone();

            binding.Converter = new CompositeConverter(
                singleBindingSource.Converter,
                new DelegateConverter(value => Convert(targetObject, targetProperty, value, properties, onCompletedCallback)));

            return binding;
        }

        private object? Convert(
            DependencyObject targetObject,
            DependencyProperty targetProperty,
            object? newValue,
            AnimationProperties? properties,
            Action? onCompletedCallback)
        {
            var bindingProperties = GetBindingProperties(targetObject, targetProperty);

            try
            {
                var isFirstTime = !bindingProperties.IsInitialized;

                var isInstant = properties?.Duration == TimeSpan.Zero &&
                                properties?.Delay == TimeSpan.Zero;

                var isUnset = properties == null ||
                              newValue == null ||
                              newValue == DependencyProperty.UnsetValue;

                var skipAnimation = isFirstTime || isInstant || isUnset;

                if (skipAnimation)
                {
                    if (onCompletedCallback != null)
                    {
                        Executers.InUiAsync(onCompletedCallback.Invoke);
                    }

                    return newValue is Animatable animatableNewValue && animatableNewValue.IsFrozen
                        ? animatableNewValue.Clone()
                        : newValue;
                }

                var currentValue = targetObject.GetValue(targetProperty);

                if (AnimatedValueEqualityComparer.Equals(newValue, bindingProperties.AnimatingValue))
                {
                    return currentValue;
                }

                if (!AnimatedValueEqualityComparer.Equals(newValue, currentValue))
                {
                    targetObject.WhenInitialized(() => StartAnimation(targetObject, targetProperty, newValue!, properties, onCompletedCallback));
                }

                return currentValue;
            }
            finally
            {
                if (!bindingProperties.IsInitialized)
                {
                    bindingProperties.IsInitialized = true;
                }
            }
        }

        private void StartAnimation(
            DependencyObject targetObject,
            DependencyProperty targetProperty,
            object value,
            AnimationProperties? properties,
            Action? onCompletedCallback)
        {
            var animationProperties = properties ?? _animationSettingsProvider.DefaultAnimationProperties;
            var animationFrameRate = _animationSettingsProvider.GetDesiredFrameRate();

            var bindingProperties = GetBindingProperties(targetObject, targetProperty);
            bindingProperties.AnimatingValue = value;

            var animation = AnimationFactory.CreateAnimation(
                targetObject,
                targetProperty,
                animationFrameRate,
                animationProperties,
                value);

            var storyboard = new Storyboard();

            if (onCompletedCallback != null)
            {
                storyboard.Completed += (o, e) => onCompletedCallback.Invoke();
            }

            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        #region AnimatedBindingProperties

        private static AnimatedBindingProperties GetBindingProperties(DependencyObject dependencyObject, DependencyProperty dependencyProperty)
        {
            var propertiesDictionary = GetAnimatedBindingProperties(dependencyObject);
            if (propertiesDictionary is null)
            {
                propertiesDictionary = new Dictionary<DependencyProperty, AnimatedBindingProperties>();
                SetAnimatedBindingProperties(dependencyObject, propertiesDictionary);
            }

            if (!propertiesDictionary.TryGetValue(dependencyProperty, out var properties))
            {
                propertiesDictionary.Add(dependencyProperty, properties = new AnimatedBindingProperties());
            }

            return properties;
        }

        private static IDictionary<DependencyProperty, AnimatedBindingProperties>? GetAnimatedBindingProperties(DependencyObject obj)
            => (IDictionary<DependencyProperty, AnimatedBindingProperties>?)obj.GetValue(_animatedBindingPropertiesProperty);

        private static void SetAnimatedBindingProperties(DependencyObject obj, IDictionary<DependencyProperty, AnimatedBindingProperties> value)
            => obj.SetValue(_animatedBindingPropertiesProperty, value);

        private static readonly DependencyProperty _animatedBindingPropertiesProperty =
            DependencyProperty.RegisterAttached(
                "AnimatedBindingProperties",
                typeof(IDictionary<DependencyProperty, AnimatedBindingProperties>),
                typeof(AnimatedBindingExtension));

        private sealed record AnimatedBindingProperties
        {
            public bool IsInitialized { get; set; }

            public object? AnimatingValue { get; set; }
        }

        #endregion

        private readonly IAnimationSettingsProvider _animationSettingsProvider;
    }
}
