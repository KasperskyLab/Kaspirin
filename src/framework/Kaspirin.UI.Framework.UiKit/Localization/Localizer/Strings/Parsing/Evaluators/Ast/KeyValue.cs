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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Evaluators.Ast
{
    public sealed class KeyValue : Item
    {
        public KeyValue(Token key, Token equalsLiteral, Expression expression, Token? comment, Token? newLine)
        {
            Key = key;
            EqualsLiteral = equalsLiteral;
            Expression = expression;
            Comment = comment;
            NewLine = newLine;
        }

        public Token Key { get; }
        public Token EqualsLiteral { get; }
        public Expression Expression { get; }
        public Token? Comment { get; }
        public Token? NewLine { get; }
        public override string GetText() => $"{Key.GetText()} = {Expression.GetText()}{GetTailText()}";
        public override string ToString()
        {
            var startLineColumn = Key.Position.StartLineColumn;
            return $"{startLineColumn.Line}:{startLineColumn.Column}: {GetText()}";
        }

        private string GetTailText() => (Comment.HasValue ? " " + Comment.Value.GetText() : "") + (NewLine.HasValue ? NewLine.Value.GetText() : "\r\n");
    }
}