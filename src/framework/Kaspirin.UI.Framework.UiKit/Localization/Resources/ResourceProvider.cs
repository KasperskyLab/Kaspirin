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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources;

public sealed class ResourceProvider
{
    public ResourceProvider(
        IEnumerable<IResourceBrowser> resourceBrowsers,
        LocalizationCultureInfo cultureInfo,
        string? theme = null,
        string? prefix = null)
    {
        _resourceBrowsers = Guard.EnsureArgumentIsNotNull(resourceBrowsers).ToList();
        _resourceBrowsers.ForEach(b => b.ResourcesLoaded += OnResourcesLoaded);
        _cultureInfo = Guard.EnsureArgumentIsNotNull(cultureInfo);
        _cultureInfo.CultureChanged += (_, _) => BuildSearchFlow();
        _theme = theme;
        _prefix = prefix;

        BuildSearchFlow();
    }

    public event EventHandler<ResourcesLoadedEventArgs>? ResourcesLoaded;

    public ResourceItem[] SearchResources(string scopePath)
    {
        Guard.ArgumentIsNotNull(scopePath);

        try
        {
            EnsureNotDisposed();

            return _searchFlow
                .Select(part => part.Clone(scope: scopePath))
                .SelectMany(SearchInBrowsers)
                .ToArray();
        }
        catch (Exception e)
        {
            e.ProcessAsResourceProviderException($"Failed to enumerate available resources for scope path '{scopePath}'.");
            return new ResourceItem[0];
        }
    }

    public Stream ReadResourceStream(ResourceItem resource)
    {
        Guard.ArgumentIsNotNull(resource);

        try
        {
            EnsureNotDisposed();

            return _resourceBrowsers.First(rb => rb.Contains(resource)).Read(resource);
        }
        catch (Exception e)
        {
            e.ProcessAsResourceProviderException($"Failed to read resource stream for '{resource}'.");
            return new MemoryStream();
        }
    }

    public byte[] ReadResourceBytes(ResourceItem resource)
    {
        Guard.ArgumentIsNotNull(resource);

        try
        {
            EnsureNotDisposed();

            using var resourceStream = ReadResourceStream(resource);
            using var memoryStream = new MemoryStream();

            resourceStream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
        catch (Exception e)
        {
            e.ProcessAsResourceProviderException($"Failed to read resource bytes for '{resource}'.");
            return new byte[0];
        }
    }

    public string ReadResourceString(ResourceItem resource, Encoding encoding)
    {
        Guard.ArgumentIsNotNull(resource);
        Guard.ArgumentIsNotNull(encoding);

        try
        {
            EnsureNotDisposed();

            using var resourceStream = ReadResourceStream(resource);
            using var resourceReader = new StreamReader(resourceStream, encoding);

            return resourceReader.ReadToEnd();
        }
        catch (Exception e)
        {
            e.ProcessAsResourceProviderException($"Failed to read resource string for '{resource}'.");
            return string.Empty;
        }
    }

    internal void SetTheme(string? theme)
    {
        _theme = theme;

        BuildSearchFlow();
    }

    internal void SetPrefix(string? prefix)
    {
        _prefix = prefix;

        BuildSearchFlow();
    }

    internal void Dispose()
    {
        _disposed = true;

        _resourceBrowsers.ForEach(b => b.ResourcesLoaded -= OnResourcesLoaded);
        _resourceBrowsers.ForEach(b => b.Dispose());
        _resourceBrowsers = new();
    }

    // Search resource in next order:
    // 1. /culture/prefix/theme/scope/
    // 2. /culture/prefix/scope/
    // 3. /culture/theme/scope/
    // 4. /culture/scope/
    // 5. /neutral/prefix/theme/scope/
    // 6. /neutral/prefix/scope/
    // 7. /neutral/theme/scope/
    // 8. /neutral/scope/
    private void BuildSearchFlow()
    {
        var cultureParts = GetCultureSearchParts();
        var templatedPart = GetTemplatedCultureSearchParts();
        var invariantParts = GetInvariantCultureSearchParts();

        _searchFlow = AddToSearchFlow(cultureParts)
            .Concat(AddToSearchFlow(templatedPart))
            .Concat(AddToSearchFlow(invariantParts))
            .ToList();
    }

    private IEnumerable<ResourceDescriptor> AddToSearchFlow(IEnumerable<ResourceDescriptor> parts)
    {
        if (!string.IsNullOrEmpty(_prefix))
        {
            parts = parts.SelectMany(part => new[] { part.Clone(prefix: _prefix), part });
        }

        if (!string.IsNullOrEmpty(_theme))
        {
            parts = parts.SelectMany(part => new[] { part.Clone(theme: _theme), part });
        }

        return parts;
    }

    private IEnumerable<ResourceDescriptor> GetCultureSearchParts()
    {
        return _cultureInfo.CultureParts.Select((_, i) =>
        {
            var culture = string.Join("-", _cultureInfo.CultureParts.Take(_cultureInfo.CultureParts.Length - i));

            return ResourceDescriptor.Create(
                scope: string.Empty,
                culture: culture,
                theme: string.Empty,
                prefix: string.Empty);
        });
    }

    private IEnumerable<ResourceDescriptor> GetTemplatedCultureSearchParts()
    {
#if DEBUG
        return new[]
        {
            ResourceDescriptor.Create(
                scope: string.Empty,
                culture: $"__-__-{_cultureInfo.CultureTail}",
                theme: string.Empty,
                prefix: string.Empty)
        };
#else 
        return new ResourceDescriptor[0];
#endif
    }

    private IEnumerable<ResourceDescriptor> GetInvariantCultureSearchParts()
    {
        return new[]
        {
            ResourceDescriptor.Create(
                scope: string.Empty,
                culture: InvariantCultureName,
                theme: string.Empty,
                prefix: string.Empty)
        };
    }

    private IEnumerable<ResourceItem> SearchInBrowsers(ResourceDescriptor descriptor)
    {
        return _resourceBrowsers.SelectMany(rb => SearchInBrowserSafe(rb, descriptor));
    }

    private IEnumerable<ResourceItem> SearchInBrowserSafe(IResourceBrowser resourceBrowser, ResourceDescriptor descriptor)
    {
        try
        {
            return resourceBrowser.Search(descriptor);
        }
        catch (Exception e)
        {
            e.ProcessAsResourceProviderException($"Failed to execute operation in resource browser '{resourceBrowser}'.");
            return new List<ResourceItem>();
        }
    }

    private void EnsureNotDisposed()
    {
        if (_disposed)
        {
            throw new ResourceProviderException($"{nameof(ResourceProvider)} disposed.");
        }
    }

    private void OnResourcesLoaded(object? sender, ResourcesLoadedEventArgs e)
        => ResourcesLoaded?.Invoke(sender, e);

    private const string InvariantCultureName = "neutral";

    private readonly LocalizationCultureInfo _cultureInfo;

    private List<IResourceBrowser> _resourceBrowsers = new();
    private List<ResourceDescriptor> _searchFlow = new();

    private string? _theme;
    private string? _prefix;
    private bool _disposed;
}
