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
    ///     Checks that the specified argument is <see langword="null" />.
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
    ///     It is thrown if the argument is not equal to <see langword="null" />.
    /// </exception>
    public static void ArgumentIsNull<T>(
        T? argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIfNotNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentIsNull(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument is <see langword="null" />.
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
    ///     It is thrown if the argument is not equal to <see langword="null" />.
    /// </exception>
    public static void ArgumentIsNull<T>(
        T? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        if (argument is not null)
        {
            OnGuardFailed($"Argument {argumentExpression} of type \"{typeof(T)}\" must be null. {message}");
        }
    }

    /// <summary>
    ///     Checks that the specified value is <see langword="null" />.
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
    ///     It is thrown if the value is not equal to <see langword="null" />.
    /// </exception>
    public static void IsNull<T>(
        T? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIfNotNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        IsNull(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified value is <see langword="null" />.
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
    ///     It is thrown if the value is not equal to <see langword="null" />.
    /// </exception>
    public static void IsNull<T>(
        T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        if (value is not null)
        {
            OnGuardFailed($"The value {valueExpression} of type \"{typeof(T)}\" must be null. {message}");
        }
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> value is <see langword="null" />.
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
    ///     It is thrown if the value is not equal to <see langword="null" />.
    /// </exception>
    public static void IsNull<T>(
        T? value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIfNotNullInterpolatedStringHandler handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : struct
    {
        IsNull(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified <see cref="Nullable{T}" /> value is <see langword="null" />.
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
    ///     It is thrown if the value is not equal to <see langword="null" />.
    /// </exception>
    public static void IsNull<T>(
        T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
        where T : struct
    {
        if (value is not null)
        {
            OnGuardFailed($"The value {valueExpression} of type \"{typeof(T)}\" must be null. {message}");
        }
    }
}
