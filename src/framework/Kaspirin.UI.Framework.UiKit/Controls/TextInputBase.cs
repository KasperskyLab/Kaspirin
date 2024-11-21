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
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Extensions.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_Placeholder, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_ContentStub, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_ContentHost, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_Counter, Type = typeof(TextBlock))]
    public abstract class TextInputBase : TextBox
    {
        public const string PART_Placeholder = "PART_Placeholder";
        public const string PART_ContentStub = "PART_ContentStub";
        public const string PART_ContentHost = "PART_ContentHost";
        public const string PART_Counter = "PART_Counter";

        static TextInputBase()
        {
            IsEnabledProperty.OverrideMetadata(typeof(TextInputBase), new UIPropertyMetadata(OnIsEnabledChanged));

            TextProperty.OverrideMetadata(
                typeof(TextInputBase),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceText)
                {
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });
        }

        public TextInputBase()
        {
            Loaded += OnLoaded;

            DataObject.AddPastingHandler(this, OnPasting);
        }

        #region Caption

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
            nameof(Caption),
            typeof(string),
            typeof(TextInputBase));

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
            typeof(TextInputBase),
            new PropertyMetadata(InputGetFocusBehaviorType.Default, OnGetFocusBehaviorChanged));

        private static void OnGetFocusBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((TextInputBase)d).UpdateGetFocusBehavior();

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
            typeof(TextInputBase),
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
            typeof(TextInputBase),
            new PropertyMetadata(TextInputFlowBehaviorType.Default, OnFlowBehaviorChanged));

        private static void OnFlowBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((TextInputBase)d).UpdateFlowBehavior();

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
            typeof(TextInputBase));

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
            typeof(TextInputBase));

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
            typeof(TextInputBase),
            new PropertyMetadata(PopupPosition.Right));

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
            typeof(TextInputBase));

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
            typeof(TextInputBase));

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
            typeof(TextInputBase),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSetFocusFlagChanged));

        private static void OnSetFocusFlagChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textInputBase = (TextInputBase)d;
            if (textInputBase.SetFocusFlag)
            {
                textInputBase.SetSelection();
                textInputBase.SetFocus();

                Executers.InUiAsync(() => textInputBase.SetCurrentValue(SetFocusFlagProperty, false));
            }
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);

            _placeholderTextBlock = GetTemplateChild(PART_Placeholder) as TextBlock;
            _contentStubTextBlock = GetTemplateChild(PART_ContentStub) as TextBlock;
            if (_contentStubTextBlock != null)
            {
                this.LinkTextStyle(_contentStubTextBlock);
                this.SetBinding(CaretBrushProperty, new Binding()
                {
                    Source = _contentStubTextBlock,
                    Path = TextBlock.BackgroundProperty.AsPath(),
                    Converter = new InputCaretBrushConverter()
                });
            }

            _contentHost = GetTemplateChild(PART_ContentHost) as FrameworkElement;
            _contentHost?.SetBinding(MarginProperty, new MultiBinding()
            {
                Bindings =
                {
                    new Binding() { Source = _contentStubTextBlock, Path = MarginProperty.AsPath() },
                    new Binding() { Source = _contentStubTextBlock, Path = TextAlignmentProperty.AsPath() }
                },
                Converter = new InputContentHostMarginConverter()
            });

            _counterTextBlock = GetTemplateChild(PART_Counter) as TextBlock;
            _counterTextBlock?.SetBinding(TextBlock.TextProperty, new MultiBinding()
            {
                Bindings =
                {
                    new Binding() { Source = this, Path = TextProperty.AsPath() },
                    new Binding() { Source = this, Path = MaxLengthProperty.AsPath() }
                },
                Converter = new CounterFormatConverter()
            });

            InvalidateText();
            InvalidatePlaceholderStyle();
            InvalidateImeState();
        }

        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            if (e.OriginalSource is DependencyObject source && source.FindVisualParent<ScrollViewer>() != _contentHost)
            {
                e.Handled = true;
            }

            base.OnContextMenuOpening(e);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.ClickCount == 3 && Text != null)
            {
                var clickPoint = e.GetPosition(this);
                var charIndex = GetCharacterIndexFromPoint(clickPoint, true);

                var paragraphStart = FindParagraphStart(charIndex);
                var paragraphEnd = FindParagraphEnd(charIndex);
                var paragraphLength = paragraphEnd - paragraphStart;

                SetSelection(paragraphStart, paragraphLength);
            }

            base.OnPreviewMouseLeftButtonDown(e);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            var newText = ProcessInput(e.Text);
            var textChanged = FilterText(newText, out _);
            e.Handled = !textChanged;

            base.OnPreviewTextInput(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // OnPreviewTextInput does not intercept spaces.
            // So we need to process spaces as a special case.
            if (e.Key == Key.Space)
            {
                var newText = ProcessInput(" ");
                var textChanged = FilterText(newText, out _);
                e.Handled = !textChanged;
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            InvalidateTextBinding();
            InvalidateCursorPosition(e);

            base.OnTextChanged(e);

            _lastFilteredText = default;
        }

        internal void InvalidateText()
            => InvalidateProperty(TextProperty);

        internal void InvalidatePlaceholderStyle()
        {
            if (_placeholderTextBlock == null || _contentStubTextBlock == null)
            {
                return;
            }

            var placeholder = TextInputBaseInternals.GetPlaceholder(this);
            if (placeholder is TextInputMaskPlaceholder)
            {
                _placeholderTextBlock.SetBinding(StyleProperty, new Binding()
                {
                    Source = _contentStubTextBlock,
                    Path = StyleProperty.AsPath()
                });
            }
            else
            {
                _placeholderTextBlock.ClearValue(StyleProperty);
            }
        }

        private void InvalidateCursorPosition(TextChangedEventArgs e)
        {
            var placeholder = TextInputBaseInternals.GetPlaceholder(this);
            if (placeholder is not TextInputMaskPlaceholder maskPlaceholder)
            {
                return;
            }

            if (e.Changes.Count != 1)
            {
                return;
            }

            var changeInfo = e.Changes.Single();
            var changeOffset = changeInfo.Offset;

            var caretIndex = CaretIndex;

            var isOneCharAdded = changeInfo.AddedLength == 1 && changeInfo.RemovedLength == 0;
            if (isOneCharAdded)
            {
                caretIndex = maskPlaceholder
                    .GetMaskItems()
                    .TakeWhile((item, index) => index < changeOffset || index >= changeOffset && item is TextInputMaskStaticItem)
                    .Count() + 1;
            }
            else
            {
                var isOneCharRemoved = changeInfo.AddedLength == 0 && changeInfo.RemovedLength == 1;
                if (isOneCharRemoved)
                {
                    caretIndex = changeOffset;
                }
            }

            if (CaretIndex != caretIndex)
            {
                CaretIndex = caretIndex;
            }
        }

        private void InvalidateTextBinding()
        {
            if (_inputValueFiltered is false)
            {
                return;
            }

            _inputValueFiltered = false;

            var hasBinding = BindingOperations.GetBindingExpressionBase(this, TextProperty) != null;
            if (hasBinding)
            {
                SetCurrentValue(TextProperty, Text);
            }
        }

        internal void InvalidateImeState()
        {
            var placeholder = TextInputBaseInternals.GetPlaceholder(this);
            InputMethod.SetIsInputMethodEnabled(this, placeholder is not TextInputMaskPlaceholder);
        }

        private static object? CoerceText(DependencyObject d, object? baseValue)
        {
            var textInput = (TextInputBase)d;
            var inputValue = textInput._lastFilteredText ?? (string?)baseValue;

            if (textInput.FilterText(inputValue, out var filteredValue))
            {
                textInput._inputValueFiltered = !string.Equals(inputValue, filteredValue, StringComparison.CurrentCulture);
            }

            return filteredValue;
        }

        private bool FilterText(string? value, out string? filteredValue)
        {
            var placeholder = TextInputBaseInternals.GetPlaceholder(this) ?? TextInputStringPlaceholder.Empty;

            filteredValue = InputFilter?.FilterInput(value) ?? value;
            filteredValue = placeholder.FilterInputText(filteredValue);

            var textChanged = !string.Equals(filteredValue, Text, StringComparison.CurrentCulture);

            if (_placeholderTextBlock != null)
            {
                var isWindowRtl = GetRootWindowFlowDirection() == FlowDirection.RightToLeft;
                var isRtlSimulation = FlowBehavior == TextInputFlowBehaviorType.RtlSimulation;

                var placeholderTextInlines = placeholder.GetPlaceholderText(filteredValue, isRTL: isWindowRtl && !isRtlSimulation);

                _placeholderTextBlock.Inlines.Clear();
                _placeholderTextBlock.Inlines.AddRange(placeholderTextInlines);
            }

            if (textChanged)
            {
                _lastFilteredText = filteredValue;
            }

            return textChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (FlowBehavior == TextInputFlowBehaviorType.RtlSimulation)
            {
                UpdateFlowBehavior();
            }

            UpdateGetFocusBehavior();
            UpdateLostFocusBehavior();
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var pastedText = SecureClipboard.GetClipboardContent();
            if (pastedText != null)
            {
                var newText = ProcessInput(pastedText);
                var textChanged = FilterText(newText, out _);

                if (!textChanged)
                {
                    e.CancelCommand();
                }
            }
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is false)
            {
                // When focused input is disabled, it automatically looses focus and also switches to broken state.
                // Due to this state focus behavior stops working as expected: when input is focused programmatically,
                // it has no focus border and has a static caret (it's not blinking as usual).
                // The reason is that underlying TextBox doesn't actually receive keyboard focus. Looks like a bug in WPF.
                // To mitigate this issue, we should clear focus from the input when it's disabled.
                InputFocusManager.ClearInputFocus((TextInputBase)d);
            }
        }

        private void UpdateFlowBehavior()
        {
            switch (FlowBehavior)
            {
                case TextInputFlowBehaviorType.RtlSimulation:
                    SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
                    SetBinding(TextAlignmentProperty, new Binding()
                    {
                        Source = this.GetWindow(),
                        Path = FlowDirectionProperty.AsPath(),
                        Converter = new RtlSimulationConverter()
                    });

                    break;

                case TextInputFlowBehaviorType.Default:
                default:
                    ClearValue(FlowDirectionProperty);
                    ClearValue(TextAlignmentProperty);

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
                SetSelection(caretIndex: 0, length: Text?.Length ?? 0);
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
            => this.GetWindow()?.SetValue(TextInputLostFocusHandler.IsEnabledProperty, true);

        private void SetFocus()
        {
            var isUnloaded = false;

            this.WhenUnloaded(() => isUnloaded = true);

            Executers.InUiAsync(() =>
            {
                if (isUnloaded)
                {
                    return;
                }

                BringIntoView();

                InputFocusManager.SetInputFocus(this);
            },
            DispatcherPriority.Input);
        }

        private void SetSelection(int caretIndex = -1, int length = 0)
        {
            caretIndex = caretIndex == -1
                ? Text?.Length ?? 0
                : caretIndex;

            Select(caretIndex, length);
        }

        private string ProcessInput(string input)
        {
            var text = Text ?? string.Empty;

            var selectionStart = SelectionStart;
            var selectionEnd = SelectionStart + SelectionLength;

            var oldValueStart = text.Substring(0, selectionStart);
            var oldValueEnd = text.Substring(selectionEnd);

            var newValueUnrestricted = string.Concat(oldValueStart, input, oldValueEnd);
            var newValue = MaxLength > 0
                ? newValueUnrestricted.Substring(0, Math.Min(newValueUnrestricted.Length, MaxLength))
                : newValueUnrestricted;

            return newValue;
        }

        private int FindParagraphStart(int charIndex)
        {
            var i = charIndex;

            while (i > 0)
            {
                if (Text[i - 1] == '\r' && Text[i] == '\n')
                {
                    return i;
                }

                i--;
            }

            return 0;
        }

        private int FindParagraphEnd(int charIndex)
        {
            var i = charIndex;

            while (i < Text.Length - 1)
            {
                if (Text[i] == '\r' && Text[i + 1] == '\n')
                {
                    return i + 1;
                }

                i++;
            }

            return Text.Length;
        }

        private FlowDirection GetRootWindowFlowDirection()
            => this.GetWindow()?.FlowDirection ?? FlowDirection.LeftToRight;

        private sealed class CounterFormatConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                var text = (string)values[0];
                var maxLength = (int)values[1];
                var currentLength = text?.Length ?? 0;

                return $"{currentLength}/{maxLength}";
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
                => throw new NotImplementedException();
        }

        private static class TextInputLostFocusHandler
        {
            #region IsEnabled

            public static readonly DependencyProperty IsEnabledProperty =
                DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(TextInputLostFocusHandler),
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
                if (focusedElement is TextInputBase textInput && textInput.LostFocusBehavior == InputLostFocusBehaviorType.WhenClickOutside)
                {
                    InputFocusManager.ClearInputFocus(textInput);
                }
            }
        }

        private TextBlock? _counterTextBlock;
        private TextBlock? _placeholderTextBlock;
        private TextBlock? _contentStubTextBlock;
        private FrameworkElement? _contentHost;
        private bool _inputValueFiltered;
        private string? _lastFilteredText;
    }
}
