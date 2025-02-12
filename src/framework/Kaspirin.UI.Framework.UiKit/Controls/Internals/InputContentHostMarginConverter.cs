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
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class InputContentHostMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var margin = (Thickness)values[0];
            var textAlignment = (TextAlignment)values[1];

            return new Thickness
            {
                Bottom = margin.Bottom,
                Top = margin.Top,
                Left = textAlignment == TextAlignment.Left ? margin.Left - (SystemParameters.CaretWidth + 1) : 0,
                Right = textAlignment == TextAlignment.Right ? margin.Right - (SystemParameters.CaretWidth + 1) : 0,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
