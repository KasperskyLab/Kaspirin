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
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Kaspirin.UI.Framework.UiKit.Localization.LocResources;

namespace Kaspirin.UI.Framework.UiKit.Localization
{
    public sealed class LocalizationManager
    {
        #region Public static

        public static LocalizationManager Current => GetInstance();

        public static void Initialize(LocalizationParameters parameters)
        {
            Guard.ArgumentIsNotNull(parameters);

            _instance = new LocalizationManager(parameters);
        }

        #endregion

        #region Public

        /// <summary>DisplayCulture is used for application resources localizations.</summary>
        public LocalizationCultureInfo DisplayCulture { get; }

        /// <summary>FormatCulture is used to format dates, numbers, percentages.</summary>
        public LocalizationCultureInfo FormatCulture { get; }

        /// <summary>OsCulture is used to get OS interface localization.</summary>
        public LocalizationCultureInfo OsCulture { get; }

        public ResourceProvider ResourceProvider { get; }

        public MetadataStorage MetadataStorage { get; }

        public LocalizerFactoryBase LocalizerFactory { get; }

        public void ChangeTheme(string? theme)
        {
            if (theme == ResourceProvider.Theme)
            {
                return;
            }

            ThemeChanging.Invoke();

            ResourceProvider.SetTheme(theme);
            LocalizerFactory.ResetCache();

            ThemeChanged.Invoke();

            MetadataStorage.Items.ToList().ForEach(m => m.Update());
        }

        public void ChangeCulture(string culture)
        {
            Guard.ArgumentIsNotNull(culture);

            if (culture == DisplayCulture.FullLocalization)
            {
                return;
            }

            CultureChanging.Invoke();

            DisplayCulture.SetCulture(culture);
            LocalizerFactory.ResetCache();

            CultureChanged.Invoke();

            MetadataStorage.Items.ToList().ForEach(m => m.Update());
        }

        public event Action ThemeChanged = () => { };

        public event Action ThemeChanging = () => { };

        public event Action CultureChanged = () => { };

        public event Action CultureChanging = () => { };

        #endregion

        #region Private constructor

        private LocalizationManager(LocalizationParameters parameters)
        {
            Guard.ArgumentIsNotNull(parameters);

            DisplayCulture = new LocalizationCultureInfo(parameters.DisplayCulture);
            FormatCulture = new LocalizationCultureInfo(parameters.FormatCulture);
            OsCulture = new LocalizationCultureInfo(parameters.OsCulture);

            SetFrameworkElementsLanguage();

            MetadataStorage = new MetadataStorage(parameters.MetadataStorageSettings);

            ResourceProvider = new ResourceProvider(parameters.ResourceBrowsers, DisplayCulture, parameters.Theme);
            ResourceProvider.ResourcesLoaded += (_, _) => LocalizerFactory?.ResetCache();

            var parameterFactory = new LocalizerParameterFactory(parameters.LocalizerSettings, DisplayCulture, ResourceProvider);

            LocalizerFactory = parameters.LocalizerFactory(parameterFactory);

            _exceptionPolicy = new LocExceptionPolicy(parameters.ExceptionPolicySettings);
        }

        #endregion

        private void SetFrameworkElementsLanguage()
        {
            //It is possible to perform a LanguageProperty override only once
            if (_frameworkElementMetadata == null &&
                _frameworkContentElementMetadata == null)
            {
                var language = FormatCulture.XmlLanguage;

                _frameworkContentElementMetadata = new FrameworkPropertyMetadata(language);
                _frameworkElementMetadata = new FrameworkPropertyMetadata(language);

                FrameworkContentElement.LanguageProperty.OverrideMetadata(typeof(TextElement), _frameworkContentElementMetadata);
                FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), _frameworkElementMetadata);
            }
        }

        private static LocalizationManager GetInstance()
            => Guard.EnsureIsNotNull(_instance, $"{nameof(LocalizationManager)} is not initialized.");

        // reference holder
        private readonly LocExceptionPolicy _exceptionPolicy;

        private static FrameworkPropertyMetadata? _frameworkContentElementMetadata;
        private static FrameworkPropertyMetadata? _frameworkElementMetadata;
        private static LocalizationManager? _instance;
    }
}
