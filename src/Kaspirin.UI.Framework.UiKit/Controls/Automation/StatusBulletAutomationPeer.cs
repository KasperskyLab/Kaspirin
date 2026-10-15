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
using System.Windows.Automation.Peers;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class StatusBulletAutomationPeer : BaseFrameworkElementAutomationPeer<StatusBullet>
{
    public StatusBulletAutomationPeer(StatusBullet statusBullet) : base(statusBullet)
    { }

    protected override string? GetNameText()
    {
        return GetLocalizer().GetString(
            "StatusBullet_AutomationName",
            new Dictionary<string, object?>()
            {
                { "Type", GetTypeParameter() },
                { "Message", GetMessageParameter() }
            });
    }

    protected override string GetClassNameCore()
        => nameof(StatusBullet);

    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Text;

    private string? GetTypeParameter()
    {
        return GetLocalizer().GetString($"{nameof(StatusBullet)}_{nameof(StatusBullet.Type)}_{ElementOwner.Type}_AutomationName");
    }

    private string? GetMessageParameter()
    {
        return ElementOwner.GetAccessibilityText(
            headerProperty: StatusBullet.ContentProperty,
            options: AccessibilityTextOptions.IgnoreFallback);
    }
}
