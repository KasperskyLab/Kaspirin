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
using System.Linq;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions;

using StringExtensions = Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Extensions.StringExtensions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Evaluators.ValueExpression
{
    using TResult = KeyValuePair<string, Expressions.ValueExpression>;

    public sealed class ValueExpressionEvaluator : IKeyValueEvaluator<TResult, IExpression, VariableExpression, IExpression, IExpression>
    {
        public ValueExpressionEvaluator(CultureInfo cultureInfo) => _cultureInfo = cultureInfo;

        public TResult KeyValue(Token key, Token equals, IExpression expression, Token? comment, Token? newLine) => new(key.GetText(), (Expressions.ValueExpression)expression);

        public bool ToItem(Token comment, Token newLine, out TResult result)
        {
            result = default;
            return false;
        }

        public IExpression StringLiteral(Token literal)
        {
            var p = literal.Position;
            return new LiteralExpression(p.SourceText.Substring(p.Start + 1, p.Length - 2), StringExtensions.LiteralUnescape);
        }

        public VariableExpression Param(Token dollar, Token identifier) => new VariableExpression(name: identifier.GetText());

        public IExpression KeyRef(Token identifier) => new KeyExpression(identifier.GetText());

        public IExpression PluralForm(
            Token openingCurlyBrace,
            VariableExpression param,
            Token colon,
            IEnumerable<IExpression> pluralFormExpressions,
            Token closingCurlyBrace) =>
            new PluralExpression(
                selector: param,
                pluralForms: pluralFormExpressions.Take(PluralForms.GetFormCount(_cultureInfo)).Cast<Expressions.ValueExpression>().ToArray(),
                cultureInfo: _cultureInfo);

        public IExpression PluralFormExpression(IEnumerable<IExpression> operands) => new Expressions.ValueExpression(operands.ToArray());

        public IExpression Expression(IEnumerable<IExpression> operands) => new Expressions.ValueExpression(operands.ToArray());

        private readonly CultureInfo _cultureInfo;
    }
}