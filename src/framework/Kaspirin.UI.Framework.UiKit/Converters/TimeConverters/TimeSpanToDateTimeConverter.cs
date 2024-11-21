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
    public sealed class TimeSpanToDateTimeConverter : ValueConverterMarkupExtension<TimeSpanToDateTimeConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                TimeSpan timeSpan => ToDateTimeSafe(timeSpan),
                _ => value
            };
        }

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                DateTime dateTime => new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                _ => value
            };
        }

        private static DateTime ToDateTimeSafe(TimeSpan value)
        {
            if (value.Hours < 0 || value.Hours > 24 ||
                value.Minutes < 0 || value.Minutes > 59 ||
                value.Seconds < 0 || value.Seconds > 59)
            {
                return DateTime.MinValue;
            }

            return new DateTime(1, 1, 1, value.Hours, value.Minutes, value.Seconds);
        }
    }
}
