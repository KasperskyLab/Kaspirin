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
using Kaspirin.UI.Framework.UiKit.Controls.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

internal sealed class SearchInput : TextInputBase
{
    #region Placeholder

    public TextInputPlaceholder? Placeholder
    {
        get => (TextInputPlaceholder?)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder),
        typeof(TextInputPlaceholder),
        typeof(SearchInput),
        new PropertyMetadata(default(TextInputPlaceholder), OnPlaceholderChanged));

    private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(TextInputBaseInternals.PlaceholderProperty, e.NewValue);
    }

    #endregion

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new SearchInputAutomationPeer(this);
    }
}
