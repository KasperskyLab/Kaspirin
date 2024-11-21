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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Extensions
{
    internal static class StringExtensions
    {
        public const char BorderChar = '\'';
        
        public static string ReplaceAll(this string target, KeyValuePair<string, string>[] replacementMap)
        {
            foreach (var x in replacementMap)
            {
                if (target.IndexOf(x.Key, StringComparison.Ordinal) >= 0)
                {
                    var builder = new StringBuilder(target);
                    foreach (var item in replacementMap)
                        builder.Replace(item.Key, item.Value);
                    return builder.ToString();
                }
            }

            return target;
        }

        public static string Unescape(this string text)
        {
            if (text.IndexOf('\\') < 0)
                return text;
            
            var builder = new StringBuilder(text.Length);
            
            for (int i = 0; i < text.Length; i++)
            {
                var ch = text[i]; 
                if (ch == '\\' && i + 1 < text.Length)
                    builder.Append(text[++i] switch { 'r' => '\r', 'n' => '\n', 't' => '\t', var ch2 => ch2 });
                else
                    builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string LiteralUnescape(this string value) => value.Unescape().ReplaceAll(CharReplacementMap);

        private static readonly KeyValuePair<string, string>[] CharReplacementMap = 
        {
            new("&nbsp;", "\x00a0"),
            new("&lrm;",  "\x200e"),
            new("&rlm;",  "\x200f"),
        };
    }
}
