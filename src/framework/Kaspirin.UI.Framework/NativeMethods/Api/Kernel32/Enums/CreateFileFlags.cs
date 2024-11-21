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
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum CreateFileFlags : uint
    {
        None = 0,
        FILE_ATTRIBUTE_READONLY = 0x1,
        FILE_ATTRIBUTE_HIDDEN = 0x2,
        FILE_ATTRIBUTE_SYSTEM = 0x4,
        FILE_ATTRIBUTE_ARCHIVE = 0x20,
        FILE_ATTRIBUTE_NORMAL = 0x80,
        FILE_ATTRIBUTE_TEMPORARY = 0x100,
        FILE_ATTRIBUTE_OFFLINE = 4096,
        FILE_ATTRIBUTE_ENCRYPTED = 0x4000,
        FILE_FLAG_OPEN_NO_RECALL = 0x00100000,
        FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000,
        FILE_FLAG_BACKUP_SEMANTICS = 0x02000000,
        FILE_FLAG_SESSION_AWARE = 0x00800000,
        FILE_FLAG_POSIX_SEMANTICS = 0x01000000,
        FILE_FLAG_DELETE_ON_CLOSE = 0x04000000,
        FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000,
        FILE_FLAG_RANDOM_ACCESS = 0x10000000,
        FILE_FLAG_NO_BUFFERING = 0x20000000,
        FILE_FLAG_OVERLAPPED = 0x40000000,
        FILE_FLAG_WRITE_THROUGH = 0x80000000
    }
}
