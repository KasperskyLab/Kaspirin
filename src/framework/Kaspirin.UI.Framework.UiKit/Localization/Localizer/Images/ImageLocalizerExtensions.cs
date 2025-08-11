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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Images;

public static class ImageLocalizerExtensions
{
    public static bool TryGetBitmapImage(this IImageLocalizer localizer, string key, [NotNullWhen(true)] out BitmapImage? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetBitmapImage(key));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetBitmapFrame(this IImageLocalizer localizer, string key, [NotNullWhen(true)] out BitmapFrame? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetBitmapFrame(key));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetBitmapFrame(this IImageLocalizer localizer, string key, Size frameSize, [NotNullWhen(true)] out BitmapFrame? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetBitmapFrame(key, frameSize));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetSvgImage(this IImageLocalizer localizer, string key, [NotNullWhen(true)] out DrawingImage? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetSvgImage(key));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetUri(this IImageLocalizer localizer, string key, [NotNullWhen(true)] out Uri? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetUri(key));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetStream(this IImageLocalizer localizer, string key, [NotNullWhen(true)] out Stream? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetStream(key));
            return true;
        }

        result = null;
        return false;
    }
}
