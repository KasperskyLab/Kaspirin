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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Interfaces
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-ishellfolder">Learn more</seealso>.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(ShlGuids.IidIShellFolder)]
    public interface IShellFolder
    {
        void ParseDisplayName(
            IntPtr hwnd,
            IntPtr pbc,
            string pszDisplayName,
            uint pchEaten,
            out IntPtr ppidl,
            ref ShGetAttributesOfFlags pdwAttributes);

        [PreserveSig]
        int EnumObjects(
            IntPtr hwnd,
            ShContentFolderFlags grfFlags,
            out IEnumIDList ppenumIdList);

        [PreserveSig]
        int BindToObject(
            IntPtr pidl,
            IntPtr pbc,
            [In] ref Guid riid,
            out IShellFolder ppv);

        void BindToStorage(
            IntPtr pidl,
            IntPtr pbc,
            [In] ref Guid riid,
            out IntPtr ppv);

        [PreserveSig]
        int CompareIDs(
            int lParam,
            IntPtr pidl1,
            IntPtr pidl2);

        void CreateViewObject(
            IntPtr hwndOwner,
            [In] ref Guid riid,
            out IntPtr ppv);

        void GetAttributesOf(
            uint cidl,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]
            IntPtr[] apidl,
            ref ShGetAttributesOfFlags rgfInOut);

        void GetUIObjectOf(IntPtr hwndOwner, uint cidl,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            IntPtr[] apidl,
            [In] ref Guid riid,
            uint rgfReserved,
            out IntPtr ppv);

        void GetDisplayNameOf(
            IntPtr pidl,
            ShGetDisplayNameOfFlags uFlags,
            out ShStringReturn pName);

        void SetNameOf(
            IntPtr hwnd,
            IntPtr pidl,
            string pszName,
            ShGetDisplayNameOfFlags uFlags,
            out IntPtr ppidlOut);
    }
}