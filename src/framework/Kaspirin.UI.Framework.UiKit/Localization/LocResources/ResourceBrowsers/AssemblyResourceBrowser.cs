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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Localization.LocResources.ResourceBrowsers
{
    public sealed class AssemblyResourceBrowser : ResourceBrowserBase
    {
        public AssemblyResourceBrowser(string root, bool handleChanges)
            : this(root, handleChanges, new List<string>())
        { }

        public AssemblyResourceBrowser(string root, bool handleChanges, IEnumerable<string> assemblyNameOrder)
        {
            Guard.ArgumentIsNotNull(root);
            Guard.ArgumentIsNotNull(assemblyNameOrder);

            _root = new Uri(root, UriKind.Relative);
            _assemblyNameOrder = CreateAssemblyOrderList(assemblyNameOrder);

            RegisterWpfProtocol();

            if (handleChanges)
            {
                _appDomain = AppDomain.CurrentDomain;
                _appDomain.AssemblyLoad += OnAssemblyLoad;
            }

            UpdateResourcesAsync(resources =>
            {
                var newResources = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(ScanAssemblyResources);

                UpdateResources(resources, newResources);

                ResourcesLoaded?.Invoke(this, new ResourcesLoadedEventArgs(resources));
            });
        }

        public override event EventHandler<ResourcesLoadedEventArgs>? ResourcesLoaded;

        ~AssemblyResourceBrowser()
        {
            if (_appDomain != null)
            {
                _appDomain.AssemblyLoad -= OnAssemblyLoad;
            }
        }

        #region IResourceBrowser

        public override byte[] Read(Uri resourceUri)
        {
            Guard.ArgumentIsNotNull(resourceUri);

            using var ms = new MemoryStream();
            Application.GetResourceStream(resourceUri)?.Stream?.CopyTo(ms);
            return ms.ToArray();
        }

        #endregion

        private void RegisterWpfProtocol()
        {
            var _ = PackUriHelper.UriSchemePack;
        }

        private IList<string> CreateAssemblyOrderList(IEnumerable<string> assemblyNameOrder)
        {
            return assemblyNameOrder.Select(a => $"pack://application:,,,/{a};".ToLowerInvariant()).ToList();
        }

        private void OnAssemblyLoad(object? sender, AssemblyLoadEventArgs args)
        {
            UpdateResourcesAsync(resources =>
            {
                var assemblyResources = ScanAssemblyResources(args.LoadedAssembly);
                if (assemblyResources.None())
                {
                    return;
                }

                UpdateResources(resources, assemblyResources);

                ResourcesLoaded?.Invoke(this, new ResourcesLoadedEventArgs(assemblyResources));
            });
        }

        private Uri[] ScanAssemblyResources(Assembly assembly)
        {
            if (ShouldSkipAssembly(assembly))
            {
#if NETCOREAPP
                return Array.Empty<Uri>();
#else
                return new Uri[0];
#endif
            }

            try
            {
                var assemblyResources = assembly
                    .GetManifestResourceNames()
                    .Where(r => r.EndsWith(".resources"))
                    .Select(r => r.Replace(".resources", ""))
                    .Select(r => new ResourceManager(r, assembly))
                    .SelectMany(rm => rm.GetResourceSet(CultureInfo.InvariantCulture, true, false)?.Cast<DictionaryEntry>() ?? new DictionaryEntry[0])
                    .Select(rkv => rkv.Key.ToString())
                    .Where(key => key?.StartsWith(_root.OriginalString.ToLowerInvariant()) == true)
                    .Select(key => new Uri($"pack://application:,,,/{assembly.GetName().Name};component/{key}", UriKind.Absolute))
                    .ToArray();

                _tracer.TraceInformation($"Resources scanned in assembly '{assembly.FullName}'. Resources count: {assemblyResources.Length}.");

                return assemblyResources;
            }
            catch (Exception e)
            {
                e.TraceException($"Failed to scan resources in '{assembly.FullName}'.");

                throw;
            }
        }

        private void UpdateResources(HashSet<Uri> set, IEnumerable<Uri> resources)
        {
            set.UnionWith(resources);

            var orderedList = new List<Uri>();
            var unorderedList = set.ToList();

            foreach (var assemblyName in _assemblyNameOrder)
            {
                var resourcesInAssembly = unorderedList
                    .Where(uri => uri.ToString().ToLowerInvariant().Contains(assemblyName))
                    .Where(uri => orderedList.Contains(uri) == false)
                    .ToList();

                orderedList.AddRange(resourcesInAssembly);
                unorderedList = unorderedList.Except(resourcesInAssembly).ToList();
            }

            orderedList.AddRange(unorderedList);

            set.Clear();
            set.UnionWith(orderedList);
        }

        private static bool ShouldSkipAssembly(Assembly assembly)
        {
            var assemblyName = assembly.FullName ?? string.Empty;
            var assemblyPath = assembly.IsDynamic ? string.Empty : assembly.Location;

            if (assemblyName.StartsWith("Microsoft.", StringComparison.CurrentCultureIgnoreCase) ||
                assemblyPath.Contains("Microsoft.NETCore.App", StringComparison.CurrentCultureIgnoreCase) ||
                assemblyPath.Contains("Microsoft.AspNetCore.App", StringComparison.CurrentCultureIgnoreCase) ||
                assemblyPath.Contains("Microsoft.WindowsDesktop.App", StringComparison.CurrentCultureIgnoreCase) ||
#if NET40
                assembly.GlobalAssemblyCache ||
#endif
                assembly.IsDynamic)
            {
                return true;
            }

            return false;
        }

        private readonly Uri _root;
        private readonly IList<string> _assemblyNameOrder;
        private readonly AppDomain? _appDomain;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Localization);
    }
}
