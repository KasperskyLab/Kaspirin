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
using System.Collections.Generic;
using System.Threading;

using DoesNotReturnAttribute = System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute;
using DoesNotReturnIfAttribute = System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#else
using Kaspirin.UI.Framework.BackwardCompatibility;
#endif

namespace Kaspirin.UI.Framework.Guards
{
    /// <summary>
    ///     A static class that provides various methods for checking conditions (guards).
    /// </summary>
    public static class Guard
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
            [CallerArgumentExpression("argument")] string? argumentExpression = null) where T : class
        {
            return EnsureArgumentIsNotNull(argument, handler.ToStringAndClear(), argumentExpression);
        }

        /// <summary>
        ///     Checks that the specified argument is not equal to <see langword="null" />.
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
        ///     It is thrown if the argument is <see langword="null" />.
        /// </exception>
        /// <returns>
        ///     The original value of the argument.
        /// </returns>
        public static bool EnsureArgumentIsNotNull(
            [NotNull] bool? argument,
            string? message = null,
            [CallerArgumentExpression("argument")] string? argumentExpression = null)
        {
            ArgumentIsNotNull(argument, message, argumentExpression);
            return argument.Value;
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
            [CallerArgumentExpression("argument")] string? argumentExpression = null) where T : class
        {
            ArgumentIsNotNull(argument, message, argumentExpression);
            return argument;
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

        /// <summary>
        ///     Checks that the specified argument is not <see langword="null" /> or an empty string.
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
        ///     It is thrown if the argument is <see langword="null" /> or an empty string.
        /// </exception>
        /// <returns>
        ///     The original value of the argument.
        /// </returns>
        public static string EnsureArgumentIsNotNullOrEmpty(
            [NotNull] string? argument,
            [InterpolatedStringHandlerArgument("argument")] ref AssertIfNullOrEmptyInterpolatedStringHandler handler,
            [CallerArgumentExpression("argument")] string? argumentExpression = null)
        {
            return EnsureArgumentIsNotNullOrEmpty(argument, handler.ToStringAndClear(), argumentExpression);
        }

        /// <summary>
        ///     Checks that the specified argument is not <see langword="null" /> or an empty string.
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
        ///     It is thrown if the argument is <see langword="null" /> or an empty string.
        /// </exception>
        /// <returns>
        ///     The original value of the argument.
        /// </returns>
        public static string EnsureArgumentIsNotNullOrEmpty(
            [NotNull] string? argument,
            string? message = null,
            [CallerArgumentExpression("argument")] string? argumentExpression = null)
        {
            ArgumentIsNotNullOrEmpty(argument, message, argumentExpression);
            return argument;
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
        ///     Checks that the specified argument is not <see langword="null" /> or an empty string.
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
        ///     It is thrown if the argument is <see langword="null" /> or an empty string.
        /// </exception>
        public static void ArgumentIsNotNullOrEmpty(
            [NotNull] string? argument,
            [InterpolatedStringHandlerArgument("argument")] ref AssertIfNullOrEmptyInterpolatedStringHandler handler,
            [CallerArgumentExpression("argument")] string? argumentExpression = null)
        {
            ArgumentIsNotNullOrEmpty(argument, handler.ToStringAndClear(), argumentExpression);
        }

        /// <summary>
        ///     Checks that the specified argument is not <see langword="null" /> or an empty string.
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
        ///     It is thrown if the argument is <see langword="null" /> or an empty string.
        /// </exception>
        public static void ArgumentIsNotNullOrEmpty(
            [NotNull] string? argument,
            string? message = null,
            [CallerArgumentExpression("argument")] string? argumentExpression = null)
        {
            if (string.IsNullOrEmpty(argument))
            {
                OnGuardFailed($"Argument {argumentExpression} with value \"{argument ?? "[null]"}\" must not be null or empty. {message}");
            }
        }

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
        ///     Checks that the specified argument <paramref name="argument" /> is empty <see cref="Guid" />.
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
        ///     Called when the <paramref name="argument" /> argument is not empty <see cref="Guid" />.
        /// </exception>
        public static void ArgumentIsEmptyGuid(
            Guid argument,
            [InterpolatedStringHandlerArgument("argument")] ref AssertIfNotEmptyGuidInterpolatedStringHandler handler,
            [CallerArgumentExpression("argument")] string? argumentExpression = null)
        {
            ArgumentIsEmptyGuid(argument, handler.ToStringAndClear(), argumentExpression);
        }

        /// <summary>
        ///     Checks that the specified argument <paramref name="argument" /> is empty <see cref="Guid" />.
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
        ///     Called when the <paramref name="argument" /> argument is not empty <see cref="Guid" />.
        /// </exception>
        public static void ArgumentIsEmptyGuid(
            Guid argument,
            string? message = null,
            [CallerArgumentExpression("argument")] string? argumentExpression = null)
        {
            if (argument != Guid.Empty)
            {
                OnGuardFailed($"Guid argument {argumentExpression} with value \"{argument}\" must be empty. {message}");
            }
        }

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
        ///     Checks that the current thread is a user interface thread.
        /// </summary>
        /// <exception cref="GuardException">
        ///     It is thrown if the method is not executed in the user interface thread.
        /// </exception>
        public static void AssertIsUiThread()
        {
            IsNotNull(_uiThreadId, "UI Thread ID is not set");

            if (_uiThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                OnGuardFailed("Method should be executed in UI thread");
            }
        }

        /// <summary>
        ///     Checks that the current thread is not a user interface thread.
        /// </summary>
        /// <exception cref="GuardException">
        ///     It is thrown if the method is executed in the user interface thread.
        /// </exception>
        public static void AssertIsNotUiThread()
        {
            IsNotNull(_uiThreadId, "UI Thread ID is not set");

            if (_uiThreadId == Thread.CurrentThread.ManagedThreadId)
            {
                OnGuardFailed("Method should be executed not in UI thread");
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
            [CallerArgumentExpression("value")] string? valueExpression = null) where T : class
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
            [CallerArgumentExpression("value")] string? valueExpression = null) where T : class
        {
            IsNotNull(value, message, valueExpression);
            return value;
        }

        /// <summary>
        ///     Checks that the specified <see cref="bool" />? the value is not equal to <see langword="null" /> and returns it.
        /// </summary>
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
        public static bool EnsureIsNotNull(
            [NotNull] bool? value,
            string? message = null,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            IsNotNull(value, message, valueExpression);
            return value.Value;
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
            [CallerArgumentExpression("value")] string? valueExpression = null) where T : struct
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
            [CallerArgumentExpression("value")] string? valueExpression = null) where T : struct
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
            where T : class
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
        ///     Checks that the specified value is not <see langword="null" /> or an empty string.
        /// </summary>
        /// <param name="value">
        ///     The string value being checked.
        /// </param>
        /// <param name="handler">
        ///     An interpolated string handler for the condition validation error message.
        /// </param>
        /// <param name="valueExpression">
        ///     An optional expression representing the <paramref name="value" /> argument to include in the error message.
        /// </param>
        /// <exception cref="GuardException">
        ///     It is thrown if the value is <see langword="null" /> or an empty string.
        /// </exception>
        public static void IsNotNullOrEmpty(
            [NotNull] string? value,
            [InterpolatedStringHandlerArgument("value")] ref AssertIfNullOrEmptyInterpolatedStringHandler handler,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            IsNotNullOrEmpty(value, handler.ToStringAndClear(), valueExpression);
        }

        /// <summary>
        ///     Checks that the specified value is not <see langword="null" /> or an empty string.
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
        ///     It is thrown if the value is <see langword="null" /> or an empty string.
        /// </exception>
        public static void IsNotNullOrEmpty(
            [NotNull] string? value,
            string? message = null,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                OnGuardFailed($"The value {valueExpression} must not be null or empty. {message}");
            }
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
        }

        /// <summary>
        ///     Checks that the specified value <see cref="Guid" /> is empty.
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
        ///     It is thrown if the value <see cref="Guid" /> is not empty.
        /// </exception>
        public static void IsEmptyGuid(
            Guid value,
            [InterpolatedStringHandlerArgument("value")] ref AssertIfNotEmptyGuidInterpolatedStringHandler handler,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            IsEmptyGuid(value, handler.ToStringAndClear(), valueExpression);
        }

        /// <summary>
        ///     Checks that the specified value <see cref="Guid" /> is empty.
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
        ///     It is thrown if the value <see cref="Guid" /> is not empty.
        /// </exception>
        public static void IsEmptyGuid(
            Guid value,
            string? message = null,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            if (value != Guid.Empty)
            {
                OnGuardFailed($"Guid {valueExpression} with value \"{value}\" must be empty. {message}");
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

        /// <summary>
        ///     Sets the UI thread ID used in the <see cref="AssertIsUiThread" /> and <see cref="AssertIsNotUiThread" /> methods.
        /// </summary>
        /// <param name="uiThreadId">
        ///     The identifier of the UI stream.
        /// </param>
        public static void SetUiThreadId(int uiThreadId)
        {
            _uiThreadId = uiThreadId;
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

        private static int? _uiThreadId;
    }
}
