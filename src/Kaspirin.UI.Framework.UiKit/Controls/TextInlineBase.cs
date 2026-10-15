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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public abstract class TextInlineBase : InlineUIContainer
{
    #region Margin

    public Thickness Margin
    {
        get => (Thickness)GetValue(MarginProperty);
        set => SetValue(MarginProperty, value);
    }

    public static readonly DependencyProperty MarginProperty = DependencyProperty.Register(
        nameof(Margin),
        typeof(Thickness),
        typeof(TextInlineBase),
        new PropertyMetadata(UIKitConstants.TextInlineBaseMargin));

    #endregion

    #region Visibility

    public Visibility Visibility
    {
        get => (Visibility)GetValue(VisibilityProperty);
        set => SetValue(VisibilityProperty, value);
    }

    public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register(
        nameof(Visibility),
        typeof(Visibility),
        typeof(TextInlineBase),
        new PropertyMetadata(Visibility.Visible));

    #endregion

    protected sealed override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        var parent = this.FindParentTextBlock();
        if (parent != null)
        {
            Child = PrepareChildElement(parent);

            if (Child is FrameworkElement fe)
            {
                fe.SetBinding(FrameworkElement.MarginProperty, new Binding { Source = this, Path = MarginProperty.AsPath() });
                fe.SetBinding(FrameworkElement.VisibilityProperty, new Binding { Source = this, Path = VisibilityProperty.AsPath() });
            }
        }
    }

    protected abstract UIElement PrepareChildElement(TextBlock parent);
}
