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
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum ProcessAccessFlags : uint
    {
        PROCESS_TERMINATE = 0x0001,
        PROCESS_CREATE_THREAD = 0x0002,
        PROCESS_VM_OPERATION = 0x0008,
        PROCESS_VM_READ = 0x0010,
        PROCESS_VM_WRITE = 0x0020,
        PROCESS_DUP_HANDLE = 0x0040,
        PROCESS_CREATE_PROCESS = 0x0080,
        PROCESS_SET_QUOTA = 0x0100,
        PROCESS_SET_INFORMATION = 0x0200,
        PROCESS_QUERY_INFORMATION = 0x0400,
        PROCESS_SUSPEND_RESUME = 0x0800,
        PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,
    }
}