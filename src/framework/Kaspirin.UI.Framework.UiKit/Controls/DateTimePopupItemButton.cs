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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class DateTimePopupItemButton : Button
    {
        public DateTimePopupItemButton(double offset, double centerOffset, double minOffset, double maxOffset)
        {
            if (minOffset >= maxOffset)
            {
                throw new InvalidOperationException($"{nameof(maxOffset)} must be greater than {nameof(minOffset)}");
            }

            _minOffset = minOffset;
            _maxOffset = maxOffset;
            _animation = new DateTimePopupItemAnimation(this, minOffset, maxOffset);
            _animation.Completed += SetIsSelected;

            _thisButton = this;

            _centerOffset = Math.Max(minOffset, Math.Min(centerOffset, maxOffset));
            CurrentOffset = Math.Max(minOffset, Math.Min(offset, maxOffset));
        }

        #region CurrentOffset

        public double CurrentOffset
        {
            get { return (double)GetValue(CurrentOffsetProperty); }
            private set { SetValue(CurrentOffsetProperty, value); }
        }

        public static readonly DependencyProperty CurrentOffsetProperty =
            DependencyProperty.Register("CurrentOffset", typeof(double), typeof(DateTimePopupItemButton),
                new PropertyMetadata(0D, OnCurrentOffsetChanged));

        private static void OnCurrentOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePopupItemButton)d).ValidateOffsetBounds((double)e.OldValue, (double)e.NewValue);
        }

        #endregion

        #region IsAvailable

        public bool IsAvailable
        {
            get { return (bool)GetValue(IsAvailableProperty); }
            private set { SetValue(_isAvailablePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _isAvailablePropertyKey =
            DependencyProperty.RegisterReadOnly("IsAvailable", typeof(bool), typeof(DateTimePopupItemButton),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IsAvailableProperty =
            _isAvailablePropertyKey.DependencyProperty;

        #endregion

        #region IsSelected

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            private set { SetValue(_isSelectedPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _isSelectedPropertyKey =
            DependencyProperty.RegisterReadOnly("IsSelected", typeof(bool), typeof(DateTimePopupItemButton),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsSelectedProperty =
            _isSelectedPropertyKey.DependencyProperty;

        #endregion

        #region TextStyle

        public Style TextStyle
        {
            get { return (Style)GetValue(TextStyleProperty); }
            set { SetValue(TextStyleProperty, value); }
        }

        public static readonly DependencyProperty TextStyleProperty =
            DependencyProperty.Register("TextStyle", typeof(Style), typeof(DateTimePopupItemButton));

        #endregion

        public event Action<DateTimePopupItemButton> MinOffsetAchieved = (b) => { };

        public event Action<DateTimePopupItemButton> MaxOffsetAchieved = (b) => { };

        public event Action<DateTimePopupItemButton> Selected = (b) => { };

        public void Select()
        {
            var changeDelta = _centerOffset - CurrentOffset;

            _thisButton.Move(changeDelta, this);
        }

        public void SetNextButton(DateTimePopupItemButton? nextButton)
        {
            _nextButton = nextButton;
        }

        private void Move(double changeDelta, DateTimePopupItemButton initiator)
        {
            if (!IsAvailable)
            {
                _animation.Requested();
                return;
            }

            var isInitiator = _thisButton == initiator;
            if (isInitiator && changeDelta == 0)
            {
                if (IsSelected == true)
                {
                    return;
                }

                IsSelected = true;
            }
            else if (!isInitiator && changeDelta == 0)
            {
                IsSelected = false;
            }
            else
            {
                IsAvailable = false;
                IsSelected = false;

                initiator.Selected -= HoldOffset;
                initiator.Selected += HoldOffset;

                if (isInitiator)
                {
                    _animation.Start(CurrentOffset, changeDelta);
                }
                else
                {
                    _thisButton.SetBinding(CurrentOffsetProperty, new Binding()
                    {
                        Source = initiator,
                        Path = _offsetPath,
                        Converter = new OffsetConverter(initiator, _thisButton, _minOffset, _maxOffset)
                    });
                }
            }

            _nextButton?.Move(changeDelta, initiator);
        }

        private void HoldOffset(DateTimePopupItemButton button)
        {
            _animation.Hold(CurrentOffset);

            IsAvailable = true;
        }

        private void SetIsSelected()
        {
            IsSelected = true;

            Selected.Invoke(this);
        }

        private void ValidateOffsetBounds(double oldValue, double newValue)
        {
            var halfLenght = Math.Abs(_centerOffset - _maxOffset);
            var changeLenght = Math.Abs(newValue - oldValue);

            if (changeLenght > halfLenght)
            {
                if (newValue < _centerOffset)
                {
                    MaxOffsetAchieved.Invoke(this);
                }
                else
                {
                    MinOffsetAchieved.Invoke(this);
                }
            }
        }

        private sealed class OffsetConverter : IValueConverter
        {
            public OffsetConverter(DateTimePopupItemButton source, DateTimePopupItemButton target, double minOffset, double maxOffset)
            {
                _source = source;
                _sourceInitialOffset = source.CurrentOffset;

                _target = target;
                _targetInitialOffset = _target.CurrentOffset;

                _minOffset = minOffset;
                _maxOffset = maxOffset;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var sourceCurrentOffset = (double)value;

                var delta = sourceCurrentOffset - _sourceInitialOffset;
                var currentOffset = _targetInitialOffset + delta;

                if (_maxOffset <= currentOffset)
                {
                    currentOffset = _minOffset + Math.Abs(currentOffset - _maxOffset);
                }
                else if (_minOffset > currentOffset)
                {
                    currentOffset = _maxOffset - Math.Abs(currentOffset - _minOffset);
                }

                return currentOffset;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            private readonly double _minOffset;
            private readonly double _maxOffset;
            private readonly double _sourceInitialOffset;
            private readonly double _targetInitialOffset;
            private readonly DateTimePopupItemButton _source;
            private readonly DateTimePopupItemButton _target;
        }

        private readonly DateTimePopupItemAnimation _animation;
        private readonly DateTimePopupItemButton _thisButton;
        private readonly double _minOffset;
        private readonly double _maxOffset;
        private readonly double _centerOffset;
        private readonly PropertyPath _offsetPath = new(CurrentOffsetProperty);

        private DateTimePopupItemButton? _nextButton;
    }
}
