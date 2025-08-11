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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer;

public class LocalizerFactory
{
    public LocalizerFactory(LocalizerParameterFactory parameterFactory)
    {
        _parameterFactory = Guard.EnsureArgumentIsNotNull(parameterFactory);
    }

    public TLocalizer Resolve<TLocalizer>(string scope) where TLocalizer : ILocalizer
    {
        Guard.ArgumentIsNotNull(scope);

        return (TLocalizer)Resolve(scope, typeof(TLocalizer));
    }

    public ILocalizer Resolve(string scope, Type localizerType)
    {
        Guard.ArgumentIsNotNull(scope);
        Guard.ArgumentIsNotNull(localizerType);

        var key = CreateLocalizerKey(scope, localizerType);

        return _localizerCache.GetOrAdd(key, key =>
        {
            var parameters = _parameterFactory.Resolve(scope, localizerType);

            return CreateLocalizer(parameters, localizerType);
        });
    }

    public void ResetCache()
    {
        _localizerCache.Values.ToList().ForEach(l => l.ResetCache());
    }

    protected virtual LocalizerKey CreateLocalizerKey(string scope, Type localizerType)
    {
        return new LocalizerKey(scope, localizerType);
    }

    protected virtual ILocalizer CreateLocalizer(LocalizerParameters parameters, Type localizerType)
    {
        if (localizerType == typeof(IStringLocalizer))
        {
            var scope = parameters.Scope.Scope;
            var fallback = parameters.Scope.FallbackScope;

            return scope.Equals(fallback, StringComparison.OrdinalIgnoreCase)
                ? new StringLocalizer(parameters)
                : new StringLocalizer(parameters, (StringLocalizer)Resolve(fallback, typeof(IStringLocalizer)));
        }

        if (localizerType == typeof(IXamlLocalizer))
        {
            return new XamlLocalizer(parameters);
        }

        if (localizerType == typeof(IImageLocalizer))
        {
            return new ImageLocalizer(parameters);
        }

        if (localizerType == typeof(IFileLocalizer))
        {
            return new FileLocalizer(parameters);
        }

        var localizerInterface = localizerType.GetInterfaces().FirstOrDefault(i => typeof(ILocalizer).IsAssignableFrom(i) && i != typeof(ILocalizer));
        if (localizerInterface != null)
        {
            return CreateLocalizer(parameters, localizerInterface);
        }

        throw new LocException($"Failed to create localizer. Unknown type {localizerType}");
    }

    private readonly ConcurrentDictionary<LocalizerKey, ILocalizer> _localizerCache = new();
    private readonly LocalizerParameterFactory _parameterFactory;
}