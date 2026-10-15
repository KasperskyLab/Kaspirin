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
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Core;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Automation;

public class TextInputAutomationPeer : BaseTextBoxAutomationPeer<TextInputBase>
{
    public TextInputAutomationPeer(TextInputBase textInputBase) : base(textInputBase)
    {
    }

    protected override string? GetNameText()
    {
        var label = ElementOwner.Label;
        var placeholderControl = TextInputBaseInternals.GetPlaceholder(ElementOwner);
        var placeholder = placeholderControl?.GetAccessibilityText(ElementOwner.Text);

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
}
