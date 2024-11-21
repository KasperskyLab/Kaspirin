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
using System.Security;

namespace Kaspirin.UI.Framework.Extensions.Strings
{
    /// <summary>
    ///     Extension methods for <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
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
        ///     source string, otherwise <see langword="false" />.
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
    }
}
