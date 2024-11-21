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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = "PART_Container", Type = typeof(FrameworkElement))]
    public class NavigationMenuButton : NavigationMenuButtonBase
    {
        #region Caption

        public object Caption
        {
            get { return GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(object), typeof(NavigationMenuButton));

        #endregion

        #region Description

        public object Description
        {
            get { return GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(object), typeof(NavigationMenuButton));

        #endregion

        #region Level

        public uint Level
        {
            get { return (uint)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(uint), typeof(NavigationMenuButton), new PropertyMetadata(0U));

        #endregion

        public override void OnApplyTemplate()
        {
            var container = GetTemplateChild("PART_Container") as FrameworkElement;
            if (container != null)
            {
                var marginBinding = new MultiBinding();
                marginBinding.Bindings.Add(new Binding() { Source = container, Path = NavigationMenuButtonInternals.MarginLevel1Property.AsPath() });
                marginBinding.Bindings.Add(new Binding() { Source = container, Path = NavigationMenuButtonInternals.MarginLevel2Property.AsPath() });
                marginBinding.Bindings.Add(new Binding() { Source = this, Path = LevelProperty.AsPath() });
                marginBinding.Converter = new MarginConverter();

                container.SetBinding(FrameworkElement.MarginProperty, marginBinding);
            }
        }

        private sealed class MarginConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                var level1Margin = (Thickness)values[0];
                var level2Margin = (Thickness)values[1];
                var currentLevel = (uint)values[2];

                var leftDelta = level2Margin.Left - level1Margin.Left;

                return new Thickness
                {
                    Left = level1Margin.Left + leftDelta * currentLevel,
                    Right = level1Margin.Right,
                    Bottom = level1Margin.Bottom,
                    Top = level1Margin.Top,
                };
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
