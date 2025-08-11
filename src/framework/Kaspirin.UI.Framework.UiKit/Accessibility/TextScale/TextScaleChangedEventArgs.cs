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

namespace Kaspirin.UI.Framework.UiKit.Accessibility.TextScale;

/// <summary>
///     Data about the text zoom event.
/// </summary>
public sealed class TextScaleChangedEventArgs
{
    /// <summary>
    ///     Creates a new instance of <see cref="TextScaleChangedEventArgs" /> with the specified values of the old and new scale.
    /// </summary>
    /// <param name="oldScale">
    ///     The old scale value.
    /// </param>
    /// <param name="newScale">
    ///     New scale value.
    /// </param>
    public TextScaleChangedEventArgs(double oldScale, double newScale)
    {
        OldScale = oldScale;
        NewScale = newScale;
    }

    /// <summary>
    ///     New scale value.
    /// </summary>
    public double NewScale { get; }

    /// <summary>
    ///     The old scale value.
    /// </summary>
    public double OldScale { get; }
}