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
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    public sealed class DateTimeRangeConverter : MultiValueConverterMarkupExtension<DateTimeRangeConverter>
    {
        public override object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            Guard.ArgumentIsNotNull(values);
            Guard.Argument(values.Count() == 2);
            Guard.ArgumentIsNotNull(values[0]);
            Guard.ArgumentIsInstanceOfType<DateTime>(values[0], "values[0] is DateTime");
            Guard.ArgumentIsNotNull(values[1]);
            Guard.ArgumentIsInstanceOfType<DateTime>(values[1], "values[1] is DateTime");

            var dateFrom = (DateTime)values[0]!;
            var dateTo = (DateTime)values[1]!;

            return Convert(dateFrom, dateTo).ProvideConstantStringValue();
        }

        public static LocExtension Convert(DateTime dateFrom, DateTime dateTo)
        {
            var displayCultureInfo = LocalizationManager.Current.DisplayCulture.CultureInfo;
            var dateRangeParameters = new LocParameterCollection(new Dictionary<string, string>
            {
                {"MonthFrom", displayCultureInfo.DateTimeFormat.MonthGenitiveNames[dateFrom.Month-1]},
                {"MonthTo", displayCultureInfo.DateTimeFormat.MonthGenitiveNames[dateTo.Month-1]},
                {"DayFrom", dateFrom.ToFormat("dd")},
                {"DayTo", dateTo.ToFormat("dd")},
                {"YearFrom", dateFrom.ToFormat("yyyy")},
                {"YearTo", dateTo.ToFormat("yyyy")}
            });

            if (dateFrom.Month == dateTo.Month &&
                dateFrom.Year == dateTo.Year)
            {
                return new LocExtension("TimeConverter_RangePatternDayDifference", UIKitConstants.LocalizationScope)
                {
                    Params = dateRangeParameters
                };
            }

            if (dateFrom.Month != dateTo.Month &&
                dateFrom.Year == dateTo.Year)
            {
                return new LocExtension("TimeConverter_RangePatternDayMonthDifference", UIKitConstants.LocalizationScope)
                {
                    Params = dateRangeParameters
                };
            }

            if (dateFrom.Month != dateTo.Month &&
                dateFrom.Year != dateTo.Year)
            {
                return new LocExtension("TimeConverter_RangePatternDayMonthYearDifference", UIKitConstants.LocalizationScope)
                {
                    Params = dateRangeParameters
                };
            }

            return new LocExtension("TimeConverter_Value", UIKitConstants.LocalizationScope)
            {
                Param = new LocParameter("Value")
                {
                    ParamSource = new Binding
                    {
                        Source = string.Format("{0} - {1}", dateFrom.ToString("d"), dateTo.ToString("d"))
                    }
                },
            };
        }
    }
}