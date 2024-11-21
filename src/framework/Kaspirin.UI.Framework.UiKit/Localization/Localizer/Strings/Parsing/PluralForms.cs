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
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing
{
    // Useful plural form references:
    // 1. https://www.unicode.org/cldr/charts/latest/supplemental/language_plural_rules.html
    // 2. https://docs.translatehouse.org/projects/localization-guide/en/latest/l10n/pluralforms.html

    public static class PluralForms
    {
        static PluralForms()
        {
            var otherwise = new Func<int, bool>[] { _ => true };

            var supportedPluralForms = new Dictionary<string, Func<int, bool>[]>
            {
                { "th",  new Func<int, bool>[] { } },
                { "fa",  new Func<int, bool>[] { } },
                { "id",  new Func<int, bool>[] { } },
                { "ja",  new Func<int, bool>[] { } },
                { "kk",  new Func<int, bool>[] { } },
                { "ko",  new Func<int, bool>[] { } },
                { "zh",  new Func<int, bool>[] { } },
                { "tr",  new Func<int, bool>[] { n => n < 2 } },
                { "vi",  new Func<int, bool>[] { } },

                { "bg",  new Func<int, bool>[] { n => n == 1 } },
                { "da",  new Func<int, bool>[] { n => n == 1 } },
                { "de",  new Func<int, bool>[] { n => n == 1 } },
                { "el",  new Func<int, bool>[] { n => n == 1 } },
                { "en",  new Func<int, bool>[] { n => n == 1 } },
                { "es",  new Func<int, bool>[] { n => n == 1 } },
                { "et",  new Func<int, bool>[] { n => n == 1 } },
                { "fi",  new Func<int, bool>[] { n => n == 1 } },
                { "hu",  new Func<int, bool>[] { n => n == 1 } },
                { "it",  new Func<int, bool>[] { n => n == 1 } },
                { "nl",  new Func<int, bool>[] { n => n == 1 } },
                { "no",  new Func<int, bool>[] { n => n == 1 } },
                { "nb",  new Func<int, bool>[] { n => n == 1 } },
                { "pt",  new Func<int, bool>[] { n => n == 1 } },
                { "sq",  new Func<int, bool>[] { n => n == 1 } },
                { "sv",  new Func<int, bool>[] { n => n == 1 } },
                { "ar",  new Func<int, bool>[] { n => n == 0,
                                                 n => n == 1,
                                                 n => n == 2,
                                                 n => n % 100 >= 3 && n % 100 <= 10,
                                                 n => n % 100 >= 11 && n % 100 <= 99 } },

                { "fr",     new Func<int, bool>[] { n => n < 2 } },
                { "pt-BR",  new Func<int, bool>[] { n => n < 2 } },

                { "cs", new Func<int, bool>[] { n => n == 1,
                                                n => n is >= 2 and <= 4 } },

                { "ga", new Func<int, bool>[] { n => n == 1,
                                                n => n == 2,
                                                n => n % 10 >= 3 && n % 10 <= 6,
                                                n => n % 10 >= 7 && n % 10 <= 9} },

                { "lt", new Func<int, bool>[] { n => n % 10 == 1 && n % 100 != 11,
                                                n => n % 10 >= 2 && (n % 100 < 10 || n % 100 >= 20)} },

                { "lv", new Func<int, bool>[] { n => n % 10 == 1 && n % 100 != 11,
                                                n => n == 0} },

                { "mk", new Func<int, bool>[] { n => n % 10 == 1,
                                                n => n % 10 == 2} },

                { "pl", new Func<int, bool>[] { n => n == 1,
                                                n => n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10  || n % 100 > 20)} },

                { "ro", new Func<int, bool>[] { n => n == 1,
                                                n => n == 0 || (n % 100 > 0 && n % 100 < 20)} },

                { "ru", new Func<int, bool>[] { n => n % 10 == 1 && n % 100 != 11,
                                                n => n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 > 20)} },

                { "uk", new Func<int, bool>[] { n => n % 10 == 1 && n % 100 != 11,
                                                n => n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 > 20)} },

                { "sr", new Func<int, bool>[] { n => n % 10 == 1 && n % 100 != 11,
                                                n => n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)} },

                { "sl", new Func<int, bool>[] { n => n == 1,
                                                n => n % 100 == 2,
                                                n => n % 100 == 3 || n % 100 == 4} }
            };

            _rules = supportedPluralForms.ToDictionary(
                kv => new CultureInfo(kv.Key),
                kv => kv.Value.Concat(otherwise).ToArray());

            _rules.Add(CultureInfo.InvariantCulture, otherwise);
        }

        public static int GetFormCount(CultureInfo cultureInfo)
        {
            return GetRules(cultureInfo).Length;
        }

        public static int GetFormNumber(CultureInfo cultureInfo, int discriminator)
        {
            return GetRules(cultureInfo).TakeWhile(predicate => !predicate(discriminator)).Count();
        }

        public static ImmutableArray<string> GetSupportedCulturesList() => _rules.Keys.Select(x => x.Name).ToImmutableArray();

        private static Func<int, bool>[] GetRules(CultureInfo cultureInfo)
        {
            var rules = cultureInfo.GetParentCultures()
                .Where(culture => _rules.ContainsKey(culture))
                .Select(culture => _rules[culture])
                .FirstOrDefault();

            return rules switch
            {
                null => throw new KeyNotFoundException($"Plural form rules not found for '{cultureInfo}' culture."),
                _ => rules
            };
        }

        private static readonly IDictionary<CultureInfo, Func<int, bool>[]> _rules;
    }
}
