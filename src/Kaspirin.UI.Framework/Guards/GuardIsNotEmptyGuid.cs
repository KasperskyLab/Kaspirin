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
    ///     Checks that the specified argument <paramref name="argument" /> is not empty <see cref="Guid" />.
    /// </summary>
    /// <param name="argument">
    ///     The <see cref="Guid" /> argument for verification.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition validation error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     Called when the <paramref name="argument" /> argument is empty <see cref="Guid" />.
    /// </exception>
    public static void ArgumentIsNotEmptyGuid(
        Guid argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIfEmptyGuidInterpolatedStringHandler handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentIsNotEmptyGuid(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument <paramref name="argument" /> is not empty <see cref="Guid" />.
    /// </summary>
    /// <param name="argument">
    ///     The <see cref="Guid" /> argument for verification.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     Called when the <paramref name="argument" /> argument is empty <see cref="Guid" />.
    /// </exception>
    public static void ArgumentIsNotEmptyGuid(
        Guid argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        if (argument == Guid.Empty)
        {
            OnGuardFailed($"Guid argument {argumentExpression} with value \"{argument}\" must not be empty. {message}");
        }
    }

    /// <summary>
    ///     Checks that the specified value <see cref="Guid" /> is not empty.
    /// </summary>
    /// <param name="value">
    ///     The value of <see cref="Guid" /> for verification.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition validation error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the value <see cref="Guid" /> is empty.
    /// </exception>
    public static void IsNotEmptyGuid(
        Guid value,
        [InterpolatedStringHandlerArgument("value")] ref AssertIfEmptyGuidInterpolatedStringHandler handler,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        IsNotEmptyGuid(value, handler.ToStringAndClear(), valueExpression);
    }

    /// <summary>
    ///     Checks that the specified value <see cref="Guid" /> is not empty.
    /// </summary>
    /// <param name="value">
    ///     The value of <see cref="Guid" /> for verification.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the value <see cref="Guid" /> is empty.
    /// </exception>
    public static void IsNotEmptyGuid(
        Guid value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        if (value == Guid.Empty)
        {
            OnGuardFailed($"Guid {valueExpression} with value \"{value}\" must not be empty. {message}");
        }
    }
}
