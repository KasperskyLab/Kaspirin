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

#pragma warning disable CA1416 // This call site is reachable on all platforms

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.SystemInfo
{
    /// <summary>
    ///     Provides information about the operating system version.
    /// </summary>
    public static class OperatingSystemInfo
    {
        static OperatingSystemInfo()
        {
            _trace = ComponentTracer.Get(ComponentTracers.SystemInfo);
            _versionInfo = GetOsVersionInfo();

            InitializeProperties();
        }

        /// <summary>
        ///     Indicates whether the operating system is a Windows Server.
        /// </summary>
        public static bool IsWindowsServer { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows Server 2016.
        /// </summary>
        public static bool IsWindowsServer16 { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows Server 2016 or later.
        /// </summary>
        public static bool IsWindowsServer16OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is a multi-session version of Windows.
        /// </summary>
        public static bool IsWindowsMultiSession { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows Vista or later.
        /// </summary>
        public static bool IsVistaOrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 7.
        /// </summary>
        public static bool IsWin7 { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 7 or later.
        /// </summary>
        public static bool IsWin7OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 8.
        /// </summary>
        public static bool IsWin8 { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 8 or later.
        /// </summary>
        public static bool IsWin8OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 8.1.
        /// </summary>
        public static bool IsWin81 { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 8.1 or later.
        /// </summary>
        public static bool IsWin81OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 10 or later.
        /// </summary>
        public static bool IsWin10OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 10 RS1 or later.
        /// </summary>
        public static bool IsWin10Rs1OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 10 RS4 or later.
        /// </summary>
        public static bool IsWin10Rs4OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 10 RS5 or later.
        /// </summary>
        public static bool IsWin10Rs5OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 10 RS6 or later.
        /// </summary>
        public static bool IsWin10Rs6OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the operating system is Windows 10 version 19H1 or later.
        /// </summary>
        public static bool IsWin10Version19H1OrHigher { get; private set; }

        /// <summary>
        ///     Indicates whether the current operating system is Windows 11 or later.
        /// </summary>
        public static bool IsWin11OrHigher { get; private set; }

        /// <summary>
        ///     Retrieves information about the current version of the operating system and passes it as an
        ///     argument to the compliance check delegate <paramref name="isVersionSuitable" />.
        /// </summary>
        /// <param name="isVersionSuitable">
        ///     A delegate for checking the version for compliance.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the delegate <paramref name="isVersionSuitable" /> returned <see langword="true" />,
        ///     otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWindowsOfSpecificVersion(Func<OsVersionInfo, bool> isVersionSuitable)
        {
            Guard.ArgumentIsNotNull(isVersionSuitable);

            return _versionInfo.HasValue && isVersionSuitable(_versionInfo.Value);
        }

        private static bool CheckIsWindowsServer16()
        {
            if (!_versionInfo.HasValue)
            {
                return false;
            }

            return _versionInfo.Value.MajorVersion == 10
                && _versionInfo.Value.MinorVersion == 0
                && _versionInfo.Value.ProductType != OsProductType.VER_NT_WORKSTATION;
        }

        private static bool CheckIsWindowsServer()
        {
            if (!_versionInfo.HasValue)
            {
                return false;
            }

            return _versionInfo.Value.ProductType != OsProductType.VER_NT_WORKSTATION;
        }

        private static bool CheckIsWindowsServer16OrHigher()
        {
            if (!_versionInfo.HasValue)
            {
                return false;
            }

            return _versionInfo.Value.MajorVersion >= 10
                && _versionInfo.Value.ProductType != OsProductType.VER_NT_WORKSTATION;
        }

        private static bool CheckIsWindowsMultiSession()
        {
            var versionInfo = new OsVersionInfo();
            versionInfo.OsVersionInfoSize = Marshal.SizeOf(versionInfo);
            versionInfo.ProductType = OsProductType.VER_NT_SERVER;
            var typeMask = OsVersionInfoTypeFlags.VER_PRODUCT_TYPE;
            var conditionMask = Kernel32Dll.VerSetConditionMask(0, typeMask, OsVersionInfoConditionMask.VER_EQUAL);
            if (!Kernel32Dll.VerifyVersionInfo(ref versionInfo, typeMask, conditionMask))
            {
                return false;
            }

            using var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion");
            return key?.GetValue("InstallationType")?.ToString()?.Equals("Client") ?? false;
        }

        private static OsVersionInfo? GetOsVersionInfo()
        {
            var osInfo = new OsVersionInfo();
            osInfo.OsVersionInfoSize = Marshal.SizeOf(osInfo);

            if (!Kernel32Dll.GetVersionEx(ref osInfo))
            {
                _trace.TraceError($"GetVersionEx failed with 0x{Marshal.GetLastWin32Error():X}");
                return null;
            }

            if (IsWindowsMultiSession)
            {
                osInfo.ProductType = OsProductType.VER_NT_WORKSTATION;
            }

            _trace.TraceInformation($"OS: Major={osInfo.MajorVersion}, " +
                                    $"Minor={osInfo.MinorVersion}, " +
                                    $"Build={osInfo.BuildNumber}, " +
                                    $"Type={osInfo.ProductType}");

            return osInfo;
        }

        private static void InitializeProperties()
        {
            IsWindowsServer = CheckIsWindowsServer();
            IsWindowsServer16 = CheckIsWindowsServer16();
            IsWindowsServer16OrHigher = CheckIsWindowsServer16OrHigher();

            IsWindowsMultiSession = CheckIsWindowsMultiSession();

            IsVistaOrHigher = Environment.OSVersion.IsVistaOrHigher();
            IsWin7 = Environment.OSVersion.IsWin7();
            IsWin7OrHigher = Environment.OSVersion.IsWin7OrHigher();
            IsWin8 = Environment.OSVersion.IsWin8();
            IsWin8OrHigher = Environment.OSVersion.IsWin8OrHigher();
            IsWin81 = Environment.OSVersion.IsWin81();
            IsWin81OrHigher = Environment.OSVersion.IsWin81OrHigher();
            IsWin10OrHigher = Environment.OSVersion.IsWin10OrHigher();
            IsWin10Rs1OrHigher = Environment.OSVersion.IsWin10Rs1OrHigher();
            IsWin10Rs4OrHigher = Environment.OSVersion.IsWin10Rs4OrHigher();
            IsWin10Rs5OrHigher = Environment.OSVersion.IsWin10Rs5OrHigher();
            IsWin10Rs6OrHigher = Environment.OSVersion.IsWin10Rs6OrHigher();
            IsWin10Version19H1OrHigher = Environment.OSVersion.IsWin10Version19H1OrHigher();
            IsWin11OrHigher = Environment.OSVersion.IsWin11OrHigher();
        }

        private static readonly OsVersionInfo? _versionInfo;
        private static readonly ComponentTracer _trace;
    }
}
