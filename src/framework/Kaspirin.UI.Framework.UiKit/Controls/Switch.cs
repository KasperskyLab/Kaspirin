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
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public class Switch : ToggleButton
    {
        public Switch()
        {
            Loaded += OnLoaded;

            PreviewKeyDown += OnPreviewKeyDown;
            PreviewMouseDown += OnPreviewMouseDown;
            PreviewMouseMove += OnPreviewMouseMove;
            PreviewMouseUp += OnPreviewMouseUp;

            Checked += (o, e) => { SetChecked(true); };
            Unchecked += (o, e) => { SetChecked(false); };
        }

        #region IsCheckedConfirmBehavior

        public static readonly DependencyProperty IsCheckedConfirmBehaviorProperty
            = DependencyProperty.Register("IsCheckedConfirmBehavior", typeof(ISwitchConfirmableSetter<bool>), typeof(Switch));

        public ISwitchConfirmableSetter<bool> IsCheckedConfirmBehavior
        {
            get { return (ISwitchConfirmableSetter<bool>)GetValue(IsCheckedConfirmBehaviorProperty); }
            set { SetValue(IsCheckedConfirmBehaviorProperty, value); }
        }

        #endregion

        #region IsPressed

        public new bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            private set { SetValue(IsPressedProperty, value); }
        }

        private static readonly DependencyPropertyKey _isPressedPropertyKey
            = DependencyProperty.RegisterReadOnly("IsPressed", typeof(bool), typeof(Switch),
                new FrameworkPropertyMetadata(false));

        public static new readonly DependencyProperty IsPressedProperty =
            _isPressedPropertyKey.DependencyProperty;

        #endregion

        public override void OnApplyTemplate()
        {
            _slider = (Slider)GetTemplateChild("PART_Switcher");
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);

            SetIsPressed(false);
            EndMouseDrag();
        }

        protected override void OnClick()
        {
            // user input is handled in Switch.OnPreviewMouseUp
        }

        protected virtual void BeforeCheck() { }
        protected virtual void BeforeUncheck() { }

        private void OnLoaded(object sender, RoutedEventArgs eventArgs)
        {
            if (_slider != null)
            {
                _slider.Value = IsChecked == true ? 1 : 0;
                _slider.IsEnabledChanged += OnSliderEnabledChanged;
            }

            IsSliderEnabled = _slider?.IsEnabled ?? true;
        }

        private void OnSliderEnabledChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is bool)
            {
                IsSliderEnabled = (bool)args.NewValue;
            }
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs eventArgs)
        {
            Keyboard.Focus(_slider);

            switch (eventArgs.Key)
            {
                case Key.Up:
                case Key.Right:
                    SetIsPressed(false);
                    TryCheck();
                    break;
                case Key.Down:
                case Key.Left:
                    SetIsPressed(false);
                    TryUncheck();
                    break;
                case Key.Space:
                case Key.Enter:
                    SetIsPressed(false);
                    TryInvert();
                    break;
            }
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.ChangedButton == MouseButton.Left && eventArgs.ButtonState == MouseButtonState.Pressed)
            {
                SetIsPressed(true);
                StartMouseDrag();
            }
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs eventArgs)
        {
            UpdateMouseDrag();
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs eventArgs)
        {
            SetIsPressed(false);
            EndMouseDrag();
        }

        private void TryCheck()
        {
            BeforeCheck();

            var checkedSetter = IsCheckedConfirmBehavior;
            if (checkedSetter == null)
            {
                SetChecked(true);
            }
            else
            {
                DisableSlider();

                checkedSetter.SetWithConfirmation(true,
                    () =>
                    {
                        SetChecked(Guard.EnsureIsNotNull(IsChecked));

                        EnableSlider();
                    },
                    () =>
                    {
                        TrySetSliderValue(0);

                        EnableSlider();
                    });
            }
        }

        private void TryUncheck()
        {
            BeforeUncheck();

            var checkedSetter = IsCheckedConfirmBehavior;
            if (checkedSetter == null)
            {
                SetChecked(false);
            }
            else
            {
                DisableSlider();

                checkedSetter.SetWithConfirmation(false,
                    () =>
                    {
                        SetChecked(Guard.EnsureIsNotNull(IsChecked));
                        EnableSlider();
                    },
                    () =>
                    {
                        TrySetSliderValue(1);
                        EnableSlider();
                    });
            }
        }

        private void DisableSlider()
        {
            if (_slider != null)
            {
                _slider.IsEnabled = false;
            }

            IsSliderEnabled = false;
        }

        private void EnableSlider()
        {
            if (_slider != null)
            {
                _slider.IsEnabled = true;
            }

            IsSliderEnabled = true;
        }

        private void SetIsPressed(bool pressed)
        {
            if (pressed)
            {
                SetValue(_isPressedPropertyKey, true);
            }
            else
            {
                ClearValue(_isPressedPropertyKey);
            }
        }

        private void StartMouseDrag()
        {
            _isMouseDragging = true;
            _isMouseDragged = false;
            _mouseDownPosition = Mouse.GetPosition(this);
        }

        private void UpdateMouseDrag()
        {
            if (_mouseDownPosition != Mouse.GetPosition(this))
            {
                _isMouseDragged = true;
            }
        }

        private void EndMouseDrag()
        {
            if (!_isMouseDragging || Mouse.GetPosition(this) == _mouseDownPosition && _isMouseDragged)
            {
                return;
            }

            try
            {
                if (!IsSliderEnabled)
                {
                    return;
                }

                if (!_isMouseDragged)
                {
                    TryInvert();
                }
                else
                {
                    Guard.IsNotNull(_slider);
                    if (_slider.Value < 0.5)
                    {
                        TryUncheck();
                    }
                    else // Slider.Value >= 0.5
                    {
                        TryCheck();
                    }
                }
            }
            finally
            {
                _isMouseDragging = false;
            }
        }

        private void TryInvert()
        {
            if (IsChecked ?? false)
            {
                TryUncheck();
            }
            else
            {
                TryCheck();
            }
        }

        private void SetChecked(bool state)
        {
            if (state != IsChecked)
            {
                IsChecked = state;
            }

            TrySetSliderValue((bool)IsChecked ? 1 : 0);
        }

        private void TrySetSliderValue(int value)
        {
            if (_slider != null)
            {
                _slider.Value = value;
            }
        }

        private bool IsSliderEnabled { get; set; }

        private bool _isMouseDragging;
        private bool _isMouseDragged;
        private Point _mouseDownPosition;
        private Slider? _slider;
    }

    public interface ISwitchConfirmableSetter<T>
    {
        void SetWithConfirmation(T value, Action onConfirmed, Action onCancelled);
    }
}
