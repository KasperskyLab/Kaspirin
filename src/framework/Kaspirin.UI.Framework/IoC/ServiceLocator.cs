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
    ///     Allows you to use the IoC container as a static service locator.
    /// </summary>
    /// <remarks>
    ///     It is intended for use only in places where it is impossible to create dependencies from an
    ///     IoC container in the usual way.
    /// </remarks>
    public sealed class ServiceLocator
    {
        /// <summary>
        ///     The current instance is <see cref="ServiceLocator" />.
        /// </summary>
        public static ServiceLocator Instance => Guard.EnsureIsNotNull(_instance).Value;

        /// <summary>
        ///     Sets <paramref name="container" /> as the current container for this <see cref="ServiceLocator" />.
        /// </summary>
        /// <param name="container">
        ///     The container to be installed.
        /// </param>
        /// <remarks>
        ///     The method cannot be called again.
        /// </remarks>
        public static void SetContainer(IUnityContainer container)
        {
            Guard.ArgumentIsNotNull(container);
            Guard.Assert(!(_instance?.IsValueCreated ?? false), "Can't change container in created service locator");

            _instance = new Lazy<ServiceLocator>(() => new ServiceLocator(container));
        }

        /// <summary>
        ///     Retrieves an instance of the specified service from the IoC container.
        /// </summary>
        /// <typeparam name="TService">
        ///     The type of service requested.
        /// </typeparam>
        /// <returns>
        ///     An instance of the requested service.
        /// </returns>
        public TService GetService<TService>() => _unityContainer.Resolve<TService>();

        private ServiceLocator(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        private static Lazy<ServiceLocator>? _instance;
        private readonly IUnityContainer _unityContainer;
    }
}
