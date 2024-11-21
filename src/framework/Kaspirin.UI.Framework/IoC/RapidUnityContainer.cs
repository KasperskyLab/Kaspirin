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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.IoC
{
    /// <summary>
    ///     Provides the functionality of an IoC container implementing the <see cref="IUnityContainer" /> interface.
    /// </summary>
    public sealed class RapidUnityContainer : IUnityContainer
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RapidUnityContainer" /> class.
        /// </summary>
        public RapidUnityContainer() : this(registerSelf: true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapidUnityContainer" /> class.
        /// </summary>
        /// <param name="registerSelf">
        ///     Registers the current instance in the IoC container.
        /// </param>
        public RapidUnityContainer(bool registerSelf)
        {
            if (registerSelf)
            {
                RegisterInstance<IUnityContainer>(this);
            }
        }

        #region IDisposable

        /// <summary>
        ///     Calls <see cref="IDisposable.Dispose" /> for all registered types in the IoC container.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _initializationPlanner.Dispose();

                    lock (_dependencyBags)
                    {
                        _dependencyBags
                           .Where(o => o.Value is ContainerControlledDependencyBag { HasValue: true } or ContainerControlledInstanceDependencyBag)
                           .Where(o => o.Key.Type.IsClass)
                           .Select(o => o.Value.Resolve())
                           .OfType<IDisposable>()
                           .ForEach(DisposeInstance);
                    }
                }

                _isDisposed = true;
            }
        }

        private void DisposeInstance(IDisposable instance)
        {
            try
            {
                instance.Dispose();
                _tracer.TraceInformation($"Instance of type {instance.GetType()} disposed successfully");
            }
            catch (Exception ex)
            {
                _tracer.TraceError($"Failed to dispose instance of type {instance.GetType()}{Environment.NewLine}{ex}");
            }
        }

        #endregion

        /// <inheritdoc cref="IUnityContainer.RegisterInstance{T}(T)"/>
        public IUnityContainer RegisterInstance<T>(T instance) where T : notnull
            => RegisterInstance(typeof(T), instance, Default.ContainerControlledLifetimeManager);

        /// <inheritdoc cref="IUnityContainer.RegisterInstance{T}(T, LifetimeManager)"/>
        public IUnityContainer RegisterInstance<T>(T instance, LifetimeManager lifetimeManager) where T : notnull
            => RegisterInstance(typeof(T), instance, lifetimeManager);

        /// <inheritdoc cref="IUnityContainer.RegisterInstance(Type, object)"/>
        public IUnityContainer RegisterInstance(Type type, object instance)
            => RegisterInstance(type, instance, Default.ContainerControlledLifetimeManager);

        /// <inheritdoc cref="IUnityContainer.RegisterInstance(Type, object, string)"/>
        public IUnityContainer RegisterInstance(Type type, object instance, string name)
            => RegisterInstance(type, instance, name, Default.ContainerControlledLifetimeManager);

        /// <inheritdoc cref="IUnityContainer.RegisterInstance(Type, object, LifetimeManager)"/>
        public IUnityContainer RegisterInstance(Type type, object instance, LifetimeManager lifetimeManager)
            => RegisterInstance(type, instance, Default.String, lifetimeManager);

        /// <inheritdoc cref="IUnityContainer.RegisterInstance(Type, object, string, LifetimeManager)"/>
        public IUnityContainer RegisterInstance(Type type, object instance, string name, LifetimeManager lifetimeManager)
        {
            Guard.ArgumentIsNotNull(type);
            Guard.ArgumentIsNotNull(instance);
            Guard.ArgumentIsNotNull(name);
            Guard.ArgumentIsNotNull(lifetimeManager);

            return RegisterDependencyBag(new DependencyKey(type, name), CreateInstanceDependencyBag(instance, lifetimeManager));
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType{T}()"/>
        public IUnityContainer RegisterType<T>()
            => RegisterType(typeof(T), Default.TransientLifetimeManager, Default.String, Default.InjectionMemberArray);

        /// <inheritdoc cref="IUnityContainer.RegisterType{T}(InjectionMember[])"/>
        public IUnityContainer RegisterType<T>(params InjectionMember[] injectionMembers)
            => RegisterType(typeof(T), Default.TransientLifetimeManager, Default.String, injectionMembers);

        /// <inheritdoc cref="IUnityContainer.RegisterType{T}(LifetimeManager)"/>
        public IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager)
            => RegisterType(typeof(T), lifetimeManager, Default.String, Default.InjectionMemberArray);

        /// <inheritdoc cref="IUnityContainer.RegisterType{T}(LifetimeManager, InjectionMember[])"/>
        public IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
            => RegisterType(typeof(T), lifetimeManager, Default.String, injectionMembers);

        /// <inheritdoc cref="IUnityContainer.RegisterType{T}(LifetimeManager, string, InjectionMember[])"/>
        public IUnityContainer RegisterType<T>(LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers)
            => RegisterType(typeof(T), lifetimeManager, name, injectionMembers);

        /// <inheritdoc cref="IUnityContainer.RegisterType(Type)"/>
        public IUnityContainer RegisterType(Type type)
            => RegisterType(type, Default.TransientLifetimeManager, Default.String, Default.InjectionMemberArray);

        /// <inheritdoc cref="IUnityContainer.RegisterType(Type, InjectionMember[])"/>
        public IUnityContainer RegisterType(Type type, params InjectionMember[] injectionMembers)
            => RegisterType(type, Default.TransientLifetimeManager, Default.String, injectionMembers);

        /// <inheritdoc cref="IUnityContainer.RegisterType(Type, LifetimeManager)"/>
        public IUnityContainer RegisterType(Type type, LifetimeManager lifetimeManager)
            => RegisterType(type, lifetimeManager, Default.String, Default.InjectionMemberArray);

        /// <inheritdoc cref="IUnityContainer.RegisterType(Type, LifetimeManager, InjectionMember[])"/>
        public IUnityContainer RegisterType(Type type, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
            => RegisterType(type, lifetimeManager, Default.String, injectionMembers);

        /// <inheritdoc cref="IUnityContainer.RegisterType(Type, LifetimeManager, string, InjectionMember[])"/>
        public IUnityContainer RegisterType(Type type, LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers)
        {
            Guard.ArgumentIsNotNull(type);
            Guard.ArgumentIsNotNull(name);
            Guard.ArgumentIsNotNull(lifetimeManager);
            Guard.ArgumentIsNotNull(injectionMembers);

            return RegisterDependencyBag(new DependencyKey(type, name), CreateDependencyBag(type, lifetimeManager, injectionMembers));
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType{TFrom, TTo}()"/>
        public IUnityContainer RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            if (IsOverriddenInterface<TFrom>())
            {
                return this;
            }

            RegisterType(typeof(TTo), Default.TransientLifetimeManager, Default.String, Default.InjectionMemberArray);
            return MapInterface(typeof(TFrom), typeof(TTo), Default.String);
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType{TFrom, TTo}(InjectionMember[])"/>
        public IUnityContainer RegisterType<TFrom, TTo>(params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            if (IsOverriddenInterface<TFrom>())
            {
                return this;
            }

            RegisterType(typeof(TTo), Default.TransientLifetimeManager, Default.String, Default.InjectionMemberArray);
            return MapInterface(typeof(TFrom), typeof(TTo), Default.String);
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType{TFrom, TTo}(LifetimeManager)"/>
        public IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager) where TTo : TFrom
        {
            if (IsOverriddenInterface<TFrom>())
            {
                return this;
            }

            RegisterType(typeof(TTo), lifetimeManager, Default.String, Default.InjectionMemberArray);
            return MapInterface(typeof(TFrom), typeof(TTo), Default.String);
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType{TFrom, TTo}(LifetimeManager, InjectionMember[])"/>
        public IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            if (IsOverriddenInterface<TFrom>())
            {
                return this;
            }

            RegisterType(typeof(TTo), lifetimeManager, Default.String, injectionMembers);
            return MapInterface(typeof(TFrom), typeof(TTo), Default.String);
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType{TFrom, TTo}(LifetimeManager, string, InjectionMember[])"/>
        public IUnityContainer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            if (IsOverriddenInterface<TFrom>())
            {
                return this;
            }

            RegisterType(typeof(TTo), lifetimeManager, name, injectionMembers);
            return MapInterface(typeof(TFrom), typeof(TTo), name);
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType(Type, Type, LifetimeManager, InjectionMember[])"/>
        public IUnityContainer RegisterType(Type typeFrom, Type typeTo, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            RegisterType(typeTo, lifetimeManager, Default.String, injectionMembers);
            return MapInterface(typeFrom, typeTo, Default.String);
        }

        /// <inheritdoc cref="IUnityContainer.RegisterType(Type, Type, LifetimeManager, string, InjectionMember[])"/>
        public IUnityContainer RegisterType(Type typeFrom, Type typeTo, LifetimeManager lifetimeManager, string name, params InjectionMember[] injectionMembers)
        {
            RegisterType(typeTo, lifetimeManager, name, injectionMembers);
            return MapInterface(typeFrom, typeTo, name);
        }

        /// <inheritdoc cref="IUnityContainer.MapInterface{TFrom, TTo}()"/>
        public IUnityContainer MapInterface<TFrom, TTo>() where TTo : TFrom
            => MapInterface(typeof(TFrom), typeof(TTo), Default.String);

        /// <inheritdoc cref="IUnityContainer.MapInterface{TFrom, TTo}(string)"/>
        public IUnityContainer MapInterface<TFrom, TTo>(string name) where TTo : TFrom
            => MapInterface(typeof(TFrom), typeof(TTo), name);

        /// <inheritdoc cref="IUnityContainer.MapInterface(Type, Type)"/>
        public IUnityContainer MapInterface(Type typeFrom, Type typeTo)
            => MapInterface(typeFrom, typeTo, Default.String);

        /// <inheritdoc cref="IUnityContainer.MapInterface(Type, Type, string)"/>
        public IUnityContainer MapInterface(Type typeFrom, Type typeTo, string name)
        {
            Guard.ArgumentIsNotNull(typeFrom);
            Guard.ArgumentIsNotNull(typeTo);
            Guard.ArgumentIsNotNull(name);

            if (typeFrom == typeTo)
            {
                return this;
            }

            if (IsOverriddenInterface(typeFrom))
            {
                return this;
            }

            var dependencyKeyTo = new DependencyKey(typeTo, name);
            Guard.Assert(_dependencyBags.TryGetValue(dependencyKeyTo, out var dependencyBag), $"Target type {typeTo} not registered in container. Parameter name: {nameof(typeTo)}");

            var dependencyKeyFrom = new DependencyKey(typeFrom, name);
            return RegisterDependencyBag(dependencyKeyFrom, dependencyBag);
        }

        /// <inheritdoc cref="IUnityContainer.OverrideType{TFrom, TTo}()"/>
        public IUnityContainer OverrideType<TFrom, TTo>() where TTo : TFrom
            => OverrideType<TFrom, TTo>(Default.TransientLifetimeManager);

        /// <inheritdoc cref="IUnityContainer.OverrideType{TFrom, TTo}(LifetimeManager)"/>
        public IUnityContainer OverrideType<TFrom, TTo>(LifetimeManager lifetimeManager) where TTo : TFrom
        {
            var type = typeof(TFrom);

            if (IsTypeRegistered(type))
            {
                _dependencyBags.TryRemove(new DependencyKey(type, Default.String), out _);
            }

            RegisterType<TFrom, TTo>(lifetimeManager);
            _overrides.Add(type);
            return this;
        }

        /// <inheritdoc cref="IUnityContainer.IsTypeRegistered(Type)"/>
        public bool IsTypeRegistered(Type type)
            => IsTypeRegistered(type, Default.String);

        /// <inheritdoc cref="IUnityContainer.IsTypeRegistered(Type, string)"/>
        public bool IsTypeRegistered(Type type, string name)
        {
            Guard.ArgumentIsNotNull(type);
            Guard.ArgumentIsNotNull(name);

            return _dependencyBags.ContainsKey(new DependencyKey(type, name));
        }

        /// <inheritdoc cref="IUnityContainer.Resolve{T}()"/>
        public T Resolve<T>()
            => (T)Resolve(typeof(T), Default.String, Default.ResolverOverrideArray);

        /// <inheritdoc cref="IUnityContainer.Resolve{T}(ResolverOverride[])"/>
        public T Resolve<T>(params ResolverOverride[] resolverOverrides)
            => (T)Resolve(typeof(T), Default.String, resolverOverrides);

        /// <inheritdoc cref="IUnityContainer.Resolve{T}(string, ResolverOverride[])"/>
        public T Resolve<T>(string name, params ResolverOverride[] resolverOverrides)
            => (T)Resolve(typeof(T), name, resolverOverrides);

        /// <inheritdoc cref="IUnityContainer.Resolve(Type)"/>
        public object Resolve(Type type)
            => Resolve(type, Default.String, Default.ResolverOverrideArray);

        /// <inheritdoc cref="IUnityContainer.Resolve(Type, ResolverOverride[])"/>
        public object Resolve(Type type, params ResolverOverride[] resolverOverrides)
            => Resolve(type, Default.String, resolverOverrides);

        /// <inheritdoc cref="IUnityContainer.Resolve(Type, string, ResolverOverride[])"/>
        public object Resolve(Type type, string name, params ResolverOverride[] resolverOverrides)
        {
            Guard.ArgumentIsNotNull(type);
            Guard.ArgumentIsNotNull(name);
            Guard.ArgumentIsNotNull(resolverOverrides);

            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(RapidUnityContainer));
            }

            try
            {
                var dependencyKey = new DependencyKey(type, name);

                if (!_dependencyBags.TryGetValue(dependencyKey, out var dependencyBag))
                {
                    if (IsFunctionFactory(type))
                    {
                        dependencyBag = MakeFunctionFactoryDependencyBag(type, name);
                        AddDependencyBag(dependencyKey, dependencyBag);
                    }
                    else if (IsLazyFactory(type))
                    {
                        dependencyBag = MakeLazyFactoryDependencyBag(type, name);
                        AddDependencyBag(dependencyKey, dependencyBag);
                    }
                    else if (IsParametrizedFactory(type))
                    {
                        dependencyBag = MakeParametrizedFactoryDependencyBag(type, name);
                        AddDependencyBag(dependencyKey, dependencyBag);
                    }
                    else if (IsGenericType(type, name, out dependencyBag))
                    {
                        dependencyBag = MakeGenericTypeDependencyBag(type, Guard.EnsureIsInstanceOfType<OpenGenericTypeDependencyBag>(dependencyBag));
                        AddDependencyBag(dependencyKey, dependencyBag);
                    }
                    else if (type.IsClass)
                    {
                        dependencyBag = CreateDependencyBag(type, Default.TransientLifetimeManager, Default.InjectionMemberArray);
                        AddDependencyBag(dependencyKey, dependencyBag);
                    }
                    else
                    {
                        throw new ResolutionFailedException($"There is no registered object for type {type} and name \"{name}\"");
                    }
                }

                return dependencyBag.Resolve(resolverOverrides);
            }
            catch (Exception exception)
            {
                throw new ResolutionFailedException($"Resolution of the dependency failed, type = {type}, name = \"{name}\"", exception);
            }
        }

        /// <inheritdoc cref="IUnityContainer.DiscoveryTypesInAssembly"/>
        public void DiscoveryTypesInAssembly(Assembly assembly)
        {
            Guard.ArgumentIsNotNull(assembly);
            _initializationPlanner.Plan(new DiscoveryTypesInAssemblyTask(_reflectionCache, assembly));
        }

        /// <inheritdoc cref="IUnityContainer.Registrations"/>
        public IEnumerable<ContainerRegistration> Registrations => _dependencyBags.Select(o => new ContainerRegistration(o.Key.Type, o.Key.Name));

        private static bool IsFunctionFactory(Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == _funcGenericTypeDefinition;

        private static bool IsLazyFactory(Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == _lazyGenericTypeDefinition;

        private static bool IsParametrizedFactory(Type type)
            => type.IsGenericType && (type.GetGenericTypeDefinition().FullName?.StartsWith("System.Func`") ?? false);

        private bool IsGenericType(Type type, string name, out IDependencyBag? dependencyBag)
        {
            if (type.IsGenericType)
            {
                var typeDefinition = type.GetGenericTypeDefinition();
                var dependencyKey = new DependencyKey(typeDefinition, name);
                return _dependencyBags.TryGetValue(dependencyKey, out dependencyBag);
            }

            dependencyBag = default;
            return false;
        }

        private IDependencyBag MakeFunctionFactoryDependencyBag(Type type, string name)
        {
            var targetType = type.GetGenericArguments()[0];
            var resolver = Activator.CreateInstance(typeof(GenericResolver<>).MakeGenericType(targetType), this, name);
            Guard.IsNotNull(resolver);

            var func = Delegate.CreateDelegate(type, resolver, nameof(GenericResolver<object>.Resolve));

            return CreateInstanceDependencyBag(func, Default.ContainerControlledLifetimeManager);
        }

        private IDependencyBag MakeLazyFactoryDependencyBag(Type type, string name)
        {
            var targetType = type.GetGenericArguments()[0];
            var resolver = Activator.CreateInstance(typeof(GenericResolver<>).MakeGenericType(targetType), this, name);
            Guard.IsNotNull(resolver);

            var factoryType = _funcGenericTypeDefinition.MakeGenericType(targetType);
            var factory = Delegate.CreateDelegate(factoryType, resolver, nameof(GenericResolver<object>.Resolve));

            var factoryDependencyKey = new DependencyKey(factoryType, name);
            if (!_dependencyBags.ContainsKey(factoryDependencyKey))
            {
                var dependencyBag = CreateInstanceDependencyBag(factory, Default.ContainerControlledLifetimeManager);
                AddDependencyBag(factoryDependencyKey, dependencyBag);
            }

            return CreateDependencyBag(type, Default.TransientLifetimeManager, new InjectionConstructor(new ResolvedParameter(factoryType, name)));
        }

        private IDependencyBag MakeParametrizedFactoryDependencyBag(Type factoryType, string name)
        {
            var arguments = factoryType.GetGenericArguments();

            var parametersTypes = arguments.Take(arguments.Length - 1).ToArray();
            var targetType = arguments[arguments.Length - 1];

            var resolverType = typeof(OverridesResolver<>).MakeGenericType(targetType);
            var resolver = Activator.CreateInstance(resolverType, new object[] { this, name, parametersTypes });
            var constant = Expression.Constant(resolver);

            var parameters = arguments.Take(arguments.Length - 1).Select((p, i) => Expression.Parameter(p, "arg_" + i)).ToArray();

            var body = Expression.Call(
                Expression.Constant(resolver),
                Guard.EnsureIsNotNull(resolverType.GetMethod(nameof(OverridesResolver<object>.Resolve))),
                Expression.NewArrayInit(typeof(object), parameters.Select(p => Expression.TypeAs(p, typeof(object))))
            );

            var factory = Expression.Lambda(factoryType, body, parameters).Compile();

            return CreateInstanceDependencyBag(factory, Default.ContainerControlledLifetimeManager);
        }

        private IDependencyBag MakeGenericTypeDependencyBag(Type type, OpenGenericTypeDependencyBag dependencyBag)
        {
            var typeArguments = type.GetGenericArguments();
            var implementationType = dependencyBag.Type.MakeGenericType(typeArguments);

            return CreateDependencyBag(implementationType, dependencyBag.LifetimeManager, dependencyBag.InjectionMembers);
        }

        private void AddDependencyBag(DependencyKey dependencyKey, IDependencyBag dependencyBag)
        {
            if (!_dependencyBags.TryAdd(dependencyKey, dependencyBag))
            {
                if (!_dependencyBags.TryGetValue(dependencyKey, out _))
                {
                    throw new ResolutionFailedException(
                        $"There is no registered object for type {dependencyKey.Type} and name \"{dependencyKey.Name}\"");
                }
            }
        }

        private IDependencyBag CreateInstanceDependencyBag(object instance, LifetimeManager lifetimeManager)
        {
            return lifetimeManager switch
            {
                ContainerControlledLifetimeManager => new ContainerControlledInstanceDependencyBag(instance),
                ExternallyControlledLifetimeManager => new ExternallyControlledInstanceDependencyBag(instance),
                _ => throw new ArgumentException($"Unknown lifetime manager: {lifetimeManager}", nameof(lifetimeManager))
            };
        }

        private IDependencyBag CreateDependencyBag(Type type, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            if (IsOpenGenericType(type))
            {
                return new OpenGenericTypeDependencyBag(type, lifetimeManager, injectionMembers);
            }

            return lifetimeManager switch
            {
                ContainerControlledLifetimeManager => new ContainerControlledDependencyBag(CreateValueFactory(type, injectionMembers)),
                ExternallyControlledLifetimeManager => new ExternallyControlledDependencyBag(CreateValueFactory(type, injectionMembers)),
                TransientLifetimeManager => new TransientDependencyBag(CreateValueFactory(type, injectionMembers)),
                _ => throw new ArgumentException($"Unknown lifetime manager: {lifetimeManager}", nameof(lifetimeManager))
            };
        }

        private bool IsOpenGenericType(Type type) => type.IsGenericTypeDefinition;

        private IUnityContainer RegisterDependencyBag(DependencyKey dependencyKey, IDependencyBag dependencyBag)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(RapidUnityContainer));
            }

            Guard.Assert(_dependencyBags.TryAdd(dependencyKey, dependencyBag),
                $"Dependency with type {dependencyKey.Type} and name \"{dependencyKey.Name}\" is already registered");

            return this;
        }

        private bool IsOverriddenInterface<T>()
            => IsOverriddenInterface(typeof(T));

        private bool IsOverriddenInterface(Type type)
            => (type.IsInterface || type.IsAbstract) && IsOverriddenType(type);

        private bool IsOverriddenType(Type type)
            => _overrides.Contains(type);

        private IValueFactory CreateValueFactory(Type type, InjectionMember[] injectionMembers)
        {
            if (injectionMembers.Length == 1)
            {
                var injectionMember = injectionMembers[0];

                return injectionMember switch
                {
                    InjectionFactory injectionFactory
                        => new InjectionFactoryBasedValueFactory(this, injectionFactory),
                    InjectionConstructor injectionConstructor
                        => new InjectionConstructorBasedValueFactory(this, _initializationPlanner, _reflectionCache, type, injectionConstructor),
                    _ => throw new NotSupportedException($"Injection member {injectionMember} not supported (type {type})")
                };
            }

            return injectionMembers.Length switch
            {
                0 => new TypeBasedValueFactory(this, _initializationPlanner, _reflectionCache, type),
                _ => throw new NotSupportedException($"Several injection members not supported (type {type})")
            };
        }

        private sealed class InitializationPlanner : IDisposable
        {
            public InitializationPlanner()
            {
                Task.Factory.StartNew(WorkerThread, TaskCreationOptions.LongRunning);
            }

            #region IDisposable

            ~InitializationPlanner()
            {
                Dispose(disposing: false);
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (!_isDisposed)
                {
                    Stop();

                    _isDisposed = true;
                }
            }

            #endregion

            public void Plan(IInitializer initializer)
            {
                _initializers.Add(initializer);
                _autoResetEvent.Set();
            }

            public void EnsureInitialized(IInitializer initializer)
            {
                if (initializer.IsInitialized)
                {
                    return;
                }

                lock (initializer)
                {
                    if (initializer.IsInitialized)
                    {
                        return;
                    }

                    initializer.Initialize();
                }
            }

            private void Stop()
            {
                _tokenSource?.Cancel();
                _autoResetEvent?.Set();
            }

            private void WorkerThread()
            {
                while (_autoResetEvent.WaitOne() && !_tokenSource.IsCancellationRequested)
                {
                    while (_initializers.TryTake(out var initializer))
                    {
                        EnsureInitialized(initializer);
                    }
                }
            }

            private bool _isDisposed;

            private readonly ConcurrentBag<IInitializer> _initializers = new();
            private readonly AutoResetEvent _autoResetEvent = new(false);
            private readonly CancellationTokenSource _tokenSource = new();
        }

        private interface IInitializer
        {
            bool IsInitialized { get; }

            void Initialize();
        }

        private sealed class DiscoveryTypesInAssemblyTask : IInitializer
        {
            public DiscoveryTypesInAssemblyTask(ReflectionCache reflectionCache, Assembly assembly)
            {
                _reflectionCache = reflectionCache;
                _assembly = assembly;
            }

            public bool IsInitialized { get; private set; }

            public void Initialize()
            {
                _reflectionCache.DiscoveryTypesInAssembly(_assembly);
                IsInitialized = true;
            }

            private readonly ReflectionCache _reflectionCache;
            private readonly Assembly _assembly;
        }

        private sealed class ReflectionCache
        {
            public void DiscoveryTypesInAssembly(Assembly assembly)
            {
                var assemblyTypes = assembly.GetExportedTypes();
                var assemblyTypesLength = assemblyTypes.Length;

                for (var i = 0; i < assemblyTypesLength; i++)
                {
                    var type = assemblyTypes[i];
                    if (!type.IsClass || type.IsAbstract || type.IsGenericTypeDefinition || type.IsGenericType || type.IsEnum || type.IsNested)
                    {
                        continue;
                    }

                    _typePublicInstanceConstructorsCache.TryAdd(type, GetTypePublicInstanceConstructorsWithoutCache(type));
                }
            }

            public TypeConstructorInfo[] GetTypePublicInstanceConstructors(Type type)
            {
                if (_typePublicInstanceConstructorsCache.TryGetValue(type, out var constructor))
                {
                    return constructor;
                }

                var constructors = GetTypePublicInstanceConstructorsWithoutCache(type);
                _typePublicInstanceConstructorsCache.TryAdd(type, constructors);

                return constructors;
            }

            private static TypeConstructorInfo[] GetTypePublicInstanceConstructorsWithoutCache(Type type)
            {
                var constructors = type.GetConstructors(PublicInstanceBindingFlags);
                var constructorsInfo = new TypeConstructorInfo[constructors.Length];
                for (var c = 0; c < constructors.Length; c++)
                {
                    constructorsInfo[c] = new TypeConstructorInfo(constructors[c]);
                }

                return constructorsInfo;
            }

            private const BindingFlags PublicInstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public;

            private readonly ConcurrentDictionary<Type, TypeConstructorInfo[]> _typePublicInstanceConstructorsCache = new();
        }

        private readonly struct TypeConstructorInfo
        {
            public TypeConstructorInfo(ConstructorInfo constructorInfo)
            {
                Constructor = constructorInfo;
                Parameters = constructorInfo.GetParameters();
            }

            public readonly ConstructorInfo Constructor;

            public readonly ParameterInfo[] Parameters;
        }

        private interface IDependencyBag
        {
            object Resolve(params ResolverOverride[] resolverOverrides);
        }

        private sealed class ContainerControlledInstanceDependencyBag : IDependencyBag
        {
            public ContainerControlledInstanceDependencyBag(object instance)
            {
                _instance = instance;
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
                => _instance;

            private readonly object _instance;
        }

        private sealed class ExternallyControlledInstanceDependencyBag : IDependencyBag
        {
            public ExternallyControlledInstanceDependencyBag(object instance)
            {
                _instanceRef = new WeakReference(instance);
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
                => _instanceRef.Target
                    ?? throw new ResolutionFailedException("Failed to provide external object because it was destroyed");

            private readonly WeakReference _instanceRef;
        }

        private sealed class ContainerControlledDependencyBag : IDependencyBag
        {
            public ContainerControlledDependencyBag(IValueFactory valueFactory)
            {
                _valueFactory = valueFactory;
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
            {
                if (_value is not null)
                {
                    return _value;
                }

                lock (this)
                {
                    _value ??= _valueFactory.Resolve(resolverOverrides);
                }

                return _value;
            }

            public bool HasValue => _value is not null;

            private object? _value;

            private readonly IValueFactory _valueFactory;
        }

        private sealed class ExternallyControlledDependencyBag : IDependencyBag
        {
            public ExternallyControlledDependencyBag(IValueFactory valueFactory)
            {
                _valueFactory = valueFactory;
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
            {
                var target = _weakReference.Target;
                if (target is null)
                {
                    lock (_weakReference)
                    {
                        target = _weakReference.Target ??= _valueFactory.Resolve(resolverOverrides);
                    }
                }

                return target;
            }

            private readonly IValueFactory _valueFactory;
            private readonly WeakReference _weakReference = new(null);
        }

        private sealed class TransientDependencyBag : IDependencyBag
        {
            public TransientDependencyBag(IValueFactory valueFactory)
            {
                _valueFactory = valueFactory;
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
                => _valueFactory.Resolve(resolverOverrides);

            private readonly IValueFactory _valueFactory;
        }

        private sealed class OpenGenericTypeDependencyBag : IDependencyBag
        {
            public OpenGenericTypeDependencyBag(Type type, LifetimeManager lifetimeManager, InjectionMember[] injectionMembers)
            {
                Type = type;
                LifetimeManager = lifetimeManager;
                InjectionMembers = injectionMembers;
            }

            public Type Type { get; }

            public LifetimeManager LifetimeManager { get; }

            public InjectionMember[] InjectionMembers { get; }

            object IDependencyBag.Resolve(params ResolverOverride[] resolverOverrides)
                => throw new InvalidOperationException($"Can't create instance of open generic type. '{Type.FullName}'.");
        }

        private interface IValueFactory
        {
            object Resolve(params ResolverOverride[] resolverOverrides);
        }

        private sealed class TypeBasedValueFactory : IValueFactory, IInitializer
        {
            public TypeBasedValueFactory(
                IUnityContainer unityContainer,
                InitializationPlanner initializationPlanner,
                ReflectionCache reflectionCache,
                Type type)
            {
                _unityContainer = unityContainer;
                _initializationPlanner = initializationPlanner;
                _reflectionCache = reflectionCache;
                _type = type;

                initializationPlanner.Plan(this);
            }

            public bool IsInitialized { get; private set; }

            public void Initialize()
            {
                var constructorsInfo = _reflectionCache.GetTypePublicInstanceConstructors(_type);
                if (constructorsInfo.Length != 1)
                {
                    throw new NotSupportedException(
                        $"Type with several constructors and without InjectionFactory or InjectionConstructor is not supported (type {_type})");
                }

                var constructorInfo = constructorsInfo[0];
                _constructor = constructorInfo.Constructor;
                _parameters = constructorInfo.Parameters;

                IsInitialized = true;
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
            {
                Guard.ArgumentIsNotNull(resolverOverrides);

                _initializationPlanner.EnsureInitialized(this);

                Guard.IsNotNull(_parameters);
                var parameters = new object[_parameters.Length];

                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameterInfo = _parameters[i];

                    var overrideFound = false;

                    if (resolverOverrides.Length > 0)
                    {
                        for (var resolverIndex = 0; resolverIndex < resolverOverrides.Length; resolverIndex++)
                        {
                            if (resolverOverrides[resolverIndex].TryGetOverride(_type, parameterInfo.ParameterType, parameterInfo.Name ?? Default.String, out var overrideValue))
                            {
                                parameters[i] = overrideValue;
                                overrideFound = true;
                                break;
                            }
                        }
                    }

                    if (!overrideFound)
                    {
                        parameters[i] = _unityContainer.Resolve(parameterInfo.ParameterType, resolverOverrides);
                    }
                }

                Guard.IsNotNull(_constructor);
                var resolvedObject = _constructor.Invoke(parameters);

                return resolvedObject;
            }

            private ParameterInfo[]? _parameters;
            private ConstructorInfo? _constructor;

            private readonly IUnityContainer _unityContainer;
            private readonly InitializationPlanner _initializationPlanner;
            private readonly ReflectionCache _reflectionCache;
            private readonly Type _type;
        }

        private sealed class InjectionConstructorBasedValueFactory : IValueFactory, IInitializer
        {
            public InjectionConstructorBasedValueFactory(
                IUnityContainer unityContainer,
                InitializationPlanner initializationPlanner,
                ReflectionCache reflectionCache,
                Type type,
                InjectionConstructor injectionConstructor)
            {
                _unityContainer = unityContainer;
                _initializationPlanner = initializationPlanner;
                _reflectionCache = reflectionCache;
                _type = type;
                _injectionConstructor = injectionConstructor;

                initializationPlanner.Plan(this);
            }

            public bool IsInitialized { get; private set; }

            public void Initialize()
            {
                var constructorsInfo = _reflectionCache.GetTypePublicInstanceConstructors(_type);
                var resolvedParameters = _injectionConstructor.ResolvedParameters;
                var resolvedParametersLength = resolvedParameters.Length;

                for (var i = 0; i < constructorsInfo.Length; i++)
                {
                    var constructorInfo = constructorsInfo[i];
                    var constructor = constructorInfo.Constructor;
                    var parameters = constructorInfo.Parameters;

                    if (parameters.Length != resolvedParametersLength)
                    {
                        continue;
                    }

                    var isConstructorMatched = true;
                    for (var parameterIndex = 0; parameterIndex < resolvedParametersLength; parameterIndex++)
                    {
                        var constructorParameterInfo = parameters[parameterIndex];
                        var resolvedParameterInfo = resolvedParameters[parameterIndex];
                        if (constructorParameterInfo.ParameterType != resolvedParameterInfo.ParameterType)
                        {
                            isConstructorMatched = false;
                            break;
                        }
                    }

                    if (isConstructorMatched)
                    {
                        _constructor = constructor;
                        _parameters = parameters;
                        break;
                    }
                }

                IsInitialized = true;
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
            {
                Guard.ArgumentIsNotNull(resolverOverrides);

                _initializationPlanner.EnsureInitialized(this);

                Guard.IsNotNull(_parameters);
                var parameters = new object[_parameters.Length];
                var resolvedParameters = _injectionConstructor.ResolvedParameters;

                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameterInfo = _parameters[i];
                    var overrideFound = false;

                    if (resolverOverrides.Length > 0)
                    {
                        for (var resolverIndex = 0; resolverIndex < resolverOverrides.Length; resolverIndex++)
                        {
                            if (resolverOverrides[resolverIndex].TryGetOverride(_type, parameterInfo.ParameterType, parameterInfo.Name ?? Default.String, out var overrideValue))
                            {
                                parameters[i] = overrideValue;
                                overrideFound = true;
                                break;
                            }
                        }
                    }

                    if (!overrideFound)
                    {
                        var resolvedParameterName = resolvedParameters[i].Name ?? Default.String;

                        parameters[i] = _unityContainer.Resolve(parameterInfo.ParameterType, resolvedParameterName, resolverOverrides);
                    }
                }

                Guard.IsNotNull(_constructor);
                return _constructor.Invoke(parameters);
            }

            private ConstructorInfo? _constructor;
            private ParameterInfo[]? _parameters;

            private readonly IUnityContainer _unityContainer;
            private readonly InitializationPlanner _initializationPlanner;
            private readonly ReflectionCache _reflectionCache;
            private readonly Type _type;
            private readonly InjectionConstructor _injectionConstructor;
        }

        private sealed class InjectionFactoryBasedValueFactory : IValueFactory
        {
            public InjectionFactoryBasedValueFactory(IUnityContainer unityContainer, InjectionFactory injectionFactory)
            {
                _unityContainer = unityContainer;
                _injectionFactory = injectionFactory;
            }

            public object Resolve(params ResolverOverride[] resolverOverrides)
                => _injectionFactory.Factory(_unityContainer, resolverOverrides);

            private readonly IUnityContainer _unityContainer;
            private readonly InjectionFactory _injectionFactory;
        }

        private static class Default
        {
            public static readonly InjectionMember[] InjectionMemberArray = new InjectionMember[0];

            public static readonly ResolverOverride[] ResolverOverrideArray = new ResolverOverride[0];

            public static readonly ContainerControlledLifetimeManager ContainerControlledLifetimeManager = new();

            public static readonly TransientLifetimeManager TransientLifetimeManager = new();

            public static readonly string String = string.Empty;
        }

        private sealed class DependencyKeyComparer : IEqualityComparer<DependencyKey>
        {
            public bool Equals(DependencyKey x, DependencyKey y)
                => x.Type == y.Type && x.Name == y.Name;

            public int GetHashCode(DependencyKey obj)
                => (obj.Type.GetHashCode() * 397) ^ obj.Name.GetHashCode();
        }

        private readonly struct DependencyKey : IEquatable<DependencyKey>
        {
            public DependencyKey(Type type, string name)
            {
                Type = type;
                Name = name;
            }

            public bool Equals(DependencyKey dependencyKey)
                => dependencyKey.Type == Type && dependencyKey.Name == Name;

            public override bool Equals(object? obj)
            {
                return obj is DependencyKey dependencyKey && dependencyKey.Type == Type && dependencyKey.Name == Name;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Type.GetHashCode() * 397) ^ Name.GetHashCode();
                }
            }

            public override string ToString()
                => $"{Type.FullName} {Name}";

            public readonly Type Type;
            public readonly string Name;

            public static readonly IEqualityComparer<DependencyKey> Comparer = new DependencyKeyComparer();
        }

        private sealed class GenericResolver<T>
        {
            public GenericResolver(IUnityContainer container, string name)
            {
                _container = container;
                _name = name;
            }

            public T Resolve()
                => _container.Resolve<T>(_name, Default.ResolverOverrideArray);

            private readonly IUnityContainer _container;
            private readonly string _name;
        }

        private sealed class OverridesResolver<T>
        {
            public OverridesResolver(IUnityContainer container, string name, Type[] overrideTypes)
            {
                _container = container;
                _name = name;
                _overrideTypes = overrideTypes;
            }

            public T Resolve(object[] arguments)
            {
                Guard.ArgumentIsNotNull(arguments);
                Guard.Argument(arguments.Length == _overrideTypes.Length, $"Wrong number of arguments. Must be {_overrideTypes.Length}");

                var overrides = new DependencyOverrides();

                for (var i = 0; i < _overrideTypes.Length; i++)
                {
                    overrides.Add(_overrideTypes[i], arguments[i]);
                }

                return _container.Resolve<T>(_name, overrides);
            }

            private readonly IUnityContainer _container;
            private readonly string _name;
            private readonly Type[] _overrideTypes;
        }

        private bool _isDisposed;

        private readonly ConcurrentDictionary<DependencyKey, IDependencyBag> _dependencyBags = new(DependencyKey.Comparer);
        private readonly InitializationPlanner _initializationPlanner = new();
        private readonly ReflectionCache _reflectionCache = new();
        private readonly List<Type> _overrides = new();

        private static readonly Type _funcGenericTypeDefinition = typeof(Func<>);
        private static readonly Type _lazyGenericTypeDefinition = typeof(Lazy<>);
        private static readonly ComponentTracer _tracer = ComponentTracer.Get(ComponentTracers.Container);
    }
}
