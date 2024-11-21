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

namespace Kaspirin.UI.Framework.Extensions.Enumerables
{
    /// <summary>
    ///     Extension methods for <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Checks for the presence of an element in the enumeration that satisfies the verification condition <paramref name="predict" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <param name="predicate">
        ///     Verification condition.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the element is found, otherwise <see langword="false" />.
        /// </returns>
        public static bool Contains<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(predicate);

            return source.Any(predicate);
        }

        /// <summary>
        ///     Searches for the index of the first element in the enumeration that meets the verification
        ///     condition <paramref name="predict" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <param name="predicate">
        ///     Verification condition.
        /// </param>
        /// <returns>
        ///     Returns the index of the first found element, or -1 if the element is not found.
        /// </returns>
        public static int FindIndex<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(predicate);

            var foundItem = source
                .Select((element, index) => new { IsTrue = predicate(element), Index = index })
                .FirstOrDefault(item => item.IsTrue);

            return foundItem == null ? -1 : foundItem.Index;
        }

        /// <summary>
        ///     Calls the action <paramref name="action" /> for each element of the enumeration.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     Enumeration.
        /// </param>
        /// <param name="action">
        ///     The action being performed.
        /// </param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(action);

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        ///     Checks for duplicates in the enumeration.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <param name="comparer">
        ///     A comparator of enumeration elements.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if there are duplicates, otherwise <see langword="false" />.
        /// </returns>
        public static bool HasDuplicate<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer = null)
        {
            Guard.ArgumentIsNotNull(source);

            var checkBuffer = new HashSet<T>(comparer ?? EqualityComparer<T>.Default);
            foreach (var element in source)
            {
                if (checkBuffer.Add(element))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Splits the enumeration into blocks of the specified size, and returns the block at the specified index.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     Enumeration.
        /// </param>
        /// <param name="chunkSize">
        ///     The size of the block.
        /// </param>
        /// <param name="chunkIndex">
        ///     The block number.
        /// </param>
        /// <returns>
        ///     Returns an enumeration containing the selected block of elements.
        /// </returns>
        public static IEnumerable<T> GetChunk<T>(this IEnumerable<T> source, int chunkSize, int chunkIndex)
        {
            Guard.ArgumentIsNotNull(source);

            return source
                .Skip(chunkIndex * chunkSize)
                .Take(chunkSize);
        }

        /// <summary>
        ///     Splits the enumeration into blocks of a given size.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     Enumeration.
        /// </param>
        /// <param name="chunkSize">
        ///     The size of the block.
        /// </param>
        /// <returns>
        ///     Returns an enumeration containing blocks of elements.
        /// </returns>
        public static IEnumerable<IEnumerable<T>> ToChunks<T>(this IEnumerable<T> source, int chunkSize)
        {
            Guard.ArgumentIsNotNull(source);

            var buffer = new List<T>(chunkSize);

            foreach (var element in source)
            {
                buffer.Add(element);
                if (buffer.Count == chunkSize)
                {
                    yield return buffer;
                    buffer = new List<T>(chunkSize);
                }
            }

            if (buffer.Any())
            {
                yield return buffer;
            }
        }

        /// <summary>
        ///     Checks that the enumeration is empty.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the enumeration is empty, otherwise <see langword="false" />.
        /// </returns>
        public static bool None<T>(this IEnumerable<T> source)
        {
            Guard.ArgumentIsNotNull(source);

            return !source.Any();
        }

        /// <summary>
        ///     Checks that the enumeration does not contain elements that meet the verification condition <paramref name="predict" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <param name="predicate">
        ///     Verification condition.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the enumeration does not contain elements that meet the
        ///     verification condition, otherwise <see langword="false" />.
        /// </returns>
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(predicate);

            return !source.Any(predicate);
        }

        /// <summary>
        ///     Filters the enumeration, leaving elements in it that are not equal to <see langword="null" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     A filterable enumeration.
        /// </param>
        /// <returns>
        ///     An enumeration that does not contain elements with the value <see langword="null" />.
        /// </returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
        {
            Guard.ArgumentIsNotNull(source);

            return (IEnumerable<T>)source.Where(i => i is not null);
        }

        /// <inheritdoc cref="WhereNotNull{T}(IEnumerable{T})" />
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
        {
            Guard.ArgumentIsNotNull(source);

            return source.Where(i => i.HasValue).Select(i => i!.Value);
        }

        /// <summary>
        ///     Summarizes the elements of the enumeration, reducing each element to a number using the <paramref name="selector" /> delegate.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the element.
        /// </typeparam>
        /// <param name="source">
        ///     Enumeration.
        /// </param>
        /// <param name="selector">
        ///     The delegate that reduces the enumeration element to a number.
        /// </param>
        /// <returns>
        ///     The sum of all the elements of the transfer.
        /// </returns>
        public static ulong Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(selector);

            ulong sum = 0;
            foreach (var item in source)
            {
                sum += selector(item);
            }

            return sum;
        }
    }
}
