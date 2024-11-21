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
using System.Text;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32
{
    /// <summary>
    ///     Provides API methods for functions from shell32.dll .
    /// </summary>
    public static class Shell32Dll
    {
        #region shellapi.h

        /// <summary>
        ///     The ExtractIconEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-extracticonexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern uint ExtractIconEx(
            string fileName,
            int iconIndex,
            IntPtr[] iconLarge,
            IntPtr[] iconSmall,
            uint nIcons);

        /// <summary>
        ///     The SHGetImageList API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetimagelist">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int SHGetImageList(
            ShGetImageListSize iImageList,
            ref Guid riid,
            ref IImageList? ppv);

        /// <summary>
        ///     The SHGetFileInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetfileinfow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(
            string path,
            FileAttributeFlags fileAttributes,
            out ShFileInfo fileInfoBuffer,
            int fileInfoBufferSize,
            ShGetFileInfoFlags flags);

        /// <summary>
        ///     The SHGetFileInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetfileinfow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(
            IntPtr ppidl,
            FileAttributeFlags fileAttributes,
            out ShFileInfo fileInfoBuffer,
            int fileInfoBufferSize,
            ShGetFileInfoFlags flags);

        /// <summary>
        ///     The SHFileOperation API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shfileoperationw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern int SHFileOperation(
            ref ShFileOpStruct lpFileOp);

        /// <summary>
        ///     The SHQueryUserNotificationState API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shqueryusernotificationstate">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int SHQueryUserNotificationState(
            out ShQueryUserNotificationState state);

        #endregion

        #region shlobj_core.h

        /// <summary>
        ///     The SHGetDesktopFolder API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shgetdesktopfolder">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern int SHGetDesktopFolder(
            out IShellFolder shellFolder);

        /// <summary>
        ///     The ILCombine API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-ilcombine">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern IntPtr ILCombine(
            IntPtr itemIdList1,
            IntPtr itemIdList2);

        /// <summary>
        ///     The SHGetPathFromIDListEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shgetpathfromidlistex">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SHGetPathFromIDListEx(
            IntPtr pidl,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pathBuffer,
            int bufferSize,
            uint flags);

        /// <summary>
        ///     The SHGetKnownFolderIDList API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shgetknownfolderidlist">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SHGetKnownFolderIDList(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            ShGetKnownFolderFlags dwFlags,
            IntPtr hToken,
            out IntPtr ppidl);

        /// <summary>
        ///     The SHDefExtractIcon API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shdefextracticonw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern int SHDefExtractIcon(
            string pszIconFile,
            int iIndex,
            uint uFlags,
            out IntPtr phiconLarge,
            out IntPtr phiconSmall,
            uint nIconSize);

        #endregion

        #region shobjidl_core.h

        /// <summary>
        ///     The SHCreateItemFromIDList API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-shcreateitemfromidlist">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SHCreateItemFromIDList(
            IntPtr pidl,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            out IShellItem ppv);

        #endregion

        #region not documented

        /// <summary>
        ///     An undocumented API. Copies the user account image to a temporary directory and returns the
        ///     path or returns various paths related to the user's images.
        /// </summary>
        [DllImport(DllName, EntryPoint = "#261", CharSet = CharSet.Unicode)]
        public static extern int SHGetUserPicturePath(
            string userName,
            ShGetUserPicturePathFlags flags,
            StringBuilder picPath,
            int picPathLength);

        #endregion

        private const string DllName = "shell32.dll";
    }
}
