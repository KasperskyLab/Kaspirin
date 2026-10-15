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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Wininet.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wininet/nf-wininet-internetsetcookieexw">Learn more</seealso>.
/// </summary>
[Flags]
public enum InternetCookieFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The INTERNET_COOKIE_THIRD_PARTY constant.
    /// </summary>
    ThirdParty = 0x10,

    /// <summary>
    ///     The INTERNET_COOKIE_EVALUATE_P3P constant.
    /// </summary>
    EvaluateP3p = 0x40,

    /// <summary>
    ///     The INTERNET_FLAG_RESTRICTED_ZONE constant.
    /// </summary>
    RestrictedZone = 0x200,

    /// <summary>
    ///     The INTERNET_COOKIE_HTTPONLY constant.
    /// </summary>
    HttpOnly = 0x00002000
}
