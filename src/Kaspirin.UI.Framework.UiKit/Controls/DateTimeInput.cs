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
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Converters.TimeConverters;
using Kaspirin.UI.Framework.UiKit.Extensions.Internals;

using TextInputControl = Kaspirin.UI.Framework.UiKit.Controls.TextInput;
using WpfPopup = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_TextInput, Type = typeof(TextInput))]
[TemplatePart(Name = PART_Popup, Type = typeof(WpfPopup))]
[TemplatePart(Name = PART_PopupPresenter, Type = typeof(DateTimePopupPresenter))]
public sealed class DateTimeInput : TextInputBasedControl
{
    public const string PART_Popup = "PART_Popup";
    public const string PART_PopupPresenter = "PART_PopupPresenter";

    public DateTimeInput()
    {
        _dateTimeInputCalendarAction = new(this);
        _dateConverter = new DelegateConverter<DateTime?>(ConvertDateTimeToString);

        this.WhenLoaded(() => InvalidateIsNullable());
    }

    #region MaxDateTime

    public DateTime MaxDateTime
    {
        get => (DateTime)GetValue(MaxDateTimeProperty);
        set => SetValue(MaxDateTimeProperty, value);
    }

    public static readonly DependencyProperty MaxDateTimeProperty = DependencyProperty.Register(
        nameof(MaxDateTime),
        typeof(DateTime),
        typeof(DateTimeInput),
        new PropertyMetadata(DateTime.MaxValue, OnMaxDateTimeChanged));

    private static void OnMaxDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((DateTimeInput)d).UpdateMinMax();

    #endregion

    #region MinDateTime

    public DateTime MinDateTime
    {
        get => (DateTime)GetValue(MinDateTimeProperty);
        set => SetValue(MinDateTimeProperty, value);
    }

    public static readonly DependencyProperty MinDateTimeProperty = DependencyProperty.Register(
        nameof(MinDateTime),
        typeof(DateTime),
        typeof(DateTimeInput),
        new PropertyMetadata(DateTime.MinValue, OnMinDateTimeChanged));

    private static void OnMinDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((DateTimeInput)d).UpdateMinMax();

    #endregion

    #region Placeholder

    public string? Placeholder
    {
        get => (string?)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder),
        typeof(string),
        typeof(DateTimeInput),
        new PropertyMetadata(default(string)));

    #endregion

    #region SelectionMode

    public DateTimeInputSelectionMode SelectionMode
    {
        get => (DateTimeInputSelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionModeProperty, value);
    }

    public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
        nameof(SelectionMode),
        typeof(DateTimeInputSelectionMode),
        typeof(DateTimeInput),
        new PropertyMetadata(DateTimeInputSelectionMode.Date, OnSelectionModeChanged));

    private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((DateTimeInput)d).InvalidateSelectedDate();

    #endregion

    #region SelectedDateTime

    public DateTime? SelectedDateTime
    {
        get => (DateTime?)GetValue(SelectedDateTimeProperty);
        set => SetValue(SelectedDateTimeProperty, value);
    }

    public static readonly DependencyProperty SelectedDateTimeProperty = DependencyProperty.Register(
        nameof(SelectedDateTime),
        typeof(DateTime?),
        typeof(DateTimeInput),
        new FrameworkPropertyMetadata(default(DateTime?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region IsDropDownOpen

    public bool IsDropDownOpen
    {
        get => (bool)GetValue(IsDropDownOpenProperty);
        set => SetValue(IsDropDownOpenProperty, value);
    }

    public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
        nameof(IsDropDownOpen),
        typeof(bool),
        typeof(DateTimeInput),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDropDownOpenChanged, CoerceIsDropDownOpen));

    private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((DateTimeInput)d).OnIsDropDownOpenChanged();

    private static object CoerceIsDropDownOpen(DependencyObject d, object value)
        => value is true ? ((FrameworkElement)d).CoerceWhenLoaded(IsDropDownOpenProperty) : value;

    #endregion

    #region IsValid

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register(
        nameof(IsValid),
        typeof(bool),
        typeof(DateTimeInput),
        new PropertyMetadata(true));

    #endregion

    #region IsNullable

    public bool IsNullable
    {
        get => (bool)GetValue(IsNullableProperty);
        set => SetValue(IsNullableProperty, value);
    }

    public static readonly DependencyProperty IsNullableProperty = DependencyProperty.Register(
        nameof(IsNullable),
        typeof(bool),
        typeof(DateTimeInput),
        new PropertyMetadata(default(bool), OnIsNullableChanged));

    private static void OnIsNullableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((DateTimeInput)d).InvalidateIsNullable();

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _popupPresenter = (DateTimePopupPresenter)GetTemplateChild(PART_PopupPresenter);

        _textInput = (TextInput)GetTemplateChild(PART_TextInput);
        _textInput.SetBinding(TextInputControl.TextProperty, new Binding()
        {
            Source = this,
            Path = DateTimeInput.SelectedDateTimeProperty.AsPath(),
            UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
            Converter = _dateConverter
        });
        _textInput.SetBinding(TextInputControl.PlaceholderProperty, new Binding()
        {
            Source = this,
            Path = PlaceholderProperty.AsPath(),
            Converter = new DelegateConverter(value => new TextInputStringPlaceholder(value as string ?? string.Empty))
        });
        _textInput.LostKeyboardFocus += (s, e) =>
        {
            InvalidateSelectedDate();
        };
        _textInput.TextChanged += (s, e) =>
        {
            InvalidateIsValid();
        };
        _textInput.ActionBar = new()
        {
            _dateTimeInputCalendarAction
        };

        _popup = (WpfPopup)GetTemplateChild(PART_Popup);
        _popup.AllowsTransparency = true;
        _popup.StaysOpen = false;
        _popup.SetBinding(WpfPopup.IsOpenProperty, new Binding()
        {
            Source = this,
            Path = IsDropDownOpenProperty.AsPath(),
            Mode = BindingMode.TwoWay
        });

        _popup.Opened += (s, e) =>
        {
            _dateTimeInputCalendarAction.IsHitTestVisible = false;
            _wasFocused = _textInput.IsFocused;

            _popupPresenter.SetFocus();
        };
        _popup.Closed += (s, e) =>
        {
            _dateTimeInputCalendarAction.IsHitTestVisible = true;
            _textInput.SetFocusFlag = _wasFocused;
        };

        _textInput.WhenLoaded(() =>
        {
            var targetElement = _textInput;
            var innerElement = targetElement
                .FindVisualChildren<FrameworkElement>()
                .FirstOrDefault(Popup.GetIsPopupTarget);

            _popup.PlacementTarget = innerElement ?? targetElement;
        });

        _popupPresenter.SetBinding(DateTimePopupPresenter.MaxDateTimeProperty, new Binding { Source = this, Path = DateTimeInput.MaxDateTimeProperty.AsPath() });
        _popupPresenter.SetBinding(DateTimePopupPresenter.MinDateTimeProperty, new Binding { Source = this, Path = DateTimeInput.MinDateTimeProperty.AsPath() });
        _popupPresenter.SetBinding(DateTimePopupPresenter.SelectionModeProperty, new Binding { Source = this, Path = DateTimeInput.SelectionModeProperty.AsPath() });
        _popupPresenter.Confirmed += () =>
        {
            SelectedDateTime = _popupPresenter.SelectedDateTime;
            IsDropDownOpen = false;
        };

        _popupPresenter.Cancelled += () =>
        {
            IsDropDownOpen = false;
        };
    }

    private void OnIsDropDownOpenChanged()
    {
        if (_popup == null || _popupPresenter == null)
        {
            return;
        }

        if (IsDropDownOpen)
        {
            InvalidateSelectedDate();

            _popupPresenter.SelectedDateTime = SelectedDateTime ?? DateTime.Now;
        }
    }

    private void UpdateMinMax()
    {
        if (!IsLoaded)
        {
            return;
        }

        if (MinDateTime > MaxDateTime)
        {
            MinDateTime = MaxDateTime;
        }
        else
        {
            InvalidateSelectedDate();
        }
    }

    private void InvalidateSelectedDate()
    {
        if (_textInput == null)
        {
            return;
        }

        var dateTimeText = _textInput.Text;

        if (IsNullable && string.IsNullOrEmpty(dateTimeText))
        {
            SelectedDateTime = null;
        }
        else
        {
            if (TryParseDateTime(dateTimeText, out var dateTime))
            {
                SelectedDateTime = new DateTime(Math.Max(MinDateTime.Ticks, Math.Min(MaxDateTime.Ticks, dateTime.Ticks)));
            }
        }

        BindingOperations.GetBindingExpression(_textInput, TextInputControl.TextProperty)?.UpdateTarget();
    }

    private void InvalidateIsValid()
    {
        if (_textInput == null)
        {
            return;
        }

        var dateTimeText = _textInput.Text;

        if (IsNullable && string.IsNullOrEmpty(dateTimeText))
        {
            IsValid = true;
        }
        else
        {
            IsValid = TryParseDateTime(dateTimeText, out var _);
        }
    }

    private void InvalidateIsNullable()
    {
        if (SelectedDateTime == null && !IsNullable)
        {
            SelectedDateTime = DateTime.Now;
        }
    }

    private bool TryParseDateTime(string dateTimeString, out DateTime dateTime)
    {
        if (dateTimeString == null)
        {
            dateTime = default;
            return false;
        }

        var dateTimeFormatString = GetDateTimeFormatString();
        var dateTimeFormatCulture = LocalizationManager.FormatCulture.CultureInfo;

        if (DateTime.TryParseExact(dateTimeString, dateTimeFormatString, dateTimeFormatCulture, DateTimeStyles.AllowWhiteSpaces, out dateTime))
        {
            return true;
        }

        return false;
    }

    private object? ConvertDateTimeToString(DateTime? dateTime)
    {
        if (dateTime == null)
        {
            return null;
        }

        var dateTimeFormat = GetDateTimeFormatString();

        return dateTime.Value.ToFormat(dateTimeFormat);
    }

    private string GetDateTimeFormatString()
    {
        return SelectionMode == DateTimeInputSelectionMode.Date ? "d" : "t";
    }

    private readonly DateTimeInputAction _dateTimeInputCalendarAction;
    private readonly DelegateConverter<DateTime?> _dateConverter;

    private bool _wasFocused;
    private TextInput? _textInput;
    private WpfPopup? _popup;
    private DateTimePopupPresenter? _popupPresenter;
}
