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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kaspirin.UI.Framework.Mvvm
{
    /// <summary>
    ///     Provides a basic implementation for the ViewModel and implements the <see cref="INotifyPropertyChanged" /> interface.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        ///     The event that is triggered when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///     Generates an event about a change in a specific property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property.
        /// </param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = default)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        ///     Generates an event about changing all properties.
        /// </summary>
        protected void RaiseAllPropertyChanged()
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));

        #endregion

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
            RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
