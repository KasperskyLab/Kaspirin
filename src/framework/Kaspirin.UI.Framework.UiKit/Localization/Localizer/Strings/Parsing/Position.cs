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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing
{
    public readonly struct Position
    {
        public Position(int start, int end, string fileName, string sourceText)
        {
            Guard.Argument(start >= 0, $"start ({start}) < 0");
            Guard.Argument(end >= 0, $"end ({end}) < 0");
            Guard.Argument(start <= end, $"start ({start}) > end ({end})");

            Start = start;
            End = end;
            FileName = fileName;
            SourceText = sourceText;
        }

        public static Position operator +(Position a, Position b)
        {
            Guard.Assert(a.FileName == b.FileName, "a.FileName == b.FileName");
            Guard.Assert(object.ReferenceEquals(a.SourceText, b.SourceText), "object.ReferenceEquals(a.SourceText, b.SourceText)");

            return new Position(Math.Min(a.Start, b.Start), Math.Max(a.End, b.End), a.FileName, a.SourceText);
        }

        public int Length => End - Start;
        public int Start { get; }
        public int End { get; }
        public string FileName { get; }
        public string SourceText { get; }
        public LineColumn StartLineColumn => ToLineColumn(Start);
        public LineColumn EndLineColumn => ToLineColumn(End);

        public string GetText(string sourceText) => sourceText.Substring(Start, Length);

        public string GetText() => SourceText.Substring(Start, Length);

        public LineColumn ToLineColumn(int pos) => ToLineColumn(SourceText, pos);

        public static LineColumn ToLineColumn(string sourceText, int pos)
        {
            Guard.Argument(pos <= sourceText.Length, $"The position {pos} greater then source text length ({sourceText.Length}).");

            var line = 1;
            var column = 1;
            for (var i = 0; i < sourceText.Length; i++, column++)
            {
                var ch = sourceText[i];
                if (ch == '\n')
                {
                    line++;
                    column = 0;
                }

                if (i == pos)
                {
                    return new LineColumn(line, column);
                }
            }

            return new LineColumn(line, column);
        }

        public string GetStartLineTextWithPointer()
        {
            var lineColumn = StartLineColumn;
            var indent = new string(' ', lineColumn.Column - 1);
            var lineText = GetLineText(lineColumn.Line);

            return $"{lineText}\r\n{indent}^";
        }

        public string GetLineText(int line) => GetLineText(SourceText, line);

        public static string GetLineText(string sourceText, int line)
        {
            Guard.Argument(line >= 1, $"The {nameof(line)} argument must be greater than or equal to 1 but equal to {line}.");

            var currentLine = 1;
            var startIndex = 0;
            var length = 0;

            for (var i = 0; i < sourceText.Length; i++)
            {
                var ch = sourceText[i];
                if (ch == '\n')
                {
                    currentLine++;
                    if (line + 1 == currentLine)
                    {
                        return sourceText.Substring(startIndex, length);
                    }

                    length = 0;
                    startIndex = i + 1;
                    continue;
                }

                if (ch != '\r')
                {
                    length++;
                }
            }

            Guard.Assert(line <= currentLine, $"The {nameof(line)} argument greater than number of line in source. It must be less or equal to {currentLine} but equal to {line}.");

            return sourceText.Substring(startIndex);
        }

        public override string ToString() => ToString(SourceText);

        public string ToString(string sourceText)
        {
            var text = sourceText.Substring(Start, Length);
            return $"{StartLineColumn.Line}:{StartLineColumn.Column} ({Length}): \"{text}\" FileName: '{FileName}'";
        }
    }
}