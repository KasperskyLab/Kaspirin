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
using System.Collections.Generic;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public sealed class TimeSpanConverter : BaseTimeConverter<TimeSpan, TimeSpanConverterType>
    {
        public TimeSpanConverter()
        {
            Type = TimeSpanConverterType.TimePassed;
            Validator = DefaultTimeSpanValidator.Instance;
        }

        public override LocExtension Convert(TimeSpan timeSpan)
        {
            return Type switch
            {
                TimeSpanConverterType.TimePassed => ToDurationWithPartsAndUnits(timeSpan),
                TimeSpanConverterType.TimeLeft => ToTimeLeft(timeSpan),
                TimeSpanConverterType.ShortTime => ToShortTime(timeSpan),
                TimeSpanConverterType.TimeSpan => ToTimeSpan(timeSpan),
                TimeSpanConverterType.TimeSpanSeconds => ConvertToTimeSpanSeconds(timeSpan),
                TimeSpanConverterType.ShortTimePast => ConvertToShortTimePast(timeSpan),
                _ => throw new ArgumentOutOfRangeException(nameof(Type)),
            };
        }

        private LocExtension ToTimeSpan(TimeSpan timeSpan)
        {
            return ConvertToTimeSpan(timeSpan);
        }

        private LocExtension ToShortTime(TimeSpan timeSpan)
        {
            var dateTimeConverter = new DateTimeConverter
            {
                Type = DateTimeConverterType.ShortTime,
                LowercaseFirstLetter = LowercaseFirstLetter,
                InvalidValueString = InvalidValueString
            };
            return dateTimeConverter.Convert(DateTime.Today + timeSpan);
        }

        private LocExtension ToDurationWithPartsAndUnits(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 1)
            {
                return ConvertToSecondsPassed(timeSpan);
            }

            if (timeSpan.TotalHours < 1 && timeSpan.Seconds != 0)
            {
                return ConvertToMinutesAndSecondsPassed(timeSpan);
            }

            if (timeSpan.TotalHours < 1)
            {
                return ConvertToMinutesPassed(timeSpan);
            }

            if (timeSpan.TotalDays < 1 && timeSpan.Minutes != 0)
            {
                return ConvertToHoursAndMinutesPassed(timeSpan);
            }

            if (timeSpan.TotalDays < 1)
            {
                return ConvertToHoursPassed(timeSpan);
            }

            if (timeSpan.Hours != 0)
            {
                return ConvertToDaysAndHoursPassed(timeSpan);
            }

            return ConvertToDaysPassed(timeSpan);
        }

        private LocExtension ToTimeLeft(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 1)
            {
                return ConvertToLessMinutesLeft();
            }

            if (timeSpan.TotalHours < 1)
            {
                return ConvertToMinutesLeft(timeSpan);
            }

            return timeSpan.TotalDays switch
            {
                < 1 => ConvertToHoursLeft(timeSpan),
                _ => ConvertToDaysLeft(timeSpan)
            };
        }

        private LocExtension ConvertToShortTimePast(TimeSpan timeSpan)
        {
            return timeSpan.TotalHours switch
            {
                < 1 => ConvertToMinutesAgoShort(timeSpan),
                _ => ConvertToHoursAgoShort(timeSpan)
            };
        }

        #region Localization methods
        private LocExtension ConvertToLessMinutesLeft()
        {
            return GetLocForResource(
                resourceKey: "TimeConverter_RemainingTimeLessMinute");
        }
        private LocExtension ConvertToMinutesLeft(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_RemainingTimeMinutes",
                paramName: "MinuteCount",
                value: timeSpan.Minutes.ToString());
        }
        private LocExtension ConvertToHoursLeft(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_RemainingTimeHours",
                paramName: "HourCount",
                value: timeSpan.Hours.ToString());
        }
        private LocExtension ConvertToDaysLeft(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_RemainingTimeDays",
                paramName: "DayCount",
                value: timeSpan.Days.ToString());
        }
        private LocExtension ConvertToDaysPassed(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_PassedTimeDays",
                paramName: "DayCount",
                value: timeSpan.Days.ToString());
        }
        private LocExtension ConvertToDaysAndHoursPassed(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParams(
                resourceKey: "TimeConverter_PassedTimeDaysAndHours",
                new Dictionary<string, string>()
                {
                    {"DayCount", timeSpan.Days.ToString()},
                    {"HourCount", timeSpan.Hours.ToString()}
                });
        }
        private LocExtension ConvertToHoursPassed(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_PassedTimeHours",
                paramName: "HourCount",
                value: timeSpan.Hours.ToString());
        }
        private LocExtension ConvertToHoursAndMinutesPassed(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParams(
                resourceKey: "TimeConverter_PassedTimeHoursAndMinutes",
                paramValues: new Dictionary<string, string>
                {
                    {"HourCount", timeSpan.Hours.ToString()},
                    {"MinuteCount", timeSpan.Minutes.ToString()},
                });
        }
        private LocExtension ConvertToMinutesPassed(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_PassedTimeMinutes",
                paramName: "MinuteCount",
                value: timeSpan.Minutes.ToString());
        }
        private LocExtension ConvertToMinutesAndSecondsPassed(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParams(
                resourceKey: "TimeConverter_PassedTimeMinutesAndSeconds",
                paramValues: new Dictionary<string, string>
                {
                    {"MinuteCount", timeSpan.Minutes.ToString()},
                    {"SecondCount", timeSpan.Seconds.ToString()},
                });
        }
        private LocExtension ConvertToSecondsPassed(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_PassedTimeSeconds",
                paramName: "SecondCount",
                value: timeSpan.Seconds.ToString());
        }
        private LocExtension ConvertToTimeSpan(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParams(
                resourceKey: "TimeConverter_TimeSpan",
                paramValues: new Dictionary<string, string>
                {
                    {"HourCount", timeSpan.Hours.ToString()},
                    {"MinuteCount", timeSpan.Minutes.ToString("D2")}
                });
        }
        private LocExtension ConvertToTimeSpanSeconds(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParams(
                resourceKey: "TimeConverter_TimeSpan",
                paramValues: new Dictionary<string, string>
                {
                    {"HourCount", timeSpan.Minutes.ToString("D2")},
                    {"MinuteCount", timeSpan.Seconds.ToString("D2")}
                });
        }
        private LocExtension ConvertToMinutesAgoShort(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_AgoMinutesShort",
                paramName: "MinuteCount",
                value: timeSpan.Minutes.ToString());
        }
        private LocExtension ConvertToHoursAgoShort(TimeSpan timeSpan)
        {
            return GetLocForResourceWithParam(
                resourceKey: "TimeConverter_AgoHoursShort",
                paramName: "HourCount",
                value: timeSpan.Hours.ToString());
        }
        #endregion
    }
}
