// Copyright © 2026 AO Kaspersky Lab.
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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.Tests.TestSuites.IoC;

[TestClass]
public sealed class RapidUnityContainerTests
{
    #region Constructor

    [TestMethod]
    public void Constructor_Default_RegistersSelfAsIUnityContainer()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act
        var resolved = container.Resolve<IUnityContainer>();

        // Assert
        Assert.AreSame(container, resolved);
    }

    [TestMethod]
    public void Constructor_WithoutSelfRegistration_DoesNotRegisterSelf()
    {
        // Arrange
        using var container = new RapidUnityContainer(registerSelf: false);

        // Act + Assert
        Assert.IsFalse(container.IsTypeRegistered(typeof(IUnityContainer)));
        Assert.ThrowsException<ResolutionFailedException>(() => container.Resolve<IUnityContainer>());
    }

    #endregion

    #region RegisterInstance

    [TestMethod]
    public void RegisterInstance_Singleton_ResolveReturnsSameInstance()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var instance = new SimpleService();

        // Act
        container.RegisterInstance(instance);

        // Assert
        Assert.AreSame(instance, container.Resolve<SimpleService>());
        Assert.AreSame(instance, container.Resolve(typeof(SimpleService)));
    }

    [TestMethod]
    public void RegisterInstance_WithName_ResolvableOnlyByThatName()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var instance = new ServiceA();

        // Act
        container.RegisterInstance(typeof(ITestService), instance, "special");

        // Assert
        Assert.AreSame(instance, container.Resolve<ITestService>("special"));
        Assert.ThrowsException<ResolutionFailedException>(() => container.Resolve<ITestService>());
    }

    [TestMethod]
    public void RegisterInstance_DuplicateKey_ThrowsGuardException()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterInstance(new SimpleService());

        // Act + Assert
        Assert.ThrowsException<GuardException>(() => container.RegisterInstance(new SimpleService()));
    }

    [TestMethod]
    public void RegisterInstance_NullInstance_ThrowsGuardException()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act + Assert
        Assert.ThrowsException<GuardException>(() => container.RegisterInstance(typeof(SimpleService), null!));
    }

    [TestMethod]
    public void RegisterInstance_TransientLifetime_ThrowsUnexpectedValueException()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act + Assert
        Assert.ThrowsException<UnexpectedValueException>(
            () => container.RegisterInstance(new SimpleService(), LifetimeType.Transient));
    }

    [TestMethod]
    public void RegisterInstance_WeakSingleton_ReturnsSameInstanceWhileAlive()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var instance = new SimpleService();
        container.RegisterInstance(instance, LifetimeType.WeakSingleton);

        // Act
        var resolved = container.Resolve<SimpleService>();

        // Assert
        Assert.AreSame(instance, resolved);
        GC.KeepAlive(instance);
    }

    [TestMethod]
    public void RegisterInstance_WeakSingleton_ThrowsAfterInstanceCollected()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        RegisterCollectableInstance(container);

        // Act
        CollectGarbage();

        // Assert
        Assert.ThrowsException<ResolutionFailedException>(() => container.Resolve<SimpleService>());
    }

    #endregion

    #region RegisterType + lifetimes

    [TestMethod]
    public void RegisterType_ReturnsSameContainerForChaining()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act
        var result = container.RegisterType<SimpleService>();

        // Assert
        Assert.AreSame(container, result);
    }

    [TestMethod]
    public void RegisterType_DefaultLifetimeIsTransient_ReturnsNewInstanceOnEachResolve()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>();

        // Act
        var first = container.Resolve<SimpleService>();
        var second = container.Resolve<SimpleService>();

        // Assert
        Assert.AreNotSame(first, second);
    }

    [TestMethod]
    public void RegisterType_Singleton_ReturnsSameInstanceOnEachResolve()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>(LifetimeType.Singleton);

        // Act
        var first = container.Resolve<SimpleService>();
        var second = container.Resolve<SimpleService>();

        // Assert
        Assert.AreSame(first, second);
    }

    [TestMethod]
    public void RegisterType_WeakSingleton_ReturnsSameInstanceWhileReferenced()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>(LifetimeType.WeakSingleton);

        // Act
        var first = container.Resolve<SimpleService>();
        var second = container.Resolve<SimpleService>();

        // Assert
        Assert.AreSame(first, second);
        GC.KeepAlive(first);
    }

    [TestMethod]
    public void RegisterType_WeakSingleton_RecreatesInstanceAfterCollection()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>(LifetimeType.WeakSingleton);
        var weakReference = ResolveAsWeakReference(container);

        // Act
        CollectGarbage();
        var recreated = container.Resolve<SimpleService>();

        // Assert
        Assert.IsNotNull(recreated);
        Assert.IsFalse(weakReference.IsAlive, "WeakSingleton instance was not collected.");
    }

    [TestMethod]
    public void RegisterType_DuplicateRegistration_ThrowsGuardException()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>();

        // Act + Assert
        Assert.ThrowsException<GuardException>(() => container.RegisterType<SimpleService>());
    }

    [TestMethod]
    public void RegisterType_FromTo_ResolveInterfaceReturnsImplementation()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<ITestService, ServiceA>();

        // Act
        var resolved = container.Resolve<ITestService>();

        // Assert
        Assert.IsInstanceOfType(resolved, typeof(ServiceA));
    }

    [TestMethod]
    public void RegisterType_FromTo_Singleton_SharesInstanceBetweenInterfaceAndImplementation()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<ITestService, ServiceA>(LifetimeType.Singleton);

        // Act
        var byInterface = container.Resolve<ITestService>();
        var byImplementation = container.Resolve<ServiceA>();

        // Assert
        Assert.AreSame(byInterface, byImplementation);
    }

    [TestMethod]
    public void RegisterType_WithNames_ResolvesImplementationByName()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<ITestService, ServiceA>(LifetimeType.Transient, "a");
        container.RegisterType<ITestService, ServiceB>(LifetimeType.Transient, "b");

        // Act + Assert
        Assert.IsInstanceOfType(container.Resolve<ITestService>("a"), typeof(ServiceA));
        Assert.IsInstanceOfType(container.Resolve<ITestService>("b"), typeof(ServiceB));
    }

    #endregion

    #region Dependency injection

    [TestMethod]
    public void Resolve_ClassWithDependencies_ResolvesConstructorParametersFromContainer()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var dependency = new SimpleService();
        container.RegisterInstance(dependency);

        // Act
        var resolved = container.Resolve<ServiceWithDependency>();

        // Assert
        Assert.AreSame(dependency, resolved.Dependency);
    }

    [TestMethod]
    public void Resolve_UnregisteredClass_AutoRegistersAsTransient()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        Assert.IsFalse(container.IsTypeRegistered(typeof(SimpleService)));

        // Act
        var first = container.Resolve<SimpleService>();
        var second = container.Resolve<SimpleService>();

        // Assert
        Assert.IsNotNull(first);
        Assert.AreNotSame(first, second);
        Assert.IsTrue(container.IsTypeRegistered(typeof(SimpleService)));
    }

    [TestMethod]
    public void Resolve_UnregisteredInterface_ThrowsResolutionFailedException()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act + Assert
        Assert.ThrowsException<ResolutionFailedException>(() => container.Resolve<ITestService>());
    }

    [TestMethod]
    public void Resolve_ClassWithMultipleConstructors_ThrowsResolutionFailedException()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act
        var exception = Assert.ThrowsException<ResolutionFailedException>(
            () => container.Resolve<MultiConstructorService>());

        // Assert
        Assert.IsInstanceOfType(exception.InnerException, typeof(NotSupportedException));
    }

    [TestMethod]
    public void RegisterType_WithInjectionConstructor_SelectsMatchingConstructor()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<MultiConstructorService>(
            LifetimeType.Transient,
            new InjectionConstructor(new ResolvedParameter(typeof(SimpleService))));

        // Act
        var resolved = container.Resolve<MultiConstructorService>();

        // Assert
        Assert.IsNotNull(resolved.Dependency);
        Assert.IsFalse(resolved.UsedDefaultConstructor);
    }

    [TestMethod]
    public void RegisterType_WithEmptyInjectionConstructor_SelectsParameterlessConstructor()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<MultiConstructorService>(
            LifetimeType.Transient,
            new InjectionConstructor());

        // Act
        var resolved = container.Resolve<MultiConstructorService>();

        // Assert
        Assert.IsTrue(resolved.UsedDefaultConstructor);
        Assert.IsNull(resolved.Dependency);
    }

    [TestMethod]
    public void RegisterType_WithNamedResolvedParameter_ResolvesNamedDependency()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var namedDependency = new ServiceB();
        container.RegisterInstance(typeof(ITestService), namedDependency, "special");
        container.RegisterType<ServiceWithInterfaceDependency>(
            LifetimeType.Transient,
            new InjectionConstructor(new ResolvedParameter(typeof(ITestService), "special")));

        // Act
        var resolved = container.Resolve<ServiceWithInterfaceDependency>();

        // Assert
        Assert.AreSame(namedDependency, resolved.Dependency);
    }

    [TestMethod]
    public void RegisterType_WithInjectionFactory_UsesFactoryAndPassesContainer()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        IUnityContainer? factoryContainer = null;
        var instance = new ServiceA();

        container.RegisterType<ITestService>(
            LifetimeType.Singleton,
            new InjectionFactory(c =>
            {
                factoryContainer = c;
                return instance;
            }));

        // Act
        var resolved = container.Resolve<ITestService>();

        // Assert
        Assert.AreSame(instance, resolved);
        Assert.AreSame(container, factoryContainer);
    }

    #endregion

    #region Resolver overrides

    [TestMethod]
    public void Resolve_WithDependencyOverride_UsesOverriddenValue()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var replacement = new ServiceB();

        // Act
        var resolved = container.Resolve<ServiceWithInterfaceDependency>(
            new DependencyOverride(typeof(ITestService), replacement));

        // Assert
        Assert.AreSame(replacement, resolved.Dependency);
    }

    [TestMethod]
    public void Resolve_WithDependencyOverride_PropagatesOverrideToNestedDependencies()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var replacement = new ServiceB();

        // Act
        var resolved = container.Resolve<OuterService>(
            new DependencyOverride(typeof(ITestService), replacement));

        // Assert
        Assert.AreSame(replacement, resolved.Inner.Dependency);
    }

    #endregion

    #region Factories (Func, Lazy, parametrized Func)

    [TestMethod]
    public void Resolve_FuncFactory_ReturnsFactoryCreatingNewInstances()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>();

        // Act
        var factory = container.Resolve<Func<SimpleService>>();
        var first = factory();
        var second = factory();

        // Assert
        Assert.IsNotNull(first);
        Assert.AreNotSame(first, second);
    }

    [TestMethod]
    public void Resolve_FuncFactory_ForSingleton_ReturnsSameInstance()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>(LifetimeType.Singleton);

        // Act
        var factory = container.Resolve<Func<SimpleService>>();

        // Assert
        Assert.AreSame(factory(), factory());
        Assert.AreSame(factory(), container.Resolve<SimpleService>());
    }

    [TestMethod]
    public void Resolve_LazyFactory_CreatesValueOnlyOnAccess()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var counter = new InstanceCounter();
        container.RegisterInstance(counter);

        // Act
        var lazy = container.Resolve<Lazy<CountingService>>();

        // Assert
        Assert.AreEqual(0, counter.Count, "Lazy value must not be created before access.");

        var value = lazy.Value;
        Assert.AreEqual(1, counter.Count);
        Assert.AreSame(value, lazy.Value);
        Assert.AreEqual(1, counter.Count);
    }

    [TestMethod]
    public void Resolve_ParametrizedFactory_PassesArgumentsAsOverrides()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act
        var factory = container.Resolve<Func<string, ServiceWithStringParameter>>();
        var resolved = factory("expected value");

        // Assert
        Assert.AreEqual("expected value", resolved.Value);
    }

    #endregion

    #region Generic types

    [TestMethod]
    public void RegisterType_OpenGeneric_ResolvesClosedGenericWithRegisteredLifetime()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType(typeof(Repository<>), LifetimeType.Singleton);

        // Act
        var firstInt = container.Resolve(typeof(Repository<int>));
        var secondInt = container.Resolve(typeof(Repository<int>));
        var firstString = container.Resolve(typeof(Repository<string>));

        // Assert
        Assert.AreSame(firstInt, secondInt);
        Assert.IsInstanceOfType(firstString, typeof(Repository<string>));
    }

    [TestMethod]
    public void RegisterType_OpenGenericInterfaceMapping_ResolvesClosedInterface()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType(typeof(IRepository<>), typeof(Repository<>), LifetimeType.Transient);

        // Act
        var resolved = container.Resolve(typeof(IRepository<int>));

        // Assert
        Assert.IsInstanceOfType(resolved, typeof(Repository<int>));
    }

    [TestMethod]
    public void Resolve_OpenGenericDefinition_ThrowsResolutionFailedException()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType(typeof(Repository<>), LifetimeType.Transient);

        // Act + Assert
        Assert.ThrowsException<ResolutionFailedException>(() => container.Resolve(typeof(Repository<>)));
    }

    #endregion

    #region MapInterface / OverrideType

    [TestMethod]
    public void MapInterface_TargetRegistered_ResolvesInterfaceToTarget()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<ServiceA>(LifetimeType.Singleton);

        // Act
        container.MapInterface<ITestService, ServiceA>();

        // Assert
        Assert.AreSame(container.Resolve<ServiceA>(), container.Resolve<ITestService>());
    }

    [TestMethod]
    public void MapInterface_TargetNotRegistered_ThrowsGuardException()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act + Assert
        Assert.ThrowsException<GuardException>(() => container.MapInterface<ITestService, ServiceA>());
    }

    [TestMethod]
    public void MapInterface_SameTypes_DoesNothing()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act
        container.MapInterface(typeof(SimpleService), typeof(SimpleService));

        // Assert
        Assert.IsFalse(container.IsTypeRegistered(typeof(SimpleService)));
    }

    [TestMethod]
    public void OverrideType_ReplacesExistingRegistration()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<ITestService, ServiceA>(LifetimeType.Singleton);

        // Act
        container.OverrideType<ITestService, ServiceB>(LifetimeType.Singleton);

        // Assert
        Assert.IsInstanceOfType(container.Resolve<ITestService>(), typeof(ServiceB));
    }

    [TestMethod]
    public void OverrideType_SubsequentRegistrationsForOverriddenInterface_AreIgnored()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.OverrideType<ITestService, ServiceB>(LifetimeType.Singleton);

        // Act
        container.RegisterType<ITestService, ServiceA>(LifetimeType.Singleton);
        container.MapInterface<ITestService, ServiceB>();

        // Assert
        Assert.IsInstanceOfType(container.Resolve<ITestService>(), typeof(ServiceB));
    }

    #endregion

    #region IsTypeRegistered / Registrations

    [TestMethod]
    public void IsTypeRegistered_ReflectsRegistrationState()
    {
        // Arrange
        using var container = new RapidUnityContainer();

        // Act + Assert
        Assert.IsFalse(container.IsTypeRegistered(typeof(SimpleService)));

        container.RegisterType<SimpleService>();
        Assert.IsTrue(container.IsTypeRegistered(typeof(SimpleService)));
        Assert.IsFalse(container.IsTypeRegistered(typeof(SimpleService), "named"));

        container.RegisterType<SimpleService>(LifetimeType.Transient, "named");
        Assert.IsTrue(container.IsTypeRegistered(typeof(SimpleService), "named"));
    }

    [TestMethod]
    public void Registrations_ContainsAllRegisteredKeys()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>();
        container.RegisterInstance(typeof(ITestService), new ServiceA(), "named");

        // Act
        var registrations = container.Registrations.ToArray();

        // Assert
        Assert.IsTrue(registrations.Any(r => r.RegisteredType == typeof(IUnityContainer) && r.Name.Length == 0));
        Assert.IsTrue(registrations.Any(r => r.RegisteredType == typeof(SimpleService) && r.Name.Length == 0));
        Assert.IsTrue(registrations.Any(r => r.RegisteredType == typeof(ITestService) && r.Name == "named"));
    }

    #endregion

    #region Threading

    [TestMethod]
    public void Resolve_SingletonConcurrently_CreatesSingleInstance()
    {
        // Arrange
        using var container = new RapidUnityContainer();
        var counter = new InstanceCounter();
        container.RegisterInstance(counter);
        container.RegisterType<CountingService>(LifetimeType.Singleton);

        var resolved = new ConcurrentBag<CountingService>();

        // Act
        Parallel.For(0, 32, _ => resolved.Add(container.Resolve<CountingService>()));

        // Assert
        Assert.AreEqual(1, counter.Count);
        Assert.AreEqual(1, resolved.Distinct().Count());
    }

    #endregion

    #region Dispose

    [TestMethod]
    public void Dispose_DisposesRegisteredAndResolvedSingletonInstances()
    {
        // Arrange
        var container = new RapidUnityContainer();

        var registeredInstance = new DisposableService();
        container.RegisterInstance(registeredInstance);

        container.RegisterType<AnotherDisposableService>(LifetimeType.Singleton);
        var resolvedSingleton = container.Resolve<AnotherDisposableService>();

        // Act
        container.Dispose();

        // Assert
        Assert.IsTrue(registeredInstance.IsDisposed);
        Assert.IsTrue(resolvedSingleton.IsDisposed);
    }

    [TestMethod]
    public void Dispose_DoesNotCreateUnresolvedSingletons_AndDoesNotDisposeTransients()
    {
        // Arrange
        var container = new RapidUnityContainer();
        var counter = new InstanceCounter();
        container.RegisterInstance(counter);
        container.RegisterType<CountingService>(LifetimeType.Singleton);

        var transient = container.Resolve<DisposableService>();

        // Act
        container.Dispose();

        // Assert
        Assert.AreEqual(0, counter.Count, "Unresolved singleton must not be created on dispose.");
        Assert.IsFalse(transient.IsDisposed, "Transient instances must not be disposed by the container.");
    }

    [TestMethod]
    public void Dispose_InstanceDisposeThrows_OtherInstancesAreStillDisposed()
    {
        // Arrange
        var container = new RapidUnityContainer();
        var throwing = new ThrowingDisposableService();
        var wellBehaved = new DisposableService();
        container.RegisterInstance(throwing);
        container.RegisterInstance(wellBehaved);

        // Act
        container.Dispose();

        // Assert
        Assert.IsTrue(wellBehaved.IsDisposed);
    }

    [TestMethod]
    public void Dispose_CalledTwice_DoesNotThrow()
    {
        // Arrange
        var container = new RapidUnityContainer();
        container.RegisterInstance(new DisposableService());

        // Act + Assert
        container.Dispose();
        container.Dispose();
    }

    [TestMethod]
    public void Resolve_AfterDispose_ThrowsObjectDisposedException()
    {
        // Arrange
        var container = new RapidUnityContainer();
        container.RegisterType<SimpleService>();
        container.Dispose();

        // Act + Assert
        Assert.ThrowsException<ObjectDisposedException>(() => container.Resolve<SimpleService>());
    }

    [TestMethod]
    public void Register_AfterDispose_ThrowsObjectDisposedException()
    {
        // Arrange
        var container = new RapidUnityContainer();
        container.Dispose();

        // Act + Assert
        Assert.ThrowsException<ObjectDisposedException>(() => container.RegisterType<SimpleService>());
        Assert.ThrowsException<ObjectDisposedException>(() => container.RegisterInstance(new SimpleService()));
    }

    #endregion

    #region Helpers

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void RegisterCollectableInstance(IUnityContainer container)
        => container.RegisterInstance(new SimpleService(), LifetimeType.WeakSingleton);

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference ResolveAsWeakReference(IUnityContainer container)
        => new(container.Resolve<SimpleService>());

    private static void CollectGarbage()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    #endregion
}

#region Test types

internal interface ITestService
{
}

internal sealed class ServiceA : ITestService
{
}

internal sealed class ServiceB : ITestService
{
}

internal sealed class SimpleService
{
}

internal sealed class ServiceWithDependency
{
    public ServiceWithDependency(SimpleService dependency)
    {
        Dependency = dependency;
    }

    public SimpleService Dependency { get; }
}

internal sealed class ServiceWithInterfaceDependency
{
    public ServiceWithInterfaceDependency(ITestService dependency)
    {
        Dependency = dependency;
    }

    public ITestService Dependency { get; }
}

internal sealed class OuterService
{
    public OuterService(ServiceWithInterfaceDependency inner)
    {
        Inner = inner;
    }

    public ServiceWithInterfaceDependency Inner { get; }
}

internal sealed class MultiConstructorService
{
    public MultiConstructorService()
    {
        UsedDefaultConstructor = true;
    }

    public MultiConstructorService(SimpleService dependency)
    {
        Dependency = dependency;
    }

    public bool UsedDefaultConstructor { get; }

    public SimpleService? Dependency { get; }
}

internal sealed class ServiceWithStringParameter
{
    public ServiceWithStringParameter(string value)
    {
        Value = value;
    }

    public string Value { get; }
}

internal interface IRepository<T>
{
}

internal sealed class Repository<T> : IRepository<T>
{
}

internal sealed class InstanceCounter
{
    public int Count => _count;

    public void Increment() => Interlocked.Increment(ref _count);

    private int _count;
}

internal sealed class CountingService
{
    public CountingService(InstanceCounter counter)
    {
        counter.Increment();
    }
}

internal sealed class DisposableService : IDisposable
{
    public bool IsDisposed { get; private set; }

    public void Dispose() => IsDisposed = true;
}

internal sealed class AnotherDisposableService : IDisposable
{
    public bool IsDisposed { get; private set; }

    public void Dispose() => IsDisposed = true;
}

internal sealed class ThrowingDisposableService : IDisposable
{
    public void Dispose() => throw new InvalidOperationException("Dispose failed");
}

#endregion
