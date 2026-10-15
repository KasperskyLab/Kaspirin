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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class SelectItemAutomationPeer : SelectorItemAutomationPeer
{
    internal SelectItemAutomationPeer(object item, SelectorAutomationPeer control, SelectItemInfoProvider itemInfoProvider)
        : base(item, control)
    {
        _itemInfoProvider = itemInfoProvider;
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
        return AutomationControlType.ListItem;
    }

    protected override string GetClassNameCore()
    {
        return nameof(SelectItem);
    }

    protected override string? GetNameCore()
    {
        var select = Guard.EnsureIsInstanceOfType<Select>(ItemsControlAutomationPeer.Owner);
        if (select.IsDropDownOpen)
        {
            return _itemInfoProvider.GetContainer().Header;
        }

        return string.Empty;
    }

    private readonly SelectItemInfoProvider _itemInfoProvider;
}
