// Copyright © 2026 AO Kaspersky Lab.
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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Provides an implementation of <see cref="ICommand" /> for commands that perform navigation.
/// </summary>
/// <typeparam name="T">
///     The type of parameter passed to the delegates.
/// </typeparam>
public class NavigateCommand<T> : NavigateCommand
{
    /// <inheritdoc cref="NavigateCommand(Action,  Func{bool})"/>
    public NavigateCommand(Action<T?> executeMethod, Func<bool> isExternalMethod)
        : this(executeMethod, o => true, isExternalMethod)
    {
    }

    /// <inheritdoc cref="NavigateCommand(Action, Func{bool}, Func{bool})"/>
    public NavigateCommand(Action<T?> executeMethod, Func<T?, bool> canExecuteMethod, Func<bool> isExternalMethod)
        : base(
            o => executeMethod(ConvertParameter<T>(o)),
            o => canExecuteMethod(ConvertParameter<T>(o)),
            isExternalMethod)
    {
    }

    /// <inheritdoc cref="DelegateCommand.CanExecute()"/>
    public bool CanExecute(T parameter)
        => CanExecute((object?)parameter);

    /// <inheritdoc cref="DelegateCommand.Execute()"/>
    public void Execute(T parameter)
        => Execute((object?)parameter);
}
