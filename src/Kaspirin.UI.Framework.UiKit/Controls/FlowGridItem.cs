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
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class FlowGridItem : ContentControl
{
    #region ActualColumnSpan

    public int ActualColumnSpan
    {
        get => (int)GetValue(ActualColumnSpanProperty);
        private set => SetValue(_actualColumnSpanPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _actualColumnSpanPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(ActualColumnSpan),
        typeof(int),
        typeof(FlowGridItem),
        new PropertyMetadata(1));

    public static readonly DependencyProperty ActualColumnSpanProperty = _actualColumnSpanPropertyKey.DependencyProperty;

    #endregion

    #region ActualRowSpan

    public int ActualRowSpan
    {
        get => (int)GetValue(ActualRowSpanProperty);
        private set => SetValue(_actualRowSpanPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _actualRowSpanPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(ActualRowSpan),
        typeof(int),
        typeof(FlowGridItem),
        new PropertyMetadata(1));

    public static readonly DependencyProperty ActualRowSpanProperty = _actualRowSpanPropertyKey.DependencyProperty;

    #endregion

    #region ColumnSpan

    public int ColumnSpan
    {
        get => (int)GetValue(ColumnSpanProperty);
        set => SetValue(ColumnSpanProperty, value);
    }

    public static int GetColumnSpan(DependencyObject obj)
        => (int)obj.GetValue(ColumnSpanProperty);

    public static void SetColumnSpan(DependencyObject obj, int value)
        => obj.SetValue(ColumnSpanProperty, value);

    public static readonly DependencyProperty ColumnSpanProperty = DependencyProperty.RegisterAttached(
        "ColumnSpan",
        typeof(int),
        typeof(FlowGridItem),
        new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsParentMeasure, OnSpanChanged, SpanCoerce));

    #endregion

    #region Column

    public int Column
    {
        get => (int)GetValue(ColumnProperty);
        private set => SetValue(_columnPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _columnPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(Column),
        typeof(int),
        typeof(FlowGridItem),
        new PropertyMetadata(default(int)));

    public static readonly DependencyProperty ColumnProperty = _columnPropertyKey.DependencyProperty;

    #endregion

    #region RowSpan

    public int RowSpan
    {
        get => (int)GetValue(RowSpanProperty);
        set => SetValue(RowSpanProperty, value);
    }

    public static int GetRowSpan(DependencyObject obj)
        => (int)obj.GetValue(RowSpanProperty);

    public static void SetRowSpan(DependencyObject obj, int value)
        => obj.SetValue(RowSpanProperty, value);

    public static readonly DependencyProperty RowSpanProperty = DependencyProperty.RegisterAttached(
        "RowSpan",
        typeof(int),
        typeof(FlowGridItem),
        new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsParentMeasure, OnSpanChanged, SpanCoerce));

    #endregion

    #region Row

    public int Row
    {
        get => (int)GetValue(RowProperty);
        private set => SetValue(_rowPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _rowPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(Row),
        typeof(int),
        typeof(FlowGridItem),
        new PropertyMetadata(default(int)));

    public static readonly DependencyProperty RowProperty = _rowPropertyKey.DependencyProperty;

    #endregion

    internal int InternalRow { get; set; }

    internal int InternalRowSpan { get; set; }

    internal int InternalColumn { get; set; }

    internal int InternalColumnSpan { get; set; }

    internal new Size Measure(Size size)
    {
        base.Measure(size);

        if (size.Height.NearlyEqual(double.PositiveInfinity))
        {
            size.Height = DesiredSize.Height;
        }

        return size;
    }

    internal new void Arrange(Rect finalRect)
    {
        base.Arrange(finalRect);

        Row = InternalRow;
        Column = InternalColumn;

        ActualRowSpan = InternalRowSpan;
        ActualColumnSpan = InternalColumnSpan;
    }

    internal void Reset()
    {
        InternalRow = 0;
        InternalRowSpan = RowSpan;
        InternalColumn = 0;
        InternalColumnSpan = ColumnSpan;
    }

    private static void OnSpanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FlowGridItem)
        {
            var parent = d.FindVisualParent<FlowGridItem>();
            if (parent != null)
            {
                parent.SetBinding(e.Property, new Binding() { Source = d, Path = e.Property.AsPath() });
            }
            else
            {
                d.WhenLoaded(() =>
                {
                    d.FindVisualParent<FlowGridItem>()?.SetBinding(e.Property, new Binding() { Source = d, Path = e.Property.AsPath() });
                });
            }
        }
    }

    private static object SpanCoerce(DependencyObject d, object baseValue)
        => (int)baseValue <= 0 ? 1 : baseValue;
}
