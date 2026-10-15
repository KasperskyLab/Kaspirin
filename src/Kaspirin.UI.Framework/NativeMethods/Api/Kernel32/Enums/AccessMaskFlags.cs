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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/secauthz/access-mask">Learn more</seealso>.
/// </summary>
[Flags]
public enum AccessMaskFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The GENERIC_READ constant.
    /// </summary>
    GenericRead = 0x80000000,

    /// <summary>
    ///     The GENERIC_WRITE constant.
    /// </summary>
    GenericWrite = 0x40000000,

    /// <summary>
    ///     The GENERIC_EXECUTE constant.
    /// </summary>
    GenericExecute = 0x20000000,

    /// <summary>
    ///     The GENERIC_ALL constant.
    /// </summary>
    GenericAll = 0x10000000,

    /// <summary>
    ///     The DELETE constant.
    /// </summary>
    Delete = 0x00010000,

    /// <summary>
    ///     The READ_CONTROL constant.
    /// </summary>
    ReadControl = 0x00020000,

    /// <summary>
    ///     The WRITE_DAC constant.
    /// </summary>
    WriteDac = 0x00040000,

    /// <summary>
    ///     The WRITE_OWNER constant.
    /// </summary>
    WriteOwner = 0x00080000,

    /// <summary>
    ///     The SYNCHRONIZE constant.
    /// </summary>
    Synchronize = 0x00100000,

    //CreateFile flags

    /// <summary>
    ///     The FILE_LIST_DIRECTORY constant.
    /// </summary>
    FileListDirectory = 0x00000001,

    /// <summary>
    ///     The FILE_WRITE_DATA constant.
    /// </summary>
    FileWriteData = 0x00000002,

    /// <summary>
    ///     The FILE_ADD_FILE constant.
    /// </summary>
    FileAddFile = 0x00000002,

    /// <summary>
    ///     The FILE_ADD_SUBDIRECTORY constant.
    /// </summary>
    FileAddSubdirectory = 0x00000004,

    /// <summary>
    ///     The FILE_TRAVERSE constant.
    /// </summary>
    FileTraverse = 0x00000020,

    /// <summary>
    ///     The FILE_WRITE_ATTRIBUTES constant.
    /// </summary>
    FileWriteAttributes = 0x00000100,
}