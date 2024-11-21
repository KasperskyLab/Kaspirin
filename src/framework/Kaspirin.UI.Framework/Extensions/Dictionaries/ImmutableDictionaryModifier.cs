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
using System.Linq;

namespace Kaspirin.UI.Framework.Extensions.Dictionaries
{
    internal static class ImmutableDictionaryModifier
    {
        public static IImmutableDictionary<TKey, TValue?> UpdateItem<TKey, TValue>(
            IImmutableDictionary<TKey, TValue?> dictionary,
            TKey key,
            Func<TValue?, TValue?> modifier)
            where TKey : notnull
        {
            Guard.ArgumentIsNotNull(dictionary);
            Guard.ArgumentIsNotNull(modifier);

            return dictionary.SetItem(key, modifier(dictionary[key]));
        }

        public static IImmutableDictionary<TKey, TValue?> UpdateItems<TKey, TValue>(
            IImmutableDictionary<TKey, TValue?> dictionary,
            Func<TKey, TValue?, TValue?> modifier)
            where TKey : notnull
        {
            Guard.ArgumentIsNotNull(dictionary);
            Guard.ArgumentIsNotNull(modifier);

            if (dictionary is ImmutableDictionary<TKey, TValue?> immutableDictionary)
            {
                var builder = immutableDictionary.ToBuilder();

                foreach (var key in builder.Keys.ToList())
                {
                    builder[key] = modifier(key, builder[key]);
                }

                return builder.ToImmutable();
            }

#if NETCOREAPP
            if (dictionary is ImmutableSortedDictionary<TKey, TValue?> immutableSortedDictionary)
            {
                var builder = immutableSortedDictionary.ToBuilder();

                foreach (var key in builder.Keys.ToList())
                {
                    builder[key] = modifier(key, builder[key]);
                }

                return builder.ToImmutable();
            }
#endif

            throw new NotSupportedException($"Not supported dictionary type {dictionary.GetType()}");
        }

        public static IImmutableDictionary<TKey, TValue?> AddOrUpdate<TKey, TValue>(
            IImmutableDictionary<TKey, TValue?> dictionary,
            TKey key,
            Func<TValue?> defaultValueFactory,
            Func<TValue?, TValue?> modifier)
            where TKey : notnull
        {
            Guard.ArgumentIsNotNull(dictionary);
            Guard.ArgumentIsNotNull(defaultValueFactory);
            Guard.ArgumentIsNotNull(modifier);

            if (dictionary.TryGetValue(key, out var oldValue))
            {
                var newValue = modifier(oldValue);
                dictionary = dictionary.Remove(key);
                dictionary = dictionary.Add(key, newValue);
            }
            else
            {
                dictionary = dictionary.Add(key, defaultValueFactory());
            }

            return dictionary;
        }
    }
}
