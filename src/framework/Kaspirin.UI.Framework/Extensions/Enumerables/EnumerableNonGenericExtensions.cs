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

namespace Kaspirin.UI.Framework.Extensions.Enumerables
{
    /// <summary>
    ///     Extension methods for <see cref="IEnumerable" />.
    /// </summary>
    public static class EnumerableNonGenericExtensions
    {
        /// <summary>
        ///     Checks that the enumeration is empty.
        /// </summary>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the enumeration is empty, otherwise <see langword="false" />.
        /// </returns>
        public static bool AnyNonGeneric(this IEnumerable source)
            => AnyNonGeneric(source, _ => true);

        /// <summary>
        ///     Checks that the enumeration contains elements that meet the verification condition <paramref name="predict" />.
        /// </summary>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <param name="predicate">
        ///     Verification condition.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the enumeration contains elements that meet the verification
        ///     condition, otherwise <see langword="false" />.
        /// </returns>
        public static bool AnyNonGeneric(this IEnumerable source, Func<object?, bool> predicate)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(predicate);

            var enumerator = source.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return true;
                    }
                }
            }
            finally
            {
                EnsureEnumeratorDisposed(enumerator);
            }

            return false;
        }

        /// <summary>
        ///     Counts the number of elements in the enumeration.
        /// </summary>
        /// <param name="source">
        ///     Enumeration.
        /// </param>
        /// <returns>
        ///     Returns the number of items in the enumeration.
        /// </returns>
        public static int CountNonGeneric(this IEnumerable source)
            => CountNonGeneric(source, _ => true);

        /// <summary>
        ///     Counts the number of elements in the enumeration that meet the verification condition <paramref name="predict" />.
        /// </summary>
        /// <param name="source">
        ///     The enumeration being checked.
        /// </param>
        /// <param name="predicate">
        ///     Verification condition.
        /// </param>
        /// <returns>
        ///     Returns the number of items in the enumeration that meet the verification condition.
        /// </returns>
        public static int CountNonGeneric(this IEnumerable source, Func<object?, bool> predicate)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(predicate);

            var count = 0;

            var enumerator = source.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        count++;
                    }
                }
            }
            finally
            {
                EnsureEnumeratorDisposed(enumerator);
            }

            return count;
        }

        /// <summary>
        ///     Calls the action <paramref name="action" /> for each element of the enumeration.
        /// </summary>
        /// <param name="source">
        ///     Enumeration.
        /// </param>
        /// <param name="action">
        ///     The action being performed.
        /// </param>
        public static void ForEachNonGeneric(this IEnumerable source, Action<object> action)
        {
            Guard.ArgumentIsNotNull(source);
            Guard.ArgumentIsNotNull(action);

            foreach (var item in source)
            {
                action(item);
            }
        }

        private static void EnsureEnumeratorDisposed(IEnumerator enumerator)
        {
            // IEnumerator does not inherit from IDisposable, but IEnumerator<T> does.
            // We have to check whether enumerator is IDisposable or not for the case
            // when argument is IEnumerable<T> that is passed by IEnumerable reference.

            if (enumerator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
