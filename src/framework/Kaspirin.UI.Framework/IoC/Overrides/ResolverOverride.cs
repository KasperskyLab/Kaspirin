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
using System.Diagnostics.CodeAnalysis;

namespace Kaspirin.UI.Framework.IoC.Overrides
{
    /// <summary>
    ///     A base class for overriding dependency values.
    /// </summary>
    public abstract class ResolverOverride
    {
        /// <summary>
        ///     The type of dependency owner.
        /// </summary>
        public Type? OwnerType { get; private set; }

        /// <summary>
        ///     Specifies the type of dependency owner.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of dependency owner.
        /// </typeparam>
        /// <returns>
        ///     The current instance is <see cref="dependencyOverrides" />.
        /// </returns>
        public ResolverOverride OnType<T>()
        {
            OwnerType = typeof(T);
            return this;
        }

        /// <summary>
        ///     Provides an overridden dependency value, if possible.
        /// </summary>
        /// <param name="ownerType">
        ///     The type of dependency owner.
        /// </param>
        /// <param name="dependencyType">
        ///     The type of addiction.
        /// </param>
        /// <param name="dependencyName">
        ///     The name of the dependency.
        /// </param>
        /// <param name="value">
        ///     An overridden value, if found.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if an override was found, otherwise <see langword="false" />.
        /// </returns>
        public abstract bool TryGetOverride(Type ownerType, Type dependencyType, string dependencyName, [NotNullWhen(true)] out object? value);
    }
}
