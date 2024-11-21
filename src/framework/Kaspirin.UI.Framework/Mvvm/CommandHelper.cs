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

namespace Kaspirin.UI.Framework.Mvvm
{
    /// <summary>
    ///     Provides auxiliary methods for working with commands.
    /// </summary>
    public static class CommandHelper
    {
        /// <summary>
        ///     Executes the command associated with the specified source.
        /// </summary>
        /// <param name="commandSource">
        ///     The source of the command.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the command was executed successfully, otherwise <see langword="false" />.
        /// </returns>
        public static bool ExecuteCommandSource(ICommandSource commandSource)
        {
            Guard.ArgumentIsNotNull(commandSource);

            return ExecuteCommand(
                commandSource.Command,
                commandSource.CommandParameter,
                commandSource.CommandTarget ?? commandSource as IInputElement);
        }

        /// <summary>
        ///     Executes the specified command.
        /// </summary>
        /// <param name="command">
        ///     The command to execute.
        /// </param>
        /// <param name="parameter">
        ///     The parameter passed to the command.
        /// </param>
        /// <param name="target">
        ///     The purpose of the command (for <see cref="RoutedCommand" />).
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the command was executed successfully, otherwise <see langword="false" />.
        /// </returns>
        public static bool ExecuteCommand(ICommand? command, object? parameter = null, IInputElement? target = null)
        {
            if (command is null)
            {
                return false;
            }

            if (command is RoutedCommand routedCommand)
            {
                if (routedCommand.CanExecute(parameter, target))
                {
                    routedCommand.Execute(parameter, target);
                    return true;
                }
            }
            else if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines whether the specified command can be executed.
        /// </summary>
        /// <param name="command">
        ///     The command to check.
        /// </param>
        /// <param name="parameter">
        ///     The parameter passed to the command.
        /// </param>
        /// <param name="target">
        ///     The purpose of the command (for <see cref="RoutedCommand" />).
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the command can be executed, otherwise <see langword="false" />.
        /// </returns>
        public static bool CanExecuteCommand(ICommand? command, object? parameter = null, IInputElement? target = null)
        {
            if (command is null)
            {
                return false;
            }

            return command is RoutedCommand routedCommand
                ? routedCommand.CanExecute(parameter, target)
                : command.CanExecute(parameter);
        }
    }
}
