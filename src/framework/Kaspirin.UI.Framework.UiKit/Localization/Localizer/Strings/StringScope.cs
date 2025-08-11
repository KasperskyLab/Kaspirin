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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings;

public sealed class StringScope : IScope<object>
{
    public StringScope(ResourceItem resource, ResourceProvider resourceProvider, CultureInfo cultureInfo)
    {
        Guard.ArgumentIsNotNull(resource);
        Guard.ArgumentIsNotNull(resourceProvider);
        Guard.ArgumentIsNotNull(cultureInfo);

        var scopeText = resourceProvider.ReadResourceString(resource, Encoding.UTF8).TrimStart(_bom);
        var scopeName = resource.Descriptor.Scope;
        var scopeResources = ScopeDictionaryBuilder.BuildScope(
            scopeText,
            scopeName,
            cultureInfo);

        Keys = scopeResources.Keys;

        _resources = scopeResources;
    }

    public IEnumerable<string> Keys { get; }

    public object GetValue(string key)
    {
        Guard.ArgumentIsNotNull(key);

        return _resources[key];
    }

    private readonly IScopeDictionary _resources;

    private static readonly char[] _bom = Encoding.Unicode.GetChars(Encoding.Unicode.GetPreamble());
}
