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
    ///     Disables condition checking in all methods of the <see cref="Guard" /> class.
    /// </summary>
    public static bool DisableGuard { get; set; } = false;

    /// <summary>
    ///     An event with information about an error that occurs when the conditions for checking guards are violated.
    /// </summary>
    public static event EventHandler<GuardFailedEventArgs> GuardFailed = delegate { };

    /// <summary>
    ///     Checks that the specified condition <paramref name="condition" /> returns <see langword="true" />.
    /// </summary>
    /// <param name="condition">
    ///     The condition being checked.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition <paramref name="condition" />.
    /// </param>
    /// <param name="conditionExpression">
    ///     An optional expression representing the condition <paramref name="condition" /> to be included in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the condition <paramref name="condition" /> is not true.
    /// </exception>
    public static void Argument(
        [DoesNotReturnIf(false)] bool condition,
        [InterpolatedStringHandlerArgument("condition")] ref AssertIfFalseInterpolatedStringHandler handler,
        [CallerArgumentExpression("condition")] string? conditionExpression = null)
    {
        Argument(condition, handler.ToStringAndClear(), conditionExpression);
    }

    /// <summary>
    ///     Checks that the specified condition <paramref name="condition" /> returns <see langword="true" />.
    /// </summary>
    /// <param name="condition">
    ///     The condition being checked.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="conditionExpression">
    ///     An optional expression representing the condition <paramref name="condition" /> to be included in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the condition <paramref name="condition" /> is not true.
    /// </exception>
    public static void Argument(
        [DoesNotReturnIf(false)] bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")] string? conditionExpression = null)
    {
        if (!condition)
        {
            OnGuardFailed($"The condition {conditionExpression} should be true. {message}");
        }
    }

    /// <summary>
    ///     Checks that the specified condition is true.
    /// </summary>
    /// <param name="condition">
    ///     The condition for verification.
    /// </param>
    /// <param name="handler">
    ///     An interpolated string handler for the condition <paramref name="condition" />.
    /// </param>
    /// <param name="conditionExpression">
    ///     An optional expression representing the <paramref name="condition" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the condition <paramref name="condition" /> is false.
    /// </exception>
    public static void Assert(
        [DoesNotReturnIf(false)] bool condition,
        [InterpolatedStringHandlerArgument("condition")] ref AssertIfFalseInterpolatedStringHandler handler,
        [CallerArgumentExpression("condition")] string? conditionExpression = null)
    {
        Assert(condition, handler.ToStringAndClear(), conditionExpression);
    }

    /// <summary>
    ///     Checks that the specified condition is true.
    /// </summary>
    /// <param name="condition">
    ///     The condition for verification.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="conditionExpression">
    ///     An optional expression representing the <paramref name="condition" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the condition <paramref name="condition" /> is false.
    /// </exception>
    public static void Assert(
        [DoesNotReturnIf(false)] bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")] string? conditionExpression = null)
    {
        if (!condition)
        {
            OnGuardFailed($"The condition {conditionExpression} should be true. {message}");
        }
    }

    /// <summary>
    ///     A method that fails with the specified message and arguments.
    /// </summary>
    /// <param name="message">
    ///     The error message.
    /// </param>
    /// <param name="messageArgs">
    ///     The arguments to be used in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown out when the method is executed.
    /// </exception>
    [DoesNotReturn]
    public static void Fail(string message, params object[] messageArgs)
    {
        OnGuardFailed(message, messageArgs);
    }

    /// <summary>
    ///     Checks that the parameter <paramref name="value" /> satisfies the condition <paramref name="condition" />
    ///     and returns the value <paramref name="value" /> if successful.
    /// </summary>
    /// <returns>
    ///     Returns the value <paramref name="value" /> if successful.
    /// </returns>
    /// <exception cref="GuardException">
    ///     It is thrown if the condition <paramref name="condition" /> is not true.
    /// </exception>
    public static T EnsureAssertion<T>(
        T value,
        Func<T, bool> condition,
        [CallerArgumentExpression("condition")] string? conditionExpression = null, string? message = null)
    {
        if (!condition(value))
        {
            OnGuardFailed($"The '{value}' value must satisfy the '{conditionExpression}' condition. {message}");
        }

        return value;
    }

    [DoesNotReturn]
    private static void OnGuardFailed(string messageFormat, params object[] messageArgs)
    {
        if (DisableGuard)
        {
#pragma warning disable CS8763 // A method marked [DoesNotReturn] should not return.
            return;
#pragma warning restore CS8763 // A method marked [DoesNotReturn] should not return.
        }

        var message = (messageArgs is null || messageArgs.Length == 0)
            ? messageFormat
            : string.Format(messageFormat, messageArgs);

        var guardFailedEventArgs = new GuardFailedEventArgs(message, new GuardException(message));

        GuardFailed(null, guardFailedEventArgs);
        throw guardFailedEventArgs.OriginalException;
    }
}
