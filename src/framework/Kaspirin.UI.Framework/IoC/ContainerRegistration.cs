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

namespace Kaspirin.UI.Framework.IoC
{
    /// <summary>
    ///     A class that provides information about type registration in an IoC container.
    /// </summary>
    public sealed class ContainerRegistration
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerRegistration" /> class.
        /// </summary>
        /// <param name="registeredType">
        ///     The type being registered.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        public ContainerRegistration(Type registeredType, string name)
        {
            RegisteredType = Guard.EnsureArgumentIsNotNull(registeredType);
            Name = Guard.EnsureArgumentIsNotNull(name);
        }

        /// <summary>
        ///     The registered type.
        /// </summary>
        public Type RegisteredType { get; }

        /// <summary>
        ///     The name of the registration.
        /// </summary>
        public string Name { get; }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString() => $"RegisteredType={RegisteredType.Name} ({RegisteredType.Namespace}) Name={Name}";
    }
}
