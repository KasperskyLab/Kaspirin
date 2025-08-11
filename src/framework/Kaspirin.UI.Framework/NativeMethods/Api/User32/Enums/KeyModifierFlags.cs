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

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey">Learn more</seealso>.
/// </summary>
[Flags]
public enum KeyModifierFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The MOD_ALT constant.
    /// </summary>
    Alt = 1,

    /// <summary>
    ///     The MOD_CONTROL constant.
    /// </summary>
    Control = 2,

    /// <summary>
    ///     The MOD_SHIFT constant.
    /// </summary>
    Shift = 4,

    /// <summary>
    ///     The MOD_WIN constant.
    /// </summary>
    Win = 8,
}
