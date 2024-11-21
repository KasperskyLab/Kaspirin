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
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Exceptions
{
    public sealed class ExpectTokensException : SyntaxErrorException
    {
        public ExpectTokensException(Token actualToken, TokenType expectTokenType)
        {
            ExpectTokenTypes = new[] { expectTokenType };
            CurrentToken = actualToken;
        }

        public ExpectTokensException(Token actualToken, params TokenType[] expectTokenTypes)
        {
            if (expectTokenTypes.Length < 1)
                throw new ArgumentException("expectTokenTypes.Length must be great then 1");

            ExpectTokenTypes = expectTokenTypes;
            CurrentToken = actualToken;
        }

        public override Position Position => CurrentToken.Position;
        public IEnumerable<TokenType> ExpectTokenTypes { get; }
        public Token CurrentToken { get; }

        public override string ErrorMessage
        {
            get
            {
                var expects = ExpectTokenTypes.Select(x => x switch
                {
                    TokenType.Eof => "End Of File",
                    TokenType.Identifier => "Identifier",
                    TokenType.StringLiteral => "String Literal",
                    TokenType.Dollar => "'$'",
                    TokenType.Plus => "'+'",
                    TokenType.Colon => "':'",
                    TokenType.WhiteSpace => "space",
                    TokenType.NewNile => "New Nile",
                    TokenType.Equals => "'='",
                    TokenType.Comment => "Comment",
                    TokenType.OpeningCurlyBrace => "'{'",
                    TokenType.ClosingCurlyBrace => "'}'",
                    TokenType.Comma => "','",
                    _ => throw new IndexOutOfRangeException($"Unknown TokenType: '{x}'"),
                });
                var position = CurrentToken.Position;
                return $"Expected: {string.Join(", ", expects)} but found '{position.GetText()}'\r\n{position.GetStartLineTextWithPointer()}";
            }
        }
    }
}