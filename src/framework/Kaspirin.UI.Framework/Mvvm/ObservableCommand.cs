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
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;
using Kaspirin.UI.Framework.Mvvm.Internals;

namespace Kaspirin.UI.Framework.Mvvm
{
    /// <summary>
    ///     Provides an implementation of <see cref="ICommand" /> that automatically monitors all properties
    ///     used in the expression for checking <see cref="CanExecute()" />. This class automatically calls <see cref="RaiseCanExecuteChanged" />
    ///     every time these properties are changed.
    /// </summary>
    /// <remarks>
    ///     Only the properties <b> defined explicitly in the expression </b> will be automatically
    ///     tracked. Currently, nested properties are not supported. The following operators are supported in the expression:
    ///     <para /> • Unary (negation)
    ///     <para /> • Binary (arithmetic, logical, bitwise, comparative, equality, comparison, union)
    ///     <para /> • Ternary
    ///     <para /> • Type operations (is, as, cast)
    ///     <para /> • Method calls
    ///     <para /> Examples:
    ///     <code>
    ///     new ObservableCommand(() => DoSomeWork(), () => PropToObserve)
    ///     new ObservableCommand(() => DoSomeWork(), () => !PropToObserve)
    ///     new ObservableCommand(() => DoSomeWork(), () => PropToObserve1 > PropToObserve2 / 100)
    ///     new ObservableCommand(() => DoSomeWork(), () => PropToObserve1 != SomeMethod(PropToObserve2))
    ///     new ObservableCommand(() => DoSomeWork(), () => PropToObserve1 || !PropToObserve2)
    ///     new ObservableCommand(() => DoSomeWork(), () => (PropToObserve1 ?? PropToObserve2) == "test")
    ///     new ObservableCommand(() => DoSomeWork(), () => ConditionProp ? PropToObserve1 : PropToObserve2) == "test")
    ///     </code>
    /// </remarks>
    public class ObservableCommand : ICommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableCommand" /> class with the specified
        ///     delegate to execute and an expression to determine whether the command can be executed.
        /// </summary>
        /// <param name="executeAction">
        ///     The delegate being executed.
        /// </param>
        /// <param name="canExecuteExpression">
        ///     An expression to determine whether a command can be executed.
        /// </param>
        public ObservableCommand(Action executeAction, Expression<Func<bool>> canExecuteExpression)
        {
            Guard.ArgumentIsNotNull(executeAction);
            Guard.ArgumentIsNotNull(canExecuteExpression);

            var canExecuteWrapperExpression = Expression.Lambda<Func<object?, bool>>(
                canExecuteExpression.Body,
                Expression.Parameter(typeof(object), "o"));

            _synchronizationContext = SynchronizationContext.Current;
            _execute = o => executeAction();
            _canExecute = canExecuteWrapperExpression.Compile();

            ObserveProperties(canExecuteWrapperExpression);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableCommand" /> class with the specified
        ///     delegate to execute and an expression to determine whether the command can be executed.
        /// </summary>
        /// <param name="executeAction">
        ///     The delegate being executed.
        /// </param>
        /// <param name="canExecuteExpression">
        ///     An expression to determine whether a command can be executed.
        /// </param>
        protected ObservableCommand(Action<object?> executeAction, Expression<Func<object?, bool>> canExecuteExpression)
        {
            Guard.ArgumentIsNotNull(executeAction);
            Guard.ArgumentIsNotNull(canExecuteExpression);

            _synchronizationContext = SynchronizationContext.Current;
            _execute = executeAction;
            _canExecute = canExecuteExpression.Compile();

            ObserveProperties(canExecuteExpression);
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

        /// <summary>
        ///     Starts monitoring properties in the expression <paramref name="canExecuteExpression" />.
        /// </summary>
        /// <param name="canExecuteExpression">
        ///     An expression that needs to be observed.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     It is thrown out if the expression is already observed.
        /// </exception>
        protected void ObserveProperties(Expression<Func<object?, bool>> canExecuteExpression)
        {
            Guard.Argument(
                _observedExpressions.Add(canExecuteExpression.ToString()),
                $"{canExecuteExpression} expression is already being observed.");

            _propertyObservers.AddRange(PropertyObserver.CreatePropertyObservers(canExecuteExpression, RaiseCanExecuteChanged));
        }

        private readonly HashSet<string> _observedExpressions = new();
        private readonly List<PropertyObserver> _propertyObservers = new();
        private readonly SynchronizationContext? _synchronizationContext;
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;
    }
}
