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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Advapi32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/secauthz/access-rights-for-access-token-objects">Learn more</seealso>.
/// </summary>
[Flags]
public enum TokenAccessFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The STANDARD_RIGHTS_REQUIRED constant.
    /// </summary>
    StandardRightsRequired = 0x000F0000,

    /// <summary>
    ///     The STANDARD_RIGHTS_READ constant.
    /// </summary>
    StandardRightsRead = 0x00020000,

    /// <summary>
    ///     The TOKEN_ASSIGN_PRIMARY constant.
    /// </summary>
    AssignPrimary = 0x0001,

    /// <summary>
    ///     The TOKEN_DUPLICATE constant.
    /// </summary>
    Duplicate = 0x0002,

    /// <summary>
    ///     The TOKEN_IMPERSONATE constant.
    /// </summary>
    Impersonate = 0x0004,

    /// <summary>
    ///     The TOKEN_QUERY constant.
    /// </summary>
    Query = 0x0008,

    /// <summary>
    ///     The TOKEN_QUERY_SOURCE constant.
    /// </summary>
    QuerySource = 0x0010,

    /// <summary>
    ///     The TOKEN_ADJUST_PRIVILEGES constant.
    /// </summary>
    AdjustPrivileges = 0x0020,

    /// <summary>
    ///     The TOKEN_ADJUST_GROUPS constant.
    /// </summary>
    AdjustGroups = 0x0040,

    /// <summary>
    ///     The TOKEN_READ constant.
    /// </summary>
    Read = StandardRightsRead | Query,
}
