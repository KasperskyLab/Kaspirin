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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer;

public sealed class ScopeMetaInfo
{
    public ScopeMetaInfo(string scopePattern, string scope, string fallbackScope)
    {
        Guard.ArgumentIsNotNull(scopePattern);
        Guard.ArgumentIsNotNull(scope);
        Guard.ArgumentIsNotNull(fallbackScope);

        Scope = scope;
        FallbackScope = fallbackScope;
        ScopePaths = BuildScopePaths(scopePattern);
        ScopeType = Path.GetExtension(ScopePaths.First()).IsEmpty()
            ? ScopeType.Directory
            : ScopeType.File;
    }

    public string Scope { get; }

    public string FallbackScope { get; }

    public ScopeType ScopeType { get; }

    public IEnumerable<string> ScopePaths { get; }

    public string ConstraintScopePath(string scopePath)
    {
        Guard.ArgumentIsNotNull(scopePath);

        return ScopeType == ScopeType.Directory
            ? TrimScopeToDirectory(scopePath)
            : scopePath;
    }

    private IList<string> BuildScopePaths(string scopePattern)
    {
        Guard.ArgumentIsNotNull(scopePattern);
        Guard.Assert(scopePattern.Contains(LocalizerSettings.ScopeUriMarker),
            $"{nameof(LocalizerSettings.ScopeUriPatterns)} must contains marker '{LocalizerSettings.ScopeUriMarker}'");

        var scopePath = Regex.Replace(scopePattern, ScopeMarker, Scope);

        var lookups = new List<string>();

        var extensionMatch = Regex.Match(scopePattern, ExtensionMarker);
        if (extensionMatch.Success)
        {
            lookups.AddRange(extensionMatch
                .Groups["Extension"]
                .Captures.Cast<Capture>()
                .Select(c => Regex.Replace(scopePath, ExtensionMarker, c.Value))
                .ToList());
        }
        else
        {
            lookups.Add(scopePath);
        }

        return lookups;
    }

    private string TrimScopeToDirectory(string scopePath)
    {
        var trimSplitString = ScopePaths.Single();
        var trimStartIndex = scopePath.IndexOf(trimSplitString, StringComparison.OrdinalIgnoreCase);
        return trimStartIndex switch
        {
            < 0 => GetDirectoryPath(scopePath),
            _ => scopePath.Remove(trimStartIndex + trimSplitString.Length)
        };
    }

    private static string GetDirectoryPath(string path)
    {
        var index = path.LastIndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
        if (index < 0)
        {
            return path;
        }

        if (index == path.Length - 1)
        {
            return path;
        }

        return path.Substring(0, index + 1);
    }

    private const string ExtensionMarker = @"\(((?<Extension>[\w]+)\|?)+\)$";
    private const string ScopeMarker = @"(?<Scope>\{[Ss]cope\})";
}
