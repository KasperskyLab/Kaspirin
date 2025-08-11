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

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class SplitButton : ContextMenuButton
{
    #region State

    public SplitButtonState State
    {
        get => (SplitButtonState)GetValue(StateProperty);
        private set => SetValue(_statePropertyKey, value);
    }

    private static readonly DependencyPropertyKey _statePropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(State),
        typeof(SplitButtonState),
        typeof(SplitButton),
        new PropertyMetadata(SplitButtonState.Enabled));

    public static readonly DependencyProperty StateProperty = _statePropertyKey.DependencyProperty;

    #endregion

    #region IconLocation

    public ButtonIconLocation IconLocation
    {
        get => (ButtonIconLocation)GetValue(IconLocationProperty);
        set => SetValue(IconLocationProperty, value);
    }

    public static readonly DependencyProperty IconLocationProperty = DependencyProperty.Register(
        nameof(IconLocation),
        typeof(ButtonIconLocation),
        typeof(SplitButton),
        new PropertyMetadata(default(ButtonIconLocation)));

    #endregion

    #region IsContextMenuButtonBusy

    public bool IsContextMenuButtonBusy
    {
        get => (bool)GetValue(IsContextMenuButtonBusyProperty);
        set => SetValue(IsContextMenuButtonBusyProperty, value);
    }

    public static readonly DependencyProperty IsContextMenuButtonBusyProperty = DependencyProperty.Register(
        nameof(IsContextMenuButtonBusy),
        typeof(bool),
        typeof(SplitButton),
        new PropertyMetadata(default(bool)));

    #endregion

    #region IsContextMenuButtonEnabled

    public bool IsContextMenuButtonEnabled
    {
        get => (bool)GetValue(IsContextMenuButtonEnabledProperty);
        set => SetValue(IsContextMenuButtonEnabledProperty, value);
    }

    public static readonly DependencyProperty IsContextMenuButtonEnabledProperty = DependencyProperty.Register(
        nameof(IsContextMenuButtonEnabled),
        typeof(bool),
        typeof(SplitButton),
        new PropertyMetadata(true));

    #endregion

    #region IsMainButtonBusy

    public bool IsMainButtonBusy
    {
        get => (bool)GetValue(IsMainButtonBusyProperty);
        set => SetValue(IsMainButtonBusyProperty, value);
    }

    public static readonly DependencyProperty IsMainButtonBusyProperty = DependencyProperty.Register(
        nameof(IsMainButtonBusy),
        typeof(bool),
        typeof(SplitButton),
        new PropertyMetadata(default(bool)));

    #endregion

    #region IsMainButtonEnabled

    public bool IsMainButtonEnabled
    {
        get => (bool)GetValue(IsMainButtonEnabledProperty);
        set => SetValue(IsMainButtonEnabledProperty, value);
    }

    public static readonly DependencyProperty IsMainButtonEnabledProperty = DependencyProperty.Register(
        nameof(IsMainButtonEnabled),
        typeof(bool),
        typeof(SplitButton),
        new PropertyMetadata(true));

    #endregion

    #region Command

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(SplitButton),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region CommandParameter

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        nameof(CommandParameter),
        typeof(object),
        typeof(SplitButton),
        new PropertyMetadata(default(object)));

    #endregion

    internal void SetState(SplitButtonState state)
    {
        State = state;
    }
}
