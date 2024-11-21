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
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings
{
    public sealed class StringLocalizer : LocalizerBase, IStringLocalizer
    {
        public StringLocalizer(LocalizerParameters parameters)
            : this(parameters, null)
        { }

        public StringLocalizer(LocalizerParameters parameters, StringLocalizer? fallbackLocalizer)
            : base(parameters)
        {
            _fallbackLocalizer = fallbackLocalizer;
        }

        #region IStringLocalizer

        public string? GetString(string key, IStringLocalizerOption? option = null)
        {
            return GetString(key, new Dictionary<string, object?>(), option);
        }

        public string? GetString(string key, IDictionary<string, object?> keyParams, IStringLocalizerOption? option = null)
        {
            Guard.ArgumentIsNotNull(keyParams);
            
            var resolveParamCallback = CreateResolveParamCallback(keyParams);
            return GetString(key, resolveParamCallback, option);
        }

        public string? GetString(string key, Func<string, object?> resolveParamCallback, IStringLocalizerOption? option = null)
        {
            Guard.ArgumentIsNotNull(key);
            Guard.ArgumentIsNotNull(resolveParamCallback);

            try
            {
                var stringExpression = GetValue<ValueExpression>(key);
                var stringResolved = stringExpression.Resolve(ResolveKeyCallback, resolveParamCallback);
                var stringTransformed = ApplyOptionTransformation(stringResolved, option);
                var stringUnique = StringCache.GetUniqueString(stringTransformed);

                return stringUnique;
            }
            catch (Exception e)
            {
                e.ProcessAsLocalizerException($"failed to provide string for key='{key}'.");
                return null;
            }
        }

        #endregion

        protected override IScope CreateScopeObject(Uri scopeUri)
        {
            return new StringScope(scopeUri, ResourceProvider, CultureInfo);
        }

        private ValueExpression ResolveKeyCallback(string key)
        {
            var currentScope = ResolveScopeObject(key);
            if (currentScope != null)
            {
                return (ValueExpression)currentScope.GetValue(key);
            }

            var fallbackScope = _fallbackLocalizer?.ResolveScopeObject(key);
            if (fallbackScope != null)
            {
                return (ValueExpression)fallbackScope.GetValue(key);
            }

            throw new LocalizerException($"failed to provide value for key = '{key}' in scope = '{ScopeInfo.Scope}'.");
        }

        private string ApplyOptionTransformation(string stringValue, IStringLocalizerOption? option)
        {
            if (option != null)
            {
                return option.Apply(stringValue);
            }

            return stringValue;
        }

        private static Func<string, object?> CreateResolveParamCallback(IDictionary<string, object?> keyParams)
        {
            return key =>
            {
                if (keyParams.TryGetValue(key, out var paramValue))
                {
                    return paramValue;
                }

                var possibleValue = keyParams.Keys.Any()
                    ? $"Possible parameters: {string.Join(",", keyParams.Keys)}"
                    : "No possible parameters available for current string";

                throw new LocalizerException($"The given parameter key = '{key}' was not present in the dictionary. {possibleValue}");
            };
        }

        private readonly StringLocalizer? _fallbackLocalizer;

        private static readonly StringCache StringCache = new();
    }
}