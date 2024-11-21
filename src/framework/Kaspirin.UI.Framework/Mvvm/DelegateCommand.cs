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

#pragma warning disable KCAIDE0006 // Class should be abstract or sealed

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kaspirin.UI.Framework.Mvvm
{
    /// <summary>
    ///     Provides an implementation of <see cref="ICommand" /> to perform actions using delegates.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DelegateCommand" /> class, which executes the specified delegate as asynchronous.
        /// </summary>
        /// <param name="action">
        ///     The function to execute.
        /// </param>
        /// <returns>
        ///     A new instance of the <see cref="DelegateCommand" /> class.
        /// </returns>
        public static DelegateCommand CreateAsync(Action action)
            => new(() => Task.Factory.StartNew(action));

        /// <summary>
        ///     Creates a new instance of the <see cref="DelegateCommand" /> class that executes the specified delegate.
        /// </summary>
        /// <param name="action">
        ///     The function to execute.
        /// </param>
        /// <returns>
        ///     A new instance of the <see cref="DelegateCommand" /> class.
        /// </returns>
        public static DelegateCommand CreateFrom(Func<bool> action)
            => new(() => action());

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
        {
            _synchronizationContext = SynchronizationContext.Current;

            _execute = o => executeMethod();
            _canExecute = o => canExecuteMethod();
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
            _synchronizationContext = SynchronizationContext.Current;

            _execute = executeMethod;
            _canExecute = canExecuteMethod;
        }

        /// <summary>
        ///     An event that occurs when the ability to execute a command changes.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        ///     Determines whether the command can be executed.
        /// </summary>
        /// <returns>
        ///     Returns the value <see langword="true" /> if the command can be executed, otherwise <see langword="false" />.
        /// </returns>
        public bool CanExecute()
            => CanExecute(null);

        /// <summary>
        ///     Determines whether the command can be executed with the specified parameter.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter used to determine whether the command can be executed.
        /// </param>
        /// <returns>
        ///     Returns the value <see langword="true" /> if the command can be executed, otherwise <see langword="false" />.
        /// </returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute switch
            {
                null => true,
                _ => _canExecute(parameter)
            };
        }

        /// <summary>
        ///     Executes the command.
        /// </summary>
        public void Execute()
            => Execute(null);

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="parameter">
        ///     A command parameter.
        /// </param>
        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
            {
                _execute(parameter);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
                {
                    _synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
                }
                else
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private readonly SynchronizationContext? _synchronizationContext;
        private readonly Func<object?, bool> _canExecute;
        private readonly Action<object?> _execute;
    }
}

