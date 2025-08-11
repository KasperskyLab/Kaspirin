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

namespace Kaspirin.UI.Framework.UiKit.Localization;

public sealed class LocalizationManager
{
    #region Public static

    public static event Action PrefixChanged
    {
        add => GetInstance().PrefixChangedInternal += value;
        remove => GetInstance().PrefixChangedInternal -= value;
    }

    public static event Action PrefixChanging
    {
        add => GetInstance().PrefixChangingInternal += value;
        remove => GetInstance().PrefixChangingInternal -= value;
    }

    public static event Action ThemeChanged
    {
        add => GetInstance().ThemeChangedInternal += value;
        remove => GetInstance().ThemeChangedInternal -= value;
    }

    public static event Action ThemeChanging
    {
        add => GetInstance().ThemeChangingInternal += value;
        remove => GetInstance().ThemeChangingInternal -= value;
    }

    public static event Action CultureChanged
    {
        add => GetInstance().CultureChangedInternal += value;
        remove => GetInstance().CultureChangedInternal -= value;
    }

    public static event Action CultureChanging
    {
        add => GetInstance().CultureChangingInternal += value;
        remove => GetInstance().CultureChangingInternal -= value;
    }

    /// <summary>
    ///     DisplayCulture is used for application resources localizations.
    /// </summary>
    public static LocalizationCultureInfo DisplayCulture => GetInstance()._displayCulture;

    /// <summary>
    ///     FormatCulture is used to format dates, numbers, percentages.
    /// </summary>
    public static LocalizationCultureInfo FormatCulture => GetInstance()._formatCulture;

    /// <summary>
    ///     OsCulture is used to get OS interface localization.
    /// </summary>
    public static LocalizationCultureInfo OsCulture => GetInstance()._osCulture;

    public static bool IsInitialized => _instance != null;

    public static void Initialize(LocalizationParameters parameters)
    {
        Guard.ArgumentIsNotNull(parameters);

        _instance?.Dispose();
        _instance = new LocalizationManager(parameters);
    }

    public static void Initialize(string culture, string? theme = null, string? prefix = null)
        => Initialize(new LocalizationParameters(culture, theme, prefix));

    public static TLocalizer GetLocalizer<TLocalizer>(string scope) where TLocalizer : ILocalizer
    {
        Guard.ArgumentIsNotNull(scope);

        return GetInstance()._localizerFactory.Resolve<TLocalizer>(scope);
    }

    public static ILocalizer GetLocalizer(string scope, Type localizerType)
    {
        Guard.ArgumentIsNotNull(scope);
        Guard.ArgumentIsNotNull(localizerType);

        return GetInstance()._localizerFactory.Resolve(scope, localizerType);
    }

    public static void StoreMetadata(MetadataItem metadataItem, object metadataRefHolder)
    {
        Guard.ArgumentIsNotNull(metadataItem);
        Guard.ArgumentIsNotNull(metadataRefHolder);

        GetInstance()._metadataStorage.Store(metadataItem, metadataRefHolder);
    }

    public static MetadataItem[] GetMetadata()
    {
        return GetInstance()._metadataStorage.Items;
    }

    public static void ChangeCulture(string culture)
    {
        Guard.ArgumentIsNotNullOrWhiteSpace(culture);

        GetInstance().ChangeCultureImpl(culture);
    }

    public static void ChangeTheme(string? theme)
    {
        GetInstance().ChangeThemeImpl(theme);
    }

    public static void ChangePrefix(string? prefix)
    {
        GetInstance().ChangePrefixImpl(prefix);
    }

    #endregion

    #region Private instance

    private void Dispose()
    {
        _metadataStorage.Clear();
        _localizerFactory.ResetCache();
        _resourceProvider.Dispose();

        _tracer.TraceInformation($"{nameof(LocalizationManager)} disposed.");
    }

    private LocalizationManager(LocalizationParameters parameters)
    {
        Guard.ArgumentIsNotNull(parameters);

        _displayCulture = new LocalizationCultureInfo(parameters.DisplayCulture);
        _formatCulture = new LocalizationCultureInfo(parameters.FormatCulture);
        _osCulture = new LocalizationCultureInfo(parameters.OsCulture);

        SetFrameworkElementsLanguage();

        _culture = parameters.DisplayCulture;
        _theme = parameters.Theme;
        _prefix = parameters.Prefix;

        _metadataStorage = new MetadataStorage(parameters.MetadataStorageSettings);

        _resourceProvider = new ResourceProvider(parameters.ResourceBrowsers, _displayCulture, parameters.Theme, parameters.Prefix);
        _resourceProvider.ResourcesLoaded += (_, _) => _localizerFactory?.ResetCache();

        var parameterFactory = new LocalizerParameterFactory(parameters.LocalizerSettings, _displayCulture, _resourceProvider);

        _localizerFactory = parameters.LocalizerFactory(parameterFactory);

        _exceptionPolicy = new LocExceptionPolicy(parameters.ExceptionPolicySettings);

        _tracer.TraceInformation($"{nameof(LocalizationManager)} initialized with Culture:{_culture}, Theme:{_theme}, Prefix:{_prefix}.");
    }

    private event Action PrefixChangedInternal = () => { };

    private event Action PrefixChangingInternal = () => { };

    private event Action ThemeChangedInternal = () => { };

    private event Action ThemeChangingInternal = () => { };

    private event Action CultureChangedInternal = () => { };

    private event Action CultureChangingInternal = () => { };

    private void ChangeThemeImpl(string? theme)
    {
        if (theme == _theme)
        {
            return;
        }

        ThemeChangingInternal.Invoke();

        _theme = theme;

        _resourceProvider.SetTheme(theme);
        _localizerFactory.ResetCache();

        ThemeChangedInternal.Invoke();

        _metadataStorage.Items.ToList().ForEach(m => m.Update());
    }

    private void ChangePrefixImpl(string? prefix)
    {
        if (prefix == _prefix)
        {
            return;
        }

        PrefixChangingInternal.Invoke();

        _prefix = prefix;

        _resourceProvider.SetPrefix(prefix);
        _localizerFactory.ResetCache();

        PrefixChangedInternal.Invoke();

        _metadataStorage.Items.ToList().ForEach(m => m.Update());
    }

    private void ChangeCultureImpl(string culture)
    {
        Guard.ArgumentIsNotNullOrWhiteSpace(culture);

        if (culture == _culture)
        {
            return;
        }

        CultureChangingInternal.Invoke();

        _culture = culture;

        _displayCulture.SetCulture(culture);
        _localizerFactory.ResetCache();

        CultureChangedInternal.Invoke();

        _metadataStorage.Items.ToList().ForEach(m => m.Update());
    }

    private void SetFrameworkElementsLanguage()
    {
        //It is possible to perform a LanguageProperty override only once
        if (_frameworkElementMetadata == null &&
            _frameworkContentElementMetadata == null)
        {
            var language = _formatCulture.XmlLanguage;

            _frameworkContentElementMetadata = new FrameworkPropertyMetadata(language);
            _frameworkElementMetadata = new FrameworkPropertyMetadata(language);

            FrameworkContentElement.LanguageProperty.OverrideMetadata(typeof(TextElement), _frameworkContentElementMetadata);
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), _frameworkElementMetadata);
        }
    }

    #endregion

    private static LocalizationManager GetInstance()
        => _instance ?? throw new InvalidOperationException($"{nameof(LocalizationManager)} is not initialized.");

    // reference holder
    private readonly LocExceptionPolicy _exceptionPolicy;

    private readonly ResourceProvider _resourceProvider;
    private readonly LocalizerFactory _localizerFactory;
    private readonly MetadataStorage _metadataStorage;
    private readonly LocalizationCultureInfo _displayCulture;
    private readonly LocalizationCultureInfo _formatCulture;
    private readonly LocalizationCultureInfo _osCulture;
    private string _culture;
    private string? _theme;
    private string? _prefix;

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Localization);

    private static FrameworkPropertyMetadata? _frameworkContentElementMetadata;
    private static FrameworkPropertyMetadata? _frameworkElementMetadata;
    private static LocalizationManager? _instance;
}
