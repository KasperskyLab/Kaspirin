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

using System.Windows.Automation.Peers;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class TagAutomationPeer : BaseFrameworkElementAutomationPeer<Tag>
{
    public TagAutomationPeer(Tag tag) : base(tag)
    { }

    protected override string? GetNameText()
    {
        return GetLocalizer().GetString("Tag_AutomationName", "Text", ElementOwner.Text);
    }

    protected override string? GetHelpText()
    {
        return GetLocalizer().GetString(
            "Tag_AutomationHelpText",
            paramName: "Color",
            paramValue: GetLocalizer().GetString($"{nameof(Tag)}_{nameof(Tag.Color)}_{ElementOwner.Color}_AutomationName"));
    }

    protected override string GetClassNameCore()
        => nameof(Tag);

    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Text;
}
