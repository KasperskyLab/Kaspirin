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

namespace Kaspirin.UI.Framework.Storage.KeyValue
{
    /// <summary>
    ///     Implements the <see cref="IKeyValueStorage" /> interface and uses a dictionary inside the current object as storage.
    /// </summary>
    public sealed class InMemoryKeyValueStorage : IKeyValueStorage
    {
        /// <inheritdoc cref="IKeyValueStorage.GetValue"/>
        public object? GetValue(string key, object? defaultValue)
        {
            Guard.ArgumentIsNotNull(key);

            return _storage.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <inheritdoc cref="IKeyValueStorage.HasValue"/>
        public bool HasValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            return _storage.ContainsKey(key);
        }

        /// <inheritdoc cref="IKeyValueStorage.SetValue"/>
        public bool SetValue(string key, object value)
        {
            Guard.ArgumentIsNotNull(key);

            _storage[key] = value;
            return true;
        }

        private readonly Dictionary<string, object> _storage = new();
    }
}