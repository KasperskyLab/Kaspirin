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

#pragma warning disable SYSLIB0004 // The constrained execution region (CER) feature is not supported

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Kaspirin.UI.Framework.NativeMethods.SafeHandles
{
    /// <summary>
    ///     The icon descriptor.
    /// </summary>
    [SecurityCritical]
    public sealed class SafeIconHandle : SafeHandle
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SafeIconHandle" /> class.
        /// </summary>
        /// <param name="icon">
        ///     The icon pointer.
        /// </param>
        [SecurityCritical]
        public SafeIconHandle(IntPtr icon)
            : base(IntPtr.Zero, true)
        {
            SetHandle(icon);
        }

        /// <inheritdoc />
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <inheritdoc />
        [SecurityCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
            => IsInvalid || User32Dll.DestroyIcon(handle);
    }
}
