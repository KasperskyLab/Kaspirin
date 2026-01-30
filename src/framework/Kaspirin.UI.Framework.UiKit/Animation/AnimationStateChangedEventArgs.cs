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

namespace Kaspirin.UI.Framework.UiKit.Animation;

/// <summary>
///     Data about the animation state change event.
/// </summary>
public sealed class AnimationStateChangedEventArgs
{
    /// <summary>
    ///     Creates a new instance of <see cref="AnimationStateChangedEventArgs" /> with the specified
    ///     values of the old and new animation states.
    /// </summary>
    /// <param name="oldScale">
    ///     The old animation state.
    /// </param>
    /// <param name="newScale">
    ///     The new animation state.
    /// </param>
    public AnimationStateChangedEventArgs(AnimationState oldState, AnimationState newState)
    {
        OldState = oldState;
        NewState = newState;
    }

    /// <summary>
    ///     The new animation state.
    /// </summary>
    public AnimationState NewState { get; }

    /// <summary>
    ///     The old animation state.
    /// </summary>
    public AnimationState OldState { get; }
}
