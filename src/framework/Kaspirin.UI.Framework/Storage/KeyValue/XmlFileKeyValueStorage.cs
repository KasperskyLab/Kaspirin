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
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Kaspirin.UI.Framework.Storage.KeyValue
{
    /// <summary>
    ///     Implements the <see cref="IKeyValueStorage" /> interface and uses an XML file as storage.
    /// </summary>
    public sealed class XmlFileKeyValueStorage : IKeyValueStorage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlFileKeyValueStorage" /> class.
        /// </summary>
        /// <param name="filePath">
        ///     The path to the XML file.
        /// </param>
        public XmlFileKeyValueStorage(string filePath)
        {
            Guard.ArgumentIsNotNull(filePath);

            _storageFile = filePath;
        }

        /// <inheritdoc cref="IKeyValueStorage.GetValue"/>
        public object? GetValue(string key, object? defaultValue)
        {
            Guard.ArgumentIsNotNull(key);

            key = FormatKey(key);

            var storage = Retrieve();

            return storage.TryGetValue(key, out var value)
                ? value != null
                    ? value
                    : defaultValue
                : defaultValue;
        }

        /// <inheritdoc cref="IKeyValueStorage.HasValue"/>
        public bool HasValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            key = FormatKey(key);

            var storage = Retrieve();

            return storage.ContainsKey(key);
        }

        /// <inheritdoc cref="IKeyValueStorage.SetValue"/>
        public bool SetValue(string key, object value)
        {
            Guard.ArgumentIsNotNull(key);

            key = FormatKey(key);

            var storage = Retrieve();

            storage[key] = value.ToString();

            Store(storage);

            return true;
        }

        private void Store(IDictionary<string, string?> dictionary)
        {
            var xmlElement = new XElement("root", dictionary.Select(kv => new XElement(kv.Key, kv.Value)));

            xmlElement.Save(_storageFile, SaveOptions.OmitDuplicateNamespaces);
        }

        private IDictionary<string, string?> Retrieve()
        {
            if (File.Exists(_storageFile))
            {
                return XElement.Parse(File.ReadAllText(_storageFile))
               .Elements()
               .ToDictionary(k => k.Name.ToString(), v => v.Value?.ToString());
            }

            return new Dictionary<string, string?>();
        }

        private string FormatKey(string key)
        {
            return key.Replace("\\", "_");
        }

        private readonly string _storageFile;
    }
}
