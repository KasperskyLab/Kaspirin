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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Kaspirin.UI.Framework.Guards;

/// <summary>
///     A static class that provides various methods for checking conditions (guards).
/// </summary>
public static partial class Guard
{
    /// <summary>
    ///     Checks that the provided list is not <see langword="null" /> or empty.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of items in the list.
    /// </typeparam>
    /// <param name="argument">
    ///     The list being checked.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the list is empty or <see langword="null" />.
    /// </exception>
    public static void ArgumentCollectionIsNotNullOrEmpty<T>(
        [NotNull] ICollection<T>? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        if (argument is null || argument.Count == 0)
        {
            OnGuardFailed($"Argument {argumentExpression} (collection of type \"{typeof(T)}\") must not be null or empty. {message}");
        }
    }

    /// <summary>
    ///     Checks that the provided list is not <see langword="null" /> or empty.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of items in the list.
    /// </typeparam>
    /// <param name="argument">
    ///     The list being checked.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="argumentExpression">
    ///     An optional expression representing the <paramref name="argument" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the list is empty or <see langword="null" />.
    /// </exception>
    /// <returns>
    ///     The list being checked.
    /// </returns>
    public static ICollection<T> EnsureArgumentCollectionIsNotNullOrEmpty<T>(
        [NotNull] ICollection<T>? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? argumentExpression = null)
    {
        ArgumentCollectionIsNotNullOrEmpty(argument, message, argumentExpression);
        return argument;
    }
}
