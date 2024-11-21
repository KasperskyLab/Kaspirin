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
    public class Divider : Control
    {
        static Divider()
        {
            HorizontalAlignmentProperty.OverrideMetadata(
                typeof(Divider),
                new FrameworkPropertyMetadata((d, e) => { }, CoerceHorizontalAlignment));

            VerticalAlignmentProperty.OverrideMetadata(
                typeof(Divider),
                new FrameworkPropertyMetadata((d, e) => { }, CoerceVerticalAlignment));
        }

        private static object CoerceHorizontalAlignment(DependencyObject d, object baseValue)
        {
            var divider = (Divider)d;

            if (double.IsNaN(divider.Length) && divider.Orientation == Orientation.Horizontal)
            {
                return HorizontalAlignment.Stretch;
            }

            return baseValue;
        }

        private static object CoerceVerticalAlignment(DependencyObject d, object baseValue)
        {
            var divider = (Divider)d;

            if (double.IsNaN(divider.Length) && divider.Orientation == Orientation.Vertical)
            {
                return VerticalAlignment.Stretch;
            }

            return baseValue;
        }

        #region Orientation

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Divider), new PropertyMetadata(Orientation.Horizontal, OnDividerChanged));

        #endregion

        #region Length

        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register("Length", typeof(double), typeof(Divider), new PropertyMetadata(double.NaN, OnDividerChanged));

        #endregion

        private static void OnDividerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lenght = (double)d.GetValue(LengthProperty);

            if (double.IsNaN(lenght))
            {
                d.SetValue(HorizontalAlignmentProperty, d.GetValue(HorizontalAlignmentProperty));
                d.SetValue(VerticalAlignmentProperty, d.GetValue(VerticalAlignmentProperty));
            }
        }
    }
}
