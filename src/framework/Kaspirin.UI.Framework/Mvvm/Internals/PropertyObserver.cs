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
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Kaspirin.UI.Framework.Mvvm.Internals
{
    /// <summary>
    ///     Provides a way to monitor changes in the properties of objects <see cref="INotifyPropertyChanged" />
    ///     and calls a custom delegate when an event occurs <see cref="INotifyPropertyChanged.PropertyChanged" />.
    /// </summary>
    internal sealed class PropertyObserver
    {
        internal static PropertyObserver[] CreatePropertyObservers<T>(Expression<Func<object?, T>> expression, Action action)
        {
            var body = Guard.EnsureArgumentIsNotNull(expression).Body;

            var memberExpressions = GetMemberExpressions(body)
                .DistinctBy(expression => expression.ToString())
                .ToArray();

            Guard.Assert(
                memberExpressions.Any(),
                "At least one property should be inside the expression body.");

            return memberExpressions
                .Select(memberExpression => new PropertyObserver(memberExpression, action))
                .ToArray();
        }

        private PropertyObserver(MemberExpression memberExpression, Action action)
        {
            _listener = CreateListener(Guard.EnsureArgumentIsNotNull(memberExpression), Guard.EnsureArgumentIsNotNull(action));
        }

        private PropertyObserverEventListener CreateListener(MemberExpression memberExpression, Action action)
        {
            var containingExpression = Guard.EnsureIsNotNull(
                memberExpression.Expression,
                $"Failed to get containing object expression for expression {memberExpression}.");

            if (containingExpression is not ConstantExpression constantExpression)
            {
                throw new InvalidOperationException(
                    $"Containing object expression for expression {memberExpression} is not {nameof(ConstantExpression)}. Nested members are not supported.");
            }

            var propertyInfo = Guard.EnsureIsInstanceOfType<PropertyInfo>(
                memberExpression.Member,
                "Observing is supported only for properties (not fields).");

            var propertyOwner = Guard.EnsureArgumentIsInstanceOfType<INotifyPropertyChanged>(
                constantExpression.Value,
                $"Observing is supported only for objects that implement {nameof(INotifyPropertyChanged)}, " +
                $"but object that owns '{propertyInfo.Name}' property doesn't.");

            return new PropertyObserverEventListener(propertyOwner, propertyInfo, action);
        }

        private static IEnumerable<MemberExpression> GetMemberExpressions(Expression expression)
        {
            switch (expression)
            {
                case MemberExpression memberExpression:
                    yield return memberExpression;
                    break;

                case UnaryExpression unaryExpression:
                    foreach (var propertyExpression in GetMemberExpressions(unaryExpression.Operand))
                    {
                        yield return propertyExpression;
                    }

                    break;

                case BinaryExpression binaryExpression:
                    foreach (var propertyExpression in GetMemberExpressions(binaryExpression.Left))
                    {
                        yield return propertyExpression;
                    }

                    foreach (var propertyExpression in GetMemberExpressions(binaryExpression.Right))
                    {
                        yield return propertyExpression;
                    }

                    break;

                case TypeBinaryExpression typeBinaryExpression:
                    foreach (var propertyExpression in GetMemberExpressions(typeBinaryExpression.Expression))
                    {
                        yield return propertyExpression;
                    }

                    break;

                case ConditionalExpression conditionalExpression:
                    foreach (var propertyExpression in GetMemberExpressions(conditionalExpression.Test))
                    {
                        yield return propertyExpression;
                    }

                    foreach (var propertyExpression in GetMemberExpressions(conditionalExpression.IfTrue))
                    {
                        yield return propertyExpression;
                    }

                    foreach (var propertyExpression in GetMemberExpressions(conditionalExpression.IfFalse))
                    {
                        yield return propertyExpression;
                    }

                    break;

                case MethodCallExpression methodCallExpression:
                    if (methodCallExpression.Object is not null)
                    {
                        foreach (var propertyExpression in GetMemberExpressions(methodCallExpression.Object))
                        {
                            yield return propertyExpression;
                        }
                    }

                    foreach (var argumentExpression in methodCallExpression.Arguments)
                    {
                        foreach (var propertyExpression in GetMemberExpressions(argumentExpression))
                        {
                            yield return propertyExpression;
                        }
                    }

                    break;

                case ConstantExpression constantExpression:
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Expression of type {expression.GetType().Name} ({nameof(expression.NodeType)} = {expression.NodeType}) is not supported.");
            }
        }

        private readonly PropertyObserverEventListener _listener;
    }
}
