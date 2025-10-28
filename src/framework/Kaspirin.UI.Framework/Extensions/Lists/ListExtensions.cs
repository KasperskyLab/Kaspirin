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

namespace Kaspirin.UI.Framework.Extensions.Lists;

/// <summary>
///     Extension methods for the list <see cref="IList{T}" />.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    ///     Replaces the first item in the list that meets the condition <paramref name="predicate" />,
    ///     or adds the item to the end of the list if no such items are found.
    /// </summary>
    /// <typeparam name="TElement">
    ///     The type of the item in the list.
    /// </typeparam>
    /// <param name="elements">
    ///     List.
    /// </param>
    /// <param name="predicate">
    ///     A condition for searching for a replacement element.
    /// </param>
    /// <param name="element">
    ///     An element that is being replaced or added.
    /// </param>
    public static void ReplaceFirstOrAdd<TElement>(
        this IList<TElement> elements,
        Predicate<TElement> predicate,
        TElement element)
    {
        Guard.ArgumentIsNotNull(elements);
        Guard.ArgumentIsNotNull(predicate);

        var index = elements.IndexOf(predicate);
        if (index >= 0)
        {
            elements[index] = element;
        }
        else
        {
            elements.Add(element);
        }
    }
}
