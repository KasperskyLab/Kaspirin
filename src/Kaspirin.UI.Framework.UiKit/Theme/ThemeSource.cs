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
///     Sources of the color theme.
/// </summary>
public enum ThemeSource
{
    /// <summary>
    ///     The color theme installed in the operating system is used.
    /// </summary>
    System,

    /// <summary>
    ///     The color theme set by the user is used.
    /// </summary>
    User
}
