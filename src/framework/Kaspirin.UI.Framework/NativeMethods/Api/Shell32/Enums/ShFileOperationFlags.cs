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
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shfileopstructa">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum ShFileOperationFlags : ushort
    {
        None = 0,
        FOF_MULTIDESTFILES = 1,
        FOF_CONFIRMMOUSE = 2,
        FOF_SILENT = 4,
        FOF_RENAMEONCOLLISION = 8,
        FOF_NOCONFIRMATION = 16,
        FOF_WANTMAPPINGHANDLE = 32,
        FOF_ALLOWUNDO = 64,
        FOF_FILESONLY = 128,
        FOF_SIMPLEPROGRESS = 256,
        FOF_NOCONFIRMMKDIR = 512,
        FOF_NOERRORUI = 1024,
        FOF_NOCOPYSECURITYATTRIBS = 2048,
        FOF_NORECURSION = 4096,
        FOF_NO_CONNECTED_ELEMENTS = 8192,
        FOF_WANTNUKEWARNING = 16384,
        FOF_NORECURSEREPARSE = 32768,
    }
}
