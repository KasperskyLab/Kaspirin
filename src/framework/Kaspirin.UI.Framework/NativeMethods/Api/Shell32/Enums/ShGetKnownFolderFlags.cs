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
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/ne-shlobj_core-known_folder_flag">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum ShGetKnownFolderFlags : uint
    {
        KF_FLAG_DEFAULT = 0x00000000,
        KF_FLAG_FORCE_APP_DATA_REDIRECTION = 0x00080000,
        KF_FLAG_RETURN_FILTER_REDIRECTION_TARGET = 0x00040000,
        KF_FLAG_FORCE_PACKAGE_REDIRECTION = 0x00020000,
        KF_FLAG_NO_PACKAGE_REDIRECTION = 0x00010000,
        KF_FLAG_FORCE_APPCONTAINER_REDIRECTION = 0x00020000,
        KF_FLAG_NO_APPCONTAINER_REDIRECTION = 0x00010000,
        KF_FLAG_CREATE = 0x00008000,
        KF_FLAG_DONT_VERIFY = 0x00004000,
        KF_FLAG_DONT_UNEXPAND = 0x00002000,
        KF_FLAG_NO_ALIAS = 0x00001000,
        KF_FLAG_INIT = 0x00000800,
        KF_FLAG_DEFAULT_PATH = 0x00000400,
        KF_FLAG_NOT_PARENT_RELATIVE = 0x00000200,
        KF_FLAG_SIMPLE_IDLIST = 0x00000100,
        KF_FLAG_ALIAS_ONLY = 0x80000000
    }
}
