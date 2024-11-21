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
using System.Globalization;
using System.IO;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions
{
    public class PluralExpression : IExpression
    {
        public PluralExpression(VariableExpression selector, ValueExpression[] pluralForms, CultureInfo cultureInfo)
        {
            Selector = selector;
            PluralForms = pluralForms;
            _cultureInfo = cultureInfo;
        }

        public VariableExpression Selector { get; }

        public ValueExpression[] PluralForms { get; }

        public string Resolve(Func<string, ValueExpression> keyResolver, Func<string, object?> variableResolver)
        {
            var result = Selector.Resolve(variableResolver);
            var formNumber = Parsing.PluralForms.GetFormNumber(_cultureInfo, Convert.ToInt32(result));

            if (formNumber >= PluralForms.Length)
            {
                var formCount = Parsing.PluralForms.GetFormCount(_cultureInfo);
                var forms = string.Join(", ", PluralForms.Select(x => "'" + x.Resolve(keyResolver, variableResolver) + "'"));
                throw new InvalidDataException(
                    $"Invalid plural form count for '{_cultureInfo}' culture: expected {formCount}, got {PluralForms.Length}. {{${Selector.Name}: {forms}}}");
            }

            return PluralForms[formNumber].Resolve(keyResolver, variableResolver);
        }

        private readonly CultureInfo _cultureInfo;
    }
}
