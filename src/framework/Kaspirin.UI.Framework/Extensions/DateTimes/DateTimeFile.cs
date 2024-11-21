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

namespace Kaspirin.UI.Framework.Extensions.DateTimes
{
    /// <summary>
    ///     Extension methods for working with the date of files in Windows File Time format.
    /// </summary>
    public static class DateTimeFile
    {
        /// <summary>
        ///     Converts <paramref name="fileTime" /> in Windows File Time format to an object <see cref="DateTime" />.
        /// </summary>
        /// <param name="fileTime">
        ///     Time in Windows File Time format.
        /// </param>
        /// <returns>
        ///     Returns the file time as an object <see cref="DateTime" />, or an object <see cref="DateTime.MinValue" /> if the conversion failed.
        /// </returns>
        public static DateTime FromFileTimeUtcSafe(this long fileTime)
            => FromFileTimeUtcSafe(fileTime, DateTime.MinValue);

        /// <summary>
        ///     Converts <paramref name="fileTime" /> in Windows File Time format to an object <see cref="DateTime" />.
        /// </summary>
        /// <param name="fileTime">
        ///     Time in Windows File Time format.
        /// </param>
        /// <param name="defaultValue">
        ///     The time returned if the conversion failed.
        /// </param>
        /// <returns>
        ///     Returns the file time as an object <see cref="DateTime" />, or <paramref name="defaultValue" /> if the conversion failed.
        /// </returns>
        public static DateTime FromFileTimeUtcSafe(this long fileTime, DateTime defaultValue)
        {
            try
            {
                var isValid = fileTime >= 0 &&
                              fileTime <= MaxValidFileTime;

                return isValid
                    ? DateTime.FromFileTimeUtc(fileTime)
                    : defaultValue;
            }
            catch (Exception e)
            {
                e.TraceExceptionSuppressed($"Can't convert file time {fileTime} to DateTime. Using {defaultValue} instead.");
                return defaultValue;
            }
        }

        /// <summary>
        ///     Converts <paramref name="fileTime" /> to a number in Windows File Time format.
        /// </summary>
        /// <param name="fileTime">
        ///     Time.
        /// </param>
        /// <returns>
        ///     Returns the file time in Windows File Time format, or 0 if the conversion failed.
        /// </returns>
        public static long ToFileTimeUtcSafe(this DateTime fileTime)
            => ToFileTimeUtcSafe(fileTime, 0);

        /// <summary>
        ///     Converts <paramref name="fileTime" /> to a number in Windows File Time format.
        /// </summary>
        /// <param name="fileTime">
        ///     Time.
        /// </param>
        /// <param name="defaultValue">
        ///     The time returned if the conversion failed.
        /// </param>
        /// <returns>
        ///     Returns the file time in Windows File Time format, or <paramref name="defaultValue" /> if the conversion failed.
        /// </returns>
        public static long ToFileTimeUtcSafe(this DateTime fileTime, long defaultValue)
        {
            try
            {
                var isValid = fileTime != DateTime.MaxValue &&
                              fileTime != DateTime.MinValue &&
                              fileTime.ToUniversalTime().Ticks - MinValidFileTimeTicks >= 0;

                return isValid
                    ? fileTime.ToFileTimeUtc()
                    : defaultValue;
            }
            catch (Exception e)
            {
                e.TraceExceptionSuppressed($"Can't convert {fileTime} to file time. Using {defaultValue} instead.");
                return defaultValue;
            }
        }

        private const long MinValidFileTimeTicks = 504911232000000000L;
        private const long MaxValidFileTime = 2650467743999999999L;
    }
}
