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

#pragma warning disable CA1416 // This call site is reachable on all platforms.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.UiKit.Theme.Internals;

/// <summary>
///     A service for managing the color theme of an application.
/// </summary>
/// <remarks>
///     The service receives information about the color theme value from the WinRT API for Windows
///     10 and later, and in other cases, from the Windows registry. If there is no value in the registry,
///     the WindowColor property from <see cref="SystemColors" /> is used.
/// </remarks>
internal sealed class ThemeManager : IThemeManager, IDisposable
{
    public ThemeManager(
        IRegistry registry,
        IRegistryTracker registryTracker,
        IThemeManagerDataStorage storage,
        IWinRTUISettings winRTUISettings,
        ISystemEvents systemEvents)
    {
        _registry = Guard.EnsureArgumentIsNotNull(registry);
        _registryTracker = Guard.EnsureArgumentIsNotNull(registryTracker);
        _storage = Guard.EnsureArgumentIsNotNull(storage);
        _winRTUISettings = Guard.EnsureArgumentIsNotNull(winRTUISettings);
        _systemEvents = Guard.EnsureArgumentIsNotNull(systemEvents);

        _tracer = ComponentTracer.Get(UIKitComponentTracers.Theme, this);

        Initialize();
    }

    /// <inheritdoc />
    public event ThemeChangedDelegate? ThemeChanged;

    /// <inheritdoc />
    public event ThemeChangedDelegate? SystemThemeChanged;

    /// <inheritdoc />
    public ThemeName Theme
    {
        get => _theme;
        private set
        {
            if (_theme != value)
            {
                var oldValue = _theme;
                var newValue = value;

                _theme = value;

                if (_isInitialized)
                {
                    _tracer.TraceInformation($"{nameof(Theme)} changed from {oldValue} to {newValue}.");
                    ThemeChanged?.Invoke(this, new(oldValue, newValue));
                }
            }
        }
    }

    /// <inheritdoc />
    public ThemeName SystemTheme
    {
        get => _systemTheme;
        private set
        {
            if (_systemTheme != value)
            {
                var oldValue = _systemTheme;
                var newValue = value;

                _systemTheme = value;

                if (_isInitialized)
                {
                    _tracer.TraceInformation($"{nameof(SystemTheme)} changed from {oldValue} to {newValue}.");
                    SystemThemeChanged?.Invoke(this, new(oldValue, newValue));
                }
            }
        }
    }

    /// <inheritdoc />
    public ThemeSource Source
    {
        get => _source;
        private set
        {
            if (_source != value)
            {
                var oldValue = _source;
                var newValue = value;

                _source = value;

                if (_isInitialized)
                {
                    _tracer.TraceInformation($"{nameof(Source)} changed from {oldValue} to {newValue}.");
                }
            }
        }
    }

    /// <inheritdoc />
    public void ResetUserTheme()
    {
        _tracer.TraceInformation($"User theme reset.");
        _storage.UserTheme = null;

        Source = ThemeSource.System;

        UpdateTheme();
    }

    /// <inheritdoc />
    public void SetUserTheme(ThemeName theme)
    {
        _tracer.TraceInformation($"User theme set to {theme}.");
        _storage.UserTheme = theme;

        Source = ThemeSource.User;

        UpdateTheme();
    }

    #region IDisposable

    /// <inheritdoc />
    void IDisposable.Dispose()
        => StopTrackThemeChanges();

    #endregion

    private void Initialize()
    {
        _source = _storage.UserTheme == null
            ? ThemeSource.System
            : ThemeSource.User;

        StartTrackThemeChanges();

        UpdateTheme();

        _isInitialized = true;

        _tracer.TraceInformation($"Service initialized. Theme: {Theme}. Source: {Source}.");
    }

    private void UpdateTheme()
    {
        UpdateThemeFromUserSource();
        UpdateThemeFromSystemSource();
    }

    private void UpdateThemeFromUserSource()
    {
        if (Source != ThemeSource.User)
        {
            return;
        }

        var theme = _storage.UserTheme;
        if (theme == null)
        {
            _tracer.TraceMethodError("User theme is unset in storage. Fallback to system theme.");

            ResetUserTheme();
            return;
        }

        _tracer.TraceInformation($"Theme '{theme.Value}' provided from user storage.");
        Theme = theme.Value;
    }

    private void UpdateThemeFromSystemSource()
    {
        SystemTheme = GetThemeFromSystemSource();

        if (Source != ThemeSource.System)
        {
            return;
        }

        Theme = SystemTheme;
    }

    private ThemeName GetThemeFromSystemSource()
    {
        var theme = ThemeName.Light;
        var themeProvided = false;

        if (!themeProvided)
        {
            var themeWinRTValue = GetThemeFromWinRT();
            if (themeWinRTValue != null)
            {
                theme = themeWinRTValue.Value;
                _tracer.TraceInformation($"Theme '{theme}' provided from WinRT.");

                themeProvided = true;
            }
        }

        if (!themeProvided)
        {
            var themeRegValue = GetThemeFromRegistry();
            if (themeRegValue != null)
            {
                theme = themeRegValue.Value;
                _tracer.TraceInformation($"Theme '{theme}' provided from Registry.");

                themeProvided = true;
            }
        }

        if (!themeProvided)
        {
            theme = GetThemeFromSystemProperties();
            _tracer.TraceInformation($"Theme '{theme}' provided from System Properties.");
        }

        return theme;
    }

    private void StartTrackThemeChanges()
    {
        if (_isTracking)
        {
            return;
        }

        _isTracking = true;

        if (_winRTUISettings.IsAvailable)
        {
            StartTrackChangesFromWinRT();
        }
        else
        {
            var isRegistryValueAvailable = GetThemeFromRegistry() != null;
            if (isRegistryValueAvailable)
            {
                StartTrackChangesFromRegistry();

            }
            else
            {
                StartTrackChangesFromSystemColors();
            }
        }
    }

    private void StopTrackThemeChanges()
    {
        if (!_isTracking)
        {
            return;
        }

        StopTrackChangesFromWinRT();
        StopTrackChangesFromRegistry();
        StopTrackChangesFromSystemColors();

        _isTracking = false;
    }

    private void StartTrackChangesFromWinRT()
    {
        _winRTUISettings.ColorValuesChanged -= OnThemeChangedFromWinRT;
        _winRTUISettings.ColorValuesChanged += OnThemeChangedFromWinRT;

        _tracer.TraceInformation("Tracking of theme changes from WinRT API started.");
    }

    private void StopTrackChangesFromWinRT()
    {
        _winRTUISettings.ColorValuesChanged -= OnThemeChangedFromWinRT;

        _tracer.TraceInformation("Tracking of theme changes from WinRT API stopped.");
    }

    private void StartTrackChangesFromRegistry()
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _trackingTask = _registryTracker.TrackChangesAsync(
            ThemeHive,
            ThemeView,
            ThemeSubkey,
            _cancellationTokenSource.Token,
            OnThemeChangedFromRegistry);

        _tracer.TraceInformation("Tracking of theme changes from registry started.");
    }

    private void StopTrackChangesFromRegistry()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();

            if (_trackingTask is not null)
            {
                var cancellationTokenSource = _cancellationTokenSource;
                _cancellationTokenSource = null;

                _trackingTask.ContinueWith(_ => cancellationTokenSource.Dispose());
                _trackingTask = null;
            }
            else
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        _tracer.TraceInformation("Tracking of theme changes from registry stopped.");
    }

    private void StartTrackChangesFromSystemColors()
    {
        Executers.DispatcherExecutor.WhenAvailable(() =>
        {
            _systemEvents.UserPreferenceChanged -= OnThemeChangedFromSystemColors;
            _systemEvents.UserPreferenceChanged += OnThemeChangedFromSystemColors;

            _tracer.TraceInformation("Tracking of theme changes from system colors started.");
        });
    }

    private void StopTrackChangesFromSystemColors()
    {
        _systemEvents.UserPreferenceChanged -= OnThemeChangedFromSystemColors;

        _tracer.TraceInformation("Tracking of theme changes from system colors stopped.");
    }

    private void OnThemeChangedFromWinRT(object? sender, EventArgs e)
        => UpdateThemeFromSystemSource();

    private void OnThemeChangedFromRegistry()
        => UpdateThemeFromSystemSource();

    private void OnThemeChangedFromSystemColors(object? sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category == UserPreferenceCategory.Color)
        {
            UpdateThemeFromSystemSource();
        }
    }

    private ThemeName? GetThemeFromWinRT()
    {
        if (!_winRTUISettings.IsAvailable)
        {
            return null;
        }

        var bgColor = _winRTUISettings.GetColorValue(WinRTUIColorType.Background);
        if (bgColor == null)
        {
            return null;
        }

        var isDark = IsDarkColor(bgColor.Value);

        return isDark
            ? ThemeName.Dark
            : ThemeName.Light;
    }

    private ThemeName? GetThemeFromRegistry()
    {
        try
        {
            var value = _registry.GetValue(ThemeRegPath, ThemeRegValue, null);
            return value == null
                ? null
                : Convert.ToInt32(value) == 1
                    ? ThemeName.Light
                    : ThemeName.Dark;
        }
        catch (Exception e)
        {
            e.TraceExceptionSuppressed();

            _tracer.TraceWarning("Failed to get theme from registry");
            return null;
        }
    }

    private ThemeName GetThemeFromSystemProperties()
    {
        var bgColor = SystemColors.WindowColor;

        var isDark = IsDarkColor(bgColor);

        return isDark
            ? ThemeName.Dark
            : ThemeName.Light;
    }

    private static bool IsDarkColor(Color color)
        => (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255 < 0.5;

    private readonly IRegistry _registry;
    private readonly IRegistryTracker _registryTracker;
    private readonly IThemeManagerDataStorage _storage;
    private readonly IWinRTUISettings _winRTUISettings;
    private readonly ISystemEvents _systemEvents;
    private readonly ComponentTracer _tracer;

    private const string ThemeRegPath = ThemeRegistryConstants.ThemeRegPath;
    private const string ThemeRegValue = ThemeRegistryConstants.ThemeRegValue;
    private const string ThemeSubkey = ThemeRegistryConstants.ThemeSubkey;
    private const RegistryHive ThemeHive = RegistryHive.CurrentUser;
    private const RegistryView ThemeView = RegistryView.Default;

    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _trackingTask;
    private ThemeSource _source;
    private ThemeName _theme;
    private ThemeName _systemTheme;
    private bool _isInitialized;
    private bool _isTracking;
}
