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
using System.Threading;

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsValid(this DateTime dateTime)
        {
            var minSupportedDateTime = GetMinValueForCurrentLocale();
            var maxSupportedDateTime = GetMaxValueForCurrentLocale();

            var utcDateTime = dateTime.ToUniversalTimeSafe();
            var localDateTime = dateTime.ToLocalTimeSafe();

            return utcDateTime >= minSupportedDateTime
                && utcDateTime <= maxSupportedDateTime
                && localDateTime >= minSupportedDateTime
                && localDateTime <= maxSupportedDateTime;
        }

        public static DateTime EnsureToBeValid(this DateTime dateTime)
        {
            var minSupportedDateTime = GetMinValueForCurrentLocale();
            var maxSupportedDateTime = GetMaxValueForCurrentLocale();

            var utcDateTime = dateTime.ToUniversalTimeSafe();
            var localDateTime = dateTime.ToLocalTimeSafe();

            if (utcDateTime < minSupportedDateTime || localDateTime < minSupportedDateTime)
            {
                return minSupportedDateTime;
            }

            if (utcDateTime > maxSupportedDateTime || localDateTime > maxSupportedDateTime)
            {
                return maxSupportedDateTime;
            }

            return dateTime;
        }

        private static DateTime GetMinValueForCurrentLocale()
        {
            var localizationManagerCalendar = LocalizationManager.Current.DisplayCulture.CultureInfo.DateTimeFormat.Calendar;

            var threadCalendar = Thread.CurrentThread.CurrentCulture
                ?.DateTimeFormat.Calendar
                ?? localizationManagerCalendar;

            var minSupportedDateTime = localizationManagerCalendar.MinSupportedDateTime > threadCalendar.MinSupportedDateTime
                                       ? localizationManagerCalendar.MinSupportedDateTime
                                       : threadCalendar.MinSupportedDateTime;

            return minSupportedDateTime;
        }

        private static DateTime GetMaxValueForCurrentLocale()
        {
            var localizationManagerCalendar = LocalizationManager.Current.DisplayCulture.CultureInfo.DateTimeFormat.Calendar;

            var threadCalendar = Thread.CurrentThread.CurrentCulture
                ?.DateTimeFormat.Calendar
                ?? localizationManagerCalendar;

            var maxSupportedDateTime = localizationManagerCalendar.MaxSupportedDateTime < threadCalendar.MaxSupportedDateTime
                                       ? localizationManagerCalendar.MaxSupportedDateTime
                                       : threadCalendar.MaxSupportedDateTime;

            return maxSupportedDateTime;
        }

        private static DateTime ToLocalTimeSafe(this DateTime time)
        {
            if (time == default)
            {
                return time;
            }

            try
            {
                return time.ToLocalTime();
            }
            catch
            {
                return time;
            }
        }

        private static DateTime ToUniversalTimeSafe(this DateTime time)
        {
            if (time == default)
            {
                return time;
            }

            try
            {
                return time.ToUniversalTime();
            }
            catch
            {
                return time;
            }
        }
    }
}
