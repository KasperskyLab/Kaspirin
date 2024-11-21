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
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using IResourceProvider = Kaspirin.UI.Framework.UiKit.Localization.LocResources.IResourceProvider;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Images
{
    public sealed class ImageScope : IScope
    {
        public ImageScope(Uri scopeUri, IResourceProvider resourceProvider)
        {
            ScopeUri = Guard.EnsureArgumentIsNotNull(scopeUri);

            _resources = resourceProvider.SearchResources(scopeUri)
                .GroupBy(fileUri => fileUri.Segments.Last().ToLowerInvariant())
                .ToDictionary(g => g.Key, g => g.ToDictionary(GetScaleFromUri));
        }

        public Uri ScopeUri { get; }

        public IEnumerable<string> Keys => _resources.Keys;

        public object GetValue(string key)
        {
            Guard.ArgumentIsNotNull(key);

            var currentScale = GetCurrentScale();
            return _resources[key.ToLowerInvariant()].OrderBy(g => g.Key).First(g => g.Key.LesserOrNearlyEqual(currentScale)).Value;
        }

        private static double GetScaleFromUri(Uri uri)
        {
            var match = Regex.Match(uri.AbsolutePath, @"/scale-(\d*)/");
            return match.Success
                ? double.Parse(match.Groups[1].Value, NumberFormatInfo.InvariantInfo)
                : 100;
        }

        private static int GetCurrentScale()
        {
            return (int)Math.Round(GetCurrentDpi() / StandardDpi * 100);
        }

        private static double GetCurrentDpi()
        {
            using var g = Graphics.FromHwnd(IntPtr.Zero);
            return g.DpiX;
        }

        private const double StandardDpi = 96;

        private readonly Dictionary<string, Dictionary<double, Uri>> _resources;
    }
}
