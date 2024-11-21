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
    ///     Extension methods for <see cref="DateTime" />.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Checks whether the date value in <paramref name="time" /> matches today's date.
        /// </summary>
        /// <param name="time">
        ///     Time to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the date value in <paramref name="time" /> corresponds to
        ///     today's date, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsToday(this DateTime time)
        {
            var localTime = time.ToLocalTimeSafe();
            var today = DateTime.Today;
            return today <= localTime && localTime < today.AddDays(1);
        }

        /// <summary>
        ///     Checks whether the date value in <paramref name="time" /> matches yesterday's date.
        /// </summary>
        /// <param name="time">
        ///     Time to check.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the date value in <paramref name="time" /> corresponds to
        ///     yesterday's date, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsYesterday(this DateTime time)
        {
            var localTime = time.ToLocalTimeSafe();
            var today = DateTime.Today;
            return today.AddDays(-1) <= localTime && localTime < today;
        }

        /// <summary>
        ///     Converts <paramref name="time" /> to local time.
        /// </summary>
        /// <param name="time">
        ///     The time being converted.
        /// </param>
        /// <returns>
        ///     Returns the time converted to the local one, or the original <paramref name="time" /> object if the conversion failed.
        /// </returns>
        public static DateTime ToLocalTimeSafe(this DateTime time)
            => ToLocalTimeSafe(time, false, time);

        /// <summary>
        ///     Converts <paramref name="time" /> to local time.
        /// </summary>
        /// <param name="time">
        ///     The time being converted.
        /// </param>
        /// <param name="defaultValue">
        ///     The time returned if the conversion failed.
        /// </param>
        /// <returns>
        ///     Returns the time converted to local, or the <paramref name="defaultValue" /> object if the conversion failed.
        /// </returns>
        public static DateTime ToLocalTimeSafe(this DateTime time, DateTime defaultValue)
            => ToLocalTimeSafe(time, false, defaultValue);

        /// <summary>
        ///     Converts <paramref name="time" /> to local time.
        /// </summary>
        /// <param name="time">
        ///     The time being converted.
        /// </param>
        /// <param name="fromUtc">
        ///     If <see langword="true" />, then the conversion takes place from UTC format.
        /// </param>
        /// <returns>
        ///     Returns the time converted to the local one, or the original <paramref name="time" /> object if the conversion failed.
        /// </returns>
        public static DateTime ToLocalTimeSafe(this DateTime time, bool fromUtc)
            => ToLocalTimeSafe(time, fromUtc, time);

        /// <summary>
        ///     Converts <paramref name="time" /> to local time.
        /// </summary>
        /// <param name="time">
        ///     The time being converted.
        /// </param>
        /// <param name="fromUtc">
        ///     If <see langword="true" />, then the conversion takes place from UTC format.
        /// </param>
        /// <param name="defaultValue">
        ///     The time returned if the conversion failed.
        /// </param>
        /// <returns>
        ///     Returns the time converted to local, or the <paramref name="defaultValue" /> object if the conversion failed.
        /// </returns>
        public static DateTime ToLocalTimeSafe(this DateTime time, bool fromUtc, DateTime defaultValue)
        {
            if (time == default)
            {
                return defaultValue;
            }

            try
            {
                if (fromUtc)
                {
                    time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                }

                return time.ToLocalTime();
            }
            catch (Exception e)
            {
                e.TraceExceptionSuppressed($"Can't convert {time} to local time. Using {defaultValue} instead.");
                return defaultValue;
            }
        }
    }
}
