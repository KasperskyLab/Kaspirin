// Copyright © 2026 AO Kaspersky Lab.
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

using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class ExpandPanelAutomationPeer : BaseFrameworkElementAutomationPeer<ExpandPanel>, IExpandCollapseProvider
{
    public ExpandPanelAutomationPeer(ExpandPanel expandPanel) : base(expandPanel)
    {
    }

    protected override string GetClassNameCore()
        => nameof(ExpandPanel);

    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Group;

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

        ElementOwner.IsExpanded = true;
    }

    void IExpandCollapseProvider.Collapse()
    {
        if (!IsEnabled())
        {
            throw new ElementNotEnabledException();
        }

        ElementOwner.IsExpanded = false;
    }

    ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
    {
        get
        {
            return ElementOwner.IsExpanded
                ? ExpandCollapseState.Expanded
                : ExpandCollapseState.Collapsed;
        }
    }

    #endregion ExpandCollapse
}
