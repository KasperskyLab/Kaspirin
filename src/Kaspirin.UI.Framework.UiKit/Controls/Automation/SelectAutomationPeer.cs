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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public sealed class SelectAutomationPeer : BaseSelectorAutomationPeer<Select>, IExpandCollapseProvider
{
    internal SelectAutomationPeer(Select owner, SelectInfoProvider infoProvider) : base(owner)
    {
        _infoProvider = infoProvider;
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.ExpandCollapse)
        {
            return this;
        }

        return base.GetPattern(patternInterface);
    }

    protected override ItemAutomationPeer CreateItemAutomationPeer(object item)
    {
        var itemInfoProvider = Guard.EnsureIsNotNull(_infoProvider.GetTargetInfo(item));

        return new SelectItemAutomationPeer(item, this, itemInfoProvider);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
        return AutomationControlType.ComboBox;
    }

    protected override string GetClassNameCore()
    {
        return nameof(Select);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
        var children = base.GetChildrenCore() ?? new();

        var selectPresenter = ElementOwner.Presenter;
        if (selectPresenter != null)
        {
            var presenterPeer = CreatePeerForElement(selectPresenter);
            presenterPeer.EventsSource = EventsSource ?? this;

            children.Insert(0, presenterPeer);
        }

        return children;
    }

    protected override bool HasKeyboardFocusCore()
    {
        return ElementOwner.Presenter?.IsKeyboardFocused ?? false;
    }

    protected override string? GetNameText()
    {
        var header = ElementOwner.Presenter?.Header;
        var label = ElementOwner.Label;
        var placeholder = ElementOwner.SelectedItem == null ? ElementOwner.Placeholder : header;

        var text = new List<string?> { label, placeholder };

        if (text.All(t => t.IsNullOrEmpty()))
        {
            return base.GetNameText();
        }

        if (text.Any(t => t.IsNullOrEmpty()))
        {
            return text.Single(t => t.IsNotNullOrEmpty());
        }

        return string.Join(", ", text);
    }

    #region ExpandCollapse

    void IExpandCollapseProvider.Expand()
    {
        if (!IsEnabled())
        {
            throw new ElementNotEnabledException();
        }

        ElementOwner.SetCurrentValue(Select.IsDropDownOpenProperty, true);
    }

    void IExpandCollapseProvider.Collapse()
    {
        if (!IsEnabled())
        {
            throw new ElementNotEnabledException();
        }

        ElementOwner.SetCurrentValue(Select.IsDropDownOpenProperty, false);
    }

    ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
    {
        get
        {
            return ElementOwner.IsDropDownOpen
                ? ExpandCollapseState.Expanded
                : ExpandCollapseState.Collapsed;
        }
    }

    #endregion ExpandCollapse

    internal void RaiseExpandCollapseAutomationEvent(bool newValue)
    {
        RaisePropertyChangedEvent(
            ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty,
            oldValue: newValue ? ExpandCollapseState.Collapsed : ExpandCollapseState.Expanded,
            newValue: newValue ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed);
    }

    private readonly SelectInfoProvider _infoProvider;
}
