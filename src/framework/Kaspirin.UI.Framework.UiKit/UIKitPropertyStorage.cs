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
using System.Collections.Concurrent;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit
{
    internal static class UIKitPropertyStorage
    {
        public static DependencyProperty GetOrCreate(string propertyName, Type propertyType, object? defaultValue = null)
        {
            Guard.ArgumentIsNotNullOrEmpty(propertyName);
            Guard.ArgumentIsNotNull(propertyType);

            var property = _propertyStorage.GetOrAdd(propertyName, name =>
            {
                defaultValue ??= (propertyType.IsValueType ? Activator.CreateInstance(propertyType) : null);

                return DependencyProperty.RegisterAttached(name, propertyType, typeof(UIKitPropertyStorage), new PropertyMetadata(defaultValue));
            });
            if (property.PropertyType != propertyType)
            {
                throw new InvalidOperationException(
                    $"Property '{propertyName}' already registered with type '{property.PropertyType}'. Expected type '{propertyType}'");
            }

            return property;
        }

        public static DependencyProperty Get(string propertyName)
        {
            Guard.ArgumentIsNotNullOrEmpty(propertyName);

            if (_propertyStorage.TryGetValue(propertyName, out var property))
            {
                return property;
            }

            throw new InvalidOperationException($"Property '{propertyName}' is not registered'");
        }

        private static readonly ConcurrentDictionary<string, DependencyProperty> _propertyStorage = new();
    }
}
