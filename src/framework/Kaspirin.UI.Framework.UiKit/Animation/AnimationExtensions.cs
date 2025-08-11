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
using System.Windows;
using System.Windows.Media.Animation;

namespace Kaspirin.UI.Framework.UiKit.Animation;

/// <summary>
///     Extension methods for working with animation.
/// </summary>
public static class AnimationExtensions
{
    /// <summary>
    ///     Forcibly converts the set time to <see cref="Duration" />, considering the possibility of animation.
    /// </summary>
    /// <param name="duration">
    ///     It's time for the transformation.
    /// </param>
    /// <returns>
    ///     The initial time, or 1 millisecond if animation is disabled.
    /// </returns>
    public static Duration CoerceDuration(this Duration duration)
    {
        return ServiceLocator.Instance.GetService<IAnimationSettingsProvider>().IsAnimationEnabled
            ? duration
            : _instant;
    }

    /// <summary>
    ///     Forcibly converts the set time to <see cref="Duration" />, considering the possibility of animation.
    /// </summary>
    /// <param name="duration">
    ///     It's time for the transformation.
    /// </param>
    /// <returns>
    ///     The initial time, or 1 millisecond if animation is disabled.
    /// </returns>
    public static Duration CoerceDuration(this TimeSpan duration)
    {
        return ServiceLocator.Instance.GetService<IAnimationSettingsProvider>().IsAnimationEnabled
            ? new Duration(duration)
            : _instant;
    }

    /// <summary>
    ///     Sets the desired frame rate for the specified <paramref name="storyboard" />.
    /// </summary>
    /// <param name="storyboard">
    ///     The storyboard for which the desired frame rate is set.
    /// </param>
    /// <param name="quality">
    ///     Animation quality.
    /// </param>
    public static void SetFrameRate(this Storyboard storyboard, AnimationRenderQuality quality = AnimationRenderQuality.Auto)
    {
        var frameRate = ServiceLocator.Instance.GetService<IAnimationSettingsProvider>().GetDesiredFrameRate(quality);

        storyboard.SetValue(Timeline.DesiredFrameRateProperty, frameRate);
    }

    private static Duration _instant = new(TimeSpan.FromMilliseconds(1));
}