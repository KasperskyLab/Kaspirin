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

namespace Kaspirin.UI.Framework.WinRT;

/// <summary>
///     An interface for getting OS settings via the WinRT Api.
/// </summary>
public interface IWinRTUISettings
{
    /// <summary>
    ///     Indicates whether the WinRT API is available.
    /// </summary>
    bool IsAvailable { get; }

    /// <summary>
    ///     Indicates whether user interface animation is enabled.
    /// </summary>
    bool AnimationsEnabled { get; }

    /// <summary>
    ///     Specifies the current text zoom level.
    /// </summary>
    double TextScaleFactor { get; }

    /// <summary>
    ///     Gets the system color for the specified <paramref name="ColorType" />.
    /// </summary>
    /// <returns>
    ///     The value of the system color.
    /// </returns>
    Color? GetColorValue(WinRTUIColorType colorType);

    /// <summary>
    ///     Event about changing the setting <see cref="TextScaleFactor" />.
    /// </summary>
    event EventHandler? TextScaleFactorChanged;

    /// <summary>
    ///     An event about changing the system colors.
    /// </summary>
    event EventHandler? ColorValuesChanged;
}
