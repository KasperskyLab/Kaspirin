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

namespace Kaspirin.UI.Framework.UiKit.Animation;

/// <summary>
///     Interface of the animation settings provider.
/// </summary>
public interface IAnimationSettingsProvider
{
    /// <summary>
    ///     Event about a property change <see cref="IsAnimationEnabled" />.
    /// </summary>
    event EventHandler AnimationEnabledChanged;

    /// <summary>
    ///     The event about the completion of provider initialization.
    /// </summary>
    event EventHandler Initialized;

    /// <summary>
    ///     Indicates whether animation is enabled.
    /// </summary>
    bool IsAnimationEnabled { get; }

    /// <summary>
    ///     Indicates whether the initialization of the provider has been completed.
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    ///     Provides standard animation properties.
    /// </summary>
    AnimationProperties DefaultAnimationProperties { get; }

    /// <summary>
    ///     Returns the desired frame rate depending on the specified <paramref name="quality" />.
    /// </summary>
    /// <param name="quality">
    ///     Animation quality.
    /// </param>
    /// <returns>
    ///     The desired frame rate, or <see langword="null" /> if <paramref name="quality" /> is <see cref="AnimationRenderQuality.Auto" />.
    /// </returns>
    int? GetDesiredFrameRate(AnimationRenderQuality quality = AnimationRenderQuality.Auto);
}