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

namespace Kaspirin.UI.Framework.Storage.KeyValue
{
    /// <summary>
    ///     Interface for working with key-value storage.
    /// </summary>
    public interface IKeyValueStorage
    {
        /// <summary>
        ///     Checks if there is a value for the specified key in the storage.
        /// </summary>
        /// <param name="key">
        ///     The key for verification.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the value is stored for the property, otherwise <see langword="false" />.
        /// </returns>
        bool HasValue(string key);

        /// <summary>
        ///     Retrieves the value from the storage using the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key is for reading.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The value from the repository, or <paramref name="defaultValue" /> if the key is not found.
        /// </returns>
        object? GetValue(string key, object? defaultValue);

        /// <summary>
        ///     Saves the value in the storage by the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key to save.
        /// </param>
        /// <param name="value">
        ///     The stored value.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the save was successful, otherwise <see langword="false" />.
        /// </returns>
        bool SetValue(string key, object value);
    }
}