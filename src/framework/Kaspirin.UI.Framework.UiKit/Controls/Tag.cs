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

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class Tag : TagBase
{
    #region Color

    public TagColor Color
    {
        get => (TagColor)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        nameof(Color),
        typeof(TagColor),
        typeof(Tag),
        new PropertyMetadata(default(TagColor)));

    #endregion

    #region Icon

    public UIKitIcon_16 Icon
    {
        get => (UIKitIcon_16)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(UIKitIcon_16),
        typeof(Tag),
        new PropertyMetadata(default(UIKitIcon_16)));

    #endregion
}
