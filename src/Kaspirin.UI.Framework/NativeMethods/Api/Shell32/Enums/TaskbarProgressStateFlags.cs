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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-itaskbarlist3-setprogressstate">Learn more</seealso>.
/// </summary>
[Flags]
public enum TaskbarProgressStateFlags : uint
{
    /// <summary>
    ///     The TBPF_NOPROGRESS constant.
    /// </summary>
    NoProgress = 0x00000000,

    /// <summary>
    ///     The TBPF_INDETERMINATE constant.
    /// </summary>
    Indeterminate = 0x00000001,

    /// <summary>
    ///     The TBPF_NORMAL constant.
    /// </summary>
    Normal = 0x00000002,

    /// <summary>
    ///     The TBPF_ERROR constant.
    /// </summary>
    Error = 0x00000004,

    /// <summary>
    ///     The TBPF_PAUSED constant.
    /// </summary>
    Paused = 0x00000008,
}
