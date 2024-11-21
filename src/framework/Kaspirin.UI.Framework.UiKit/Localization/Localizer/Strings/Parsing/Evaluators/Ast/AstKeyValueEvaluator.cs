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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Evaluators.Ast
{
    public sealed class AstKeyValueEvaluator : IKeyValueEvaluator<Item, Expression, Param, PluralFormExpression, Operand>
    {
        public Item KeyValue(Token key, Token equals, Expression expression, Token? comment, Token? newLine) =>
            new KeyValue(key, equals, expression, comment, newLine);
        public bool ToItem(Token comment, Token newLine, out Item result)
        {
            result = new Comment(comment, newLine);
            return true;
        }

        public Operand StringLiteral(Token literal) => new StringLiteral(literal);

        public Param Param(Token dollar, Token identifier) => new(dollar, identifier);

        public Operand KeyRef(Token identifier) => new KeyRef(identifier);

        public Operand PluralForm(
            Token openingCurlyBrace,
            Param param, Token colon,
            IEnumerable<PluralFormExpression> pluralFormExpressions,
            Token closingCurlyBrace) =>
            new PluralForm(openingCurlyBrace, param, colon, pluralFormExpressions, closingCurlyBrace);

        public PluralFormExpression PluralFormExpression(IEnumerable<Operand> operands) => new(operands);

        public Expression Expression(IEnumerable<Operand> operands) => new(operands);
    }
}