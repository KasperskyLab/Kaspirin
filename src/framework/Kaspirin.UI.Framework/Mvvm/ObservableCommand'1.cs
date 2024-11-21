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
using System.Linq.Expressions;
using System.Windows.Input;

namespace Kaspirin.UI.Framework.Mvvm
{
    /// <summary>
    ///     Provides an implementation of <see cref="ICommand" /> for performing actions using delegates,
    ///     which automatically monitors all properties used in the expression for <see cref="CanExecute" />.
    ///     This class automatically calls <see cref="ObservableCommand.RaiseCanExecuteChanged" /> every
    ///     time these properties are changed.
    /// </summary>
    /// <remarks>
    ///     For details and examples, see <see cref="ObservableCommand" />.
    /// </remarks>
    /// <typeparam name="T">
    ///     The type of parameter passed to the delegates.
    /// </typeparam>
    public sealed class ObservableCommand<T> : ObservableCommand
    {
        /// <inheritdoc cref="ObservableCommand(Action, Expression{Func{bool}})"/>
        public ObservableCommand(Action<T?> executeAction, Expression<Func<T?, bool>> canExecuteExpression)
            : base(
                o => Guard.EnsureArgumentIsNotNull(executeAction)((T?)o),
                Expression.Lambda<Func<object?, bool>>(
                    Guard.EnsureArgumentIsNotNull(canExecuteExpression).Body,
                    Expression.Parameter(typeof(object), "o")))
        {
        }

        /// <inheritdoc cref="ObservableCommand.CanExecute()"/>
        public bool CanExecute(T parameter)
            => CanExecute((object?)parameter);

        /// <inheritdoc cref="ObservableCommand.Execute()"/>
        public void Execute(T parameter)
            => Execute((object?)parameter);
    }
}
