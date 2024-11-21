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

using System.Globalization;
using Kaspirin.UI.Framework.UiKit.Localization.LocResources;

namespace Kaspirin.UI.Framework.UiKit.Localization
{
    public class LocalizationParameters
    {
        public LocalizationParameters(string displayCulture)
        {
            DisplayCulture = Guard.EnsureArgumentIsNotNull(displayCulture);
            LocalizerFactory = CreateDefaultLocalizerFactory;
        }

        public LocalizationParameters(string displayCulture, LocalizerFactoryDelegate localizerFactory)
        {
            DisplayCulture = Guard.EnsureArgumentIsNotNull(displayCulture);
            LocalizerFactory = Guard.EnsureArgumentIsNotNull(localizerFactory);
        }

        public string DisplayCulture { get; }

        public string FormatCulture { get; set; } = CultureInfo.CurrentCulture.Name;

        public string OsCulture { get; set; } = CultureInfo.CurrentUICulture.Name;

        public LocalizerFactoryDelegate LocalizerFactory { get; }

        public string? Theme { get; set; }

        public LocExceptionPolicySettings ExceptionPolicySettings { get; set; } = new();

        public MetadataStorageSettings MetadataStorageSettings { get; set; } = new();

        public LocalizerSettings LocalizerSettings { get; set; } = new();

        public IResourceBrowser[] ResourceBrowsers { get; set; } = new IResourceBrowser[0];

        private static LocalizerFactoryBase CreateDefaultLocalizerFactory(LocalizerParameterFactory parameterFactory)
        {
            return new LocalizerFactory(parameterFactory);
        }
    }
}
