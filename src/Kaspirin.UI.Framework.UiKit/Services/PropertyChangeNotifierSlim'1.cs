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
using System.ComponentModel;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Services;

/// <summary>
///     Monitors the change in the dependency property for the <typeparamref name="TControl" /> control.
/// </summary>
/// <remarks>
///     A lighter version <see cref="PropertyChangeNotifier{TControl,TProperty}" />. Uses <see cref="DependencyPropertyDescriptor" />
///     instead of <see cref="INotifyPropertyChanged" />. To prevent memory leaks, you must call the <see cref="Release" /> method at the end of use.
/// </remarks>
/// <typeparam name="TControl">
///     The type of the control.
/// </typeparam>
/// <typeparam name="TProperty">
///     The type of the monitored property.
/// </typeparam>
public class PropertyChangeNotifierSlim<TControl, TProperty> : IDisposable
    where TControl : FrameworkElement
{
    /// <summary>
    ///     Initializes a new instance of the class <see cref="PropertyChangeNotifierSlim{TControl, TProperty}" />.
    /// </summary>
    /// <param name="propertySource">
    ///     The control whose property is being monitored.
    /// </param>
    /// <param name="property">
    ///     The dependency property that is being monitored.
    /// </param>
    public PropertyChangeNotifierSlim(TControl propertySource, DependencyProperty property)
    {
        _propertySource = propertySource;
        _property = property;
        _propertyDescriptor = DependencyPropertyDescriptor.FromProperty(property, typeof(TControl));

        Value = _propertySource.GetValue<TProperty?>(_property);

        _propertySource.Loaded += OnSourceLoaded;
        _propertySource.Unloaded += OnSourceUnloaded;
    }

    /// <summary>
    ///     Event for changing the property value.
    /// </summary>
    public event Action<TControl, TProperty?, TProperty?> ValueChanged = (source, oldValue, newValue) => { };

    /// <summary>
    ///     The current value of the monitored property.
    /// </summary>
    public TProperty? Value
    {
        get => _value;
        private set
        {
            if (_value?.Equals(value) == true)
            {
                return;
            }

            var oldValue = _value;
            var newValue = value;

            _value = newValue;

            ValueChanged.Invoke(_propertySource, oldValue, newValue);
        }
    }

    /// <summary>
    ///     Frees up the resources used to track the property.
    /// </summary>
    /// <remarks>
    ///     This method must always be called when working with this object is completed.
    /// </remarks>
    public void Release()
    {
        _propertySource.Loaded -= OnSourceLoaded;
        _propertySource.Unloaded -= OnSourceUnloaded;
        _propertyDescriptor.RemoveValueChanged(_propertySource, OnPropertyChanged);

        _value = default;
    }

    void IDisposable.Dispose()
    {
        Release();
    }

    private void OnSourceLoaded(object sender, RoutedEventArgs e)
    {
        _propertyDescriptor.AddValueChanged(sender, OnPropertyChanged);
    }

    private void OnSourceUnloaded(object sender, RoutedEventArgs e)
    {
        _propertyDescriptor.RemoveValueChanged(sender, OnPropertyChanged);
    }

    private void OnPropertyChanged(object? sender, EventArgs e)
    {
        Value = _propertySource.GetValue<TProperty?>(_property);
    }

    private readonly DependencyPropertyDescriptor _propertyDescriptor;
    private readonly DependencyProperty _property;
    private readonly TControl _propertySource;

    private TProperty? _value;
}
