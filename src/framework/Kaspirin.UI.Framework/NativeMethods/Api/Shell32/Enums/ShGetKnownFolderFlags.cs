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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/ne-shlobj_core-known_folder_flag">Learn more</seealso>.
/// </summary>
[Flags]
public enum ShGetKnownFolderFlags : uint
{
    /// <summary>
    ///     The KF_FLAG_DEFAULT constant.
    /// </summary>
    Default = 0x00000000,

    /// <summary>
    ///     The KF_FLAG_FORCE_APP_DATA_REDIRECTION constant.
    /// </summary>
    ForceAppDataRedirection = 0x00080000,

    /// <summary>
    ///     The KF_FLAG_RETURN_FILTER_REDIRECTION_TARGET constant.
    /// </summary>
    ReturnFilterRedirectionTarget = 0x00040000,

    /// <summary>
    ///     The KF_FLAG_FORCE_PACKAGE_REDIRECTION constant.
    /// </summary>
    ForcePackageRedirection = 0x00020000,

    /// <summary>
    ///     The KF_FLAG_NO_PACKAGE_REDIRECTION constant.
    /// </summary>
    NoPackageRedirection = 0x00010000,

    /// <summary>
    ///     The KF_FLAG_FORCE_APPCONTAINER_REDIRECTION constant.
    /// </summary>
    ForceAppContainerRedirection = 0x00020000,

    /// <summary>
    ///     The KF_FLAG_NO_APPCONTAINER_REDIRECTION constant.
    /// </summary>
    NoAppContainerRedirection = 0x00010000,

    /// <summary>
    ///     The KF_FLAG_CREATE constant.
    /// </summary>
    Create = 0x00008000,

    /// <summary>
    ///     The KF_FLAG_DONT_VERIFY constant.
    /// </summary>
    DontVerify = 0x00004000,

    /// <summary>
    ///     The KF_FLAG_DONT_UNEXPAND constant.
    /// </summary>
    DontUnexpand = 0x00002000,

    /// <summary>
    ///     The KF_FLAG_NO_ALIAS constant.
    /// </summary>
    NoAlias = 0x00001000,

    /// <summary>
    ///     The KF_FLAG_INIT constant.
    /// </summary>
    Init = 0x00000800,

    /// <summary>
    ///     The KF_FLAG_DEFAULT_PATH constant.
    /// </summary>
    DefaultPath = 0x00000400,

    /// <summary>
    ///     The KF_FLAG_NOT_PARENT_RELATIVE constant.
    /// </summary>
    NotParentRelative = 0x00000200,

    /// <summary>
    ///     The KF_FLAG_SIMPLE_IDLIST constant.
    /// </summary>
    SimpleIdList = 0x00000100,

    /// <summary>
    ///     The KF_FLAG_ALIAS_ONLY constant.
    /// </summary>
    AliasOnly = 0x80000000,
}
