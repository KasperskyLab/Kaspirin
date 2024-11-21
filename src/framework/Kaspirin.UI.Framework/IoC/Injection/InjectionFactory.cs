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

namespace Kaspirin.UI.Framework.IoC.Injection
{
    /// <summary>
    ///     Allows you to completely override the creation of an object when it is requested from an IoC container.
    /// </summary>
    public sealed class InjectionFactory : InjectionMember
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InjectionFactory" /> class.
        /// </summary>
        /// <param name="factoryWithOverrides">
        ///     A factory for creating an object.
        /// </param>
        public InjectionFactory(Func<IUnityContainer, object> factoryWithOverrides)
        {
            _factoryWithOverrides = Guard.EnsureArgumentIsNotNull(factoryWithOverrides);

            Factory = ResolveFromFactoryWithOverrides;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InjectionFactory" /> class.
        /// </summary>
        /// <param name="factory">
        ///     A factory for creating an object with dependency overrides.
        /// </param>
        public InjectionFactory(Func<IUnityContainer, ResolverOverride[], object> factory)
        {
            Factory = Guard.EnsureArgumentIsNotNull(factory);
        }

        /// <summary>
        ///     The factory delegate for creating the object.
        /// </summary>
        public Func<IUnityContainer, ResolverOverride[], object> Factory { get; }

        private object ResolveFromFactoryWithOverrides(IUnityContainer container, ResolverOverride[] overrides)
        {
            Guard.IsNotNull(_factoryWithOverrides);
            return _factoryWithOverrides(container);
        }

        private readonly Func<IUnityContainer, object>? _factoryWithOverrides;
    }
}
