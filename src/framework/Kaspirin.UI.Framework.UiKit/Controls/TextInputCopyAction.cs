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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class TextInputCopyAction : InputActionBase
    {
        public TextInputCopyAction()
        {
            ActionMode = InputActionMode.OnClick;

            this.WhenLoaded(() =>
            {
                _textInput = this.FindVisualParent<TextInput>();
                if (_textInput is not null)
                {
                    _textInputTextNotifier = new PropertyChangeNotifier<TextInput, string>(_textInput, TextInputBase.TextProperty);
                    _textInputTextNotifier.ValueChanged += TextChanged;

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
            typeof(TextInputCopyAction),
            new PropertyMetadata(SecureClipboardCleanupMode.None));

        #endregion

        #region ExplicitValue

        public string? ExplicitValue
        {
            get => (string?)GetValue(ExplicitValueProperty);
            set => SetValue(ExplicitValueProperty, value);
        }

        public static readonly DependencyProperty ExplicitValueProperty = DependencyProperty.Register(
            nameof(ExplicitValue),
            typeof(string),
            typeof(TextInputCopyAction));

        #endregion

        protected override void OnClick()
        {
            if (_textInput is null)
            {
                return;
            }

            var valueToCopy = ExplicitValue ?? _textInput.Text;
            if (valueToCopy is null)
            {
                return;
            }

            SecureClipboard.Copy(valueToCopy, CleanupMode);
            OnCopied(_textInput);
        }

        private void TextChanged(TextInput input, string? oldValue, string? newValue)
            => InvalidateVisibility();

        private void InvalidateVisibility()
        {
            var isVisible = _textInput?.Text?.Length > 0;

            SetActionVisibility(isVisible);
        }

        private static void OnCopied(object sender)
            => Copied?.Invoke(sender, EventArgs.Empty);

        private TextInput? _textInput;
        private PropertyChangeNotifier<TextInput, string>? _textInputTextNotifier;
    }
}
