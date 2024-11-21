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

using System.Windows.Markup;
using System;

namespace Kaspirin.UI.Framework.Storage.KeyValue
{
    /// <summary>
    ///     Markup extension for working with the interface <see cref="IKeyValueStorage" /> in XAML.
    /// </summary>
    public abstract class KeyValueStorageExtension : MarkupExtension, IKeyValueStorage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyValueStorageExtension" /> class.
        /// </summary>
        /// <param name="keyValueStorage">
        ///     An instance of <see cref="IKeyValueStorage" />.
        /// </param>
        protected KeyValueStorageExtension(IKeyValueStorage keyValueStorage)
        {
            _keyValueStorage = keyValueStorage;
        }

        /// <summary>
        ///     Returns <see langword="this" />.
        /// </summary>
        /// <param name="serviceProvider">
        ///     Not used.
        /// </param>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <inheritdoc cref="IKeyValueStorage.HasValue"/>
        public virtual bool HasValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            return _keyValueStorage.HasValue(key);
        }

        /// <inheritdoc cref="IKeyValueStorage.GetValue"/>
        public virtual object? GetValue(string key, object? defaultValue)
        {
            Guard.ArgumentIsNotNull(key);

            return _keyValueStorage.GetValue(key, defaultValue);
        }

        /// <inheritdoc cref="IKeyValueStorage.SetValue"/>
        public virtual bool SetValue(string key, object value)
        {
            Guard.ArgumentIsNotNull(key);
            Guard.ArgumentIsNotNull(value);

            return _keyValueStorage.SetValue(key, value);
        }

        private readonly IKeyValueStorage _keyValueStorage;
    }
}