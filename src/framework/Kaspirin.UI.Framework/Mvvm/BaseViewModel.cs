// Copyright © 2025 AO Kaspersky Lab.
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Provides a basic implementation for the ViewModel and implements the <see cref="INotifyPropertyChanged" /> interface.
/// </summary>
public abstract class BaseViewModel : BaseAppObject, INotifyPropertyChanged
{
    /// <inheritdoc cref="BaseViewModel(string)"/>
    protected BaseViewModel()
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="BaseViewModel" /> class.
    /// </summary>
    /// <param name="traceComponent">
    ///     The name of the component for the message tracer.
    /// </param>
    protected BaseViewModel(string traceComponent)
        : base(traceComponent)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="BaseViewModel" /> class.
    /// </summary>
    /// <param name="tracer">
    ///     The message tracer.
    /// </param>
    protected BaseViewModel(ComponentTracer tracer)
        : base(tracer)
    {
    }

    /// <summary>
    ///     The event that is triggered when the property is changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Called when the <paramref name="propertyName" /> property is changed.
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property.
    /// </param>
    protected virtual void OnPropertyChanged(string? propertyName)
    {
    }

    /// <summary>
    ///     Generates events about property changes <paramref name="propertyNames" />.
    /// </summary>
    /// <param name="propertyNames">
    ///     The names of the properties.
    /// </param>
    protected void RaisePropertyChanged(params string[] propertyNames)
    {
        Guard.ArgumentCollectionIsNotNullOrEmpty(propertyNames);

        foreach (var propertyName in propertyNames)
        {
            Guard.IsNotNullOrEmpty(propertyName);

            RaisePropertyChangedCore(propertyName);
        }
    }

    /// <summary>
    ///     Generates an event about changing all properties.
    /// </summary>
    protected void RaiseAllPropertiesChanged()
        => RaisePropertyChangedCore(string.Empty);

    /// <summary>
    ///     Sets the value of the property and generates a property change event if the value has changed.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the property.
    /// </typeparam>
    /// <param name="storage">
    ///     A reference to the property.
    /// </param>
    /// <param name="value">
    ///     The new value of the property.
    /// </param>
    /// <param name="propertyName">
    ///     The name of the property.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the property value has been changed, otherwise <see langword="false" />.
    /// </returns>
    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = default)
        => SetProperty(ref storage, value, onChanged: default, propertyName);

    /// <summary>
    ///     Sets the value of the property and generates a property change event if the value has changed.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the property.
    /// </typeparam>
    /// <param name="storage">
    ///     A reference to the property.
    /// </param>
    /// <param name="value">
    ///     The new value of the property.
    /// </param>
    /// <param name="onChanged">
    ///     The action that will be performed after the property is changed.
    /// </param>
    /// <param name="propertyName">
    ///     The name of the property.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the property value has been changed, otherwise <see langword="false" />.
    /// </returns>
    protected virtual bool SetProperty<T>(ref T storage, T value, Action? onChanged, [CallerMemberName] string? propertyName = default)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        onChanged?.Invoke();

        RaisePropertyChangedCore(propertyName);

        return true;
    }

    /// <inheritdoc cref="Executers.InUiAsync(Action, DispatcherPriority)"/>
    protected Task InUiAsync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        => Executers.InUiAsync(action, priority);

    private void RaisePropertyChangedCore(string? propertyName)
    {
        var executer = Executers.Implementation;
        if (executer.CanExecuteInUiThread && executer.IsUiThread is false)
        {
            executer.ExecuteInUiThreadAsync(() => RaiseEvent(this, propertyName));
        }
        else
        {
            RaiseEvent(this, propertyName);
        }

        static void RaiseEvent(BaseViewModel sender, string? propertyName)
        {
            var propertyChangedArgs = new PropertyChangedEventArgs(propertyName);

            sender.PropertyChanged?.Invoke(sender, propertyChangedArgs);
            sender.OnPropertyChanged(propertyName);
        }
    }
}
