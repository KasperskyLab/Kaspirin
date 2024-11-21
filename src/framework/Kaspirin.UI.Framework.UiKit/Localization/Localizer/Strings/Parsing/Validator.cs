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
using System.IO;
using System.Linq;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Evaluators.Ast;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Exceptions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing
{
    public static class Validator
    {
        public static void ValidateCommonResources(Dictionary<string, IEnumerable<Operand>> commonDictionary, CultureInfo cultureInfo) =>
            ValidateResources(dictionary: commonDictionary, parentDictionary: new Dictionary<string, IEnumerable<Operand>>(), cultureInfo);

        public static void ValidateScopeResources(
            Dictionary<string, IEnumerable<Operand>> scopeDictionary,
            Dictionary<string, IEnumerable<Operand>> commonDictionary,
            CultureInfo cultureInfo) =>
            ValidateResources(scopeDictionary, commonDictionary, cultureInfo);

        private static void ValidateResources(
            Dictionary<string, IEnumerable<Operand>> dictionary,
            Dictionary<string, IEnumerable<Operand>> parentDictionary,
            CultureInfo cultureInfo)
        {
            void validateResolveChain(string key, List<string> resolvedKeys)
            {
                if (resolvedKeys.Contains(key))
                {
                    var first = resolvedKeys.First();
                    var loopedKeys = resolvedKeys.TakeWhile(x => x != key).ToList();
                    loopedKeys.Add(key);
                    throw new InvalidDataException(message:
                        $@"Cyclic reference found: {string.Join("->", loopedKeys)}; ""->"")->{first}");
                }
            }

            void validateKeyRef(string parentKey, string keyRefValue, Position position)
            {
                if (!(dictionary.ContainsKey(keyRefValue) || parentDictionary.ContainsKey(keyRefValue)))
                {
                    throw new ValidationErrorException(position, $"Unable to resolve key reference '{keyRefValue}' in '{parentKey}' key.");
                }
            }

            void validatePluralForm(string key, int operandCount, Position position)
            {
                if (!cultureInfo.Equals(CultureInfo.InvariantCulture) && PluralForms.GetFormCount(cultureInfo) != operandCount)
                {
                    throw new ValidationErrorException(
                        position,
                        $"Invalid plural form count for '{cultureInfo}' culture in key '{key}': expected {PluralForms.GetFormCount(cultureInfo)}, got {operandCount}.");
                }
            }

            void validateOperand(string key, Operand operand, List<string> resolvedKeys, IList<string> validatedKeys)
            {
                switch (operand)
                {
                    case KeyRef keyRef:
                        var keyRefValue = keyRef.Identifier.GetText();
                        validateKeyRef(key, keyRefValue, keyRef.Position);
                        resolvedKeys.Add(key);
                        validateKey(keyRefValue, resolvedKeys, validatedKeys);
                        break;

                    case PluralForm pluralForm:
                        validatePluralForm(key, pluralForm.PluralFormExpressions.Count(), pluralForm.ClosingCurlyBrace.Position);
                        break;
                }
            }

            void validateKey(string key, List<string> resolvedKeys, IList<string> validatedKeys)
            {
                validateResolveChain(key, resolvedKeys);

                if (validatedKeys.Contains(key) || parentDictionary.ContainsKey(key))
                {
                    return;
                }

                foreach (var operand in dictionary[key])
                {
                    validateOperand(key, operand, resolvedKeys, validatedKeys);
                }

                validatedKeys.Add(key);
            }

            foreach (var key in dictionary.Keys)
            {
                validateKey(key, resolvedKeys: new List<string>(), validatedKeys: new List<string>());
            }
        }
    }
}