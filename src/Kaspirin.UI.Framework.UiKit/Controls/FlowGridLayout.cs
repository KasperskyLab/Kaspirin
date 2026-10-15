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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls;

internal sealed class FlowGridLayout
{
    public FlowGridLayout(int columnCount)
    {
        _columnCount = columnCount;

        _itemsGrid = new FlowGridItemsGrid(_items, _columnCount);
    }

    public double PanelWidth => _columnCount * _columnWidth;

    public double PanelHeight => _rowHeights.Sum();

    public void Reset()
    {
        _rowHeights = EmptyArray.For<double>();
        _columnWidth = 0.0;

        _items.ForEach(i => i.Reset());
        _itemsGrid = new FlowGridItemsGrid(_items, _columnCount);
    }

    public void Build(IEnumerable<FlowGridItem> items)
    {
        Reset();

        _items.Clear();
        _items.AddRange(items);

        FillLayout();

        var rowsChanged = TryRemoveEmptyRows();
        if (rowsChanged)
        {
            FillLayout();
        }

        _itemsGrid.Build();
    }

    public void Seal(FlowGridSealRule sealRule)
    {
        ApplySealExpansion(sealRule);

        _itemsGrid.Build();
    }

    public void Measure(double columnWidth)
    {
        _columnWidth = columnWidth;

        foreach (var item in _items)
        {
            var size = new Size
            {
                Width = item.InternalColumnSpan * _columnWidth,
                Height = double.PositiveInfinity
            };

            item.Measure(size);
        }

        _rowHeights = ComputeRowHeights();
    }

    public FlowGridItemsGrid Arrange(double panelWidth)
    {
        _columnWidth = panelWidth / _columnCount;

        var prefix = new double[_rowHeights.Length + 1];

        for (var i = 0; i < _rowHeights.Length; i++)
        {
            prefix[i + 1] = prefix[i] + _rowHeights[i];
        }

        foreach (var item in _items)
        {
            var x = item.InternalColumn * _columnWidth;
            var y = prefix[item.InternalRow];
            var w = item.InternalColumnSpan * _columnWidth;

            var endRowExclusive = Math.Min(item.InternalRow + item.InternalRowSpan, _rowHeights.Length);
            var h = prefix[endRowExclusive] - prefix[item.InternalRow];

            var itemSize = new Size(w, h);
            var itemCell = new Point(x, y);
            var itemPos = new Rect(itemCell, itemSize);

            item.Arrange(itemPos);
        }

        return _itemsGrid;
    }

    private double[] ComputeRowHeights()
    {
        var rowCount = MaxRow + 1;
        var rowHeights = new double[rowCount];

        var rowsByEnd = _items
            .GroupBy(i => i.InternalRow + i.InternalRowSpan - 1)
            .ToDictionary(g => g.Key, g => g.ToList());

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            double rowHeight = 0;

            if (rowsByEnd.TryGetValue(rowIndex, out var row))
            {
                foreach (var item in row)
                {
                    double prevRows = 0;

                    for (var rr = item.InternalRow; rr < rowIndex; rr++)
                    {
                        prevRows += rowHeights[rr];
                    }

                    rowHeight = Math.Max(rowHeight, Math.Max(0, item.DesiredSize.Height - prevRows));
                }
            }

            rowHeights[rowIndex] = rowHeight;
        }

        return rowHeights;
    }

    private void ApplySealExpansion(FlowGridSealRule sealRule)
    {
        var sealRows = sealRule.GetRows(MaxRow + 1);

        foreach (var row in sealRows.OrderBy(x => x))
        {
            if (row < 0 || row > MaxRow)
            {
                continue;
            }

            var rowItems = new HashSet<FlowGridItem>();

            for (var col = 0; col < _columnCount; col++)
            {
                var item = _itemsGrid.Get(row, col);
                if (item != null)
                {
                    rowItems.Add(item);
                }
            }

            foreach (var item in rowItems.OrderBy(x => x.InternalColumn))
            {
                while (CanExpandRight(item))
                {
                    var newCol = item.InternalColumn + item.InternalColumnSpan;

                    for (var rr = item.InternalRow; rr < item.InternalRow + item.InternalRowSpan; rr++)
                    {
                        _itemsGrid.Set(rr, newCol, item);
                    }

                    item.InternalColumnSpan++;
                }
            }
        }
    }

    private bool CanExpandRight(FlowGridItem item)
    {
        var newCol = item.InternalColumn + item.InternalColumnSpan;
        if (newCol >= _columnCount)
        {
            return false;
        }

        for (var rr = item.InternalRow; rr < item.InternalRow + item.InternalRowSpan; rr++)
        {
            if (_itemsGrid.Has(rr, newCol))
            {
                return false;
            }
        }

        return true;
    }

    private bool TryRemoveEmptyRows()
    {
        var rowsByEnd = _items
            .GroupBy(i => i.InternalRow + i.InternalRowSpan - 1)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.ToList());

        var rowPrev = 0;
        var rowCur = rowsByEnd.First().Key;

        var rowSpanChanged = false;

        foreach (var row in rowsByEnd)
        {
            rowPrev = rowCur;
            rowCur = row.Key;

            var offsetSize = rowCur - rowPrev;

            var canRemoveRow = row.Value.All(i => i.InternalRow != rowCur);
            if (canRemoveRow)
            {
                row.Value.ForEach(i => i.InternalRowSpan -= offsetSize);

                rowSpanChanged = true;
            }
        }

        return rowSpanChanged;
    }

    private void FillLayout()
    {
        var occupied = new HashSet<(int r, int c)>();
        _items.ForEach(i => i.InternalRow = i.InternalColumn = 0);
        _items.ForEach(i => PlaceItem(i, occupied, _columnCount));
    }

    private void PlaceItem(FlowGridItem item, HashSet<(int r, int c)> occupied, int columnCount)
    {
        var rowSpan = Math.Max(1, item.InternalRowSpan);
        var colSpan = Math.Min(Math.Max(1, item.InternalColumnSpan), columnCount);
        var startRow = _items.Max(i => i.InternalRow);

        var (row, col) = FindFreePlace(occupied, columnCount, colSpan, rowSpan, startRow);

        MarkOccupied(occupied, row, col, rowSpan, colSpan);

        item.InternalRow = row;
        item.InternalColumn = col;
        item.InternalRowSpan = rowSpan;
        item.InternalColumnSpan = colSpan;
    }

    private static (int row, int col) FindFreePlace(
        HashSet<(int r, int c)> occupied,
        int columnCount,
        int colSpan,
        int rowSpan,
        int startRow)
    {
        var row = startRow;

        while (true)
        {
            for (var col = 0; col <= columnCount - colSpan; col++)
            {
                if (CanPlace(occupied, row, col, rowSpan, colSpan))
                {
                    return (row, col);
                }
            }

            row++;
        }
    }

    private static bool CanPlace(
        HashSet<(int r, int c)> occupied,
        int row,
        int col,
        int rowSpan,
        int colSpan)
    {
        for (var r = row; r < row + rowSpan; r++)
        {
            for (var c = col; c < col + colSpan; c++)
            {
                if (occupied.Contains((r, c)))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void MarkOccupied(
        HashSet<(int r, int c)> occupied,
        int row,
        int col,
        int rowSpan,
        int colSpan)
    {
        for (var r = row; r < row + rowSpan; r++)
        {
            for (var c = col; c < col + colSpan; c++)
            {
                occupied.Add((r, c));
            }
        }
    }

    private int MaxRow => _items.Max(p => p.InternalRow + p.InternalRowSpan - 1);

    private double _columnWidth;
    private double[] _rowHeights = EmptyArray.For<double>();
    private FlowGridItemsGrid _itemsGrid;

    private readonly int _columnCount;
    private readonly List<FlowGridItem> _items = new();
}