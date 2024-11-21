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
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getvolumeinformationw">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum FileSystemFlags : uint
    {
        None = 0,
        FILE_CASE_SENSITIVE_SEARCH = 0x00000001,
        FILE_CASE_PRESERVED_NAMES = 0x00000002,
        FILE_UNICODE_ON_DISK = 0x00000004,
        FILE_PERSISTENT_ACLS = 0x00000008,
        FILE_FILE_COMPRESSION = 0x00000010,
        FILE_VOLUME_QUOTAS = 0x00000020,
        FILE_SUPPORTS_SPARSE_FILES = 0x00000040,
        FILE_SUPPORTS_REPARSE_POINTS = 0x00000080,
        FILE_SUPPORTS_REMOTE_STORAGE = 0x00000100,
        FILE_RETURNS_CLEANUP_RESULT_INFO = 0x00000200,
        FILE_SUPPORTS_POSIX_UNLINK_RENAME = 0x00000400,
        FILE_VOLUME_IS_COMPRESSED = 0x00008000,
        FILE_SUPPORTS_OBJECT_IDS = 0x00010000,
        FILE_SUPPORTS_ENCRYPTION = 0x00020000,
        FILE_NAMED_STREAMS = 0x00040000,
        FILE_READ_ONLY_VOLUME = 0x00080000,
        FILE_SEQUENTIAL_WRITE_ONCE = 0x00100000,
        FILE_SUPPORTS_TRANSACTIONS = 0x00200000,
        FILE_SUPPORTS_HARD_LINKS = 0x00400000,
        FILE_SUPPORTS_EXTENDED_ATTRIBUTES = 0x00800000,
        FILE_SUPPORTS_OPEN_BY_FILE_ID = 0x01000000,
        FILE_SUPPORTS_USN_JOURNAL = 0x02000000,
        FILE_SUPPORTS_INTEGRITY_STREAMS = 0x04000000,
        FILE_SUPPORTS_BLOCK_REFCOUNTING = 0x08000000,
        FILE_SUPPORTS_SPARSE_VDL = 0x10000000,
        FILE_DAX_VOLUME = 0x20000000,
        FILE_SUPPORTS_GHOSTING = 0x40000000
    }
}