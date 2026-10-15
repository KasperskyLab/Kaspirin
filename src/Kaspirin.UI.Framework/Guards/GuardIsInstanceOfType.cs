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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

#if !NET6_0_OR_GREATER
using Kaspirin.UI.Framework.BackwardCompatibility;
#endif

namespace Kaspirin.UI.Framework.Guards;

/// <summary>
///     A static class that provides various methods for checking conditions (guards).
/// </summary>
public static partial class Guard
{
    /// <summary>
    ///     Checks that the specified argument is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the argument.
    /// </typeparam>
    /// <param name="argument">
    ///     An argument for verification.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition validation error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the argument is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    public static void ArgumentIsInstanceOfType<T>(
        [NotNull] object? argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIfNotInstanceOfTypeInterpolatedStringHandler<T> handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentIsInstanceOfType<T>(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the argument.
    /// </typeparam>
    /// <param name="argument">
    ///     An argument for verification.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the argument is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    /// <returns>
    ///     An argument cast to the specified type.
    /// </returns>
    public static void ArgumentIsInstanceOfType<T>(
        [NotNull] object? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        if (argument is not T)
        {
            var typeName = argument == null ? "(null)" : argument.GetType().FullName;
            OnGuardFailed
                ($"Argument {argumentExpression} of type \"{typeName}\" should be an instance of type \"{typeof(T)}\". {message}");
        }
    }

    /// <summary>
    ///     Checks that the specified argument is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the argument.
    /// </typeparam>
    /// <param name="argument">
    ///     An argument for verification.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition validation error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the argument is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    /// <returns>
    ///     An argument cast to the specified type.
    /// </returns>
    public static T EnsureArgumentIsInstanceOfType<T>(
        [NotNull] object? argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIfNotInstanceOfTypeInterpolatedStringHandler<T> handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        return EnsureArgumentIsInstanceOfType<T>(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the argument.
    /// </typeparam>
    /// <param name="argument">
    ///     An argument for verification.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the argument is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    /// <returns>
    ///     An argument cast to the specified type.
    /// </returns>
    public static T EnsureArgumentIsInstanceOfType<T>(
        [NotNull] object? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentIsInstanceOfType<T>(argument, message, argumentExpression);
        return (T)argument;
    }

    /// <summary>
    ///     Checks that the specified value is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type to check.
    /// </typeparam>
    /// <param name="value">
    ///     The value to check.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition validation error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    /// <returns>
    ///     The value converted to the specified type.
    /// </returns>
    public static T EnsureIsInstanceOfType<T>(
        [NotNull] object? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIfNotInstanceOfTypeInterpolatedStringHandler<T> handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        return EnsureIsInstanceOfType<T>(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified value is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type to check.
    /// </typeparam>
    /// <param name="value">
    ///     The value to check.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    /// <returns>
    ///     The value converted to the specified type.
    /// </returns>
    public static T EnsureIsInstanceOfType<T>(
        [NotNull] object? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        IsInstanceOfType<T>(value, message, valueExpression);
        return (T)value;
    }

    /// <summary>
    ///     Checks that the specified value is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type to check.
    /// </typeparam>
    /// <param name="value">
    ///     The value to check.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition validation error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    public static void IsInstanceOfType<T>(
        [NotNull] object? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIfNotInstanceOfTypeInterpolatedStringHandler<T> handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        IsInstanceOfType<T>(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified value is an instance of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type to check.
    /// </typeparam>
    /// <param name="value">
    ///     The value to check.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is not an instance of the type <typeparamref name="T" />.
    /// </exception>
    public static void IsInstanceOfType<T>(
        [NotNull] object? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        if (value is not T)
        {
            var typeName = value == null ? "(null)" : value.GetType().FullName;
            OnGuardFailed(
                $"Object {valueExpression} of type \"{typeName}\" should be an instance of type \"{typeof(T)}\". {message}");
        }
    }
}
