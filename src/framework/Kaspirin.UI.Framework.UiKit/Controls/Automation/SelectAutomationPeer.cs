// Copyright © 2024 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation
{
    public class SelectAutomationPeer : SelectorAutomationPeer, IExpandCollapseProvider
    {
        public SelectAutomationPeer(Select owner) : base(owner)
        { }

        protected override ItemAutomationPeer CreateItemAutomationPeer(object item)
        {
            return new ListBoxItemAutomationPeer(item, this);
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ComboBox;
        }

        protected override string GetClassNameCore()
        {
            return nameof(Select);
        }

        public override object GetPattern(PatternInterface pattern)
        {
            object? iface;

            if (pattern == PatternInterface.ExpandCollapse)
            {
                iface = this;
            }
            else
            {
                iface = base.GetPattern(pattern);
            }

            return iface;
        }

        #region ExpandCollapse

        void IExpandCollapseProvider.Expand()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            Guard.EnsureIsInstanceOfType<Select>(Owner).SetCurrentValue(Select.IsDropDownOpenProperty, true);
        }

        void IExpandCollapseProvider.Collapse()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            Guard.EnsureIsInstanceOfType<Select>(Owner).SetCurrentValue(Select.IsDropDownOpenProperty, false);
        }

        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
        {
            get
            {
                return Guard.EnsureIsInstanceOfType<Select>(Owner).IsDropDownOpen
                    ? ExpandCollapseState.Expanded
                    : ExpandCollapseState.Collapsed;
            }
        }

        #endregion ExpandCollapse
    }
}
