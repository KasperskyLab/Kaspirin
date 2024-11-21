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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/ne-shobjidl_core-_shcontf">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum ShContentFolderFlags : uint
    {
        None = 0,
        SHCONTF_CHECKING_FOR_CHILDREN = 0x10,
        SHCONTF_FOLDERS = 0x20,
        SHCONTF_NONFOLDERS = 0x40,
        SHCONTF_INCLUDEHIDDEN = 0x80,
        SHCONTF_INIT_ON_FIRST_NEXT = 0x100,
        SHCONTF_NETPRINTERSRCH = 0x200,
        SHCONTF_SHAREABLE = 0x400,
        SHCONTF_STORAGE = 0x800,
        SHCONTF_NAVIGATION_ENUM = 0x1000,
        SHCONTF_FASTITEMS = 0x2000,
        SHCONTF_FLATLIST = 0x4000,
        SHCONTF_ENABLE_ASYNC = 0x8000,
        SHCONTF_INCLUDESUPERHIDDEN = 0x10000,
    }
}