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

public sealed class SelectableTextHyperlinkAutomationPeer : BaseHyperlinkAutomationPeer<SelectableTextHyperlink>
{
    public SelectableTextHyperlinkAutomationPeer(SelectableTextHyperlink owner) : base(owner)
    {
        _shownActionId = Guid.NewGuid().ToString();
    }

    protected override string GetHelpText()
    {
        var result = ElementOwnerParent.GetValue<string>(AutomationProperties.HelpTextProperty);
        if (result.IsNullOrWhiteSpace())
        {
            result = GetLocalizer().GetString("SelectableText_Ready_AutomationHelpText")!;
        }

        return result;
    }

    public void RaiseShown()
    {
#if NETCOREAPP
        if (!ListenerExists(AutomationEvents.Notification))
        {
            return;
        }

        var text = AutomationProperties.GetName(ElementOwner);
        if (text.IsNullOrEmpty())
        {
            text = ElementOwnerParent.Text;
        }

        var displayText = GetLocalizer().GetString(
            "SelectableText_Shown_AutomationName",
            new Dictionary<string, object?>()
            {
                { "Text", text },
            });

        RaiseNotificationEvent(
            AutomationNotificationKind.ItemAdded,
            AutomationNotificationProcessing.ImportantAll,
            displayText,
            _shownActionId);
#endif
    }

    private SelectableText ElementOwnerParent =>
        Guard.EnsureIsInstanceOfType<SelectableText>(ElementOwner.TemplatedParent);

    private readonly string _shownActionId;
}
