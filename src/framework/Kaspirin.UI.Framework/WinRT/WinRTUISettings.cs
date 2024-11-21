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

#pragma warning disable  CA1416 // This call site is reachable on all platforms

using System;

#if WINDOWS10_0_17763_0_OR_GREATER
using Windows.UI.ViewManagement;
#endif

namespace Kaspirin.UI.Framework.WinRT
{
    /// <summary>
    ///     Provides an implementation of <see cref="IWinRTUISettings" />.
    /// </summary>
    /// <remarks>
    ///     It is supported only on Windows 10 RS5 and above.
    /// </remarks>
    public sealed class WinRTUISettings : IWinRTUISettings
    {
#if WINDOWS10_0_17763_0_OR_GREATER

        /// <summary>
        ///     Initializes a new instance of the <see cref="WinRTUISettings" /> class.
        /// </summary>
        public WinRTUISettings()
        {
            IsAvailable = OperatingSystemInfo.IsWin10Rs5OrHigher;

            if (IsAvailable)
            {
                _uiSettings = new UISettings();
                _uiSettings.TextScaleFactorChanged += OnTextScaleFactorChanged;
            }
        }

        /// <inheritdoc cref="IWinRTUISettings.TextScaleFactor" />
        public double TextScaleFactor => _uiSettings?.TextScaleFactor ?? 1;

        /// <inheritdoc cref="IWinRTUISettings.AnimationsEnabled" />
        public bool AnimationsEnabled => _uiSettings?.AnimationsEnabled ?? true;

        /// <inheritdoc cref="IWinRTUISettings.IsAvailable" />
        public bool IsAvailable { get; }

        /// <inheritdoc cref="IWinRTUISettings.TextScaleFactorChanged" />
        public event EventHandler? TextScaleFactorChanged;

        private void OnTextScaleFactorChanged(UISettings sender, object args)
        {
            TextScaleFactorChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly UISettings? _uiSettings;
#else
        /// <inheritdoc cref="IWinRTUISettings.TextScaleFactor" />
        public double TextScaleFactor => 1;

        /// <inheritdoc cref="IWinRTUISettings.AnimationsEnabled" />
        public bool AnimationsEnabled => true;

        /// <inheritdoc cref="IWinRTUISettings.IsAvailable" />
        public bool IsAvailable => false;

        /// <inheritdoc cref="IWinRTUISettings.TextScaleFactorChanged" />
        public event EventHandler? TextScaleFactorChanged = (o, e) => { };
#endif   
    }
}
