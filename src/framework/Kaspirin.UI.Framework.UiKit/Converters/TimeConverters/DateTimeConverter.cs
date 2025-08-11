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
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Converters.TimeConverters.Validation;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters;

[ValueConversion(typeof(DateTime), typeof(string))]
public sealed class DateTimeConverter : BaseTimeConverter<DateTime>
{
    public DateTimeConverter()
    {
        Format = DateTimeConverterFormat.DateShortTime;
        RecentDaysFormat = DateTimeConverterRecentDaysFormat.None;
        Validator = DefaultDateTimeValidator.Instance;
    }

    public DateTimeConverterFormat Format { get; set; }

    public DateTimeConverterRecentDaysFormat RecentDaysFormat { get; set; }

    public bool UtcTimeToLocalTime { get; set; }

    public override LocExtension Convert(DateTime dateTime)
    {
        if (UtcTimeToLocalTime)
        {
            dateTime = dateTime.ToLocalTimeSafe(fromUtc: true);
        }

        if (RecentDaysFormat is not DateTimeConverterRecentDaysFormat.None)
        {
            if (dateTime.IsToday())
            {
                return ConvertRecentDateTime(dateTime, "TimeConverter_Today", "TimeConverter_TodayPattern");
            }

            if (dateTime.IsYesterday())
            {
                return ConvertRecentDateTime(dateTime, "TimeConverter_Yesterday", "TimeConverter_YesterdayPattern");
            }
        }

        return ConvertDateTime(dateTime, Format);
    }

    private LocExtension ConvertDateTime(DateTime dateTime, DateTimeConverterFormat format)
    {
        var formatPattern = GetFormatPattern(format);
        return GetLocForConstantString(dateTime.ToFormat(formatPattern));
    }

    private LocExtension ConvertRecentDateTime(DateTime dateTime, string staticResourceKey, string patternResourceKey)
        => RecentDaysFormat is DateTimeConverterRecentDaysFormat.Static
            ? GetLocForResource(staticResourceKey)
            : ConvertDateTimeWithPattern(dateTime, RecentDaysFormat, patternResourceKey);

    private LocExtension ConvertDateTimeWithPattern(DateTime dateTime, DateTimeConverterRecentDaysFormat format, string patternResourceKey)
    {
        var formatPattern = GetFormatPattern(format);
        return GetLocForResourceWithParam(
            resourceKey: patternResourceKey,
            paramName: "DateTime",
            value: dateTime.ToFormat(formatPattern));
    }

    private string GetFormatPattern(DateTimeConverterFormat format)
        => format switch
        {
            DateTimeConverterFormat.Date => ShortDateFormatPattern,
            DateTimeConverterFormat.ShortTime => ShortTimeFormatPattern,
            DateTimeConverterFormat.LongTime => LongTimeFormatPattern,
            DateTimeConverterFormat.DateShortTime => GeneralDateShortTimeFormatPattern,
            DateTimeConverterFormat.DateLongTime => GeneralDateLongTimeFormatPattern,
            _ => throw new UnexpectedValueException(format)
        };

    private string GetFormatPattern(DateTimeConverterRecentDaysFormat format)
        => format switch
        {
            DateTimeConverterRecentDaysFormat.Date => ShortDateFormatPattern,
            DateTimeConverterRecentDaysFormat.ShortTime => ShortTimeFormatPattern,
            DateTimeConverterRecentDaysFormat.LongTime => LongTimeFormatPattern,
            DateTimeConverterRecentDaysFormat.DateShortTime => GeneralDateShortTimeFormatPattern,
            DateTimeConverterRecentDaysFormat.DateLongTime => GeneralDateLongTimeFormatPattern,
            _ => throw new UnexpectedValueException(format)
        };

    private const string ShortDateFormatPattern = "d";
    private const string ShortTimeFormatPattern = "t";
    private const string LongTimeFormatPattern = "T";
    private const string GeneralDateShortTimeFormatPattern = "g";
    private const string GeneralDateLongTimeFormatPattern = "G";
}
