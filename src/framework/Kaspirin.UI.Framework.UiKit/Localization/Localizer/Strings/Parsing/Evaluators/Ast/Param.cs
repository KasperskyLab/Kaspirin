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
    public sealed class Param : Operand
    {
        public Param(Token dollar, Token identifier)
        {
            Dollar = dollar;
            Identifier = identifier;
        }

        public override Position Position => Dollar.Position + Identifier.Position;
        public Token Dollar { get; }
        public Token Identifier { get; }
        public override string GetText() => Dollar.Position.GetText() + Identifier.Position.GetText();
    }
}