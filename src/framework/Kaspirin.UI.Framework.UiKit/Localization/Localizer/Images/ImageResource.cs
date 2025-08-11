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
using System.Globalization;
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Images;

public sealed class ImageResource
{
    public ImageResource(ResourceItem imageResource)
    {
        var imageUri = imageResource.Uri.RelativeUri.OriginalString;
        var imageDescription = imageResource.Descriptor.ToString();

        var trimmedUri = imageUri.Substring(imageDescription.Length);

        var imageMatch = _imageRegex.Match(trimmedUri);
        if (imageMatch.Success)
        {
            Scale = imageMatch.Groups["scale"].Success ? double.Parse(imageMatch.Groups["scaleValue"].Value, NumberFormatInfo.InvariantInfo) : 100;
            Name = Guard.EnsureIsNotNull(imageMatch.Groups["name"].Value);
            Resource = imageResource;
        }
        else
        {
            throw new InvalidOperationException($"Failed to parse image uri {imageResource.Uri}");
        }
    }

    public string Name { get; }

    public double Scale { get; }

    public ResourceItem Resource { get; }

    private static readonly Regex _imageRegex = new(@"^(?<scale>/scale-(?<scaleValue>\d*))?/(?<name>.*\.\w*)$", RegexOptions.Compiled);
}