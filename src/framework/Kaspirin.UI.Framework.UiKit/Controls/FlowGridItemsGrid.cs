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

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

using System.Collections.Generic;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class FlowGridItemsGrid
{
    public FlowGridItemsGrid(IList<FlowGridItem> items, int columnCount)
    {
        _columnCount = columnCount;

        _items = items;
        _itemsGrid = new FlowGridItem[_columnCount, 0];
    }

    public int RowCount => _itemsGrid.GetLength(0);

    public int ColumnCount => _itemsGrid.GetLength(1);

    public FlowGridItem? Get(int row, int column)
    {
        if (Has(row, column))
        {
            return _itemsGrid[row, column];
        }

        return null;
    }

    public bool Has(int row, int column)
    {
        if (RowCount <= row || ColumnCount <= column)
        {
            return false;
        }

        return _itemsGrid[row, column] != null;
    }

    internal void Build()
    {
        var rowCount = _items.Any() ? _items.Max(p => p.InternalRow + p.InternalRowSpan - 1) + 1 : 0;
        var columnCount = _columnCount;

        _itemsGrid = new FlowGridItem?[rowCount, columnCount];

        foreach (var item in _items)
        {
            for (var r = item.InternalRow; r < item.InternalRow + item.InternalRowSpan; r++)
            {
                for (var c = item.InternalColumn; c < item.InternalColumn + item.InternalColumnSpan; c++)
                {
                    Set(r, c, item);
                }
            }
        }
    }

    internal void Set(int row, int column, FlowGridItem? item)
    {
        Guard.Assert(RowCount > row);
        Guard.Assert(ColumnCount > column);

        _itemsGrid[row, column] = item;
    }

    private FlowGridItem?[,] _itemsGrid = new FlowGridItem[0, 0];

    private readonly IList<FlowGridItem> _items;
    private readonly int _columnCount;
}
