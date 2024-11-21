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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions
{
    public class LiteralExpression : IExpression
    {
        public LiteralExpression(string effectiveValue, Func<string, string> literalNormalizationFunc)
        {
            _effectiveValue = effectiveValue;
            _literalNormalizationFunc = literalNormalizationFunc;
        }

        public string Literal => _literal ??= _literalNormalizationFunc(_effectiveValue);

        public string Resolve() => Literal;

        private readonly string _effectiveValue;
        private readonly Func<string, string> _literalNormalizationFunc;

        private string? _literal;
    }
}
