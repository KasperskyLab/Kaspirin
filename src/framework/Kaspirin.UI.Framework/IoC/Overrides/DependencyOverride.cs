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

#pragma warning disable KCAIDE0006 // Class should be abstract or sealed

using System;
using System.Diagnostics.CodeAnalysis;

namespace Kaspirin.UI.Framework.IoC.Overrides
{
    /// <summary>
    ///     Provides the ability to override the dependency value for the type registered in the IoC container.
    /// </summary>
    /// <remarks>
    ///     The choice of a dependency in the constructor of the instance being created is based on the type of argument.
    /// </remarks>
    public class DependencyOverride : ResolverOverride
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyOverride" /> class.
        /// </summary>
        /// <param name="dependencyValue">
        ///     The meaning of dependence.
        /// </param>
        public DependencyOverride(object dependencyValue)
            : this(dependencyValue.GetType(), dependencyValue)
        {
            _overrideDependencyBaseTypes = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyOverride" /> class.
        /// </summary>
        /// <param name="typeToConstruct">
        ///     The type of dependency to be redefined.
        /// </param>
        /// <param name="dependencyValue">
        ///     The meaning of dependence.
        /// </param>
        public DependencyOverride(Type typeToConstruct, object dependencyValue)
            : this(typeToConstruct, () => dependencyValue)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyOverride" /> class.
        /// </summary>
        /// <param name="typeToConstruct">
        ///     The type of dependency to be redefined.
        /// </param>
        /// <param name="dependencyFactory">
        ///     A factory for creating a dependency value.
        /// </param>
        public DependencyOverride(Type typeToConstruct, Func<object> dependencyFactory)
        {
            TypeToConstruct = Guard.EnsureArgumentIsNotNull(typeToConstruct);
            _dependencyFactory = Guard.EnsureArgumentIsNotNull(dependencyFactory);
        }

        /// <summary>
        ///     The type of addiction.
        /// </summary>
        public Type TypeToConstruct { get; }

        /// <summary>
        ///     The meaning of dependence.
        /// </summary>
        public object DependencyValue => _dependencyFactory();

        /// <inheritdoc cref="ResolverOverride.TryGetOverride"/>
        public override bool TryGetOverride(Type ownerType, Type dependencyType, string dependencyName, [NotNullWhen(true)] out object? value)
        {
            Guard.ArgumentIsNotNull(ownerType);
            Guard.ArgumentIsNotNull(dependencyType);

            if (OwnerType != null && OwnerType != ownerType)
            {
                value = null;
                return false;
            }

            if (dependencyType != TypeToConstruct && (!_overrideDependencyBaseTypes || !dependencyType.IsAssignableFrom(TypeToConstruct)))
            {
                value = null;
                return false;
            }

            value = DependencyValue;
            return true;
        }

        private readonly Func<object> _dependencyFactory;
        private readonly bool _overrideDependencyBaseTypes;
    }
}
