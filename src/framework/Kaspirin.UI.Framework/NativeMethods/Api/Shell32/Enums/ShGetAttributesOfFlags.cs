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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/shell/sfgao">Learn more</seealso>.
/// </summary>
[Flags]
public enum ShGetAttributesOfFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The SFGAO_CANCOPY constant.
    /// </summary>
    CanCopy = 0x00000001,

    /// <summary>
    ///     The SFGAO_CANMOVE constant.
    /// </summary>
    CanMove = 0x00000002,

    /// <summary>
    ///     The SFGAO_CANLINK constant.
    /// </summary>
    CanLink = 0x00000004,

    /// <summary>
    ///     The SFGAO_STORAGE constant.
    /// </summary>
    Storage = 0x00000008,

    /// <summary>
    ///     The SFGAO_CANRENAME constant.
    /// </summary>
    CanRename = 0x00000010,

    /// <summary>
    ///     The SFGAO_CANDELETE constant.
    /// </summary>
    CanDelete = 0x00000020,

    /// <summary>
    ///     The SFGAO_HASPROPSHEET constant.
    /// </summary>
    HasPropSheet = 0x00000040,

    /// <summary>
    ///     The SFGAO_DROPTARGET constant.
    /// </summary>
    DropTarget = 0x00000100,

    /// <summary>
    ///     The SFGAO_CAPABILITYMASK constant.
    /// </summary>
    CapabilityMask = 0x00000177,

    /// <summary>
    ///     The SFGAO_SYSTEM constant.
    /// </summary>
    System = 0x00001000,

    /// <summary>
    ///     The SFGAO_ENCRYPTED constant.
    /// </summary>
    Encrypted = 0x00002000,

    /// <summary>
    ///     The SFGAO_ISSLOW constant.
    /// </summary>
    IsSlow = 0x00004000,

    /// <summary>
    ///     The SFGAO_GHOSTED constant.
    /// </summary>
    Ghosted = 0x00008000,

    /// <summary>
    ///     The SFGAO_LINK constant.
    /// </summary>
    Link = 0x00010000,

    /// <summary>
    ///     The SFGAO_SHARE constant.
    /// </summary>
    Share = 0x00020000,

    /// <summary>
    ///     The SFGAO_READONLY constant.
    /// </summary>
    Readonly = 0x00040000,

    /// <summary>
    ///     The SFGAO_HIDDEN constant.
    /// </summary>
    Hidden = 0x00080000,

    /// <summary>
    ///     The SFGAO_DISPLAYATTRMASK constant.
    /// </summary>
    DisplayAttrMask = 0x000FC000,

    /// <summary>
    ///     The SFGAO_NONENUMERATED constant.
    /// </summary>
    NonEnumerated = 0x00100000,

    /// <summary>
    ///     The SFGAO_NEWCONTENT constant.
    /// </summary>
    NewContent = 0x00200000,

    /// <summary>
    ///     The SFGAO_STREAM constant.
    /// </summary>
    Stream = 0x00400000,

    /// <summary>
    ///     The SFGAO_STORAGEANCESTOR constant.
    /// </summary>
    StorageAncestor = 0x00800000,

    /// <summary>
    ///     The SFGAO_VALIDATE constant.
    /// </summary>
    Validate = 0x01000000,

    /// <summary>
    ///     The SFGAO_REMOVABLE constant.
    /// </summary>
    Removable = 0x02000000,

    /// <summary>
    ///     The SFGAO_COMPRESSED constant.
    /// </summary>
    Compressed = 0x04000000,

    /// <summary>
    ///     The SFGAO_BROWSABLE constant.
    /// </summary>
    Browsable = 0x08000000,

    /// <summary>
    ///     The SFGAO_FILESYSANCESTOR constant.
    /// </summary>
    FileSysAncestor = 0x10000000,

    /// <summary>
    ///     The SFGAO_FOLDER constant.
    /// </summary>
    Folder = 0x20000000,

    /// <summary>
    ///     The SFGAO_FILESYSTEM constant.
    /// </summary>
    FileSystem = 0x40000000,

    /// <summary>
    ///     The SFGAO_STORAGECAPMASK constant.
    /// </summary>
    StorageCapMask = 0x70C50008,

    /// <summary>
    ///     The SFGAO_HASSUBFOLDER constant.
    /// </summary>
    HasSubfolder = 0x80000000,

    /// <summary>
    ///     The SFGAO_CONTENTSMASK constant.
    /// </summary>
    ContentsMask = 0x80000000,

    /// <summary>
    ///     The SFGAO_PKEYSFGAOMASK constant.
    /// </summary>
    PKeySfgaoMask = 0x81044000,
}