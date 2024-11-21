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
using System.IO;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Input.Filter
{
    public static class InputFilters
    {
        public static IInputFilter Digits { get; } = new DelegateInputFilter(
            input => FilterString(input, c => char.IsDigit(c)));

        public static IInputFilter DigitsAndSpaces { get; } = new DelegateInputFilter(
            input => FilterString(input, c => char.IsDigit(c) || c == ' '));

        public static IInputFilter Letters { get; } = new DelegateInputFilter(
            input => FilterString(input, c => char.IsLetter(c)));

        public static IInputFilter LettersAndSpaces { get; } = new DelegateInputFilter(
            input => FilterString(input, c => char.IsLetter(c) || c == ' '));

        public static IInputFilter LettersAndDigits { get; } = new DelegateInputFilter(
            input => FilterString(input, c => char.IsLetterOrDigit(c)));

        public static IInputFilter LettersAndDigitsAndSpaces { get; } = new DelegateInputFilter(
            input => FilterString(input, c => char.IsLetterOrDigit(c) || c == ' '));

        public static IInputFilter FileNameChars { get; } = new DelegateInputFilter(input =>
        {
            var invalidChars = Path.GetInvalidFileNameChars();

            return FilterString(input, c => !invalidChars.Contains(c));
        });

        public static IInputFilter DirectoryNameChars { get; } = new DelegateInputFilter(input =>
        {
            var pathSeparators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };

            var invalidPathChars = Path.GetInvalidPathChars()
                .Concat(pathSeparators)
                .ToArray();

            return FilterString(input, c => !invalidPathChars.Contains(c));
        });

        public static string? FilterString(string str, Func<char, bool> predicate)
        {
            if (str.Length == 0)
            {
                return string.Empty;
            }

            var charArray = default(char[]);
            try
            {
                charArray = str.Where(predicate).ToArray();
                return new string(charArray);
            }
            finally
            {
                if (charArray is not null)
                {
                    Array.Clear(charArray, 0, charArray.Length);
                }
            }
        }
    }
}
