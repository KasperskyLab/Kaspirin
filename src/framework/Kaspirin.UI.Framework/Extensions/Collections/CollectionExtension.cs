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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kaspirin.UI.Framework.Extensions.Collections
{
    /// <summary>
    ///     Extension methods for the collection <see cref="ICollection" />.
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        ///     Checks if the collection is empty.
        /// </summary>
        /// <param name="collection">
        ///     Collection.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the collection is empty, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEmpty(this ICollection collection)
        {
            Guard.ArgumentIsNotNull(collection);

            return collection.Count == 0;
        }

        /// <summary>
        ///     Adds items to the collection.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the item in the collection.
        /// </typeparam>
        /// <param name="collection">
        ///     Collection.
        /// </param>
        /// <param name="range">
        ///     The elements being added.
        /// </param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            Guard.ArgumentIsNotNull(collection);
            Guard.ArgumentIsNotNull(range);

            foreach (var item in range)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        ///     Deletes elements that meet the condition.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the item in the collection.
        /// </typeparam>
        /// <param name="collection">
        ///     Collection.
        /// </param>
        /// <param name="predicate">
        ///     The condition for deletion.
        /// </param>
        /// <returns>
        ///     The number of deleted items.
        /// </returns>
        public static int RemoveAll<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            Guard.ArgumentIsNotNull(collection);
            Guard.ArgumentIsNotNull(predicate);

            var toRemove = collection
                .Where(item => predicate(item))
                .ToArray();
            foreach (var itemToRemove in toRemove)
            {
                collection.Remove(itemToRemove);
            }

            return toRemove.Length;
        }

        /// <summary>
        ///     Deletes items from the collection.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the item in the collection.
        /// </typeparam>
        /// <param name="collection">
        ///     Collection.
        /// </param>
        /// <param name="items">
        ///     Items to delete.
        /// </param>
        /// <returns>
        ///     The <paramref name="collection" /> object from which the <paramref name="items" /> elements were removed.
        /// </returns>
        public static ICollection<T> RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Guard.ArgumentIsNotNull(collection);
            Guard.ArgumentIsNotNull(items);

            foreach (var each in items)
            {
                collection.Remove(each);
            }

            return collection;
        }
    }
}
