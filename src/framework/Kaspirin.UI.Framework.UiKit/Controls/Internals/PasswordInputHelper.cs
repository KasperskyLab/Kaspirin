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
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal static class PasswordInputHelper
    {
        public static void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var passwordInput = Guard.EnsureArgumentIsInstanceOfType<PasswordInput>(sender);

            var pastedText = SecureClipboard.GetClipboardContent();
            if (pastedText is not null)
            {
                string? oldPassword = default,
                    oldPasswordStart = default,
                    oldPasswordEnd = default,
                    newPasswordUnrestricted = default,
                    newPassword = default;

                try
                {
                    if (passwordInput.InputFilter is not null)
                    {
                        string? filteredPastedText = default;

                        try
                        {
                            filteredPastedText = passwordInput.InputFilter.FilterInput(pastedText);
                        }
                        finally
                        {
                            if (!ReferenceEquals(pastedText, filteredPastedText))
                            {
                                pastedText.CleanupMemory();
                            }

                            pastedText = filteredPastedText;
                        }
                    }

                    if (pastedText is null)
                    {
                        return;
                    }

                    SecureClipboard.Copy(pastedText);

                    if (!passwordInput.TryGetSelection(out var selectionStart, out var selectionEnd))
                    {
                        selectionStart = selectionEnd = 0;
                    }

                    var caretPosition = Math.Min(selectionStart.Value + pastedText.Length, passwordInput.MaxLength);

                    oldPassword = passwordInput.Password.ToSimpleString() ?? string.Empty;
                    oldPasswordStart = oldPassword.Substring(0, selectionStart.Value);
                    oldPasswordEnd = oldPassword.Substring(selectionEnd.Value);
                    newPasswordUnrestricted = string.Concat(oldPasswordStart, pastedText, oldPasswordEnd);
                    newPassword = passwordInput.MaxLength > 0
                        ? newPasswordUnrestricted.Substring(0, Math.Min(newPasswordUnrestricted.Length, passwordInput.MaxLength))
                        : newPasswordUnrestricted;

                    passwordInput.Password = newPassword.ToSecureString();

                    passwordInput.SetSelection(caretPosition);
                    passwordInput.SetFocus();
                }
                finally
                {
                    if (!ReferenceEquals(newPassword, newPasswordUnrestricted))
                    {
                        newPassword?.CleanupMemory();
                    }

                    if (!ReferenceEquals(newPasswordUnrestricted, oldPasswordStart) &&
                        !ReferenceEquals(newPasswordUnrestricted, pastedText) &&
                        !ReferenceEquals(newPasswordUnrestricted, oldPasswordEnd))
                    {
                        newPasswordUnrestricted?.CleanupMemory();
                    }

                    if (!string.IsNullOrEmpty(oldPasswordStart) && !ReferenceEquals(oldPasswordStart, oldPassword))
                    {
                        oldPasswordStart?.CleanupMemory();
                    }

                    if (!string.IsNullOrEmpty(oldPasswordEnd) && !ReferenceEquals(oldPasswordEnd, oldPassword))
                    {
                        oldPasswordEnd?.CleanupMemory();
                    }

                    if (!string.IsNullOrEmpty(oldPassword))
                    {
                        oldPassword.CleanupMemory();
                    }

                    pastedText?.CleanupMemory();
                }

                e.CancelCommand();
            }
        }

        public static void GetSelection(PasswordBox passwordBox, out int start, out int end)
        {
            // PasswordBox.Selection property (has value of type ITextSelection that derives from ITextRange).
            var selection = Guard.EnsureIsNotNull(_passwordBoxSelectionProperty.Value.GetValue(passwordBox, null));

            EnsureTextRangePropertiesAreInitialized(selection);

            // System.Windows.Documents.ITextRange.Start property (has value of type ITextPointer).
            var startPointer = Guard.EnsureIsNotNull(_iTextRangeStartProperty.Value.GetValue(selection, null));
            // System.Windows.Documents.ITextRange.End property (has value of type ITextPointer).
            var endPointer = Guard.EnsureIsNotNull(_iTextRangeEndProperty.Value.GetValue(selection, null));

            EnsureTextPointerOffsetPropertyIsInitialized(startPointer);

            start = Guard.EnsureIsInstanceOfType<int>(_iTextPointerOffsetProperty.Value.GetValue(startPointer, null));
            end = Guard.EnsureIsInstanceOfType<int>(_iTextPointerOffsetProperty.Value.GetValue(endPointer, null));
        }

        public static void SetSelection(PasswordBox passwordBox, int start, int length)
            => _passwordBoxSelectMethod.Value.Invoke(passwordBox, new object[] { start, length });

        private static PropertyInfo GetPasswordBoxSelectionProperty()
            => Guard.EnsureIsNotNull(typeof(PasswordBox).GetProperty("Selection", BindingFlags.Instance | BindingFlags.NonPublic));

        private static MethodInfo GetPasswordBoxSelectMethod()
            => Guard.EnsureIsNotNull(typeof(PasswordBox).GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic));

        [MemberNotNull(nameof(_iTextRangeStartProperty), nameof(_iTextRangeEndProperty))]
        private static void EnsureTextRangePropertiesAreInitialized(object selection)
        {
            if (_iTextRangeStartProperty is not null && _iTextRangeEndProperty is not null)
            {
                return;
            }

            var iTextRangeType = selection.GetType()
                .GetInterfaces()
                .GuardedSingleOrDefault(t => t.FullName == "System.Windows.Documents.ITextRange");
            Guard.IsNotNull(iTextRangeType);

            _iTextRangeStartProperty = new Lazy<PropertyInfo>(() =>
                Guard.EnsureIsNotNull(iTextRangeType.GetProperty("Start", BindingFlags.Instance | BindingFlags.Public)));

            _iTextRangeEndProperty = new Lazy<PropertyInfo>(() =>
                Guard.EnsureIsNotNull(iTextRangeType.GetProperty("End", BindingFlags.Instance | BindingFlags.Public)));
        }

        [MemberNotNull(nameof(_iTextPointerOffsetProperty))]
        private static void EnsureTextPointerOffsetPropertyIsInitialized(object textPointer)
        {
            if (_iTextPointerOffsetProperty is not null)
            {
                return;
            }

            _iTextPointerOffsetProperty = new Lazy<PropertyInfo>(() =>
                Guard.EnsureIsNotNull(textPointer.GetType().GetProperty("Offset", BindingFlags.Instance | BindingFlags.NonPublic)));
        }

        private static readonly Lazy<PropertyInfo> _passwordBoxSelectionProperty = new(GetPasswordBoxSelectionProperty);
        private static readonly Lazy<MethodInfo> _passwordBoxSelectMethod = new(GetPasswordBoxSelectMethod);

        private static Lazy<PropertyInfo>? _iTextRangeStartProperty;
        private static Lazy<PropertyInfo>? _iTextRangeEndProperty;
        private static Lazy<PropertyInfo>? _iTextPointerOffsetProperty;
    }
}
