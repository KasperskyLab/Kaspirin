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
using System.ComponentModel;
using System.Reflection;

namespace Kaspirin.UI.Framework.Mvvm.Internals
{
    internal sealed class PropertyObserverEventListener
    {
        public PropertyObserverEventListener(INotifyPropertyChanged target, PropertyInfo propertyInfo, Action action)
        {
            _target = Guard.EnsureArgumentIsNotNull(target);
            _propertyInfo = Guard.EnsureArgumentIsNotNull(propertyInfo);
            _action = Guard.EnsureArgumentIsNotNull(action);

            _target.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _propertyInfo.Name || e.PropertyName == null)
            {
                _action.Invoke();
            }
        }

        private readonly INotifyPropertyChanged _target;
        private readonly PropertyInfo _propertyInfo;
        private readonly Action _action;
    }
}
