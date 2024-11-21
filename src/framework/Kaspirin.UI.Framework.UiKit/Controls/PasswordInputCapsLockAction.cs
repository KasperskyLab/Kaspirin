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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class PasswordInputCapsLockAction : InputActionBase
    {
        public PasswordInputCapsLockAction()
        {
            _hidePopupAction = new DeferredAction(() => IsPopupOpen = false, TimeSpan.FromMilliseconds(2000));

            this.WhenLoaded(() =>
            {
                _passwordInput = this.FindVisualParent<PasswordInput>();
                if (_passwordInput != null)
                {
                    _passwordInputIsKeyboardFocusWithinPropertyNotifier = new PropertyChangeNotifier<PasswordInput, bool>(_passwordInput, PasswordInput.IsKeyboardFocusWithinProperty);
                    _passwordInputIsKeyboardFocusWithinPropertyNotifier.ValueChanged += PasswordBoxOnIsKeyboardFocusWithinChanged;
                    _passwordInput.PreviewKeyDown += PasswordBoxOnPreviewKeyDown;
                }

                SetActionVisibility(false);
            });
        }

        #region IsPopupOpen

        public bool IsPopupOpen
        {
            get { return (bool)GetValue(IsPopupOpenProperty); }
            private set { SetValue(_isPopupOpenPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _isPopupOpenPropertyKey =
            DependencyProperty.RegisterReadOnly("IsPopupOpen", typeof(bool), typeof(PasswordInputCapsLockAction),
                new PropertyMetadata(false, OnIsPopupOpenChanged));

        public static readonly DependencyProperty IsPopupOpenProperty =
            _isPopupOpenPropertyKey.DependencyProperty;

        private static void OnIsPopupOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PasswordInputCapsLockAction)d).HidePopupWithTimer();
        }

        #endregion

        private void PasswordBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.CapsLock)
            {
                var isActive = e.KeyboardDevice.IsKeyToggled(Key.CapsLock);

                SetActionVisibility(isActive);
                SetPopupVisibility(isActive);
            }
        }

        private void PasswordBoxOnIsKeyboardFocusWithinChanged(PasswordInput input, bool oldIsFocused, bool newIsFocused)
        {
            var isActive = newIsFocused && Console.CapsLock;

            SetActionVisibility(isActive);
            SetPopupVisibility(isActive);
        }

        private void SetPopupVisibility(bool isVisible)
        {
            IsPopupOpen = isVisible;
        }

        private void HidePopupWithTimer()
        {
            if (IsPopupOpen)
            {
                _hidePopupAction.Execute();
            }
        }

        private readonly DeferredAction _hidePopupAction;

        private PasswordInput? _passwordInput;
        private PropertyChangeNotifier<PasswordInput, bool>? _passwordInputIsKeyboardFocusWithinPropertyNotifier;
    }
}
