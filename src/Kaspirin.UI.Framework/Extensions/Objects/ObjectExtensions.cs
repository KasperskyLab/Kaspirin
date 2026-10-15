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

using System.Collections.Generic;
using System.Linq;
using Kaspirin.UI.Framework.Log.Internals;

namespace Kaspirin.UI.Framework.Extensions.Objects;

/// <summary>
///     Extension methods for objects.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    ///     Checks that the <paramref name="obj" /> object is contained in the <paramref name="array" /> array.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the object being checked.
    /// </typeparam>
    /// <param name="obj">
    ///     The object being checked.
    /// </param>
    /// <param name="array">
    ///     An array of objects.
    /// </param>
    /// <param name="comparer">
    ///     Object comparator.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the object is contained in an array, otherwise <see langword="false" />.
    /// </returns>
    public static bool In<T>(this T obj, T[] array, IEqualityComparer<T> comparer)
        => array.Contains(obj, comparer);

    /// <inheritdoc cref="In{T}(T, T[], IEqualityComparer{T})"/>
    public static bool In<T>(this T obj, params T[] array)
        => array.Contains(obj);

    /// <summary>
    ///     Checks that the <paramref name="obj" /> object is not contained in the <paramref name="array" /> array.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the object being checked.
    /// </typeparam>
    /// <param name="obj">
    ///     The object being checked.
    /// </param>
    /// <param name="parameters">
    ///     An array of objects.
    /// </param>
    /// <param name="array">
    ///     Object comparator.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the object is not contained in the array, otherwise - <see langword="false" />.
    /// </returns>
    public static bool NotIn<T>(this T obj, T[] parameters, IEqualityComparer<T> array)
        => !parameters.Contains(obj, array);

    /// <inheritdoc cref="NotIn{T}(T, T[], IEqualityComparer{T})"/>
    public static bool NotIn<T>(this T obj, params T[] parameters)
        => !parameters.Contains(obj);

    /// <summary>
    ///     Creates a string representation of an object with the ability to adjust margins and maximum output depth.
    /// </summary>
    /// <remarks>
    ///     This method is intended for debugging.
    /// </remarks>
    /// <param name="obj">
    ///     An object to convert to a string.
    /// </param>
    /// <param name="startIndentLevel">
    ///     The initial indentation level (0 by default).
    /// </param>
    /// <param name="maxDepthLevel">
    ///     The maximum nesting depth during output (-1 by default, which means unlimited depth).
    /// </param>
    /// <returns>
    ///     String representation of the object.
    /// </returns>
    public static string Dump(this object? obj, int startIndentLevel = 0, int maxDepthLevel = -1)
        => ObjectDumper.Dump(obj, startIndentLevel, maxDepthLevel);
}
