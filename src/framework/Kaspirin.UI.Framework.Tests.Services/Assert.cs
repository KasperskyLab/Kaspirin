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
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OriginalAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Kaspirin.UI.Framework.Tests.Services
{
    /// <Summary>
    ///     Checks the fulfillment of the conditions in the tests. If the condition has not been verified, an exception is thrown.
    /// </Summary>
    public static class Assert
    {
        /// <summary>
        ///     Checks whether the specified values are equal and throws an exception if two values are not
        ///     equal. Equality is calculated using the default comparator <see cref="EqualityComparer{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of values to compare.
        /// </typeparam>
        /// <param name="expected">
        ///     The first value for comparison. This is the value expected in the tests.
        /// </param>
        /// <param name="actual">
        ///     The second value is for comparison. This is the value obtained by the code under test.
        /// </param>
        /// <param name="message">
        ///     A message to include in an exception when the actual value is not equal to the expected value.
        ///     The message is displayed in the test results.
        /// </param>
        /// <param name="expectedExpression">
        ///     An optional expression representing the <paramref name="expectedExpression" /> argument to
        ///     be included in the error message.
        /// </param>
        /// <param name="actualExpression">
        ///     An optional expression representing the <paramref name="actualExpression" /> argument to be
        ///     included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     It is thrown if the expected value is not equal to the actual value.
        /// </exception>
        public static void AreEqual<T>(
            T? expected,
            T? actual,
            string? message = null,
            [CallerArgumentExpression("expected")] string? expectedExpression = null,
            [CallerArgumentExpression("actual")] string? actualExpression = null)
        {
            OriginalAssert.AreEqual(expected, actual, message ?? $"expected: {expectedExpression}, actual: {actualExpression}");
        }

        /// <summary>
        ///     Checks if the specified rows are equal and throws an exception if they are not equal. For comparison, <see cref="CultureInfo.InvariantCulture" /> is used.
        /// </summary>
        /// <param name="expected">
        ///     The first line is for comparison. This is the value expected in the tests.
        /// </param>
        /// <param name="actual">
        ///     The second line is for comparison. This is the value obtained by the code under test.
        /// </param>
        /// <param name="ignoreCase">
        ///     A value indicating whether or not case is taken into account when comparing strings.
        /// </param>
        /// <param name="message">
        ///     A message to include in an exception when the actual value is not equal to the expected value.
        ///     The message is displayed in the test results.
        /// </param>
        /// <param name="expectedExpression">
        ///     An optional expression representing the <paramref name="expectedExpression" /> argument to
        ///     be included in the error message.
        /// </param>
        /// <param name="actualExpression">
        ///     An optional expression representing the <paramref name="actualExpression" /> argument to be
        ///     included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     It is thrown if the expected value is not equal to the actual value.
        /// </exception>
        public static void AreEqual(
            string? expected,
            string? actual,
            bool ignoreCase,
            string? message = null,
            [CallerArgumentExpression("expected")] string? expectedExpression = null,
            [CallerArgumentExpression("actual")] string? actualExpression = null)
        {
            OriginalAssert.AreEqual(expected, actual, ignoreCase, message ?? $"expected: {expectedExpression}, actual: {actualExpression}");
        }

        /// <summary>
        ///     Checks if the specified values are not equal, and throws an exception if two values are equal.
        ///     Equality is calculated using the default comparator <see cref="EqualityComparer{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of values to compare.
        /// </typeparam>
        /// <param name="notExpected">
        ///     The first value for comparison. This is a value that the test expects does not match the actual value.
        /// </param>
        /// <param name="actual">
        ///     The second value is for comparison. This is the value obtained by the code under test.
        /// </param>
        /// <param name="message">
        ///     A message to include in the exception when the actual value is equal to the expected value.
        ///     The message is displayed in the test results.
        /// </param>
        /// <param name="notExpectedEpression">
        ///     An optional expression representing the <paramref name="notExpectedEpression" /> argument to
        ///     be included in the error message.
        /// </param>
        /// <param name="actualExpression">
        ///     An optional expression representing the <paramref name="actualExpression" /> argument to be
        ///     included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     It is thrown if the expected value is equal to the actual value.
        /// </exception>
        public static void AreNotEqual<T>(
            T? notExpected,
            T? actual,
            string? message = null,
            [CallerArgumentExpression("notExpected")] string? notExpectedEpression = null,
            [CallerArgumentExpression("actual")] string? actualExpression = null)
        {
            OriginalAssert.AreNotEqual(notExpected, actual, message ?? $"notExpected: {notExpectedEpression}, actual: {actualExpression}");
        }

        /// <summary>
        ///     Throws an AssertFailedException.
        /// </summary>
        /// <param name="message">
        ///     A message to include in the exception. The message is displayed in the test results.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     It is always thrown away.
        /// </exception>
        [DoesNotReturn]
        public static void Fail(string message)
        {
            OriginalAssert.Fail(message);
        }

        /// <summary>
        ///     Throws an AssertInconclusiveException.
        /// </summary>
        /// <param name="message">
        ///     A message to include in the exception. The message is displayed in the test results.
        /// </param>
        /// <exception cref="AssertInconclusiveException">
        ///     It is always thrown away.
        /// </exception>
        public static void Inconclusive(string message)
        {
            OriginalAssert.Inconclusive(message);
        }

        /// <summary>
        ///     Checks if the specified condition is false, and throws an exception if the condition is true.
        /// </summary>
        /// <param name="condition">
        ///     A condition that the test expects to be false.
        /// </param>
        /// <param name="message">
        ///     A message to include in the exception if the condition is true. The message is displayed in the test results.
        /// </param>
        /// <param name="conditionExpression">
        ///     An optional expression representing the <paramref name="ConditionExpression" /> argument to
        ///     be included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     It is discarded if the condition is true.
        /// </exception>
        public static void IsFalse(
            [DoesNotReturnIf(true)] bool? condition,
            string? message = null,
            [CallerArgumentExpression("condition")] string? conditionExpression = null)
        {
            OriginalAssert.IsFalse(condition, message ?? conditionExpression);
        }

        /// <summary>
        ///     Checks whether the specified object is an instance of the expected type, and throws an exception
        ///     if the expected type is not present in the object's inheritance hierarchy.
        /// </summary>
        /// <param name="value">
        ///     The object that is expected in the test belongs to the specified type.
        /// </param>
        /// <param name="expectedType">
        ///     The expected type.
        /// </param>
        /// <param name="message">
        ///     A message to include in an exception when an object is not an instance of the expected type.
        ///     The message is displayed in the test results.
        /// </param>
        /// <param name="valueExpression">
        ///     An optional expression representing the <paramref name="ValueExpression" /> argument to be
        ///     included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     Generated if the value is <see langword="null" /> or the expected type is missing from the object inheritance hierarchy.
        /// </exception>
        public static void IsInstanceOfType(
            object? value,
            Type expectedType,
            string? message = null,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            OriginalAssert.IsInstanceOfType(value, expectedType, message ?? valueExpression);
        }

        /// <summary>
        ///     Checks whether the specified object is null and throws an exception if it is <see langword="null" />.
        /// </summary>
        /// <param name="value">
        ///     The object that the test expects should not be equal to <see langword="null" />.
        /// </param>
        /// <param name="message">
        ///     A message to include in the exception if the value is <see langword="null" />. The message
        ///     is displayed in the test results.
        /// </param>
        /// <param name="valueExpression">
        ///     An optional expression representing the <paramref name="ValueExpression" /> argument to be
        ///     included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     Generated if the value is <see langword="null" />.
        /// </exception>
        public static void IsNotNull(
            [NotNull] object? value,
            string? message = null,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            OriginalAssert.IsNotNull(value, message ?? valueExpression);
        }

        /// <summary>
        ///     Checks whether the specified object is <see langword="null" />, and throws an exception if it is not.
        /// </summary>
        /// <param name="value">
        ///     The object that the test expects should be <see langword="null" />.
        /// </param>
        /// <param name="message">
        ///     A message to include in the exception if the value is not <see langword="null" />. The message
        ///     is displayed in the test results.
        /// </param>
        /// <param name="valueExpression">
        ///     An optional expression representing the <paramref name="ValueExpression" /> argument to be
        ///     included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     Generated if the value is not equal to <see langword="null" />.
        /// </exception>
        public static void IsNull(
            object? value,
            string? message = null,
            [CallerArgumentExpression("value")] string? valueExpression = null)
        {
            OriginalAssert.IsNull(value, message ?? valueExpression);
        }

        /// <summary>
        ///     Checks whether the specified condition is true, and throws an exception if the condition is false.
        /// </summary>
        /// <param name="condition">
        ///     A condition that the test expects to be true.
        /// </param>
        /// <param name="message">
        ///     A message to include in the exception if the condition is false. The message is displayed in the test results.
        /// </param>
        /// <param name="conditionExpression">
        ///     An optional expression representing the <paramref name="ConditionExpression" /> argument to
        ///     be included in the error message.
        /// </param>
        /// <exception cref="AssertFailedException">
        ///     It is thrown if the condition is false.
        /// </exception>
        public static void IsTrue(
            [DoesNotReturnIf(false)] bool condition,
            string? message = null,
            [CallerArgumentExpression("condition")] string? conditionExpression = null)
        {
            OriginalAssert.IsTrue(condition, message ?? conditionExpression);
        }

        /// <summary>
        ///     Checks whether the code in the specified delegate generates an exactly specified exception
        ///     of the type <typeparamref name="T" /> (and not a derived type), and generates <see cref="AssertFailedException" />
        ///     if the code does not generate an exception or generates an exception of a type other than <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of expected exception.
        /// </typeparam>
        /// <param name="action">
        ///     A delegate that needs to be tested and which is expected to raise an exception.
        /// </param>
        /// <param name="message">
        ///     A message to include in an exception when an action does not result in an exception of the
        ///     type <typeparamref name="T" />.
        /// </param>
        /// <returns>
        ///     The exception that was thrown.
        /// </returns>
        /// <exception cref="AssertFailedException">
        ///     Called if the action does not result in an exception of the type <typeparamref name="T" />.
        /// </exception>
        public static T ThrowsException<T>(
            Action action,
            [CallerArgumentExpression("action")] string? message = null) where T : Exception
        {
            return OriginalAssert.ThrowsException<T>(action, message ?? string.Empty);
        }
    }
}
