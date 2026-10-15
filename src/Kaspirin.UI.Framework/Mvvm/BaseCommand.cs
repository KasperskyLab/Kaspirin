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
        ///     Initializes a new instance of the <see cref="BaseCommand" /> class.
        /// </summary>
        protected BaseCommand()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseCommand" /> class with the specified delegates
        ///     for executing and determining whether the command can be executed.
        /// </summary>
        /// <param name="execute">
        ///     The delegate executed by the command.
        /// </param>
        /// <param name="canExecute">
        ///     A delegate that determines whether the command can be executed.
        /// </param>
        protected BaseCommand(Action<object?> execute, Func<object?, bool> canExecute)
        {
            _execute = Guard.EnsureArgumentIsNotNull(execute);
            _canExecute = Guard.EnsureArgumentIsNotNull(canExecute);
        }

        /// <summary>
        ///     An event that occurs when the ability to execute a command changes.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        ///     An event that occurs before executing a command.
        /// </summary>
        public event CommandEventHandler? Executing;

        /// <summary>
        ///     An event that occurs after executing a command.
        /// </summary>
        public event CommandEventHandler? Executed;

        /// <summary>
        ///     Adds a global handler that is called before executing any command <see cref="BaseCommand" />.
        /// </summary>
        /// <param name="handler">
        ///     The handler being added.
        /// </param>
        public static void AddExecutingHandler(CommandEventHandler handler)
        {
            Guard.EnsureArgumentIsNotNull(handler);

            lock (_staticHandlersLock)
            {
                _staticExecuting += handler;
            }
        }

        /// <summary>
        ///     Adds a global handler that is called after executing any command <see cref="BaseCommand" />.
        /// </summary>
        /// <param name="handler">
        ///     The handler being added.
        /// </param>
        public static void AddExecutedHandler(CommandEventHandler handler)
        {
            Guard.EnsureArgumentIsNotNull(handler);

            lock (_staticHandlersLock)
            {
                _staticExecuted += handler;
            }
        }

        /// <summary>
        ///     Deletes the global handler previously added via <see cref="AddExecutingHandler" />.
        /// </summary>
        /// <param name="handler">
        ///     The handler being deleted.
        /// </param>
        public static void RemoveExecutingHandler(CommandEventHandler handler)
        {
            Guard.EnsureArgumentIsNotNull(handler);

            lock (_staticHandlersLock)
            {
                _staticExecuting -= handler;
            }
        }

        /// <summary>
        ///     Deletes the global handler previously added via <see cref="AddExecutedHandler" />.
        /// </summary>
        /// <param name="handler">
        ///     The handler being deleted.
        /// </param>
        public static void RemoveExecutedHandler(CommandEventHandler handler)
        {
            Guard.EnsureArgumentIsNotNull(handler);

            lock (_staticHandlersLock)
            {
                _staticExecuted -= handler;
            }
        }

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
            => CanExecuteCore(parameter);

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="parameter">
        ///     A command parameter.
        /// </param>
        public void Execute(object? parameter)
        {
            var args = new CommandEventArgs(parameter);

            OnExecuting(this, args);

            if (!args.Handled)
            {
                Executing?.Invoke(this, args);
            }

            if (!args.Handled)
            {
                ExecuteCore(parameter);
            }

            if (!args.Handled)
            {
                Executed?.Invoke(this, args);
            }

            OnExecuted(this, args);
        }

        /// <summary>
        ///     Contains the logic for determining whether a command can be executed.
        /// </summary>
        /// <remarks>
        ///     The default implementation uses the delegate passed to the constructor. If no delegate is specified,
        ///     the command is considered available for execution.
        /// </remarks>
        /// <param name="parameter">
        ///     The parameter used to determine whether the command can be executed.
        /// </param>
        /// <returns>
        ///     Returns the value <see langword="true" /> if the command can be executed, otherwise <see langword="false" />.
        /// </returns>
        protected virtual bool CanExecuteCore(object? parameter)
            => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        ///     Contains the logic of command execution.
        /// </summary>
        /// <remarks>
        ///     The default implementation executes the delegate passed to the constructor if the command is
        ///     available for execution (see <see cref="CanExecute" />).
        /// </remarks>
        /// <param name="parameter">
        ///     A command parameter.
        /// </param>
        protected virtual void ExecuteCore(object? parameter)
        {
            if (CanExecute(parameter))
            {
                _execute?.Invoke(parameter);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
            => RaiseEventInUiThread(CanExecuteChanged);

        /// <summary>
        ///     Calls the specified event handler in the UI thread.
        /// </summary>
        /// <param name="handler">
        ///     The event handler to be called.
        /// </param>
        protected void RaiseEventInUiThread(EventHandler? handler)
        {
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

        /// <summary>
        ///     Sets the command parameter to the type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The target type of the parameter.
        /// </typeparam>
        /// <param name="parameter">
        ///     A command parameter.
        /// </param>
        /// <returns>
        ///     A parameter of the type <typeparamref name="T" />. For <see langword="null" /> and the type <typeparamref name="T" />
        ///     that allows <see langword="null" />, <see langword="null" /> is returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     It is thrown if <paramref name="parameter" /> is equal to <see langword="null" />, and the
        ///     type <typeparamref name="T" /> is a significant type that does not allow <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     It is thrown if <paramref name="parameter" /> cannot be converted to the type <typeparamref name="T" />.
        /// </exception>
        protected static T ConvertParameter<T>(object? parameter)
        {
            if (parameter is T value)
            {
                return value;
            }

            if (parameter is null)
            {
                if (default(T) is null)
                {
                    return default!;
                }

                throw new ArgumentNullException(
                    nameof(parameter),
                    $"A parameter of type '{typeof(T)}' is required, but null was provided.");
            }

            throw new ArgumentException(
                $"A parameter of type '{typeof(T)}' is expected, but '{parameter.GetType()}' was provided.",
                nameof(parameter));
        }

        private static void OnExecuting(object sender, CommandEventArgs args)
        {
            _staticExecuting?.Invoke(sender, args);
        }

        private static void OnExecuted(object sender, CommandEventArgs args)
        {
            _staticExecuted?.Invoke(sender, args);
        }

        private static readonly object _staticHandlersLock = new();
        private static CommandEventHandler? _staticExecuting;
        private static CommandEventHandler? _staticExecuted;

        private readonly Action<object?>? _execute;
        private readonly Func<object?, bool>? _canExecute;
    }
}
