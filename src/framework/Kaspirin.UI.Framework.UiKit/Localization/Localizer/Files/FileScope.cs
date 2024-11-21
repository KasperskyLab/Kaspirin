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

using IResourceProvider = Kaspirin.UI.Framework.UiKit.Localization.LocResources.IResourceProvider;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Files
{
    public sealed class FileScope : IScope
    {
        public FileScope(Uri scopeUri, IResourceProvider resourceProvider)
        {
            ScopeUri = Guard.EnsureArgumentIsNotNull(scopeUri);

            _resources = resourceProvider.SearchResources(scopeUri)
                .ToDictionary(fileUri => fileUri.Segments.Last().ToLowerInvariant(), fileUri => fileUri);
        }

        public Uri ScopeUri { get; }

        public IEnumerable<string> Keys => _resources.Keys;

        public object GetValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            return _resources[key.ToLowerInvariant()];
        }

        private readonly IDictionary<string, Uri> _resources;
    }
}
