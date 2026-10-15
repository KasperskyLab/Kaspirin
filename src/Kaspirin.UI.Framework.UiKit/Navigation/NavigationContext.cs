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

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class NavigationContext
{
    internal NavigationContext(Region region, NavigationDirection direction, string? targetViewName, RegionView? targetView, RegionView? activeView)
    {
        Region = region;
        Direction = direction;
        TargetViewName = targetViewName ?? targetView?.Name ?? "<none>";
        TargetView = targetView;
        ActiveView = activeView;
    }

    public Region Region { get; }

    public NavigationDirection Direction { get; }

    public string TargetViewName { get; }

    public RegionView? TargetView { get; }

    public RegionView? ActiveView { get; }
}
