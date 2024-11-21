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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class SelectItemInfoProvider : DependencyObject
    {
        public SelectItemInfoProvider(ItemsControl itemsControl, object item)
        {
            _itemsControl = Guard.EnsureArgumentIsNotNull(itemsControl);

            _itemContainerGenerator = itemsControl.ItemContainerGenerator;

            if (item is SelectItem itemContainer)
            {
                _item = _itemContainerGenerator.ItemFromContainer(itemContainer);
                _container = new(itemContainer);
            }
            else
            {
                _item = item;
                _container = new(ProvideContainer());
            }

            UpdateProperties();
        }

        #region Icon

        public UIKitIcon_16 Icon
        {
            get => (UIKitIcon_16)GetValue(_iconProperty);
            private set => SetValue(_iconProperty, value);
        }

        private static readonly DependencyProperty _iconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(UIKitIcon_16),
            typeof(SelectItemInfoProvider),
            new PropertyMetadata(UIKitIcon_16.UIKitUnset));

        #endregion

        #region Image

        public ImageSource? Image
        {
            get => (ImageSource?)GetValue(_imageProperty);
            set => SetValue(_imageProperty, value);
        }

        private static readonly DependencyProperty _imageProperty = DependencyProperty.Register(
            nameof(Image),
            typeof(ImageSource),
            typeof(SelectItemInfoProvider),
            new PropertyMetadata(default(ImageSource?)));

        #endregion

        #region ImageFlowDirection

        public FlowDirection ImageFlowDirection
        {
            get => (FlowDirection)GetValue(_imageFlowDirectionProperty);
            set => SetValue(_imageFlowDirectionProperty, value);
        }

        private static readonly DependencyProperty _imageFlowDirectionProperty = DependencyProperty.Register(
            nameof(ImageFlowDirection),
            typeof(FlowDirection),
            typeof(SelectItemInfoProvider),
            new PropertyMetadata(default(FlowDirection)));

        #endregion

        #region Header

        public string Header
        {
            get => (string)GetValue(_headerProperty);
            private set => SetValue(_headerProperty, value);
        }

        private static readonly DependencyProperty _headerProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SelectItemInfoProvider),
            new PropertyMetadata(string.Empty));

        #endregion

        #region IsSelected

        public bool IsSelected
        {
            get => (bool)GetValue(_isSelectedProperty);
            private set => SetValue(_isSelectedProperty, value);
        }

        private static readonly DependencyProperty _isSelectedProperty = DependencyProperty.Register(
            nameof(IsSelected),
            typeof(bool),
            typeof(SelectItemInfoProvider),
            new PropertyMetadata(default(bool)));

        #endregion

        #region IsHighlighted

        public bool IsHighlighted
        {
            get => (bool)GetValue(_isHighlightedProperty);
            private set => SetValue(_isHighlightedProperty, value);
        }

        private static readonly DependencyProperty _isHighlightedProperty = DependencyProperty.Register(
            nameof(IsHighlighted),
            typeof(bool),
            typeof(SelectItemInfoProvider),
            new PropertyMetadata(default(bool)));

        #endregion

        public bool IsAlive => _container.TryGetTarget(out var _);

        public bool ContainsText(string text)
        {
            return Header.Contains(text, StringComparison.InvariantCultureIgnoreCase);
        }

        public object GetItem()
        {
            return _item;
        }

        public SelectItem GetContainer()
        {
            if (_container.TryGetTarget(out var target))
            {
                return target;
            }

            Invalidate();

            if (_container.TryGetTarget(out target))
            {
                return target;
            }

            throw new InvalidOperationException("Container is invalid.");
        }

        public void Invalidate()
        {
            var container = ProvideContainer();

            if (_container.TryGetTarget(out var target) && target == container)
            {
                return;
            }

            _container = new(container);

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            _container.TryGetTarget(out var container);

            Guard.IsNotNull(container);

            UpdateContext(() =>
            {
                BindingOperations.SetBinding(this, _isHighlightedProperty, new Binding() { Source = container, Path = SelectItem._isHighlightedProperty.AsPath(), Mode = BindingMode.OneWay });
                BindingOperations.SetBinding(this, _isSelectedProperty, new Binding() { Source = container, Path = SelectItem.IsSelectedProperty.AsPath(), Mode = BindingMode.OneWay });
                BindingOperations.SetBinding(this, _headerProperty, new Binding() { Source = container, Path = SelectItem.HeaderProperty.AsPath(), Mode = BindingMode.OneWay });
                BindingOperations.SetBinding(this, _iconProperty, new Binding() { Source = container, Path = SelectItem.IconProperty.AsPath(), Mode = BindingMode.OneWay });
                BindingOperations.SetBinding(this, _imageProperty, new Binding() { Source = container, Path = SelectItem.ImageProperty.AsPath(), Mode = BindingMode.OneWay });
                BindingOperations.SetBinding(this, _imageFlowDirectionProperty, new Binding() { Source = container, Path = SelectItem.ImageFlowDirectionProperty.AsPath(), Mode = BindingMode.OneWay });
            });
        }

        private void UpdateContext(Action action)
        {
            if (_isUpdating)
            {
                return;
            }

            _isUpdating = true;

            try
            {
                action();
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private SelectItem ProvideContainer()
        {
            return TryGetEffectiveContainer() ?? GetFakeContainer();
        }

        private SelectItem GetFakeContainer()
        {
            if (_container?.TryGetTarget(out var container) == true && container?.GetValue<bool>(_isFakeContainerProperty) == true)
            {
                return container;
            }

            container = UIKitItemsControlHelper.CreateItemContainer<SelectItem>(_itemsControl, _item);
            container.DataContext = _item;
            container.SetValue(_isFakeContainerProperty, true);

            EnsureBindingIsAccessible(container);

            return container;
        }

        private void EnsureBindingIsAccessible(SelectItem container)
        {
            if (IsBindingUnattached(container, SelectItem.HeaderProperty) ||
                IsBindingUnattached(container, SelectItem.IconProperty))
            {
                container.UpdateLayout();
                container.Measure(new Size(0, 0));
            }
        }

        private bool IsBindingUnattached(DependencyObject dependencyObject, DependencyProperty property)
        {
            var be = BindingOperations.GetBindingExpressionBase(dependencyObject, property);
            if (be != null)
            {
                return be.Status == BindingStatus.Unattached;
            }

            return false;
        }

        private SelectItem? TryGetEffectiveContainer()
        {
            return (SelectItem)_itemContainerGenerator.ContainerFromItem(_item);
        }

        private bool _isUpdating;
        private WeakReference<SelectItem> _container;

        private readonly ItemContainerGenerator _itemContainerGenerator;
        private readonly ItemsControl _itemsControl;
        private readonly object _item;

        private static readonly DependencyProperty _isFakeContainerProperty = DependencyProperty.RegisterAttached("IsFakeContainer", typeof(bool), typeof(SelectItemInfoProvider));
    }
}