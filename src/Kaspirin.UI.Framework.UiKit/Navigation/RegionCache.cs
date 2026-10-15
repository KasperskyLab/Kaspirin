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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class RegionCache : IEnumerable<RegionView>
{
    internal RegionCache()
    {
    }

    internal event Action<RegionView> Added = EmptyDelegate.For<Action<RegionView>>();

    internal event Action<RegionView> Removed = EmptyDelegate.For<Action<RegionView>>();

    public int Count => _views.Count;

    public RegionView? TryFind(string? viewName)
    {
        if (string.IsNullOrEmpty(viewName))
        {
            return null;
        }

        return _views.FirstOrDefault(view => view.Name == viewName);
    }

    public bool Contains<TView>()
        where TView : FrameworkElement
    {
        return Contains(typeof(TView).FullName!);
    }

    public bool Contains(string? viewName)
    {
        return TryFind(viewName) != null;
    }

    public bool Remove<TView>()
        where TView : FrameworkElement
    {
        return Remove(typeof(TView).FullName!);
    }

    public bool Remove(string viewName)
    {
        Guard.EnsureArgumentIsNotNullOrEmpty(viewName);

        var view = TryFind(viewName);
        if (view == null)
        {
            return false;
        }

        _views.Remove(view);

        Removed.Invoke(view);

        return true;
    }

    #region IEnumerable

    public IEnumerator<RegionView> GetEnumerator()
    {
        return ((IEnumerable<RegionView>)_views.ToList()).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    internal void Add(RegionView view)
    {
        Guard.ArgumentIsNotNull(view);

        if (_views.Contains(view))
        {
            return;
        }

        _views.Add(view);

        Added.Invoke(view);
    }

    internal void Clear()
    {
        _views.ToArray().ForEach(v => Remove(v.Name));
    }

    private readonly List<RegionView> _views = new();
}
