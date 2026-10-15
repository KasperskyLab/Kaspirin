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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class RegionHistory : IEnumerable<string>
{
    internal RegionHistory()
    {
    }

    public int Count => _items.Count;

    public string? Current => _items.FirstOrDefault();

    public string? Previous => _items.Skip(1).FirstOrDefault();

    public bool Contains<TView>()
        where TView : FrameworkElement
    {
        return Contains(typeof(TView).FullName!);
    }

    public bool Contains(string? item)
    {
        return !string.IsNullOrEmpty(item) && _items.Contains(item!);
    }

    public bool Remove<TView>()
        where TView : FrameworkElement
    {
        return Remove(typeof(TView).FullName!);
    }

    public bool Remove(string item)
    {
        Guard.EnsureArgumentIsNotNullOrEmpty(item);

        return _items.Remove(item);
    }

    #region IEnumerable

    public IEnumerator<string> GetEnumerator()
    {
        return ((IEnumerable<string>)_items.ToList()).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    internal void Push(string item)
    {
        Guard.ArgumentIsNotNull(item);

        _items.Insert(0, item);
    }

    internal string? Pop()
    {
        var item = _items.FirstOrDefault();
        if (item == null)
        {
            return null;
        }

        _items.Remove(item);
        return item;
    }

    internal IList<string> PopRange(int count)
    {
        Guard.Argument(count > 0);

        var items = _items.GetRange(0, count);

        _items.RemoveRange(0, count);

        return items;
    }

    internal int IndexOf(string item)
    {
        return _items.IndexOf(item);
    }

    internal void Clear()
    {
        _items.Clear();
    }

    private readonly List<string> _items = new();
}
