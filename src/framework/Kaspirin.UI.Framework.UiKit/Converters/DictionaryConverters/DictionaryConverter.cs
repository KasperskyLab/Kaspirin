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
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Converters.DictionaryConverters
{
    [ContentProperty(nameof(Resources))]
    public sealed class DictionaryConverter : IValueConverter
    {
        public static string Default = "_defaultValue";

        public ResourceDictionary Resources { get; set; } = new ResourceDictionary();

        public DictionaryConverter? BasedOn { get; set; } = null;

        public bool ShouldTraceOnNotFound { get; set; } = true;

        public DictionaryConverterValueMode ProvideValueMode { get; set; } = DictionaryConverterValueMode.Default;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                _trace.TraceWarning($"{nameof(DictionaryConverter)}: value is invalid");
                return DependencyProperty.UnsetValue;
            }

            var resourceKey = GetResourceKey(value);
            var resourceObject = GetResourceObject(resourceKey);
            if (resourceObject == null && BasedOn != null)
            {
                resourceObject = BasedOn.GetResourceObject(resourceKey);
            }

            if (resourceObject == null && ShouldTraceOnNotFound)
            {
                _trace.TraceError($"{nameof(DictionaryConverter)}: ResourceKey '{resourceKey}' (type '{resourceKey.GetType().FullName}') is not found in the dictionary: {string.Join(", ", Resources.Keys.Cast<object>())}");
                return DependencyProperty.UnsetValue;
            }

            return resourceObject switch
            {
                IDictionaryConverterItem obj => obj.GetItemValue(),
                _ => resourceObject
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (DictionaryEntry entry in Resources)
            {
                var resValue = (entry.Value as IDictionaryConverterItem)?.GetItemValue();

                if (object.ReferenceEquals(resValue, value) || value.Equals(value))
                {
                    return entry.Key;
                }
            }

            _trace.TraceWarning($"{nameof(DictionaryConverter)}: ConvertBack has not find key for value '{value}'.");
            return DependencyProperty.UnsetValue;
        }

        private object GetResourceKey(object value)
        {
            return ProvideValueMode switch
            {
                DictionaryConverterValueMode.Type => value.GetType(),
                _ => value
            };
        }

        private object? GetResourceObject(object resourceKey)
        {
            if (resourceKey == null)
            {
                return null;
            }

            if (Resources.Contains(resourceKey))
            {
                return Resources[resourceKey];
            }

            var resourceKeyString = resourceKey.ToString();
            if (Resources.Contains(resourceKeyString))
            {
                return Resources[resourceKeyString];
            }

            if (Resources.Contains(Default))
            {
                return Resources[Default];
            }

            return null;
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Converters);
    }
}
