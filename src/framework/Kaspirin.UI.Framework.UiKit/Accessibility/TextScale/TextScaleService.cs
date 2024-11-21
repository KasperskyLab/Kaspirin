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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.UiKit.Accessibility.TextScale
{
    public sealed class TextScaleService : ITextScaleService, IDisposable
    {
        public const string ScaleFactorRegPath = "HKEY_CURRENT_USER\\" + ScaleFactorSubkey;
        public const string ScaleFactorRegValue = "TextScaleFactor";

        public TextScaleService(IRegistry registry, IRegistryTracker registryTracker, IWinRTUISettings uiSettings)
        {
            _winRTuiSettings = Guard.EnsureArgumentIsNotNull(uiSettings);
            _registry = Guard.EnsureArgumentIsNotNull(registry);
            _registryTracker = Guard.EnsureArgumentIsNotNull(registryTracker);

            IsEnabled = true;
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (!OperatingSystemInfo.IsWin10OrHigher)
                {
                    _isEnabled = false;

                    if (value)
                    {
                        _trace.TraceWarning($"Service is unavailable on current OS Version.");
                    }
                }
                else
                {
                    _isEnabled = value;

                    _trace.TraceInformation($"{nameof(IsEnabled)} is set to {value}.");

                    if (_isEnabled)
                    {
                        StartTrackScaleChanges();
                    }
                    else
                    {
                        StopTrackScaleChanges();
                    }

                    UpdateScale();
                }
            }
        }

        public double ScaleFactor { get; private set; } = 1;

        public event TextScaleChangedDelegate? ScaleFactorChanged;

        #region IDisposable

        void IDisposable.Dispose()
        {
            StopTrackScaleChanges();
        }

        #endregion

        private void UpdateScale()
        {
            var scale = 1D;

            if (IsEnabled)
            {
                var systemScale = GetSystemTextScaleFactor();
                if (systemScale > 1)
                {
                    // add 0.07 on each 25% of scale
                    scale += (Math.Floor(systemScale / 0.25) - 3D) * 0.07;
                }
            }

            if (ScaleFactor.NotNearlyEqual(scale))
            {
                var oldScale = ScaleFactor;
                var newScale = scale;

                ScaleFactor = scale;

                _trace.TraceInformation($"ScaleFactor changed to {scale}.");

                Executers.InUiAsync(() => ScaleFactorChanged?.Invoke(this, new TextScaleChangedEventArgs(oldScale, newScale)));
            }
        }

        private void StartTrackScaleChanges()
        {
            if (_isTracking)
            {
                return;
            }

            _isTracking = true;

            if (_winRTuiSettings.IsAvailable)
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
            _trace.TraceInformation("Starting tracking of scale changes from WinRT API");

            _winRTuiSettings.TextScaleFactorChanged -= OnTextScaleFactorChangedFromWinRT;
            _winRTuiSettings.TextScaleFactorChanged += OnTextScaleFactorChangedFromWinRT;
        }

        private void StopTrackChangesFromWinRT()
        {
            _trace.TraceInformation("Stopping tracking of scale changes from WinRT API");

            _winRTuiSettings.TextScaleFactorChanged -= OnTextScaleFactorChangedFromWinRT;
        }

        private void StartTrackChangesFromRegistry()
        {
            _trace.TraceInformation("Starting tracking of scale changes from registry");

            _cancellationTokenSource = new CancellationTokenSource();

            _trackingTask = _registryTracker.TrackChangesAsync(
                ScaleFactorHive,
                ScaleFactorView,
                ScaleFactorSubkey,
                _cancellationTokenSource.Token,
                UpdateScale);
        }

        private void StopTrackChangesFromRegistry()
        {
            _trace.TraceInformation("Stopping tracking of scale changes from registry");

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
                    _trace.TraceInformation($"SystemScaleFactor '{scaleFactor}' provided from WinRT.");

                    scaleProvided = true;
                }
            }

            if (!scaleProvided)
            {
                var scaleFactorRegValue = GetScaleFactorFromRegistry();
                if (scaleFactorRegValue != null)
                {
                    scaleFactor = Convert.ToDouble(scaleFactorRegValue) / DefaultScaleFactorRegValue;
                    _trace.TraceInformation($"SystemScaleFactor '{scaleFactor}' provided from Registry.");

                    scaleProvided = true;
                }
            }

            if (!scaleProvided)
            {
                scaleFactor = GetScaleFactorFromSystemFonts();
                _trace.TraceInformation($"SystemScaleFactor '{scaleFactor}' provided from SystemFonts.");
            }

            if (scaleFactor < 1)
            {
                scaleFactor = 1;
            }

            return scaleFactor;
        }

        private void OnTextScaleFactorChangedFromWinRT(object? sender, EventArgs e)
        {
            UpdateScale();
        }

        private object? GetScaleFactorFromRegistry()
        {
            return _registry.GetValue(ScaleFactorRegPath, ScaleFactorRegValue, DefaultScaleFactorRegValue);
        }

        private double? GetScaleFactorFromWinRT()
        {
            return _winRTuiSettings.IsAvailable ? _winRTuiSettings.TextScaleFactor : null;
        }

        private double GetScaleFactorFromSystemFonts()
        {
            return SystemFonts.MessageFontSize / DefaultMessageFontSize;
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(nameof(TextScaleService));

        private const int DefaultMessageFontSize = 12;
        private const int DefaultScaleFactorRegValue = 100;

        private const RegistryHive ScaleFactorHive = RegistryHive.CurrentUser;
        private const RegistryView ScaleFactorView = RegistryView.Default;
        private const string ScaleFactorSubkey = "Software\\Microsoft\\Accessibility";
        private const string ScaleChangedPropertyName = "NonClientMetrics";

        private readonly IWinRTUISettings _winRTuiSettings;
        private readonly IRegistry _registry;
        private readonly IRegistryTracker _registryTracker;

        private CancellationTokenSource? _cancellationTokenSource;
        private Task? _trackingTask;
        private bool _isEnabled;
        private bool _isTracking;
    }
}
