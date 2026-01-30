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
///     Provides information and color theme management.
/// </summary>
public interface IThemeManager
{
    /// <summary>
    ///     Event about changing the app's color theme.
    /// </summary>
    event ThemeChangedDelegate ThemeChanged;

    /// <summary>
    ///     Event about changing the color theme of the operating system.
    /// </summary>
    event ThemeChangedDelegate SystemThemeChanged;

    /// <summary>
    ///     The current color theme of the app.
    /// </summary>
    ThemeName Theme { get; }

    /// <summary>
    ///     The current color theme of the operating system.
    /// </summary>
    ThemeName SystemTheme { get; }

    /// <summary>
    ///     The current source of the app's color theme.
    /// </summary>
    ThemeSource Source { get; }

    /// <summary>
    ///     Sets the custom value of the application's color theme.
    /// </summary>
    /// <param name="theme">
    ///     The meaning of the color theme.
    /// </param>
    /// <remarks>
    ///     After calling this method, the <see cref="Source" /> property will change to <see cref="ThemeSource.User" />.
    /// </remarks>
    void SetUserTheme(ThemeName theme);

    /// <summary>
    ///     Cancels the use of the application's custom color theme.
    /// </summary>
    /// <remarks>
    ///     After calling this method, the <see cref="Source" /> property will change to <see cref="ThemeSource.System" />.
    /// </remarks>
    void ResetUserTheme();
}