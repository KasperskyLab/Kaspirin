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

using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace Kaspirin.UI.Framework.NativeMethods.SafeHandles
{
    /// <summary>
    ///     The module descriptor.
    /// </summary>
    /// <remarks>
    ///     Allows you to dynamically load and unload libraries and get function pointers from them.
    /// </remarks>
    [SuppressUnmanagedCodeSecurity]
    public sealed class SafeDynamicModuleHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal SafeDynamicModuleHandle() : base(true)
        {
        }

        /// <summary>
        ///     Retrieves the module descriptor at the specified path.
        /// </summary>
        /// <param name="modulePath">
        ///     The path to the module. The module must be loaded using the full path.
        /// </param>
        /// <returns>
        ///     The module descriptor.
        /// </returns>
        public static SafeDynamicModuleHandle? Open(string modulePath)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(modulePath);

            var isFullPath = string.Equals(Path.GetFullPath(modulePath), modulePath, StringComparison.OrdinalIgnoreCase);

            Guard.Assert(isFullPath, "Dynamic module must be loaded by full path.");

            return File.Exists(modulePath)
                ? Kernel32Dll.LoadLibrary(modulePath)
                : null;
        }

        /// <summary>
        ///     Gets a delegate by the name of the function in the module.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of delegate.
        /// </typeparam>
        /// <param name="procName">
        ///     The name of the function.
        /// </param>
        /// <returns>
        ///     The delegate of the function, or <see langword="null" /> if the specified function is not found in the module.
        /// </returns>
        public T? GetProc<T>(string procName) where T : Delegate
        {
            var procAddress = Kernel32Dll.GetProcAddress(handle, procName);
            if (procAddress == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T)) as T;
        }

        /// <summary>
        ///     Checks for the presence of a function in the module by the specified name, and returns a delegate
        ///     to it, if there is such a function.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of delegate.
        /// </typeparam>
        /// <param name="procName">
        ///     The name of the function.
        /// </param>
        /// <param name="proc">
        ///     The delegate of the function, or <see langword="null" /> if the specified function is not found in the module.
        /// </param>
        /// <returns>
        ///     
        /// <see langword="true" /> if the function is found, otherwise - <see langword="false" />.
        /// </returns>
        public bool TryGetProc<T>(string procName, [NotNullWhen(true)] out T? proc) where T : Delegate
        {
            proc = GetProc<T>(procName);

            return proc != null;
        }

        /// <inheritdoc />
        protected override bool ReleaseHandle()
        {
            return Kernel32Dll.FreeLibrary(handle);
        }
    }
}
