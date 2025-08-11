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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage">Learn more</seealso>.
/// </summary>
[Flags]
public enum FormatMessageFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The FORMAT_MESSAGE_ALLOCATE_BUFFER constant.
    /// </summary>
    AllocateBuffer = 0x00000100,

    /// <summary>
    ///     The FORMAT_MESSAGE_IGNORE_INSERTS constant.
    /// </summary>
    IgnoreInserts = 0x00000200,

    /// <summary>
    ///     The FORMAT_MESSAGE_FROM_STRING constant.
    /// </summary>
    FromString = 0x00000400,

    /// <summary>
    ///     The FORMAT_MESSAGE_FROM_HMODULE constant.
    /// </summary>
    FromHModule = 0x00000800,

    /// <summary>
    ///     The FORMAT_MESSAGE_FROM_SYSTEM constant.
    /// </summary>
    FromSystem = 0x00001000,

    /// <summary>
    ///     The FORMAT_MESSAGE_ARGUMENT_ARRAY constant.
    /// </summary>
    ArgumentArray = 0x00002000,

    /// <summary>
    ///     The FORMAT_MESSAGE_MAX_WIDTH_MASK constant.
    /// </summary>
    MaxWidthMask = 0x000000FF,
}
