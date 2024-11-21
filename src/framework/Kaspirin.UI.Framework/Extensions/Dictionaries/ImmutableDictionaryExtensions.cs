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
using System.Collections.Immutable;

namespace Kaspirin.UI.Framework.Extensions.Dictionaries
{
    /// <summary>
    ///     Extension methods for <see cref="ImmutableDictionary{TKey,TValue}" />.
    /// </summary>
    public static class ImmutableDictionaryExtensions
    {
        /// <summary>
        ///     Returns a copy of the dictionary <paramref name="dictionary" /> with the value changed by key.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of key.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of value.
        /// </typeparam>
        /// <param name="dictionary">
        ///     Dictionary.
        /// </param>
        /// <param name="key">
        ///     The key to change the value.
        /// </param>
        /// <param name="modifier">
        ///     The delegate that returns the modified value.
        /// </param>
        /// <returns>
        ///     A copy of the dictionary <paramref name="dictionary" /> with changes.
        /// </returns>
        public static ImmutableDictionary<TKey, TValue?> UpdateItem<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue?> dictionary,
            TKey key,
            Func<TValue?, TValue?> modifier)
            where TKey : notnull
            => (ImmutableDictionary<TKey, TValue?>)ImmutableDictionaryModifier.UpdateItem(dictionary, key, modifier);

        /// <summary>
        ///     Returns a copy of the dictionary <paramref name="dictionary" /> with modified values. Each
        ///     value in the dictionary is modified by the <paramref name="modifier" /> delegate.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of key.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of value.
        /// </typeparam>
        /// <param name="dictionary">
        ///     Dictionary.
        /// </param>
        /// <param name="modifier">
        ///     The delegate that returns the modified value.
        /// </param>
        /// <returns>
        ///     A copy of the dictionary <paramref name="dictionary" /> with changes.
        /// </returns>
        public static ImmutableDictionary<TKey, TValue?> UpdateItems<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue?> dictionary,
            Func<TValue?, TValue?> modifier)
            where TKey : notnull
            => UpdateItems(dictionary, (k, v) => modifier(v));

        /// <summary>
        ///     Returns a copy of the dictionary <paramref name="dictionary" /> with modified values. Each
        ///     value in the dictionary is modified by the <paramref name="modifier" /> delegate.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of key.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of value.
        /// </typeparam>
        /// <param name="dictionary">
        ///     Dictionary.
        /// </param>
        /// <param name="modifier">
        ///     The delegate that returns the modified value.
        /// </param>
        /// <returns>
        ///     A copy of the dictionary <paramref name="dictionary" /> with changes.
        /// </returns>
        public static ImmutableDictionary<TKey, TValue?> UpdateItems<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue?> dictionary,
            Func<TKey, TValue?, TValue?> modifier)
            where TKey : notnull
            => (ImmutableDictionary<TKey, TValue?>)ImmutableDictionaryModifier.UpdateItems(dictionary, modifier);

        /// <summary>
        ///     Returns a copy of the dictionary <paramref name="dictionary" /> with the value changed or added by key.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of key.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of value.
        /// </typeparam>
        /// <param name="dictionary">
        ///     Dictionary.
        /// </param>
        /// <param name="key">
        ///     The key to change the value.
        /// </param>
        /// <param name="modifier">
        ///     A delegate that returns the modified value if the key already exists.
        /// </param>
        /// <remarks>
        ///     If the key does not exist, default values will be added to the dictionary <see langword="default" />
        ///     for this type <typeparamref name="TValue" />.
        /// </remarks>
        /// <returns>
        ///     A copy of the dictionary <paramref name="dictionary" /> with changes.
        /// </returns>
        public static ImmutableDictionary<TKey, TValue?> AddOrUpdate<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue?> dictionary,
            TKey key,
            Func<TValue?, TValue?> modifier)
            where TKey : notnull
            => AddOrUpdate(dictionary, key, () => default, modifier);

        /// <summary>
        ///     Returns a copy of the dictionary <paramref name="dictionary" /> with the value changed or added by key.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of key.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of value.
        /// </typeparam>
        /// <param name="dictionary">
        ///     Dictionary.
        /// </param>
        /// <param name="key">
        ///     The key to change the value.
        /// </param>
        /// <param name="modifier">
        ///     A delegate that returns the modified value if the key already exists.
        /// </param>
        /// <param name="defaultValueFactory">
        ///     A delegate that returns the default value if the key is not found.
        /// </param>
        /// <returns>
        ///     A copy of the dictionary <paramref name="dictionary" /> with changes.
        /// </returns>
        public static ImmutableDictionary<TKey, TValue?> AddOrUpdate<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue?> dictionary,
            TKey key,
            Func<TValue?> defaultValueFactory,
            Func<TValue?, TValue?> modifier)
            where TKey : notnull
            => (ImmutableDictionary<TKey, TValue?>)ImmutableDictionaryModifier.AddOrUpdate(dictionary, key, defaultValueFactory, modifier);
    }
}
