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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Kaspirin.UI.Framework.UiKit.Extensions;

/// <summary>
///     Extension methods for <see cref="Inline" />.
/// </summary>
public static class InlineExtensions
{
    /// <summary>
    ///     Searches for the parent <see cref="TextBlock" /> in the logical tree.
    /// </summary>
    /// <param name="inline">
    ///     The element that starts the search.
    /// </param>
    /// <returns>
    ///     Returns the found <see cref="TextBlock" /> or <see langword="null" /> if the parent is not found.
    /// </returns>
    public static TextBlock? FindParentTextBlock(this Inline inline)
    {
        var current = inline;
        while (current != null)
        {
            var parent = LogicalTreeHelper.GetParent(current) as TextBlock;
            if (parent != null)
            {
                return parent;
            }

            current = LogicalTreeHelper.GetParent(current) as Inline;
        }

        return null;
    }
}
