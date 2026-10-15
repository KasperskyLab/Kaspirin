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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-verifyversioninfow">Learn more</seealso>.
/// </summary>
[Flags]
public enum OsVersionInfoTypeFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The VER_MINORVERSION constant.
    /// </summary>
    MinorVersion = 0x1,

    /// <summary>
    ///     The VER_MAJORVERSION constant.
    /// </summary>
    MajorVersion = 0x2,

    /// <summary>
    ///     The VER_BUILDNUMBER constant.
    /// </summary>
    BuildNumber = 0x4,

    /// <summary>
    ///     The VER_PLATFORMID constant.
    /// </summary>
    PlatformId = 0x8,

    /// <summary>
    ///     The VER_SERVICEPACKMINOR constant.
    /// </summary>
    ServicePackMinor = 0x10,

    /// <summary>
    ///     The VER_SERVICEPACKMAJOR constant.
    /// </summary>
    ServicePackMajor = 0x20,

    /// <summary>
    ///     The VER_SUITENAME constant.
    /// </summary>
    SuiteName = 0x40,

    /// <summary>
    ///     The VER_PRODUCT_TYPE constant.
    /// </summary>
    ProductType = 0x80,
}
