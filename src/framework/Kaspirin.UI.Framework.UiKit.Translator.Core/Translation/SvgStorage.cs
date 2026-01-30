// Copyright © 2025 AO Kaspersky Lab.
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation
{
    internal class SvgStorage
    {
        static SvgStorage()
        {
            var embeddedConfigurationProvider = new EmbeddedConfigurationProvider();
            if (!embeddedConfigurationProvider.TryGetValue(Const.SvgStorageEmbeddedConfigurationKey, out var storageDirectory))
            {
                throw new Exception("failed to get svg storage directory path.");
            }

            RootDirectory = storageDirectory;
        }

        public SvgStorage(
            string storageName,
            string outputDirectory,
            LineEndingMode lineEndingMode,
            TaskLoggingHelper log)
        {
            _storageFile = Path.Combine(RootDirectory, $"storage_{storageName.ToLower()}.xml");
            _outputDirectory = outputDirectory;
            _lineEndingMode = lineEndingMode;
            _log = log;
            _addedFiles = new();

            Directory.CreateDirectory(_outputDirectory);
            Directory.CreateDirectory(RootDirectory);

            _storage = ReadStorageFile(_storageFile);
        }

        public static string RootDirectory { get; }

        public string[] Files => _addedFiles.ToArray();

        public void Add(string svg, string hash, IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                var svgContent = string.Empty;
                var svgPath = path;

                _addedFiles.Add(svgPath);

                var cachedSvgByHash = _storage.SingleOrDefault(i => Equals(i.Hash, hash));
                if (cachedSvgByHash != null)
                {
                    svgContent = cachedSvgByHash.Content;
                }
                else
                {
                    svgContent = LineEndingHelper.NormalizeLineEndings(svg, _lineEndingMode);

                    var cachedSvgByPath = _storage.SingleOrDefault(i => Equals(i.Path, svgPath));
                    if (cachedSvgByPath != null)
                    {
                        cachedSvgByPath.Hash = hash;
                        cachedSvgByPath.Content = svgContent;
                    }
                    else
                    {
                        _storage.RemoveAll(i => Equals(i.Path, svgPath));
                        _storage.Add(new()
                        {
                            Content = svgContent,
                            Hash = hash,
                            Path = svgPath
                        });
                    }
                }
            }
        }

        public void Store()
        {
            _storage
                .ToList()
                .ForEach(i =>
                {
                    if (_addedFiles.Any(svgPath => Equals(i.Path, svgPath)))
                    {
                        File.WriteAllText(i.Path, i.Content, Encoding.UTF8);
                    }
                    else
                    {
                        if (File.Exists(i.Path))
                        {
                            File.Delete(i.Path);
                        }

                        _storage.Remove(i);
                    }
                });

            UpdateStorageFile(_storageFile, _storage);
        }

        private void UpdateStorageFile(string filePath, List<SvgStorageItem> items)
        {
            items.Sort((a, b) => string.Compare(a.Path, b.Path, StringComparison.Ordinal));

            var serializer = new XmlSerializer(typeof(SvgStorageItems));
            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            serializer.Serialize(writer, new SvgStorageItems { Items = items });
        }

        private List<SvgStorageItem> ReadStorageFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<SvgStorageItem>();
            }

            var serializer = new XmlSerializer(typeof(SvgStorageItems));
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            var svgStorageItems = (SvgStorageItems)serializer.Deserialize(reader);
            return svgStorageItems.Items;
        }

        private bool Equals(string first, string second)
        {
            return first.Equals(second, StringComparison.CurrentCultureIgnoreCase);
        }

        private readonly string _storageFile;
        private readonly string _outputDirectory;
        private readonly LineEndingMode _lineEndingMode;
        private readonly TaskLoggingHelper _log;
        private readonly List<string> _addedFiles;
        private readonly List<SvgStorageItem> _storage;
    }
}
