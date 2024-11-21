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
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Evaluators.Ast
{
    public sealed class PluralForm : Operand
    {
        public PluralForm(Token openingCurlyBrace, Param param, Token colon, IEnumerable<PluralFormExpression> pluralFormExpressions, Token closingCurlyBrace)
        {
            OpeningCurlyBrace = openingCurlyBrace;
            Param = param;
            Colon = colon;
            PluralFormExpressions = pluralFormExpressions;
            ClosingCurlyBrace = closingCurlyBrace;
        }

        public override Position Position => OpeningCurlyBrace.Position + ClosingCurlyBrace.Position;
        public override string GetText() => $"{{{Param.GetText()}: {string.Join(", ", PluralFormExpressions.Select(x => x.GetText()))}}}";

        public Token OpeningCurlyBrace { get; }
        public Param Param { get; }
        public Token Colon { get; }
        public IEnumerable<PluralFormExpression> PluralFormExpressions { get; }
        public Token ClosingCurlyBrace { get; }
    }
}