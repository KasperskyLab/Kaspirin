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
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal static class LineEndingHelper
    {
        public static string NormalizeLineEndings(string value, LineEndingMode mode)
            => _lineEndingsRegex.Replace(value, GetLineEnding(mode));

        private static string GetLineEnding(LineEndingMode mode)
        {
            return mode switch
            {
                LineEndingMode.CrLf => CrLfLineEnding,
                LineEndingMode.Lf => LfLineEnding,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported line ending mode"),
            };
        }

        private static readonly Regex _lineEndingsRegex = new(@"\r\n|\n\r|\n|\r", RegexOptions.Compiled);

        private const string CrLfLineEnding = "\r\n";
        private const string LfLineEnding = "\n";
    }
}
