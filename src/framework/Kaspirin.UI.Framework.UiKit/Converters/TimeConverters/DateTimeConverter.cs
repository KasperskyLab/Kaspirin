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

using Kaspirin.UI.Framework.UiKit.Converters.TimeConverters.Validation;
using System;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public sealed class DateTimeConverter : BaseTimeConverter<DateTime, DateTimeConverterType>
    {
        public DateTimeConverter()
        {
            Type = DateTimeConverterType.ShortDateTime;
            Validator = DefaultDateTimeValidator.Instance;
        }

        public bool UtcTimeToLocalTime { get; set; }

        public override LocExtension Convert(DateTime dateTime)
        {
            if (UtcTimeToLocalTime)
            {
                dateTime = dateTime.ToLocalTimeSafe(fromUtc: true);
            }

            return Type switch
            {
                DateTimeConverterType.ShortDateTime => ToShortDateTime(dateTime),
                DateTimeConverterType.ShortDateTimeWithoutTodayYesterday => ToShortDateTimeWithoutTodayYesterday(dateTime),
                DateTimeConverterType.ShortDateTimeWithoutToday => ToShortDateTimeWithoutToday(dateTime),
                DateTimeConverterType.ShortDateTimeWithPinnedDate => ToShortDateTimeWithPinnedDate(dateTime, false),
                DateTimeConverterType.ShortDateTimeWithPinnedDateAndSeconds => ToShortDateTimeWithPinnedDate(dateTime, true),
                DateTimeConverterType.ShortTime => ToShortTime(dateTime),
                DateTimeConverterType.ShortDate => ToShortDate(dateTime),
                DateTimeConverterType.DateTimeWithShortTimeForTodayYesterday => ToDateTimeWithShortTimeForTodayYesterday(dateTime),
                DateTimeConverterType.DateTimeAge => ToDateTimeAge(dateTime),
                DateTimeConverterType.FullDateTimeWithoutTodayYesterday => ToFullDateTimeWithoutTodayYesterday(dateTime),
                DateTimeConverterType.FullDateShortTime => ToFullDateShortTime(dateTime),
                DateTimeConverterType.FullDate => ToFullDate(dateTime),
                DateTimeConverterType.ShortDateTimeTodayYesterday => ToDateTimeWithTodayYesterday(dateTime),
                DateTimeConverterType.HoursMinutesTime => ToHoursMinutesTime(dateTime),
                _ => throw new ArgumentOutOfRangeException(nameof(Type)),
            };
        }

        private LocExtension ToShortTime(DateTime dateTime)
        {
            return ConvertToShortTime(dateTime);
        }

        private LocExtension ToShortDate(DateTime dateTime)
        {
            return ConvertToShortDate(dateTime);
        }

        private LocExtension ToDateTimeWithTodayYesterday(DateTime dateTime)
        {
            if (dateTime.IsToday())
            {
                return GetLocForResource("TimeConverter_Today");
            }

            if (dateTime.IsYesterday())
            {
                return GetLocForResource("TimeConverter_Yesterday");
            }

            return ConvertToShortDate(dateTime);
        }

        private LocExtension ToShortDateTime(DateTime dateTime)
        {
            if (dateTime.IsToday())
            {
                return ConvertToShortTimeWithToday(dateTime);
            }

            if (dateTime.IsYesterday())
            {
                return ConvertToShortTimeWithYesterday(dateTime);
            }

            return ConvertToShortDateTime(dateTime);
        }

        private LocExtension ToShortDateTimeWithoutToday(DateTime dateTime)
        {
            if (dateTime.IsToday())
            {
                return ConvertToShortTime(dateTime);
            }

            if (dateTime.IsYesterday())
            {
                return ConvertToShortTimeWithYesterday(dateTime);
            }

            return ConvertToShortDateTime(dateTime);
        }

        private LocExtension ToShortDateTimeWithoutTodayYesterday(DateTime dateTime)
        {
            return ConvertToShortDateTime(dateTime);
        }

        private LocExtension ToShortDateTimeWithPinnedDate(DateTime dateTime, bool withSeconds)
        {
            if (dateTime.IsToday())
            {
                return ConvertToShortDateTimeWithToday(dateTime, withSeconds);
            }

            if (dateTime.IsYesterday())
            {
                return ConvertToShortDateTimeWithYesterday(dateTime, withSeconds);
            }

            return ConvertToShortDateTime(dateTime, withSeconds);
        }

        private LocExtension ToDateTimeWithShortTimeForTodayYesterday(DateTime dateTime)
        {
            if (dateTime.IsToday() || dateTime.IsYesterday())
            {
                return ConvertToShortTime(dateTime);
            }

            return ConvertToShortDateTime(dateTime);
        }

        private LocExtension ToFullDateTimeWithoutTodayYesterday(DateTime dateTime)
        {
            return ConvertToFullDateTime(dateTime);
        }

        private LocExtension ToFullDateShortTime(DateTime dateTime)
        {
            return ConvertToFullDateShortTime(dateTime);
        }

        private LocExtension ToFullDate(DateTime dateTime)
        {
            return ConvertToFullDate(dateTime);
        }

        private LocExtension ToDateTimeAge(DateTime dateTime)
        {
            var dateTimeAge = dateTime.TryGetDateTimeAge();

            return dateTimeAge == null
                ? GetLocForResource("TimeConverter_Never")
                : ConvertToDateTimeAge(dateTimeAge);
        }

        #region Localization methods
        private LocExtension ConvertToFullDateTime(DateTime dateTime)
        {
            // G Format Specifier      en-US Culture                    10/31/2008 5:04:32 PM
            return GetLocForConstantString(dateTime.ToFormat("G"));
        }
        private LocExtension ConvertToFullDateShortTime(DateTime dateTime)
        {
            // f Format Specifier      en-US Culture         Friday, October 31, 2008 5:04 PM
            // g Format Specifier      en-US Culture                       10/31/2008 5:04 PM
            return GetLocForConstantString(dateTime.ToFormat("g"));
        }

        private LocExtension ConvertToShortTime(DateTime dateTime)
        {
            return GetLocForConstantString(dateTime.ToFormat("t"));
        }

        private LocExtension ToHoursMinutesTime(DateTime dateTime)
        {
            return GetLocForConstantString(dateTime.ToFormat("HH:mm"));
        }

        private LocExtension ConvertToShortDate(DateTime dateTime)
        {
            return GetLocForConstantString(dateTime.ToFormat("d"));
        }

        private LocExtension ConvertToShortDateTime(DateTime dateTime, bool withSeconds = false)
        {
            return GetLocForConstantString(dateTime.ToFormat(withSeconds ? "G" : "g"));
        }

        private LocExtension ConvertToFullDate(DateTime dateTime)
        {
            // D Format Specifier      en-US Culture                 Friday, October 31, 2008
            // d Format Specifier      en-US Culture                               10/31/2008
            return GetLocForConstantString(dateTime.ToFormat("d"));
        }

        private LocExtension ConvertToShortTimeWithToday(DateTime dateTime)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_TodayPattern",
                paramName: "DateTime",
                value: dateTime.ToFormat("t"));
        }

        private LocExtension ConvertToShortTimeWithYesterday(DateTime dateTime)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_YesterdayPattern",
                paramName: "DateTime",
                value: dateTime.ToFormat("t"));
        }

        private LocExtension ConvertToShortDateTimeWithToday(DateTime dateTime, bool withSeconds)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_TodayPattern",
                paramName: "DateTime",
                value: dateTime.ToFormat(withSeconds ? "G" : "g"));
        }

        private LocExtension ConvertToShortDateTimeWithYesterday(DateTime dateTime, bool withSeconds)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_YesterdayPattern",
                paramName: "DateTime",
                value: dateTime.ToFormat(withSeconds ? "G" : "g"));
        }

        private LocExtension ConvertToDateTimeAge(DateTimeAge dateTimeAge)
        {
            return dateTimeAge.Estimate switch
            {
                DateTimeAgeEstimate.Future => GetLocForResourceWithParam(
                                        resourceKey: "TimeConverter_Value",
                                        paramName: "Value",
                                        value: dateTimeAge.EstimatedTime.ToFormat("g")),
                DateTimeAgeEstimate.SecondsAgo => GetLocForResource(
                                        resourceKey: "TimeConverter_AgoSeconds"),
                DateTimeAgeEstimate.MinutesAgo => GetLocForResourceWithParam(
                                        resourceKey: "TimeConverter_AgoMinutes",
                                        paramName: "Value",
                                        value: dateTimeAge.TotalMinutes.ToString()),
                DateTimeAgeEstimate.HoursAgo => GetLocForResourceWithParam(
                                        resourceKey: "TimeConverter_AgoHours",
                                        paramName: "Value",
                                        value: dateTimeAge.TotalHours.ToString()),
                DateTimeAgeEstimate.Yesterday => GetLocForResource(
                                        resourceKey: "TimeConverter_AgoYesterday"),
                DateTimeAgeEstimate.DaysAgo => GetLocForResourceWithParam(
                                        resourceKey: "TimeConverter_AgoDays",
                                        paramName: "Value",
                                        value: dateTimeAge.TotalDays.ToString()),
                DateTimeAgeEstimate.MonthsAgo => GetLocForResourceWithParam(
                                        resourceKey: "TimeConverter_AgoMonths",
                                        paramName: "Value",
                                        value: dateTimeAge.TotalMonths.ToString()),
                DateTimeAgeEstimate.YearsAgo => GetLocForResourceWithParam(
                                        resourceKey: "TimeConverter_AgoYears",
                                        paramName: "Value",
                                        value: dateTimeAge.TotalYears.ToString()),
                _ => throw new ArgumentOutOfRangeException(nameof(dateTimeAge)),
            };
        }
        #endregion
    }
}
