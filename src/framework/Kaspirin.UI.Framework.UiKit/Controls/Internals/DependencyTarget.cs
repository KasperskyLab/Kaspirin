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
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal readonly struct DependencyTarget : IServiceProvider, IProvideValueTarget
    {
        public DependencyTarget(DependencyObject targetObject, DependencyProperty targetProperty)
        {
            Guard.ArgumentIsNotNull(targetObject);
            Guard.ArgumentIsNotNull(targetProperty);

            _targetObject = new(targetObject);
            _targetProperty = new(targetProperty);
        }

        public object? GetService(Type serviceType)
        {
            if (serviceType == typeof(IProvideValueTarget))
            {
                return this;
            }

            return null;
        }

        object? IProvideValueTarget.TargetObject { get { return _targetObject.Target; } }
        object? IProvideValueTarget.TargetProperty { get { return _targetProperty.Target; } }

        private readonly WeakReference _targetObject;
        private readonly WeakReference _targetProperty;
    }
}
