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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public class SplitButton : ContextMenuButton
    {
        #region State

        public SplitButtonState State
        {
            get { return (SplitButtonState)GetValue(StateProperty); }
            private set { SetValue(_statePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _statePropertyKey =
            DependencyProperty.RegisterReadOnly("State", typeof(SplitButtonState), typeof(SplitButton),
                new PropertyMetadata(SplitButtonState.Enabled));

        public static readonly DependencyProperty StateProperty =
            _statePropertyKey.DependencyProperty;

        #endregion

        #region IconLocation

        public ButtonIconLocation IconLocation
        {
            get { return (ButtonIconLocation)GetValue(IconLocationProperty); }
            set { SetValue(IconLocationProperty, value); }
        }

        public static readonly DependencyProperty IconLocationProperty = DependencyProperty.Register(
            "IconLocation", typeof(ButtonIconLocation), typeof(SplitButton));

        #endregion

        #region IsContextMenuButtonEnabled

        public bool IsContextMenuButtonEnabled
        {
            get { return (bool)GetValue(IsContextMenuButtonEnabledProperty); }
            set { SetValue(IsContextMenuButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsContextMenuButtonEnabledProperty = DependencyProperty.Register(
            "IsContextMenuButtonEnabled", typeof(bool), typeof(SplitButton), new PropertyMetadata(true));

        #endregion

        #region IsMainButtonEnabled

        public bool IsMainButtonEnabled
        {
            get { return (bool)GetValue(IsMainButtonEnabledProperty); }
            set { SetValue(IsMainButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsMainButtonEnabledProperty = DependencyProperty.Register(
            "IsMainButtonEnabled", typeof(bool), typeof(SplitButton), new PropertyMetadata(true));

        #endregion

        #region Command

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(SplitButton));

        #endregion

        #region CommandParameter

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(SplitButton));

        #endregion

        internal void SetState(SplitButtonState state)
        {
            State = state;
        }
    }
}
