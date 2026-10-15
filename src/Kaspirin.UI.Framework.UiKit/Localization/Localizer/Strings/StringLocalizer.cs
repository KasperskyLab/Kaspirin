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
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings;

public class StringLocalizer : BaseLocalizer<object>, IStringLocalizer
{
    public StringLocalizer(LocalizerParameters parameters)
        : this(parameters, null)
    { }

    public StringLocalizer(LocalizerParameters parameters, StringLocalizer? fallbackLocalizer)
        : base(parameters)
    {
        _fallbackLocalizer = fallbackLocalizer;
    }

    public virtual string? GetString(string key, IStringLocalizerOption? option = null)
    {
        return ResolveString(key, StringParamResolver.Empty, option);
    }

    public virtual string? GetString(string key, IDictionary<string, object?> parameters, IStringLocalizerOption? option = null)
    {
        return ResolveString(key, new StringParamResolver(parameters), option);
    }

    public virtual string? GetString(string key, IStringParamResolver paramResolver, IStringLocalizerOption? option = null)
    {
        return ResolveString(key, paramResolver, option);
    }

    protected override IScope<object> CreateFileScopeObject(ResourceItem resource)
        => new StringScope(resource, ResourceProvider, CultureInfo);

    private ValueExpression ResolveKeyCallback(string key)
    {
        var currentScope = ResolveScopeObject(key);
        if (currentScope != null)
        {
            return Guard.EnsureIsInstanceOfType<ValueExpression>(currentScope.GetValue(key));
        }

        var fallbackScope = _fallbackLocalizer?.ResolveScopeObject(key);
        if (fallbackScope != null)
        {
            return Guard.EnsureIsInstanceOfType<ValueExpression>(fallbackScope.GetValue(key));
        }

        throw new LocalizerException($"failed to provide value for key = '{key}' in scope = '{ScopeInfo.Scope}'.");
    }

    private string? ResolveString(string key, IStringParamResolver paramResolver, IStringLocalizerOption? option = null)
    {
        Guard.ArgumentIsNotNull(key);
        Guard.ArgumentIsNotNull(paramResolver);

        try
        {
            var stringExpression = ResolveKeyCallback(key);
            var stringResolved = stringExpression.Resolve(ResolveKeyCallback, paramResolver.Resolve);
            var stringTransformed = ApplyOptionTransformation(stringResolved, option);
            var stringUnique = _stringCache.GetUniqueString(stringTransformed);

            return stringUnique;
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"failed to provide string for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    private string ApplyOptionTransformation(string stringValue, IStringLocalizerOption? option)
    {
        if (option != null)
        {
            return option.Apply(stringValue);
        }

        return stringValue;
    }

    private readonly StringLocalizer? _fallbackLocalizer;

    private static readonly StringCache _stringCache = new();
}