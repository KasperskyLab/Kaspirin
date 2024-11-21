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
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Kaspirin.UI.Framework.UiKit.Controls.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Extensions.Internals;
using WpfPopup = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(SelectItem))]
    [TemplatePart(Name = PART_Popup, Type = typeof(WpfPopup))]
    [TemplatePart(Name = PART_SelectPresenter, Type = typeof(SelectPresenter))]
    public sealed class Select : SelectorList<SelectItem>
    {
        public const string PART_Popup = "PART_Popup";
        public const string PART_SelectPresenter = "PART_SelectPresenter";

        static Select()
        {
            IsTabStopProperty.OverrideMetadata(typeof(Select), new FrameworkPropertyMetadata(true));
            SelectionModeProperty.OverrideMetadata(typeof(Select), new FrameworkPropertyMetadata(SelectionMode.Single, FrameworkPropertyMetadataOptions.None, null, CoerceSelectionMode));
        }

        public Select()
        {
            _infoProvider = new SelectInfoProvider(this);

            SetValue(Grid.IsSharedSizeScopeProperty, true);

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        #region Caption

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(Select));

        #endregion

        #region MaxDropDownWidth

        public double MaxDropDownWidth
        {
            get { return (double)GetValue(MaxDropDownWidthProperty); }
            set { SetValue(MaxDropDownWidthProperty, value); }
        }

        public static readonly DependencyProperty MaxDropDownWidthProperty =
            DependencyProperty.Register("MaxDropDownWidth", typeof(double), typeof(Select),
                new PropertyMetadata(SystemParameters.PrimaryScreenWidth));

        #endregion

        #region MaxDropDownHeight

        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(Select),
                new PropertyMetadata(SystemParameters.PrimaryScreenHeight / 3));

        #endregion

        #region IsDropDownOpen

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public static readonly DependencyProperty IsDropDownOpenProperty =
                DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(Select),
                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDropDownOpenChanged, CoerceIsDropDownOpen));

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((Select)d).OnIsDropDownOpenChanged();

        private static object CoerceIsDropDownOpen(DependencyObject d, object value)
            => value is true ? ((FrameworkElement)d).CoerceWhenLoaded(IsDropDownOpenProperty) : value;

        #endregion

        #region IsInvalidState

        public bool IsInvalidState
        {
            get { return (bool)GetValue(IsInvalidStateProperty); }
            set { SetValue(IsInvalidStateProperty, value); }
        }

        public static readonly DependencyProperty IsInvalidStateProperty =
            DependencyProperty.Register("IsInvalidState", typeof(bool), typeof(Select));

        #endregion

        #region InvalidStatePopupContent

        public string InvalidStatePopupContent
        {
            get { return (string)GetValue(InvalidStatePopupContentProperty); }
            set { SetValue(InvalidStatePopupContentProperty, value); }
        }

        public static readonly DependencyProperty InvalidStatePopupContentProperty =
            DependencyProperty.Register("InvalidStatePopupContent", typeof(string), typeof(Select));

        #endregion

        #region InvalidStatePopupPosition

        public PopupPosition InvalidStatePopupPosition
        {
            get { return (PopupPosition)GetValue(InvalidStatePopupPositionProperty); }
            set { SetValue(InvalidStatePopupPositionProperty, value); }
        }

        public static readonly DependencyProperty InvalidStatePopupPositionProperty =
            DependencyProperty.Register("InvalidStatePopupPosition", typeof(PopupPosition), typeof(Select),
                new PropertyMetadata(PopupPosition.Right));

        #endregion

        #region IsFilterEnabled

        public bool IsFilterEnabled
        {
            get { return (bool)GetValue(IsFilterEnabledProperty); }
            set { SetValue(IsFilterEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsFilterEnabledProperty =
            DependencyProperty.Register("IsFilterEnabled", typeof(bool), typeof(Select));

        #endregion

        #region IsFilterHasResult

        public bool IsFilterHasResult
        {
            get { return (bool)GetValue(IsFilterHasResultProperty); }
            private set { SetValue(_isFilterHasResultPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _isFilterHasResultPropertyKey =
            DependencyProperty.RegisterReadOnly("IsFilterHasResult", typeof(bool), typeof(Select),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IsFilterHasResultProperty =
            _isFilterHasResultPropertyKey.DependencyProperty;

        #endregion

        #region GetFocusBehavior

        public InputGetFocusBehaviorType GetFocusBehavior
        {
            get { return (InputGetFocusBehaviorType)GetValue(GetFocusBehaviorProperty); }
            set { SetValue(GetFocusBehaviorProperty, value); }
        }

        public static readonly DependencyProperty GetFocusBehaviorProperty =
            DependencyProperty.Register("GetFocusBehavior", typeof(InputGetFocusBehaviorType), typeof(Select),
                new PropertyMetadata(InputGetFocusBehaviorType.Default, OnGetFocusBehaviorChanged));

        private static void OnGetFocusBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Select)d).UpdateGetFocusBehavior();
        }

        #endregion

        #region LostFocusBehavior

        public InputLostFocusBehaviorType LostFocusBehavior
        {
            get { return (InputLostFocusBehaviorType)GetValue(LostFocusBehaviorProperty); }
            set { SetValue(LostFocusBehaviorProperty, value); }
        }

        public static readonly DependencyProperty LostFocusBehaviorProperty =
            DependencyProperty.Register("LostFocusBehavior", typeof(InputLostFocusBehaviorType), typeof(Select),
                new PropertyMetadata(InputLostFocusBehaviorType.Default));

        #endregion

        #region InputFilter

        public IInputFilter? InputFilter
        {
            get => (IInputFilter?)GetValue(InputFilterProperty);
            set => SetValue(InputFilterProperty, value);
        }

        public static readonly DependencyProperty InputFilterProperty = DependencyProperty.Register(
            nameof(InputFilter),
            typeof(IInputFilter),
            typeof(Select));

        #endregion

        #region Label

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(Select));

        #endregion

        #region Placeholder

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(Select));

        #endregion

        #region RightBar

        public object RightBar
        {
            get { return (object)GetValue(RightBarProperty); }
            set { SetValue(RightBarProperty, value); }
        }

        public static readonly DependencyProperty RightBarProperty =
            DependencyProperty.Register("RightBar", typeof(object), typeof(Select), new PropertyMetadata(null, OnRightBarChanged));

        private static void OnRightBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Select)d).UpdatePresenter();
        }

        #endregion

        #region RightBarTemplate

        public DataTemplate RightBarTemplate
        {
            get { return (DataTemplate)GetValue(RightBarTemplateProperty); }
            set { SetValue(RightBarTemplateProperty, value); }
        }

        public static readonly DependencyProperty RightBarTemplateProperty =
            DependencyProperty.Register("RightBarTemplate", typeof(DataTemplate), typeof(Select), new PropertyMetadata(null, OnRightBarTemplateChanged));

        private static void OnRightBarTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Select)d).UpdatePresenter();
        }

        #endregion

        #region SelectedItemContainer

        public SelectItem? SelectedItemContainer
        {
            get => (SelectItem?)GetValue(SelectedItemContainerProperty);
            private set => SetValue(_selectedItemContainerPropertyKey, value);
        }

        private static readonly DependencyPropertyKey _selectedItemContainerPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(SelectedItemContainer),
            typeof(SelectItem),
            typeof(Select),
            new PropertyMetadata(default(SelectItem?)));

        public static readonly DependencyProperty SelectedItemContainerProperty = _selectedItemContainerPropertyKey.DependencyProperty;

        #endregion

        #region SelectWidth

        public double SelectWidth
        {
            get { return (double)GetValue(SelectWidthProperty); }
            set { SetValue(SelectWidthProperty, value); }
        }

        public static readonly DependencyProperty SelectWidthProperty =
            DependencyProperty.Register("SelectWidth", typeof(double), typeof(Select), new PropertyMetadata(double.NaN));

        #endregion

        #region SelectHorizontalAlignment

        public HorizontalAlignment SelectHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(SelectHorizontalAlignmentProperty); }
            set { SetValue(SelectHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty SelectHorizontalAlignmentProperty =
            DependencyProperty.Register("SelectHorizontalAlignment", typeof(HorizontalAlignment), typeof(Select), new PropertyMetadata(HorizontalAlignment.Left));

        #endregion

        #region SetFocusFlag

        public bool SetFocusFlag
        {
            get => (bool)GetValue(SetFocusFlagProperty);
            set => SetValue(SetFocusFlagProperty, value);
        }

        public static readonly DependencyProperty SetFocusFlagProperty = DependencyProperty.Register(
            nameof(SetFocusFlag),
            typeof(bool),
            typeof(Select),
            new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSetFocusFlagChanged));

        private static void OnSetFocusFlagChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var select = (Select)d;
            if (select.SetFocusFlag)
            {
                select.SetFocus();

                Executers.InUiAsync(() => select.SetCurrentValue(SetFocusFlagProperty, false));
            }
        }

        #endregion

        #region Accessibility

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new SelectAutomationPeer(this);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _presenter = (SelectPresenter)GetTemplateChild(PART_SelectPresenter);
            _presenter.Click += OnPresenterClick;
            _presenter.FilterTextChanged += OnPresenterFilterTextChanged;
            _presenter.SetBinding(SelectPresenter.InputFilterProperty, new Binding()
            {
                Source = this,
                Path = InputFilterProperty.AsPath(),
            });
            _presenter.SetBinding(SelectPresenter.IsInvalidStateProperty, new Binding()
            {
                Source = this,
                Path = IsInvalidStateProperty.AsPath(),
            });
            _presenter.SetBinding(SelectPresenter.IsFilterEnabledProperty, new Binding()
            {
                Source = this,
                Path = IsFilterEnabledProperty.AsPath(),
            });

            _popup = Guard.EnsureIsInstanceOfType<WpfPopup>(GetTemplateChild(PART_Popup));
            _popup.PlacementTarget = _presenter;
            _popup.StaysOpen = false;
            _popup.AllowsTransparency = true;
            _popup.SetBinding(WpfPopup.IsOpenProperty, new Binding()
            {
                Source = this,
                Path = IsDropDownOpenProperty.AsPath(),
                Mode = BindingMode.TwoWay
            });

            this.WhenLoaded(() =>
            {
                UpdatePresenter();
                UpdateLostFocusBehavior();
            });
        }

        internal void NotifySelectItemMouseUp(SelectItem selectItem)
        {
            SelectedItem = _infoProvider.GetTargetInfo(selectItem)?.GetItem();

            IsDropDownOpen = false;
        }

        internal void NotifySelectItemMouseEnter(SelectItem selectItem)
            => _infoProvider.SetHighlightedInfo(selectItem);

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            UpdatePresenter();
            UpdateScroll();

            base.OnSelectionChanged(e);
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= OnItemsSourceCollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += OnItemsSourceCollectionChanged;
            }

            _infoProvider.ClearCache();

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        private void OnItemsSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _infoProvider.ClearCache();

            _popup?.WhenOpened(() =>
            {
                SetPopupSize();
            });
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            _presenter?.Focus();

            base.OnGotFocus(e);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            var isPopupSource = (e.OriginalSource as FrameworkElement)?.Parent == _popup;

            if (!IsDropDownOpen && IsKeyboardFocusWithin)
            {
                var direction = e.Delta > 0
                    ? MoveDirection.Previous
                    : MoveDirection.Next;

                MoveSelection(direction);

                e.Handled = true;
            }

            if (IsDropDownOpen && isPopupSource)
            {
                e.Handled = true;
            }

            base.OnPreviewMouseWheel(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            var isPopupSource = (e.OriginalSource as FrameworkElement)?.Parent == _popup;

            if (IsDropDownOpen && isPopupSource)
            {
                IsDropDownOpen = false;

                e.Handled = true;
            }

            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            var key = e.Key;
            if (key == Key.System)
            {
                key = e.SystemKey;
            }

            if (key == Key.Up || key == Key.Down)
            {
                var isAltPressed = (e.KeyboardDevice.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt;
                if (isAltPressed && key == Key.Down)
                {
                    IsDropDownOpen = true;
                }
                else if (isAltPressed && key == Key.Up)
                {
                    IsDropDownOpen = false;
                }
                else
                {
                    var direction = key == Key.Up
                        ? MoveDirection.Previous
                        : MoveDirection.Next;

                    if (IsDropDownOpen)
                    {
                        MoveHighlight(direction);
                    }
                    else
                    {
                        MoveSelection(direction);
                    }
                }

                e.Handled = true;
                return;
            }

            if (IsDropDownOpen)
            {
                if ((key == Key.Home || key == Key.End) && !IsFilterEnabled)
                {
                    var direction = key == Key.Home
                        ? MoveDirection.First
                        : MoveDirection.Last;

                    MoveHighlight(direction);

                    e.Handled = true;
                    return;
                }

                if (key == Key.Enter)
                {
                    SelectHighlight();

                    IsDropDownOpen = false;

                    e.Handled = true;
                    return;
                }
                else if (key == Key.Space && !IsFilterEnabled)
                {
                    SelectHighlight();

                    e.Handled = true;
                    return;
                }
                else if (key == Key.Escape)
                {
                    IsDropDownOpen = false;

                    e.Handled = true;
                    return;
                }
            }
            else
            {
                if (key == Key.Home || key == Key.End)
                {
                    var direction = key == Key.Home
                        ? MoveDirection.First
                        : MoveDirection.Last;

                    MoveSelection(direction);

                    e.Handled = true;
                    return;
                }

                if (key == Key.Enter || key == Key.Space || key == Key.F2)
                {
                    IsDropDownOpen = true;

                    e.Handled = true;
                    return;
                }
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsDropDownOpen && !IsKeyboardFocusWithin)
            {
                IsDropDownOpen = false;
            }

            base.OnIsKeyboardFocusWithinChanged(e);
        }

        private void OnIsDropDownOpenChanged()
        {
            if (IsDropDownOpen || IsKeyboardFocusWithin)
            {
                Focus();
            }

            if (!IsDropDownOpen)
            {
                ResetFilter();
                ResetHighlight();
            }

            UpdatePresenter();
            UpdateScroll();
        }

        private void OnPresenterClick(object sender, RoutedEventArgs e)
            => IsDropDownOpen = true;

        private void OnPresenterFilterTextChanged(object sender, RoutedEventArgs e)
            => ApplyFilter();

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ItemsSource is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged -= OnItemsSourceCollectionChanged;
                observableCollection.CollectionChanged += OnItemsSourceCollectionChanged;
            }

            var notificationLayer = NotificationLayer.FindLayer(this, isModal: true);
            if (notificationLayer != null)
            {
                _notificationLayer = notificationLayer;
                _notificationLayer.IsModalStateChanged -= OnNotificationLayerStateChanged;
                _notificationLayer.IsModalStateChanged += OnNotificationLayerStateChanged;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (ItemsSource is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged -= OnItemsSourceCollectionChanged;
            }

            if (_notificationLayer != null)
            {
                _notificationLayer.IsModalStateChanged -= OnNotificationLayerStateChanged;
                _notificationLayer = null;
            }
        }

        private void OnNotificationLayerStateChanged(object sender, RoutedEventArgs e)
            => IsDropDownOpen = false;

        private void ApplyFilter()
        {
            var filterText = _presenter?.FilterText ?? string.Empty;

            UpdateFilteredItems(filterText);
        }

        private void ResetFilter()
        {
            var filterText = string.Empty;

            UpdateFilteredItems(filterText, resetFilter: true);
        }

        private void ResetHighlight()
            => _infoProvider.SetHighlightedInfo(null);

        private void MoveHighlight(MoveDirection direction)
        {
            var currItem = _infoProvider.GetHighlightedInfo();
            if (currItem == null)
            {
                currItem = _infoProvider.GetSelectedInfo().FirstOrDefault();
            }

            var nextItem = GetNextItem(currItem, direction);
            if (nextItem != null)
            {
                _infoProvider.SetHighlightedInfo(nextItem, scrollIntoView: true);
            }
        }

        private void MoveSelection(MoveDirection direction)
            => SelectedIndex = GetNextIndex(SelectedIndex, direction);

        private void SelectHighlight()
        {
            var highlightedInfo = _infoProvider.GetHighlightedInfo();
            if (highlightedInfo != null)
            {
                SelectedItem = highlightedInfo.GetItem();
            }
        }

        private void UpdateFilteredItems(string filterText, bool resetFilter = false)
        {
            if (resetFilter && Items.Filter == null)
            {
                return;
            }

            var filteredItems = _infoProvider.GetFilteredInfo(filterText, fromSource: true).Select(i => i.GetItem()).ToList();
            var itemsCount = Items.SourceCollection?.CountNonGeneric() ?? 0;

            IsFilterHasResult = filteredItems.Any() || itemsCount == 0;

            try
            {
                _isFilterApplying = true;

                if (filteredItems.Count == 0 ||
                    filteredItems.Count == itemsCount)
                {
                    if (Items.Filter != null)
                    {
                        Items.Filter = null;
                    }
                }
                else
                {
                    if (filteredItems.Contains(SelectedItem) is false)
                    {
                        SelectedItem = filteredItems[0];
                    }

                    Items.Filter = filteredItems.Contains;
                }
            }
            finally
            {
                _isFilterApplying = false;
            }

            _infoProvider.SetHighlightedInfo(null);

            UpdateScroll();
        }

        private SelectItemInfoProvider? GetNextItem(SelectItemInfoProvider? currItem, MoveDirection direction)
        {
            var firstIndex = 0;
            var lastIndex = Items.Count - 1;
            var nextIndex = -1;

            if (currItem != null)
            {
                nextIndex = ItemContainerGenerator.IndexFromContainer(currItem.GetContainer());
            }

            while (true)
            {
                nextIndex = GetNextIndex(nextIndex, direction);

                if (TryGetItemContainerFromIndex(nextIndex, out var nextItemContainer))
                {
                    return _infoProvider.GetTargetInfo(nextItemContainer);
                }

                if (nextIndex == lastIndex || nextIndex == firstIndex)
                {
                    break;
                }
            }

            return null;
        }

        private bool TryGetItemContainerFromIndex(int index, out object? itemContainer)
        {
            itemContainer = (SelectItem)ItemContainerGenerator.ContainerFromIndex(index);

            if (itemContainer != null)
            {
                return true;
            }

            ScrollIntoView(Items[index]);

            itemContainer = (SelectItem)ItemContainerGenerator.ContainerFromIndex(index);

            if (itemContainer != null)
            {
                return true;
            }

            return false;
        }

        private int GetNextIndex(int currIndex, MoveDirection direction)
        {
            var firstIndex = 0;
            var lastIndex = Items.Count - 1;
            var nextIndex = currIndex;

            nextIndex += direction switch
            {
                MoveDirection.Next => 1,
                MoveDirection.Previous => -1,
                MoveDirection.First => firstIndex - nextIndex,
                MoveDirection.Last => lastIndex - nextIndex,
                _ => throw new NotImplementedException(),
            };

            if (nextIndex > lastIndex)
            {
                nextIndex = lastIndex;
            }
            else if (nextIndex < firstIndex)
            {
                nextIndex = firstIndex;
            }

            return nextIndex;
        }

        private void UpdateScroll()
        {
            if (IsDropDownOpen && SelectedItem != null)
            {
                ScrollIntoView(SelectedItem);
            }
        }

        private void UpdatePresenter()
        {
            if (_isFilterApplying)
            {
                return;
            }

            if (_presenter != null)
            {
                var itemInfo = _infoProvider.GetTargetInfo(SelectedItem, fromSource: true);
                var hasRightBar = RightBar != null || RightBarTemplate != null;

                SelectedItemContainer = itemInfo?.GetContainer();

                _presenter.UpdatePresenter(SelectedItemContainer, hasRightBar);
                _presenter.IsActive = IsDropDownOpen;
            }
        }

        private void UpdateGetFocusBehavior()
        {
            void Focus()
                => SetFocus();

            void FocusAndSelect()
            {
                if (IsFilterEnabled)
                {
                    IsDropDownOpen = true;
                }
                else
                {
                    SetFocus();
                }
            }

            switch (GetFocusBehavior)
            {
                case InputGetFocusBehaviorType.FocusWhenLoaded:
                    this.WhenLoaded(Focus);
                    break;

                case InputGetFocusBehaviorType.FocusWhenVisible:
                    this.WhenVisible(Focus);
                    break;

                case InputGetFocusBehaviorType.FocusAndSelectWhenLoaded:
                    this.WhenLoaded(FocusAndSelect);
                    break;

                case InputGetFocusBehaviorType.FocusAndSelectWhenVisible:
                    this.WhenVisible(FocusAndSelect);
                    break;

                case InputGetFocusBehaviorType.Default:
                default:
                    break;
            }
        }

        private void UpdateLostFocusBehavior()
            => this.GetWindow()?.SetValue(SelectLostFocusHandler.IsEnabledProperty, true);

        private void SetFocus()
        {
            var isUnloaded = false;

            this.WhenUnloaded(() => isUnloaded = true);

            Executers.InUiAsync(() =>
            {
                if (isUnloaded)
                {
                    return;
                }

                if (_presenter == null)
                {
                    return;
                }

                BringIntoView();

                InputFocusManager.SetInputFocus(_presenter);
            },
            DispatcherPriority.Input);
        }

        private void SetPopupSize()
        {
            if (_popup == null)
            {
                return;
            }

            var popupDecorator = Guard.EnsureIsInstanceOfType<SelectPopupDecorator>(_popup.Child);

            popupDecorator.Measure(new Size(MaxDropDownWidth, MaxDropDownHeight));

            _popup.Width = Math.Min(popupDecorator.DesiredSize.Width, MaxDropDownWidth);
            _popup.Height = Math.Min(popupDecorator.DesiredSize.Height, MaxDropDownHeight);
        }

        private static object CoerceSelectionMode(DependencyObject d, object baseValue)
            => SelectionMode.Single;

        private static class SelectLostFocusHandler
        {
            #region IsEnabled

            public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(SelectLostFocusHandler),
                new PropertyMetadata(false, OnIsEnabledChanged));

            public static bool GetIsEnabled(UIElement element)
                => (bool)element.GetValue(IsEnabledProperty);

            public static void SetIsEnabled(UIElement element, bool value)
                => element.SetValue(IsEnabledProperty, value);

            private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var window = (Window)d;

                if (e.NewValue is true)
                {
                    window.MouseLeftButtonDown += OnMouseLeftButtonDown;
                }
                else
                {
                    window.MouseLeftButtonDown -= OnMouseLeftButtonDown;
                }
            }

            #endregion

            private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                var window = (Window)sender;
                var focusedElement = FocusManager.GetFocusedElement(window);
                if (focusedElement is SelectPresenter select)
                {
                    if (select.TemplatedParent is Select parentSelect && parentSelect.LostFocusBehavior == InputLostFocusBehaviorType.WhenClickOutside)
                    {
                        InputFocusManager.ClearInputFocus(select);
                    }
                }
            }
        }

        private enum MoveDirection : int
        {
            Next,
            Previous,
            First,
            Last
        }

        private bool _isFilterApplying;
        private WpfPopup? _popup;
        private SelectPresenter? _presenter;
        private NotificationLayer? _notificationLayer;

        private readonly SelectInfoProvider _infoProvider;
    }
}
