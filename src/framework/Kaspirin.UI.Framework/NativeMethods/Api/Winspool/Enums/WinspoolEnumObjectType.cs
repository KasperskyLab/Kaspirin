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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Winspool.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/printdocs/enumprinters">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum WinspoolEnumObjectType
    {
        Default = 0x00000001,
        Local = 0x00000002,
        Connections = 0x00000004,
        Favorite = 0x00000004,
        Name = 0x00000008,
        Remote = 0x00000010,
        Shared = 0x00000020,
        Network = 0x00000040,
        Expand = 0x00004000,
        Container = 0x00008000,
        IconMask = 0x00ff0000,
        Icon1 = 0x00010000,
        Icon2 = 0x00020000,
        Icon3 = 0x00040000,
        Icon4 = 0x00080000,
        Icon5 = 0x00100000,
        Icon6 = 0x00200000,
        Icon7 = 0x00400000,
        Icon8 = 0x00800000,
        Hide = 0x01000000
    }
}
