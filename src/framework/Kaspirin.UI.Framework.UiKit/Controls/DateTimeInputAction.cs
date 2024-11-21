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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System.Windows;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class DateTimeInputAction : InputActionBase
    {
        public DateTimeInputAction(DateTimeInput dateInput)
        {
            Guard.ArgumentIsNotNull(dateInput);

            ActionMode = InputActionMode.OnClick;

            _dateTimeInput = dateInput;

            _dateTimeInputIsReadOnlyNotifier = new PropertyChangeNotifier<DateTimeInput, bool>(_dateTimeInput, DateTimeInput.IsReadOnlyProperty);
            _dateTimeInputIsReadOnlyNotifier.ValueChanged += OnDateTimeInputIsReadOnlyChanged;

            SetBinding(SelectionModeProperty, new Binding() { Source = _dateTimeInput, Path = DateTimeInput.SelectionModeProperty.AsPath() });
        }

        #region SelectionMode

        public DateTimeInputSelectionMode SelectionMode
        {
            get { return (DateTimeInputSelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(DateTimeInputSelectionMode), typeof(DateTimeInputAction));

        #endregion

        private void OnDateTimeInputIsReadOnlyChanged(DateTimeInput input, bool oldIsReadOnly, bool newIsReadOnly)
        {
            SetActionVisibility(!newIsReadOnly);
        }

        protected override void OnClick()
        {
            _dateTimeInput.IsDropDownOpen = true;
        }

        private readonly DateTimeInput _dateTimeInput;
        private readonly PropertyChangeNotifier<DateTimeInput, bool> _dateTimeInputIsReadOnlyNotifier;
    }
}
