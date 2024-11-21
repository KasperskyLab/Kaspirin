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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Structs
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfoexw">Learn more</seealso>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct OsVersionInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public int OsVersionInfoSize;

        [MarshalAs(UnmanagedType.U4)]
        public int MajorVersion;

        [MarshalAs(UnmanagedType.U4)]
        public int MinorVersion;

        [MarshalAs(UnmanagedType.U4)]
        public int BuildNumber;

        [MarshalAs(UnmanagedType.U4)]
        public int PlatformId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string CsdVersion;

        [MarshalAs(UnmanagedType.U2)]
        public short ServicePackMajor;

        [MarshalAs(UnmanagedType.U2)]
        public short ServicePackMinor;

        [MarshalAs(UnmanagedType.U2)]
        public short SuiteMask;

        [MarshalAs(UnmanagedType.U1)]
        public OsProductType ProductType;

        public byte Reserved;
    }
}