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

using System.Collections.Concurrent;
using System.Linq;

namespace Kaspirin.UI.Framework.Cache
{
    /// <summary>
    ///     Implements a ring buffer.
    /// </summary>
    /// <typeparam name="TKey">
    ///     The type of key.
    /// </typeparam>
    /// <typeparam name="TValue">
    ///     The type of value.
    /// </typeparam>
    public sealed class CircularBuffer<TKey, TValue>
        where TKey : notnull
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CircularBuffer{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="size">
        ///     The size of the buffer.
        /// </param>
        public CircularBuffer(int size)
        {
            _size = size;
        }

        /// <summary>
        ///     Checks for the presence of an element in the buffer and returns its value if the element is found.
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
            return _storage.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Deletes an item from the buffer.
        /// </summary>
        /// <param name="key">
        ///     The key of the element.
        /// </param>
        public void Remove(TKey key)
        {
            _storage.TryRemove(key, out var _);
            _queue.TryDequeue(out var _);
        }

        /// <summary>
        ///     Adds an element to the buffer.
        /// </summary>
        /// <param name="key">
        ///     The key of the element.
        /// </param>
        /// <param name="value">
        ///     The value of the element.
        /// </param>
        public void Add(TKey key, TValue? value)
        {
            _storage.TryAdd(key, value);
            _queue.Enqueue(key);

            while (_queue.Count > _size)
            {
                if (_queue.TryDequeue(out var tmpKey))
                {
                    _storage.TryRemove(tmpKey, out var tmpValue);
                }
            }
        }

        /// <summary>
        ///     Returns all the values of the elements from the buffer.
        /// </summary>
        /// <returns>
        ///     An array of values.
        /// </returns>
        public TValue?[] GetAllValues()
        {
            return _storage.Values.ToArray();
        }

        private readonly ConcurrentDictionary<TKey, TValue?> _storage = new();
        private readonly ConcurrentQueue<TKey> _queue = new();
        private readonly int _size;
    }
}