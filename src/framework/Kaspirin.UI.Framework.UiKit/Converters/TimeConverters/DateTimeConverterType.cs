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

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    public enum DateTimeConverterType
    {
        /// <summary>
        /// 7/31/2023
        /// </summary>
        ShortDate,

        /// <summary>
        /// Today, 15:30:05
        /// Yesterday, 15:30:05
        /// 15/05/2023 15:30:05
        /// </summary>
        ShortDateTime,

        /// <summary>
        /// 12:00 AM
        /// Yesterday, 12:00 AM
        /// 7/31/2023 12:00 AM
        /// </summary>
        ShortDateTimeWithoutToday,

        /// <summary>
        /// 8/2/2023 12:00 AM
        /// 8/1/2023 12:00 AM
        /// 7/31/2023 12:00 AM
        /// </summary>
        ShortDateTimeWithoutTodayYesterday,

        /// <summary>
        /// Today, 8/2/2023 12:00 AM
        /// Yesterday, 8/1/2023 12:00 AM
        /// 7/31/2023 12:00 AM
        /// </summary>
        ShortDateTimeWithPinnedDate,

        /// <summary>
        /// 12:00 AM
        /// </summary>
        ShortTime,

        /// <summary>
        /// 12:00 AM
        /// 7/31/2023 12:00 AM
        /// </summary>
        DateTimeWithShortTimeForTodayYesterday,

        /// <summary>
        /// 3 days ago
        /// 2 months ago
        /// 17 hours ago
        /// </summary>
        DateTimeAge,

        /// <summary>
        /// 7/30/2023 12:00:00 AM
        /// </summary>
        FullDateTimeWithoutTodayYesterday,

        /// <summary>
        /// 7/30/2023 12:00 AM
        /// </summary>
        FullDateShortTime,

        /// <summary>
        /// 8/1/2023
        /// </summary>
        FullDate,

        /// <summary>
        /// Today
        /// Yesterday
        /// 7/31/2023
        /// </summary>
        ShortDateTimeTodayYesterday,

        /// <summary>
        /// Today, 8/2/2023 12:00:00 AM
        /// Yesterday, 8/1/2023 12:00:00 AM
        /// 7/31/2023 12:00:00 AM
        /// </summary>
        ShortDateTimeWithPinnedDateAndSeconds,

        /// <summary>
        /// 17:30
        /// </summary>
        HoursMinutesTime
    }
}
