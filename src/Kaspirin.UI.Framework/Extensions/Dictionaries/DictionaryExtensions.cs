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

namespace Kaspirin.UI.Framework.Extensions.Dictionaries;

/// <summary>
///     Extension methods for <see cref="IDictionary{TKey,TValue}" />.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    ///     Gets the value by key or adds the default value to the dictionary if there is no such key.
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
    ///     The key to get or add a value.
    /// </param>
    /// <remarks>
    ///     The default value is <see langword="default" /> for this type <typeparamref name="TValue" />.
    /// </remarks>
    /// <returns>
    ///     The key value or the default value if there is no such key.
    /// </returns>
    public static TValue? GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key)
        where TKey : notnull
        => dictionary.GetOrAdd(key, () => default);

    /// <summary>
    ///     Gets the value by key or adds the default value to the dictionary if there is no such key.
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
    ///     The key to get or add a value.
    /// </param>
    /// <param name="defaultValue">
    ///     The default value.
    /// </param>
    /// <returns>
    ///     The value by key or <paramref name="defaultValue" />, if there is no such key.
    /// </returns>
    public static TValue? GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key, TValue? defaultValue)
        where TKey : notnull
        => dictionary.GetOrAdd(key, () => defaultValue);

    /// <summary>
    ///     Gets the value by key or adds the default value to the dictionary if there is no such key.
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
    ///     The key to get or add a value.
    /// </param>
    /// <param name="defaultValueFactory">
    ///     The delegate that returns the default value.
    /// </param>
    /// <returns>
    ///     The value by key or the value obtained from <paramref name="defaultValueFactory" />, if there is no such key.
    /// </returns>
    public static TValue? GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key, Func<TValue?> defaultValueFactory)
        where TKey : notnull
    {
        Guard.ArgumentIsNotNull(dictionary);
        Guard.ArgumentIsNotNull(defaultValueFactory);

        if (dictionary.TryGetValue(key, out var value))
        {
            return value;
        }

        value = defaultValueFactory();

        dictionary.Add(key, value);

        return value;
    }

    /// <summary>
    ///     Returns the key value or the default value if there is no such key.
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
    ///     The key to get the value.
    /// </param>
    /// <remarks>
    ///     The default value is <see langword="default" /> for this type <typeparamref name="TValue" />.
    /// </remarks>
    /// <returns>
    ///     The key value or the default value if there is no such key.
    /// </returns>
    public static TValue? GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key)
        where TKey : notnull
        => dictionary.GetValueOrDefault(key, () => default);

    /// <summary>
    ///     Returns the key value or the default value if there is no such key.
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
    ///     The key to get the value.
    /// </param>
    /// <param name="defaultValue">
    ///     The default value.
    /// </param>
    /// <returns>
    ///     The value by key or <paramref name="defaultValue" />, if there is no such key.
    /// </returns>
    public static TValue? GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key, TValue? defaultValue)
        where TKey : notnull
        => dictionary.GetValueOrDefault(key, () => defaultValue);

    /// <summary>
    ///     Returns the key value or the default value if there is no such key.
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
    ///     The key to get the value.
    /// </param>
    /// <param name="defaultValueFactory">
    ///     The delegate that returns the default value.
    /// </param>
    /// <returns>
    ///     The value by key or the value obtained from <paramref name="defaultValueFactory" />, if there is no such key.
    /// </returns>
    public static TValue? GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key, Func<TValue?> defaultValueFactory)
        where TKey : notnull
    {
        Guard.ArgumentIsNotNull(dictionary);
        Guard.ArgumentIsNotNull(defaultValueFactory);

        return dictionary.TryGetValue(key, out var value) ? value : defaultValueFactory();
    }

    /// <summary>
    ///     Adds a new key-value pair to the dictionary or updates an existing key value.
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
    ///     The key to add or update the value.
    /// </param>
    /// <param name="value">
    ///     The value to add or update.
    /// </param>
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key, TValue? value)
    {
        Guard.ArgumentIsNotNull(dictionary);

        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    /// <summary>
    ///     Adds a new key-value pair to the dictionary or updates an existing key value using the update function.
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
    ///     The key to add or update the value.
    /// </param>
    /// <param name="addValue">
    ///     The value to add if the key is missing.
    /// </param>
    /// <param name="updateValueFactory">
    ///     The value update function that is called when there is a key.
    /// </param>
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, TKey key, TValue? addValue, Func<TKey, TValue?, TValue?> updateValueFactory)
    {
        Guard.ArgumentIsNotNull(dictionary);
        Guard.ArgumentIsNotNull(updateValueFactory);

        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = updateValueFactory(key, dictionary[key]);
        }
        else
        {
            dictionary.Add(key, addValue);
        }
    }

    /// <summary>
    ///     Converts the dictionary into a string for debugging by representing each key-value pair, connecting
    ///     each pair using a separator ",", or <see cref="Environment.NewLine" /> if <paramref name="multipleLines" /> is <see langword="true" />.
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
    /// <param name="multipleLines">
    ///     Specifies whether to use a newline character as a separator between key-value pairs.
    /// </param>
    /// <param name="indent">
    ///     Specifies whether to apply indentation to each key-value pair during output.
    /// </param>
    /// <returns>
    ///     A string representing a dictionary with key-value pairs separated in the specified way. If <paramref name="dictionary" />
    ///     is <see langword="null" />, an empty string is returned.
    /// </returns>
    public static string Dump<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, bool multipleLines = false, bool indent = false)
    {
        if (dictionary == null)
        {
            return string.Empty;
        }

        return dictionary.Select(kvp => $"{kvp.Key}: {kvp.Value}").Dump(multipleLines, indent);
    }
}
