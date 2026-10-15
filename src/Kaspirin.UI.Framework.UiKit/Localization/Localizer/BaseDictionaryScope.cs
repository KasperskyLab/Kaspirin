// Copyright © 2025 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer;

public abstract class BaseDictionaryScope : IScope<ResourceItem>
{
    protected BaseDictionaryScope(IEnumerable<ResourceItem> resources)
    {
        Guard.ArgumentIsNotNull(resources);

        _resources = new(() => PrepareResourcesDictionary(resources));
    }

    public IEnumerable<string> Keys => _resources.Value.Keys;

    public ResourceItem GetValue(string key)
    {
        Guard.ArgumentIsNotNull(key);

        return _resources.Value[key.ToLowerInvariant()].Select();
    }

    protected abstract IScopeResourceSelector CreateResourceSelector(IEnumerable<ResourceItem> resources);

    private IDictionary<string, IScopeResourceSelector> PrepareResourcesDictionary(IEnumerable<ResourceItem> resources)
    {
        return resources
            .GroupBy(res => res.Uri.AbsoluteUri.Segments.Last().ToLowerInvariant())
            .ToDictionary(
                group => group.Key,
                group => group.Count() == 1
                            ? new SingleResourceSelector(group.First())
                            : CreateResourceSelector(group));
    }

    private sealed class SingleResourceSelector : IScopeResourceSelector
    {
        public SingleResourceSelector(ResourceItem resource)
            => _resource = resource;

        public ResourceItem Select()
            => _resource;

        private readonly ResourceItem _resource;
    }

    private readonly Lazy<IDictionary<string, IScopeResourceSelector>> _resources;
}
