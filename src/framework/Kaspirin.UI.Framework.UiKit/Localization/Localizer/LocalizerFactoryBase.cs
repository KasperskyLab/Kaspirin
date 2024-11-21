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
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer
{
    public abstract class LocalizerFactoryBase
    {
        public TLocalizer Resolve<TLocalizer>(string scope) where TLocalizer : ILocalizer
        {
            Guard.ArgumentIsNotNull(scope);

            return (TLocalizer)Resolve(scope, typeof(TLocalizer));
        }

        public ILocalizer Resolve(string scope, Type localizerType)
        {
            Guard.ArgumentIsNotNull(scope);
            Guard.ArgumentIsNotNull(localizerType);

            var metaKey = new LocalizerMetaKey(scope, localizerType);
            return _localizerCache.GetOrAdd(metaKey, key => CreateLocalizer(key.Scope, key.LocalizerType));
        }

        public void ResetCache()
        {
            _localizerCache.Values.ToList().ForEach(l => l.IsValid = false);
            _localizerCache.Clear();
        }

        protected abstract ILocalizer CreateLocalizer(string scope, Type localizerType);

        private struct LocalizerMetaKey
        {
            public LocalizerMetaKey(string scope, Type localizerType)
            {
                Scope = scope.ToLowerInvariant();
                LocalizerType = localizerType;
            }

            public string Scope { get; }
            public Type LocalizerType { get; }
        }

        private readonly ConcurrentDictionary<LocalizerMetaKey, ILocalizer> _localizerCache = new();
    }
}
