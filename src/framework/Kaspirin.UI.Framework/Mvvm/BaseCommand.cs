// Copyright © 2025 AO Kaspersky Lab.
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
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Mvvm
{
    /// <summary>
    ///     The base class for implementing the command.
    /// </summary>
    /// <remarks>
    ///     This class always triggers the <see cref="CanExecuteChanged" /> event in the UI thread.
    /// </remarks>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        ///     An event that occurs when the ability to execute a command changes.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        ///     Determines whether the command can be executed with the specified parameter.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter used to determine whether the command can be executed.
        /// </param>
        /// <returns>
        ///     Returns the value <see langword="true" /> if the command can be executed, otherwise <see langword="false" />.
        /// </returns>
        public abstract bool CanExecute(object? parameter);

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="parameter">
        ///     A command parameter.
        /// </param>
        public abstract void Execute(object? parameter);

        /// <summary>
        ///     Triggers the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler == null)
            {
                return;
            }

            var executor = Executers.DispatcherExecutor;
            if (executor.IsAvailable && executor.VerifyThread() is false)
            {
                executor.ExecuteAsync(
                    action: () => handler.Invoke(this, EventArgs.Empty),
                    priority: DispatcherPriority.Normal,
                    cancellationToken: CancellationToken.None);
            }
            else
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
