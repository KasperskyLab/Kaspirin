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
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_ConfirmButton, Type = typeof(DateTimePopupFooterButton))]
    [TemplatePart(Name = PART_CancelButton, Type = typeof(DateTimePopupFooterButton))]
    [TemplatePart(Name = PART_DayPicker, Type = typeof(DateTimePopupSelector))]
    [TemplatePart(Name = PART_MonthPicker, Type = typeof(DateTimePopupSelector))]
    [TemplatePart(Name = PART_YearPicker, Type = typeof(DateTimePopupSelector))]
    [TemplatePart(Name = PART_HourPicker, Type = typeof(DateTimePopupSelector))]
    [TemplatePart(Name = PART_MinutePicker, Type = typeof(DateTimePopupSelector))]
    [TemplatePart(Name = PART_MeridianPicker, Type = typeof(DateTimePopupSelector))]
    internal sealed class DateTimePopupPresenter : Control
    {
        public const string PART_DayPicker = "PART_DayPicker";
        public const string PART_MonthPicker = "PART_MonthPicker";
        public const string PART_YearPicker = "PART_YearPicker";
        public const string PART_HourPicker = "PART_HourPicker";
        public const string PART_MinutePicker = "PART_MinutePicker";
        public const string PART_MeridianPicker = "PART_MeridianPicker";
        public const string PART_ConfirmButton = "PART_ConfirmButton";
        public const string PART_CancelButton = "PART_CancelButton";

        public DateTimePopupPresenter()
        {
            _dateTimeDisplayInfo = LocalizationManager.Current.DisplayCulture.CultureInfo.DateTimeFormat;
            _dateTimeFormatInfo = LocalizationManager.Current.FormatCulture.CultureInfo.DateTimeFormat;
            _hasMeridian = !string.IsNullOrEmpty(_dateTimeFormatInfo.AMDesignator) ||
                           !string.IsNullOrEmpty(_dateTimeFormatInfo.PMDesignator);

            Loaded += OnLoaded;
        }

        #region MaxDateTime

        public DateTime MaxDateTime
        {
            get { return (DateTime)GetValue(MaxDateTimeProperty); }
            set { SetValue(MaxDateTimeProperty, value); }
        }

        public static readonly DependencyProperty MaxDateTimeProperty =
            DependencyProperty.Register("MaxDateTime", typeof(DateTime), typeof(DateTimePopupPresenter),
                new PropertyMetadata(DateTime.MaxValue, OnMaxDateTimeChanged));

        private static void OnMaxDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupPresenter)d).ResetYears();
            ((DateTimePopupPresenter)d).UpdatePickers();
        }

        #endregion

        #region MinDateTime

        public DateTime MinDateTime
        {
            get { return (DateTime)GetValue(MinDateTimeProperty); }
            set { SetValue(MinDateTimeProperty, value); }
        }

        public static readonly DependencyProperty MinDateTimeProperty =
            DependencyProperty.Register("MinDateTime", typeof(DateTime), typeof(DateTimePopupPresenter),
                new PropertyMetadata(DateTime.MinValue, OnMinDateTimeChanged));

        private static void OnMinDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupPresenter)d).ResetYears();
            ((DateTimePopupPresenter)d).CoerceSelectedDateTime();
            ((DateTimePopupPresenter)d).UpdatePickers();
        }

        #endregion

        #region SelectedDateTime

        public DateTime SelectedDateTime
        {
            get { return (DateTime)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime), typeof(DateTimePopupPresenter),
                new PropertyMetadata(default(DateTime), OnSelectedDateTimeChanged));

        private static void OnSelectedDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupPresenter)d).UpdatePickers();
        }

        #endregion

        #region SelectionMode

        public DateTimeInputSelectionMode SelectionMode
        {
            get { return (DateTimeInputSelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(DateTimeInputSelectionMode), typeof(DateTimePopupPresenter),
                new PropertyMetadata(DateTimeInputSelectionMode.Date, OnSelectionModeChanged));

        private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupPresenter)d).UpdatePickers();
        }

        #endregion

        public event Action Confirmed = () => { };

        public event Action Cancelled = () => { };

        public override void OnApplyTemplate()
        {
            _confirmButton = (DateTimePopupFooterButton)GetTemplateChild(PART_ConfirmButton);
            _confirmButton.IsDefault = true;
            _confirmButton.Click += ConfirmButtonOnClick;

            _cancelButton = (DateTimePopupFooterButton)GetTemplateChild(PART_CancelButton);
            _cancelButton.IsCancel = true;
            _cancelButton.Click += CancelButtonOnClick;

            _dayPicker = (DateTimePopupSelector)GetTemplateChild(PART_DayPicker);
            _dayPicker.SelectedItemChanged += OnPickerSelectedItemChanged;

            _monthPicker = (DateTimePopupSelector)GetTemplateChild(PART_MonthPicker);
            _monthPicker.SelectedItemChanged += OnPickerSelectedItemChanged;

            _yearPicker = (DateTimePopupSelector)GetTemplateChild(PART_YearPicker);
            _yearPicker.SelectedItemChanged += OnPickerSelectedItemChanged;

            _hourPicker = (DateTimePopupSelector)GetTemplateChild(PART_HourPicker);
            _hourPicker.SelectedItemChanged += OnPickerSelectedItemChanged;

            _minutePicker = (DateTimePopupSelector)GetTemplateChild(PART_MinutePicker);
            _minutePicker.SelectedItemChanged += OnPickerSelectedItemChanged;

            _meridianPicker = (DateTimePopupSelector)GetTemplateChild(PART_MeridianPicker);
            _meridianPicker.SelectedItemChanged += OnPickerSelectedItemChanged;
        }

        public void SetFocus()
        {
            _confirmButton?.Focus();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdatePickers();
        }

        private void OnPickerSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            UpdateSelectedDate();
        }

        private void ConfirmButtonOnClick(object sender, RoutedEventArgs e)
        {
            UpdateSelectedDate();

            Confirmed.Invoke();
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            Cancelled.Invoke();
        }

        private void UpdateSelectedDate()
        {
            if (_isUpdating)
            {
                return;
            }

            var hour = 0;
            var minute = 0;
            var year = 1;
            var month = 1;
            var day = 1;

            if (_hourPicker != null && _hourPicker.IsVisible)
            {
                hour = GetPickerValue(_hourPicker);
            }

            if (_minutePicker != null && _minutePicker.IsVisible)
            {
                minute = GetPickerValue(_minutePicker);
            }

            if (_meridianPicker != null && _meridianPicker.IsVisible && _hasMeridian)
            {
                var meridian = GetPickerValue(_meridianPicker);
                if (meridian > 0)
                {
                    hour += 12;
                }
            }

            if (_yearPicker != null && _yearPicker.IsVisible)
            {
                year = GetPickerValue(_yearPicker);
            }

            if (_monthPicker != null && _monthPicker.IsVisible)
            {
                month = GetPickerValue(_monthPicker);
            }

            if (_dayPicker != null && _dayPicker.IsVisible)
            {
                day = GetPickerValue(_dayPicker);
            }

            var daysInMonth = DateTime.DaysInMonth(year, month);
            if (daysInMonth < day)
            {
                day = daysInMonth;
            }

            var newDate = new DateTime(year, month, day, hour, minute, 0);

            SelectedDateTime = new DateTime(Math.Max(MinDateTime.Ticks, Math.Min(MaxDateTime.Ticks, newDate.Ticks)));
        }

        private void UpdatePickers()
        {
            _isUpdating = true;

            UpdateHours();
            UpdateMinutes();
            UpdateMeridian();

            UpdateDays();
            UpdateMonths();
            UpdateYears();

            _isUpdating = false;
        }

        private void UpdateHours()
        {
            if (_hourPicker == null)
            {
                return;
            }

            if (SelectionMode == DateTimeInputSelectionMode.Time)
            {
                _hourPicker.Visibility = Visibility.Visible;

                var hoursCount = _hasMeridian ? 12 : 24;

                if (_hourPicker.Items.Count != hoursCount)
                {
                    _hourPicker.Items.Clear();

                    var hourFrom = 0;
                    var hourTo = _hasMeridian ? 11 : 23;

                    for (var hour = hourFrom; hour <= hourTo; hour++)
                    {
                        var displayHour = _hasMeridian && hour == 0 ? 12 : hour;

                        _hourPicker.Items.Add(new DateTimePopupItem(hour, displayHour.ToString()));
                    }
                }

                var currentHour = SelectedDateTime.Hour;
                if (currentHour > 11 && _hasMeridian)
                {
                    currentHour = SelectedDateTime.Hour - 12;
                }

                _hourPicker.SelectedItem = _hourPicker.Items.Single(i => i.Value.Equals(currentHour));
            }
            else
            {
                _hourPicker.Items.Clear();
                _hourPicker.SelectedItem = null;
                _hourPicker.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateMinutes()
        {
            if (_minutePicker == null)
            {
                return;
            }

            if (SelectionMode == DateTimeInputSelectionMode.Time)
            {
                _minutePicker.Visibility = Visibility.Visible;

                if (_minutePicker.Items.Count == 0)
                {
                    var minutesFrom = 0;
                    var minutesTo = 59;

                    for (var minute = minutesFrom; minute <= minutesTo; minute += 5)
                    {
                        _minutePicker.Items.Add(new DateTimePopupItem(minute));
                    }
                }

                _minutePicker.SelectedItem = _minutePicker.Items.OrderBy(x => Math.Abs(x.Value - SelectedDateTime.Minute)).First();
            }
            else
            {
                _minutePicker.Items.Clear();
                _minutePicker.SelectedItem = null;
                _minutePicker.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateMeridian()
        {
            if (_meridianPicker == null)
            {
                return;
            }

            if (SelectionMode == DateTimeInputSelectionMode.Time && _hasMeridian)
            {
                _meridianPicker.Visibility = Visibility.Visible;

                if (_meridianPicker.Items.Count == 0)
                {
                    _meridianPicker.Items.Add(new DateTimePopupItem(0, _dateTimeFormatInfo.AMDesignator));
                    _meridianPicker.Items.Add(new DateTimePopupItem(1, _dateTimeFormatInfo.PMDesignator));
                    _meridianPicker.SelectedItem = SelectedDateTime.Hour < 12 ? _meridianPicker.Items[0] : _meridianPicker.Items[1];
                }
            }
            else
            {
                _meridianPicker.Items.Clear();
                _meridianPicker.SelectedItem = null;
                _meridianPicker.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateDays()
        {
            if (_dayPicker == null)
            {
                return;
            }

            if (SelectionMode == DateTimeInputSelectionMode.Date)
            {
                _dayPicker.Visibility = Visibility.Visible;

                var daysInMonth = DateTime.DaysInMonth(
                    year: SelectedDateTime.Year,
                    month: SelectedDateTime.Month);

                if (_dayPicker.Items.Count != daysInMonth)
                {
                    _dayPicker.Items.Clear();

                    var dayFrom = 1;
                    var dayTo = daysInMonth;

                    for (var day = dayFrom; day <= daysInMonth; day++)
                    {
                        _dayPicker.Items.Add(new DateTimePopupItem(day));
                    }
                }

                _dayPicker.SelectedItem = _dayPicker.Items.Single(i => i.Value.Equals(SelectedDateTime.Day));
            }
            else
            {
                _dayPicker.Items.Clear();
                _dayPicker.SelectedItem = null;
                _dayPicker.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateMonths()
        {
            if (_monthPicker == null)
            {
                return;
            }

            if (SelectionMode == DateTimeInputSelectionMode.Date)
            {
                _monthPicker.Visibility = Visibility.Visible;

                if (_monthPicker.Items.Count == 0)
                {
                    var monthFrom = 1;
                    var monthTo = 12;

                    for (var month = monthFrom; month <= monthTo; month++)
                    {
                        var monthName = _dateTimeDisplayInfo.GetMonthName(month);

                        _monthPicker.Items.Add(new DateTimePopupItem(month, monthName));
                    }
                }

                _monthPicker.SelectedItem = _monthPicker.Items.Single(i => i.Value.Equals(SelectedDateTime.Month));
            }
            else
            {
                _monthPicker.Items.Clear();
                _monthPicker.SelectedItem = null;
                _monthPicker.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateYears()
        {
            if (_yearPicker == null)
            {
                return;
            }

            if (SelectionMode == DateTimeInputSelectionMode.Date)
            {
                _yearPicker.Visibility = Visibility.Visible;

                if (_yearPicker.Items.Count == 0)
                {
                    var yearFrom = MinDateTime.Year;
                    var yearTo = MaxDateTime.Year;

                    for (var year = yearFrom; year <= yearTo; year++)
                    {
                        _yearPicker.Items.Add(new DateTimePopupItem(year));
                    }
                }

                _yearPicker.SelectedItem = _yearPicker.Items.Single(i => i.Value.Equals(SelectedDateTime.Year));
            }
            else
            {
                _yearPicker.Items.Clear();
                _yearPicker.SelectedItem = null;
                _yearPicker.Visibility = Visibility.Collapsed;
            }
        }

        private void CoerceSelectedDateTime()
        {
            SelectedDateTime = new DateTime(Math.Max(MinDateTime.Ticks, Math.Min(MaxDateTime.Ticks, SelectedDateTime.Ticks)));
        }

        private void ResetYears()
        {
            if (_yearPicker == null)
            {
                return;
            }

            _yearPicker.Items.Clear();
        }

        private static int GetPickerValue(DateTimePopupSelector selector)
        {
            return selector.SelectedItem switch
            {
                null => 1,
                _ => selector.SelectedItem.Value
            };
        }

        private DateTimePopupSelector? _dayPicker;
        private DateTimePopupSelector? _monthPicker;
        private DateTimePopupSelector? _yearPicker;
        private DateTimePopupSelector? _hourPicker;
        private DateTimePopupSelector? _minutePicker;
        private DateTimePopupSelector? _meridianPicker;
        private DateTimePopupFooterButton? _confirmButton;
        private DateTimePopupFooterButton? _cancelButton;
        private bool _isUpdating;

        private readonly DateTimeFormatInfo _dateTimeDisplayInfo;
        private readonly DateTimeFormatInfo _dateTimeFormatInfo;
        private readonly bool _hasMeridian;
    }
}
