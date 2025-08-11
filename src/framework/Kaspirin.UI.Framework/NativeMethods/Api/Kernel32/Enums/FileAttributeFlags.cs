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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/fileio/file-attribute-constants">Learn more</seealso>.
/// </summary>
[Flags]
public enum FileAttributeFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The FILE_ATTRIBUTE_READONLY constant.
    /// </summary>
    ReadOnly = 0x00000001,

    /// <summary>
    ///     The FILE_ATTRIBUTE_HIDDEN constant.
    /// </summary>
    Hidden = 0x00000002,

    /// <summary>
    ///     The FILE_ATTRIBUTE_SYSTEM constant.
    /// </summary>
    System = 0x00000004,

    /// <summary>
    ///     The FILE_ATTRIBUTE_DIRECTORY constant.
    /// </summary>
    Directory = 0x00000010,

    /// <summary>
    ///     The FILE_ATTRIBUTE_ARCHIVE constant.
    /// </summary>
    Archive = 0x00000020,

    /// <summary>
    ///     The FILE_ATTRIBUTE_DEVICE constant.
    /// </summary>
    Device = 0x00000040,

    /// <summary>
    ///     The FILE_ATTRIBUTE_NORMAL constant.
    /// </summary>
    Normal = 0x00000080,

    /// <summary>
    ///     The FILE_ATTRIBUTE_TEMPORARY constant.
    /// </summary>
    Temporary = 0x00000100,

    /// <summary>
    ///     The FILE_ATTRIBUTE_SPARSE_FILE constant.
    /// </summary>
    SparseFile = 0x00000200,

    /// <summary>
    ///     The FILE_ATTRIBUTE_REPARSE_POINT constant.
    /// </summary>
    ReparsePoint = 0x00000400,

    /// <summary>
    ///     The FILE_ATTRIBUTE_COMPRESSED constant.
    /// </summary>
    Compressed = 0x00000800,

    /// <summary>
    ///     The FILE_ATTRIBUTE_OFFLINE constant.
    /// </summary>
    Offline = 0x00001000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_NOT_CONTENT_INDEXED constant.
    /// </summary>
    NotContentIndexed = 0x00002000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_ENCRYPTED constant.
    /// </summary>
    Encrypted = 0x00004000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_INTEGRITY_STREAM constant.
    /// </summary>
    IntegrityStream = 0x00008000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_VIRTUAL constant.
    /// </summary>
    Virtual = 0x00010000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_NO_SCRUB_DATA constant.
    /// </summary>
    NoScrubData = 0x00020000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_EA constant.
    /// </summary>
    Ea = 0x00040000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_PINNED constant.
    /// </summary>
    Pinned = 0x00080000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_UNPINNED constant.
    /// </summary>
    Unpinned = 0x00100000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_RECALL_ON_OPEN constant.
    /// </summary>
    RecallOnOpen = 0x00040000,

    /// <summary>
    ///     The FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS constant.
    /// </summary>
    RecallOnDataAccess = 0x00400000,
}
