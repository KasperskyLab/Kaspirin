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

using System.Collections.Generic;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class NavigationMenuButtonAutomationPeer : NavigationMenuButtonAutomationPeer<NavigationMenuButton>
{
    public NavigationMenuButtonAutomationPeer(NavigationMenuButton owner) : base(owner)
    {
    }

    protected override string? GetNameText()
    {
        return GetLocalizer().GetString(
            "NavigationMenuButton_AutomationName",
            new Dictionary<string, object?>()
            {
                { "Caption", GetCaption() },
                { "Counter",  GetCounter() }
            });
    }

    protected override string? GetHelpText()
    {
        var description = $"{ElementOwner.Description}";
        var type = GetBadgeTypeDescription();

        if (description.IsNotNullOrEmpty() || type.IsNotNullOrEmpty())
        {
            return GetLocalizer().GetString(
                "NavigationMenuButton_AutomationHelpText",
                new Dictionary<string, object?>()
                {
                    { "Description", description },
                    { "BadgeType", type }
                });
        }

        return string.Empty;
    }

    protected override string GetClassNameCore()
        => nameof(NavigationMenuButton);

    private string? GetCaption()
    {
        return ElementOwner.GetAccessibilityText(
            headerProperty: NavigationMenuButton.CaptionProperty,
            options: AccessibilityTextOptions.IgnoreFallback);
    }

    private string? GetCounter()
    {
        return ElementOwner.ShowCounter ? $"{ElementOwner.Counter}" : null;
    }
}
