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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer
{
    public sealed class LocalizerParameterFactory
    {
        public LocalizerParameterFactory(
            LocalizerSettings localizerSettings,
            LocalizationCultureInfo localizationCulture,
            IResourceProvider resourceProvider)
        {
            Guard.ArgumentIsNotNull(localizerSettings);
            Guard.ArgumentIsNotNull(localizationCulture);
            Guard.ArgumentIsNotNull(resourceProvider);

            _scopeUriPatterns = localizerSettings.ScopeUriPatterns
                .Where(kv => typeof(ILocalizer)
                .IsAssignableFrom(kv.Key))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            _fallbackScope = localizerSettings.FallbackScope.ToLowerInvariant();
            _resourceProvider = resourceProvider;
            _localizationCulture = localizationCulture;
        }

        public LocalizerParameters Resolve(string scope, Type type)
        {
            Guard.ArgumentIsNotNull(scope);
            Guard.ArgumentIsNotNull(type);

            var scopePattern = GetLocalizerScopePattern(type);
            var scopeMetainfo = new ScopeMetainfo(scopePattern, scope, _fallbackScope);

            return new LocalizerParameters(scopeMetainfo, _resourceProvider, _localizationCulture.CultureInfo);
        }

        private string GetLocalizerScopePattern(Type localizerType)
        {
            if (_scopeUriPatterns.TryGetValue(localizerType, out var scopePattern))
            {
                return scopePattern;
            }

            var localizerInterface = localizerType.GetInterfaces().FirstOrDefault(i => _scopeUriPatterns.ContainsKey(i));
            if (localizerInterface != null)
            {
                return _scopeUriPatterns[localizerInterface];
            }

            throw new LocException($"Scope pattern not found for localizer type {localizerType}");
        }

        private readonly IDictionary<Type, string> _scopeUriPatterns;
        private readonly IResourceProvider _resourceProvider;
        private readonly LocalizationCultureInfo _localizationCulture;
        private readonly string _fallbackScope;
    }
}
