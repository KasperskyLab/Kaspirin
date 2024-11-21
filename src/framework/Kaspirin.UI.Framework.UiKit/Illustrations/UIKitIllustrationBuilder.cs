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
using System.Linq;
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Illustrations
{
    internal sealed class UIKitIllustrationBuilder
    {
        public UIKitIllustrationBuilder(Enum illustration)
        {
            Illustration = illustration;
            IllustrationName = illustration.ToString();
            IllustrationKey = _illustrationKeyTemplate.Replace(NameMask, IllustrationName);
            IllustrationScope = illustration.GetType().Name.Replace(ScopePrefix, string.Empty);
        }

        public UIKitIllustrationBuilder(string key, string scope, string assemblyName)
            : this(GetEnumValueFromAssembly(key, scope, assemblyName))
        {
        }

        private static Enum GetEnumValueFromAssembly(string key, string scope, string assemblyName)
        {
            var targetAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => string.Equals(a.GetName().Name, assemblyName, StringComparison.OrdinalIgnoreCase))
                .GuardedSingleOrDefault();

            Guard.ArgumentIsNotNull(targetAssembly);

            var targetEnumType = targetAssembly
                .GetTypes()
                .Where(t => t.IsEnum && string.Equals(t.Name, ScopePrefix + scope, StringComparison.OrdinalIgnoreCase))
                .GuardedSingleOrDefault();

            Guard.ArgumentIsNotNull(targetEnumType);

            var targetEnumValue = Enum
                .GetValues(targetEnumType)
                .Cast<Enum>()
                .Where(value => string.Equals(value.ToString(), _illustrationKeyRegex.Match(key).Groups[1].Value, StringComparison.OrdinalIgnoreCase))
                .GuardedSingleOrDefault();

            Guard.ArgumentIsNotNull(targetEnumValue);

            return targetEnumValue;
        }

        public Enum Illustration { get; }
        public string IllustrationName { get; }
        public string IllustrationKey { get; }
        public string IllustrationScope { get; }

        private const string NameMask = "%NAME%";
        private const string ScopePrefix = "UIKitIllustration_";

        private static readonly string _illustrationKeyTemplate = $"UIKitIllustration_{NameMask}.svg";
        private static readonly Regex _illustrationKeyRegex = new($"UIKitIllustration_(.*).svg", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}
