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

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources.ResourceBrowsers
{
    public sealed class ZipArchiveResourceBrowser : BaseResourceBrowser
    {
        public ZipArchiveResourceBrowser(
            string root,
            bool handleChanges,
            string fileName,
            Func<string, IZipArchiveProvider>? zipFactory = null)
        {
            _root = Guard.EnsureArgumentIsNotNullOrEmpty(root);
            _fileName = Guard.EnsureArgumentIsNotNullOrEmpty(fileName);
            _zipFactory = zipFactory ?? GetDefaultZipFactory();

            if (!Directory.Exists(_root))
            {
                return;
            }

            if (handleChanges)
            {
                _rootDirectoryWatcher = new FileSystemWatcher(_root);
                _rootDirectoryWatcher.Changed += RootDirectoryChanged;
            }

            UpdateResourcesAsync(ScanResources);
        }

        protected override Stream Read(ResourceUri uri)
        {
            var resourcePath = uri.AbsoluteUri.AbsolutePath;
            var resourceStream = new MemoryStream();
            var subStringIndex = resourcePath.IndexOf(_fileName, StringComparison.OrdinalIgnoreCase) + _fileName.Length;

            var zipArchiveKey = resourcePath.Substring(0, subStringIndex);
            var zipArchive = _zipArchives[zipArchiveKey];

            //Entries from ZipArchive can only be read in single-threaded mode.
            lock (zipArchive)
            {
                var entityName = resourcePath.Replace(zipArchiveKey, string.Empty).TrimStart('/');
                var entityStream = zipArchive.Entries.GuardedSingle(e => e.FullName == entityName).Open();

                entityStream.CopyTo(resourceStream);
                entityStream.Dispose();
            }

            resourceStream.Position = 0;

            return resourceStream;
        }

        protected override void Dispose(bool disposing)
        {
            if (_zipArchives != null && disposing)
            {
                _zipArchives.ForEach(kv => kv.Value.Dispose());
                _zipArchives.Clear();
            }

            if (_rootDirectoryWatcher != null && disposing)
            {
                _rootDirectoryWatcher.Changed -= RootDirectoryChanged;
            }
        }

        private void RootDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            UpdateResourcesAsync(ScanResources);
        }

        private void ScanResources(HashSet<ResourceUri> resources)
        {
            try
            {
                _tracer.TraceDebug($"Resources scanning in zip archives in folder '{_root}'.");

                var zipArchiveResources = new DirectoryInfo(_root)
                    .GetFiles(_fileName, SearchOption.AllDirectories)
                    .Select(fileInfo => fileInfo.FullName)
                    .SelectMany(ScanZipResources)
                    .ToArray();

                if (zipArchiveResources.Any())
                {
                    resources.Clear();
                    resources.UnionWith(zipArchiveResources);

                    RaiseResourcesLoaded(zipArchiveResources);
                }
            }
            catch (Exception e)
            {
                e.TraceException($"Failed to scan resources in zip archives in folder '{_root}'.");

                throw;
            }
        }

        private ResourceUri[] ScanZipResources(string zipArchivePath)
        {
            try
            {
                _tracer.TraceDebug($"Resources scanning in zip archive '{zipArchivePath}'.");

                var zipArchiveKey = new Uri(zipArchivePath, UriKind.Absolute).AbsolutePath;
                var zipArchiveFilePath = zipArchivePath;
                var zipArchive = _zipFactory(zipArchivePath);

                _zipArchives.Add(zipArchiveKey, zipArchive);

                var zipArchiveResources = zipArchive.Entries.Where(e => e.IsFile)
                    .Select(entry => CreateUri(entry.FullName, zipArchiveFilePath, _root, _fileName))
                    .ToArray();

                if (zipArchiveResources.Length > 0)
                {
                    _tracer.TraceInformation($"Resources found in zip archive '{zipArchivePath}'. Resources count: {zipArchiveResources.Length}.");
                }

                return zipArchiveResources;
            }
            catch (Exception e)
            {
                e.TraceException($"Failed to scan resources in zip archive '{zipArchivePath}'.");

                throw;
            }
        }

        private static ResourceUri CreateUri(string entryPath, string zipArchiveFilePath, string rootPath, string fileName)
        {
            return new ResourceUri(
                absoluteUri: new Uri(Path.Combine(zipArchiveFilePath, entryPath), UriKind.Absolute),
                relativeUri: new Uri($"{zipArchiveFilePath.Substring(rootPath.Length).Replace(fileName, string.Empty).Replace('\\', '/')}{entryPath}".ToLowerInvariant(), UriKind.Relative));
        }

        private static Func<string, IZipArchiveProvider> GetDefaultZipFactory()
        {
#if NETCOREAPP
            return ZipArchiveProvider.Create;
#else
            throw new NotSupportedException($"{nameof(ZipArchiveResourceBrowser)} is not supported.");
#endif
        }

        private readonly string _root;
        private readonly string _fileName;
        private readonly Func<string, IZipArchiveProvider> _zipFactory;
        private readonly Dictionary<string, IZipArchiveProvider> _zipArchives = new();
        private readonly FileSystemWatcher? _rootDirectoryWatcher;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Localization);
    }
}