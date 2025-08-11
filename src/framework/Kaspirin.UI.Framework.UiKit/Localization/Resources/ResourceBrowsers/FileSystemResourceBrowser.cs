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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources.ResourceBrowsers;

public sealed class FileSystemResourceBrowser : BaseResourceBrowser
{
    public FileSystemResourceBrowser(string root, bool handleChanges)
    {
        _root = Guard.EnsureArgumentIsNotNullOrEmpty(root);

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
        return new FileStream(uri.AbsoluteUri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.None);
    }

    protected override void Dispose(bool disposing)
    {
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
            _tracer.TraceDebug($"Resources scanning in folder '{_root}'.");

            var fileSystemResources = new DirectoryInfo(_root)
                .GetFiles("*", SearchOption.AllDirectories)
                .Select(fileInfo => CreateUri(fileInfo.FullName, _root))
                .ToArray();

            if (fileSystemResources.Length > 0)
            {
                _tracer.TraceInformation($"Resources found in folder '{_root}'. Resources count: {fileSystemResources.Length}.");

                resources.Clear();
                resources.UnionWith(fileSystemResources);

                RaiseResourcesLoaded(fileSystemResources);
            }
        }
        catch (Exception e)
        {
            e.TraceException($"Failed to scan resources in folder '{_root}'.");

            throw;
        }
    }

    private static ResourceUri CreateUri(string fullPath, string rootPath)
    {
        return new ResourceUri(
            absoluteUri: new Uri(fullPath, UriKind.Absolute),
            relativeUri: new Uri(fullPath.Replace(rootPath, string.Empty).Replace('\\', '/').ToLowerInvariant(), UriKind.Relative));
    }

    private readonly FileSystemWatcher? _rootDirectoryWatcher;
    private readonly string _root;

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Localization);
}
