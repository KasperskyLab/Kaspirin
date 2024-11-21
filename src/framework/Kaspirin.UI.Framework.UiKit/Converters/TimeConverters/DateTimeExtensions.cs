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
using System.Globalization;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    public static class DateTimeExtensions
    {
        public static DateTimeAge? TryGetDateTimeAge(this DateTime dateTime)
        {
            var now = DateTime.Now;
            var localDateTime = dateTime.ToLocalTimeSafe();

            return localDateTime.IsValid()
                ? new DateTimeAge(localDateTime, now)
                : null;
        }

        public static string ToFormat(this DateTime dateTime, string formatPattern, IFormatProvider? formatProvider = null)
        {
            var dateTimeString = formatProvider != null
                ? dateTime.ToString(formatPattern, formatProvider)
                : dateTime.ToString(formatPattern, dateTime.GetDateTimeFormat());

            return dateTimeString.CreateLeftToRightString();
        }

        public static DateTimeFormatInfo GetDateTimeFormat(this DateTime dateTime, DateTimeFormatInfo? formatInfo = null)
        {
            var dateTimeFormatInfo = formatInfo ?? LocalizationManager.Current.FormatCulture.CultureInfo.DateTimeFormat;
            var dateTimeFormatInfoFallback = CultureInfo.InvariantCulture.DateTimeFormat;

            if (CanBePresentedInCalendar(dateTime, dateTimeFormatInfo))
            {
                return dateTimeFormatInfo;
            }

            if (CanBePresentedInCalendar(dateTime, dateTimeFormatInfoFallback))
            {
                return dateTimeFormatInfoFallback;
            }

            throw new ArgumentOutOfRangeException("dateTime", string.Format(CultureInfo.InvariantCulture,
                "Specified DateTime = {0} cant be presented with the current calendar {1} [{2}, {3}].",
                dateTime,
                dateTimeFormatInfo.Calendar.GetType().Name,
                dateTimeFormatInfo.Calendar.MinSupportedDateTime,
                dateTimeFormatInfo.Calendar.MaxSupportedDateTime));
        }

        private static bool CanBePresentedInCalendar(DateTime dateTime, DateTimeFormatInfo formatInfo)
        {
            return
                dateTime.Ticks >= formatInfo.Calendar.MinSupportedDateTime.Ticks &&
                dateTime.Ticks <= formatInfo.Calendar.MaxSupportedDateTime.Ticks;
        }
    }
}
