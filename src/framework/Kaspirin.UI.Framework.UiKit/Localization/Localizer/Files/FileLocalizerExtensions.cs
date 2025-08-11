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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Files;

public static class FileLocalizerExtensions
{
    public static bool TryGetContent(this IFileLocalizer localizer, string key, [NotNullWhen(true)] out byte[]? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetContent(key));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetStream(this IFileLocalizer localizer, string key, [NotNullWhen(true)] out Stream? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetStream(key));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetText(this IFileLocalizer localizer, string key, Encoding encoding, [NotNullWhen(true)] out string? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetText(key, encoding));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetUri(this IFileLocalizer localizer, string key, [NotNullWhen(true)] out Uri? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetUri(key));
            return true;
        }

        result = null;
        return false;
    }
}
