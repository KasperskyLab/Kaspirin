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

#pragma warning disable CA1416 // This call site is reachable on all platforms.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.UiKit.Accessibility.TextScale.Internals;

/// <summary>
///     A service for controlling text scaling.
/// </summary>
/// <remarks>
///     The service receives information about the text scaling value from the WinRT API for Windows
///     10 and later, and in other cases from the Windows registry. If there is no value in the registry,
///     the <see cref="SystemFonts.MessageFontSize" /> setting is used.
/// </remarks>
internal sealed class TextScaleService : ITextScaleService, IDisposable
{
    /// <summary>
    ///     Initializes an instance of the <see cref="TextScaleService" /> class.
    /// </summary>
    /// <param name="registry">
    ///     An object for getting settings from the Windows registry.
    /// </param>
    /// <param name="registryTracker">
    ///     An object for tracking changes to settings in the Windows registry.
    /// </param>
    /// <param name="winRTUISettings">
    ///     An object for getting settings from the WinRT API.
    /// </param>
    public TextScaleService(IRegistry registry, IRegistryTracker registryTracker, IWinRTUISettings winRTUISettings)
    {
        _winRTUISettings = Guard.EnsureArgumentIsNotNull(winRTUISettings);
        _registry = Guard.EnsureArgumentIsNotNull(registry);
        _registryTracker = Guard.EnsureArgumentIsNotNull(registryTracker);
        _tracer = ComponentTracer.Get(UIKitComponentTracers.Accessibility, this);

        Initialize();
    }

    /// <inheritdoc />
    public double ScaleFactor
    {
        get => _scaleFactor;
        private set
        {
            if (_scaleFactor.NotNearlyEqual(value))
            {
                var oldValue = _scaleFactor;
                var newValue = value;

                _scaleFactor = value;

                if (_itInitialized)
                {
                    _tracer.TraceInformation($"{nameof(ScaleFactor)} changed from {oldValue} to {newValue}.");
                    ScaleFactorChanged?.Invoke(this, new(oldValue, newValue));
                }
            }
        }
    }

    /// <inheritdoc />
    public event TextScaleChangedDelegate? ScaleFactorChanged;

    #region IDisposable

    /// <inheritdoc />
    void IDisposable.Dispose()
        => StopTrackScaleChanges();

    #endregion

    private void Initialize()
    {
        StartTrackScaleChanges();

        UpdateScale();

        _itInitialized = true;

        _tracer.TraceInformation($"Service initialized. ScaleFactor: {ScaleFactor}.");
    }

    private void UpdateScale()
    {
        var scale = 1D;

        var systemScale = GetSystemTextScaleFactor();
        if (systemScale > 1)
        {
            // add 0.07 on each 25% of scale
            scale += (Math.Floor(systemScale / 0.25) - 3D) * 0.07;
        }

        ScaleFactor = scale;
    }

    private void StartTrackScaleChanges()
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
            StartTrackChangesFromRegistry();
        }
    }

    private void StopTrackScaleChanges()
    {
        if (!_isTracking)
        {
            return;
        }

        StopTrackChangesFromWinRT();
        StopTrackChangesFromRegistry();

        _isTracking = false;
    }

    private void StartTrackChangesFromWinRT()
    {
        _winRTUISettings.TextScaleFactorChanged -= OnTextScaleFactorChangedFromWinRT;
        _winRTUISettings.TextScaleFactorChanged += OnTextScaleFactorChangedFromWinRT;

        _tracer.TraceInformation("Tracking of scale changes from WinRT API started.");
    }

    private void StopTrackChangesFromWinRT()
    {
        _tracer.TraceInformation("Tracking of scale changes from WinRT API stopped.");

        _winRTUISettings.TextScaleFactorChanged -= OnTextScaleFactorChangedFromWinRT;
    }

    private void StartTrackChangesFromRegistry()
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _trackingTask = _registryTracker.TrackChangesAsync(
            ScaleFactorHive,
            ScaleFactorView,
            ScaleFactorSubkey,
            _cancellationTokenSource.Token,
            OnTextScaleFactorChangedFromRegistry);

        _tracer.TraceInformation("Tracking of scale changes from registry started.");
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

        _tracer.TraceInformation("Tracking of scale changes from registry stopped.");
    }

    private double GetSystemTextScaleFactor()
    {
        var scaleFactor = 1D;
        var scaleProvided = false;

        if (!scaleProvided)
        {
            var scaleFactorWinRTValue = GetScaleFactorFromWinRT();
            if (scaleFactorWinRTValue != null)
            {
                scaleFactor = scaleFactorWinRTValue.Value;
                _tracer.TraceInformation($"SystemScaleFactor '{scaleFactor}' provided from WinRT.");

                scaleProvided = true;
            }
        }

        if (!scaleProvided)
        {
            var scaleFactorRegValue = GetScaleFactorFromRegistry();
            if (scaleFactorRegValue != null)
            {
                scaleFactor = scaleFactorRegValue.Value;
                _tracer.TraceInformation($"SystemScaleFactor '{scaleFactor}' provided from Registry.");

                scaleProvided = true;
            }
        }

        if (!scaleProvided)
        {
            scaleFactor = GetScaleFactorFromSystemFonts();
            _tracer.TraceInformation($"SystemScaleFactor '{scaleFactor}' provided from SystemFonts.");
        }

        if (scaleFactor < 1)
        {
            scaleFactor = 1;
        }

        return scaleFactor;
    }

    private void OnTextScaleFactorChangedFromWinRT(object? sender, EventArgs e)
        => UpdateScale();

    private void OnTextScaleFactorChangedFromRegistry()
        => UpdateScale();

    private double? GetScaleFactorFromRegistry()
    {
        var value = _registry.GetValue(ScaleFactorRegPath, ScaleFactorRegValue, null);

        return value == null
            ? null
            : Convert.ToDouble(value) / DefaultScaleFactorRegValue;
    }

    private double? GetScaleFactorFromWinRT()
        => _winRTUISettings.IsAvailable ? _winRTUISettings.TextScaleFactor : null;

    private double GetScaleFactorFromSystemFonts()
        => SystemFonts.MessageFontSize / DefaultMessageFontSize;

    private const int DefaultMessageFontSize = 12;
    private const int DefaultScaleFactorRegValue = 100;
    private const string ScaleFactorRegPath = TextScaleRegistryConstants.ScaleFactorRegPath;
    private const string ScaleFactorRegValue = TextScaleRegistryConstants.ScaleFactorRegValue;
    private const string ScaleFactorSubkey = TextScaleRegistryConstants.ScaleFactorSubkey;
    private const RegistryHive ScaleFactorHive = RegistryHive.CurrentUser;
    private const RegistryView ScaleFactorView = RegistryView.Default;

    private readonly ComponentTracer _tracer;
    private readonly IWinRTUISettings _winRTUISettings;
    private readonly IRegistry _registry;
    private readonly IRegistryTracker _registryTracker;

    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _trackingTask;
    private bool _isTracking;
    private bool _itInitialized;
    private double _scaleFactor;
}