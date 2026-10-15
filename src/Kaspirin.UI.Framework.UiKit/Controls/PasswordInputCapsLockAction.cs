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

#pragma warning disable CA1416 // This call site is reachable on all platforms.

using System;
using System.Windows;
using System.Windows.Input;
using InputType = Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums.InputType;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class PasswordInputCapsLockAction : InputActionBase
{
    public PasswordInputCapsLockAction()
    {
        _hidePopupAction = DeferredActionFactory.CreateDebouncerOnUi(() => IsPopupOpen = false, TimeSpan.FromSeconds(2));

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
        get => (bool)GetValue(IsPopupOpenProperty);
        private set => SetValue(_isPopupOpenPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _isPopupOpenPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(IsPopupOpen),
        typeof(bool),
        typeof(PasswordInputCapsLockAction),
        new PropertyMetadata(default(bool), OnIsPopupOpenChanged));

    public static readonly DependencyProperty IsPopupOpenProperty = _isPopupOpenPropertyKey.DependencyProperty;

    private static void OnIsPopupOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((PasswordInputCapsLockAction)d).HidePopupWithTimer();

    #endregion

    protected override void OnClick()
    {
        ToggleCapsLock();
    }

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

    private static void ToggleCapsLock()
    {
        var inputs = new InputInfo[2];

        inputs[0].Type = InputType.Keyboard;
        inputs[0].Data.Keyboard.VirtualKey = VirtualKey.Capital;
        inputs[0].Data.Keyboard.Flags = KeyboardInputFlags.None;

        inputs[1].Type = InputType.Keyboard;
        inputs[1].Data.Keyboard.VirtualKey = VirtualKey.Capital;
        inputs[1].Data.Keyboard.Flags = KeyboardInputFlags.KeyUp;

        User32Dll.SendInput((uint)inputs.Length, inputs, InputInfo.Size);
    }

    private readonly IDeferredAction _hidePopupAction;

    private PasswordInput? _passwordInput;
    private PropertyChangeNotifier<PasswordInput, bool>? _passwordInputIsKeyboardFocusWithinPropertyNotifier;
}
