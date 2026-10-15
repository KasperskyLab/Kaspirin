// Copyright © 2025 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Controls;

internal sealed class InteractivityMediaProvider
{
    public InteractivityMediaProvider()
    {
        _storyboard = new();
        _storyboard.SetFrameRate();
    }

    public void LaunchShowAnimation(UIElement target, Action? onCompletedAction = null)
    {
        Guard.IsNotNull(target);

        target.Opacity = 0;

        LaunchOpacityAnimation(show: true, target, onCompletedAction);
    }

    public void LaunchHideAnimation(UIElement target, Action? onCompletedAction = null)
    {
        Guard.IsNotNull(target);

        LaunchOpacityAnimation(show: false, target, onCompletedAction);
    }

    private void LaunchOpacityAnimation(bool show, UIElement target, Action? onCompletedAction)
    {
        var newOpacity = Convert.ToInt32(show);

        var shouldSkip = target.Opacity.NearlyEqual(newOpacity);
        if (shouldSkip)
        {
            onCompletedAction?.Invoke();
            return;
        }

        _storyboard.Remove();
        _storyboard.Children.Clear();

        var isAnimationEnabled = _animationManager.Value.State == AnimationState.Enabled;
        if (isAnimationEnabled)
        {
            var opacityAnimation = new DoubleAnimation
            {
                To = newOpacity,
                FillBehavior = FillBehavior.HoldEnd,
                Duration = _animationDuration
            };

            opacityAnimation.Completed += (o, e) =>
            {
                onCompletedAction?.Invoke();
                onCompletedAction = null;
            };
            opacityAnimation.SetValue(Storyboard.TargetProperty, target);
            opacityAnimation.SetValue(Storyboard.TargetPropertyProperty, _opacityPropertyPath);
            opacityAnimation.Freeze();

            _storyboard.Children.Add(opacityAnimation);
            _storyboard.Begin();
        }
        else
        {
            target.Opacity = newOpacity;

            onCompletedAction?.Invoke();
        }
    }

    private readonly Storyboard _storyboard;

    private static readonly PropertyPath _opacityPropertyPath = new(UIElement.OpacityProperty);
    private static readonly Duration _animationDuration = new(TimeSpan.FromMilliseconds(200));
    private static readonly Lazy<IAnimationManager> _animationManager = new(() => ServiceLocator.GetService<IAnimationManager>());
}
