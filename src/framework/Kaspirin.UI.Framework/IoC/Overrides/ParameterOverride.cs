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
    ///     Provides the ability to override the dependency value for the type registered in the IoC container.
    /// </summary>
    /// <remarks>
    ///     The dependency is selected in the constructor of the created instance by the name of the argument.
    /// </remarks>
    public sealed class ParameterOverride : ResolverOverride
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterOverride" /> class.
        /// </summary>
        /// <param name="parameterName">
        ///     The name of the dependency.
        /// </param>
        /// <param name="parameterValue">
        ///     The meaning of dependence.
        /// </param>
        public ParameterOverride(string parameterName, object parameterValue)
        {
            ParameterName = Guard.EnsureArgumentIsNotNull(parameterName);
            ParameterValue = parameterValue;
        }

        /// <summary>
        ///     The name of the dependency.
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        ///     The meaning of dependence.
        /// </summary>
        public object ParameterValue { get; }

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

            if (dependencyName != ParameterName)
            {
                value = null;
                return false;
            }

            value = ParameterValue;
            return true;
        }
    }
}
