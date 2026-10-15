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
using System.Windows.Automation.Provider;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

internal sealed class TextViewerBoxAutomationPeer : BaseTextBoxAutomationPeer<TextViewerBox>
{
    public TextViewerBoxAutomationPeer(TextViewerBox textViewerBox) : base(textViewerBox)
    {
    }

    public override object? GetPattern(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.Scroll)
        {
            if (_scrollPattern != null)
            {
                return _scrollPattern;
            }

            var scroll = Guard.EnsureIsInstanceOfType<TextViewer>(ElementOwner.TemplatedParent).ScrollViewer;
            if (scroll != null && _scrollPattern == null)
            {
                var scrollPeer = (ScrollViewerAutomationPeer)CreatePeerForElement(scroll);
                scrollPeer.EventsSource = this;

                return _scrollPattern = scrollPeer;
            }
        }

        return base.GetPattern(patternInterface);
    }

    protected override bool IsTemplated => true;

#if NETCOREAPP
    protected override bool IsDialogCore()
        => true;
#endif

    protected override string? GetLocalizedControlTypeText()
        => null;

    protected override AutomationControlType GetAutomationControlTypeCore()
        => AutomationControlType.Group;

    private IScrollProvider? _scrollPattern;
}
