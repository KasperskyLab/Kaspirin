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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources
{
    public sealed class ResourceDescriptor : IEquatable<ResourceDescriptor>
    {
        public static ResourceDescriptor Create(string scope, string culture, string theme, string prefix)
        {
            return new ResourceDescriptor(scope, culture, theme, prefix);
        }

        private ResourceDescriptor(string scope, string culture, string theme, string prefix)
        {
            Scope = string.Intern(scope.ToLowerInvariant());
            Culture = string.Intern(culture.ToLowerInvariant());
            Theme = string.Intern(theme.ToLowerInvariant());
            Prefix = string.Intern(prefix.ToLowerInvariant());

            _hash = new(() => HashCode.Combine(Scope, Culture, Theme, Prefix));
            _toString = new(CreateToString);
        }

        public string Scope { get; }

        public string Culture { get; }

        public string Theme { get; }

        public string Prefix { get; }

        public ResourceDescriptor Clone(
            string? scope = null,
            string? culture = null,
            string? theme = null,
            string? prefix = null)
        {
            return Create(
                scope: scope ?? Scope,
                culture: culture ?? Culture,
                theme: theme ?? Theme,
                prefix: prefix ?? Prefix);
        }

        public bool Equals(ResourceDescriptor? other)
            => other != null && Scope == other.Scope
                             && Culture == other.Culture
                             && Theme == other.Theme
                             && Prefix == other.Prefix;

        public override bool Equals(object? other)
            => other is ResourceDescriptor descriptor && Equals(descriptor);

        public override int GetHashCode()
            => _hash.Value;

        public override string ToString()
            => _toString.Value;

        private string CreateToString()
        {
            var sb = new StringBuilder();

            if (Culture.IsNotEmpty())
            {
                sb.Append('/').Append(Culture);
            }

            if (Prefix.IsNotEmpty())
            {
                sb.Append('/').Append(Prefix);
            }

            if (Theme.IsNotEmpty())
            {
                sb.Append('/').Append(Theme);
            }

            if (Scope.IsNotEmpty())
            {
                sb.Append('/').Append(Scope);
            }

            return sb.ToString().ToLowerInvariant();
        }

        private readonly Lazy<int> _hash;
        private readonly Lazy<string> _toString;
    }
}
