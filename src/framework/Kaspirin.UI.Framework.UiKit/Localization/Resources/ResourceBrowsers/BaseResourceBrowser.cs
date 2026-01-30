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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources.ResourceBrowsers;

public abstract class BaseResourceBrowser : IResourceBrowser
{
    public event EventHandler<ResourcesLoadedEventArgs>? ResourcesLoaded;

    public IEnumerable<ResourceItem> Search(ResourceDescriptor descriptor)
    {
        Guard.ArgumentIsNotNull(descriptor);

        return GetResources()
            .Where(res => Match(res.RelativeUri, descriptor))
            .Select(res => GetResourceItem(res, descriptor));
    }

    public bool Contains(ResourceItem resource)
    {
        Guard.ArgumentIsNotNull(resource);

        return _resourceItems.Values.Contains(resource);
    }

    public Stream Read(ResourceItem resource)
        => Read(resource.Uri);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected abstract Stream Read(ResourceUri uri);

    protected virtual void Dispose(bool disposing) { }

    protected virtual bool Match(Uri relativeUri, ResourceDescriptor descriptor)
    {
        return relativeUri.OriginalString.StartsWith(descriptor.ToString(), StringComparison.Ordinal);
    }

    protected void UpdateResourcesAsync(Action<HashSet<ResourceUri>> action)
    {
        Guard.ArgumentIsNotNull(action);

        var updateTask = Executers.InTpAsync(() => UpdateResources(action));
        _updateResourceTasks.TryAdd(updateTask, updateTask);
    }

    protected void RaiseResourcesLoaded(IEnumerable<ResourceUri> resources)
        => ResourcesLoaded?.Invoke(this, new ResourcesLoadedEventArgs(resources));

    private IEnumerable<ResourceUri> GetResources()
    {
        foreach (var task in _updateResourceTasks.Keys)
        {
            task.Wait();
            _updateResourceTasks.TryRemove(task, out _);
        }

        lock (_lock)
        {
            return _resourceUris.ToArray();
        }
    }

    private void UpdateResources(Action<HashSet<ResourceUri>> action)
    {
        Guard.ArgumentIsNotNull(action);

        lock (_lock)
        {
            action(_resourceUris);
        }
    }

    private ResourceItem GetResourceItem(ResourceUri uri, ResourceDescriptor descriptor)
        => _resourceItems.GetOrAdd(uri, new ResourceItem(uri, descriptor));

    private readonly ConcurrentDictionary<Task, Task> _updateResourceTasks = new();
    private readonly ConcurrentDictionary<ResourceUri, ResourceItem> _resourceItems = new();
    private readonly HashSet<ResourceUri> _resourceUris = new();
    private readonly object _lock = new();
}