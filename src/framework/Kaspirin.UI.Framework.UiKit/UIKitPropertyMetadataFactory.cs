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

namespace Kaspirin.UI.Framework.UiKit
{
    internal static class UIKitPropertyMetadataFactory
    {
        public static PropertyMetadata CreatePropsMetadata(
            Type expectedSenderType,
            string propertyName,
            PropertyChangedCallback? onValueChanged = null,
            object? defaultValue = null)
        {
            void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (expectedSenderType.IsAssignableFrom(d.GetType()) is false)
                {
                    throw new InvalidOperationException($"Unexpected target object type for '{propertyName}'. Expected '{expectedSenderType}', actual '{d.GetType()}'");
                }

                onValueChanged?.Invoke(d, e);
            }

            return defaultValue != null
                ? new PropertyMetadata(defaultValue, OnValueChangedCallback)
                : new PropertyMetadata(OnValueChangedCallback);
        }
    }
}
