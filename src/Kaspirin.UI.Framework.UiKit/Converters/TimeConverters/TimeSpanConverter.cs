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
using System.Collections.Generic;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Converters.TimeConverters.Validation;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters;

[ValueConversion(typeof(TimeSpan), typeof(string))]
public sealed class TimeSpanConverter : BaseTimeConverter<TimeSpan>
{
    public TimeSpanConverter()
    {
        Format = TimeSpanConverterFormat.TimePassed;
        Validator = DefaultTimeSpanValidator.Instance;
    }

    public TimeSpanConverterFormat Format { get; set; }

    public override LocExtension Convert(TimeSpan timeSpan)
        => Format switch
        {
            TimeSpanConverterFormat.TimePassed => ToDurationWithPartsAndUnits(timeSpan),
            TimeSpanConverterFormat.TimeLeft => ToTimeLeft(timeSpan),
            TimeSpanConverterFormat.ShortTime => ToShortTime(timeSpan),
            TimeSpanConverterFormat.TimeSpan => ToTimeSpan(timeSpan),
            TimeSpanConverterFormat.TimeSpanSeconds => ToTimeSpanSeconds(timeSpan),
            TimeSpanConverterFormat.ShortTimePast => ToShortTimePast(timeSpan),
            _ => throw new UnexpectedValueException(Format)
        };

    private LocExtension ToTimeSpan(TimeSpan timeSpan)
        => ConvertToTimeSpan(timeSpan);

    private LocExtension ToTimeSpanSeconds(TimeSpan timeSpan)
        => ConvertToTimeSpanSeconds(timeSpan);

    private LocExtension ToShortTime(TimeSpan timeSpan)
    {
        var dateTimeConverter = new DateTimeConverter
        {
            Format = DateTimeConverterFormat.ShortTime,
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

        if (timeSpan.TotalHours < 1)
        {
            return timeSpan.Seconds != 0
                ? ConvertToMinutesAndSecondsPassed(timeSpan)
                : ConvertToMinutesPassed(timeSpan);
        }

        if (timeSpan.TotalDays < 1)
        {
            return timeSpan.Minutes != 0
                ? ConvertToHoursAndMinutesPassed(timeSpan)
                : ConvertToHoursPassed(timeSpan);
        }

        return timeSpan.Hours != 0
            ? ConvertToDaysAndHoursPassed(timeSpan)
            : ConvertToDaysPassed(timeSpan);
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

    private LocExtension ToShortTimePast(TimeSpan timeSpan)
        => timeSpan.TotalHours switch
        {
            < 1 => ConvertToMinutesAgoShort(timeSpan),
            _ => ConvertToHoursAgoShort(timeSpan)
        };

    #region Localization methods

    private LocExtension ConvertToLessMinutesLeft()
        => GetLocForResource(resourceKey: "TimeConverter_RemainingTimeLessMinute");

    private LocExtension ConvertToMinutesLeft(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_RemainingTimeMinutes",
            paramName: "MinuteCount",
            value: timeSpan.Minutes.ToString());

    private LocExtension ConvertToHoursLeft(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_RemainingTimeHours",
            paramName: "HourCount",
            value: timeSpan.Hours.ToString());

    private LocExtension ConvertToDaysLeft(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_RemainingTimeDays",
            paramName: "DayCount",
            value: timeSpan.Days.ToString());

    private LocExtension ConvertToDaysPassed(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_PassedTimeDays",
            paramName: "DayCount",
            value: timeSpan.Days.ToString());

    private LocExtension ConvertToDaysAndHoursPassed(TimeSpan timeSpan)
        => GetLocForResourceWithParams(
            resourceKey: "TimeConverter_PassedTimeDaysAndHours",
            new Dictionary<string, string>()
            {
                { "DayCount", timeSpan.Days.ToString() },
                { "HourCount", timeSpan.Hours.ToString() }
            });

    private LocExtension ConvertToHoursPassed(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_PassedTimeHours",
            paramName: "HourCount",
            value: timeSpan.Hours.ToString());

    private LocExtension ConvertToHoursAndMinutesPassed(TimeSpan timeSpan)
        => GetLocForResourceWithParams(
            resourceKey: "TimeConverter_PassedTimeHoursAndMinutes",
            paramValues: new Dictionary<string, string>
            {
                { "HourCount", timeSpan.Hours.ToString() },
                { "MinuteCount", timeSpan.Minutes.ToString() },
            });

    private LocExtension ConvertToMinutesPassed(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_PassedTimeMinutes",
            paramName: "MinuteCount",
            value: timeSpan.Minutes.ToString());

    private LocExtension ConvertToMinutesAndSecondsPassed(TimeSpan timeSpan)
        => GetLocForResourceWithParams(
            resourceKey: "TimeConverter_PassedTimeMinutesAndSeconds",
            paramValues: new Dictionary<string, string>
            {
                { "MinuteCount", timeSpan.Minutes.ToString() },
                { "SecondCount", timeSpan.Seconds.ToString() },
            });

    private LocExtension ConvertToSecondsPassed(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_PassedTimeSeconds",
            paramName: "SecondCount",
            value: timeSpan.Seconds.ToString());

    private LocExtension ConvertToTimeSpan(TimeSpan timeSpan)
        => GetLocForResourceWithParams(
            resourceKey: "TimeConverter_TimeSpan",
            paramValues: new Dictionary<string, string>
            {
                { "HourCount", timeSpan.Hours.ToString() },
                { "MinuteCount", timeSpan.Minutes.ToString("D2") }
            });

    private LocExtension ConvertToTimeSpanSeconds(TimeSpan timeSpan)
        => GetLocForResourceWithParams(
            resourceKey: "TimeConverter_TimeSpan",
            paramValues: new Dictionary<string, string>
            {
                { "HourCount", timeSpan.Minutes.ToString("D2") },
                { "MinuteCount", timeSpan.Seconds.ToString("D2") }
            });

    private LocExtension ConvertToMinutesAgoShort(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_AgoMinutesShort",
            paramName: "MinuteCount",
            value: timeSpan.Minutes.ToString());

    private LocExtension ConvertToHoursAgoShort(TimeSpan timeSpan)
        => GetLocForResourceWithParam(
            resourceKey: "TimeConverter_AgoHoursShort",
            paramName: "HourCount",
            value: timeSpan.Hours.ToString());

    #endregion
}
