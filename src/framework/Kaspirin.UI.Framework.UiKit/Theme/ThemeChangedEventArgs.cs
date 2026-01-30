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

namespace Kaspirin.UI.Framework.UiKit.Theme;

/// <summary>
///     Data about the event when the color theme was changed.
/// </summary>
public sealed class ThemeChangedEventArgs
{
    /// <summary>
    ///     Creates a new instance of <see cref="ThemeChangedEventArgs" /> with the specified values of the old and new color themes.
    /// </summary>
    /// <param name="oldScale">
    ///     The old meaning of the color theme.
    /// </param>
    /// <param name="newScale">
    ///     The new meaning of the color theme.
    /// </param>
    public ThemeChangedEventArgs(ThemeName oldTheme, ThemeName newTheme)
    {
        OldTheme = oldTheme;
        NewTheme = newTheme;
    }

    /// <summary>
    ///     New color theme.
    /// </summary>
    public ThemeName NewTheme { get; }

    /// <summary>
    ///     The old color theme.
    /// </summary>
    public ThemeName OldTheme { get; }
}
