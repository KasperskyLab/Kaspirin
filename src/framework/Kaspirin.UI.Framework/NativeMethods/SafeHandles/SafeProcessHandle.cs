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
using System.IO;
using System.Security;
using System.Text;

namespace Kaspirin.UI.Framework.NativeMethods.SafeHandles
{
    /// <summary>
    ///     The process descriptor.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public sealed class SafeProcessHandle : SafeTokenHandle
    {
        [SecurityCritical]
        internal SafeProcessHandle() : base(true)
        {
        }

        /// <summary>
        ///     Retrieves the process descriptor by the process ID specified in <paramref name="ProcessID" />.
        /// </summary>
        /// <param name="processId">
        ///     The ID of the process.
        /// </param>
        /// <returns>
        ///     The process descriptor.
        /// </returns>
        public static SafeProcessHandle Open(int processId)
            => Kernel32Dll.OpenProcess(ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION, false, processId);

        /// <summary>
        ///     Gets the name of the process.
        /// </summary>
        /// <returns>
        ///     The name of the process, or <see langword="null" /> if the name could not be obtained.
        /// </returns>
        public string? QueryFullProcessImageName()
        {
            const string kernel32 = "kernel32.dll";
            const string procName = "QueryFullProcessImageNameW";

            using (var module = SafeDynamicModuleHandle.Open(Path.Combine(Environment.SystemDirectory, kernel32)))
            {
                if (module == null)
                {
                    return default;
                }

                var exeNameBuilder = new StringBuilder(PathConstants.MaxPathLength);
                var size = PathConstants.MaxPathLength;

                var isFuncAvailable = module.TryGetProc(procName, out QueryFullProcessImageName? queryFullProcessImageName);
                if (isFuncAvailable)
                {
                    var res = queryFullProcessImageName!(handle, 0, exeNameBuilder, ref size);
                    if (res && size <= PathConstants.MaxPathLength)
                    {
                        return exeNameBuilder.ToString();
                    }
                }

                return default;
            }
        }
    }
}
