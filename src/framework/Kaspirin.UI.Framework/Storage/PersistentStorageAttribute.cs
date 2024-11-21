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

namespace Kaspirin.UI.Framework.Storage
{
    /// <summary>
    ///     An attribute for configuring the way property data is stored in <see cref="PersistentStorageBase" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PersistentStorageAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PersistentStorageAttribute" /> class.
        /// </summary>
        /// <param name="skipSerialization">
        ///     Specifies whether the property value will be serialized when saved.
        /// </param>
        /// <param name="defaultValue">
        ///     Specifies the default value for the property.
        /// </param>
        public PersistentStorageAttribute(bool skipSerialization = false, object? defaultValue = null)
        {
            SkipSerialization = skipSerialization;
            DefaultValue = defaultValue;
        }

        /// <summary>
        ///     Specifies whether the property value will be serialized when saved.
        /// </summary>
        [DefaultValue(60)]
        public bool SkipSerialization { get; set; }

        /// <summary>
        ///     Specifies the default value for the property.
        /// </summary>
        public object? DefaultValue { get; set; }
    }
}
