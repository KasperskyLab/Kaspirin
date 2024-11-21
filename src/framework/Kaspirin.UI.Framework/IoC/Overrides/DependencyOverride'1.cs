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

namespace Kaspirin.UI.Framework.IoC.Overrides
{
    /// <summary>
    ///     Provides the ability to override the dependency value for the type registered in the IoC container.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of dependency to be redefined.
    /// </typeparam>
    /// <remarks>
    ///     The choice of a dependency in the constructor of the instance being created is based on the type of argument.
    /// </remarks>
    public sealed class DependencyOverride<T> : DependencyOverride
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyOverride{T}" /> class.
        /// </summary>
        /// <param name="dependencyValue">
        ///     The meaning of dependence.
        /// </param>
        public DependencyOverride(object dependencyValue)
            : base(typeof(T), dependencyValue)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyOverride{T}" /> class.
        /// </summary>
        /// <param name="dependencyFactory">
        ///     A factory for creating a dependency value.
        /// </param>
        public DependencyOverride(Func<object> dependencyFactory)
            : base(typeof(T), dependencyFactory)
        {
        }
    }
}
