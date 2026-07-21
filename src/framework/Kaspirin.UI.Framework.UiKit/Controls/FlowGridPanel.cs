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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls;

internal sealed class FlowGridPanel : Panel
{
    protected override Size MeasureOverride(Size availableSize)
    {
        var children = GetVisibleChildren();
        if (children.Any() == false)
        {
            return Size.Empty;
        }

        var panelWidth = availableSize.Width;
        var panelStrategy = GetEffectiveStrategy(panelWidth);

        var sealRule = panelStrategy.SealRule;
        var columnCount = panelStrategy.ColumnCount;
        var columnWidth = panelWidth / columnCount;

        _layout.Reset();
        _layout = new FlowGridLayout(columnCount);
        _layout.Build(children);
        _layout.Seal(sealRule);
        _layout.Measure(columnWidth);

        return new Size
        {
            Width = _layout.PanelWidth,
            Height = _layout.PanelHeight
        };
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var children = GetVisibleChildren();
        if (children.Any() == false)
        {
            return finalSize;
        }

        var panelWidth = finalSize.Width;
        if (panelWidth.NotNearlyEqual(_layout.PanelWidth, 1))
        {
            InvalidateMeasure();

            return finalSize;
        }

        var parentGrid = ParentGrid;
        if (parentGrid != null)
        {
            parentGrid.ItemsGrid = _layout.Arrange(panelWidth);
        }

        return new Size
        {
            Width = _layout.PanelWidth,
            Height = _layout.PanelHeight
        };
    }

    private FlowGridStrategy GetEffectiveStrategy(double panelWidth)
    {
        var parentGrid = ParentGrid;
        if (parentGrid == null)
        {
            return new();
        }

        var strategy = parentGrid.Strategies.ChooseSuitableStrategy(panelWidth);
        if (strategy != parentGrid.ActualStrategy)
        {
            _nextStrategy = strategy;

            if (_strategyUpdating is false)
            {
                _strategyUpdating = true;

                Executers.InUiAsync(() =>
                {
                    _strategyUpdating = false;

                    parentGrid.ActualStrategy = _nextStrategy;

                    InvalidateMeasure();
                });
            }

            return parentGrid.ActualStrategy;
        }

        return strategy;
    }

    private FlowGrid? ParentGrid => TemplatedParent as FlowGrid;

    private IList<FlowGridItem> GetVisibleChildren()
    {
        InternalChildren
            .OfType<FlowGridItem>()
            .Where(i => !i.IsMeasureValid)
            .ForEach(i => i.Measure(_infinitySize));

        return InternalChildren
            .OfType<FlowGridItem>()
            .Where(i => i.DesiredSize.Height.NotNearlyZero())
            .ToList();
    }

    private FlowGridLayout _layout = new(1);
    private FlowGridStrategy _nextStrategy = new();
    private bool _strategyUpdating;

    private static Size _infinitySize = new() { Height = double.PositiveInfinity, Width = double.PositiveInfinity };
}