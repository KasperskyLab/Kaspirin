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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Converters.EqualityConverters;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class ChipsItem : SelectorItem
    {
        public ChipsItem()
        {
            this.WhenInitialized(() =>
            {
                SetBinding(_isMultiSelectInternalProperty, new Binding()
                {
                    RelativeSource = new RelativeSource { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(ChipsControl) },
                    Path = ChipsControl.SelectionModeProperty.AsPath(),
                    Mode = BindingMode.OneWay,
                    Converter = _equalityComverter,
                    ConverterParameter = SelectionMode.Single
                });

                SetBinding(MarginProperty, new Binding()
                {
                    RelativeSource = new RelativeSource { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(ChipsControl) },
                    Path = ChipsControl.ModeProperty.AsPath(),
                    Mode = BindingMode.OneWay,
                    Converter = new DelegateConverter<ChipsControlMode>(ConvertModeToMargin)
                });
            });
        }

        #region Counter

        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register("Counter", typeof(int), typeof(ChipsItem),
                new PropertyMetadata(0, null, CoerceCounter));

        private static object CoerceCounter(DependencyObject d, object baseValue)
        {
            var counter = (int)baseValue;
            return counter switch
            {
                < 0 => 0,
                _ => counter
            };
        }

        #endregion

        #region MaxCounter

        public int MaxCounter
        {
            get { return (int)GetValue(MaxCounterProperty); }
            set { SetValue(MaxCounterProperty, value); }
        }

        public static readonly DependencyProperty MaxCounterProperty =
            DependencyProperty.Register("MaxCounter", typeof(int), typeof(ChipsItem),
                new PropertyMetadata(UIKitConstants.ChipsItemMaxCounter, null, CoerceMaxCounter));

        private static object CoerceMaxCounter(DependencyObject d, object baseValue)
        {
            var counter = (int)baseValue;
            return counter switch
            {
                < 1 => 1,
                _ => counter
            };
        }

        #endregion

        #region Icon

        public UIKitIcon_16 Icon
        {
            get { return (UIKitIcon_16)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(UIKitIcon_16), typeof(ChipsItem));

        #endregion

        #region IsMultiSelect

        public bool IsMultiSelect
        {
            get { return (bool)GetValue(IsMultiSelectProperty); }
            private set { SetValue(_isMultiSelectPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _isMultiSelectPropertyKey =
            DependencyProperty.RegisterReadOnly("IsMultiSelect", typeof(bool), typeof(ChipsItem), new PropertyMetadata(false));

        private static readonly DependencyProperty _isMultiSelectInternalProperty =
            DependencyProperty.Register("IsMultiSelectInternal", typeof(bool), typeof(ChipsItem), new PropertyMetadata(false, OnIsMultiSelectChanged));

        private static void OnIsMultiSelectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChipsItem)d).SetValue(_isMultiSelectPropertyKey, e.NewValue);
        }

        public static readonly DependencyProperty IsMultiSelectProperty =
            _isMultiSelectPropertyKey.DependencyProperty;

        #endregion

        #region HasCounter

        public bool HasCounter
        {
            get { return (bool)GetValue(HasCounterProperty); }
            set { SetValue(HasCounterProperty, value); }
        }

        public static readonly DependencyProperty HasCounterProperty =
            DependencyProperty.Register("HasCounter", typeof(bool), typeof(ChipsItem));

        #endregion

        #region HasIcon

        public bool HasIcon
        {
            get { return (bool)GetValue(HasIconProperty); }
            set { SetValue(HasIconProperty, value); }
        }

        public static readonly DependencyProperty HasIconProperty =
            DependencyProperty.Register("HasIcon", typeof(bool), typeof(ChipsItem));

        #endregion

        private static object ConvertModeToMargin(ChipsControlMode orientation)
        {
            return orientation switch
            {
                ChipsControlMode.Wrap => UIKitConstants.ChipsItemMargin,
                ChipsControlMode.Fade => new Thickness()
                {
                    Left = UIKitConstants.ChipsItemMargin.Left,
                    Right = UIKitConstants.ChipsItemMargin.Right
                },
                _ => throw new Exception()
            };
        }

        private static readonly EqualityToBooleanInverseConverter _equalityComverter = new();
    }
}
