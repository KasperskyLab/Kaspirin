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
using System.Globalization;
using System.Text;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions;

using IResourceProvider = Kaspirin.UI.Framework.UiKit.Localization.LocResources.IResourceProvider;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings
{
    public sealed class StringScope : IScope
    {
        public StringScope(Uri scopeUri, IResourceProvider resourceProvider, CultureInfo cultureInfo)
        {
            Guard.ArgumentIsNotNull(scopeUri);
            Guard.ArgumentIsNotNull(resourceProvider);
            Guard.ArgumentIsNotNull(cultureInfo);

            var scopeText = Encoding.UTF8.GetString(resourceProvider.ReadResource(scopeUri)).TrimStart(Bom);
            var scopeResources = DictionaryBuilder.BuildScope(
                scopeText,
                cultureInfo,
                filePath: scopeUri.ToString());

            ScopeUri = scopeUri;
            Keys = scopeResources.Keys;

            _resources = scopeResources;
        }

        public Uri ScopeUri { get; }

        public IEnumerable<string> Keys { get; }

        public object GetValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            return _resources[key];
        }

        private readonly IReadOnlyDictionary<string, ValueExpression> _resources;

        private static readonly char[] Bom = Encoding.Unicode.GetChars(Encoding.Unicode.GetPreamble());
    }
}
