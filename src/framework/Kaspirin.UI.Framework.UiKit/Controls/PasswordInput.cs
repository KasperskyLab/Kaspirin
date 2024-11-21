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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Extensions.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_OpenPasswordTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = PART_OpenPasswordTextBlock, Type = typeof(SecureTextBlock))]
    [TemplatePart(Name = PART_InputPasswordElement, Type = typeof(PasswordBox))]
    [TemplatePart(Name = PART_Placeholder, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_ContentStub, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_ValidationPopup, Type = typeof(Popup))]
    [ContentProperty(nameof(ActionBar))]
    public sealed class PasswordInput : Control, ISecureInputClient
    {
        public const string PART_OpenPasswordTextBox = "PART_OpenPasswordTextBox";
        public const string PART_OpenPasswordTextBlock = "PART_OpenPasswordTextBlock";
        public const string PART_InputPasswordElement = "PART_InputPasswordElement";
        public const string PART_Placeholder = "PART_Placeholder";
        public const string PART_ContentStub = "PART_ContentStub";
        public const string PART_ValidationPopup = "PART_ValidationPopup";

        static PasswordInput()
        {
            IsEnabledProperty.OverrideMetadata(typeof(PasswordInput), new UIPropertyMetadata(OnIsEnabledChanged));
        }

        public PasswordInput()
        {
            _sessionProvider = ServiceLocator.Instance.GetService<ISessionProvider>();
            _secureInputManager = ServiceLocator.Instance.GetService<ISecureInputManager>();

            MouseLeftButtonDown += (o, e) => SetFocus();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;

            AddHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler(OnPasting));
        }

        #region ActionBar

        public InputActionCollection ActionBar
        {
            get => (InputActionCollection)GetValue(ActionBarProperty);
            set => SetValue(ActionBarProperty, value);
        }

        public static readonly DependencyProperty ActionBarProperty = DependencyProperty.Register(
            nameof(ActionBar),
            typeof(InputActionCollection),
            typeof(PasswordInput));

        #endregion

        #region FontMode

        public InputFontMode FontMode
        {
            get => (InputFontMode)GetValue(FontModeProperty);
            set => SetValue(FontModeProperty, value);
        }

        public static readonly DependencyProperty FontModeProperty = DependencyProperty.Register(
            nameof(FontMode),
            typeof(InputFontMode),
            typeof(PasswordInput),
            new PropertyMetadata(InputFontMode.Regular));

        #endregion

        #region InputHorizontalAlignment

        public HorizontalAlignment InputHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(InputHorizontalAlignmentProperty);
            set => SetValue(InputHorizontalAlignmentProperty, value);
        }

        public static readonly DependencyProperty InputHorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(InputHorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(PasswordInput),
            new PropertyMetadata(HorizontalAlignment.Left));

        #endregion

        #region InputWidth

        public double InputWidth
        {
            get => (double)GetValue(InputWidthProperty);
            set => SetValue(InputWidthProperty, value);
        }

        public static readonly DependencyProperty InputWidthProperty = DependencyProperty.Register(
            nameof(InputWidth),
            typeof(double),
            typeof(PasswordInput), new PropertyMetadata(double.NaN));

        #endregion

        #region Caption

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
            nameof(Caption),
            typeof(string),
            typeof(PasswordInput));

        #endregion

        #region GetFocusBehavior

        public InputGetFocusBehaviorType GetFocusBehavior
        {
            get => (InputGetFocusBehaviorType)GetValue(GetFocusBehaviorProperty);
            set => SetValue(GetFocusBehaviorProperty, value);
        }

        public static readonly DependencyProperty GetFocusBehaviorProperty = DependencyProperty.Register(
            nameof(GetFocusBehavior),
            typeof(InputGetFocusBehaviorType),
            typeof(PasswordInput),
            new PropertyMetadata(InputGetFocusBehaviorType.Default, OnGetFocusBehaviorChanged));

        private static void OnGetFocusBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((PasswordInput)d).UpdateGetFocusBehavior();

        #endregion

        #region LostFocusBehavior

        public InputLostFocusBehaviorType LostFocusBehavior
        {
            get => (InputLostFocusBehaviorType)GetValue(LostFocusBehaviorProperty);
            set => SetValue(LostFocusBehaviorProperty, value);
        }

        public static readonly DependencyProperty LostFocusBehaviorProperty = DependencyProperty.Register(
            nameof(LostFocusBehavior),
            typeof(InputLostFocusBehaviorType),
            typeof(PasswordInput),
            new PropertyMetadata(InputLostFocusBehaviorType.Default));

        #endregion

        #region FlowBehavior

        public TextInputFlowBehaviorType FlowBehavior
        {
            get => (TextInputFlowBehaviorType)GetValue(FlowBehaviorProperty);
            set => SetValue(FlowBehaviorProperty, value);
        }

        public static readonly DependencyProperty FlowBehaviorProperty = DependencyProperty.Register(
            nameof(FlowBehavior),
            typeof(TextInputFlowBehaviorType),
            typeof(PasswordInput),
            new PropertyMetadata(TextInputFlowBehaviorType.Default, OnFlowBehaviorChanged));

        private static void OnFlowBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((PasswordInput)d).UpdateFlowBehavior();

        #endregion

        #region InputFilter

        public IInputFilter? InputFilter
        {
            get => (IInputFilter?)GetValue(InputFilterProperty);
            set => SetValue(InputFilterProperty, value);
        }

        public static readonly DependencyProperty InputFilterProperty = DependencyProperty.Register(
            nameof(InputFilter),
            typeof(IInputFilter),
            typeof(PasswordInput));

        #endregion

        #region IsInvalidState

        public bool IsInvalidState
        {
            get => (bool)GetValue(IsInvalidStateProperty);
            set => SetValue(IsInvalidStateProperty, value);
        }

        public static readonly DependencyProperty IsInvalidStateProperty = DependencyProperty.Register(
            nameof(IsInvalidState),
            typeof(bool),
            typeof(PasswordInput));

        #endregion

        #region InvalidStatePopupContent

        public string InvalidStatePopupContent
        {
            get => (string)GetValue(InvalidStatePopupContentProperty);
            set => SetValue(InvalidStatePopupContentProperty, value);
        }

        public static readonly DependencyProperty InvalidStatePopupContentProperty = DependencyProperty.Register(
            nameof(InvalidStatePopupContent),
            typeof(string),
            typeof(PasswordInput));

        #endregion

        #region InvalidStatePopupPosition

        public PopupPosition InvalidStatePopupPosition
        {
            get => (PopupPosition)GetValue(InvalidStatePopupPositionProperty);
            set => SetValue(InvalidStatePopupPositionProperty, value);
        }

        public static readonly DependencyProperty InvalidStatePopupPositionProperty = DependencyProperty.Register(
            nameof(InvalidStatePopupPosition),
            typeof(PopupPosition),
            typeof(PasswordInput),
            new PropertyMetadata(PopupPosition.Right));

        #endregion

        #region Label

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            nameof(Label),
            typeof(string),
            typeof(PasswordInput));

        #endregion

        #region SetFocusFlag

        public bool SetFocusFlag
        {
            get => (bool)GetValue(SetFocusFlagProperty);
            set => SetValue(SetFocusFlagProperty, value);
        }

        public static readonly DependencyProperty SetFocusFlagProperty = DependencyProperty.Register(
            nameof(SetFocusFlag),
            typeof(bool),
            typeof(PasswordInput),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSetFocusFlagChanged));

        private static void OnSetFocusFlagChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordInput = (PasswordInput)d;
            if (passwordInput.SetFocusFlag)
            {
                passwordInput.SetSelection();
                passwordInput.SetFocus();

                Executers.InUiAsync(() => passwordInput.SetCurrentValue(SetFocusFlagProperty, false));
            }
        }

        #endregion

        #region Placeholder

        public string? Placeholder
        {
            get => (string?)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            nameof(Placeholder),
            typeof(string),
            typeof(PasswordInput));

        #endregion

        #region RevealMode

        public PasswordInputRevealMode RevealMode
        {
            get => (PasswordInputRevealMode)GetValue(RevealModeProperty);
            set => SetValue(RevealModeProperty, value);
        }

        public static readonly DependencyProperty RevealModeProperty = DependencyProperty.Register(
            nameof(RevealMode),
            typeof(PasswordInputRevealMode),
            typeof(PasswordInput),
            new PropertyMetadata(PasswordInputRevealMode.ReadOnly, OnRevealModeChanged));

        private static void OnRevealModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((PasswordInput)d).InvalidateReveal();

        #endregion

        #region IsPasswordRevealed

        public bool IsPasswordRevealed
        {
            get => (bool)GetValue(IsPasswordRevealedProperty);
            set => SetValue(IsPasswordRevealedProperty, value);
        }

        public static readonly DependencyProperty IsPasswordRevealedProperty = DependencyProperty.Register(
            nameof(IsPasswordRevealed),
            typeof(bool),
            typeof(PasswordInput),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsPasswordRevealedChanged));

        private static void OnIsPasswordRevealedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordInput passwordInput)
            {
                passwordInput.InvalidateReveal();
                passwordInput.SyncCaretPosition();
            }
        }

        #endregion

        #region Password

        public SecureString? Password
        {
            get => (SecureString?)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
            nameof(Password),
            typeof(SecureString),
            typeof(PasswordInput),
            new FrameworkPropertyMetadata(new SecureString(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged)
            {
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            });

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var passwordInput = (PasswordInput)d;

            passwordInput.TryWritePasswordToInputPasswordElement();
            passwordInput.TryWritePasswordToOpenPasswordTextBox();
        }

        #endregion

        #region MaxLength

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
            nameof(MaxLength),
            typeof(int),
            typeof(PasswordInput),
            new PropertyMetadata(0));

        #endregion

        #region ISecureInputClient

        public bool HandleKeyPress(SecureInputAction inputAction)
        {
            Guard.ArgumentIsNotNull(inputAction);

            var isControlKeysNotPressedExceptShift = _pressedControlKeys.All(k => k == VirtualKey.VK_SHIFT);
            if (isControlKeysNotPressedExceptShift && inputAction.InputText != null)
            {
                AppendText(inputAction.InputText);
                return true;
            }

            _pressedControlKeys.Add(inputAction.VirtualKey);
            return false;
        }

        public bool HandleKeyRelease(SecureInputAction inputAction)
        {
            Guard.ArgumentIsNotNull(inputAction);

            if (!_pressedControlKeys.Contains(inputAction.VirtualKey))
            {
                return true;
            }

            _pressedControlKeys.Remove(inputAction.VirtualKey);
            return false;
        }

        #endregion

        public void Clear()
            => SetCurrentValue(PasswordProperty, SecureStringExtensions.Empty);

        public override void OnApplyTemplate()
        {
            _placeholderTextBlock = (TextBlock)GetTemplateChild(PART_Placeholder);
            _contentStubTextBlock = (TextBlock)GetTemplateChild(PART_ContentStub);
            _openPasswordTextBlock = (SecureTextBlock)GetTemplateChild(PART_OpenPasswordTextBlock);
            _openPasswordTextBox = (TextBox)GetTemplateChild(PART_OpenPasswordTextBox);
            _inputPasswordElement = (PasswordBox)GetTemplateChild(PART_InputPasswordElement);

            _openPasswordTextBox.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);

            _inputPasswordElement.PasswordChanged += OnPasswordBoxPasswordChanged;

            _placeholderTextBlock.SetBinding(TextBlock.TextProperty, new Binding() { Source = this, Path = PlaceholderProperty.AsPath() });
            _placeholderTextBlock.SetBinding(VisibilityProperty, new Binding() { Source = this, Path = PasswordProperty.AsPath(), Converter = new PlaceholderVisibilityConverter() });

            SetTextStyleBindings(_inputPasswordElement, _contentStubTextBlock);
            SetTextStyleBindings(_openPasswordTextBox, _contentStubTextBlock);

            SetCaretBrushBindings(_inputPasswordElement, _contentStubTextBlock, PasswordBox.CaretBrushProperty);
            SetCaretBrushBindings(_openPasswordTextBox, _contentStubTextBlock, TextBox.CaretBrushProperty);

            TryWritePasswordToInputPasswordElement();
        }

        internal void SetFocus()
        {
            var isUnloaded = false;

            this.WhenUnloaded(() => isUnloaded = true);

            Executers.InUiAsync(() =>
            {
                if (isUnloaded)
                {
                    return;
                }

                if (IsKeyboardFocusWithin)
                {
                    return;
                }

                if (_openPasswordTextBox == null || _inputPasswordElement == null)
                {
                    return;
                }

                if (IsPasswordRevealed && RevealMode == PasswordInputRevealMode.Editable)
                {
                    InputFocusManager.SetInputFocus(_openPasswordTextBox);
                }
                else
                {
                    InputFocusManager.SetInputFocus(_inputPasswordElement);
                }
            },
            DispatcherPriority.Input);
        }

        internal bool TryGetSelection([NotNullWhen(true)] out int? selectionStart, [NotNullWhen(true)] out int? selectionEnd)
        {
            selectionStart = selectionEnd = default;

            if (_openPasswordTextBox == null || _inputPasswordElement == null)
            {
                return false;
            }

            if (IsPasswordRevealed && RevealMode == PasswordInputRevealMode.Editable)
            {
                selectionStart = _openPasswordTextBox.SelectionStart;
                selectionEnd = _openPasswordTextBox.SelectionStart + _openPasswordTextBox.SelectionLength;
            }
            else
            {
                PasswordInputHelper.GetSelection(_inputPasswordElement, out var start, out var end);

                selectionStart = start;
                selectionEnd = end;
            }

            return true;
        }

        internal void SetSelection(int caretIndex = -1, int length = 0)
        {
            if (_openPasswordTextBox == null || _inputPasswordElement == null)
            {
                return;
            }

            caretIndex = caretIndex == -1
                ? Password?.Length ?? 0
                : caretIndex;

            if (IsPasswordRevealed && RevealMode == PasswordInputRevealMode.Editable)
            {
                _openPasswordTextBox.Select(caretIndex, length);
            }
            else
            {
                PasswordInputHelper.SetSelection(_inputPasswordElement, caretIndex, length);
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            UnregisterInputClient();
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);

            RegisterInputClient();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);

            UnregisterInputClient();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            var filteredInput = FilterInput(e.Text);
            e.Handled = string.IsNullOrEmpty(filteredInput);

            base.OnPreviewTextInput(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // OnPreviewTextInput does not intercept spaces.
            // So we need to process spaces as a special case.
            if (e.Key == Key.Space)
            {
                var filteredInput = FilterInput(" ");
                e.Handled = string.IsNullOrEmpty(filteredInput);
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);

            e.Handled = true;

            RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = MouseWheelEvent,
                Source = this
            });
        }

        private void OnPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
            => TryReadPasswordFromInputPasswordElement();

        private void OnOpenPasswordTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (IsPasswordRevealed && RevealMode == PasswordInputRevealMode.Editable)
            {
                var openPasswordTextBox = (TextBox)sender;

                _lastCaretSelectionStart = openPasswordTextBox.SelectionStart;
                _lastCaretSelectionLength = openPasswordTextBox.SelectionLength;
            }
        }

        private void OnOpenPasswordTextBoxTextChanged(object sender, TextChangedEventArgs e)
            => TryReadPasswordFromOpenPasswordTextBox();

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (FlowBehavior == TextInputFlowBehaviorType.RtlSimulation)
            {
                UpdateFlowBehavior();
            }

            InvalidateReveal();
            UpdateGetFocusBehavior();
            UpdateLostFocusBehavior();

            _sessionProvider.OnDeactivateSession += OnDeactivateSession;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _sessionProvider.OnDeactivateSession -= OnDeactivateSession;

            InvalidateReveal();
            UnregisterInputClient();
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
            => PasswordInputHelper.OnPasting(sender, e);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is false)
            {
                // When focused input is disabled, it automatically looses focus and also switches to broken state.
                // Due to this state focus behavior stops working as expected: when input is focused programmatically,
                // it has no focus border and has a static caret (it's not blinking as usual).
                // The reason is that underlying PasswordBox/TextBox doesn't actually receive keyboard focus. Looks like a bug in WPF.
                // To mitigate this issue, we should clear focus from the input when it's disabled.
                InputFocusManager.ClearInputFocus((PasswordInput)d);
            }
        }

        private void UpdateFlowBehavior()
        {
            switch (FlowBehavior)
            {
                case TextInputFlowBehaviorType.RtlSimulation:
                    if (_openPasswordTextBox != null)
                    {
                        _openPasswordTextBox.SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
                        _openPasswordTextBox.SetBinding(TextBox.TextAlignmentProperty, new Binding()
                        {
                            Source = this.GetWindow(),
                            Path = FlowDirectionProperty.AsPath(),
                            Converter = new RtlSimulationConverter()
                        });
                    }

                    if (_openPasswordTextBlock != null)
                    {
                        _openPasswordTextBlock.SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
                        _openPasswordTextBlock.SetBinding(SecureTextBlock.TextAlignmentProperty, new Binding()
                        {
                            Source = this.GetWindow(),
                            Path = FlowDirectionProperty.AsPath(),
                            Converter = new RtlSimulationConverter()
                        });
                    }

                    break;

                case TextInputFlowBehaviorType.Default:
                default:
                    if (_openPasswordTextBox != null)
                    {
                        _openPasswordTextBox.ClearValue(FlowDirectionProperty);
                        _openPasswordTextBox.ClearValue(TextBox.TextAlignmentProperty);
                    }

                    if (_openPasswordTextBlock != null)
                    {
                        _openPasswordTextBlock.ClearValue(FlowDirectionProperty);
                        _openPasswordTextBlock.ClearValue(SecureTextBlock.TextAlignmentProperty);
                    }

                    break;
            }
        }

        private void UpdateGetFocusBehavior()
        {
            void Focus()
            {
                SetSelection();
                SetFocus();
            }

            void FocusAndSelect()
            {
                SetSelection(caretIndex: 0, length: Password?.Length ?? 0);
                SetFocus();
            }

            switch (GetFocusBehavior)
            {
                case InputGetFocusBehaviorType.FocusWhenLoaded:
                    this.WhenLoaded(Focus);
                    break;

                case InputGetFocusBehaviorType.FocusWhenVisible:
                    this.WhenVisible(Focus);
                    break;

                case InputGetFocusBehaviorType.FocusAndSelectWhenLoaded:
                    this.WhenLoaded(FocusAndSelect);
                    break;

                case InputGetFocusBehaviorType.FocusAndSelectWhenVisible:
                    this.WhenVisible(FocusAndSelect);
                    break;

                case InputGetFocusBehaviorType.Default:
                default:
                    break;
            }
        }

        private void UpdateLostFocusBehavior()
            => this.GetWindow()?.SetValue(PasswordInputLostFocusHandler.IsEnabledProperty, true);

        private void OnDeactivateSession()
        {
            InputFocusManager.ClearInputFocus(this);

            UnregisterInputClient();
        }

        private void TryReadPasswordFromInputPasswordElement()
        {
            UpdatePassword(() =>
            {
                if (_inputPasswordElement != null)
                {
                    Password = _inputPasswordElement.SecurePassword;
                }
            }, ref _isUpdatingPassword);
        }

        private void TryReadPasswordFromOpenPasswordTextBox()
        {
            UpdatePassword(() =>
            {
                if (_openPasswordTextBox != null)
                {
                    Password = _openPasswordTextBox.Text?.ToSecureString();
                }
            }, ref _isUpdatingRevealedPassword);
        }

        private void TryWritePasswordToInputPasswordElement()
        {
            UpdatePassword(() =>
            {
                if (_inputPasswordElement == null || _inputPasswordElement.SecurePassword.IsEqualTo(Password))
                {
                    return;
                }

                var password = Password.ToSimpleString();
                try
                {
                    _inputPasswordElement.Password = password;
                }
                finally
                {
                    password.CleanupMemory();
                }
            }, ref _isUpdatingPassword);
        }

        private void TryWritePasswordToOpenPasswordTextBox()
        {
            UpdatePassword(() =>
            {
                if (_openPasswordTextBox == null)
                {
                    return;
                }

                var oldPassword = _openPasswordTextBox.Text;
                var newPassword = Password.ToSimpleString();

                var cleanupOldPassword = false;
                var cleanupNewPassword = true;

                try
                {
                    if (oldPassword == newPassword)
                    {
                        return;
                    }

                    if (RevealMode != PasswordInputRevealMode.Editable || !IsPasswordRevealed || !IsLoaded)
                    {
                        return;
                    }

                    cleanupOldPassword = true;
                    cleanupNewPassword = false;

                    _openPasswordTextBox.Text = newPassword;
                }
                finally
                {
                    if (cleanupOldPassword)
                    {
                        oldPassword.CleanupMemory();
                    }

                    if (cleanupNewPassword)
                    {
                        newPassword.CleanupMemory();
                    }
                }
            }, ref _isUpdatingRevealedPassword);
        }

        private string? FilterInput(string value)
            => InputFilter?.FilterInput(value) ?? value;

        private void SetCaretBrushBindings(FrameworkElement target, TextBlock source, DependencyProperty property)
        {
            target.SetBinding(property, new Binding()
            {
                Source = source,
                Path = TextBlock.BackgroundProperty.AsPath(),
                Converter = new InputCaretBrushConverter()
            });
        }

        private void SetTextStyleBindings(FrameworkElement target, TextBlock source)
        {
            target.LinkTextStyle(source);
            target.SetBinding(MarginProperty, new MultiBinding()
            {
                Bindings =
                {
                    new Binding() { Source = _contentStubTextBlock, Path = TextBlock.MarginProperty.AsPath() },
                    new Binding() { Source = _contentStubTextBlock, Path = TextBlock.TextAlignmentProperty.AsPath() }
                },
                Converter = new InputContentHostMarginConverter()
            });
        }

        private void AppendText(string text)
        {
            var isOpenPasswordEditing = RevealMode == PasswordInputRevealMode.Editable && IsPasswordRevealed;

            var target = isOpenPasswordEditing
                ? _openPasswordTextBox as IInputElement
                : _inputPasswordElement as IInputElement;

            if (target != null)
            {
                var composition = new TextComposition(InputManager.Current, target, text);
                var keyboard = InputManager.Current.PrimaryKeyboardDevice;
                var eventArgs = new TextCompositionEventArgs(keyboard, composition)
                {
                    RoutedEvent = TextCompositionManager.TextInputEvent
                };

                target.RaiseEvent(eventArgs);

                CommandManager.InvalidateRequerySuggested();
            }
        }

        private void RegisterInputClient()
        {
            if (!_sessionProvider.IsSessionActive)
            {
                InputFocusManager.ClearInputFocus(this);
                return;
            }

            if (_isRegistered)
            {
                return;
            }

            if (!_secureInputManager.IsSecureInputAvailable)
            {
                return;
            }

            _secureInputManager.RegisterClient(this);

            _isRegistered = true;
        }

        private void UnregisterInputClient()
        {
            if (!_isRegistered)
            {
                return;
            }

            _secureInputManager.UnregisterClient(this);
            _pressedControlKeys.Clear();

            _isRegistered = false;
        }

        private void UpdatePassword(Action updateAction, ref bool isUpdating)
        {
            if (isUpdating)
            {
                return;
            }

            isUpdating = true;
            try
            {
                updateAction();
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void InvalidateReveal()
        {
            if (_openPasswordTextBlock != null)
            {
                if (RevealMode == PasswordInputRevealMode.ReadOnly && IsPasswordRevealed && IsLoaded)
                {
                    _openPasswordTextBlock.SetBinding(SecureTextBlock.SecureTextProperty, new Binding()
                    {
                        Source = this,
                        Path = PasswordInput.PasswordProperty.AsPath()
                    });
                }
                else
                {
                    BindingOperations.ClearBinding(_openPasswordTextBlock, SecureTextBlock.SecureTextProperty);
                }
            }

            if (_openPasswordTextBox != null)
            {
                if (RevealMode == PasswordInputRevealMode.Editable && IsPasswordRevealed && IsLoaded)
                {
                    _openPasswordTextBox.Text = Password.ToSimpleString();
                    _openPasswordTextBox.TextChanged += OnOpenPasswordTextBoxTextChanged;
                    _openPasswordTextBox.SelectionChanged += OnOpenPasswordTextBoxSelectionChanged;
                }
                else
                {
                    _openPasswordTextBox.SelectionChanged -= OnOpenPasswordTextBoxSelectionChanged;
                    _openPasswordTextBox.TextChanged -= OnOpenPasswordTextBoxTextChanged;

                    var text = _openPasswordTextBox.Text;
                    try
                    {
                        _openPasswordTextBox.Text = string.Empty;
                    }
                    finally
                    {
                        text.CleanupMemory();
                    }
                }
            }
        }

        private void SyncCaretPosition()
        {
            Executers.InUiAsync(() =>
            {
                if (!IsKeyboardFocusWithin)
                {
                    return;
                }

                if (_openPasswordTextBox == null || _inputPasswordElement == null)
                {
                    return;
                }

                if (IsPasswordRevealed && RevealMode == PasswordInputRevealMode.Editable)
                {
                    InputFocusManager.SetInputFocus(_openPasswordTextBox);

                    PasswordInputHelper.GetSelection(
                        _inputPasswordElement,
                        out var selectionStart,
                        out var selectionEnd);

                    _lastCaretSelectionStart = selectionStart;
                    _lastCaretSelectionLength = selectionEnd - selectionStart;

                    _openPasswordTextBox.Select(_lastCaretSelectionStart, _lastCaretSelectionLength);
                }
                else if (!IsPasswordRevealed && RevealMode == PasswordInputRevealMode.Editable)
                {
                    InputFocusManager.SetInputFocus(_inputPasswordElement);

                    if (_lastCaretSelectionStart != -1 &&
                       _lastCaretSelectionLength != -1)
                    {
                        PasswordInputHelper.SetSelection(
                            _inputPasswordElement,
                            _lastCaretSelectionStart,
                            _lastCaretSelectionLength);
                    }
                    else
                    {
                        PasswordInputHelper.SetSelection(
                            _inputPasswordElement,
                            Password?.Length ?? 0,
                            0);
                    }

                    _lastCaretSelectionLength = -1;
                    _lastCaretSelectionStart = -1;
                }
            }, DispatcherPriority.Input);
        }

        private sealed class PlaceholderVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is SecureString secureString && secureString.Length > 0)
                {
                    return Visibility.Collapsed;
                }

                return Visibility.Visible;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
                => throw new NotImplementedException();
        }

        private static class PasswordInputLostFocusHandler
        {
            #region IsEnabled

            public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(PasswordInputLostFocusHandler),
                new PropertyMetadata(false, OnIsEnabledChanged));

            public static bool GetIsEnabled(UIElement element)
                => (bool)element.GetValue(IsEnabledProperty);

            public static void SetIsEnabled(UIElement element, bool value)
                => element.SetValue(IsEnabledProperty, value);

            private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var window = (Window)d;

                if (e.NewValue is true)
                {
                    window.MouseLeftButtonDown += OnMouseLeftButtonDown;
                }
                else
                {
                    window.MouseLeftButtonDown -= OnMouseLeftButtonDown;
                }
            }

            #endregion

            private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                var window = (Window)sender;
                var focusedElement = FocusManager.GetFocusedElement(window);

                if (focusedElement is not FrameworkElement focusedFE)
                {
                    return;
                }

                if (focusedFE.TemplatedParent is not PasswordInput passwordInput ||
                    passwordInput.LostFocusBehavior != InputLostFocusBehaviorType.WhenClickOutside)
                {
                    return;
                }

                if (e.OriginalSource is not DependencyObject originalSource)
                {
                    return;
                }

                if (originalSource.FindVisualParent<PasswordInput>() != passwordInput)
                {
                    InputFocusManager.ClearInputFocus(focusedFE);
                }
            }
        }

        private readonly ISecureInputManager _secureInputManager;
        private readonly ISessionProvider _sessionProvider;
        private readonly HashSet<VirtualKey> _pressedControlKeys = new();

        private SecureTextBlock? _openPasswordTextBlock;
        private TextBlock? _placeholderTextBlock;
        private TextBlock? _contentStubTextBlock;
        private TextBox? _openPasswordTextBox;
        private PasswordBox? _inputPasswordElement;

        private bool _isRegistered;
        private bool _isUpdatingPassword;
        private bool _isUpdatingRevealedPassword;
        private int _lastCaretSelectionStart = -1;
        private int _lastCaretSelectionLength = -1;
    }
}
