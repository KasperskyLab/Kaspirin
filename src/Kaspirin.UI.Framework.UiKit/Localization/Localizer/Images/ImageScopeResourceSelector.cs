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

#pragma warning disable CA1416 // This call site is reachable on all platforms.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Images;

public sealed class ImageScopeResourceSelector : IScopeResourceSelector
{
    public ImageScopeResourceSelector(IEnumerable<ResourceItem> resources)
    {
        _resources = resources.Select(res => new ImageResource(res))
            .OrderBy(g => g.Scale)
            .ToArray();
    }

    public ResourceItem Select()
    {
        var currentScale = GetCurrentScale();

        return _resources.First(g => g.Scale.LesserOrNearlyEqual(currentScale)).Resource;
    }

    private static int GetCurrentScale()
    {
        using var g = Graphics.FromHwnd(IntPtr.Zero);

        return (int)Math.Round(g.DpiX / Dpi.Default.X * 100);
    }

    private readonly ImageResource[] _resources;
}
