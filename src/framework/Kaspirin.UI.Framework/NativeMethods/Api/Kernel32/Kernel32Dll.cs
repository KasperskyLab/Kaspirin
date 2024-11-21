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
using System.IO;

using SafeWaitHandle = Microsoft.Win32.SafeHandles.SafeWaitHandle;
using SafeProcessHandle = Kaspirin.UI.Framework.NativeMethods.SafeHandles.SafeProcessHandle;
using SafeFileHandle = Microsoft.Win32.SafeHandles.SafeFileHandle;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32
{
    /// <summary>
    ///     Provides API methods for functions from kernel32.dll .
    /// </summary>
    public static class Kernel32Dll
    {
        #region fileapi.h

        /// <summary>
        ///     The CreateFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            AccessMaskFlags desiredAccess,
            FileShare shareMode,
            IntPtr securityAttributes,
            FileMode fileMode,
            CreateFileFlags flagsAndAttributes,
            IntPtr templateFile);

        /// <summary>
        ///     The DeleteFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-deletefilew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(
            string fileName);

        /// <summary>
        ///     The ReadFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-readfile">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool ReadFile(
            IntPtr file,
            [Out] byte[] buffer,
            uint numberOfBytesToRead,
            out uint numberOfBytesRead,
            IntPtr overlapped);

        /// <summary>
        ///     The GetFileAttributes API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfileattributesw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetFileAttributes(
            string fileName);

        /// <summary>
        ///     The SetFileAttributes API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-setfileattributesw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetFileAttributes(
            string fileName,
            FileAttributeFlags fileAttributes);

        /// <summary>
        ///     API method GetFileAttributesEx. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfileattributesexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode, BestFitMapping = false)]
        public static extern bool GetFileAttributesEx(
            string name,
            FileAttributesInfoLevel fileInfoLevel,
            ref Win32FileAttributeData lpFileInformation);

        /// <summary>
        ///     The RemoveDirectory API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-removedirectoryw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveDirectory(
            string pathName);

        /// <summary>
        ///     The CreateDirectory API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createdirectoryw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreateDirectory(
            string pathName,
            IntPtr securityAttributes);

        /// <summary>
        ///     The FindFirstFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findfirstfilew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFindHandle FindFirstFile(
            string fileName,
            out Win32FindData findFileData);

        /// <summary>
        ///     The FindNextFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findnextfilew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNextFile(
            SafeFindHandle findFile,
            out Win32FindData findFileData);

        /// <summary>
        ///     The FindClose API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findclose">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClose(
            IntPtr findFile);

        /// <summary>
        ///     API method GetVolumePathName. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getvolumepathnamew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool GetVolumePathName(
            string filePath,
            [Out] StringBuilder volumePath,
            uint bufferLength);

        /// <summary>
        ///     API method GetFullPathName. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfullpathnamew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetFullPathName(
            string lpFileName,
            uint bufferLength,
            StringBuilder lpBuffer,
            IntPtr lpFilePart);

        /// <summary>
        ///     The GetLongPathName API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getlongpathnamew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode, BestFitMapping = false)]
        public static extern int GetLongPathName(
            string path,
            [Out] StringBuilder longPathBuffer,
            int bufferLength);

        /// <summary>
        ///     The GetVolumeInformation API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getvolumeinformationw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool GetVolumeInformation(
            string rootPath,
            StringBuilder volumeNameBuffer,
            int volumeNameSize,
            out uint volumeSerialNumber,
            out uint maximumComponentLength,
            out FileSystemFlags flags,
            StringBuilder fileSystemNameBuffer,
            int fileSystemNameSize);

        /// <summary>
        ///     The GetFinalPathNameByHandle API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfinalpathnamebyhandlew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetFinalPathNameByHandle(
            IntPtr fileHandle,
            StringBuilder filePath,
            uint filePathSize,
            VolumeNameFlags flags);

        /// <summary>
        ///     The QueryDosDevice API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-querydosdevicew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint QueryDosDevice(
            string deviceName,
            StringBuilder targetPath,
            int bufferLength);

        #endregion

        #region sysinfoapi.h

        /// <summary>
        ///     The GetVersionEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getversionexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetVersionEx(
            ref OsVersionInfo osVersionInfo);

        /// <summary>
        ///     The GlobalMemoryStatusEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-globalmemorystatusex">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx(
            ref MemoryStatusEx memoryStatus);

        #endregion

        #region synchapi.h

        /// <summary>
        ///     The OpenEvent API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-openeventw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeWaitHandle OpenEvent(
            int desiredAccess,
            bool inheritHandle,
            string name);

        /// <summary>
        ///     The OpenMutex API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-openmutexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        public static extern SafeWaitHandle OpenMutex(
            int desiredAccess,
            bool inheritHandle,
            string name);

        #endregion

        #region winbase.h

        /// <summary>
        ///     The CopyFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-copyfilew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CopyFile(
            string src,
            string dst,
            [MarshalAs(UnmanagedType.Bool)] bool failIfExists);

        /// <summary>
        ///     The MoveFile API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-movefilew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MoveFile(
            string pathNameFrom,
            string pathNameTo);

        /// <summary>
        ///     The VerifyVersionInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-verifyversioninfow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool VerifyVersionInfo(
            [In] ref OsVersionInfo lpVersionInfo,
            OsVersionInfoTypeFlags typeMask,
            ulong conditionMask);

        /// <summary>
        ///     The GlobalSize API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-globalsize">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern UIntPtr GlobalSize(
            IntPtr hMem);

        /// <summary>
        ///     The GlobalLock API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-globallock">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GlobalLock(
            IntPtr hMem);

        /// <summary>
        ///     The GlobalUnlock API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-globalunlock">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalUnlock(
            IntPtr hMem);

        /// <summary>
        ///     The GlobalAddAtom API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-globaladdatomw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern short GlobalAddAtom(
            string name);

        /// <summary>
        ///     The GlobalDeleteAtom API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-globaldeleteatom">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern short GlobalDeleteAtom(
            short nAtom);

        /// <summary>
        ///     The FormatMessage API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int FormatMessage(
            FormatMessageFlags formatMessageFlags,
            IntPtr source,
            int messageId,
            int languageId,
            StringBuilder buffer,
            int size,
            IntPtr listArguments);

        #endregion

        #region winnt.h

        /// <summary>
        ///     The VerSetConditionMask API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/nf-winnt-versetconditionmask">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern ulong VerSetConditionMask(
            ulong dwlConditionMask,
            OsVersionInfoTypeFlags typeMask,
            OsVersionInfoConditionMask conditionMask);

        #endregion

        #region winnls.h

        /// <summary>
        ///     The GetUserGeoID API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnls/nf-winnls-getusergeoid">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int GetUserGeoID(
            SysGeoClass geoClass);

        /// <summary>
        ///     The GetUserDefaultLCID API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnls/nf-winnls-getuserdefaultlcid">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int GetUserDefaultLCID();

        /// <summary>
        ///     The GetGeoInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnls/nf-winnls-getgeoinfow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int GetGeoInfo(
            int geoid,
            SysGeoType geoType,
            StringBuilder lpGeoData,
            int cchData,
            int langid);

        #endregion

        #region wow64apiset.h

        /// <summary>
        ///     The Wow64DisableWow64FsRedirection API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wow64apiset/nf-wow64apiset-wow64disablewow64fsredirection">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(
            ref IntPtr ptr);

        /// <summary>
        ///     The Wow64RevertWow64FsRedirection API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wow64apiset/nf-wow64apiset-wow64revertwow64fsredirection">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(
            IntPtr ptr);

        /// <summary>
        ///     The Wow64EnableWow64FsRedirection API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wow64apiset/nf-wow64apiset-wow64enablewow64fsredirection">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool Wow64EnableWow64FsRedirection(bool enable);

        #endregion

        #region processthreadsapi.h

        /// <summary>
        ///     The GetCurrentThreadId API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-getcurrentthreadid">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int GetCurrentThreadId();

        /// <summary>
        ///     API method GetCurrentProcessId. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-getcurrentprocessid">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern uint GetCurrentProcessId();

        /// <summary>
        ///     The OpenProcess API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        public static extern SafeProcessHandle OpenProcess(
            [In] ProcessAccessFlags desiredAccess,
            [In][MarshalAs(UnmanagedType.Bool)] bool inheritHandle,
            [In] int processId);

        /// <summary>
        ///     The API method is SetProcessShutdownParameters. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-setprocessshutdownparameters">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetProcessShutdownParameters(
            uint dwLevel,
            uint dwFlags);

        #endregion

        #region libloaderapi.h

        /// <summary>
        ///     The LoadLibrary API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW", SetLastError = true)]
        public static extern SafeDynamicModuleHandle LoadLibrary(
            [In][MarshalAs(UnmanagedType.LPWStr)] string fileName);

        /// <summary>
        ///     The GetProcAddress API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getprocaddress">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern IntPtr GetProcAddress(
            [In] IntPtr hModule,
            [In][MarshalAs(UnmanagedType.LPStr)] string procName);

        /// <summary>
        ///     The FreeLibrary API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-freelibrary">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(
            [In] IntPtr hModule);

        #endregion

        #region handleapi.h

        /// <summary>
        ///     The CloseHandle API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/handleapi/nf-handleapi-closehandle">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(
            IntPtr hObject);

        #endregion

        private const string DllName = "kernel32.dll";
    }
}
