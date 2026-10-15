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
using System.Windows.Data;
using System.Windows.Documents;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors;

public sealed class HyperlinkNavigateCommandBehavior : BaseNavigateCommandBehavior<Hyperlink, HyperlinkNavigateCommandBehavior>
{
    protected override DependencyProperty CommandProperty => Hyperlink.CommandProperty;

    protected override void AddIcon()
    {
        var hyperlink = Guard.EnsureIsNotNull(AssociatedObject);

        var margin = new Thickness(0);
        var iconSize = GetIconSize(hyperlink);

        switch (iconSize)
        {
            case SmallIconSize:
                _iconBrushProperty = TextIcon12.IconBrushProperty;
                _icon = new TextIcon12
                {
                    Icon = UIKitIcon_12.LinkExternal,
                    Margin = margin
                };
                break;
            case DefaultIconSize:
                _iconBrushProperty = TextIcon16.IconBrushProperty;
                _icon = new TextIcon16
                {
                    Icon = UIKitIcon_16.LinkExternal,
                    Margin = margin
                };
                break;
            case LargeIconSize:
                _iconBrushProperty = TextIcon24.IconBrushProperty;
                _icon = new TextIcon24
                {
                    Icon = UIKitIcon_24.LinkExternal,
                    Margin = margin
                };
                break;
            default:
                throw new UnexpectedValueException(iconSize);
        }

        _iconNbsp = new Run(NBSP);
        _icon.SetBinding(_iconBrushProperty, new Binding
        {
            Source = hyperlink,
            Path = TextElement.ForegroundProperty.AsPath(),
            Mode = BindingMode.OneWay
        });

        hyperlink.Inlines.Add(_iconNbsp);
        hyperlink.Inlines.Add(_icon);
    }

    private static int GetIconSize(Hyperlink hyperlink)
    {
        var textBlock = hyperlink.FindParentTextBlock();

        if (textBlock != null && double.IsNaN(textBlock.LineHeight) is false)
        {
            if (textBlock.LineHeight <= 16)
            {
                return SmallIconSize;
            }
            else if (textBlock.LineHeight <= 24)
            {
                return DefaultIconSize;
            }
            else
            {
                return LargeIconSize;
            }
        }

        if (hyperlink.FontSize <= 12)
        {
            return SmallIconSize;
        }
        else if (hyperlink.FontSize <= 18)
        {
            return DefaultIconSize;
        }
        else
        {
            return LargeIconSize;
        }
    }

    protected override void RemoveIcon()
    {
        var hyperlink = Guard.EnsureIsNotNull(AssociatedObject);

        if (_icon == null || _iconBrushProperty == null)
        {
            return;
        }

        hyperlink.Inlines.Remove(_iconNbsp);
        hyperlink.Inlines.Remove(_icon);

        _icon.ClearValue(_iconBrushProperty);

        _iconBrushProperty = null;
        _iconNbsp = null;
        _icon = null;
    }

    private TextInlineBase? _icon;
    private Inline? _iconNbsp;
    private DependencyProperty? _iconBrushProperty;

    private const string NBSP = "\u00A0";
    private const int DefaultIconSize = 16;
    private const int SmallIconSize = 12;
    private const int LargeIconSize = 24;
}
