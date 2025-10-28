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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Provides an implementation of <see cref="ICommand" /> to perform actions using delegates.
/// </summary>
public class DelegateCommand : BaseCommand, ICommand
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DelegateCommand" /> class with the specified delegate to execute.
    /// </summary>
    /// <param name="executeMethod">
    ///     The delegate being executed.
    /// </param>
    public DelegateCommand(Action executeMethod)
        : this(executeMethod, () => true)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DelegateCommand" /> class with the specified delegate
    ///     to execute and a delegate to determine whether the command can be executed.
    /// </summary>
    /// <param name="executeMethod">
    ///     The delegate being executed.
    /// </param>
    /// <param name="canExecuteMethod">
    ///     A delegate to determine whether a command can be executed.
    /// </param>
    public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        : this(parameter => executeMethod(), parameter => canExecuteMethod())
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DelegateCommand" /> class with the specified delegate
    ///     to execute and a delegate to determine whether the command can be executed.
    /// </summary>
    /// <param name="executeMethod">
    ///     The delegate being executed.
    /// </param>
    /// <param name="canExecuteMethod">
    ///     A delegate to determine whether a command can be executed.
    /// </param>
    protected DelegateCommand(Action<object?> executeMethod, Func<object?, bool> canExecuteMethod)
    {
        _execute = Guard.EnsureArgumentIsNotNull(executeMethod);
        _canExecute = Guard.EnsureArgumentIsNotNull(canExecuteMethod);
    }

    /// <summary>
    ///     Determines whether the command can be executed.
    /// </summary>
    /// <returns>
    ///     Returns the value <see langword="true" /> if the command can be executed, otherwise <see langword="false" />.
    /// </returns>
    public bool CanExecute()
        => CanExecute(null);

    /// <summary>
    ///     Executes the command.
    /// </summary>
    public void Execute()
        => Execute(null);

    /// <inheritdoc/>
    public override bool CanExecute(object? parameter)
        => _canExecute.Invoke(parameter);

    /// <inheritdoc/>
    public override void Execute(object? parameter)
    {
        if (CanExecute(parameter))
        {
            _execute.Invoke(parameter);
        }
    }

    private readonly Func<object?, bool> _canExecute;
    private readonly Action<object?> _execute;
}

