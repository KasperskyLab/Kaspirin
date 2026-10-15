// Copyright © 2026 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class RegionContext
{
    internal RegionContext()
    {
    }

    public event Action<string> ContextChanged = EmptyDelegate.For<Action<string>>();

    public int Count => _items.Count;

    public IEnumerable<string> Keys => _items.Keys.ToList();

    public object? this[string key] => TryGet<object>(key, out var value) ? value : null;

    public bool Contains(string key)
    {
        Guard.EnsureArgumentIsNotNullOrEmpty(key);

        return _items.ContainsKey(key);
    }

    public bool TryGet<T>(string key, out T? value)
    {
        Guard.EnsureArgumentIsNotNullOrEmpty(key);

        if (_items.TryGetValue(key, out var item) && item is T typedItem)
        {
            value = typedItem;
            return true;
        }

        value = default;
        return false;
    }

    public void Add(string key, object value)
    {
        Guard.EnsureArgumentIsNotNullOrEmpty(key);
        Guard.ArgumentIsNotNull(value);

        _items.Add(key, value);

        ContextChanged.Invoke(key);
    }

    public void Update(string key, object value)
    {
        Guard.EnsureArgumentIsNotNullOrEmpty(key);
        Guard.ArgumentIsNotNull(value);

        _items[key] = value;

        ContextChanged.Invoke(key);
    }

    public bool Remove(string key)
    {
        Guard.EnsureArgumentIsNotNullOrEmpty(key);

        if (!_items.Remove(key))
        {
            return false;
        }

        ContextChanged.Invoke(key);
        return true;
    }

    private readonly Dictionary<string, object> _items = new();
}
