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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;

namespace Kaspirin.UI.Framework.Extensions.Strings;

/// <summary>
///     Extension methods for <see cref="string" />.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Returns the original string if it is not equal to <see langword="null" />, otherwise it returns an empty string.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     The original string or an empty string if the original string is <see langword="null" />.
    /// </returns>
    public static string OrEmpty(this string? source)
    {
        return source ?? string.Empty;
    }

    /// <summary>
    ///     Checks that the string <paramref name="source" /> is empty.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the string is empty, otherwise <see langword="false" />.
    /// </returns>
    public static bool IsEmpty(this string source)
    {
        return source.Length == 0;
    }

    /// <summary>
    ///     Checks that the string <paramref name="source" /> is empty or equal to <see langword="null" />.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the string is empty or equal to <see langword="null" />,
    ///     otherwise - <see langword="false" />.
    /// </returns>
    public static bool IsNullOrEmpty(this string? source)
    {
        return source == null || source.Length == 0;
    }

    /// <summary>
    ///     Checks that the string <paramref name="source" /> is empty, contains only spaces, or is equal to <see langword="null" />.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the string is empty, contains only spaces, or is <see langword="null" />,
    ///     otherwise <see langword="false" />.
    /// </returns>
    public static bool IsNullOrWhiteSpace(this string? source)
    {
        return source?.All(char.IsWhiteSpace) ?? true;
    }

    /// <summary>
    ///     Checks that the string <paramref name="source" /> is not empty.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the string is not empty, otherwise <see langword="false" />.
    /// </returns>
    public static bool IsNotEmpty(this string source)
    {
        return source.Length > 0;
    }

    /// <summary>
    ///     Checks that the string <paramref name="source" /> is not empty and is not equal to <see langword="null" />.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the string is not empty and is not equal to <see langword="null" />,
    ///     otherwise - <see langword="false" />.
    /// </returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? source)
    {
        return source?.Length > 0;
    }

    /// <summary>
    ///     Checks that the string <paramref name="source" /> is not empty, does not contain only spaces,
    ///     and is not equal to <see langword="null" />.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the string is not empty, does not contain only spaces and
    ///     is not equal to <see langword="null" />, otherwise - <see langword="false" />.
    /// </returns>
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? source)
    {
        return source?.Any(c => !char.IsWhiteSpace(c)) ?? false;
    }

    /// <summary>
    ///     Checks for the presence of a substring in the source string.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <param name="value">
    ///     The desired substring.
    /// </param>
    /// <param name="comparisonType">
    ///     String comparison mode.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the substring <paramref name="value" /> is present in the
    ///     source string, otherwise - <see langword="false" />.
    /// </returns>
    public static bool Contains(this string? source, string value, StringComparison comparisonType)
    {
        Guard.ArgumentIsNotNull(value);

        return source?.IndexOf(value, comparisonType) >= 0;
    }

    /// <summary>
    ///     Converts the string <paramref name="source" /> into a secure string object <see cref="SecureString" />.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <returns>
    ///     A string in the form of an object <see cref="SecureString" />.
    /// </returns>
    public static SecureString ToSecureString(this string source)
    {
        Guard.ArgumentIsNotNull(source);

        var result = new SecureString();
        foreach (var symbol in source)
        {
            result.AppendChar(symbol);
        }

        result.MakeReadOnly();
        return result;
    }

    /// <summary>
    ///     Cleans up the memory block in which the string <paramref name="source" /> is stored.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <remarks>
    ///     The method clears the memory block of the string if the string <paramref name="source" /> is not empty or interned.
    /// </remarks>
    public static unsafe void CleanupMemory(this string source)
    {
        if (string.IsNullOrEmpty(source) || string.IsInterned(source) != null)
        {
            return;
        }

        fixed (void* pointer = source)
        {
            new Span<char>(pointer, source.Length).Clear();
        }
    }

    /// <summary>
    ///     Adds an indentation on the left to the line with the specified indentation level.
    /// </summary>
    /// <remarks>
    ///     Indentation is added after each newline character <see cref="Environment.NewLine" />, as well
    ///     as at the beginning of the line if <paramref name="exceptFirstLine" /> is <see langword="false" />.
    /// </remarks>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <param name="indentLevel">
    ///     The indentation level (the number of tab characters).
    /// </param>
    /// <param name="exceptFirstLine">
    ///     If <see langword="true" />, the first line will not be indented.
    /// </param>
    /// <returns>
    ///     A line with added indentation on the left.
    /// </returns>
    public static string ShiftRight(this string source, int indentLevel, bool exceptFirstLine = false)
    {
        Guard.ArgumentIsNotNull(source);

        if (indentLevel == 0 || string.IsNullOrEmpty(source))
        {
            return source;
        }

        var indent = new string('\t', indentLevel);
        return (exceptFirstLine ? string.Empty : indent) + source.Replace(Environment.NewLine, Environment.NewLine + indent);
    }

    /// <summary>
    ///     Deletes the specified suffix from the end of the string.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <param name="trimString">
    ///     The line that will be deleted from the end of the source line, if it is present there.
    /// </param>
    /// <returns>
    ///     A string without the specified suffix at the end, if it was found, otherwise it is the original string.
    /// </returns>
    public static string TrimEnd(this string source, string trimString)
    {
        Guard.ArgumentIsNotNull(source);

        return string.IsNullOrEmpty(source)
                ? source
                : source.EndsWith(trimString, StringComparison.Ordinal)
                    ? source.Substring(0, source.Length - trimString.Length)
                    : source;
    }

    /// <summary>
    ///     Deletes the specified prefix from the beginning of the string.
    /// </summary>
    /// <param name="source">
    ///     The original string.
    /// </param>
    /// <param name="trimString">
    ///     The line that will be deleted from the beginning of the source line, if it is present there.
    /// </param>
    /// <returns>
    ///     A string without the specified prefix at the beginning, if it was found, otherwise it is the original string.
    /// </returns>
    public static string TrimStart(this string source, string trimString)
    {
        Guard.ArgumentIsNotNull(source);

        return string.IsNullOrEmpty(source)
            ? source
            : source.StartsWith(trimString, StringComparison.Ordinal)
                ? source.Substring(trimString.Length)
                : source;
    }
}
