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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.UiKit.Localization.LocResources
{
    public abstract class ResourceBrowserBase : IResourceBrowser
    {
        public virtual IEnumerable<Uri> Search(string searchPattern)
        {
            Guard.ArgumentIsNotNull(searchPattern);

            return GetResources().Where(res => res.AbsolutePath.IndexOf(searchPattern, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public virtual IEnumerable<Match> Query(string queryRequest)
        {
            Guard.ArgumentIsNotNull(queryRequest);

            return GetResources()
                .Select(res => Regex.Match(res.AbsolutePath, queryRequest, RegexOptions.IgnoreCase))
                .Where(m => m != Match.Empty);
        }

        public virtual bool Contains(Uri resourceUri)
        {
            Guard.ArgumentIsNotNull(resourceUri);

            return GetResources().Contains(resourceUri);
        }

        public abstract byte[] Read(Uri resourceUri);

        public IEnumerable<Uri> GetResources()
        {
            foreach (var task in _updateResourceTasks.Keys)
            {
                task.Wait();
                _updateResourceTasks.TryRemove(task, out _);
            }

            lock (_lock)
            {
                return _resources.ToArray();
            }
        }

        public abstract event EventHandler<ResourcesLoadedEventArgs>? ResourcesLoaded;

        protected void UpdateResources(Action<HashSet<Uri>> action)
        {
            Guard.ArgumentIsNotNull(action);

            lock (_lock)
            {
                action(_resources);
            }
        }

        protected void UpdateResourcesAsync(Action<HashSet<Uri>> action)
        {
            Guard.ArgumentIsNotNull(action);

            var updateTask = new TaskFactory(TaskScheduler.Default).StartNew(() => { UpdateResources(action); });
            _updateResourceTasks.TryAdd(updateTask, updateTask);
        }

        private readonly ConcurrentDictionary<Task, Task> _updateResourceTasks = new();
        private readonly HashSet<Uri> _resources = new();
        private readonly object _lock = new();
    }
}
