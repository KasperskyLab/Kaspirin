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
using System.Security;
using System.Windows;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class PasswordInputCopyAction : InputActionBase
    {
        public PasswordInputCopyAction()
        {
            ActionMode = InputActionMode.OnClick;

            this.WhenLoaded(() =>
            {
                _passwordInput = this.FindVisualParent<PasswordInput>();
                if (_passwordInput is not null)
                {
                    _passwordInputTextNotifier = new PropertyChangeNotifier<PasswordInput, SecureString>(_passwordInput, PasswordInput.PasswordProperty);
                    _passwordInputTextNotifier.ValueChanged += PasswordChanged;

                    InvalidateVisibility();
                }
            });
        }

        public static event EventHandler? Copied;

        #region CleanupMode

        public SecureClipboardCleanupMode CleanupMode
        {
            get => (SecureClipboardCleanupMode)GetValue(CleanupModeProperty);
            set => SetValue(CleanupModeProperty, value);
        }

        public static readonly DependencyProperty CleanupModeProperty = DependencyProperty.Register(
            nameof(CleanupMode),
            typeof(SecureClipboardCleanupMode),
            typeof(PasswordInputCopyAction),
            new PropertyMetadata(SecureClipboardCleanupMode.Timer));

        #endregion

        #region ExplicitValue

        public SecureString? ExplicitValue
        {
            get => (SecureString?)GetValue(ExplicitValueProperty);
            set => SetValue(ExplicitValueProperty, value);
        }

        public static readonly DependencyProperty ExplicitValueProperty = DependencyProperty.Register(
            nameof(ExplicitValue),
            typeof(SecureString),
            typeof(PasswordInputCopyAction));

        #endregion

        protected override void OnClick()
        {
            if (_passwordInput is null)
            {
                return;
            }

            var valueToCopy = ExplicitValue?.ToSimpleString() ?? _passwordInput.Password.ToSimpleString();
            if (valueToCopy is null)
            {
                return;
            }

            try
            {
                SecureClipboard.Copy(valueToCopy, CleanupMode);
                OnCopied(_passwordInput);
            }
            finally
            {
                // Cleanup string asynchronously to let it be transferred into the clipboard without issues.
                Executers.InUiAsync(() => valueToCopy.CleanupMemory(), DispatcherPriority.ContextIdle);
            }
        }

        private void PasswordChanged(PasswordInput input, SecureString? oldValue, SecureString? newValue)
            => InvalidateVisibility();

        private void InvalidateVisibility()
        {
            var isVisible = _passwordInput?.Password?.Length > 0;

            SetActionVisibility(isVisible);
        }

        private static void OnCopied(object sender)
            => Copied?.Invoke(sender, EventArgs.Empty);

        private PasswordInput? _passwordInput;
        private PropertyChangeNotifier<PasswordInput, SecureString>? _passwordInputTextNotifier;
    }
}
