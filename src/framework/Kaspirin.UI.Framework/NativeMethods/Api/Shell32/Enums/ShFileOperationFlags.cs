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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shfileopstructa">Learn more</seealso>.
/// </summary>
[Flags]
public enum ShFileOperationFlags : ushort
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The FOF_MULTIDESTFILES constant.
    /// </summary>
    MultiDestFiles = 1,

    /// <summary>
    ///     The FOF_CONFIRMMOUSE constant.
    /// </summary>
    ConfirmMouse = 2,

    /// <summary>
    ///     The FOF_SILENT constant.
    /// </summary>
    Silent = 4,

    /// <summary>
    ///     The FOF_RENAMEONCOLLISION constant.
    /// </summary>
    RenameOnCollision = 8,

    /// <summary>
    ///     The FOF_NOCONFIRMATION constant.
    /// </summary>
    NoConfirmation = 16,

    /// <summary>
    ///     The FOF_WANTMAPPINGHANDLE constant.
    /// </summary>
    WantMappingHandle = 32,

    /// <summary>
    ///     The FOF_ALLOWUNDO constant.
    /// </summary>
    AllowUndo = 64,

    /// <summary>
    ///     The FOF_FILESONLY constant.
    /// </summary>
    FilesOnly = 128,

    /// <summary>
    ///     The FOF_SIMPLEPROGRESS constant.
    /// </summary>
    SimpleProgress = 256,

    /// <summary>
    ///     The FOF_NOCONFIRMMKDIR constant.
    /// </summary>
    NoConfirmMkdir = 512,

    /// <summary>
    ///     The FOF_NOERRORUI constant.
    /// </summary>
    NoErrorUi = 1024,

    /// <summary>
    ///     The FOF_NOCOPYSECURITYATTRIBS constant.
    /// </summary>
    NoCopySecurityAttributes = 2048,

    /// <summary>
    ///     The FOF_NORECURSION constant.
    /// </summary>
    NoRecursion = 4096,

    /// <summary>
    ///     The FOF_NO_CONNECTED_ELEMENTS constant.
    /// </summary>
    NoConnectedElements = 8192,

    /// <summary>
    ///     The FOF_WANTNUKEWARNING constant.
    /// </summary>
    WantNukeWarning = 16384,

    /// <summary>
    ///     The FOF_NORECURSEREPARSE constant.
    /// </summary>
    NoRecurseReparse = 32768,
}
