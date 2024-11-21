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
using System.Windows.Media;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Animation.Internals
{
    internal static class AnimatedValueEqualityComparer
    {
        public static new bool Equals(object? x, object? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            switch (x)
            {
                case int when y is int:
                case double when y is double:
                case Thickness when y is Thickness:
                    return x == y;

                case SolidColorBrush xSolidBrush when y is SolidColorBrush ySolidBrush:
                    return xSolidBrush.Color == ySolidBrush.Color;

                default:
                    throw new NotImplementedException($"Unable to compare animated values '{x}' ({x.GetType().Name}) and '{y}' ({x.GetType().Name})");
            }
        }
    }
}
