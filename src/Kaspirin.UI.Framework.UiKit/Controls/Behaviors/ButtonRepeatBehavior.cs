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

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors;

public sealed class ButtonRepeatBehavior : Behavior<Button, ButtonRepeatBehavior>
{
    public ButtonRepeatBehavior()
    {
        _timer = TimerFactory.CreateOnUi(OnTimeout);
    }

    #region Interval

    public static TimeSpan GetInterval(DependencyObject obj)
        => (TimeSpan)obj.GetValue(IntervalProperty);

    public static void SetInterval(DependencyObject obj, TimeSpan value)
        => obj.SetValue(IntervalProperty, value);

    public static readonly DependencyProperty IntervalProperty = DependencyProperty.RegisterAttached(
        "Interval",
        typeof(TimeSpan),
        typeof(ButtonRepeatBehavior),
        new PropertyMetadata(GetKeyboardSpeed()));

    #endregion

    #region Delay

    public static TimeSpan GetDelay(DependencyObject obj)
        => (TimeSpan)obj.GetValue(DelayProperty);

    public static void SetDelay(DependencyObject obj, TimeSpan value)
        => obj.SetValue(DelayProperty, value);

    public static readonly DependencyProperty DelayProperty = DependencyProperty.RegisterAttached(
        "Delay",
        typeof(TimeSpan),
        typeof(ButtonRepeatBehavior),
        new PropertyMetadata(GetKeyboardDelay()));

    #endregion

    #region ExecuteCommandOnRepeat

    public static bool GetExecuteCommandOnRepeat(DependencyObject obj)
        => (bool)obj.GetValue(ExecuteCommandOnRepeatProperty);

    public static void SetExecuteCommandOnRepeat(DependencyObject obj, bool value)
        => obj.SetValue(ExecuteCommandOnRepeatProperty, value);

    public static readonly DependencyProperty ExecuteCommandOnRepeatProperty = DependencyProperty.RegisterAttached(
        "ExecuteCommandOnRepeat",
        typeof(bool),
        typeof(ButtonRepeatBehavior),
        new PropertyMetadata(true));

    #endregion

    public event Action Repeat = EmptyDelegate.For<Action>();

    protected override void OnAttached()
    {
        Guard.IsNotNull(AssociatedObject);

        _buttonPressedNotifier = new PropertyChangeNotifier<Button, bool>(AssociatedObject, Button.IsPressedProperty);
        _buttonPressedNotifier.ValueChanged += OnButtonIsPressedChanged;
    }

    protected override void OnDetaching()
    {
        if (_buttonPressedNotifier != null)
        {
            _buttonPressedNotifier.ValueChanged -= OnButtonIsPressedChanged;
            _buttonPressedNotifier = null;
        }

        StopTimer();
    }

    private void OnButtonIsPressedChanged(Button sender, bool oldValue, bool newValue)
    {
        if (newValue)
        {
            RaiseRepeat();

            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }

    private void RaiseRepeat()
    {
        Repeat.Invoke();
    }

    private void StartTimer()
    {
        Guard.IsNotNull(AssociatedObject);

        if (_timer.IsRunning)
        {
            return;
        }

        _timer.Interval = GetDelay(AssociatedObject);
        _timer.Start(delay: _timer.Interval);
    }

    private void StopTimer()
    {
        _timer.Stop();
    }

    private void OnTimeout()
    {
        Guard.IsNotNull(AssociatedObject);

        _timer.Interval = GetInterval(AssociatedObject);

        Guard.IsNotNull(AssociatedObject);

        if (AssociatedObject.IsPressed)
        {
            if (GetExecuteCommandOnRepeat(AssociatedObject))
            {
                CommandHelper.ExecuteCommandSource(AssociatedObject);
            }

            RaiseRepeat();
        }
    }

    // Copied from .NET RepeatButton
    private static TimeSpan GetKeyboardDelay()
    {
        // SPI_GETKEYBOARDDELAY 0,1,2,3 correspond to 250,500,750,1000ms
        var delay = SystemParameters.KeyboardDelay;
        if (delay < 0 || delay > 3)
        {
            delay = 0;
        }

        return TimeSpan.FromMilliseconds((delay + 1) * 250);
    }

    // Copied from .NET RepeatButton
    private static TimeSpan GetKeyboardSpeed()
    {
        // SPI_GETKEYBOARDSPEED 0,...,31 correspond to 1000/2.5=400,...,1000/30 ms
        var speed = SystemParameters.KeyboardSpeed;
        if (speed < 0 || speed > 31)
        {
            speed = 31;
        }

        return TimeSpan.FromMilliseconds((31 - speed) * (400 - 1000 / 30) / 31 + 1000 / 30);
    }

    private PropertyChangeNotifier<Button, bool>? _buttonPressedNotifier;
    private readonly ITimer _timer;
}
