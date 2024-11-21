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
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Services
{
    public class PropertyChangeNotifier<TControl, TProperty> : DependencyObject, IDisposable
        where TControl : DependencyObject
    {
        public PropertyChangeNotifier(TControl propertySource, string path)
            : this(propertySource, new PropertyPath(path))
        {
        }

        public PropertyChangeNotifier(TControl propertySource, DependencyProperty property)
            : this(propertySource, property.AsPath())
        {
        }

        public PropertyChangeNotifier(TControl propertySource, PropertyPath property)
        {
            Guard.ArgumentIsNotNull(propertySource);
            Guard.ArgumentIsNotNull(property);

            _propertySource = new WeakReference<TControl>(propertySource);

            var binding = new Binding
            {
                Path = property,
                Mode = BindingMode.OneWay,
                Source = propertySource
            };

            BindingOperations.SetBinding(this, ValueProperty, binding);
        }

        public event Action<TControl, TProperty?, TProperty?> ValueChanged = (source, oldValue, newValue) => { };

        public void Dispose()
           => BindingOperations.ClearBinding(this, ValueProperty);

        public TControl? PropertySource => _propertySource.TryGetTarget(out var target)
            ? target
            : null;

        #region Value

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(TProperty),
            typeof(PropertyChangeNotifier<TControl, TProperty>),
            new FrameworkPropertyMetadata(OnValueChanged));

        public TProperty? Value
        {
            get => (TProperty?)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var notifier = Guard.EnsureArgumentIsInstanceOfType<PropertyChangeNotifier<TControl, TProperty>>(d);

            if (notifier.PropertySource is TControl source)
            {
                notifier.ValueChanged.Invoke(source, (TProperty?)e.OldValue, (TProperty?)e.NewValue);
            }
        }

        #endregion

        private readonly WeakReference<TControl> _propertySource;
    }
}
