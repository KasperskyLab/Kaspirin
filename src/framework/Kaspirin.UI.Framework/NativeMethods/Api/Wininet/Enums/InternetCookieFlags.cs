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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable KCAIDE0002 // Enum has incorrect suffix

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Wininet.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wininet/nf-wininet-internetsetcookieexw">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum InternetCookieFlags : uint
    {
        None = 0,
        INTERNET_COOKIE_THIRD_PARTY = 0x10,
        INTERNET_COOKIE_EVALUATE_P3P = 0x40,
        INTERNET_FLAG_RESTRICTED_ZONE = 0x200,
        INTERNET_COOKIE_HTTPONLY = 0x00002000
    }
}
