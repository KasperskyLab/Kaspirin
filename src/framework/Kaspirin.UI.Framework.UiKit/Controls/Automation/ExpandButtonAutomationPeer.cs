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
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class ExpandButtonAutomationPeer : BaseFrameworkElementAutomationPeer<ExpandButton>, IExpandCollapseProvider
{
    public ExpandButtonAutomationPeer(ExpandButton owner) : base(owner)
    {
    }

    protected override string GetClassNameCore()
    {
        return "Expander";
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
        return AutomationControlType.Button;
    }

    protected override List<AutomationPeer>? GetChildrenCore()
    {
        var children = base.GetChildrenCore();
        if (children != null)
        {
            foreach (var peer in children.OfType<UIElementAutomationPeer>())
            {
                if (peer.Owner == ElementOwner.ExpanderToggleButton)
                {
                    peer.EventsSource = EventsSource ?? this;
                    break;
                }
            }
        }

        return children;
    }

    protected override bool HasKeyboardFocusCore()
    {
        return ElementOwner.ExpanderToggleButton?.IsKeyboardFocused ?? false;
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.ExpandCollapse)
        {
            return this;
        }

        return base.GetPattern(patternInterface);
    }

    #region ExpandCollapse

    void IExpandCollapseProvider.Expand()
    {
        if (!IsEnabled())
        {
            throw new ElementNotEnabledException();
        }

        Guard.EnsureIsInstanceOfType<ExpandButton>(Owner).IsExpanded = true;
    }

    void IExpandCollapseProvider.Collapse()
    {
        if (!IsEnabled())
        {
            throw new ElementNotEnabledException();
        }

        Guard.EnsureIsInstanceOfType<ExpandButton>(Owner).IsExpanded = false;
    }

    ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
    {
        get
        {
            return Guard.EnsureIsInstanceOfType<ExpandButton>(Owner).IsExpanded
                ? ExpandCollapseState.Expanded
                : ExpandCollapseState.Collapsed;
        }
    }

    #endregion ExpandCollapse
}
