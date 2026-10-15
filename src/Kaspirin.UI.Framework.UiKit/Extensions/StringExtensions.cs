// Copyright © 2026 AO Kaspersky Lab.
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

using System.Linq;
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Extensions;

/// <summary>
///     Extension methods for <see cref="string" />.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Prepares the string for left-to-right display even in locales with right-to-left spelling.
    /// </summary>
    /// <param name="str">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     Returns a string with the control characters of the letter direction, or the original string if no conversion is required.
    /// </returns>
    public static string CreateLeftToRightString(this string str)
    {
        var isRTL = LocalizationManager.DisplayCulture.CultureInfo.TextInfo.IsRightToLeft;

        if (!isRTL ||
            string.IsNullOrWhiteSpace(str) ||
            ContainsArabicText(str) ||
            ContainsHebrewText(str) ||
            ContainsBiDirections(str))
        {
            return str;
        }

        return str.Aggregate(new StringBuilder(),
            (acc, sym) => acc.Append("\u200E").Append(sym),
            acc => acc.Append("\u200E").ToString());
    }

    private static bool ContainsBiDirections(string text)
    {
        return text.Any(IsBiDirectionCharacter);
    }

    private static bool ContainsArabicText(string text)
    {
        return text.Any(IsArabicCharacter);
    }

    private static bool ContainsHebrewText(string text)
    {
        return text.Any(IsHebrewCharacter);
    }

    private static bool IsArabicCharacter(char c)
    {
        return c >= 0x600 && c <= 0x6ff;
    }

    private static bool IsHebrewCharacter(char c)
    {
        return c >= 0x590 && c <= 0x5ff;
    }

    private static bool IsBiDirectionCharacter(char c)
    {
        return c >= 0x200e || c == 0x200f;
    }
}
