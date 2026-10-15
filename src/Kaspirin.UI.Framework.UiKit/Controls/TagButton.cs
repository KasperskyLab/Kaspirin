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
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using Kaspirin.UI.Framework.UiKit.Controls.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_Border, Type = typeof(Border))]
[TemplatePart(Name = PART_TopLayer, Type = typeof(Border))]
public sealed class TagButton : Button, IAccessibilityAware
{
    public const string PART_Border = "PART_Border";
    public const string PART_TopLayer = "PART_TopLayer";

    public TagButton()
    {
        _cornerRoundingHelper = new CornerRoundingHelper(this, InvalidateCornerRadius);
    }

    #region Color

    public TagColor Color
    {
        get => (TagColor)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        nameof(Color),
        typeof(TagColor),
        typeof(TagButton),
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
        typeof(TagButton),
        new PropertyMetadata(default(UIKitIcon_16)));

    #endregion

    #region Text

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(TagButton),
        new PropertyMetadata(default(string)));

    #endregion

    #region DisableRoundingTopLeft

    public bool DisableRoundingTopLeft
    {
        get => (bool)GetValue(DisableRoundingTopLeftProperty);
        set => SetValue(DisableRoundingTopLeftProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingTopLeftProperty =
        CornerRoundingHelper.DisableRoundingTopLeftProperty.AddOwner(typeof(TagButton));

    #endregion

    #region DisableRoundingTopRight

    public bool DisableRoundingTopRight
    {
        get => (bool)GetValue(DisableRoundingTopRightProperty);
        set => SetValue(DisableRoundingTopRightProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingTopRightProperty =
        CornerRoundingHelper.DisableRoundingTopRightProperty.AddOwner(typeof(TagButton));

    #endregion

    #region DisableRoundingBottomLeft

    public bool DisableRoundingBottomLeft
    {
        get => (bool)GetValue(DisableRoundingBottomLeftProperty);
        set => SetValue(DisableRoundingBottomLeftProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingBottomLeftProperty =
        CornerRoundingHelper.DisableRoundingBottomLeftProperty.AddOwner(typeof(TagButton));

    #endregion

    #region DisableRoundingBottomRight

    public bool DisableRoundingBottomRight
    {
        get => (bool)GetValue(DisableRoundingBottomRightProperty);
        set => SetValue(DisableRoundingBottomRightProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingBottomRightProperty =
        CornerRoundingHelper.DisableRoundingBottomRightProperty.AddOwner(typeof(TagButton));

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _border = GetTemplateChild(PART_Border) as Border;
        _topLayer = GetTemplateChild(PART_TopLayer) as Border;

        InvalidateCornerRadius();
    }

    protected override AutomationPeer OnCreateAutomationPeer()
        => new TagButtonAutomationPeer(this);

    bool IAccessibilityAware.Validate()
        => this.Validate();

    private void InvalidateCornerRadius()
    {
        var cornerRadius = _cornerRoundingHelper.GetCornerRadius();

        if (_border != null)
        {
            _border.CornerRadius = cornerRadius;
        }

        if (_topLayer != null)
        {
            _topLayer.CornerRadius = cornerRadius;
        }
    }

    private Border? _border;
    private Border? _topLayer;
    private readonly CornerRoundingHelper _cornerRoundingHelper;
}
