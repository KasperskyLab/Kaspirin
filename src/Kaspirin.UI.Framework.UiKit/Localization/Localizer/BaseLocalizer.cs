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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer;

public abstract class BaseLocalizer<TScopeValue> : ILocalizer
{
    protected BaseLocalizer(LocalizerParameters parameters)
    {
        Guard.ArgumentIsNotNull(parameters);

        ResourceProvider = parameters.ResourceProvider;
        ScopeInfo = parameters.Scope;
        _localizationCultureInfo = parameters.LocalizationCultureInfo;
    }

    public ScopeMetaInfo ScopeInfo { get; }

    public virtual void ResetCache()
    {
        _keyToScopeObjectMap.Clear();
        _resourceToScopeObjectMap.Clear();
        _resourcesCache = null;
    }

    public bool ContainsKey(string key)
    {
        Guard.ArgumentIsNotNull(key);

        return ResolveScopeObject(key) != null;
    }

    public IList<string> GetKeys()
    {
        return ResolveAllScopeObjects()
            .SelectMany(s => s.Keys)
            .Distinct()
            .OrderBy(s => s)
            .ToList();
    }

    protected CultureInfo CultureInfo => _localizationCultureInfo.CultureInfo;

    protected ResourceProvider ResourceProvider { get; }

    protected TScopeValue GetValue(string key)
    {
        Guard.ArgumentIsNotNull(key);

        var scopeObject = ResolveScopeObject(key);
        if (scopeObject != null)
        {
            return scopeObject.GetValue(key);
        }

        throw new LocalizerException($"Key '{key}' not found in scope '{ScopeInfo.Scope}'.");
    }

    protected IScope<TScopeValue>? ResolveScopeObject(string key)
    {
        Guard.ArgumentIsNotNull(key);

        return _keyToScopeObjectMap.GetOrAdd(key, CreateScopeObjectForKey);
    }

    protected virtual IScope<TScopeValue> CreateDirectoryScopeObject(IEnumerable<ResourceItem> resources)
        => throw new LocalizerException($"Unexpected method invocation in {GetType().Name}.");

    protected virtual IScope<TScopeValue> CreateFileScopeObject(ResourceItem resource)
        => throw new LocalizerException($"Unexpected method invocation in {GetType().Name}.");

    private IScope<TScopeValue> ResolveFileScopeObject(ResourceItem resource)
    {
        Guard.ArgumentIsNotNull(resource);

        return _resourceToScopeObjectMap.GetOrAdd(resource, resource => CreateFileScopeObject(resource));
    }

    private IScope<TScopeValue> ResolveDirectoryScopeObject(IEnumerable<ResourceItem> resources)
    {
        Guard.ArgumentIsNotNull(resources);

        resources = resources.ToArray();

        if (_resourceToScopeObjectMap.TryGetValue(resources.First(), out var scope))
        {
            return scope;
        }

        var directoryScope = CreateDirectoryScopeObject(resources);

        foreach (var resource in resources)
        {
            _resourceToScopeObjectMap.TryAdd(resource, directoryScope);
        }

        return directoryScope;
    }

    private IEnumerable<IScope<TScopeValue>> ResolveAllScopeObjects()
    {
        if (ScopeInfo.ScopeType == ScopeType.File)
        {
            return ResolveScopeResources().Select(ResolveFileScopeObject);
        }
        else
        {
            return ResolveScopeResources().GroupBy(r => r.Descriptor).Select(g => ResolveDirectoryScopeObject(g));
        }
    }

    private ResourceItem[] ResolveScopeResources()
    {
        return _resourcesCache ??= CreateScopeResourcesList();
    }

    private IScope<TScopeValue>? CreateScopeObjectForKey(string key)
    {
        return ResolveAllScopeObjects().FirstOrDefault(scope => ValidateScopeObject(scope, key));
    }

    private ResourceItem[] CreateScopeResourcesList()
    {
        return ScopeInfo.ScopePaths
            .SelectMany(ResourceProvider.SearchResources)
            .ToArray();
    }

    private static bool ValidateScopeObject(IScope<TScopeValue>? scope, string key)
    {
        if (scope == null)
        {
            return false;
        }

        return scope.Keys.Any(scopeKey => scopeKey.Equals(key, StringComparison.OrdinalIgnoreCase));
    }

    private ResourceItem[]? _resourcesCache;

    private readonly ConcurrentDictionary<string, IScope<TScopeValue>?> _keyToScopeObjectMap = new();
    private readonly ConcurrentDictionary<ResourceItem, IScope<TScopeValue>> _resourceToScopeObjectMap = new();
    private readonly LocalizationCultureInfo _localizationCultureInfo;
}
