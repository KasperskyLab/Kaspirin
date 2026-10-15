// Copyright © 2025 AO Kaspersky Lab.
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
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Converters.TimeConverters.Validation;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters;

[ValueConversion(typeof(DateTime), typeof(string))]
public sealed class DateTimeAgeConverter : BaseTimeConverter<DateTime>
{
    public DateTimeAgeConverter()
    {
        Validator = DefaultDateTimeValidator.Instance;
    }

    public bool UtcTimeToLocalTime { get; set; }

    public override LocExtension Convert(DateTime dateTime)
    {
        if (UtcTimeToLocalTime)
        {
            dateTime = dateTime.ToLocalTimeSafe(fromUtc: true);
        }

        var dateTimeAge = dateTime.TryGetDateTimeAge();

        return dateTimeAge is null
            ? GetLocForResource("TimeConverter_Never")
            : ConvertToDateTimeAge(dateTimeAge);
    }

    private LocExtension ConvertToDateTimeAge(DateTimeAge dateTimeAge)
        => dateTimeAge.Estimate switch
        {
            DateTimeAgeEstimate.Future => GetLocForResourceWithParam(
                resourceKey: "TimeConverter_Value",
                paramName: "Value",
                value: dateTimeAge.EstimatedTime.ToFormat(GeneralDateShortTimeFormatPattern)),
            DateTimeAgeEstimate.SecondsAgo => GetLocForResource(resourceKey: "TimeConverter_AgoSeconds"),
            DateTimeAgeEstimate.MinutesAgo => GetLocForResourceWithParam(
                resourceKey: "TimeConverter_AgoMinutes",
                paramName: "Value",
                value: dateTimeAge.TotalMinutes.ToString()),
            DateTimeAgeEstimate.HoursAgo => GetLocForResourceWithParam(
                resourceKey: "TimeConverter_AgoHours",
                paramName: "Value",
                value: dateTimeAge.TotalHours.ToString()),
            DateTimeAgeEstimate.Yesterday => GetLocForResource(resourceKey: "TimeConverter_AgoYesterday"),
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
            _ => throw new UnexpectedValueException(dateTimeAge.Estimate)
        };

    private const string GeneralDateShortTimeFormatPattern = "g";
}
