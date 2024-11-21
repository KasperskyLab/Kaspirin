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
using System.Runtime.InteropServices;

namespace Kaspirin.UI.Framework.SystemInfo
{
    /// <summary>
    ///     Extension methods for <see cref="OperatingSystem" /> that provide information about the current
    ///     version of the operating system.
    /// </summary>
    public static class OperatingSystemExtensions
    {
        /// <summary>
        ///     Checks whether the operating system is Windows Vista or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows Vista or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsVistaOrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.Version.Major >= 6;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 7.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 7, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin7(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.Version.Major == 6 && operatingSystem.Version.Minor == 1;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 7 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 7 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin7OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.Version.Major > 6 ||
                   operatingSystem.Version.Major == 6 && operatingSystem.Version.Minor >= 1;
        }

        /// <summary>
        ///     Checks if the operating system is Windows 8.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 8, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin8(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.Version.Major == 6 && operatingSystem.Version.Minor == 2;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 8 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 8 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin8OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.Version.Major > 6 ||
                   operatingSystem.Version.Major == 6 && operatingSystem.Version.Minor >= 2;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 8.1.
        /// </summary>
        /// <param name="operatingSystem">
        ///     Not used.
        /// </param>
        /// <remarks>
        ///     To check the version, the VerifyVersionInfo function is used in kernel32.dll .
        /// </remarks>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 8.1, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin81(this OperatingSystem operatingSystem)
        {
            ulong conditionMask = 0;
            var versionInfo = new OsVersionInfo();
            versionInfo.OsVersionInfoSize = Marshal.SizeOf(versionInfo);
            versionInfo.MajorVersion = 6;
            versionInfo.MinorVersion = 3;
            versionInfo.ServicePackMajor = 0;
            versionInfo.ServicePackMinor = 0;

            const OsVersionInfoConditionMask operation = OsVersionInfoConditionMask.VER_GREATER_EQUAL;

            // Initialize the condition mask.
            conditionMask = Kernel32Dll.VerSetConditionMask(conditionMask, OsVersionInfoTypeFlags.VER_MAJORVERSION, operation);
            conditionMask = Kernel32Dll.VerSetConditionMask(conditionMask, OsVersionInfoTypeFlags.VER_MINORVERSION, operation);

            // Perform the test.
            return Kernel32Dll.VerifyVersionInfo(
                ref versionInfo,
                OsVersionInfoTypeFlags.VER_MAJORVERSION | OsVersionInfoTypeFlags.VER_MINORVERSION,
                conditionMask);
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 8.1 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 8.1 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin81OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.IsWin8OrHigher() && !operatingSystem.IsWin8();
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 10 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 10 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin10OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.Version.Major >= 10;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 10 RS1 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 10 RS1 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin10Rs1OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.IsWin10OrHigher() && operatingSystem.Version.Build >= Windows10Rs1Build;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 10 RS4 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 10 RS4 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin10Rs4OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.IsWin10OrHigher() && operatingSystem.Version.Build >= Windows10Rs4Build;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 10 RS5 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 10 RS5 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin10Rs5OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.IsWin10OrHigher() && operatingSystem.Version.Build >= Windows10Rs5Build;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 10 RS6 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 10 RS6 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin10Rs6OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.IsWin10OrHigher() && operatingSystem.Version.Build >= Windows10Rs6Build;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 10 version 19H1 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 10 version 19H1 or later,
        ///     otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin10Version19H1OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.IsWin10OrHigher() && operatingSystem.Version.Build >= Windows10Version19H1Build;
        }

        /// <summary>
        ///     Checks whether the operating system is Windows 11 or later.
        /// </summary>
        /// <param name="operatingSystem">
        ///     The operating system to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the operating system is Windows 11 or later, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsWin11OrHigher(this OperatingSystem operatingSystem)
        {
            Guard.ArgumentIsNotNull(operatingSystem);

            return operatingSystem.Version.Major >= 10 && operatingSystem.Version.Build >= Windows11Version21H2Build;
        }

        private const int Windows10Rs1Build = 14393;
        private const int Windows10Rs4Build = 17134;
        private const int Windows10Rs5Build = 17763;
        private const int Windows10Rs6Build = 18204;
        private const int Windows10Version19H1Build = 18362;
        private const int Windows11Version21H2Build = 22000;
    }
}
