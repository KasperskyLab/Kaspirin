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

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class NumberInputAction : InputActionBase
    {
        public NumberInputAction(NumberInput numberInput, NumberInputActionType actionType)
        {
            Guard.ArgumentIsNotNull(numberInput);

            _numberInput = numberInput;
            ActionType = actionType;
            ActionMode = InputActionMode.OnPressedWithRepeat;

            _numberInputIsReadOnlyNotifier = new PropertyChangeNotifier<NumberInput, bool>(_numberInput, NumberInput.IsReadOnlyProperty);
            _numberInputIsReadOnlyNotifier.ValueChanged += OnNumberInputIsReadOnlyChanged;

            _numberInputValueNotifier = new PropertyChangeNotifier<NumberInput, int>(_numberInput, NumberInput.ValueInternalProperty);
            _numberInputValueNotifier.ValueChanged += OnNumberInputValueChanged;
            _numberInputMaximumNotifier = new PropertyChangeNotifier<NumberInput, int>(_numberInput, NumberInput.MaximumProperty);
            _numberInputMaximumNotifier.ValueChanged += OnNumberInputValueChanged;
            _numberInputMinimumNotifier = new PropertyChangeNotifier<NumberInput, int>(_numberInput, NumberInput.MinimumProperty);
            _numberInputMinimumNotifier.ValueChanged += OnNumberInputValueChanged;
        }

        #region ActionType

        public NumberInputActionType ActionType
        {
            get { return (NumberInputActionType)GetValue(ActionTypeProperty); }
            private set { SetValue(_actionTypePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _actionTypePropertyKey =
            DependencyProperty.RegisterReadOnly("ActionType", typeof(NumberInputActionType), typeof(NumberInputAction),
                new PropertyMetadata(default(NumberInputActionType)));

        public static readonly DependencyProperty ActionTypeProperty =
            _actionTypePropertyKey.DependencyProperty;

        #endregion

        protected override void OnPressed(bool isPressed)
        {
            if (isPressed && ActionType == NumberInputActionType.Decrease)
            {
                _numberInput.DecreaseValue();
            }

            if (isPressed && ActionType == NumberInputActionType.Increase)
            {
                _numberInput.IncreaseValue();
            }
        }

        private void OnNumberInputIsReadOnlyChanged(NumberInput input, bool oldIsReadOnly, bool newIsReadOnly)
        {
            SetActionVisibility(!newIsReadOnly);
        }

        private void OnNumberInputValueChanged(NumberInput input, int oldValue, int newValue)
        {
            if (input.ValueInternal <= input.Minimum && ActionType == NumberInputActionType.Decrease)
            {
                SetActionEnabled(false);
            }
            else if (input.ValueInternal >= input.Maximum && ActionType == NumberInputActionType.Increase)
            {
                SetActionEnabled(false);
            }
            else
            {
                SetActionEnabled(true);
            }
        }

        private readonly NumberInput _numberInput;
        private readonly PropertyChangeNotifier<NumberInput, int> _numberInputMaximumNotifier;
        private readonly PropertyChangeNotifier<NumberInput, int> _numberInputMinimumNotifier;
        private readonly PropertyChangeNotifier<NumberInput, int> _numberInputValueNotifier;
        private readonly PropertyChangeNotifier<NumberInput, bool> _numberInputIsReadOnlyNotifier;
    }
}
