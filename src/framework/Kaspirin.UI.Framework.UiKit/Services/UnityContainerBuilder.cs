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
using Kaspirin.UI.Framework.UiKit.Animation.Internals;
using Kaspirin.UI.Framework.UiKit.Input.Security.Internals;
using Kaspirin.UI.Framework.UiKit.Interactivity.Internals;
using Kaspirin.UI.Framework.UiKit.Navigation;
using Kaspirin.UI.Framework.UiKit.Statistics.Internals;

namespace Kaspirin.UI.Framework.UiKit.Services;

public static class UnityContainerBuilder
{
    public static void BuildUiKitServices(this IUnityContainer container)
    {
        Guard.ArgumentIsNotNull(container);

        container.RegisterDefault<IRegionViewFactory, RegionViewFactory>(LifetimeType.Singleton);
        container.RegisterDefault<IRegionBehaviorsRegistry, RegionBehaviorRegistry>(LifetimeType.Singleton);
        container.RegisterType<Regions>(LifetimeType.Singleton);

        container.RegisterDefault<ISecureClipboard, SecureClipboard>(LifetimeType.Singleton);
        container.RegisterDefault<ISecureInputManager, EmptySecureInputManager>(LifetimeType.Singleton);
        container.RegisterDefault<IStatisticsSender, EmptyStatisticsSender>(LifetimeType.Singleton);
        container.RegisterDefault<INativeDialogLauncher, NativeDialogLauncher>(LifetimeType.Singleton);
        container.RegisterDefault<IAnimationSettingsProvider, AnimationSettingsProvider>(LifetimeType.Singleton);
        container.RegisterDefault<ITextScaleService, TextScaleService>(LifetimeType.Singleton);
        container.RegisterDefault<IInteractionObjects, InteractionObjects>(LifetimeType.Singleton);

        container.RegisterType<AnimatedBindingFactory>(LifetimeType.Singleton);

        container.BuildFrameworkServices();
    }
}
