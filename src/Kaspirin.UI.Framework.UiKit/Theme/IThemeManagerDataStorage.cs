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
///     An interface for storing a user-selected color theme.
/// </summary>
public interface IThemeManagerDataStorage
{
    /// <summary>
    ///     The current theme selected by the user.
    /// </summary>
    ThemeName? UserTheme { get; set; }
}
