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

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_LoaderIcon, Type = typeof(Image))]
public sealed class Spinner : Control
{
    public const string PART_LoaderIcon = "PART_LoaderIcon";

    public Spinner()
    {
        var animationSettings = ServiceLocator.GetService<IAnimationManager>();
        var frameRate = animationSettings.GetDesiredFrameRate(AnimationRenderQuality.Low);
        if (frameRate <= 0)
        {
            throw new Exception(
                $"Unexpected value provided from {nameof(IAnimationManager)}.{nameof(IAnimationManager.GetDesiredFrameRate)}. " +
                $"FrameRate must be greater than zero.");
        }

        _isVisibleChangedNotifier = new PropertyChangeNotifier<Spinner, bool>(this, Spinner.IsVisibleProperty);
        _isVisibleChangedNotifier.ValueChanged += OnIsVisibleChanged;

        _frameRate = frameRate;
        _animationTimerInterval = TimeSpan.FromSeconds(1D / _frameRate);
    }

    #region SpinnerBrush

    public Brush SpinnerBrush
    {
        get => (Brush)GetValue(SpinnerBrushProperty);
        set => SetValue(SpinnerBrushProperty, value);
    }

    public static readonly DependencyProperty SpinnerBrushProperty = DependencyProperty.Register(
        nameof(SpinnerBrush),
        typeof(Brush),
        typeof(Spinner),
        new PropertyMetadata(default(Brush)));

    #endregion

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

    private void SetAnimationTimer()
    {
        lock (_sync)
        {
            _activeSpinnersRotationTimer ??= TimerFactory.CreateOnUi(
                interval: _animationTimerInterval,
                priority: DispatcherPriority.Render);

            _activeSpinnersRotationTimer.Tick -= OnTick;
            _activeSpinnersRotationTimer.Tick += OnTick;
            _activeSpinnersRotationTimer.Start();

            _activeSpinnersCount++;
        }
    }

    private void StopAnimationTimer()
    {
        lock (_sync)
        {
            if (_activeSpinnersRotationTimer == null)
            {
                return;
            }

            _activeSpinnersRotationTimer.Tick -= OnTick;
            _activeSpinnersCount--;

            if (_activeSpinnersRotationTimer.IsRunning &&
                _activeSpinnersCount == 0)
            {
                _activeSpinnersRotationTimer.Stop();
            }
        }
    }

    private void OnIsVisibleChanged(Spinner sender, bool oldValue, bool newValue)
    {
        if (newValue)
        {
            SetAnimationTimer();
        }
        else
        {
            StopAnimationTimer();
        }
    }

    private void OnTick(ITimer timer)
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

    private readonly PropertyChangeNotifier<Spinner, bool> _isVisibleChangedNotifier;
    private readonly int _frameRate;
    private readonly TimeSpan _animationTimerInterval;

    private static ITimer? _activeSpinnersRotationTimer;
    private static int _activeSpinnersCount = 0;
    private static readonly object _sync = new();
}
