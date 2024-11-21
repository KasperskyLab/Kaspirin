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
using System.Globalization;

using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Exceptions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing
{
    public static class Lexer
    {
        public static IEnumerable<Token> Lex(string sourceText, string fileName = "<unknown>", bool skipWhiteSpace = true, int startOffset = 0)
        {
            bool isWhiteSpace(char ch) => ch is ' ' or '\t' or '\v' or '\f'
                || CharUnicodeInfo.GetUnicodeCategory(ch) == UnicodeCategory.SpaceSeparator;
            bool isNewLine(char ch) => ch is '\r' or '\n' or '\u2028' or '\u2029';

            for (int i = 0; i < sourceText.Length; i++)
            {
                void skipNewLine()
                {
                    while (i < sourceText.Length && isNewLine(sourceText[i]))
                    {
                        i++;
                    }
                }

                var ch = sourceText[i];

                Token parseIdentifier()
                {
                    var start = startOffset + i;
                    i++;
                    while (i < sourceText.Length && (char.IsLetterOrDigit(ch = sourceText[i]) || ch is '_' or '.' or '-'))
                    {
                        i++;
                    }

                    var end = startOffset + i;
                    i--;
                    return new Token(TokenType.Identifier, start, end, fileName, sourceText);
                }

                switch (ch)
                {
                    case ',':
                        yield return new Token(TokenType.Comma,
                            startPosition: startOffset + i,
                            endPosition: startOffset + i + 1,
                            fileName: fileName,
                            sourceText);
                        break;

                    case '{':
                        yield return new Token(TokenType.OpeningCurlyBrace,
                            startPosition: startOffset + i,
                            endPosition: startOffset + i + 1,
                            fileName: fileName,
                            sourceText);
                        break;

                    case '}':
                        yield return new Token(TokenType.ClosingCurlyBrace,
                            startPosition: startOffset + i,
                            endPosition: startOffset + i + 1,
                            fileName: fileName,
                            sourceText);
                        break;

                    case '=':
                        yield return new Token(TokenType.Equals,
                            startPosition: startOffset + i,
                            endPosition: startOffset + i + 1,
                            fileName: fileName,
                            sourceText);
                        break;

                    case '$':
                        yield return new Token(TokenType.Dollar,
                            startPosition: startOffset + i,
                            endPosition: startOffset + i + 1,
                            fileName: fileName,
                            sourceText);
                        break;

                    case '+':
                        yield return new Token(TokenType.Plus,
                            startPosition: startOffset + i,
                            endPosition: startOffset + i + 1,
                            fileName: fileName,
                            sourceText);
                        break;

                    case ':':
                        yield return new Token(TokenType.Colon,
                            startPosition: startOffset + i,
                            endPosition: startOffset + i + 1,
                            fileName: fileName,
                            sourceText);
                        break;

                    case ';':
                        {
                            var start = startOffset + i;
                            i++;
                            for (; i < sourceText.Length && sourceText[i] != '\n';)
                            {
                                i++;
                            }

                            yield return new Token(TokenType.Comment,
                                startPosition: start,
                                endPosition: startOffset + i,
                                fileName: fileName,
                                sourceText);
                            i--;
                            break;
                        }

                    case '\'':
                        {
                            var start = startOffset + i;
                            i++;
                            for (; i < sourceText.Length && (ch = sourceText[i]) != '\'';)
                            {
                                if (ch == '\\')
                                {
                                    i++;
                                }

                                i++;
                            }

                            if (i >= sourceText.Length)
                            {
                                throw new ExpectedSymbolException('\'', startOffset + i, fileName, sourceText);
                            }

                            if (sourceText[i] != '\'')
                            {
                                throw new ExpectedSymbolException('\'', startOffset + i, fileName, sourceText);
                            }

                            yield return new Token(TokenType.StringLiteral,
                                startPosition: start,
                                endPosition: startOffset + i + 1,
                                fileName: fileName,
                                sourceText);
                            break;
                        }

                    case '_':
                    case >= 'a' and <= 'b':
                    case >= 'A' and <= 'B':
                        yield return parseIdentifier();
                        break;

                    case '\r':
                    case '\n':
                    case '\u2028': /*  line separator       */
                    case '\u2029': /*  paragraph separator  */
                        {
                            var start = startOffset + i;
                            i++;
                            skipNewLine();
                            yield return new Token(TokenType.NewNile, start, i, fileName, sourceText);
                            i--;
                            break;
                        }

                    case ' ':
                    case '\u00A0':
                    case '\t':
                    case var _ when isWhiteSpace(ch):
                        {
                            var start = startOffset + i;
                            i++;
                            while (i < sourceText.Length && isWhiteSpace(sourceText[i]))
                            {
                                i++;
                            }

                            var end = startOffset + i;
                            i--;
                            if (!skipWhiteSpace)
                            {
                                yield return new Token(TokenType.WhiteSpace, start, end, fileName, sourceText);
                            }

                            break;
                        }

                    case var _ when char.IsLetter(ch):
                        yield return parseIdentifier();
                        break;

                    default: throw new UnexpectedSymbolException(i, fileName, sourceText);
                }
            }

            yield return new Token(TokenType.Eof, sourceText.Length, sourceText.Length, fileName, sourceText);
        }
    }
}
