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
    ///     Checks that the specified argument is not <see langword="null" />, is not empty, and does not consist only of spaces.
    /// </summary>
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
    ///     It is thrown if the argument is <see langword="null" />, empty, or consists only of spaces.
    /// </exception>
    public static void ArgumentIsNotNullOrWhiteSpace(
        [NotNull] string? argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIfNullOrWhiteSpaceInterpolatedStringHandler handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentIsNotNullOrWhiteSpace(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument is not <see langword="null" />, is not empty, and does not consist only of spaces.
    /// </summary>
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
    ///     It is thrown if the argument is <see langword="null" />, empty, or consists only of spaces.
    /// </exception>
    public static void ArgumentIsNotNullOrWhiteSpace(
        [NotNull] string? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            OnGuardFailed($"Argument {argumentExpression} with value \"{argument ?? "[null]"}\" must not be null, empty or consists only of white-space characters. {message}");
        }

#if !NETCOREAPP3_0_OR_GREATER
        argument = string.Empty;
#endif
    }

    /// <summary>
    ///     Checks that the specified argument is not <see langword="null" />, is not empty, and does not consist only of spaces.
    /// </summary>
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
    ///     It is thrown if the argument is <see langword="null" />, empty, or consists only of spaces.
    /// </exception>
    /// <returns>
    ///     The original value of the argument.
    /// </returns>
    public static string EnsureArgumentIsNotNullOrWhiteSpace(
        [NotNull] string? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentIsNotNullOrWhiteSpace(argument, message, argumentExpression);
        return argument;
    }

    /// <summary>
    ///     Checks that the specified string value is not <see langword="null" />, is not empty, and does not consist only of spaces.
    /// </summary>
    /// <param name="value">
    ///     The string value being checked.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the value is <see langword="null" />, empty or consists only of spaces.
    /// </exception>
    public static void IsNotNullOrWhiteSpace(
        [NotNull] string? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpression = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            OnGuardFailed($"The value {valueExpression} must not be null, empty or consist only of white-space characters. {message}");
        }

#if !NETCOREAPP3_0_OR_GREATER
        value = string.Empty;
#endif
    }
}
