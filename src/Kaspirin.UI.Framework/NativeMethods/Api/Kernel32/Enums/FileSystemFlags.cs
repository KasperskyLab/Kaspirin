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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getvolumeinformationw">Learn more</seealso>.
/// </summary>
[Flags]
public enum FileSystemFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The FILE_CASE_SENSITIVE_SEARCH constant.
    /// </summary>
    CaseSensitiveSearch = 0x00000001,

    /// <summary>
    ///     The FILE_CASE_PRESERVED_NAMES constant.
    /// </summary>
    CasePreservedNames = 0x00000002,

    /// <summary>
    ///     The FILE_UNICODE_ON_DISK constant.
    /// </summary>
    UnicodeOnDisk = 0x00000004,

    /// <summary>
    ///     The FILE_PERSISTENT_ACLS constant.
    /// </summary>
    PersistentAcls = 0x00000008,

    /// <summary>
    ///     The FILE_FILE_COMPRESSION constant.
    /// </summary>
    FileCompression = 0x00000010,

    /// <summary>
    ///     The FILE_VOLUME_QUOTAS constant.
    /// </summary>
    VolumeQuotas = 0x00000020,

    /// <summary>
    ///     The FILE_SUPPORTS_SPARSE_FILES constant.
    /// </summary>
    SupportsSparseFiles = 0x00000040,

    /// <summary>
    ///     The FILE_SUPPORTS_REPARSE_POINTS constant.
    /// </summary>
    SupportsReparsePoints = 0x00000080,

    /// <summary>
    ///     The FILE_SUPPORTS_REMOTE_STORAGE constant.
    /// </summary>
    SupportsRemoteStorage = 0x00000100,

    /// <summary>
    ///     The FILE_RETURNS_CLEANUP_RESULT_INFO constant.
    /// </summary>
    ReturnsCleanupResultInfo = 0x00000200,

    /// <summary>
    ///     The FILE_SUPPORTS_POSIX_UNLINK_RENAME constant.
    /// </summary>
    SupportsPosixUnlinkRename = 0x00000400,

    /// <summary>
    ///     The FILE_VOLUME_IS_COMPRESSED constant.
    /// </summary>
    VolumeIsCompressed = 0x00008000,

    /// <summary>
    ///     The FILE_SUPPORTS_OBJECT_IDS constant.
    /// </summary>
    SupportsObjectIds = 0x00010000,

    /// <summary>
    ///     The FILE_SUPPORTS_ENCRYPTION constant.
    /// </summary>
    SupportsEncryption = 0x00020000,

    /// <summary>
    ///     The FILE_NAMED_STREAMS constant.
    /// </summary>
    NamedStreams = 0x00040000,

    /// <summary>
    ///     The FILE_READ_ONLY_VOLUME constant.
    /// </summary>
    ReadOnlyVolume = 0x00080000,

    /// <summary>
    ///     The FILE_SEQUENTIAL_WRITE_ONCE constant.
    /// </summary>
    SequentialWriteOnce = 0x00100000,

    /// <summary>
    ///     The FILE_SUPPORTS_TRANSACTIONS constant.
    /// </summary>
    SupportsTransactions = 0x00200000,

    /// <summary>
    ///     The FILE_SUPPORTS_HARD_LINKS constant.
    /// </summary>
    SupportsHardLinks = 0x00400000,

    /// <summary>
    ///     The FILE_SUPPORTS_EXTENDED_ATTRIBUTES constant.
    /// </summary>
    SupportsExtendedAttributes = 0x00800000,

    /// <summary>
    ///     The FILE_SUPPORTS_OPEN_BY_FILE_ID constant.
    /// </summary>
    SupportsOpenByFileId = 0x01000000,

    /// <summary>
    ///     The FILE_SUPPORTS_USN_JOURNAL constant.
    /// </summary>
    SupportsUsnJournal = 0x02000000,

    /// <summary>
    ///     The FILE_SUPPORTS_INTEGRITY_STREAMS constant.
    /// </summary>
    SupportsIntegrityStreams = 0x04000000,

    /// <summary>
    ///     The FILE_SUPPORTS_BLOCK_REFCOUNTING constant.
    /// </summary>
    SupportsBlockRefCounting = 0x08000000,

    /// <summary>
    ///     The FILE_SUPPORTS_SPARSE_VDL constant.
    /// </summary>
    SupportsSparseVdl = 0x10000000,

    /// <summary>
    ///     The FILE_DAX_VOLUME constant.
    /// </summary>
    DaxVolume = 0x20000000,

    /// <summary>
    ///     The FILE_SUPPORTS_GHOSTING constant.
    /// </summary>
    SupportsGhosting = 0x40000000
}