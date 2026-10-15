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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetimagelist">Learn more</seealso>.
/// </summary>
public enum ShGetImageListSize : uint
{
    /// <summary>
    ///     The SHIL_LARGE constant.
    /// </summary>
    Large = 0x000000000,

    /// <summary>
    ///     The SHIL_SMALL constant.
    /// </summary>
    Small = 0x000000001,

    /// <summary>
    ///     The SHIL_EXTRALARGE constant.
    /// </summary>
    ExtraLarge = 0x000000002,

    /// <summary>
    ///     The SHIL_SYSSMALL constant.
    /// </summary>
    SysSmall = 0x000000003,

    /// <summary>
    ///     The SHIL_JUMBO constant.
    /// </summary>
    Jumbo = 0x000000004,
}
