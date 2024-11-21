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
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;

using IResourceProvider = Kaspirin.UI.Framework.UiKit.Localization.LocResources.IResourceProvider;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer
{
    public abstract class LocalizerBase : ILocalizer
    {
        protected LocalizerBase(LocalizerParameters parameters)
        {
            Guard.ArgumentIsNotNull(parameters);

            ResourceProvider = parameters.ResourceProvider;
            ScopeInfo = parameters.Scope;
            CultureInfo = parameters.CultureInfo;
            IsValid = true;
            _scopeUris = new Lazy<Uri[]>(CreateScopeUriList);
        }

        #region ILocalizer

        public ScopeMetainfo ScopeInfo { get; }

        public bool IsValid { get; set; }

        public bool ContainsKey(string key)
        {
            Guard.ArgumentIsNotNull(key);

            return ResolveScopeObject(key) != null;
        }

        #endregion

        protected CultureInfo CultureInfo { get; }

        protected IResourceProvider ResourceProvider { get; }

        protected TValue GetValue<TValue>(string key)
        {
            Guard.ArgumentIsNotNull(key);

            var scopeObject = ResolveScopeObject(key);
            if (scopeObject != null)
            {
                return (TValue)scopeObject.GetValue(key);
            }

            throw new LocalizerException($"Key '{key}' not found in scope '{ScopeInfo.Scope}'.");
        }

        protected IScope? ResolveScopeObject(string key)
        {
            Guard.ArgumentIsNotNull(key);

            return _scopeNameToKeyCache.GetOrAdd(key, CreateScopeObjectForKey);
        }

        protected IScope? ResolveScopeObject(Uri scopeUri)
        {
            Guard.ArgumentIsNotNull(scopeUri);

            return _scopeUriToObjectCache.GetOrAdd(scopeUri, CreateScopeObject);
        }

        protected abstract IScope CreateScopeObject(Uri scopeUri);

        private IScope? CreateScopeObjectForKey(string key)
        {
            return _scopeUris.Value.Select(ResolveScopeObject).FirstOrDefault(scope => ValidateScopeObject(scope, key));
        }

        private Uri[] CreateScopeUriList()
        {
            return ScopeInfo.ScopeLookups
                .SelectMany(ResourceProvider.SearchResources)
                .Select(ScopeInfo.ConstraintScopeUri)
                .Distinct()
                .ToArray();
        }

        private static bool ValidateScopeObject(IScope? scope, string key)
        {
            return scope switch
            {
                null => false,
                _ => scope.Keys.Any(scopeKey => string.Compare(key, scopeKey, StringComparison.InvariantCultureIgnoreCase) == 0)
            };
        }

        private readonly ConcurrentDictionary<string, IScope?> _scopeNameToKeyCache = new();
        private readonly ConcurrentDictionary<Uri, IScope?> _scopeUriToObjectCache = new();
        private readonly Lazy<Uri[]> _scopeUris;
    }
}
