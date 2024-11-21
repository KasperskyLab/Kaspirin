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

using System;
using System.IO;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.LocResources.ResourceBrowsers
{
    public sealed class FileSystemResourceBrowser : ResourceBrowserBase
    {
        public FileSystemResourceBrowser(string root, bool handleChanges)
        {
            Guard.ArgumentIsNotNull(root);

            _rootUri = new Uri(root, UriKind.Absolute);

            UpdateResourcesAsync(resources =>
            {
                resources.Clear();
                resources.UnionWith(ScanDirectoryResources());
            });

            if (handleChanges)
            {
                _rootDirectoryWatcher = new FileSystemWatcher(_rootUri.LocalPath);
                _rootDirectoryWatcher.Changed += RootDirectoryChanged;
            }
        }

        ~FileSystemResourceBrowser()
        {
            if (_rootDirectoryWatcher != null)
            {
                _rootDirectoryWatcher.Changed -= RootDirectoryChanged;
            }
        }

        public override event EventHandler<ResourcesLoadedEventArgs>? ResourcesLoaded;

        #region IResourceBrowser

        public override byte[] Read(Uri resourceUri)
        {
            Guard.ArgumentIsNotNull(resourceUri);

            return ReadAllBytes(resourceUri.LocalPath);
        }

        #endregion

        private void RootDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            UpdateResourcesAsync(resources =>
            {
                resources.Clear();

                var directoryResources = ScanDirectoryResources();
                if (directoryResources.None())
                {
                    return;
                }

                resources.UnionWith(directoryResources);

                ResourcesLoaded?.Invoke(this, new ResourcesLoadedEventArgs(directoryResources));
            });
        }

        private Uri[] ScanDirectoryResources()
        {
            try
            {
                var fileSystemResources = new DirectoryInfo(_rootUri.LocalPath)
                    .GetFiles("*", SearchOption.AllDirectories)
                    .Select(fileInfo => new Uri(fileInfo.FullName))
                    .ToArray();

                _tracer.TraceInformation($"Resources scanned in folder '{_rootUri.LocalPath}'. Resources count: {fileSystemResources.Length}.");

                return fileSystemResources;
            }
            catch (Exception e)
            {
                e.TraceException($"Failed to scan resources in '{_rootUri.LocalPath}'.");

                throw;
            }
        }

        private static byte[] ReadAllBytes(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.None);

            var length = fileStream.Length;
            if (length > int.MaxValue)
            {
                throw new IOException("File size is too long");
            }

            var offset = 0;
            var remain = (int)length;
            var buffer = new byte[remain];
            while (remain > 0)
            {
                var data = fileStream.Read(buffer, offset, remain);
                if (data == 0)
                {
                    throw new IOException("Unexpected end of file reached");
                }

                offset += data;
                remain -= data;
            }

            return buffer;
        }

        private readonly FileSystemWatcher? _rootDirectoryWatcher;
        private readonly Uri _rootUri;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Localization);
    }
}
