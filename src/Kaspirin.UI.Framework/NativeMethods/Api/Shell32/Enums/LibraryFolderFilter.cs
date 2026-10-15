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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/ne-shobjidl_core-libraryfolderfilter">Learn more</seealso>.
/// </summary>
public enum LibraryFolderFilter
{
    /// <summary>
    ///     The LFF_FORCEFILESYSTEM constant.
    /// </summary>
    ForceFileSystem = 1,

    /// <summary>
    ///     The LFF_STORAGEITEMS constant.
    /// </summary>
    StorageItems = 2,

    /// <summary>
    ///     The LFF_ALLITEMS constant.
    /// </summary>
    AllItems = 3,
}
