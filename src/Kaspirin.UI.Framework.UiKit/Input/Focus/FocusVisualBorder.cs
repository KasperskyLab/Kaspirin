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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Input.Focus;

internal sealed class FocusVisualBorder : Border
{
    public FocusVisualBorder()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var adornedElement = this.FindVisualParent<Adorner>()?.AdornedElement;
        if (adornedElement == null)
        {
            return;
        }

        var cornerRadius = CornerRadius;

        CornerRadius = new CornerRadius
        {
            BottomLeft = (bool)adornedElement.GetValue(CornerRoundingHelper.DisableRoundingBottomLeftProperty)
                ? DefaultCornerRadius
                : cornerRadius.BottomLeft,
            BottomRight = (bool)adornedElement.GetValue(CornerRoundingHelper.DisableRoundingBottomRightProperty)
                ? DefaultCornerRadius
                : cornerRadius.BottomRight,
            TopLeft = (bool)adornedElement.GetValue(CornerRoundingHelper.DisableRoundingTopLeftProperty)
                ? DefaultCornerRadius
                : cornerRadius.TopLeft,
            TopRight = (bool)adornedElement.GetValue(CornerRoundingHelper.DisableRoundingTopRightProperty)
                ? DefaultCornerRadius
                : cornerRadius.TopRight,
        };
    }

    private const int DefaultCornerRadius = 3;
}
