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

using Microsoft.Win32;

namespace Kaspirin.UI.Framework.Storage
{
    /// <summary>
    ///     Interface for working with the registry.
    /// </summary>
    public interface IRegistry
    {
        /// <summary>
        ///     Retrieves the value from the registry associated with the specified key name and value name.
        /// </summary>
        /// <param name="keyName">
        ///     The name of the registry key.
        /// </param>
        /// <param name="valueName">
        ///     The name of the value.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value that will be returned if the value is not found.
        /// </param>
        /// <returns>
        ///     Returns <see langword="null" /> if the subsection specified in <paramref name="keyName" /> does
        ///     not exist, otherwise the value associated with <paramref name="ValueName" />, or <paramref name="defaultValue" />
        ///     if <paramref name="ValueName" /> not found.
        /// </returns>
        object? GetValue(string keyName, string valueName, object? defaultValue);

        /// <summary>
        ///     Sets the registry value associated with the specified key name and value name.
        /// </summary>
        /// <param name="keyName">
        ///     The name of the registry key.
        /// </param>
        /// <param name="valueName">
        ///     The name of the value.
        /// </param>
        /// <param name="value">
        ///     The value for the installation.
        /// </param>
        void SetValue(string keyName, string valueName, object value);

        /// <summary>
        ///     Creates a new registry key.
        /// </summary>
        /// <param name="rootHive">
        ///     The root folder of the registry.
        /// </param>
        /// <param name="relativeKey">
        ///     The relative path to the registry key.
        /// </param>
        /// <param name="registryView">
        ///     Presentation of the registry.
        /// </param>
        void CreateKey(RegistryHive rootHive, string relativeKey, RegistryView registryView);
    }
}
