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
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Kaspirin.UI.Framework.Mvvm;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public class NumberInput : TextInputBasedControl
    {
        static NumberInput()
        {
            var type = typeof(NumberInput);

            _increaseCommand = new RoutedCommand("IncreaseCommand", type);
            _decreaseCommand = new RoutedCommand("DecreaseCommand", type);

            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(_increaseCommand, OnIncreaseCommand));
            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(_decreaseCommand, OnDecreaseCommand));
        }

        public NumberInput()
        {
            Loaded += (o, e) => UpdateValue();
        }

        #region Minimum
        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(NumberInput),
                new PropertyMetadata(DefaultMinValue, OnMinimumChanged));

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NumberInput)d).UpdateMinMax();
        }
        #endregion

        #region Maximum
        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(NumberInput),
                new PropertyMetadata(DefaultMaxValue, OnMaximumChanged));

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NumberInput)d).UpdateMinMax();
        }
        #endregion

        #region Value
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumberInput),
                new FrameworkPropertyMetadata(DefaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NumberInput)d).UpdateValue();
        }
        #endregion

        #region ValueInternal
        internal int ValueInternal
        {
            get { return (int)GetValue(ValueInternalProperty); }
            set { SetValue(ValueInternalProperty, value); }
        }

        internal static readonly DependencyProperty ValueInternalProperty =
            DependencyProperty.Register("ValueInternal", typeof(int), typeof(NumberInput),
                new PropertyMetadata(DefaultValue));
        #endregion

        #region ValueChangedCommand

        public ICommand ValueChangedCommand
        {
            get { return (ICommand)GetValue(ValueChangedCommandProperty); }
            set { SetValue(ValueChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty ValueChangedCommandProperty =
            DependencyProperty.Register("ValueChangedCommand", typeof(ICommand), typeof(NumberInput));

        #endregion

        #region ChangeDelta
        public int ChangeDelta
        {
            get { return (int)GetValue(ChangeDeltaProperty); }
            set { SetValue(ChangeDeltaProperty, value); }
        }

        public static readonly DependencyProperty ChangeDeltaProperty =
            DependencyProperty.Register("ChangeDelta", typeof(int), typeof(NumberInput), new PropertyMetadata(DefaultChange));
        #endregion

        #region IsValid

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register("IsValid", typeof(bool), typeof(NumberInput), new PropertyMetadata(true));

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textInput = (TextInput)GetTemplateChild(PART_TextInput);
            _textInput.MouseWheel += TextInputOnMouseWheel;
            _textInput.InputBindings.Add(new InputBinding(_increaseCommand, new KeyGesture(Key.Up)));
            _textInput.InputBindings.Add(new InputBinding(_decreaseCommand, new KeyGesture(Key.Down)));
            _textInput.SetBinding(Controls.TextInput.TextProperty, new Binding()
            {
                Source = this,
                Path = ValueInternalProperty.AsPath(),
                UpdateSourceTrigger = UpdateSourceTrigger.Explicit
            });
            _textInput.TextChanged += (s, e) =>
            {
                InvalidateValueOnTextChange();
            };
            _textInput.LostKeyboardFocus += (s, e) =>
            {
                InvalidateValueOnLostFocus();
            };
            _textInput.SetBinding(Controls.TextInput.PlaceholderProperty, new Binding()
            {
                Source = this,
                Path = MaximumProperty.AsPath(),
                Converter = new DelegateConverter(value =>
                {
                    var maximum = (int)value!;

                    var maskItemsCount = GetMaxDigits(maximum);
                    var maskItems = Enumerable.Repeat(new TextInputMaskRegexItem(new Regex("[0-9]"), false, ' '), maskItemsCount);

                    return new TextInputMaskPlaceholder(maskItems);
                })
            });
            _textInput.ActionBar = new()
            {
                new NumberInputAction(this, NumberInputActionType.Increase),
                new NumberInputAction(this, NumberInputActionType.Decrease),
            };
        }

        internal void IncreaseValue()
        {
            SetValue(Math.Min(ValueInternal + ChangeDelta, Maximum));

            SetTextInputCaretToEnd();
        }

        internal void DecreaseValue()
        {
            SetValue(Math.Max(ValueInternal - ChangeDelta, Minimum));

            SetTextInputCaretToEnd();
        }

        private void UpdateValue()
        {
            if (!IsLoaded)
            {
                return;
            }

            SetValue(GetCoercedValue(Value));
        }

        private int GetCoercedValue(int value)
        {
            return Math.Max(Minimum, Math.Min(Maximum, value));
        }

        private void UpdateMinMax()
        {
            if (!IsLoaded)
            {
                return;
            }

            if (Minimum > Maximum)
            {
                Minimum = Maximum;
            }
            else
            {
                UpdateValue();
            }
        }

        private void SetTextInputCaretToEnd()
        {
            if (_textInput != null)
            {
                _textInput.CaretIndex = _textInput.Text?.Length ?? 0;
            }
        }

        private void TextInputOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var textInput = (TextInput)sender;
            if (textInput.IsFocused)
            {
                if (e.Delta > 0)
                {
                    IncreaseValue();
                }
                else
                {
                    DecreaseValue();
                }

                e.Handled = true;
            }
        }

        private void InvalidateValueOnTextChange()
        {
            if (_textInput == null)
            {
                return;
            }

            if (_textInput.Text != null && int.TryParse(_textInput.Text, out var textInputValue))
            {
                SetValue(textInputValue);
            }
            else
            {
                IsValid = false;
            }
        }

        private void InvalidateValueOnLostFocus()
        {
            SetValue(GetCoercedValue(ValueInternal));

            BindingOperations.GetBindingExpression(_textInput, Controls.TextInput.TextProperty)?.UpdateTarget();
        }

        private void SetValue(int value)
        {
            if (_isUpdating)
            {
                return;
            }

            _isUpdating = true;

            var isValueChanged = ValueInternal != value;

            var coercedValue = GetCoercedValue(value);
            if (coercedValue == value)
            {
                ValueInternal = value;
                IsValid = true;

                if (Value != value)
                {
                    Value = value;
                }
            }
            else
            {
                ValueInternal = value;
                IsValid = false;
            }

            if (isValueChanged)
            {
                CommandHelper.ExecuteCommand(ValueChangedCommand, Tuple.Create(Value, ValueInternal));
            }

            _isUpdating = false;
        }

        private static void OnIncreaseCommand(object sender, ExecutedRoutedEventArgs unused)
        {
            ((NumberInput)sender).IncreaseValue();
        }

        private static void OnDecreaseCommand(object sender, ExecutedRoutedEventArgs unused)
        {
            ((NumberInput)sender).DecreaseValue();
        }

        private static int GetMaxDigits(int maxValue)
        {
            return (int)Math.Log10(maxValue) + 1;
        }

        private TextInput? _textInput;
        private bool _isUpdating;

        private static readonly RoutedCommand _increaseCommand;
        private static readonly RoutedCommand _decreaseCommand;

        private const int DefaultMinValue = 0;
        private const int DefaultValue = DefaultMinValue;
        private const int DefaultMaxValue = 100;
        private const int DefaultChange = 1;
    }
}
