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

using System.Windows;
using System.Windows.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation.Internals;

internal sealed class CoreAutomationPeer
{
    public CoreAutomationPeer(IAutomationPeer automationPeer)
    {
        _corePeer = Guard.EnsureArgumentIsNotNull(automationPeer);
    }

    public bool IsIgnored => AccessibilityProperties.GetIsIgnored(_corePeer.Owner);

    public string? GetNameText()
        => _corePeer.Owner.GetAccessibilityText();

    public string? GetHelpText()
        => _corePeer.GetBaseHelpTextCore();

    public string? GetLocalizedControlTypeText()
        => _corePeer.GetBaseLocalizedControlTypeCore();

    public string? GetNameCore(bool withFallback = true)
    {
        if (IsIgnored)
        {
            return string.Empty;
        }

        var owner = GetOwner();

        var primaryPart = string.Empty;
        var labelPart = string.Empty;

        var labelValue = AccessibilityProperties.GetLabel(owner);
        if (labelValue is DependencyObject label)
        {
            var labelName = FrameworkObject.CreatePeerForElement(label)?.GetName();
            if (labelName.IsNotNullOrWhiteSpace())
            {
                labelPart = $"{labelName}, ";
            }
        }
        else if (labelValue is string labelString)
        {
            labelPart = $"{labelString}, ";
        }

        var name = AutomationProperties.GetName(owner);
        if (name.IsNotNullOrWhiteSpace())
        {
            primaryPart = name;
        }
        else
        {
            var labeledBy = AutomationProperties.GetLabeledBy(owner);
            if (labeledBy != null)
            {
                var labelUiElementName = FrameworkObject.CreatePeerForElement(labeledBy)?.GetName();
                if (labelUiElementName.IsNotNullOrWhiteSpace())
                {
                    primaryPart = labelUiElementName;
                }
            }

            if (primaryPart.IsNullOrWhiteSpace())
            {
                primaryPart = _corePeer.GetNameText();
            }
        }

        if (primaryPart.IsNullOrWhiteSpace() && withFallback)
        {
            primaryPart = _corePeer.GetBaseNameCore();
        }

        return labelPart + primaryPart;
    }

    public string? GetHelpTextCore()
    {
        if (IsIgnored)
        {
            return string.Empty;
        }

        var owner = GetOwner();

        var helpText = AutomationProperties.GetHelpText(owner);
        if (helpText.IsNotNullOrWhiteSpace())
        {
            return helpText;
        }

        var coreHelpText = _corePeer.GetHelpText();
        if (coreHelpText.IsNotNullOrWhiteSpace())
        {
            return coreHelpText;
        }

        return _corePeer.GetBaseHelpTextCore();
    }

    public string? GetLocalizedControlTypeCore()
    {
        if (IsIgnored)
        {
            return string.Empty;
        }

        var localizedType = _corePeer.GetLocalizedControlTypeText();
        if (localizedType.IsNotNullOrWhiteSpace())
        {
            return localizedType;
        }

        return _corePeer.GetBaseLocalizedControlTypeCore();
    }

    public Rect GetBoundingRectangleCore()
    {
        var owner = GetOwner();
        var ownerPeer = FrameworkObject.CreatePeerForElement(owner);

        if (ownerPeer is IAutomationPeer peer)
        {
            return peer.GetBaseBoundingRectangleCore();
        }

        return _corePeer.GetBaseBoundingRectangleCore();
    }

    public bool Validate()
    {
        if (IsIgnored)
        {
            return true;
        }

        var name = GetNameCore(withFallback: false);
        if (name.IsNotNullOrWhiteSpace())
        {
            return true;
        }

        var helpText = GetHelpTextCore();
        if (helpText.IsNotNullOrWhiteSpace())
        {
            return true;
        }

        return false;
    }

    public IStringLocalizer GetLocalizer()
        => LocalizationManager.GetLocalizer<IStringLocalizer>(UIKitConstants.LocalizationScope);

    private DependencyObject GetOwner()
    {
        if (_corePeer.IsTemplated)
        {
            var templateParent = new FrameworkObject(_corePeer.Owner).TemplatedParent;
            if (templateParent != null)
            {
                return templateParent;
            }
        }

        return _corePeer.Owner;
    }

    private readonly IAutomationPeer _corePeer;
}
