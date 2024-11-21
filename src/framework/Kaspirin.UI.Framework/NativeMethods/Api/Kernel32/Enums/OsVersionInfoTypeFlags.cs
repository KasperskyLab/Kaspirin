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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-verifyversioninfow">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum OsVersionInfoTypeFlags : uint
    {
        None = 0,
        VER_MINORVERSION = 0x1,
        VER_MAJORVERSION = 0x2,
        VER_BUILDNUMBER = 0x4,
        VER_PLATFORMID = 0x8,
        VER_SERVICEPACKMINOR = 0x10,
        VER_SERVICEPACKMAJOR = 0x20,
        VER_SUITENAME = 0x40,
        VER_PRODUCT_TYPE = 0x80
    }
}
