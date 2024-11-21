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
using System.Collections.Generic;
using System.Reflection;

namespace Kaspirin.UI.Framework.IoC
{
    /// <summary>
    ///     Interface for interacting with the IoC container.
    /// </summary>
    public interface IUnityContainer : IDisposable
    {
        /// <summary>
        ///     Registers an instance of the type <typeparamref name="T" /> in the container.
        /// </summary>
        /// <remarks>
        ///     The instance is registered as a singleton.
        /// </remarks>
        /// <typeparam name="T">
        ///     The type of instance.
        /// </typeparam>
        /// <param name="instance">
        ///     The instance that will be registered.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterInstance<T>(T instance) where T : notnull;

        /// <summary>
        ///     Registers an instance of the type <typeparamref name="T" /> in the container.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of instance.
        /// </typeparam>
        /// <param name="instance">
        ///     The instance that will be registered.
        /// </param>
        /// <param name="lifetimeManager">
        ///     Instance Lifecycle manager.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterInstance<T>(T instance, LifetimeManager lifetimeManager) where T : notnull;

        /// <summary>
        ///     Registers an instance of the <paramref name="type" /> type in the container.
        /// </summary>
        /// <remarks>
        ///     The instance is registered as a singleton.
        /// </remarks>
        /// <param name="type">
        ///     The type of instance.
        /// </param>
        /// <param name="instance">
        ///     The instance that will be registered.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterInstance(Type type, object instance);

        /// <summary>
        ///     Registers an instance of the <paramref name="type" /> type in the container.
        /// </summary>
        /// <remarks>
        ///     The instance is registered as a singleton.
        /// </remarks>
        /// <param name="type">
        ///     The type of instance.
        /// </param>
        /// <param name="instance">
        ///     The instance that will be registered.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterInstance(Type type, object instance, string name);

        /// <summary>
        ///     Registers an instance of the <paramref name="type" /> type in the container.
        /// </summary>
        /// <param name="type">
        ///     The type of instance.
        /// </param>
        /// <param name="instance">
        ///     The instance that will be registered.
        /// </param>
        /// <param name="lifetimeManager">
        ///     Instance Lifecycle manager.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterInstance(Type type, object instance, LifetimeManager lifetimeManager);

        /// <summary>
        ///     Registers an instance of the <paramref name="type" /> type in the container.
        /// </summary>
        /// <param name="type">
        ///     The type of instance.
        /// </param>
        /// <param name="instance">
        ///     The instance that will be registered.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <param name="lifetimeManager">
        ///     Instance Lifecycle manager.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterInstance(Type type, object instance, string name, LifetimeManager lifetimeManager);

        /// <summary>
        ///     Registers the type <typeparamref name="T" /> in the container.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <typeparamref name="T" />, a new instance will always be created from the container.
        /// </remarks>
        /// <typeparam name="T">
        ///     The type being registered.
        /// </typeparam>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<T>();

        /// <summary>
        ///     Registers the type <typeparamref name="T" /> in the container.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <typeparamref name="T" />, a new instance will always be created from the container.
        /// </remarks>
        /// <typeparam name="T">
        ///     The type being registered.
        /// </typeparam>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<T>(params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the type <typeparamref name="T" /> in the container.
        /// </summary>
        /// <typeparam name="T">
        ///     The type being registered.
        /// </typeparam>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager);

        /// <summary>
        ///     Registers the type <typeparamref name="T" /> in the container.
        /// </summary>
        /// <typeparam name="T">
        ///     The type being registered.
        /// </typeparam>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the type <typeparamref name="T" /> in the container.
        /// </summary>
        /// <typeparam name="T">
        ///     The type being registered.
        /// </typeparam>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the <paramref name="type" /> type in the container.
        /// </summary>
        /// <remarks>
        ///     When requesting the <paramref name="type" /> type, a new instance will always be created from the container.
        /// </remarks>
        /// <param name="type">
        ///     The type being registered.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType(Type type);

        /// <summary>
        ///     Registers the <paramref name="type" /> type in the container.
        /// </summary>
        /// <remarks>
        ///     When requesting the <paramref name="type" /> type, a new instance will always be created from the container.
        /// </remarks>
        /// <param name="type">
        ///     The type being registered.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType(Type type, params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the <paramref name="type" /> type in the container.
        /// </summary>
        /// <param name="type">
        ///     The type being registered.
        /// </param>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType(Type type, LifetimeManager lifetimeManager);

        /// <summary>
        ///     Registers the <paramref name="type" /> type in the container.
        /// </summary>
        /// <param name="type">
        ///     The type being registered.
        /// </param>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType(Type type, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the <paramref name="type" /> type in the container.
        /// </summary>
        /// <param name="type">
        ///     The type being registered.
        /// </param>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType(Type type, LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the type <typeparamref name="TTo" /> in the container as <typeparamref name="TFrom" />.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <typeparamref name="TFrom" />, a new instance of the type <typeparamref name="TTo" />
        ///     is provided from the container, reduced to <typeparamref name="TFrom" />.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The target type being registered.
        /// </typeparam>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        ///     Registers the type <typeparamref name="TTo" /> in the container as <typeparamref name="TFrom" />.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <typeparamref name="TFrom" />, a new instance of the type <typeparamref name="TTo" />
        ///     is provided from the container, reduced to <typeparamref name="TFrom" />.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The target type being registered.
        /// </typeparam>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<TFrom, TTo>(params InjectionMember[] injectionMembers) where TTo : TFrom;

        /// <summary>
        ///     Registers the type <typeparamref name="TTo" /> in the container as <typeparamref name="TFrom" />.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <typeparamref name="TFrom" />, an instance of the type <typeparamref name="TTo" />
        ///     is provided from the container, resulting in <typeparamref name="TFrom" />.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The target type being registered.
        /// </typeparam>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager) where TTo : TFrom;

        /// <summary>
        ///     Registers the type <typeparamref name="TTo" /> in the container as <typeparamref name="TFrom" />.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <typeparamref name="TFrom" />, an instance of the type <typeparamref name="TTo" />
        ///     is provided from the container, resulting in <typeparamref name="TFrom" />.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The target type being registered.
        /// </typeparam>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) where TTo : TFrom;

        /// <summary>
        ///     Registers the type <typeparamref name="TTo" /> in the container as <typeparamref name="TFrom" />.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <typeparamref name="TFrom" />, an instance of the type <typeparamref name="TTo" />
        ///     is provided from the container, resulting in <typeparamref name="TFrom" />.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The target type being registered.
        /// </typeparam>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers) where TTo : TFrom;

        /// <summary>
        ///     Registers the type <paramref name="typeTo" /> in the container as <paramref name="typeFrom" />.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <paramref name="typeFrom" />, an instance of the type <paramref name="typeTo" />
        ///     is provided from the container, reduced to <paramref name="typeFrom" />.
        /// </remarks>
        /// <param name="typeFrom">
        ///     The registered source type.
        /// </param>
        /// <param name="typeTo">
        ///     The target type being registered.
        /// </param>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType(Type typeFrom, Type typeTo, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the type <paramref name="typeTo" /> in the container as <paramref name="typeFrom" />.
        /// </summary>
        /// <remarks>
        ///     When requesting the type <paramref name="typeFrom" />, an instance of the type <paramref name="typeTo" />
        ///     is provided from the container, reduced to <paramref name="typeFrom" />.
        /// </remarks>
        /// <param name="typeFrom">
        ///     The registered source type.
        /// </param>
        /// <param name="typeTo">
        ///     The target type being registered.
        /// </param>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <param name="injectionMembers">
        ///     The parameters used when building the instance.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer RegisterType(Type typeFrom, Type typeTo, LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers);

        /// <summary>
        ///     Registers the type <typeparamref name="TFrom" /> in the container for which the type <typeparamref name="TTo" /> is provided.
        /// </summary>
        /// <remarks>
        ///     The type <typeparamref name="TTo" /> must be registered before calling this method.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The target type.
        /// </typeparam>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer MapInterface<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        ///     Registers the type <typeparamref name="TFrom" /> in the container for which the type <typeparamref name="TTo" /> is provided.
        /// </summary>
        /// <remarks>
        ///     The type <typeparamref name="TTo" /> must be registered before calling this method.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The target type.
        /// </typeparam>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer MapInterface<TFrom, TTo>(string name) where TTo : TFrom;

        /// <summary>
        ///     Registers the type <paramref name="typeFrom" /> in the container for which the type <paramref name="typeTo" /> is provided.
        /// </summary>
        /// <remarks>
        ///     The type <paramref name="typeTo" /> must be registered before calling this method.
        /// </remarks>
        /// <param name="typeFrom">
        ///     The registered source type.
        /// </param>
        /// <param name="typeTo">
        ///     The target type.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer MapInterface(Type typeFrom, Type typeTo);

        /// <summary>
        ///     Registers the type <paramref name="typeFrom" /> in the container for which the type <paramref name="typeTo" /> is provided.
        /// </summary>
        /// <remarks>
        ///     The type <paramref name="typeTo" /> must be registered before calling this method.
        /// </remarks>
        /// <param name="typeFrom">
        ///     The registered source type.
        /// </param>
        /// <param name="typeTo">
        ///     The target type.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer MapInterface(Type typeFrom, Type typeTo, string name);

        /// <summary>
        ///     Replaces the target type for the type <typeparamref name="TFrom" /> with a new type <typeparamref name="TTo" />.
        /// </summary>
        /// <remarks>
        ///     If the type <typeparamref name="TTo" /> and <typeparamref name="TFrom" /> were not previously
        ///     registered in the container, then this method works as <see cref="RegisterType{TFrom, TTo}()" />.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The new target type being registered.
        /// </typeparam>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer OverrideType<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        ///     Replaces the target type for the type <typeparamref name="TFrom" /> with a new type <typeparamref name="TTo" />.
        /// </summary>
        /// <remarks>
        ///     If the type <typeparamref name="TTo" /> and <typeparamref name="TFrom" /> have not previously
        ///     been registered in the container, then this method works as <see cref="RegisterType{TFrom, TTo}(LifetimeManager)" />.
        /// </remarks>
        /// <typeparam name="TFrom">
        ///     The registered source type.
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     The new target type being registered.
        /// </typeparam>
        /// <param name="lifetimeManager">
        ///     The lifecycle manager of the instance being created.
        /// </param>
        /// <returns>
        ///     The current instance is <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer OverrideType<TFrom, TTo>(LifetimeManager lifetimeManager) where TTo : TFrom;

        /// <summary>
        ///     Checks whether the <paramref name="type" /> type is registered in the container.
        /// </summary>
        /// <param name="type">
        ///     The type being checked.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the type is registered, otherwise <see langword="false" />.
        /// </returns>
        bool IsTypeRegistered(Type type);

        /// <summary>
        ///     Checks whether the type <paramref name="type" /> is registered in the container with the name <paramref name="name" />
        ///     specified during registration.
        /// </summary>
        /// <param name="type">
        ///     The type being checked.
        /// </param>
        /// <param name="name">
        ///     The name of the registration being checked.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the type is registered, otherwise <see langword="false" />.
        /// </returns>
        bool IsTypeRegistered(Type type, string name);

        /// <summary>
        ///     Provides an instance of the type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type provided.
        /// </typeparam>
        /// <returns>
        ///     An instance of the type <typeparamref name="T" />.
        /// </returns>
        T Resolve<T>();

        /// <summary>
        ///     Provides an instance of the type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type provided.
        /// </typeparam>
        /// <param name="resolverOverrides">
        ///     Parameters that override the type dependencies used when building the instance.
        /// </param>
        /// <returns>
        ///     An instance of the type <typeparamref name="T" />.
        /// </returns>
        T Resolve<T>(params ResolverOverride[] resolverOverrides);

        /// <summary>
        ///     Provides an instance of the type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type provided.
        /// </typeparam>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <param name="resolverOverrides">
        ///     Parameters that override the type dependencies used when building the instance.
        /// </param>
        /// <returns>
        ///     An instance of the type <typeparamref name="T" />.
        /// </returns>
        T Resolve<T>(string name, params ResolverOverride[] resolverOverrides);

        /// <summary>
        ///     Provides an instance of the <paramref name="type" /> type.
        /// </summary>
        /// <param name="type">
        ///     The type provided.
        /// </param>
        /// <returns>
        ///     An instance of the <paramref name="type" /> type.
        /// </returns>
        object Resolve(Type type);

        /// <summary>
        ///     Provides an instance of the <paramref name="type" /> type.
        /// </summary>
        /// <param name="type">
        ///     The type provided.
        /// </param>
        /// <param name="resolverOverrides">
        ///     Parameters that override the type dependencies used when building the instance.
        /// </param>
        /// <returns>
        ///     An instance of the <paramref name="type" /> type.
        /// </returns>
        object Resolve(Type type, params ResolverOverride[] resolverOverrides);

        /// <summary>
        ///     Provides an instance of the <paramref name="type" /> type.
        /// </summary>
        /// <param name="type">
        ///     The type provided.
        /// </param>
        /// <param name="name">
        ///     The name of the registration.
        /// </param>
        /// <param name="resolverOverrides">
        ///     Parameters that override the type dependencies used when building the instance.
        /// </param>
        /// <returns>
        ///     An instance of the <paramref name="type" /> type.
        /// </returns>
        object Resolve(Type type, string name, params ResolverOverride[] resolverOverrides);

        /// <summary>
        ///     Searches for and pre-caches types in the specified assembly.
        /// </summary>
        /// <param name="assembly">
        ///     The assembly being analyzed.
        /// </param>
        void DiscoveryTypesInAssembly(Assembly assembly);

        /// <summary>
        ///     Returns a list of type registrations.
        /// </summary>
        IEnumerable<ContainerRegistration> Registrations { get; }
    }
}
