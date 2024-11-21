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
using System.Windows.Data;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class InputCaretBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush originBrush)
            {
                //since we have no way to reduce the height of the caret,
                //we reduce its visible part by 20% with custom LinearGradientBrush

                var coercedBrush = new LinearGradientBrush();
                coercedBrush.GradientStops.Add(new GradientStop(originBrush.Color, 0));
                coercedBrush.GradientStops.Add(new GradientStop(originBrush.Color, 0.8));
                coercedBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 0.8));
                coercedBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

                return coercedBrush;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
