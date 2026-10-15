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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings;

public sealed class StringParamResolver : IStringParamResolver
{
    public static IStringParamResolver Empty { get; } = new EmptyParamResolver();

    public StringParamResolver(string key, object? value)
    {
        _implementation = new SingleValueParamResolver(key, value);
    }

    public StringParamResolver(Func<string, object?> parameters)
    {
        _implementation = new FuncParamResolver(parameters);
    }

    public StringParamResolver(IDictionary<string, object?> parameters)
    {
        _implementation = new DictionaryBasedParamResolver(parameters);
    }

    public StringParamResolver(IDictionary<string, Lazy<object?>> parameters)
    {
        _implementation = new DictionaryBasedLazyParamResolver(parameters);
    }

    public object? Resolve(string paramName)
    {
        return _implementation.Resolve(paramName);
    }

    private sealed class EmptyParamResolver : IStringParamResolver
    {
        public EmptyParamResolver()
            => _emptyResolver = new DictionaryBasedParamResolver(new Dictionary<string, object?>());

        private readonly DictionaryBasedParamResolver _emptyResolver;

        public object? Resolve(string paramName)
            => _emptyResolver.Resolve(paramName);
    }

    private sealed class SingleValueParamResolver : IStringParamResolver
    {
        public SingleValueParamResolver(string key, object? value)
        {
            _key = Guard.EnsureArgumentIsNotNull(key);
            _value = value;
        }

        public object? Resolve(string paramName)
        {
            if (paramName.Equals(_key))
            {
                return _value;
            }

            throw new LocalizerException($"The given parameter key = '{paramName}' was not present in {nameof(SingleValueParamResolver)}.");
        }

        private readonly string _key;
        private readonly object? _value;
    }

    private sealed class FuncParamResolver : IStringParamResolver
    {
        public FuncParamResolver(Func<string, object?> parameters)
        {
            _parameters = Guard.EnsureArgumentIsNotNull(parameters);
        }

        public object? Resolve(string paramName)
            => _parameters.Invoke(paramName);

        private readonly Func<string, object?> _parameters;
    }

    private sealed class DictionaryBasedParamResolver : IStringParamResolver
    {
        public DictionaryBasedParamResolver(IDictionary<string, object?> parameters)
            => _parameters = Guard.EnsureArgumentIsNotNull(parameters);

        public object? Resolve(string paramName)
        {
            if (_parameters.TryGetValue(paramName, out var paramValue))
            {
                return paramValue;
            }

            var possibleValue = _parameters.Keys.Any()
                ? $"Possible parameters: {string.Join(",", _parameters.Keys)}."
                : "No possible parameters available for current string.";

            throw new LocalizerException($"The given parameter key = '{paramName}' was not present in {nameof(DictionaryBasedParamResolver)}. {possibleValue}");
        }

        private readonly IDictionary<string, object?> _parameters;
    }

    private sealed class DictionaryBasedLazyParamResolver : IStringParamResolver
    {
        public DictionaryBasedLazyParamResolver(IDictionary<string, Lazy<object?>> parameters)
            => _parameters = Guard.EnsureArgumentIsNotNull(parameters);

        public object? Resolve(string paramName)
        {
            if (_parameters.TryGetValue(paramName, out var paramValue))
            {
                return paramValue.Value;
            }

            var possibleValue = _parameters.Keys.Any()
                ? $"Possible parameters: {string.Join(",", _parameters.Keys)}."
                : "No possible parameters available for current string.";

            throw new LocalizerException($"The given parameter key = '{paramName}' was not present in {nameof(DictionaryBasedLazyParamResolver)}. {possibleValue}");
        }

        private readonly IDictionary<string, Lazy<object?>> _parameters;
    }

    private readonly IStringParamResolver _implementation;
}