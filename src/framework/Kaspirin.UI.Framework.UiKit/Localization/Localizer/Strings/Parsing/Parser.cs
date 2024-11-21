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
using System.Diagnostics.CodeAnalysis;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Exceptions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing
{
    public static class Parser
    {
        #region Inteface metods

        public static IEnumerable<TItem>
            ParseScope<TItem, TExpression, TParam, TPluralFormExpression, TOperand>(
                string sourceText,
                IKeyValueEvaluator<TItem, TExpression, TParam, TPluralFormExpression, TOperand> evaluator,
                string filePath = "<unknown>",
                bool skipWhiteSpace = true,
                int startOffset = 0)
            where TParam : TOperand
            => new ParserImpl<TItem, TExpression, TParam, TPluralFormExpression, TOperand>(
                    tokens: Lexer.Lex(sourceText, fileName: filePath, skipWhiteSpace, startOffset), evaluator, skipWhiteSpace)
                    .ExpectScope();

        public static TExpression ParseExpression<TItem, TExpression, TParam, TPluralFormExpression, TOperand>(
                string sourceText,
                IKeyValueEvaluator<TItem, TExpression, TParam, TPluralFormExpression, TOperand> evaluator,
                string filePath = "<unknown>",
                bool skipWhiteSpace = true,
                int startOffset = 0)
            where TParam : TOperand
            => new ParserImpl<TItem, TExpression, TParam, TPluralFormExpression, TOperand>(
                    tokens: Lexer.Lex(sourceText, fileName: filePath, skipWhiteSpace, startOffset), evaluator, skipWhiteSpace: true)
                    .ExpectExpression();

        #endregion

        private sealed class ParserImpl<TItem, TExpression, TParam, TPluralFormExpression, TOperand>
            where TParam : TOperand
        {
            public ParserImpl(IEnumerable<Token> tokens,
                IKeyValueEvaluator<TItem, TExpression, TParam, TPluralFormExpression, TOperand> evaluator,
                bool skipWhiteSpace)
            {
                _tokens = tokens.GetEnumerator();
                _evaluator = evaluator;
                _skipWhiteSpace = skipWhiteSpace;
                _isEof = !_tokens.MoveNext();
            }

            private Token Current => _tokens.Current;

            public IEnumerable<TItem> ExpectScope()
            {
                Accept(TokenType.NewNile);

                while (true)
                {
                    if (AcceptKeyValue(out var keyValue))
                    {
                        yield return keyValue!;
                    }
                    else if (Accept(TokenType.Comment))
                    {
                        var comment = _acceptToken;
                        Accept(TokenType.NewNile);
                        if (_evaluator.ToItem(comment, _acceptToken, out var item))
                        {
                            yield return item;
                        }
                    }
                    else if (Accept(TokenType.NewNile))
                    {
                        // Skip
                    }
                    else
                    {
                        break;
                    }
                }

                Expect(TokenType.Eof);
            }

            public TExpression ExpectExpression()
            {
                var operand = ExpectOperand();
                var operands = new List<TOperand> { operand };

                while (Accept(TokenType.Plus))
                {
                    operands.Add(ExpectOperand());
                }

                return _evaluator.Expression(operands);
            }

            private bool AcceptKeyValue([NotNullWhen(returnValue: true)] out TItem? keyValue)
            {
                if (!Accept(TokenType.Identifier))
                {
                    keyValue = default!;
                    return false;
                }

                var key = _acceptToken;
                var equals = Expect(TokenType.Equals);
                var expression = ExpectExpression();
                Token? comment = Accept(TokenType.Comment) ? _acceptToken : null;
                Token? newLine = Accept(TokenType.NewNile) ? _acceptToken : null;
                keyValue = _evaluator.KeyValue(key, equals, expression, comment, newLine)!;
                return true;
            }

            private TOperand ExpectOperand()
            {
                if (AcceptStringLiteral(out var operand) || AcceptParam(out operand) || AcceptKeyRef(out operand) ||
                    AcceptPluralForm(out operand))
                {
                    return operand!;
                }

                throw new ExpectTokensException(Current, TokenType.Identifier, TokenType.StringLiteral,
                    TokenType.Dollar, TokenType.OpeningCurlyBrace);
            }

            private bool AcceptPluralForm([NotNullWhen(returnValue: true)] out TOperand? operand)
            {
                // '{' param ':'  pluralFormExpression (',' pluralFormExpression)* '}'
                if (!Accept(TokenType.OpeningCurlyBrace))
                {
                    operand = default!;
                    return false;
                }

                var openingCurlyBrace = _acceptToken;
                var param = ExpectParam();
                var colon = Expect(TokenType.Colon);
                var result = new List<TPluralFormExpression> { ExpectPluralFormExpression() };

                while (Accept(TokenType.Comma))
                {
                    result.Add(ExpectPluralFormExpression());
                }

                var closingCurlyBrace = Expect(TokenType.ClosingCurlyBrace);

                operand = _evaluator.PluralForm(openingCurlyBrace, param, colon, result, closingCurlyBrace)!;
                return true;
            }

            private TPluralFormExpression ExpectPluralFormExpression()
            {
                if (AcceptParam(out var operand) || AcceptStringLiteral(out operand))
                {
                    var operands = new List<TOperand> { operand };
                    if (Accept(TokenType.Plus))
                    {
                        if (AcceptParam(out operand) || AcceptStringLiteral(out operand))
                        {
                            operands.Add(operand);
                        }
                        else
                        {
                            throw new ExpectTokensException(Current, TokenType.Dollar, TokenType.StringLiteral);
                        }
                    }

                    return _evaluator.PluralFormExpression(operands);
                }

                throw new ExpectTokensException(Current, TokenType.Dollar, TokenType.StringLiteral);
            }

            private TParam ExpectParam()
            {
                var dollar = Expect(TokenType.Dollar);
                var identifier = Expect(TokenType.Identifier);
                return _evaluator.Param(dollar, identifier);
            }

            private bool AcceptKeyRef([NotNullWhen(returnValue: true)] out TOperand? operand)
            {
                if (!Accept(TokenType.Identifier))
                {
                    operand = default!;
                    return false;
                }

                operand = _evaluator.KeyRef(_acceptToken)!;
                return true;
            }

            private bool AcceptParam([NotNullWhen(returnValue: true)] out TOperand? operand)
            {
                if (!Accept(TokenType.Dollar))
                {
                    operand = default!;
                    return false;
                }

                var dollar = _acceptToken;
                Expect(TokenType.Identifier);
                operand = _evaluator.Param(dollar, _acceptToken)!;
                return true;
            }

            private bool AcceptStringLiteral([NotNullWhen(returnValue: true)] out TOperand? operand)
            {
                if (!Accept(TokenType.StringLiteral))
                {
                    operand = default!;
                    return false;
                }

                operand = _evaluator.StringLiteral(_acceptToken)!;
                return true;
            }

            private bool Accept(TokenType tokenType)
            {
                Guard.Assert(!_isEof, "!_isEof");

                if (Current.Type != tokenType)
                {
                    return false;
                }

                _acceptToken = Current;
                _isEof = !_tokens.MoveNext();

                return true;
            }

            private Token Expect(TokenType tokenType)
            {
                if (!Accept(tokenType))
                {
                    throw new ExpectTokensException(actualToken: Current, expectTokenType: tokenType);
                }

                return _acceptToken;
            }

            private Token _acceptToken;
            private bool _isEof;

            private readonly IKeyValueEvaluator<TItem, TExpression, TParam, TPluralFormExpression, TOperand> _evaluator;
            private readonly bool _skipWhiteSpace;
            private readonly IEnumerator<Token> _tokens;
        }
    }
}