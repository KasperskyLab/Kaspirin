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

using Kaspirin.UI.Framework.Services.Internals;
using Kaspirin.UI.Framework.SystemInfo.Internals;

namespace Kaspirin.UI.Framework.Services;

/// <summary>
///     Provides methods for registering framework services in an IoC container.
/// </summary>
public static class UnityContainerBuilder
{
    /// <summary>
    ///     Performs registration of the framework services in the IoC container.
    /// </summary>
    /// <param name="container">
    ///     The IoC container.
    /// </param>
    /// <remarks>
    ///     This method should be called after registering all overrides of the framework services.
    /// </remarks>
    public static void BuildFrameworkServices(this IUnityContainer container)
    {
        Guard.ArgumentIsNotNull(container);

        container.RegisterDefault<IKeyValueStorage, InMemoryKeyValueStorage>(LifetimeType.Singleton);
        container.RegisterDefault<IRegistry, WinRegistry>(LifetimeType.Singleton);
        container.RegisterDefault<IRegistryTracker, WinRegistryTracker>(LifetimeType.Singleton);
        container.RegisterDefault<ISessionProvider, SessionProvider>(LifetimeType.Singleton);
        container.RegisterDefault<ISystemEvents, SystemEvents>(LifetimeType.WeakSingleton);
        container.RegisterDefault<IWinRTLauncher, WinRTLauncher>(LifetimeType.Singleton);
        container.RegisterDefault<IWinRTUISettings, WinRTUISettings>(LifetimeType.Singleton);
    }
}
