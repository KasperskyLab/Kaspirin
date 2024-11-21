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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kaspirin.UI.Framework.IoC.Overrides
{
    /// <summary>
    ///     Provides the ability to redefine the value of several dependencies for a type registered in the IoC container.
    /// </summary>
    /// <remarks>
    ///     The dependency is selected in the constructor of the created instance by the name of the argument.
    /// </remarks>
    public sealed class ParameterOverrides : ResolverOverride, IEnumerable
    {
        /// <summary>
        ///     Adds an overridden dependency.
        /// </summary>
        /// <param name="parameterName">
        ///     The type of addiction.
        /// </param>
        /// <param name="parameterValue">
        ///     The meaning of dependence.
        /// </param>
        public void Add(string parameterName, object parameterValue)
        {
            Guard.ArgumentIsNotNull(parameterName);

            _overrides.Add(parameterName, parameterValue);
        }

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

            if (!_overrides.ContainsKey(dependencyName))
            {
                value = null;
                return false;
            }

            value = _overrides[dependencyName];
            return true;
        }

        /// <summary>
        ///     Returns <see cref="IEnumerator" /> to list overridden dependencies.
        /// </summary>
        /// <returns>
        ///     
        /// <see cref="IEnumerator" /> redefined dependencies.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return _overrides.GetEnumerator();
        }

        private readonly Dictionary<string, object> _overrides = new();
    }
}
