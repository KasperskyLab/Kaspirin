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

namespace Kaspirin.UI.Framework.UiKit.Controls.VisualStates;

public static class StateService
{
    #region State

    public static State GetState(DependencyObject obj)
        => (State)obj.GetValue(StateProperty);

    public static void SetState(DependencyObject obj, State value)
        => obj.SetValue(StateProperty, value);

    public static readonly DependencyProperty StateProperty = DependencyProperty.RegisterAttached(
        "State",
        typeof(State),
        typeof(StateService),
        new PropertyMetadata(default(State)));

    #endregion

    #region SelectableState

    public static SelectableState GetSelectableState(DependencyObject obj)
        => (SelectableState)obj.GetValue(SelectableStateProperty);

    public static void SetSelectableState(DependencyObject obj, SelectableState value)
        => obj.SetValue(SelectableStateProperty, value);

    public static readonly DependencyProperty SelectableStateProperty = DependencyProperty.RegisterAttached(
        "SelectableState",
        typeof(SelectableState),
        typeof(StateService),
        new PropertyMetadata(default(SelectableState)));

    #endregion

    #region TextInputState

    public static TextInputState GetTextInputState(DependencyObject obj)
        => (TextInputState)obj.GetValue(TextInputStateProperty);

    public static void SetTextInputState(DependencyObject obj, TextInputState value)
        => obj.SetValue(TextInputStateProperty, value);

    public static readonly DependencyProperty TextInputStateProperty = DependencyProperty.RegisterAttached(
        "TextInputState",
        typeof(TextInputState),
        typeof(StateService),
        new PropertyMetadata(default(TextInputState)));

    #endregion
}
