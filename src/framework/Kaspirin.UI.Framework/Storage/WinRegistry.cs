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

#pragma warning disable CA1416 // This call site is reachable on all platforms

using Microsoft.Win32;

namespace Kaspirin.UI.Framework.Storage
{
    /// <summary>
    ///     Performs read and write operations in the Windows registry.
    /// </summary>
    public sealed class WinRegistry : IRegistry
    {
        /// <inheritdoc cref="IRegistry.GetValue"/>
        public object? GetValue(string keyName, string valueName, object? defaultValue)
        {
            return Registry.GetValue(keyName, valueName, defaultValue);
        }

        /// <inheritdoc cref="IRegistry.SetValue"/>
        public void SetValue(string keyName, string valueName, object value)
        {
            Registry.SetValue(keyName, valueName, value);
        }

        /// <inheritdoc cref="IRegistry.CreateKey"/>
        public void CreateKey(RegistryHive rootHive, string relativeKey, RegistryView registryView)
        {
            using var rootKey = RegistryKey.OpenBaseKey(rootHive, registryView);
            using var subKey = rootKey.CreateSubKey(relativeKey);
        }
    }
}
