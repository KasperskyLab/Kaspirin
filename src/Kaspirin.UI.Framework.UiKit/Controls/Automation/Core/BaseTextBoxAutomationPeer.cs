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

using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

public abstract class BaseTextBoxAutomationPeer<TControl> :
    TextBoxAutomationPeer, IAutomationPeer, IAccessibilityAware
    where TControl : TextBox
{
    protected BaseTextBoxAutomationPeer(TControl control) : base(control)
    {
        _corePeer = new CoreAutomationPeer(this);
    }

    #region IAutomationPeer

    string? IAutomationPeer.GetNameText()
        => GetNameText();

    string? IAutomationPeer.GetHelpText()
        => GetHelpText();

    string? IAutomationPeer.GetLocalizedControlTypeText()
        => GetLocalizedControlTypeText();

    string? IAutomationPeer.GetBaseNameCore()
        => base.GetNameCore();

    string? IAutomationPeer.GetBaseHelpTextCore()
        => base.GetHelpTextCore();

    string? IAutomationPeer.GetBaseLocalizedControlTypeCore()
        => base.GetLocalizedControlTypeCore();

    Rect IAutomationPeer.GetBaseBoundingRectangleCore()
        => base.GetBoundingRectangleCore();

    DependencyObject IAutomationPeer.Owner => ElementOwner;

    bool IAutomationPeer.CanProvideName => CanProvideName;

    bool IAutomationPeer.IsTemplated => IsTemplated;

    bool IAutomationPeer.IsIgnored => IsIgnored;

    #endregion

    #region IAccessibilityAware

    bool IAccessibilityAware.Validate()
        => _corePeer.Validate();

    #endregion

    protected TControl ElementOwner => Guard.EnsureIsInstanceOfType<TControl>(Owner);

    protected virtual bool CanProvideName => true;

    protected virtual bool IsTemplated => false;

    protected bool IsIgnored => _corePeer.IsIgnored;

    protected new virtual string? GetHelpText()
        => _corePeer.GetHelpText();

    protected virtual string? GetNameText()
        => _corePeer.GetNameText();

    protected virtual string? GetLocalizedControlTypeText()
        => _corePeer.GetLocalizedControlTypeText();

    protected sealed override string? GetHelpTextCore()
        => _corePeer.GetHelpTextCore();

    protected sealed override string? GetLocalizedControlTypeCore()
        => _corePeer.GetLocalizedControlTypeCore();

    protected sealed override string? GetNameCore()
        => _corePeer.GetNameCore();

    protected sealed override Rect GetBoundingRectangleCore()
        => _corePeer.GetBoundingRectangleCore();

    protected IStringLocalizer GetLocalizer()
         => _corePeer.GetLocalizer();

    private readonly CoreAutomationPeer _corePeer;
}
