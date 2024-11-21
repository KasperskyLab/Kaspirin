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
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer
{
    public sealed class ScopeMetainfo
    {
        public ScopeMetainfo(string scopePattern, string scope, string fallbackScope)
        {
            Guard.ArgumentIsNotNull(scopePattern);
            Guard.ArgumentIsNotNull(scope);
            Guard.ArgumentIsNotNull(fallbackScope);

            Scope = scope;
            FallbackScope = fallbackScope;
            ScopeLookups = BuildScopeLookups(scopePattern);
            ScopeType = Path.GetExtension(ScopeLookups.First().OriginalString) == string.Empty ? ScopeType.Directory : ScopeType.File;
        }

        public string Scope { get; }
        public string FallbackScope { get; }

        public ScopeType ScopeType { get; }

        public IEnumerable<Uri> ScopeLookups { get; }

        public Uri ConstraintScopeUri(Uri scopeUri)
        {
            Guard.ArgumentIsNotNull(scopeUri);

            return ScopeType == ScopeType.Directory
                ? TrimScopeToDirectory(scopeUri)
                : scopeUri;
        }

        private IList<Uri> BuildScopeLookups(string scopePattern)
        {
            Guard.ArgumentIsNotNull(scopePattern);
            Guard.Assert(scopePattern.Contains(LocalizerSettings.ScopeUriMarker),
                $"{nameof(LocalizerSettings.ScopeUriPatterns)} must contains marker '{LocalizerSettings.ScopeUriMarker}'");

            var scopeUri = Regex.Replace(scopePattern, ScopeMarker, Scope);

            var lookups = new List<Uri>();

            var extensionMatch = Regex.Match(scopePattern, ExtensionMarker);
            if (extensionMatch.Success)
            {
                lookups.AddRange(extensionMatch
                    .Groups["Extension"]
                    .Captures.Cast<Capture>()
                    .Select(c => new Uri(Regex.Replace(scopeUri, ExtensionMarker, c.Value), UriKind.Relative))
                    .ToList());
            }
            else
            {
                lookups.Add(new(scopeUri, UriKind.Relative));
            }

            return lookups;
        }

        private Uri TrimScopeToDirectory(Uri scopeUri)
        {
            var trimSplitString = ScopeLookups.Single().OriginalString;
            var trimStartIndex = scopeUri.AbsoluteUri.IndexOf(trimSplitString, StringComparison.OrdinalIgnoreCase);
            return trimStartIndex switch
            {
                < 0 => new Uri(GetDirectoryPath(scopeUri.AbsoluteUri), UriKind.Absolute),
                _ => new Uri(scopeUri.AbsoluteUri.Remove(trimStartIndex + trimSplitString.Length), UriKind.Absolute)
            };
        }

        private static string GetDirectoryPath(string path)
        {
            var index = path.LastIndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            if (index < 0)
                return path;

            if (index == path.Length - 1)
                return path;

            return path.Substring(0, index + 1);
        }

        private const string ExtensionMarker = @"\(((?<Extension>[\w]+)\|?)+\)$";
        private const string ScopeMarker = @"(?<Scope>\{[Ss]cope\})";
    }
}
