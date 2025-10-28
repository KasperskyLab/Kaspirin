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

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_AnimatedIndicator, Type = typeof(RoundedPanel))]
[TemplatePart(Name = PART_RootPanel, Type = typeof(RoundedPanel))]
public sealed class ProgressBar : System.Windows.Controls.ProgressBar
{
    public const string PART_AnimatedIndicator = "PART_AnimatedIndicator";
    public const string PART_RootPanel = "PART_RootPanel";

    public ProgressBar()
    {
        _cornerRoundingHelper = new CornerRoundingHelper(this, InvalidateCornerRadius);

        _storyboard = new();
        _storyboard.SetFrameRate();

        Loaded += OnLoaded;
        ValueChanged += OnValueChanged;
        SizeChanged += OnSizeChanged;
    }

    #region State

    public string State
    {
        get => (string)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        nameof(State),
        typeof(string),
        typeof(ProgressBar),
        new PropertyMetadata(default(string)));

    #endregion

    #region Estimation

    public string Estimation
    {
        get => (string)GetValue(EstimationProperty);
        set => SetValue(EstimationProperty, value);
    }

    public static readonly DependencyProperty EstimationProperty = DependencyProperty.Register(
        nameof(Estimation),
        typeof(string),
        typeof(ProgressBar),
        new PropertyMetadata(default(string)));

    #endregion

    #region ShowHighlight

    public bool ShowHighlight
    {
        get => (bool)GetValue(ShowHighlightProperty);
        set => SetValue(ShowHighlightProperty, value);
    }

    public static readonly DependencyProperty ShowHighlightProperty = DependencyProperty.Register(
        nameof(ShowHighlight),
        typeof(bool),
        typeof(ProgressBar),
        new PropertyMetadata(true));

    #endregion

    #region ShowValue

    public bool ShowValue
    {
        get => (bool)GetValue(ShowValueProperty);
        set => SetValue(ShowValueProperty, value);
    }

    public static readonly DependencyProperty ShowValueProperty = DependencyProperty.Register(
        nameof(ShowValue),
        typeof(bool),
        typeof(ProgressBar),
        new PropertyMetadata(true));

    #endregion

    #region ShowState

    public bool ShowState
    {
        get => (bool)GetValue(ShowStateProperty);
        set => SetValue(ShowStateProperty, value);
    }

    public static readonly DependencyProperty ShowStateProperty = DependencyProperty.Register(
        nameof(ShowState),
        typeof(bool),
        typeof(ProgressBar),
        new PropertyMetadata(true));

    #endregion

    #region ShowEstimation

    public bool ShowEstimation
    {
        get => (bool)GetValue(ShowEstimationProperty);
        set => SetValue(ShowEstimationProperty, value);
    }

    public static readonly DependencyProperty ShowEstimationProperty = DependencyProperty.Register(
        nameof(ShowEstimation),
        typeof(bool),
        typeof(ProgressBar),
        new PropertyMetadata(default(bool)));

    #endregion

    #region CanRollback

    public bool CanRollback
    {
        get => (bool)GetValue(CanRollbackProperty);
        set => SetValue(CanRollbackProperty, value);
    }

    public static readonly DependencyProperty CanRollbackProperty = DependencyProperty.Register(
        nameof(CanRollback),
        typeof(bool),
        typeof(ProgressBar),
        new PropertyMetadata(default(bool)));

    #endregion

    #region Type

    public ProgressBarType Type
    {
        get => (ProgressBarType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(ProgressBarType),
        typeof(ProgressBar),
        new PropertyMetadata(ProgressBarType.Positive));

    #endregion

    #region DisableRoundingTopLeft

    public bool DisableRoundingTopLeft
    {
        get => (bool)GetValue(DisableRoundingTopLeftProperty);
        set => SetValue(DisableRoundingTopLeftProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingTopLeftProperty =
        CornerRoundingHelper.DisableRoundingTopLeftProperty.AddOwner(typeof(ProgressBar));

    #endregion

    #region DisableRoundingTopRight

    public bool DisableRoundingTopRight
    {
        get => (bool)GetValue(DisableRoundingTopRightProperty);
        set => SetValue(DisableRoundingTopRightProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingTopRightProperty =
        CornerRoundingHelper.DisableRoundingTopRightProperty.AddOwner(typeof(ProgressBar));

    #endregion

    #region DisableRoundingBottomLeft

    public bool DisableRoundingBottomLeft
    {
        get => (bool)GetValue(DisableRoundingBottomLeftProperty);
        set => SetValue(DisableRoundingBottomLeftProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingBottomLeftProperty =
        CornerRoundingHelper.DisableRoundingBottomLeftProperty.AddOwner(typeof(ProgressBar));

    #endregion

    #region DisableRoundingBottomRight

    public bool DisableRoundingBottomRight
    {
        get => (bool)GetValue(DisableRoundingBottomRightProperty);
        set => SetValue(DisableRoundingBottomRightProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingBottomRightProperty =
        CornerRoundingHelper.DisableRoundingBottomRightProperty.AddOwner(typeof(ProgressBar));

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate(); //Apply PART_GlowRect, PART_Track, PART_Indicator here

        _animatedIndicator = Guard.EnsureIsInstanceOfType<RoundedPanel>(GetTemplateChild(PART_AnimatedIndicator));
        _root = Guard.EnsureIsInstanceOfType<RoundedPanel>(GetTemplateChild(PART_RootPanel));
    }

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

    private void InvalidateCornerRadius()
    {
        if (_root == null || _animatedIndicator == null)
        {
            return;
        }

        _root.CornerRadius = _cornerRoundingHelper.GetCornerRadius();
        _animatedIndicator.CornerRadius = _cornerRoundingHelper.GetCornerRadius();
    }

    private readonly CornerRoundingHelper _cornerRoundingHelper;
    private readonly Storyboard _storyboard;

    private RoundedPanel? _animatedIndicator;
    private RoundedPanel? _root;

    private static readonly Duration _animationDuration = new(TimeSpan.FromMilliseconds(500));
}
