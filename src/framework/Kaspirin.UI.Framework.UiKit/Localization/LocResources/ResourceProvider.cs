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
using System.Linq;
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.LocResources
{
    public sealed class ResourceProvider : IResourceProvider
    {
        public ResourceProvider(IList<IResourceBrowser> resourceBrowsers, LocalizationCultureInfo cultureInfo, string? theme = null)
        {
            _resourceBrowsers = Guard.EnsureArgumentIsNotNull(resourceBrowsers);
            _cultureInfo = Guard.EnsureArgumentIsNotNull(cultureInfo);

            Theme = theme;

            BuildSearchFlow();
            Subscribe();
        }

        public string? Theme { get; private set; }

        public void SetTheme(string? theme)
        {
            Theme = theme;

            BuildSearchFlow();
        }

        public IList<Match> QueryResources(string queryRequest)
        {
            Guard.ArgumentIsNotNull(queryRequest);

            return QueryInBrowsers(queryRequest).ToList();
        }

        public IList<Uri> SearchResources(Uri searchUri)
        {
            Guard.ArgumentIsNotNull(searchUri);

            try
            {
                return searchUri.IsAbsoluteUri
                    ? SearchAbsoluteMatches(searchUri.AbsolutePath)
                    : SearchRelativeMatches(searchUri.OriginalString);
            }
            catch (Exception e)
            {
                e.ProcessAsResourceProviderException($"Failed to enumerate available resources for uri='{searchUri}'.");
                return new Uri[0];
            }
        }

        public byte[] ReadResource(Uri resourceUri)
        {
            Guard.ArgumentIsNotNull(resourceUri);

            try
            {
                return _resourceBrowsers.First(rb => rb.Contains(resourceUri)).Read(resourceUri);
            }
            catch (Exception e)
            {
                e.ProcessAsResourceProviderException($"Failed to read resource bytes for uri='{resourceUri}'.");
                return new byte[0];
            }
        }

        public IEnumerable<Uri> GetResources() => _resourceBrowsers.SelectMany(rb => rb.GetResources());

        public event EventHandler<ResourcesLoadedEventArgs>? ResourcesLoaded;

        private IList<Uri> SearchAbsoluteMatches(string absolutePath)
        {
            Guard.ArgumentIsNotNull(absolutePath);

            return SearchInBrowsers(absolutePath).ToList();
        }

        private IList<Uri> SearchRelativeMatches(string relativePath)
        {
            return _searchFlow
                .Select(part => $"/{part}/{relativePath}")
                .SelectMany(SearchInBrowsers)
                .ToList();
        }

        // Search resource in next order:
        // 1. /culture/theme/scope/name
        // 2. /culture/scope/name
        // 3. /neutral/theme/scope/name
        // 4. /neutral/scope/name
        private void BuildSearchFlow()
        {
            _searchFlow = new();

            AddToSearchFlow(GetCultureSearchParts());
#if DEBUG
            AddToSearchFlow(new[] { $"__-__-{_cultureInfo.CultureTail}" });
#endif
            AddToSearchFlow(new[] { InvariantCultureName });
        }

        private void AddToSearchFlow(IEnumerable<string> parts)
        {
            var partsWithTheme = string.IsNullOrEmpty(Theme)
                ? parts
                : parts.SelectMany(part => new[] { $"{part}/{Theme}", part });

            _searchFlow.AddRange(partsWithTheme);
        }

        private IEnumerable<string> GetCultureSearchParts()
        {
            return _cultureInfo.CultureParts.Select(
                (_, i) => string.Join("-", _cultureInfo.CultureParts.Take(_cultureInfo.CultureParts.Length - i)));
        }

        private IEnumerable<Uri> SearchInBrowsers(string searchRequest)
        {
            return ExecuteInBrowsers(resourceBrowser => resourceBrowser.Search(searchRequest));
        }

        private IEnumerable<Match> QueryInBrowsers(string queryRequest)
        {
            return ExecuteInBrowsers(resourceBrowser => resourceBrowser.Query(queryRequest));
        }

        private IEnumerable<TResult> ExecuteInBrowsers<TResult>(Func<IResourceBrowser, IEnumerable<TResult>> action)
        {
            return _resourceBrowsers
                .SelectMany(resourceBrowser =>
                {
                    try
                    {
                        return action(resourceBrowser);
                    }
                    catch (Exception e)
                    {
                        e.ProcessAsResourceProviderException($"Failed to execute operation in resource browser '{resourceBrowser}'.");
                        return new List<TResult>();
                    }
                });
        }

        private void Subscribe()
        {
            foreach (var resourceBrowser in _resourceBrowsers)
            {
                resourceBrowser.ResourcesLoaded += OnResourcesLoaded;
            }
        }

        private void OnResourcesLoaded(object? sender, ResourcesLoadedEventArgs e) => ResourcesLoaded?.Invoke(sender, e);

        private const string InvariantCultureName = "neutral";
        private readonly IList<IResourceBrowser> _resourceBrowsers;
        private readonly LocalizationCultureInfo _cultureInfo;
        private List<string> _searchFlow = new();
    }
}
