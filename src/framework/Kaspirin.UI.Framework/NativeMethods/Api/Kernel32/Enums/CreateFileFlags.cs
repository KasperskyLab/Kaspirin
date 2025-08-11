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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew">Learn more</seealso>.
/// </summary>
[Flags]
public enum CreateFileFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,
    /// <summary>
    ///     The FILE_ATTRIBUTE_READONLY constant.
    /// </summary>
    AttributeReadonly = 0x1,

    /// <summary>
    ///     The FILE_ATTRIBUTE_HIDDEN constant.
    /// </summary>
    AttributeHidden = 0x2,

    /// <summary>
    ///     The FILE_ATTRIBUTE_SYSTEM constant.
    /// </summary>
    AttributeSystem = 0x4,

    /// <summary>
    ///     The FILE_ATTRIBUTE_ARCHIVE constant.
    /// </summary>
    AttributeArchive = 0x20,

    /// <summary>
    ///     The FILE_ATTRIBUTE_NORMAL constant.
    /// </summary>
    AttributeNormal = 0x80,

    /// <summary>
    ///     The FILE_ATTRIBUTE_TEMPORARY constant.
    /// </summary>
    AttributeTemporary = 0x100,

    /// <summary>
    ///     The FILE_ATTRIBUTE_OFFLINE constant.
    /// </summary>
    AttributeOffline = 4096,

    /// <summary>
    ///     The FILE_ATTRIBUTE_ENCRYPTED constant.
    /// </summary>
    AttributeEncrypted = 0x4000,

    /// <summary>
    ///     The FILE_FLAG_OPEN_NO_RECALL constant.
    /// </summary>
    FlagOpenNoRecall = 0x00100000,

    /// <summary>
    ///     The FILE_FLAG_OPEN_REPARSE_POINT constant.
    /// </summary>
    FlagOpenReparsePoint = 0x00200000,

    /// <summary>
    ///     The FILE_FLAG_BACKUP_SEMANTICS constant.
    /// </summary>
    FlagBackupSemantics = 0x02000000,

    /// <summary>
    ///     The FILE_FLAG_SESSION_AWARE constant.
    /// </summary>
    FlagSessionAware = 0x00800000,

    /// <summary>
    ///     The FILE_FLAG_POSIX_SEMANTICS constant.
    /// </summary>
    FlagPosixSemantics = 0x01000000,

    /// <summary>
    ///     The FILE_FLAG_DELETE_ON_CLOSE constant.
    /// </summary>
    FlagDeleteOnClose = 0x04000000,

    /// <summary>
    ///     The FILE_FLAG_SEQUENTIAL_SCAN constant.
    /// </summary>
    FlagSequentialScan = 0x08000000,

    /// <summary>
    ///     The FILE_FLAG_RANDOM_ACCESS constant.
    /// </summary>
    FlagRandomAccess = 0x10000000,

    /// <summary>
    ///     The FILE_FLAG_NO_BUFFERING constant.
    /// </summary>
    FlagNoBuffering = 0x20000000,

    /// <summary>
    ///     The FILE_FLAG_OVERLAPPED constant.
    /// </summary>
    FlagOverlapped = 0x40000000,

    /// <summary>
    ///     The FILE_FLAG_WRITE_THROUGH constant.
    /// </summary>
    FlagWriteThrough = 0x80000000,
}
