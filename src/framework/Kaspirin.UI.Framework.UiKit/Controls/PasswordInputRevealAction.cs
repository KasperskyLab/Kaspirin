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

using System.Security;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class PasswordInputRevealAction : InputActionBase
    {
        public PasswordInputRevealAction()
        {
            this.WhenLoaded(() =>
            {
                _passwordInput = this.FindVisualParent<PasswordInput>();
                if (_passwordInput != null)
                {
                    _passwordInputRevealModeNotifier = new PropertyChangeNotifier<PasswordInput, PasswordInputRevealMode>(_passwordInput, PasswordInput.RevealModeProperty);
                    _passwordInputRevealModeNotifier.ValueChanged += OnPasswordInputRevealModeChanged;

                    _passwordInputPasswordNotifier = new PropertyChangeNotifier<PasswordInput, SecureString>(_passwordInput, PasswordInput.PasswordProperty);
                    _passwordInputPasswordNotifier.ValueChanged += OnPasswordInputPasswordChanged;

                    _passwordInputIsPasswordRevealedNotifier = new PropertyChangeNotifier<PasswordInput, bool>(_passwordInput, PasswordInput.IsPasswordRevealedProperty);
                    _passwordInputIsPasswordRevealedNotifier.ValueChanged += OnPasswordInputIsPasswordRevealedChanged;

                    IsRevealed = _passwordInput.IsPasswordRevealed;
                }
            });

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        #region IsRevealed

        public bool IsRevealed
        {
            get => (bool)GetValue(IsRevealedProperty);
            private set => SetValue(_isRevealedPropertyKey, value);
        }

        private static readonly DependencyPropertyKey _isRevealedPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(IsRevealed),
            typeof(bool),
            typeof(PasswordInputRevealAction),
            new PropertyMetadata(false, OnIsRevealedChanged));

        public static readonly DependencyProperty IsRevealedProperty = _isRevealedPropertyKey.DependencyProperty;

        private static void OnIsRevealedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var action = (PasswordInputRevealAction)d;
            action.OnIsRevealedChanged((bool)e.NewValue);
        }

        #endregion

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            InvalidateReveal();
            InvalidateVisibility();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
            => IsRevealed = false;

        protected override void OnClick()
        {
            IsRevealed = !IsRevealed;

            if (_passwordInput?.IsKeyboardFocusWithin == true)
            {
                _passwordInput.SetFocus();
            }
        }

        protected override void OnPressed(bool isPressed)
            => IsRevealed = isPressed;

        private void OnIsRevealedChanged(bool value)
        {
            if (_passwordInput != null && _passwordInput.IsPasswordRevealed != value)
            {
                _passwordInput.SetCurrentValue(PasswordInput.IsPasswordRevealedProperty, value);
            }
        }

        private void OnPasswordInputRevealModeChanged(PasswordInput sender, PasswordInputRevealMode oldValue, PasswordInputRevealMode newValue)
        {
            InvalidateReveal();
            InvalidateVisibility();
        }

        private void OnPasswordInputPasswordChanged(PasswordInput sender, SecureString? oldValue, SecureString? newValue)
            => InvalidateVisibility();

        private void OnPasswordInputIsPasswordRevealedChanged(PasswordInput input, bool oldValue, bool newValue)
            => IsRevealed = newValue;

        private void InvalidateReveal()
        {
            if (_passwordInput != null)
            {
                IsEnabled = true;

                if (_passwordInput.RevealMode == PasswordInputRevealMode.Editable)
                {
                    IsRevealed = _passwordInput.IsPasswordRevealed;
                    ActionMode = InputActionMode.OnClick;
                }
                else if (_passwordInput.RevealMode == PasswordInputRevealMode.ReadOnly)
                {
                    IsRevealed = false;
                    ActionMode = InputActionMode.OnPressed;
                }
                else
                {
                    IsEnabled = false;
                }
            }
        }

        private void InvalidateVisibility()
        {
            var revealMode = _passwordInput?.RevealMode;

            var isVisible = revealMode switch
            {
                PasswordInputRevealMode.ReadOnly => _passwordInput?.Password?.Length > 0,
                PasswordInputRevealMode.Editable => true,
                PasswordInputRevealMode.Disabled => false,
                _ => false
            };

            SetActionVisibility(isVisible);
        }

        private PasswordInput? _passwordInput;
        private PropertyChangeNotifier<PasswordInput, PasswordInputRevealMode>? _passwordInputRevealModeNotifier;
        private PropertyChangeNotifier<PasswordInput, SecureString>? _passwordInputPasswordNotifier;
        private PropertyChangeNotifier<PasswordInput, bool>? _passwordInputIsPasswordRevealedNotifier;
    }
}
