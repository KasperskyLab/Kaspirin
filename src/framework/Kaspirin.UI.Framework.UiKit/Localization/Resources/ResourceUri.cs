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

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources;

public sealed class ResourceUri : IEquatable<ResourceUri>
{
    public static readonly ResourceUri[] EmptyArray = new ResourceUri[0];

    public ResourceUri(Uri absoluteUri, Uri relativeUri)
    {
        AbsoluteUri = absoluteUri;
        RelativeUri = relativeUri;
    }

    public Uri AbsoluteUri { get; }

    public Uri RelativeUri { get; }

    public bool Equals(ResourceUri? other)
        => other != null &&
            AbsoluteUri.Equals(other.AbsoluteUri) &&
            RelativeUri.Equals(other.RelativeUri);

    public override bool Equals(object? other)
        => other is ResourceUri uri && Equals(uri);

    public override int GetHashCode()
        => HashCode.Combine(AbsoluteUri, RelativeUri);

    public override string ToString()
        => $"AbsoluteUri:{AbsoluteUri}; RelativeUri:{RelativeUri}";
}
