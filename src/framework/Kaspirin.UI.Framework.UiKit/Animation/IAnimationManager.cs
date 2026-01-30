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

namespace Kaspirin.UI.Framework.UiKit.Animation;

/// <summary>
///     Interface for the animation settings manager.
/// </summary>
public interface IAnimationManager
{
    /// <summary>
    ///     Event about a property change <see cref="State" />.
    /// </summary>
    event AnimationStateChangedDelegate StateChanged;

    /// <summary>
    ///     Event about completion of initialization of the animation settings manager.
    /// </summary>
    event EventHandler Initialized;

    /// <summary>
    ///     The current state of the animation.
    /// </summary>
    AnimationState State { get; }

    /// <summary>
    ///     The current source of the animation state.
    /// </summary>
    AnimationStateSource Source { get; }

    /// <summary>
    ///     Indicates whether initialization of the Animation settings manager has been completed.
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    ///     Returns animation properties for the specified <paramref name="scope" />.
    /// </summary>
    /// <param name="scope">
    ///     The animation area.
    /// </param>
    /// <returns>
    ///     Animation properties.
    /// </returns>
    AnimationProperties GetAnimationProperties(string scope = "Default");

    /// <summary>
    ///     Returns the desired frame rate for the specified <paramref name="quality" />.
    /// </summary>
    /// <param name="quality">
    ///     Animation quality.
    /// </param>
    /// <returns>
    ///     The desired frame rate.
    /// </returns>
    int GetDesiredFrameRate(AnimationRenderQuality quality = AnimationRenderQuality.Default);

    /// <summary>
    ///     Sets animation properties for the specified <paramref name="scope" />.
    /// </summary>
    /// <param name="properties">
    ///     Animation properties.
    /// </param>
    /// <param name="scope">
    ///     The animation area.
    /// </param>
    void SetAnimationProperties(AnimationProperties properties, string scope = "Default");

    /// <summary>
    ///     Sets the desired frame rate for the specified <paramref name="quality" />.
    /// </summary>
    /// <param name="quality">
    ///     Animation quality.
    /// </param>
    /// <param name="quality">
    ///     The desired frame rate.
    /// </param>
    void SetDesiredFrameRate(int frameRate, AnimationRenderQuality quality = AnimationRenderQuality.Default);

    /// <summary>
    ///     Sets a custom animation state value.
    /// </summary>
    /// <param name="state">
    ///     The value of the animation state.
    /// </param>
    /// <remarks>
    ///     After calling this method, the <see cref="Source" /> property will change to <see cref="AnimationStateSource.User" />.
    /// </remarks>
    void SetUserState(AnimationState state);

    /// <summary>
    ///     Cancels the use of a custom animation state value.
    /// </summary>
    /// <remarks>
    ///     After calling this method, the <see cref="Source" /> property will change to <see cref="AnimationStateSource.System" />.
    /// </remarks>
    void ResetUserState();
}
