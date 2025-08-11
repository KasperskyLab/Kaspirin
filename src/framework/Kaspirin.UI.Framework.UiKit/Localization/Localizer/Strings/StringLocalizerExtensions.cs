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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings;

public static class StringLocalizerExtensions
{
    public static string? GetString(this IStringLocalizer localizer, string key, string paramName, object? paramValue, IStringLocalizerOption? option = null)
    {
        return localizer.GetString(key, new StringParamResolver(paramName, paramValue), option);
    }

    public static string? GetString(this IStringLocalizer localizer, string key, IDictionary<string, Lazy<object?>> parameters, IStringLocalizerOption? option = null)
    {
        return localizer.GetString(key, new StringParamResolver(parameters), option);
    }

    public static string? GetString(this IStringLocalizer localizer, string key, Func<string, object?> parameters, IStringLocalizerOption? option = null)
    {
        return localizer.GetString(key, new StringParamResolver(parameters), option);
    }

    public static bool TryGetString(this IStringLocalizer localizer, string key, [NotNullWhen(true)] out string? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetString(key));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetString(this IStringLocalizer localizer, string key, IDictionary<string, object?> parameters, [NotNullWhen(true)] out string? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetString(key, parameters));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetString(this IStringLocalizer localizer, string key, IDictionary<string, Lazy<object?>> parameters, [NotNullWhen(true)] out string? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetString(key, parameters));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetString(this IStringLocalizer localizer, string key, Func<string, object?> parameters, [NotNullWhen(true)] out string? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetString(key, parameters));
            return true;
        }

        result = null;
        return false;
    }

    public static bool TryGetString(this IStringLocalizer localizer, string key, IStringParamResolver paramResolver, [NotNullWhen(true)] out string? result)
    {
        if (localizer.ContainsKey(key))
        {
            result = Guard.EnsureIsNotNull(localizer.GetString(key, paramResolver));
            return true;
        }

        result = null;
        return false;
    }
}
