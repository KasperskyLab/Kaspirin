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

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources.ResourceBrowsers;

public sealed class AssemblyResourceBrowser : BaseResourceBrowser
{
    public AssemblyResourceBrowser(string root, bool handleChanges)
        : this(root, handleChanges, new List<string>())
    { }

    public AssemblyResourceBrowser(string root, bool handleChanges, IEnumerable<string> assemblyNameOrder)
    {
        Guard.ArgumentIsNotNullOrEmpty(root);

        _root = NormalizeRoot(root);
        _assemblyNameOrder = CreateAssemblyOrderList(assemblyNameOrder);

        RegisterWpfProtocol();

        if (handleChanges)
        {
            _appDomain = AppDomain.CurrentDomain;
            _appDomain.AssemblyLoad += OnAssemblyLoad;
        }

        UpdateResourcesAsync(resources => AppDomain.CurrentDomain.GetAssemblies().ForEach(a => ScanResources(resources, a)));
    }

    protected override Stream Read(ResourceUri uri)
    {
        return Application.GetResourceStream(uri.AbsoluteUri).Stream;
    }

    protected override void Dispose(bool disposing)
    {
        if (_appDomain != null && disposing)
        {
            _appDomain.AssemblyLoad -= OnAssemblyLoad;
        }
    }

    private void RegisterWpfProtocol()
    {
        var _ = PackUriHelper.UriSchemePack;
    }

    private void OnAssemblyLoad(object? sender, AssemblyLoadEventArgs args)
    {
        UpdateResourcesAsync(resources => ScanResources(resources, args.LoadedAssembly));
    }

    private void ScanResources(HashSet<ResourceUri> resources, Assembly assembly)
    {
        try
        {
            if (ShouldSkipAssembly(assembly))
            {
                return;
            }

            _tracer.TraceDebug($"Resources scanning in assembly '{assembly.FullName}' in folder '{_root}'.");

            var assemblyResources = ScanAssemblyResources(assembly);
            if (assemblyResources.Any())
            {
                _tracer.TraceInformation($"Resources found in assembly '{assembly.FullName}'. Resources count: {assemblyResources.Length}.");

                UpdateResources(resources, assemblyResources);

                RaiseResourcesLoaded(assemblyResources);
            }
        }
        catch (Exception e)
        {
            e.TraceException($"Failed to scan resources in assembly '{assembly.FullName}' in folder '{_root}'.");

            throw;
        }
    }

    private ResourceUri[] ScanAssemblyResources(Assembly assembly)
    {
        return assembly
            .GetManifestResourceNames()
            .Where(r => r.EndsWith(".resources"))
            .Select(r => r.Replace(".resources", ""))
            .Select(r => new ResourceManager(r, assembly))
            .SelectMany(rm => rm.GetResourceSet(CultureInfo.InvariantCulture, true, false)?.Cast<DictionaryEntry>() ?? new DictionaryEntry[0])
            .Select(rkv => rkv.Key.ToString())
            .Where(key => key?.StartsWith(_root.ToLowerInvariant()) == true)
            .WhereNotNull()
            .Select(key => CreateUri(key, assembly, _root))
            .ToArray();
    }

    private void UpdateResources(HashSet<ResourceUri> set, IEnumerable<ResourceUri> resources)
    {
        set.UnionWith(resources);

        if (_assemblyNameOrder.Count == 0)
        {
            return;
        }

        var orderedList = new List<ResourceUri>();
        var unorderedList = set.ToList();

        foreach (var assemblyName in _assemblyNameOrder)
        {
            var resourcesInAssembly = unorderedList
                .Where(uri => uri.AbsoluteUri.ToString().ToLowerInvariant().Contains(assemblyName))
                .Where(uri => orderedList.Contains(uri) == false)
                .ToList();

            orderedList.AddRange(resourcesInAssembly);
            unorderedList = unorderedList.Except(resourcesInAssembly).ToList();
        }

        orderedList.AddRange(unorderedList);

        set.Clear();
        set.UnionWith(orderedList);
    }

    private static ResourceUri CreateUri(string key, Assembly assembly, string rootFolder)
    {
        return new ResourceUri(
            absoluteUri: new Uri($"pack://application:,,,/{assembly.GetName().Name};component/{key}", UriKind.Absolute),
            relativeUri: new Uri(key.Substring(rootFolder.Length - 1).ToLowerInvariant(), UriKind.Relative));
    }

    private static string NormalizeRoot(string root)
    {
        if (root.EndsWith("/"))
        {
            return root;
        }

        if (root.EndsWith("\\"))
        {
            return root.Replace("\\", "/");
        }

        return new(root.Append('/').ToArray());
    }

    private static IList<string> CreateAssemblyOrderList(IEnumerable<string> assemblyNameOrder)
    {
        return assemblyNameOrder.Select(a => $"pack://application:,,,/{a};".ToLowerInvariant()).ToList();
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

    private readonly string _root;
    private readonly IList<string> _assemblyNameOrder;
    private readonly AppDomain? _appDomain;

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Localization);
}
