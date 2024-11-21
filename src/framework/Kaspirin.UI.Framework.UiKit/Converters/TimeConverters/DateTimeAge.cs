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

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    public sealed class DateTimeAge
    {
        public DateTimeAge(DateTime time, DateTime nowTime)
        {
            EstimatedTime = time;
            _nowTime = nowTime;

            _timeSpan = nowTime >= time ?
                nowTime - time :
                time - nowTime;

            Estimate = GetAgeEstimate(time, _nowTime);
        }

        public DateTime EstimatedTime { get; private set; }

        public DateTimeAgeEstimate Estimate { get; private set; }

        public int TotalSeconds
        {
            get { return (int)Math.Floor(_timeSpan.TotalSeconds); }
        }

        public int TotalMinutes
        {
            get { return (int)Math.Floor(_timeSpan.TotalMinutes); }
        }

        public int TotalHours
        {
            get { return (int)Math.Floor(_timeSpan.TotalHours); }
        }

        public int TotalDays
        {
            get { return (int)Math.Floor(_timeSpan.TotalDays); }
        }

        public int TotalMonths
        {
            get { return (int)Math.Floor(_timeSpan.TotalSeconds / SecondsInMonth); }
        }

        public int TotalYears
        {
            get { return (int)Math.Floor(_timeSpan.TotalSeconds / SecondsInYear); }
        }

        public DateTimeAge AddMonths(int months)
        {
            return new DateTimeAge(EstimatedTime.Add(TimeSpan.FromSeconds(months * SecondsInMonth)), _nowTime);
        }

        private DateTimeAgeEstimate GetAgeEstimate(DateTime localTime, DateTime localNowTime)
        {
            if (localTime > localNowTime)
            {
                return DateTimeAgeEstimate.Future;
            }

            if (TotalMinutes < 1)
            {
                return DateTimeAgeEstimate.SecondsAgo;
            }

            if (TotalHours < 1)
            {
                return DateTimeAgeEstimate.MinutesAgo;
            }

            if (TotalDays < 1)
            {
                return DateTimeAgeEstimate.HoursAgo;
            }

            if (localTime.Date.AddDays(1) == localNowTime.Date)
            {
                return DateTimeAgeEstimate.Yesterday;
            }

            if (TotalSeconds < SecondsInMonth)
            {
                return DateTimeAgeEstimate.DaysAgo;
            }

            return TotalSeconds < SecondsInYear ?
                DateTimeAgeEstimate.MonthsAgo :
                DateTimeAgeEstimate.YearsAgo;
        }

        private const int SecondsInDay = 60 * 60 * 24;
        private const double SecondsInYear = SecondsInDay * 365.25;
        private const double SecondsInMonth = SecondsInYear / 12.0;

        private readonly TimeSpan _timeSpan;
        private readonly DateTime _nowTime;
    }
}