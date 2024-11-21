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

using System.Collections.Generic;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class SelectInfoProvider
    {
        public SelectInfoProvider(Select select)
        {
            Guard.ArgumentIsNotNull(select);

            _select = select;
        }

        public SelectItemInfoProvider? GetTargetInfo(object? target, bool fromSource = false)
        {
            if (target == null)
            {
                return null;
            }

            if (target is SelectItemInfoProvider targetInfoProvider)
            {
                return targetInfoProvider;
            }

            if (target is SelectItem targetContainer)
            {
                return CreateSelectItemInfoProvider(target);
            }

            CleanupCache();

            if (_itemsCache.TryGetValue(target, out var cachedInfoProvider))
            {
                return cachedInfoProvider;
            }

            return GetItems(fromSource).FirstOrDefault(i => Equals(i.GetItem(), target));
        }

        public IList<SelectItemInfoProvider> GetSelectedInfo(bool fromSource = false)
            => GetItems(fromSource).Where(c => c.IsSelected).ToList();

        public IList<SelectItemInfoProvider> GetFilteredInfo(string filterText, bool fromSource = false)
            => GetItems(fromSource).Where(c => c.ContainsText(filterText)).ToList();

        public SelectItemInfoProvider? GetHighlightedInfo()
        {
            if (_highlightedInfo == null)
            {
                return null;
            }

            _highlightedInfo.Invalidate();

            return _highlightedInfo;
        }

        public void SetHighlightedInfo(object? target, bool scrollIntoView = false)
        {
            var targetInfo = GetTargetInfo(target);

            if (_highlightedInfo == targetInfo)
            {
                return;
            }

            var highlightContainer = _highlightedInfo?.GetContainer();
            if (highlightContainer != null)
            {
                highlightContainer.IsHighlighted = false;
            }

            _highlightedInfo = targetInfo;

            var newHighlightContainer = _highlightedInfo?.GetContainer();
            if (newHighlightContainer != null)
            {
                newHighlightContainer.IsHighlighted = true;
            }

            if (scrollIntoView)
            {
                var newHighlightItem = _highlightedInfo?.GetItem();
                if (newHighlightItem != null)
                {
                    _select.ScrollIntoView(newHighlightItem);
                }
            }
        }

        public void ClearCache()
        {
            _itemsCache.Clear();

            _highlightedInfo = null;
        }

        private IEnumerable<SelectItemInfoProvider> GetItems(bool fromSource = false)
        {
            var items = fromSource
                ? _select.Items.SourceCollection
                : _select.Items;

            if (items == null)
            {
                yield break;
            }

            CleanupCache();

            foreach (var item in items)
            {
                if (_itemsCache.TryGetValue(item, out var itemProvider))
                {
                    itemProvider.Invalidate();
                }
                else
                {
                    itemProvider = CreateSelectItemInfoProvider(item);
                }

                yield return itemProvider;
            }
        }

        private void CleanupCache()
            => _itemsCache.RemoveAll(kv => !kv.Value.IsAlive);

        private SelectItemInfoProvider CreateSelectItemInfoProvider(object item)
        {
            var itemProvider = new SelectItemInfoProvider(_select, item);

            _itemsCache[itemProvider.GetContainer()] = itemProvider;
            _itemsCache[itemProvider.GetItem()] = itemProvider;

            return itemProvider;
        }

        private SelectItemInfoProvider? _highlightedInfo;

        private readonly Dictionary<object, SelectItemInfoProvider> _itemsCache = new();
        private readonly Select _select;
    }
}
