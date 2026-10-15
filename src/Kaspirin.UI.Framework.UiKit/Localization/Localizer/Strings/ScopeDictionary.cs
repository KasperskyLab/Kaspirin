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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings;

public sealed class ScopeDictionary : IScopeDictionary
{
    public static ScopeDictionary Empty { get; } = new();

    public ScopeDictionary()
    {
        _source = new Dictionary<string, ValueExpression>();
    }

    public ScopeDictionary(IDictionary<string, ValueExpression> source)
    {
        _source = source;
    }

    public ScopeDictionary(IEnumerable<KeyValuePair<string, ValueExpression>> source)
    {
        _source = source.ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
    }

    public ValueExpression this[string key] => TryGetValue(key, out var value)
        ? value
        : throw new KeyNotFoundException($"The '{key}' key not found in {GetType().FullName}");

    public IEnumerable<string> Keys => _source.Keys;

    public IEnumerable<ValueExpression> Values => _source.Values;

    public int Count => _source.Count;

    public IEnumerator<KeyValuePair<string, ValueExpression>> GetEnumerator() => _source.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_source).GetEnumerator();

    public bool ContainsKey(string key) => _source.ContainsKey(key);

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out ValueExpression value)
        => _source.TryGetValue(key, out value);

    private readonly IDictionary<string, ValueExpression> _source;
}
