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

using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#else
using Kaspirin.UI.Framework.BackwardCompatibility;
#endif

namespace Kaspirin.UI.Framework.Guards;

/// <summary>
///     A static class that provides various methods for checking conditions (guards).
/// </summary>
public static partial class Guard
{
    /// <summary>
    ///     Checks that the specified argument is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the argument is <see langword="null" />.
    /// </exception>
    public static void ArgumentIsNotNull<T>(
        [NotNull] T? argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIsNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentIsNotNull(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the argument is <see langword="null" />.
    /// </exception>
    public static void ArgumentIsNotNull<T>(
        [NotNull] T? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        if (argument is null)
        {
            OnGuardFailed($"Argument {argumentExpression} of type \"{typeof(T)}\" must not be null. {message}");
        }
    }

    /// <summary>
    ///     Checks that the specified argument is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the argument is <see langword="null" />.
    /// </exception>
    /// <returns>
    ///     The original value of the argument.
    /// </returns>
    public static T EnsureArgumentIsNotNull<T>(
        [NotNull] T? argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIsNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
        where T : class
    {
        return EnsureArgumentIsNotNull(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the argument is <see langword="null" />.
    /// </exception>
    /// <returns>
    ///     The original value of the argument.
    /// </returns>
    public static T EnsureArgumentIsNotNull<T>(
        [NotNull] T? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
        where T : class
    {
        ArgumentIsNotNull(argument, message, argumentExpression);
        return argument;
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> argument is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the argument is <see langword="null" />.
    /// </exception>
    /// <returns>
    ///     The original value of the argument.
    /// </returns>
    public static T EnsureArgumentIsNotNull<T>(
        [NotNull] T? argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIsNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
        where T : struct
    {
        return EnsureArgumentIsNotNull(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> argument is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the argument is <see langword="null" />.
    /// </exception>
    /// <returns>
    ///     The original value of the argument.
    /// </returns>
    public static T EnsureArgumentIsNotNull<T>(
        [NotNull] T? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
        where T : struct
    {
        ArgumentIsNotNull(argument, message, argumentExpression);
        return argument.Value;
    }

    /// <summary>
    ///     Checks that the specified <typeparamref name="T" />? the value is not null and returns it.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    /// <returns>
    ///     The value of the <paramref name="value" /> argument.
    /// </returns>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static T EnsureIsNotNull<T>(
        [NotNull] T? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIsNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : class
    {
        return EnsureIsNotNull(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified <typeparamref name="T" />? the value is not null and returns it.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    /// <returns>
    ///     The value of the <paramref name="value" /> argument.
    /// </returns>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static T EnsureIsNotNull<T>(
        [NotNull] T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : class
    {
        IsNotNull(value, message, valueExpression);
        return value;
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> value is not null and returns it.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    /// <returns>
    ///     The value of the <paramref name="value" /> argument.
    /// </returns>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static T EnsureIsNotNull<T>(
        [NotNull] T? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIsNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : struct
    {
        return EnsureIsNotNull(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> value is not null and returns it.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    /// <returns>
    ///     The value of the <paramref name="value" /> argument.
    /// </returns>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static T EnsureIsNotNull<T>(
        [NotNull] T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : struct
    {
        IsNotNull(value, message, valueExpression);
        return value.Value;
    }

    /// <summary>
    ///     Checks that the specified value is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static void IsNotNull<T>(
        [NotNull] T? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIsNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        IsNotNull(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified value is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static void IsNotNull<T>(
        [NotNull] T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        if (value is null)
        {
            OnGuardFailed($"The value {valueExpression} of type \"{typeof(T)}\" must not be null. {message}");
        }
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> value is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static void IsNotNull<T>(
        [NotNull] T? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIsNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : struct
    {
        IsNotNull(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> value is not equal to <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of value.
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
    ///     It is thrown if the value is <see langword="null" />.
    /// </exception>
    public static void IsNotNull<T>(
        [NotNull] T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : struct
    {
        if (value is null)
        {
            OnGuardFailed($"The value {valueExpression} of type \"{typeof(T)}\" must not be null. {message}");
        }
    }
}
