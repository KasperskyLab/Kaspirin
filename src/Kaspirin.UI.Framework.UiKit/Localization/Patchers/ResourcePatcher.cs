// Copyright © 2025 AO Kaspersky Lab.
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
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Automation;

namespace Kaspirin.UI.Framework.UiKit.Localization.Patchers
{
    internal sealed class ResourcePatcher
    {
        public static void PatchResources()
        {
            _instance.Tracer.TraceMethodStart();

            _instance.PatchResourcesInSR("System.SR", typeof(ControlType), new List<string>
            {
                "LocalizedControlTypeHyperlink",
                "LocalizedControlTypeImage"
            });

            _instance.Tracer.TraceMethodFinish();
        }

        private ResourcePatcher()
        {
            Tracer = ComponentTracer.Get(this);
        }

        private void PatchResourcesInSR(string srTypeName, Type typeInSameAssembly, List<string> locKeys)
        {
            Tracer.TraceMethodInfo($"arguments: {nameof(srTypeName)}: {srTypeName}, {nameof(typeInSameAssembly)}: {typeInSameAssembly}, {nameof(locKeys)}: {string.Join(", ", locKeys)}");

            var resourceManagerHolder = GetType(typeInSameAssembly.Assembly, srTypeName);
            if (resourceManagerHolder is not null)
            {
                var resourceManager = TypeAccessor.GetPropertyValue<ResourceManager>(resourceManagerHolder, "ResourceManager", throwOnError: false);
                if (resourceManager != null)
                {
                    var srType = GetType(typeInSameAssembly.Assembly, resourceManager.BaseName);
                    if (srType is not null)
                    {
                        var customResourceManager = new CustomResourceManager(srType, locKeys);
                        TypeAccessor.SetFieldValue(resourceManagerHolder, "s_resourceManager", customResourceManager, throwOnError: false);
                        Tracer.TraceMethodInfo($"{nameof(CustomResourceManager)} is installed");
                        return;
                    }
                }
            }

            Tracer.TraceMethodWarning($"{nameof(CustomResourceManager)} wasn't installed");

            Type? GetType(Assembly assembly, string typeName)
            {
                var type = assembly.GetType(typeName);
                if (type == null)
                {
                    Tracer.TraceMethodWarning($"Type {typeName} wasn't found in {assembly}");
                }

                return type;
            }
        }

        private sealed class CustomResourceManager : ResourceManager
        {
            public CustomResourceManager(Type resourceSource, List<string> replacedLocKeys) : base(resourceSource)
            {
                Tracer = ComponentTracer.Get(this);

                _replacedLocKeys = replacedLocKeys;
                _localizer = LocalizationManager.GetLocalizer<IStringLocalizer>(UIKitConstants.LocalizationScope);
            }

            public override string? GetString(string name, CultureInfo? culture)
            {
                culture ??= CultureInfo.CurrentUICulture;

                Tracer.TraceInformation($"Request to localize {name} in culture {culture}");

                var displayedCulture = LocalizationManager.DisplayCulture.CultureInfo.TwoLetterISOLanguageName;

                if (culture.TwoLetterISOLanguageName == displayedCulture)
                {
                    if (_replacedLocKeys.Contains(name))
                    {
                        var patchedString = _localizer.GetString(name);
                        Tracer.TraceInformation($"Return localized {name}: {patchedString}");
                        return patchedString;
                    }
                    else
                    {
                        Tracer.TraceInformation($"no need to localize locKey {name}, skipped");
                    }
                }
                else
                {
                    Tracer.TraceInformation($"Requested culture {culture.Name} is different from displayed culture {displayedCulture}, skipped");
                }

                return base.GetString(name, culture);
            }

            private ComponentTracer Tracer { get; }

            private readonly List<string> _replacedLocKeys;
            private readonly IStringLocalizer _localizer;
        }

        private ComponentTracer Tracer { get; }

        private static readonly ResourcePatcher _instance = new();
    }
}
