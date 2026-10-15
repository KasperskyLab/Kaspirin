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
using System.Collections.Concurrent;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Metadata;

public sealed class MetadataStorage
{
    public MetadataStorage(MetadataStorageSettings settings)
    {
        Guard.ArgumentIsNotNull(settings);

        CleanupDeferred = settings.CleanupDeferred;

        _cleanupAction = DeferredActionFactory.CreateDebouncerOnTp(CleanupMetadataInstantly, _cleanupActionDelay);
    }

    public bool CleanupDeferred { get; set; }

    public MetadataItem[] Items => _metadataStorage.Values.ToArray();

    public void Store(MetadataItem metadataItem, object metadataRefHolder)
    {
        Cleanup();

        _metadataStorage.TryAdd(new WeakReference(metadataRefHolder), metadataItem);
    }

    public void Clear()
    {
        _metadataStorage.Clear();
    }

    private void Cleanup()
    {
        if (CleanupDeferred && _metadataStorage.Count < CleanupStorageCount)
        {
            CleanupMetadataDeferred();
        }
        else
        {
            CleanupMetadataInstantly();
        }
    }

    private void CleanupMetadataInstantly()
    {
        var deadReferences = _metadataStorage.Keys.Where(k => k.Target == null).ToArray();
        foreach (var reference in deadReferences)
        {
            _metadataStorage.TryRemove(reference, out _);
        }
    }

    private void CleanupMetadataDeferred()
    {
        _cleanupAction.Execute();
    }

    private readonly ConcurrentDictionary<WeakReference, MetadataItem> _metadataStorage = new();
    private readonly IDeferredAction _cleanupAction;
    private readonly TimeSpan _cleanupActionDelay = TimeSpan.FromSeconds(30);

    private const int CleanupStorageCount = 500000;
}
