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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum FormatMessageFlags : uint
    {
        None = 0,
        FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
        FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,
        FORMAT_MESSAGE_FROM_STRING = 0x00000400,
        FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,
        FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
        FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000,
        FORMAT_MESSAGE_MAX_WIDTH_MASK = 0x000000FF
    }
}
