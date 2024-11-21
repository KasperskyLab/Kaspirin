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

namespace Kaspirin.UI.Framework.Mvvm
{
    /// <summary>
    ///     Provides an implementation of <see cref="ICommand" /> to perform actions using delegates.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parameter passed to the delegates.
    /// </typeparam>
    public sealed class DelegateCommand<T> : DelegateCommand
    {
        /// <inheritdoc cref="DelegateCommand(Action)"/>
        public DelegateCommand(Action<T?> executeMethod)
            : this(executeMethod, o => true)
        {
        }

        /// <inheritdoc cref="DelegateCommand(Action, Func{bool})"/>
        public DelegateCommand(Action<T?> executeMethod, Func<T?, bool> canExecuteMethod)
            : base(o => executeMethod((T?)o), o => canExecuteMethod((T?)o))
        {
        }

        /// <inheritdoc cref="DelegateCommand.CanExecute()"/>
        public bool CanExecute(T parameter)
            => CanExecute((object?)parameter);

        /// <inheritdoc cref="DelegateCommand.Execute()"/>
        public void Execute(T parameter)
            => Execute((object?)parameter);
    }
}
