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
    ///     Checks that the specified argument is present in the enumeration <typeparamref name="TEnum" />.
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
    ///     It is thrown if the argument is missing in the enumeration <typeparamref name="TEnum" />.
    /// </exception>
    public static void ArgumentIsValidEnum<TEnum>(
        [NotNull] TEnum argument,
        [InterpolatedStringHandlerArgument("argument")] ref AssertIfNullOrEmptyInterpolatedStringHandler handler,
        [CallerArgumentExpression("argument")] string? argumentExpression = null) where TEnum : struct, Enum
    {
        ArgumentIsValidEnum(argument, handler.ToStringAndClear(), argumentExpression);
    }

    /// <summary>
    ///     Checks that the specified argument is present in the enumeration <typeparamref name="TEnum" />.
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
    ///     It is thrown if the argument is missing in the enumeration <typeparamref name="TEnum" />.
    /// </exception>
    public static void ArgumentIsValidEnum<TEnum>(
        [NotNull] TEnum argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null) where TEnum : struct, Enum
    {
        if (EnumOperations.IsValidValue(argument) is false)
        {
            OnGuardFailed($"Argument {argumentExpression} with value \"{argument}\" must be in Enum {typeof(TEnum)}. {message}");
        }
    }
}
