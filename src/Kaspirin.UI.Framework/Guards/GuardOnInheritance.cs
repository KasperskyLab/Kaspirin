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

namespace Kaspirin.UI.Framework.Guards;

/// <summary>
///     A static class that provides various methods for checking conditions (guards).
/// </summary>
public static partial class Guard
{
    /// <summary>
    ///     Checks whether the specified type implements the specified interface.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of interface to check.
    /// </typeparam>
    /// <param name="type">
    ///     The type to check.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="typeExpression">
    ///     An optional expression representing the <paramref name="type" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the type does not implement the <typeparamref name="T" /> interface.
    /// </exception>
    public static void ImplementsInterface<T>(
        Type type,
        string? message = null,
        [CallerArgumentExpression("type")] string? typeExpression = null)
        where T : class
    {
        if (type.GetInterface(typeof(T).Name) is null)
        {
            OnGuardFailed($"The value {typeExpression} of type {type.Name} must implement {nameof(T)} interface. {message}");
        }
    }

    /// <summary>
    ///     Checks whether the specified type is a subtype of the base type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The base type to check.
    /// </typeparam>
    /// <param name="type">
    ///     The type to check.
    /// </param>
    /// <param name="message">
    ///     An optional message to include in the error message.
    /// </param>
    /// <param name="typeExpression">
    ///     An optional expression representing the <paramref name="type" /> argument to include in the error message.
    /// </param>
    /// <exception cref="GuardException">
    ///     It is thrown if the type is not a subtype of the base type <typeparamref name="T" />.
    /// </exception>
    public static void IsSubClassOf<T>(
        Type type,
        string? message = null,
        [CallerArgumentExpression("type")] string? typeExpression = null)
        where T : class
    {
        if (!type.IsSubclassOf(typeof(T)))
        {
            OnGuardFailed($"The value {typeExpression} that is type {type.Name} must be derived from {typeof(T).Name}. {message}");
        }
    }
}
