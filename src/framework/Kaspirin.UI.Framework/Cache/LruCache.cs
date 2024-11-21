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

namespace Kaspirin.UI.Framework.Cache
{
    /// <summary>
    ///     Implements the LRU cache.
    /// </summary>
    /// <typeparam name="TKey">
    ///     The type of key.
    /// </typeparam>
    /// <typeparam name="TValue">
    ///     The type of value.
    /// </typeparam>
    public sealed class LruCache<TKey, TValue>
        where TKey : notnull
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CircularBuffer{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">
        ///     The maximum number of elements.
        /// </param>
        public LruCache(int capacity)
        {
            _capacity = capacity;
        }

        /// <summary>
        ///     Checks for the presence of an element in the cache and returns its value if the element is found.
        /// </summary>
        /// <param name="key">
        ///     The key of the element.
        /// </param>
        /// <param name="value">
        ///     The value of the element.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the element is found, otherwise <see langword="false" />.
        /// </returns>
        public bool TryGet(TKey key, out TValue? value)
        {
            lock (_syncObj)
            {
                if (!_cacheMap.TryGetValue(key, out var node))
                {
                    value = default;

                    return false;
                }

                value = node.Value.Value;
                _lruList.Remove(node);
                _lruList.AddLast(node);
                return true;
            }
        }

        /// <summary>
        ///     Adds an item to the cache.
        /// </summary>
        /// <param name="key">
        ///     The key of the element.
        /// </param>
        /// <param name="value">
        ///     The value of the element.
        /// </param>
        public void Put(TKey key, TValue? value)
        {
            lock (_syncObj)
            {
                if (_cacheMap.Count >= _capacity)
                {
                    RemoveFirst();
                }

                var cacheItem = new LruCacheItem<TKey, TValue?>(key, value);
                var node = new LinkedListNode<LruCacheItem<TKey, TValue?>>(cacheItem);
                _lruList.AddLast(node);
                _cacheMap[key] = node;
            }
        }

        private void RemoveFirst()
        {
            // Remove from LRUPriority
            var node = _lruList.First;

            if (node == null)
            {
                return;
            }

            _lruList.RemoveFirst();

            // Remove from cache
            _cacheMap.Remove(node.Value.Key);
        }

        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<LruCacheItem<TKey, TValue?>>> _cacheMap = new();
        private readonly LinkedList<LruCacheItem<TKey, TValue?>> _lruList = new();
        private readonly object _syncObj = new();
    }
}