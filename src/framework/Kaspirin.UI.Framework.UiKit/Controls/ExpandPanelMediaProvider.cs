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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

internal sealed class ExpandPanelMediaProvider
{
    public ExpandPanelMediaProvider(FrameworkElement target)
    {
        _target = Guard.EnsureArgumentIsNotNull(target);

        _resizeStoryboard = new();
        _resizeStoryboard.SetFrameRate();

        _opacityStoryboard = new();
        _opacityStoryboard.SetFrameRate();
    }

    public event Action Expanded = () => { };

    public event Action Collapsed = () => { };

    public void LaunchExpandAnimation(ExpandDirection direction)
    {
        if (!_target.IsLoaded)
        {
            ExpandPermanent(direction);
            return;
        }

        var isAnimationEnabled = _animationSettingsProvider.Value.IsAnimationEnabled;
        if (isAnimationEnabled)
        {
            ExpandWithAnimation(direction);
        }
        else
        {
            ExpandPermanent(direction);
        }
    }

    public void LaunchCollapseAnimation(ExpandDirection direction)
    {
        if (!_target.IsLoaded)
        {
            CollapsePermanent(direction);
            return;
        }

        var isAnimationEnabled = _animationSettingsProvider.Value.IsAnimationEnabled;
        if (isAnimationEnabled)
        {
            CollapseWithAnimation(direction);
        }
        else
        {
            CollapsePermanent(direction);
        }
    }

    private void ExpandWithAnimation(ExpandDirection direction)
    {
        _resizeStoryboard.Completed -= RaiseExpandedOnCompleted;
        _resizeStoryboard.Completed -= RaiseCollapsedOnCompleted;

        _resizeStoryboard.Remove();
        _resizeStoryboard.Children.Clear();

        _opacityStoryboard.Remove();
        _opacityStoryboard.Children.Clear();

        Timeline? widthAnimation = null;
        Timeline? heightAnimation = null;

        if (direction.In(ExpandDirection.Width, ExpandDirection.WidthAndHeight))
        {
            widthAnimation = CreateResizeAnimation(
                property: FrameworkElement.MaxWidthProperty,
                from: _target.ActualWidth,
                to: OriginOrActualWidth,
                name: ExpandWidth);

            _resizeStoryboard.Children.Add(widthAnimation);
        }

        if (direction.In(ExpandDirection.Height, ExpandDirection.WidthAndHeight))
        {
            heightAnimation = CreateResizeAnimation(
                property: FrameworkElement.MaxHeightProperty,
                from: _target.ActualHeight,
                to: OriginOrActualHeight,
                name: ExpandHeight);

            _resizeStoryboard.Children.Add(heightAnimation);
        }

        var opacityAnimation = CreateOpacityAnimation(to: 1);

        var visibilityAnimation = CreateVisibilityAnimation(to: Visibility.Visible);

        UpdateAnimationDurations(widthAnimation,
                                 heightAnimation,
                                 opacityAnimation,
                                 visibilityAnimation);

        widthAnimation?.Freeze();
        heightAnimation?.Freeze();
        opacityAnimation.Freeze();
        visibilityAnimation.Freeze();

        _opacityStoryboard.Children.Add(opacityAnimation);
        _opacityStoryboard.Begin();

        _resizeStoryboard.Children.Add(visibilityAnimation);
        _resizeStoryboard.Completed += RaiseExpandedOnCompleted;
        _resizeStoryboard.Begin();
    }

    private void CollapseWithAnimation(ExpandDirection direction)
    {
        _resizeStoryboard.Completed -= RaiseExpandedOnCompleted;
        _resizeStoryboard.Completed -= RaiseCollapsedOnCompleted;

        _resizeStoryboard.Remove();
        _resizeStoryboard.Children.Clear();

        _opacityStoryboard.Remove();
        _opacityStoryboard.Children.Clear();

        Timeline? widthAnimation = null;
        Timeline? heightAnimation = null;

        if (direction.In(ExpandDirection.Width, ExpandDirection.WidthAndHeight))
        {
            _originWidth ??= OriginOrActualWidth;

            widthAnimation = CreateResizeAnimation(
                property: FrameworkElement.MaxWidthProperty,
                from: _target.ActualWidth,
                to: 0,
                name: CollapseWidth);

            _resizeStoryboard.Children.Add(widthAnimation);
        }

        if (direction.In(ExpandDirection.Height, ExpandDirection.WidthAndHeight))
        {
            _originHeight ??= OriginOrActualHeight;

            heightAnimation = CreateResizeAnimation(
                property: FrameworkElement.MaxHeightProperty,
                from: _target.ActualHeight,
                to: 0,
                name: CollapseHeight);

            _resizeStoryboard.Children.Add(heightAnimation);
        }

        var opacityAnimation = CreateOpacityAnimation(to: 0);

        var visibilityAnimation = CreateVisibilityAnimation(to: Visibility.Hidden);

        UpdateAnimationDurations(widthAnimation,
                                 heightAnimation,
                                 opacityAnimation,
                                 visibilityAnimation);

        widthAnimation?.Freeze();
        heightAnimation?.Freeze();
        opacityAnimation.Freeze();
        visibilityAnimation.Freeze();

        _opacityStoryboard.Children.Add(opacityAnimation);
        _opacityStoryboard.Begin();

        _resizeStoryboard.Children.Add(visibilityAnimation);
        _resizeStoryboard.Completed += RaiseCollapsedOnCompleted;
        _resizeStoryboard.Begin();
    }

    private Timeline CreateResizeAnimation(DependencyProperty property, double from, double to, string name)
    {
        _activeResizeAnimations.Add(name);

        var sizeAnimation = new DoubleAnimation
        {
            From = from,
            To = to,
            FillBehavior = FillBehavior.Stop,
            EasingFunction = _resizeEasing,
        };

        sizeAnimation.Completed += OnResizeAnimationCompleted;
        sizeAnimation.SetValue(Storyboard.TargetProperty, _target);
        sizeAnimation.SetValue(Storyboard.TargetPropertyProperty, property.AsPath());
        sizeAnimation.Name = name;

        return sizeAnimation;
    }

    private Timeline CreateVisibilityAnimation(Visibility to)
    {
        var visibilityAnimation = new ObjectAnimationUsingKeyFrames
        {
            FillBehavior = FillBehavior.HoldEnd,
        };

        visibilityAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(to, _visibilityFrameTime));
        visibilityAnimation.SetValue(Storyboard.TargetProperty, _target);
        visibilityAnimation.SetValue(Storyboard.TargetPropertyProperty, UIElement.VisibilityProperty.AsPath());

        return visibilityAnimation;
    }

    private Timeline CreateOpacityAnimation(double to)
    {
        var opacityAnimation = new DoubleAnimation
        {
            To = to,
            FillBehavior = FillBehavior.HoldEnd,
        };

        opacityAnimation.SetValue(Storyboard.TargetProperty, _target);
        opacityAnimation.SetValue(Storyboard.TargetPropertyProperty, UIElement.OpacityProperty.AsPath());

        return opacityAnimation;
    }

    private void OnResizeAnimationCompleted(object? sender, EventArgs e)
    {
        Guard.IsNotNull(_target);

        var animation = ((AnimationClock)sender!).Timeline;

        if (animation.Name == ExpandHeight && IsAnimationActive(ExpandHeight))
        {
            if (!IsAnimationActive(CollapseHeight))
            {
                _originHeight = null;
            }

            _target.MaxHeight = double.PositiveInfinity;

            _activeResizeAnimations.Remove(ExpandHeight);
        }

        if (animation.Name == ExpandWidth && IsAnimationActive(ExpandWidth))
        {
            if (!IsAnimationActive(CollapseWidth))
            {
                _originWidth = null;
            }

            _target.MaxWidth = double.PositiveInfinity;

            _activeResizeAnimations.Remove(ExpandWidth);
        }

        if (animation.Name == CollapseHeight && IsAnimationActive(CollapseHeight))
        {
            _target.MaxHeight = 0;

            _activeResizeAnimations.Remove(CollapseHeight);
        }

        if (animation.Name == CollapseWidth && IsAnimationActive(CollapseWidth))
        {
            _target.MaxWidth = 0;

            _activeResizeAnimations.Remove(CollapseWidth);
        }
    }

    private void ExpandPermanent(ExpandDirection direction)
    {
        switch (direction)
        {
            case ExpandDirection.WidthAndHeight:
                _target.MaxWidth = double.PositiveInfinity;
                _target.MaxHeight = double.PositiveInfinity;
                break;
            case ExpandDirection.Width:
                _target.MaxWidth = double.PositiveInfinity;
                break;
            case ExpandDirection.Height:
                _target.MaxHeight = double.PositiveInfinity;
                break;
            default:
                throw new UnexpectedValueException(direction);
        }

        _target.Visibility = Visibility.Visible;
        _target.Opacity = 1;

        Expanded.Invoke();
    }

    private void CollapsePermanent(ExpandDirection direction)
    {
        if (_target.IsLoaded)
        {
            switch (direction)
            {
                case ExpandDirection.WidthAndHeight:
                    _originHeight = _target.ActualHeight;
                    _originWidth = _target.ActualWidth;
                    break;
                case ExpandDirection.Width:
                    _originWidth = _target.ActualWidth;
                    break;
                case ExpandDirection.Height:
                    _originHeight = _target.ActualHeight;
                    break;
                default:
                    throw new UnexpectedValueException(direction);
            }
        }

        switch (direction)
        {
            case ExpandDirection.WidthAndHeight:
                _target.MaxWidth = 0;
                _target.MaxHeight = 0;
                break;
            case ExpandDirection.Width:
                _target.MaxWidth = 0;
                break;
            case ExpandDirection.Height:
                _target.MaxHeight = 0;
                break;
            default:
                throw new UnexpectedValueException(direction);
        }

        _target.Visibility = Visibility.Hidden;
        _target.Opacity = 0;

        Collapsed.Invoke();
    }

    private void RaiseExpandedOnCompleted(object? sender, EventArgs e)
        => Expanded.Invoke();

    private void RaiseCollapsedOnCompleted(object? sender, EventArgs e)
        => Collapsed.Invoke();

    private bool IsAnimationActive(string name)
        => _activeResizeAnimations.Contains(name);

    private void UpdateAnimationDurations(
        Timeline? widthAnimation,
        Timeline? heightAnimation,
        Timeline opacityAnimation,
        Timeline visibilityAnimation)
    {
        var isExpand = widthAnimation?.Name == ExpandWidth ||
                       heightAnimation?.Name == ExpandHeight;

        const double resizeMaxDuration = 900;
        const double resizeMinDuration = 400;

        const double opacityDuration = 150;

        var resizeLength = GetResizeLength(widthAnimation, heightAnimation);
        var resizeAnimationMs = Math.Max(resizeMinDuration, Math.Min(resizeLength, resizeMaxDuration));

        if (widthAnimation != null)
        {
            widthAnimation.Duration = new(TimeSpan.FromMilliseconds(resizeAnimationMs));
            widthAnimation.BeginTime = isExpand
                ? TimeSpan.Zero
                : TimeSpan.FromMilliseconds(resizeAnimationMs * 0.3);
        }

        if (heightAnimation != null)
        {
            heightAnimation.Duration = new(TimeSpan.FromMilliseconds(resizeAnimationMs));
            heightAnimation.BeginTime = isExpand
                ? TimeSpan.Zero
                : TimeSpan.FromMilliseconds(resizeAnimationMs * 0.3);
        }

        opacityAnimation.Duration = new(TimeSpan.FromMilliseconds(opacityDuration));
        opacityAnimation.BeginTime = isExpand
            ? TimeSpan.FromMilliseconds(resizeAnimationMs * 0.5)
            : TimeSpan.Zero;

        visibilityAnimation.BeginTime = isExpand
            ? TimeSpan.Zero
            : TimeSpan.FromMilliseconds(resizeAnimationMs);
    }

    private static double GetResizeLength(Timeline? widthAnimation, Timeline? heightAnimation)
    {
        var resizeWidthLength = 0D;
        var resizeHeightLength = 0D;

        if (widthAnimation is DoubleAnimation widthDoubleAnimation && widthDoubleAnimation.From != null && widthDoubleAnimation.To != null)
        {
            resizeWidthLength = Math.Abs(widthDoubleAnimation.From.Value - widthDoubleAnimation.To.Value);
        }

        if (heightAnimation is DoubleAnimation doubleHeightAnimation && doubleHeightAnimation.From != null && doubleHeightAnimation.To != null)
        {
            resizeHeightLength = Math.Abs(doubleHeightAnimation.From.Value - doubleHeightAnimation.To.Value);
        }

        var resizeLength = Math.Max(resizeWidthLength, resizeHeightLength);
        return resizeLength;
    }

    private double OriginOrActualHeight => _originHeight ?? _target.ActualHeight;
    private double OriginOrActualWidth => _originWidth ?? _target.ActualWidth;

    private readonly Storyboard _resizeStoryboard;
    private readonly Storyboard _opacityStoryboard;
    private readonly FrameworkElement _target;
    private readonly HashSet<string> _activeResizeAnimations = new();

    private const string ExpandHeight = nameof(ExpandHeight);
    private const string ExpandWidth = nameof(ExpandWidth);
    private const string CollapseHeight = nameof(CollapseHeight);
    private const string CollapseWidth = nameof(CollapseWidth);

    private static readonly Lazy<IAnimationSettingsProvider> _animationSettingsProvider = new(() => ServiceLocator.GetService<IAnimationSettingsProvider>());
    private static readonly KeyTime _visibilityFrameTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
    private static readonly EasingFunctionBase _resizeEasing = new ExponentialEase()
    {
        Exponent = 6,
        EasingMode = EasingMode.EaseOut
    };

    private double? _originHeight;
    private double? _originWidth;
}
