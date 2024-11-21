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

#pragma warning disable CA1401 // P/Invokes should not be visible

using System;
using System.Runtime.InteropServices;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Ntdll
{
    /// <summary>
    ///     Provides API methods for functions from ntdll.dll .
    /// </summary>
    public static class NtdllDll
    {
        #region ntifs.h

        /// <summary>
        ///     The NtQueryInformationFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntifs/nf-ntifs-ntqueryinformationfile">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern NtStatus NtQueryInformationFile(
            IntPtr fileHandle,
            ref IoStatusBlock ioStatusBlock,
            IntPtr fileInformation,
            int lengthOfFileInformation,
            FileInformationType fileInformationClass);

        /// <summary>
        ///     The NtSetInformationFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntifs/nf-ntifs-ntsetinformationfile">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern NtStatus NtSetInformationFile(
            IntPtr fileHandle,
            ref IoStatusBlock ioStatusBlock,
            IntPtr fileInformation,
            int lengthOfFileInformation,
            FileInformationType fileInformationClass);

        #endregion

        private const string DllName = "ntdll.dll";
    }
}
