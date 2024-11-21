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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Markup;

using IResourceProvider = Kaspirin.UI.Framework.UiKit.Localization.LocResources.IResourceProvider;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Xaml
{
    public sealed class XamlScope : IScope
    {
        public XamlScope(Uri scopeUri, IResourceProvider resourceProvider)
        {
            Guard.ArgumentIsNotNull(scopeUri);
            Guard.ArgumentIsNotNull(resourceProvider);

            ScopeUri = scopeUri;

            const string bamlResourcePattern = @"^pack:\/\/application:,,,\/[\w\W]*\.baml$";
            const string xamlResourcePattern = @"^[\w\W]*\.xaml$";

            if (Regex.IsMatch(scopeUri.AbsoluteUri, bamlResourcePattern))
            {
                var resourceUri = new Uri(scopeUri.AbsoluteUri.Replace(".baml", ".xaml"), UriKind.Absolute);

                _resourceDictionary = new ResourceDictionary { Source = resourceUri };
            }
            else if (Regex.IsMatch(scopeUri.AbsoluteUri, xamlResourcePattern))
            {
                var resourceBytes = resourceProvider.ReadResource(scopeUri);
                var resourceXaml = Encoding.UTF8.GetString(resourceBytes).TrimStart(_bom);

                _resourceDictionary = (ResourceDictionary)XamlReader.Parse(resourceXaml);
            }
            else
            {
                _resourceDictionary = new ResourceDictionary();
            }

            Keys = _resourceDictionary.Keys.Cast<string>().ToList();
        }

        public Uri ScopeUri { get; }

        public IEnumerable<string> Keys { get; }

        public object GetValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            return _resourceDictionary[key];
        }

        private readonly ResourceDictionary _resourceDictionary;

        private static readonly char[] _bom = Encoding.Unicode.GetChars(Encoding.Unicode.GetPreamble());
    }
}
