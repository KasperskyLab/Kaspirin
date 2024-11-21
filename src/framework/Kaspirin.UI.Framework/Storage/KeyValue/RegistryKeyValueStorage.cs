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

#pragma warning disable KCAIDE0006 // Class should be abstract or sealed

using System;
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.Storage.KeyValue
{
    /// <summary>
    ///     The class implements the interface <see cref="IKeyValueStorage" /> and uses the implementation <see cref="IRegistry" /> as storage.
    /// </summary>
    public class RegistryKeyValueStorage : IKeyValueStorage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegistryKeyValueStorage" /> class.
        /// </summary>
        /// <param name="root">
        ///     The root folder of the registry. It is added to the beginning of each key.
        /// </param>
        /// <param name="registry">
        ///     Implementation of <see cref="IRegistry" />.
        /// </param>
        public RegistryKeyValueStorage(string root, IRegistry registry)
        {
            Guard.ArgumentIsNotNull(root);
            Guard.ArgumentIsNotNull(registry);

            _root = root;
            _registry = registry;
        }

        /// <inheritdoc cref="IKeyValueStorage.HasValue"/>
        public bool HasValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            var keyName = ParseKey(key, out var valueName);
            var result = _registry.GetValue(_root + keyName, valueName, null);
            return result != null;
        }

        /// <inheritdoc cref="IKeyValueStorage.GetValue"/>
        public object? GetValue(string key, object? defaultValue)
        {
            Guard.ArgumentIsNotNull(key);

            try
            {
                var registryValue = _registry.GetValue(_root + ParseKey(key, out var valueName), valueName, defaultValue);

                return registryValue ?? defaultValue;
            }
            catch (Exception exception)
            {
                OnException(exception, $"Failed to get value from registry key \"{key}\" (root: \"{_root}\")");
                return defaultValue;
            }
        }

        /// <inheritdoc cref="IKeyValueStorage.SetValue"/>
        public bool SetValue(string key, object value)
        {
            Guard.ArgumentIsNotNull(key);
            Guard.ArgumentIsNotNull(value);

            try
            {
                _registry.SetValue(_root + ParseKey(key, out var valueName), valueName, value);
                return true;
            }
            catch (Exception exception)
            {
                OnException(exception, string.Empty);
                return false;
            }
        }

        /// <summary>
        ///     Called when exceptions occur when working with the repository.
        /// </summary>
        /// <param name="exception">
        ///     The original exception.
        /// </param>
        /// <param name="message">
        ///     A message describing the exception.
        /// </param>
        protected virtual void OnException(Exception exception, string message) { }

        private static string ParseKey(string key, out string valueName)
        {
            string keyName;
            if (key.Contains("\\"))
            {
                var keyParts = _keyParseRegularExpression.Matches(key);
                Guard.Assert(keyParts.Count == 1, "keyParts.Count == 1");

                var match = keyParts[0];
                Guard.Assert(match.Groups.Count == 3, "match.Groups.Count == 3");

                keyName = match.Groups[1].ToString();
                valueName = match.Groups[2].ToString();
            }
            else
            {
                keyName = string.Empty;
                valueName = key;
            }

            return keyName;
        }

        private readonly string _root;
        private readonly IRegistry _registry;

        private static readonly Regex _keyParseRegularExpression = new("(.*)" + Regex.Escape("\\") + "(.+)", RegexOptions.IgnoreCase);
    }
}