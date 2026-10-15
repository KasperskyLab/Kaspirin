// Copyright © 2025 AO Kaspersky Lab.
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
using System.Runtime.InteropServices;

namespace Kaspirin.UI.Framework.FileSystem;

/// <summary>
///     Allows you to get and change the case sensitivity attribute of directories.
/// </summary>
public static class CaseSensitivity
{
    /// <summary>
    ///     Checks the <paramref name="directoryPath" /> directory for case sensitivity.
    /// </summary>
    /// <param name="directoryPath">
    ///     The path to the directory.
    /// </param>
    /// <remarks>
    ///     The case sensitivity feature has been available since Windows 10 RS4. For earlier OS versions,
    ///     the method always returns <see langword="false" />.
    /// </remarks>
    /// <returns>
    ///     Returns <see langword="true" /> if the directory is case-sensitive, otherwise <see langword="false" />.
    /// </returns>
    public static bool GetFlag(string directoryPath)
    {
        Guard.ArgumentIsNotNullOrEmpty(directoryPath);

        if (!OperatingSystemInfo.IsWin10Rs4OrHigher)
        {
            _tracer.TraceError($"Can't get Case Sensitive attribute for directory: {directoryPath}. Case sensitivity supports only in Windows 10 RS4 and higher.");
            return false;
        }

        using (var fileHandle = Kernel32Dll.CreateFile(
            directoryPath,
            AccessMaskFlags.None,
            FileShare.ReadWrite,
            IntPtr.Zero,
            FileMode.Open,
            CreateFileFlags.FlagBackupSemantics,
            IntPtr.Zero))
        {
            if (fileHandle.IsInvalid)
            {
                _tracer.TraceError($"Can't get Case Sensitive attribute for directory: {directoryPath}. Can't open directory.");
                return false;
            }

            NtStatus status;
            CaseSensitiveInformation? info = null;

            var ioStatusBlock = new IoStatusBlock();
            var sensitiveInformationSize = Marshal.SizeOf(typeof(CaseSensitiveInformation));
            var sensitiveInformationPtr = Marshal.AllocHGlobal(sensitiveInformationSize);

            try
            {
                status = NtdllDll.NtQueryInformationFile(
                    fileHandle.DangerousGetHandle(),
                    ref ioStatusBlock,
                    sensitiveInformationPtr,
                    sensitiveInformationSize,
                    FileInformationType.FileCaseSensitiveInformation);

                if (status == NtStatus.StatusSuccess)
                {
                    info = Marshal.PtrToStructure(sensitiveInformationPtr, typeof(CaseSensitiveInformation)) as CaseSensitiveInformation?;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(sensitiveInformationPtr);
            }

            if (status != NtStatus.StatusSuccess)
            {
                _tracer.TraceError($"Can't get Case Sensitive attribute for directory: {directoryPath}. Error: {status}.");
                return false;
            }

            var isCaseSensitive = info.HasValue && info.Value.Flags.HasFlag(CaseSensitivityValues.CaseSensitiveDirectory);
            _tracer.TraceDebug($"Directory is Case {(isCaseSensitive ? "Sensitive" : "Insensitive")}: {directoryPath}.");

            return isCaseSensitive;
        }
    }

    /// <summary>
    ///     Sets or removes the case sensitivity attribute for the <paramref name="directoryPath" /> directory.
    /// </summary>
    /// <param name="directoryPath">
    ///     The path to the directory.
    /// </param>
    /// <param name="value">
    ///     The value to set.
    /// </param>
    /// <remarks>
    ///     The case sensitivity feature has been available since Windows 10 RS4. For earlier OS versions,
    ///     the method always returns <see langword="false" />.
    /// </remarks>
    /// <returns>
    ///     Returns <see langword="true" /> if the setting of the <paramref name="value" /> for the case
    ///     sensitivity attribute was successful, otherwise - <see langword="false" />.
    /// </returns>
    public static bool SetFlag(string directoryPath, bool value)
    {
        Guard.ArgumentIsNotNullOrEmpty(directoryPath);

        if (!OperatingSystemInfo.IsWin10Rs4OrHigher)
        {
            _tracer.TraceError($"Can't set Case Sensitive attribute to '{value}' for directory: {directoryPath}. Case sensitivity supports only in Windows 10 RS4 and higher.");
            return false;
        }

        using (var fileHandle = Kernel32Dll.CreateFile(
            directoryPath,
            AccessMaskFlags.FileWriteAttributes,
            FileShare.ReadWrite,
            IntPtr.Zero,
            FileMode.Open,
            CreateFileFlags.FlagBackupSemantics,
            IntPtr.Zero))
        {
            if (fileHandle.IsInvalid)
            {
                _tracer.TraceError($"Can't set Case Sensitive attribute for directory: {directoryPath}. Can't open directory.");
                return false;
            }

            var ioStatusBlock = new IoStatusBlock();
            var sensitiveInformation = new CaseSensitiveInformation
            {
                Flags = value
                    ? CaseSensitivityValues.CaseSensitiveDirectory
                    : CaseSensitivityValues.CaseInsensitiveDirectory
            };

            var sensitiveInformationSize = Marshal.SizeOf(typeof(CaseSensitiveInformation));
            var sensitiveInformationPtr = Marshal.AllocHGlobal(sensitiveInformationSize);
            Marshal.StructureToPtr(sensitiveInformation, sensitiveInformationPtr, false);

            NtStatus status;

            try
            {
                status = NtdllDll.NtSetInformationFile(fileHandle.DangerousGetHandle(),
                    ref ioStatusBlock,
                    sensitiveInformationPtr,
                    sensitiveInformationSize,
                    FileInformationType.FileCaseSensitiveInformation);
            }
            finally
            {
                Marshal.FreeHGlobal(sensitiveInformationPtr);
            }

            if (status != NtStatus.StatusSuccess)
            {
                _tracer.TraceError($"Can't set Case Sensitive for directory: {directoryPath}. Can't set attribute for directory. Error: {status}.");
                return false;
            }
        }

        _tracer.TraceInformation($"Successfully set Case {(value ? "Sensitive" : "Insensitive")} for directory: {directoryPath}.");
        return true;
    }

    private struct CaseSensitiveInformation
    {
        [MarshalAs(UnmanagedType.U4)]
        public CaseSensitivityValues Flags;
    }

    private enum CaseSensitivityValues : uint
    {
        CaseInsensitiveDirectory = 0x0,
        CaseSensitiveDirectory = 0x1,
    }

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(ComponentTracers.FileSystem);
}
