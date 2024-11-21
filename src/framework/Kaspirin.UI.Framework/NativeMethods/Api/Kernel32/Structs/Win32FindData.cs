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

using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Structs
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-win32_find_dataa">Learn more</seealso>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Win32FindData
    {
        public FileAttributeFlags FileAttributes;

        public FILETIME CreationTime;

        public FILETIME LastAccessTime;

        public FILETIME LastWriteTime;

        public uint FileSizeHigh;

        public uint FileSizeLow;

        public uint Reserved0;

        public uint Reserved1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PathConstants.MaxPathLength)]
        public string FileName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PathConstants.MaxAlternatePathLength)]
        public string Alternate;
    }
}