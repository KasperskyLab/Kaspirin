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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class WrapPanelAdaptable : Panel
    {
        #region ColumnsCount

        public uint ColumnsCount
        {
            get { return (uint)GetValue(ColumnsCountProperty); }
            private set { SetValue(_columnsCountPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _columnsCountPropertyKey =
            DependencyProperty.RegisterReadOnly("ColumnsCount", typeof(uint), typeof(WrapPanelAdaptable),
                new PropertyMetadata(1U));

        public static readonly DependencyProperty ColumnsCountProperty =
            _columnsCountPropertyKey.DependencyProperty;

        #endregion

        #region ColumnSpan

        public static uint GetColumnSpan(DependencyObject obj)
        {
            return (uint)obj.GetValue(ColumnSpanProperty);
        }

        public static void SetColumnSpan(DependencyObject obj, uint value)
        {
            obj.SetValue(ColumnSpanProperty, value);
        }

        public static readonly DependencyProperty ColumnSpanProperty =
            DependencyProperty.RegisterAttached("ColumnSpan", typeof(uint), typeof(WrapPanelAdaptable),
                new FrameworkPropertyMetadata(1U, FrameworkPropertyMetadataOptions.AffectsParentMeasure, null, ColumnSpanCoerce));

        private static object ColumnSpanCoerce(DependencyObject d, object baseValue)
        {
            return (uint)baseValue == 0 ? 1U : baseValue;
        }

        #endregion

        #region ColumnStrategies

        public WrapPanelColumnStrategyCollection ColumnStrategies
        {
            get { return (WrapPanelColumnStrategyCollection)GetValue(ColumnStrategiesProperty); }
            set { SetValue(ColumnStrategiesProperty, value); }
        }

        public static readonly DependencyProperty ColumnStrategiesProperty =
            DependencyProperty.Register("ColumnStrategies", typeof(WrapPanelColumnStrategyCollection), typeof(WrapPanelAdaptable),
                new FrameworkPropertyMetadata(new WrapPanelColumnStrategyCollection(), FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region ItemSize

        private static readonly DependencyProperty _itemSizeProperty =
            DependencyProperty.RegisterAttached(
                "ItemSize",
                typeof(Size),
                typeof(WrapPanelAdaptable));

        #endregion

        #region HasNeighbours

        public static readonly DependencyProperty HasNeighboursProperty =
            DependencyProperty.RegisterAttached(
                "HasNeighbours",
                typeof(bool),
                typeof(WrapPanelAdaptable),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHasNeighbours(UIElement textBox, bool hasNeighbours)
        {
            textBox.SetValue(HasNeighboursProperty, hasNeighbours);
        }

        public static bool GetHasNeighbours(UIElement textBox)
        {
            return (bool)textBox.GetValue(HasNeighboursProperty);
        }

        #endregion

        #region ActualColumnStrategy

        public WrapPanelColumnStrategy ActualColumnStrategy
        {
            get { return (WrapPanelColumnStrategy)GetValue(ActualColumnStrategyProperty); }
            private set { SetValue(_actualColumnStrategyPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _actualColumnStrategyPropertyKey =
            DependencyProperty.RegisterReadOnly("ActualColumnStrategy", typeof(WrapPanelColumnStrategy), typeof(WrapPanelAdaptable),
                new PropertyMetadata(WrapPanelColumnStrategy.Default));

        public static readonly DependencyProperty ActualColumnStrategyProperty =
            _actualColumnStrategyPropertyKey.DependencyProperty;

        #endregion

        protected override Size MeasureOverride(Size availableSize)
        {
            var panelHeight = 0.0;
            var panelWidth = availableSize.Width;
            var columnStrategy = UpdateActualColumnStrateg(panelWidth);
            var columnsCount = columnStrategy.Count;
            var columnWidth = panelWidth / columnStrategy.Count;

            var children = GetVisibleChildren();
            if (children.Any() == false)
            {
                return new Size(0, 0);
            }

            var panelRows = new List<PanelRow>() { new PanelRow(columnsCount, columnWidth) };
            var currentRow = panelRows.Last();

            foreach (var item in children)
            {
                if (currentRow.TryAddItem(item))
                {
                    continue;
                }
                else
                {
                    currentRow = new PanelRow(columnsCount, columnWidth);
                    currentRow.TryAddItem(item);

                    panelRows.Add(currentRow);
                }
            }

            var lastRow = panelRows.Last();

            foreach (var row in panelRows)
            {
                var skipRowAlign = columnStrategy.AlignType == WrapPanelColumnStrategy.RowAlignType.SkipLast && row == lastRow ||
                                   columnStrategy.AlignType == WrapPanelColumnStrategy.RowAlignType.SkipAll;

                row.Seal(skipRowAlign);
                row.Measure();
                row.SetNeighbours();

                panelHeight += row.Height;
            }

            if (ColumnsCount != columnsCount)
            {
                Executers.InUiAsync(() =>
                {
                    ColumnsCount = columnsCount;
                    InvalidateMeasure();
                }, DispatcherPriority.Loaded);
            }

            return new Size(panelWidth, panelHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var panelHeight = 0.0;
            var panelWidth = finalSize.Width;
            var columnStrategy = UpdateActualColumnStrateg(panelWidth);
            var columnsCount = columnStrategy.Count;
            var columnWidth = panelWidth / columnStrategy.Count;

            var children = GetVisibleChildren();
            if (children.Any() == false)
            {
                return new Size(0, 0);
            }

            var panelRows = new List<PanelRow>() { new PanelRow(columnsCount, columnWidth) };
            var currentRow = panelRows.Last();

            foreach (var item in children)
            {
                if (currentRow.TryAddItem(item))
                {
                    continue;
                }
                else
                {
                    currentRow = new PanelRow(columnsCount, columnWidth);
                    currentRow.TryAddItem(item);

                    panelRows.Add(currentRow);
                }
            }

            foreach (var row in panelRows)
            {
                row.Seal(true);
                row.Arrange(panelHeight);

                panelHeight += row.Height;
            }

            return new Size(panelWidth, panelHeight);
        }

        private WrapPanelColumnStrategy UpdateActualColumnStrateg(double panelWidth)
        {
            var effectiveStrategy = ColumnStrategies?.LastOrDefault(s => s.FromWidth.LesserOrNearlyEqual(panelWidth) && panelWidth.LesserOrNearlyEqual(s.ToWidth)) ?? WrapPanelColumnStrategy.Default;
            if (effectiveStrategy != ActualColumnStrategy)
            {
                if (ActualColumnStrategy == WrapPanelColumnStrategy.Default)
                {
                    ActualColumnStrategy = effectiveStrategy;

                    return effectiveStrategy;
                }
                else
                {
                    Executers.InUiAsync(() =>
                    {
                        ActualColumnStrategy = effectiveStrategy;
                        InvalidateMeasure();
                    }, DispatcherPriority.Loaded);

                    return ActualColumnStrategy;
                }
            }

            return effectiveStrategy;
        }

        private IEnumerable<FrameworkElement> GetVisibleChildren()
            => Children
                .OfType<FrameworkElement>()
                .Where(f => f.Visibility != Visibility.Collapsed)
                .Where(f => f.FindVisualChild<UIElement>()?.Visibility != Visibility.Collapsed)
                .ToList();

        private sealed class PanelItem
        {
            public PanelItem(FrameworkElement item)
            {
                Item = item;

                DesiredColumnSpan = (uint)Item.GetValue(ColumnSpanProperty);
                ActualColumnSpan = DesiredColumnSpan;
            }

            public FrameworkElement Item { get; }

            public uint DesiredColumnSpan { get; }

            public uint ActualColumnSpan { get; set; }

            public Size Measure(Size size)
            {
                Item.Measure(size);

                if (size.Height.NearlyEqual(double.PositiveInfinity))
                {
                    size.Height = Item.DesiredSize.Height;
                }

                return size;
            }
        }

        private sealed class PanelRow
        {
            public PanelRow(uint maxSpan, double columnWidth)
            {
                _maxSpan = maxSpan;
                _columnWidth = columnWidth;
            }

            public double Height { get; private set; }

            public IList<PanelItem> Items => _items;

            public bool TryAddItem(FrameworkElement item)
            {
                if (_isSealed)
                {
                    return false;
                }

                var panelItem = new PanelItem(item);
                if (panelItem.DesiredColumnSpan > _maxSpan)
                {
                    panelItem.ActualColumnSpan = _maxSpan;
                }

                var currentSpan = (int)_items.Sum(i => i.ActualColumnSpan);
                if (currentSpan + panelItem.ActualColumnSpan > _maxSpan)
                {
                    return false;
                }

                _items.Add(panelItem);

                return true;
            }

            public void Seal(bool skipRowAlign)
            {
                var currentSpan = (int)_items.Sum(i => i.ActualColumnSpan);
                if (currentSpan > _maxSpan)
                {
                    throw new Exception("Error on sealing PanelRow. Invalid items collection.");
                }

                if (!skipRowAlign && currentSpan < _maxSpan)
                {
                    var lastItem = _items.LastOrDefault();
                    if (lastItem != null)
                    {
                        lastItem.ActualColumnSpan += (uint)(_maxSpan - currentSpan);
                    }
                }

                _isSealed = true;
            }

            public void SetNeighbours()
            {
                _items.ForEach(i => i.Item.SetValue(HasNeighboursProperty, _items.Count > 1));
            }

            public void Measure()
            {
                var maxHeight = 0.0;

                foreach (var item in _items)
                {
                    var itemWidth = _columnWidth * item.ActualColumnSpan;
                    var itemHeight = double.PositiveInfinity;
                    var itemSize = new Size(itemWidth, itemHeight);

                    itemSize = item.Measure(itemSize);

                    maxHeight = Math.Max(maxHeight, itemSize.Height);
                }

                foreach (var item in _items)
                {
                    var itemWidth = _columnWidth * item.ActualColumnSpan;
                    var itemHeight = maxHeight;
                    var itemSize = new Size(itemWidth, itemHeight);

                    item.Item.SetValue(_itemSizeProperty, itemSize);
                }

                Height = maxHeight;
            }
            internal void Arrange(double panelHeight)
            {
                var currentRowWidth = 0.0;
                var maxHeight = 0.0;

                foreach (var item in _items)
                {
                    var itemSize = (Size)item.Item.GetValue(_itemSizeProperty);
                    var itemPos = new Rect(currentRowWidth, panelHeight, itemSize.Width, itemSize.Height);

                    item.Item.Arrange(itemPos);

                    currentRowWidth += itemSize.Width;
                    maxHeight = Math.Max(maxHeight, itemSize.Height);
                }

                Height = maxHeight;
            }

            private bool _isSealed;
            private readonly uint _maxSpan;
            private readonly double _columnWidth;
            private readonly List<PanelItem> _items = new();
        }
    }
}
