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
using System.Linq;

namespace Kaspirin.UI.Framework.Guards
{
    /// <summary>
    ///     Extension methods for checking collections <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class GuardEnumerableExtensions
    {
        /// <summary>
        ///     Returns a single element from the specified sequence or throws an exception if there are several
        ///     elements in the sequence or the sequence is empty.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of elements in the sequence.
        /// </typeparam>
        /// <param name="source">
        ///     The sequence for searching for an element.
        /// </param>
        /// <param name="message">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     An element in the sequence.
        /// </returns>
        public static T GuardedSingle<T>(this IEnumerable<T> source, string? message = null) where T : class
            => Guard.EnsureIsNotNull(GuardedSingleCore(source, message, canBeEmpty: false));

        /// <summary>
        ///     Returns a single element from the specified sequence, or the default value if the sequence
        ///     is empty, or throws an exception if there are several elements in the sequence.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of elements in the sequence.
        /// </typeparam>
        /// <param name="source">
        ///     The sequence for searching for an element.
        /// </param>
        /// <param name="message">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     An element in the sequence, or the default value if the sequence is empty.
        /// </returns>
        public static T? GuardedSingleOrDefault<T>(this IEnumerable<T> source, string? message = null) where T : class
            => GuardedSingleCore(source, message, canBeEmpty: true);

        /// <summary>
        ///     Returns the only element from the specified sequence that satisfies the condition, or throws
        ///     an exception if there are several such elements in the sequence or they are not there.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of elements in the sequence.
        /// </typeparam>
        /// <param name="source">
        ///     The sequence for searching for an element.
        /// </param>
        /// <param name="predicate">
        ///     The condition that must be met for the returned element.
        /// </param>
        /// <param name="message">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     An element in the sequence that satisfies the specified condition.
        /// </returns>
        public static T GuardedSingle<T>(this IEnumerable<T> source, Func<T, bool> predicate, string? message = null) where T : class
            => Guard.EnsureIsNotNull(GuardedSingleCore(source, predicate, message, canBeEmpty: false));

        /// <summary>
        ///     Returns the only element from the specified sequence that satisfies the condition, or throws
        ///     an exception if there are several such elements in the sequence.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of elements in the sequence.
        /// </typeparam>
        /// <param name="source">
        ///     The sequence for searching for an element.
        /// </param>
        /// <param name="predicate">
        ///     The condition that must be met for the returned element.
        /// </param>
        /// <param name="message">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     An element in the sequence that satisfies the specified condition, or the default value if
        ///     there are no such elements in the sequence.
        /// </returns>
        public static T? GuardedSingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, string? message = null) where T : class
            => GuardedSingleCore(source, predicate, message, canBeEmpty: true);

        private static T? GuardedSingleCore<T>(IEnumerable<T> source, string? message, bool canBeEmpty) where T : class
        {
            Guard.ArgumentIsNotNull(source);

            if (source is IList<T> list)
            {
                switch (list.Count)
                {
                    case 0:
                        ThrowNoElementsGuardFailIfNeed(canBeEmpty, message);
                        return default;
                    case 1:
                        return list[0];
                    default:
                        break;
                }
            }
            else
            {
                using var e = source.GetEnumerator();

                if (!e.MoveNext())
                {
                    ThrowNoElementsGuardFailIfNeed(canBeEmpty, message);
                    return default;
                }

                var result = e.Current;
                if (!e.MoveNext())
                {
                    return result;
                }
            }

            ThrowMoreElementsGuardFail(canBeEmpty, message);
            return default;
        }

        private static T? GuardedSingleCore<T>(IEnumerable<T> source, Func<T, bool> predicate, string? message, bool canBeEmpty) where T : class
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(predicate);

            using var e = source.GetEnumerator();

            while (e.MoveNext())
            {
                var result = e.Current;
                if (predicate(result))
                {
                    while (e.MoveNext())
                    {
                        if (predicate(e.Current))
                        {
                            ThrowMoreElementsGuardFail(canBeEmpty, message);
                        }
                    }

                    return result;
                }
            }

            ThrowNoElementsGuardFailIfNeed(canBeEmpty, message);
            return default;
        }

        private static void ThrowNoElementsGuardFailIfNeed(bool canBeEmpty, string? message)
        {
            var methodName = canBeEmpty
                ? nameof(Enumerable.SingleOrDefault)
                : nameof(Enumerable.Single);

            if (canBeEmpty is false)
            {
                Guard.Fail($"{methodName} returned no elements. {message}");
            }
        }

        private static void ThrowMoreElementsGuardFail(bool canBeEmpty, string? message)
        {
            var methodName = canBeEmpty
                ? nameof(Enumerable.SingleOrDefault)
                : nameof(Enumerable.Single);

            Guard.Fail($"{methodName} returned more than one element. {message}");
        }
    }
}