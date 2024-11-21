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
using System.Security.Principal;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Advapi32
{
    /// <summary>
    ///     Provides API methods for functions from advapi32.dll .
    /// </summary>
    public static class Advapi32Dll
    {
        #region wincred.h

        /// <summary>
        ///     The CredRead API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincred/nf-wincred-credreadw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredRead(
            string target,
            CredentialType type,
            CredentialReadFlags flags,
            out IntPtr credentials);

        /// <summary>
        ///     The CredFree API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincred/nf-wincred-credfree">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, EntryPoint = "CredFree", SetLastError = false)]
        public static extern bool CredFree(
            [In] IntPtr credentials);

        /// <summary>
        ///     The CredWrite API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincred/nf-wincred-credwritew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredWrite(
            [In] ref Credential credentials,
            [In] CredentialWriteFlags flags);

        #endregion

        #region winreg.h

        /// <summary>
        ///     The RegNotifyChangeKeyValue API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regnotifychangekeyvalue">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = false)]
        public static extern int RegNotifyChangeKeyValue(
            IntPtr regKey,
            bool watchSubtree,
            RegNotifyChangeKeyValueFlags filter,
            IntPtr notification,
            bool isAsynchronous);

        #endregion

        #region securitybaseapi.h

        /// <summary>
        ///     The DuplicateToken API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-duplicatetoken">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DuplicateToken(
            IntPtr existingTokenHandle,
            [MarshalAs(UnmanagedType.U4)] TokenImpersonationLevel level,
            out int duplicateTokenHandle);

        /// <summary>
        ///     The AccessCheck API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-accesscheck">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AccessCheck(
            [MarshalAs(UnmanagedType.LPArray)] byte[] securityDescriptor,
            IntPtr clientToken,
            [MarshalAs(UnmanagedType.U4)] TokenAccessLevels accessMask,
            [In] ref GenericMapping genericMapping,
            ref PrivilegeSet privilegeSet,
            ref int privilegeSetLength,
            out AccessMaskFlags grantedAccess,
            [MarshalAs(UnmanagedType.Bool)] out bool accessStatus);

        /// <summary>
        ///     The MapGenericMask API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-mapgenericmask">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = false)]
        public static extern void MapGenericMask(
            [In, MarshalAs(UnmanagedType.U4)] ref AccessMaskFlags accessMask,
            [In] ref GenericMapping map);

        /// <summary>
        ///     The GetFileSecurity API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-getfilesecurityw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetFileSecurity(
            string fileName,
            uint requestedInformation,
            byte[]? securityDescriptor,
            uint securityDescriptorLength,
            out uint securityDescriptorLengthNeeded);

        #endregion

        #region processthreadsapi.h

        /// <summary>
        ///     The OpenProcessToken API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocesstoken">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(
            IntPtr processHandle,
            TokenAccessFlags desiredAccess,
            out IntPtr tokenHandle);

        #endregion

        private const string DllName = "advapi32.dll";
    }
}
