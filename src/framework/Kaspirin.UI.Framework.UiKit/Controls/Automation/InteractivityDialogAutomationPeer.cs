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
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class InteractivityDialogAutomationPeer : BaseFrameworkElementAutomationPeer<InteractivityDialog>
{
    public InteractivityDialogAutomationPeer(InteractivityDialog interactivityDialog)
        : base(interactivityDialog)
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
            "InteractivityDialog_Shown_AutomationName",
            new Dictionary<string, object?>()
            {
                { "Type", GetInteractivityDialogTypeDescription() },
                { "Header", GetInteractivityDialogHeader() }
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
            "InteractivityDialog_AutomationName",
            new Dictionary<string, object?>()
            {
                { "Type", GetInteractivityDialogTypeDescription() },
                { "Header", GetInteractivityDialogHeader() }
            });
    }

    protected override string GetClassNameCore()
    {
        return nameof(Window);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
        return AutomationControlType.Window;
    }

    private string? GetInteractivityDialogTypeDescription()
    {
        return GetLocalizer().GetString(
            $"{nameof(InteractivityDialog)}_{nameof(InteractivityDialog.Type)}_{ElementOwner.Type}_AutomationName");
    }

    private string? GetInteractivityDialogHeader()
    {
        return ElementOwner.GetAccessibilityText(headerProperty: InteractivityDialog.HeaderProperty);
    }

    private readonly string _shownActionId;
}