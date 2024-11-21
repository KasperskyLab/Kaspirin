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
    ///     Extension methods for working with a date in UNIX format.
    /// </summary>
    public static class DateTimeUnix
    {
        /// <summary>
        ///     Converts the time <paramref name="dateTime" /> to UNIX seconds.
        /// </summary>
        /// <param name="dateTime">
        ///     Time.
        /// </param>
        /// <returns>
        ///     UNIX seconds as an integer.
        /// </returns>
        public static int ToUnixTimeStamp(this DateTime dateTime)
            => (int)(dateTime - InitialDateTime).TotalSeconds;

        /// <summary>
        ///     Converts UNIX seconds to an object <see cref="DateTime" />.
        /// </summary>
        /// <param name="unixTimeStamp">
        ///     UNIX seconds.
        /// </param>
        /// <returns>
        ///     UNIX seconds as an object <see cref="DateTime" />.
        /// </returns>
        public static DateTime FromUnixTimeStamp(this int unixTimeStamp)
            => InitialDateTime.AddSeconds(unixTimeStamp);

        /// <summary>
        ///     The starting date accepted in UNIX systems is 01.01.1970 00:00:00 UTC.
        /// </summary>
        public static DateTime InitialDateTime { get; } = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    }
}
