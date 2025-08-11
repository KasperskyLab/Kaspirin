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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights">Learn more</seealso>.
/// </summary>
[Flags]
public enum ProcessAccessFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The PROCESS_TERMINATE constant.
    /// </summary>
    Terminate = 0x0001,

    /// <summary>
    ///     The PROCESS_CREATE_THREAD constant.
    /// </summary>
    CreateThread = 0x0002,

    /// <summary>
    ///     The PROCESS_VM_OPERATION constant.
    /// </summary>
    VmOperation = 0x0008,

    /// <summary>
    ///     The PROCESS_VM_READ constant.
    /// </summary>
    VmRead = 0x0010,

    /// <summary>
    ///     The PROCESS_VM_WRITE constant.
    /// </summary>
    VmWrite = 0x0020,

    /// <summary>
    ///     The PROCESS_DUP_HANDLE constant.
    /// </summary>
    DupHandle = 0x0040,

    /// <summary>
    ///     The PROCESS_CREATE_PROCESS constant.
    /// </summary>
    CreateProcess = 0x0080,

    /// <summary>
    ///     The PROCESS_SET_QUOTA constant.
    /// </summary>
    SetQuota = 0x0100,

    /// <summary>
    ///     The PROCESS_SET_INFORMATION constant.
    /// </summary>
    SetInformation = 0x0200,

    /// <summary>
    ///     The PROCESS_QUERY_INFORMATION constant.
    /// </summary>
    QueryInformation = 0x0400,

    /// <summary>
    ///     The PROCESS_SUSPEND_RESUME constant.
    /// </summary>
    SuspendResume = 0x0800,

    /// <summary>
    ///     The PROCESS_QUERY_LIMITED_INFORMATION constant.
    /// </summary>
    QueryLimitedInformation = 0x1000,
}