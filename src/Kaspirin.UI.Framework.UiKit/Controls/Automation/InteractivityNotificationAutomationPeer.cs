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
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class InteractivityNotificationAutomationPeer : BaseFrameworkElementAutomationPeer<InteractivityNotification>
{
    public InteractivityNotificationAutomationPeer(InteractivityNotification interactivityNotification)
        : base(interactivityNotification)
    {
        _shownActionId = Guid.NewGuid().ToString();
    }

    public void RaiseShown()
    {
#if NETCOREAPP
        if (!ListenerExists(AutomationEvents.Notification))
        {
            return;
        }

        var displayText = GetLocalizer().GetString(
            "InteractivityNotification_Shown_AutomationName",
            new Dictionary<string, object?>()
            {
                { "Type", GetInteractivityNotificationTypeDescription() },
                { "Text", GetInteractivityNotificationText() }
            });

        RaiseNotificationEvent(
            AutomationNotificationKind.ItemAdded,
            AutomationNotificationProcessing.ImportantAll,
            displayText,
            _shownActionId);
#endif
    }

    protected override string? GetNameText()
    {
        return GetLocalizer().GetString(
            "InteractivityNotification_AutomationName",
            new Dictionary<string, object?>()
            {
                { "Type", GetInteractivityNotificationTypeDescription() },
                { "Text", GetInteractivityNotificationText() }
            });
    }

    protected override string GetClassNameCore()
    {
        return nameof(InteractivityNotification);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
        return AutomationControlType.Window;
    }

    private string? GetInteractivityNotificationTypeDescription()
    {
        return GetLocalizer().GetString(
            $"{nameof(InteractivityNotification)}_{nameof(InteractivityNotification.Type)}_{ElementOwner.Type}_AutomationName");
    }

    private string? GetInteractivityNotificationText()
    {
        return ElementOwner.GetAccessibilityText(contentProperty: InteractivityNotification.ContentProperty);
    }

    private readonly string _shownActionId;
}