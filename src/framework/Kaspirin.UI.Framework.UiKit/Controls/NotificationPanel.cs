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
using System.Windows.Controls;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_Root, Type = typeof(Border))]
public sealed class NotificationPanel : ContentControl
{
    public const string PART_Root = "PART_Root";

    public NotificationPanel()
    {
        _cornerRoundingHelper = new CornerRoundingHelper(this, InvalidateCornerRadius);
    }

    #region Header

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(NotificationPanel),
        new PropertyMetadata(default(object)));

    #endregion

    #region Icon

    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(ImageSource),
        typeof(NotificationPanel),
        new PropertyMetadata(default(ImageSource)));

    #endregion

    #region SubHeader

    public object SubHeader
    {
        get => GetValue(SubHeaderProperty);
        set => SetValue(SubHeaderProperty, value);
    }

    public static readonly DependencyProperty SubHeaderProperty = DependencyProperty.Register(
        nameof(SubHeader),
        typeof(object),
        typeof(NotificationPanel),
        new PropertyMetadata(default(object)));

    #endregion

    #region Type

    public NotificationPanelType Type
    {
        get => (NotificationPanelType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(NotificationPanelType),
        typeof(NotificationPanel),
        new PropertyMetadata(default(NotificationPanelType)));

    #endregion

    #region RightBar

    public object RightBar
    {
        get => GetValue(RightBarProperty);
        set => SetValue(RightBarProperty, value);
    }

    public static readonly DependencyProperty RightBarProperty = DependencyProperty.Register(
        nameof(RightBar),
        typeof(object),
        typeof(NotificationPanel),
        new PropertyMetadata(default(object)));

    #endregion

    #region DisableRoundingTopLeft

    public bool DisableRoundingTopLeft
    {
        get => (bool)GetValue(DisableRoundingTopLeftProperty);
        set => SetValue(DisableRoundingTopLeftProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingTopLeftProperty =
        CornerRoundingHelper.DisableRoundingTopLeftProperty.AddOwner(typeof(NotificationPanel));

    #endregion

    #region DisableRoundingTopRight

    public bool DisableRoundingTopRight
    {
        get => (bool)GetValue(DisableRoundingTopRightProperty);
        set => SetValue(DisableRoundingTopRightProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingTopRightProperty =
        CornerRoundingHelper.DisableRoundingTopRightProperty.AddOwner(typeof(NotificationPanel));

    #endregion

    #region DisableRoundingBottomLeft

    public bool DisableRoundingBottomLeft
    {
        get => (bool)GetValue(DisableRoundingBottomLeftProperty);
        set => SetValue(DisableRoundingBottomLeftProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingBottomLeftProperty =
        CornerRoundingHelper.DisableRoundingBottomLeftProperty.AddOwner(typeof(NotificationPanel));

    #endregion

    #region DisableRoundingBottomRight

    public bool DisableRoundingBottomRight
    {
        get => (bool)GetValue(DisableRoundingBottomRightProperty);
        set => SetValue(DisableRoundingBottomRightProperty, value);
    }

    public static readonly DependencyProperty DisableRoundingBottomRightProperty =
        CornerRoundingHelper.DisableRoundingBottomRightProperty.AddOwner(typeof(NotificationPanel));

    #endregion

    public override void OnApplyTemplate()
    {
        _root = GetTemplateChild(PART_Root) as Border;

        InvalidateCornerRadius();
    }

    private void InvalidateCornerRadius()
    {
        if (_root != null)
        {
            _root.CornerRadius = _cornerRoundingHelper.GetCornerRadius();
        }
    }

    private Border? _root;
    private readonly CornerRoundingHelper _cornerRoundingHelper;
}
