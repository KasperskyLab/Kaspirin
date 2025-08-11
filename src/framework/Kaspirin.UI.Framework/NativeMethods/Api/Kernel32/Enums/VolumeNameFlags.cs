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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfinalpathnamebyhandlew">Learn more</seealso>.
/// </summary>
[Flags]
public enum VolumeNameFlags : uint
{
    /// <summary>
    ///     The VOLUME_NAME_DOS constant.
    /// </summary>
    Dos = 0x0,

    /// <summary>
    ///     The VOLUME_NAME_GUID constant.
    /// </summary>
    Guid = 0x1,

    /// <summary>
    ///     The VOLUME_NAME_NT constant.
    /// </summary>
    Nt = 0x2,

    /// <summary>
    ///     The VOLUME_NAME_NONE constant.
    /// </summary>
    None = 0x4,
}
