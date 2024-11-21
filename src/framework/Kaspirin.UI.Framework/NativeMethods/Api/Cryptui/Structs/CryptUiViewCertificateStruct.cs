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

using System;
using System.Runtime.InteropServices;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Cryptui.Structs
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/cryptuiapi/ns-cryptuiapi-cryptui_viewcertificate_structa">Learn more</seealso>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CryptUiViewCertificateStruct
    {
        public int Size;
        public IntPtr ParentHwnd;
        public CryptUiViewFlags Flags;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Title;
        public IntPtr CertificateContextPointer;
        public IntPtr PurposesArrayPointer;
        public int PurposesArrayLength;
        public IntPtr CryptProviderDataPointer;
        public bool CryptProviderDataTrustedUsage;
        public int SignerIndex;
        public int CertificateIndex;
        public bool CounterSigner;
        public int CounterSignerIndex;
        public int StoresArrayLength;
        public IntPtr StoresArrayPointer;
        public int PropertySheetPagesLength;
        public IntPtr PropertySheetPagesArrayPointer;
        public int StartPageIndex;
    }
}
