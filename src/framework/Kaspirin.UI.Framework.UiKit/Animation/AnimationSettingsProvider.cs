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
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.UiKit.Animation
{
    public sealed class AnimationSettingsProvider : IAnimationSettingsProvider
    {
        public AnimationSettingsProvider(
            IWinRTUISettings winRTUISettings,
            ISystemEvents systemEvents)
        {
            _winRTUISettings = winRTUISettings;
            _systemEvents = systemEvents;

            Initialize();
        }

        public event EventHandler AnimationEnabledChanged = (_, _) => { };

        public event EventHandler Initialized = (_, _) => { };

        public bool IsAnimationEnabled
        {
            get => _isAnimationEnabled;
            private set
            {
                if (_isAnimationEnabled == value)
                {
                    return;
                }

                _trace.TraceInformation($"Animation state changed from {_isAnimationEnabled} to {value}");

                _isAnimationEnabled = value;

                AnimationEnabledChanged.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsInitialized { get; private set; }

        public AnimationProperties DefaultAnimationProperties => new()
        {
            Duration = TimeSpan.FromMilliseconds(150),
            Delay = TimeSpan.Zero,
            Easing = new QuadraticEase()
        };

        public int? GetDesiredFrameRate(AnimationRenderQuality quality = AnimationRenderQuality.Auto)
        {
            return quality switch
            {
                AnimationRenderQuality.High => HighFps,
                AnimationRenderQuality.Low => LowFps,
                _ => null
            };
        }

        /// <returns>
        ///     True if DirectX version is greater than or equal to 9.0.
        /// </returns>
        private bool IsHighRenderCapability => GetRenderCapability() >= 0x00020000;

        private void Initialize()
            => Executers.InUiAsync(() =>
            {
                _trace.TraceInformation($"Initialization started");

                SubscribeOnRenderCapabilityChanges();
                SubscribeOnUserPreferenceChanges();

                UpdateAnimationState();

                IsInitialized = true;
                Initialized.Invoke(this, EventArgs.Empty);

                _trace.TraceInformation($"Initialization finished");
            });

        private void UpdateAnimationState()
        {
            if (!IsHighRenderCapability)
            {
                _trace.TraceInformation($"Animation disabled because of low render capability");
                IsAnimationEnabled = false;
            }
            else if (_winRTUISettings.IsAvailable)
            {
                IsAnimationEnabled = GetAnimationEnabledFromWinRT();
            }
            else
            {
                IsAnimationEnabled = GetAnimationEnabledFromSpi();
            }
        }

        private bool GetAnimationEnabledFromWinRT()
        {
            var result = _winRTUISettings.AnimationsEnabled;

            _trace.TraceInformation($"WinRT.AnimationsEnabled = {result}");

            return result;
        }

        private bool GetAnimationEnabledFromSpi()
        {
            var winApiResult = User32Dll.SystemParametersInfo(SpiType.SPI_GETCLIENTAREAANIMATION, 0, out var uiEffectsEnabled, SpiFlags.None);

            _trace.TraceInformation($"SPI_GETCLIENTAREAANIMATION = {uiEffectsEnabled}, SystemParametersInfo = {winApiResult}");

            return winApiResult && uiEffectsEnabled;
        }

        private void SubscribeOnRenderCapabilityChanges()
        {
            // Subscribe in UI thread because RenderCapability.Tier property is using Dispatcher object of current thread.
            Guard.AssertIsUiThread();
            RenderCapability.TierChanged += OnRenderCapabilityTierChanged;
        }

        private void SubscribeOnUserPreferenceChanges()
        {
            _systemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
        }

        private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            _trace.TraceInformation($"UserPreference changed in category {e.Category}");

            if (e.Category.In(UserPreferenceCategory.General, UserPreferenceCategory.Window))
            {
                Executers.InUiAsync(UpdateAnimationState);
            }
        }

        private void OnRenderCapabilityTierChanged(object? sender, EventArgs e)
        {
            _trace.TraceInformation($"RenderCapability.Tier changed");

            Executers.InUiAsync(UpdateAnimationState);
        }

        private static int GetRenderCapability()
        {
            // Getting value in UI thread because RenderCapability.Tier property is using Dispatcher object of current thread.
            Guard.AssertIsUiThread();

            var renderCapability = RenderCapability.Tier;

            _trace.TraceInformation($"RenderCapability.Tier = 0x{renderCapability:x}");

            return renderCapability;
        }

        private bool _isAnimationEnabled;

        private readonly IWinRTUISettings _winRTUISettings;
        private readonly ISystemEvents _systemEvents;

        private const int HighFps = 200;
        private const int LowFps = 20;

        private static readonly ComponentTracer _trace = ComponentTracer.Get(nameof(AnimationSettingsProvider));
    }
}
