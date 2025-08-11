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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/ne-shobjidl_core-_shcontf">Learn more</seealso>.
/// </summary>
[Flags]
public enum ShContentFolderFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The SHCONTF_CHECKING_FOR_CHILDREN constant.
    /// </summary>
    CheckingForChildren = 0x10,

    /// <summary>
    ///     The SHCONTF_FOLDERS constant.
    /// </summary>
    Folders = 0x20,

    /// <summary>
    ///     The SHCONTF_NONFOLDERS constant.
    /// </summary>
    NonFolders = 0x40,

    /// <summary>
    ///     The SHCONTF_INCLUDEHIDDEN constant.
    /// </summary>
    IncludeHidden = 0x80,

    /// <summary>
    ///     The SHCONTF_INIT_ON_FIRST_NEXT constant.
    /// </summary>
    InitOnFirstNext = 0x100,

    /// <summary>
    ///     The SHCONTF_NETPRINTERSRCH constant.
    /// </summary>
    NetPrinterSearch = 0x200,

    /// <summary>
    ///     The SHCONTF_SHAREABLE constant.
    /// </summary>
    Shareable = 0x400,

    /// <summary>
    ///     The SHCONTF_STORAGE constant.
    /// </summary>
    Storage = 0x800,

    /// <summary>
    ///     The SHCONTF_NAVIGATION_ENUM constant.
    /// </summary>
    NavigationEnum = 0x1000,

    /// <summary>
    ///     The SHCONTF_FASTITEMS constant.
    /// </summary>
    FastItems = 0x2000,

    /// <summary>
    ///     The SHCONTF_FLATLIST constant.
    /// </summary>
    FlatList = 0x4000,

    /// <summary>
    ///     The SHCONTF_ENABLE_ASYNC constant.
    /// </summary>
    EnableAsync = 0x8000,

    /// <summary>
    ///     The SHCONTF_INCLUDESUPERHIDDEN constant.
    /// </summary>
    IncludeSuperHidden = 0x10000,
}