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
using System.Collections.Generic;
using System.Windows.Media.Animation;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.UiKit.Animation.Internals;

internal sealed class AnimationManager : IAnimationManager, IDisposable
{
    public AnimationManager(
        IAnimationManagerDataStorage dataStorage,
        IWinRTUISettings winRTUISettings,
        ISystemEvents systemEvents,
        IRenderCapability renderCapability)
    {
        _renderCapability = Guard.EnsureArgumentIsNotNull(renderCapability);
        _storage = Guard.EnsureArgumentIsNotNull(dataStorage);
        _winRTUISettings = Guard.EnsureArgumentIsNotNull(winRTUISettings);
        _systemEvents = Guard.EnsureArgumentIsNotNull(systemEvents);
        _tracer = ComponentTracer.Get(UIKitComponentTracers.Animation, this);
        _propertiesMap = new()
        {
            [DefaultScope] = new()
            {
                Duration = TimeSpan.FromMilliseconds(150),
                Delay = TimeSpan.Zero,
                Easing = new QuadraticEase()
            }
        };

        Initialize();
    }

    public event EventHandler Initialized = (_, _) => { };

    public event AnimationStateChangedDelegate StateChanged = (_, _) => { };

    public bool IsInitialized { get; private set; }

    public AnimationState State
    {
        get => _state;
        private set
        {
            if (_state != value)
            {
                var oldValue = _state;
                var newValue = value;

                _state = value;

                if (IsInitialized)
                {
                    _tracer.TraceInformation($"{nameof(State)} changed from {oldValue} to {newValue}.");
                    StateChanged?.Invoke(this, new(oldValue, newValue));
                }
            }
        }
    }

    public AnimationStateSource Source
    {
        get => _source;
        private set
        {
            if (_source != value)
            {
                var oldValue = _source;
                var newValue = value;

                _source = value;

                if (IsInitialized)
                {
                    _tracer.TraceInformation($"{nameof(Source)} changed from {oldValue} to {newValue}.");
                }
            }
        }
    }

    public void ResetUserState()
    {
        _tracer.TraceInformation($"User animation state reset.");
        _storage.UserAnimationState = null;

        Source = AnimationStateSource.System;

        UpdateState();
    }

    public void SetUserState(AnimationState state)
    {
        _tracer.TraceInformation($"User animation state set to {state}.");
        _storage.UserAnimationState = state;

        Source = AnimationStateSource.User;

        UpdateState();
    }

    public AnimationProperties GetAnimationProperties(string scope = DefaultScope)
    {
        if (_propertiesMap.TryGetValue(scope, out var properties))
        {
            return properties;
        }

        throw new InvalidOperationException($"Unknown scope \"{scope}\".");
    }

    public void SetAnimationProperties(AnimationProperties properties, string scope = DefaultScope)
    {
        _propertiesMap[scope] = properties;
    }

    public int GetDesiredFrameRate(AnimationRenderQuality quality = AnimationRenderQuality.Default)
    {
        return quality switch
        {
            AnimationRenderQuality.Default => _desiredFpsDefaultQuality,
            AnimationRenderQuality.High => _desiredFpsHighQuality,
            AnimationRenderQuality.Low => _desiredFpsLowQuality,
            _ => throw new UnexpectedValueException(quality)
        };
    }

    public void SetDesiredFrameRate(int frameRate, AnimationRenderQuality quality = AnimationRenderQuality.Default)
    {
        switch (quality)
        {
            case AnimationRenderQuality.Default:
                _desiredFpsDefaultQuality = frameRate;
                break;
            case AnimationRenderQuality.High:
                _desiredFpsHighQuality = frameRate;
                break;
            case AnimationRenderQuality.Low:
                _desiredFpsLowQuality = frameRate;
                break;
            default:
                throw new UnexpectedValueException(quality);
        }
    }

    #region IDisposable

    void IDisposable.Dispose()
        => StopTrackStateChanges();

    #endregion

    private void Initialize()
    {
        Executers.InUiSyncOrAsync(() =>
        {
            _source = _storage.UserAnimationState == null
                ? AnimationStateSource.System
                : AnimationStateSource.User;

            StartTrackStateChanges();

            UpdateState();

            _tracer.TraceInformation($"Service initialized. State: {State}. Source: {Source}.");

            IsInitialized = true;
            Initialized.Invoke(this, EventArgs.Empty);
        });
    }

    private void UpdateState()
    {
        switch (Source)
        {
            case AnimationStateSource.User:
                UpdateThemeFromUserSource();
                break;
            case AnimationStateSource.System:
                UpdateThemeFromSystemSource();
                break;
            default:
                throw new UnexpectedValueException(Source);
        }
    }

    private void UpdateThemeFromUserSource()
    {
        if (Source != AnimationStateSource.User)
        {
            return;
        }

        var state = _storage.UserAnimationState;
        if (state == null)
        {
            _tracer.TraceMethodError("User animation state is unset in storage. Fallback to system animation state.");

            ResetUserState();
            return;
        }

        _tracer.TraceInformation($"Theme '{state.Value}' provided from user storage.");
        State = state.Value;
    }

    private void UpdateThemeFromSystemSource()
    {
        if (Source != AnimationStateSource.System)
        {
            return;
        }

        Executers.InUiSyncOrAsync(() =>
        {
            if (!IsHighRenderCapability)
            {
                _tracer.TraceInformation($"Animation disabled because of low render capability");
                State = AnimationState.Disabled;
            }
            else if (_winRTUISettings.IsAvailable)
            {
                State = GetAnimationStateFromWinRT();
            }
            else
            {
                State = GetAnimationStateFromSpi();
            }
        });
    }

    private AnimationState GetAnimationStateFromWinRT()
    {
        var result = _winRTUISettings.AnimationsEnabled;

        _tracer.TraceInformation($"WinRT.AnimationsEnabled = {result}");

        return result
            ? AnimationState.Enabled
            : AnimationState.Disabled;
    }

    private AnimationState GetAnimationStateFromSpi()
    {
        var winApiResult = User32Dll.SystemParametersInfo(SpiType.GetClientAreaAnimation, 0, out var uiEffectsEnabled, SpiFlags.None);

        _tracer.TraceInformation($"SPI_GETCLIENTAREAANIMATION = {uiEffectsEnabled}, SystemParametersInfo = {winApiResult}");

        var result = winApiResult && uiEffectsEnabled;

        return result
            ? AnimationState.Enabled
            : AnimationState.Disabled;
    }

    private void StartTrackStateChanges()
    {
        if (_isTracking)
        {
            return;
        }

        _isTracking = true;

        Executers.InUiSyncOrAsync(() =>
        {
            SubscribeOnRenderCapabilityChanges();
            SubscribeOnUserPreferenceChanges();
        });
    }

    private void StopTrackStateChanges()
    {
        if (!_isTracking)
        {
            return;
        }

        Executers.InUiSyncOrAsync(() =>
        {
            UnsubscribeOnRenderCapabilityChanges();
            UnsubscribeOnUserPreferenceChanges();

            _isTracking = false;
        });
    }

    private void SubscribeOnRenderCapabilityChanges()
    {
        _renderCapability.TierChanged += OnRenderCapabilityTierChanged;
    }

    private void SubscribeOnUserPreferenceChanges()
    {
        _systemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
    }

    private void UnsubscribeOnRenderCapabilityChanges()
    {
        _renderCapability.TierChanged -= OnRenderCapabilityTierChanged;
    }

    private void UnsubscribeOnUserPreferenceChanges()
    {
        _systemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        _tracer.TraceInformation($"UserPreference changed in category {e.Category}");

        if (e.Category.In(UserPreferenceCategory.General, UserPreferenceCategory.Window))
        {
            UpdateState();
        }
    }

    private void OnRenderCapabilityTierChanged(object? sender, EventArgs e)
    {
        _tracer.TraceInformation($"RenderCapability.Tier changed");

        UpdateState();
    }

    private RenderCapabilityTier GetRenderCapability()
    {
        var renderCapability = _renderCapability.Tier;

        _tracer.TraceInformation($"RenderCapability.Tier = {renderCapability}");

        return renderCapability;
    }

    private bool IsHighRenderCapability => GetRenderCapability() == RenderCapabilityTier.Tier2;

    private readonly IRenderCapability _renderCapability;
    private readonly IAnimationManagerDataStorage _storage;
    private readonly IWinRTUISettings _winRTUISettings;
    private readonly ISystemEvents _systemEvents;
    private readonly ComponentTracer _tracer;
    private readonly Dictionary<string, AnimationProperties> _propertiesMap;

    private int _desiredFpsHighQuality = 200;
    private int _desiredFpsDefaultQuality = 20;
    private int _desiredFpsLowQuality = 40;
    private AnimationState _state;
    private AnimationStateSource _source;
    private bool _isTracking;

    private const string DefaultScope = "Default";
}