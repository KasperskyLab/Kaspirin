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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class DateTimePopupSelector : Canvas
    {
        public DateTimePopupSelector()
        {
            Items = new();
            Items.CollectionChanged += OnItemsCollectionChanged;

            AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OnDateTimePopupSelectorItemButtonClicked));
            SetValue(Canvas.ClipToBoundsProperty, true);
            SetValue(Canvas.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
            SetValue(Canvas.VerticalAlignmentProperty, VerticalAlignment.Stretch);
            SetValue(Canvas.BackgroundProperty, Brushes.Transparent);
            SetBinding(Canvas.WidthProperty, new Binding() { Source = this, Path = ItemWidthProperty.AsPath() });
            SetBinding(Canvas.HeightProperty, new MultiBinding()
            {
                Bindings =
                {
                    new Binding(){ Source = this, Path = ItemHeightProperty.AsPath() },
                    new Binding(){ Source = this, Path = VisibleItemsCountProperty.AsPath() },
                },
                Converter = new DelegateMultiConverter(values => (double)values[0]! * (int)values[1]!)
            });

            Loaded += DateTimePopupSelector_Loaded;
            MouseWheel += OnTrackMouseWheel;
        }

        private void DateTimePopupSelector_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateButtons();
            UpdateSelectedItemButton();
        }

        #region Items

        public ObservableCollection<DateTimePopupItem> Items
        {
            get { return (ObservableCollection<DateTimePopupItem>)GetValue(ItemsProperty); }
            private set { SetValue(_itemsPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _itemsPropertyKey =
            DependencyProperty.RegisterReadOnly("Items", typeof(ObservableCollection<DateTimePopupItem>), typeof(DateTimePopupSelector),
                new PropertyMetadata(default(ObservableCollection<DateTimePopupItem>)));

        public static readonly DependencyProperty ItemsProperty =
            _itemsPropertyKey.DependencyProperty;

        #endregion

        #region ItemHeight

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(DateTimePopupSelector),
                new PropertyMetadata(32D, OnItemHeightChanged));

        private static void OnItemHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupSelector)d).GenerateButtons();
            ((DateTimePopupSelector)d).UpdateSelectedItemButton();
        }

        #endregion

        #region ItemWidth

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(DateTimePopupSelector));

        #endregion

        #region ItemPadding

        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(DateTimePopupSelector));

        #endregion

        #region ItemVerticalContentAlignment

        public VerticalAlignment ItemVerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(ItemVerticalContentAlignmentProperty); }
            set { SetValue(ItemVerticalContentAlignmentProperty, value); }
        }

        public static readonly DependencyProperty ItemVerticalContentAlignmentProperty =
            DependencyProperty.Register("ItemVerticalContentAlignment", typeof(VerticalAlignment), typeof(DateTimePopupSelector),
                new PropertyMetadata(VerticalAlignment.Center));

        #endregion

        #region ItemHorizontalContentAlignment

        public HorizontalAlignment ItemHorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(ItemHorizontalContentAlignmentProperty); }
            set { SetValue(ItemHorizontalContentAlignmentProperty, value); }
        }

        public static readonly DependencyProperty ItemHorizontalContentAlignmentProperty =
            DependencyProperty.Register("ItemHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(DateTimePopupSelector),
                new PropertyMetadata(HorizontalAlignment.Center));

        #endregion

        #region ItemTextStyle

        public Style ItemTextStyle
        {
            get { return (Style)GetValue(ItemTextStyleProperty); }
            set { SetValue(ItemTextStyleProperty, value); }
        }

        public static readonly DependencyProperty ItemTextStyleProperty =
            DependencyProperty.Register("ItemTextStyle", typeof(Style), typeof(DateTimePopupSelector));

        #endregion

        #region ItemButtonStyle

        public Style ItemButtonStyle
        {
            get { return (Style)GetValue(ItemButtonStyleProperty); }
            set { SetValue(ItemButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ItemButtonStyleProperty =
            DependencyProperty.Register("ItemButtonStyle", typeof(Style), typeof(DateTimePopupSelector));

        #endregion

        #region SelectedItem 

        public DateTimePopupItem? SelectedItem
        {
            get { return (DateTimePopupItem?)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(DateTimePopupItem), typeof(DateTimePopupSelector),
                new PropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupSelector)d).UpdateSelectedItemButton();
            ((DateTimePopupSelector)d).RaiseSelectedItemChanged();
        }

        #endregion

        #region SelectedItemChanged 

        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedItemChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DateTimePopupSelector));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }

        #endregion

        #region VisibleItemsCount

        public int VisibleItemsCount
        {
            get { return (int)GetValue(VisibleItemsCountProperty); }
            set { SetValue(VisibleItemsCountProperty, value); }
        }

        public static readonly DependencyProperty VisibleItemsCountProperty =
            DependencyProperty.Register("VisibleItemsCount", typeof(int), typeof(DateTimePopupSelector),
                new PropertyMetadata(7, OnVisibleItemsCountChanged));

        private static void OnVisibleItemsCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupSelector)d).GenerateButtons();
            ((DateTimePopupSelector)d).UpdateSelectedItemButton();
        }

        #endregion

        private void OnTrackMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                SelectNext();
            }
            else
            {
                SelectPrev();
            }

            e.Handled = true;
        }

        private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            ClearButtonBindings();
            UpdateItemWidth();
        }

        private void OnDateTimePopupSelectorItemButtonClicked(object sender, RoutedEventArgs e)
        {
            var itemButton = (DateTimePopupItemButton)e.OriginalSource;
            var item = (DateTimePopupItem)itemButton.DataContext;

            SelectedItem = item;
        }

        private void SelectNext()
        {
            if (SelectedItem != null)
            {
                SelectedItem = GetElementByIndex(Items, Items.IndexOf(SelectedItem) + 1);
            }
        }

        private void SelectPrev()
        {
            if (SelectedItem != null)
            {
                SelectedItem = GetElementByIndex(Items, Items.IndexOf(SelectedItem) - 1);
            }
        }

        private void UpdateSelectedItemButton()
        {
            if (SelectedItem == null)
            {
                return;
            }

            UpdateButtonBindings();

            var buttonToSelect = _buttons.GuardedSingleOrDefault(b => b.DataContext == SelectedItem);
            buttonToSelect?.Select();
        }

        private void UpdateItemWidth()
        {
            if (Items.Count == 0)
            {
                return;
            }

            var source = DependencyPropertyHelper.GetValueSource(this, ItemWidthProperty);
            if (source.BaseValueSource == BaseValueSource.Default ||
                source.IsCurrent)
            {
                SetCurrentValue(ItemWidthProperty, Items.Max(d => GetStringWidth(d.DisplayText)));
            }
        }

        private void RaiseSelectedItemChanged()
        {
            RaiseEvent(new RoutedEventArgs(SelectedItemChangedEvent));
        }

        private void GenerateButtons()
        {
            var itemHeight = ItemHeight;
            var buttonsCount = VisibleItemsCount + 2;
            var centerOffset = buttonsCount / 2 * itemHeight;
            var maxOffset = buttonsCount * itemHeight;
            var minOffset = 0;

            _buttons.ForEach(button =>
            {
                button.MinOffsetAchieved -= OnMinOffsetAchieved;
                button.MaxOffsetAchieved -= OnMaxOffsetAchieved;
                button.Selected -= EnsureSelectionValid;
            });
            _buttons.Clear();

            Children.Clear();

            DateTimePopupItemButton? currentButton = null;
            for (var i = 0; i < buttonsCount; i++)
            {
                var offset = itemHeight * i;

                var button = new DateTimePopupItemButton(offset, centerOffset, minOffset, maxOffset);
                button.SetValue(DateTimePopupItemButton.DataContextProperty, DateTimePopupItem.Empty);
                button.SetBinding(DateTimePopupItemButton.ContentProperty, new Binding(nameof(DateTimePopupItem.DisplayText)));
                button.SetBinding(DateTimePopupItemButton.VisibilityProperty, new Binding(nameof(DateTimePopupItem.IsVisible)) { Converter = new BooleanToVisibilityConverter() });
                button.SetBinding(DateTimePopupItemButton.TextStyleProperty, new Binding() { Source = this, Path = ItemTextStyleProperty.AsPath() });
                button.SetBinding(DateTimePopupItemButton.StyleProperty, new Binding() { Source = this, Path = ItemButtonStyleProperty.AsPath() });
                button.SetBinding(DateTimePopupItemButton.VerticalContentAlignmentProperty, new Binding() { Source = this, Path = ItemVerticalContentAlignmentProperty.AsPath() });
                button.SetBinding(DateTimePopupItemButton.HorizontalContentAlignmentProperty, new Binding() { Source = this, Path = ItemHorizontalContentAlignmentProperty.AsPath() });
                button.SetNextButton(currentButton);
                button.MinOffsetAchieved += OnMinOffsetAchieved;
                button.MaxOffsetAchieved += OnMaxOffsetAchieved;
                button.Selected += EnsureSelectionValid;

                var itemContainer = new Border() { Child = button };
                itemContainer.SetValue(Border.HeightProperty, itemHeight);
                itemContainer.SetBinding(Border.WidthProperty, new Binding() { Source = this, Path = ActualWidthProperty.AsPath() });
                itemContainer.SetBinding(Canvas.TopProperty, new Binding()
                {
                    Source = button,
                    Path = DateTimePopupItemButton.CurrentOffsetProperty.AsPath(),
                    Converter = new DelegateConverter(value => (double)value! - itemHeight)
                });

                currentButton = button;

                _buttons.Add(button);

                Children.Add(itemContainer);
            }

            _buttons.First().SetNextButton(_buttons.Last());
        }

        private void OnMinOffsetAchieved(DateTimePopupItemButton button)
        {
            var currentItem = (DateTimePopupItem)button.DataContext;
            if (currentItem == DateTimePopupItem.Stub)
            {
                return;
            }

            var currentIndex = Items.IndexOf(currentItem);

            var newIndex = currentIndex + _buttons.Count;
            var newItem = GetElementByIndex(Items, newIndex);

            SetButtonDataContext(button, newItem);
        }

        private void OnMaxOffsetAchieved(DateTimePopupItemButton button)
        {
            var currentItem = (DateTimePopupItem)button.DataContext;
            if (currentItem == DateTimePopupItem.Stub)
            {
                return;
            }

            var currentIndex = Items.IndexOf(currentItem);

            var newIndex = currentIndex - _buttons.Count;
            var newItem = GetElementByIndex(Items, newIndex);

            SetButtonDataContext(button, newItem);
        }

        private void ClearButtonBindings()
        {
            _buttons.ForEach(b => b.DataContext = DateTimePopupItem.Empty);
        }

        private void UpdateButtonBindings()
        {
            if (SelectedItem == null)
            {
                return;
            }

            if (_buttons.All(b => b.DataContext != DateTimePopupItem.Empty))
            {
                return;
            }

            ClearButtonBindings();

            var centralButtonId = (int)Math.Ceiling(_buttons.Count / 2D) - 1;
            var selectedIndex = Items.IndexOf(SelectedItem);

            var selectedButton = _buttons.Where(b => b.IsSelected).SingleOrDefault();
            if (selectedButton == null)
            {
                selectedButton = _buttons[centralButtonId];
                selectedButton.Select();
            }

            var selectedButtonIndex = _buttons.IndexOf(selectedButton);

            SetButtonDataContext(selectedButton, SelectedItem);

            var itemsArranged = Items.ToList();
            if (itemsArranged.Count < _buttons.Count)
            {
                itemsArranged.AddRange(Enumerable.Repeat(DateTimePopupItem.Stub, _buttons.Count - itemsArranged.Count));
            }

            for (var i = 1; i <= centralButtonId; i++)
            {
                var prevButtonId = selectedButtonIndex - i;
                var prevButtonItemId = selectedIndex - i;
                var prevButton = GetElementByIndex(_buttons, prevButtonId);
                var prevButtonItem = GetElementByIndex(itemsArranged, prevButtonItemId);

                var nextButtonId = selectedButtonIndex + i;
                var nextButtonItemId = selectedIndex + i;
                var nextButton = GetElementByIndex(_buttons, nextButtonId);
                var nextButtonItem = GetElementByIndex(itemsArranged, nextButtonItemId);

                SetButtonDataContext(prevButton, prevButtonItem);
                SetButtonDataContext(nextButton, nextButtonItem);
            }
        }

        private void EnsureSelectionValid(DateTimePopupItemButton button)
        {
            var expectedSelectedItem = SelectedItem;
            var actualSelectedItem = button.DataContext as DateTimePopupItem;

            if (actualSelectedItem != expectedSelectedItem)
            {
                SelectedItem = actualSelectedItem;
            }
        }

        private void SetButtonDataContext(DateTimePopupItemButton? button, DateTimePopupItem? dataContext)
        {
            var buttonDataContext = button?.DataContext as DateTimePopupItem;
            if (buttonDataContext == dataContext)
            {
                return;
            }

            if (button != null && _buttons.Contains(button) == false)
            {
                throw new InvalidOperationException($"Button does not exists in buttons list.");
            }

            var alreadyExists = _buttons.Any(button => button.DataContext == dataContext && dataContext != DateTimePopupItem.Stub);
            if (alreadyExists)
            {
                throw new InvalidOperationException($"Button with DataContext '{dataContext?.DisplayText}' already exists in buttons list");
            }

            if (button != null)
            {
                button.DataContext = dataContext;
            }
        }

        private static TItem? GetElementByIndex<TItem>(ICollection<TItem> collection, int index)
        {
            if (collection.Count == 0)
            {
                return default;
            }

            var minIndex = 0;
            var maxIndex = collection.Count - 1;

            var effectiveIndex = index;

            while (effectiveIndex > maxIndex)
            {
                effectiveIndex -= collection.Count;
            }

            while (effectiveIndex < minIndex)
            {
                effectiveIndex += collection.Count;
            }

            var item = collection.ElementAt(effectiveIndex);
            return item;
        }

        private double GetStringWidth(string stringValue)
        {
            if (ItemTextStyle == null)
            {
                return 0;
            }

            var fontSize = ItemTextStyle.GetPropertyValueOrDefault<double>(Control.FontSizeProperty);
            var typeface = new Typeface(
                     ItemTextStyle.GetPropertyValueOrDefault<FontFamily>(Control.FontFamilyProperty),
                     ItemTextStyle.GetPropertyValueOrDefault<FontStyle>(Control.FontStyleProperty),
                     ItemTextStyle.GetPropertyValueOrDefault<FontWeight>(Control.FontWeightProperty),
                     ItemTextStyle.GetPropertyValueOrDefault<FontStretch>(Control.FontStretchProperty));

            var formattedText = new FormattedText(stringValue, _currentCulture, FlowDirection, typeface, fontSize, Brushes.Black);

            return ItemPadding.Left + Math.Round(formattedText.Width) + ItemPadding.Right;
        }

        private readonly List<DateTimePopupItemButton> _buttons = new();
        private readonly CultureInfo _currentCulture = LocalizationManager.Current.DisplayCulture.CultureInfo;
    }
}
