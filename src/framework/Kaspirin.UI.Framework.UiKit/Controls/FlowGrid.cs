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

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_Panel, Type = typeof(FlowGridPanel))]
public sealed class FlowGrid : ItemsControl
{
    public const string PART_Panel = "PART_Panel";

    #region ItemElement

    public DataTemplate ItemElement
    {
        get => (DataTemplate)GetValue(ItemElementProperty);
        set => SetValue(ItemElementProperty, value);
    }

    public static readonly DependencyProperty ItemElementProperty =
        UIKitItemsControlHelper.ItemElementProperty.AddOwner(typeof(FlowGridItem));

    #endregion

    #region ItemElementSelector

    public DataTemplateSelector ItemElementSelector
    {
        get => (DataTemplateSelector)GetValue(ItemElementSelectorProperty);
        set => SetValue(ItemElementSelectorProperty, value);
    }

    public static readonly DependencyProperty ItemElementSelectorProperty =
        UIKitItemsControlHelper.ItemElementSelectorProperty.AddOwner(typeof(FlowGridItem));

    #endregion

    #region Strategies

    public FlowGridStrategyCollection Strategies
    {
        get => (FlowGridStrategyCollection)GetValue(StrategiesProperty);
        set => SetValue(StrategiesProperty, value);
    }

    public static readonly DependencyProperty StrategiesProperty = DependencyProperty.Register(
        nameof(Strategies),
        typeof(FlowGridStrategyCollection),
        typeof(FlowGrid),
        new FrameworkPropertyMetadata(new FlowGridStrategyCollection(), FrameworkPropertyMetadataOptions.AffectsMeasure));

    #endregion

    #region ActualStrategy

    public FlowGridStrategy ActualStrategy
    {
        get => (FlowGridStrategy)GetValue(ActualStrategyProperty);
        internal set => SetValue(_actualStrategyPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _actualStrategyPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(ActualStrategy),
        typeof(FlowGridStrategy),
        typeof(FlowGrid),
        new FrameworkPropertyMetadata(new FlowGridStrategy()));

    public static readonly DependencyProperty ActualStrategyProperty = _actualStrategyPropertyKey.DependencyProperty;

    #endregion

    #region ItemsGrid

    public FlowGridItemsGrid? ItemsGrid
    {
        get => (FlowGridItemsGrid?)GetValue(ItemsGridProperty);
        internal set => SetValue(_itemsGridPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _itemsGridPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(ItemsGrid),
        typeof(FlowGridItemsGrid),
        typeof(FlowGrid),
        new PropertyMetadata(default(FlowGridItemsGrid?)));

    public static readonly DependencyProperty ItemsGridProperty = _itemsGridPropertyKey.DependencyProperty;

    #endregion

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        _processingItem = item;

        return UIKitItemsControlHelper.IsItemContainer<FlowGridItem>(item);
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        var processingItem = _processingItem;
        _processingItem = null;

        return UIKitItemsControlHelper.CreateItemContainer<FlowGridItem>(this, processingItem);
    }

    public override void OnApplyTemplate()
    {
        Guard.IsInstanceOfType<FlowGridPanel>(GetTemplateChild(PART_Panel));

        base.OnApplyTemplate();
    }

    private object? _processingItem;

}
