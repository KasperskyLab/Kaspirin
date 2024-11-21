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

using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = "PART_Counter", Type = typeof(TextBlock))]
    public sealed class BadgeCounter : Badge
    {
        public BadgeCounter()
        {
            this.WhenLoaded(UpdateCounterText);
        }

        #region MaxCounter

        public int MaxCounter
        {
            get { return (int)GetValue(MaxCounterProperty); }
            set { SetValue(MaxCounterProperty, value); }
        }

        public static readonly DependencyProperty MaxCounterProperty =
            DependencyProperty.Register("MaxCounter", typeof(int), typeof(BadgeCounter),
                new FrameworkPropertyMetadata(UIKitConstants.BadgeCounterMaxCounter, FrameworkPropertyMetadataOptions.None, OnCounterChanged, CoerceMaxCounter));

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

        #region Counter

        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register("Counter", typeof(int), typeof(BadgeCounter),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, OnCounterChanged, CoerceCounter));

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

        #region IsOverflow

        public bool IsOverflow
        {
            get { return (bool)GetValue(IsOverflowProperty); }
            private set { SetValue(_isOverflowPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _isOverflowPropertyKey =
            DependencyProperty.RegisterReadOnly("IsOverflow", typeof(bool), typeof(BadgeCounter), new PropertyMetadata(false));

        public static readonly DependencyProperty IsOverflowProperty =
            _isOverflowPropertyKey.DependencyProperty;

        #endregion

        public override void OnApplyTemplate()
        {
            _counterTb = (TextBlock)GetTemplateChild("PART_Counter");

            UpdateCounterText();
        }

        private static void OnCounterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BadgeCounter)d).UpdateCounterText();
        }

        private void UpdateCounterText()
        {
            IsOverflow = Counter >= MaxCounter;

            if (_counterTb != null)
            {
                _counterTb.Text = (IsOverflow ? MaxCounter - 1 : Counter).ToString();
            }
        }

        private TextBlock? _counterTb;
    }
}
