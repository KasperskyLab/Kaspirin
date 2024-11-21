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
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    internal static class WindowScaleHelper
    {
        public static void ScaleContentByDpiChange(Window target, double scaleX, double scaleY)
        {
            ScaleWindowContent(target, scaleX, scaleY, isDpiChange: true);
        }

        public static void ScaleContentByTextScaleChange(Window target, double scale)
        {
            ScaleWindowContent(target, scale, scale, isDpiChange: false);
        }

        public static void ScaleWindowByDpiChange(Window target, double scaleX, double scaleY)
        {
            ScaleWindowSize(target, scaleX, scaleY, isDpiChange: true);
        }

        public static void ScaleWindowByTextScaleChange(Window target, double scale)
        {
            ScaleWindowSize(target, scale, scale, isDpiChange: false);
        }

        private static void ScaleWindowSize(Window target, double scaleX, double scaleY, bool isDpiChange)
        {
            var minWidth = ReplacePropertyValue(target, Window.MinWidthProperty, 0.0);
            var maxWidth = ReplacePropertyValue(target, Window.MaxWidthProperty, double.PositiveInfinity);

            var minHeight = ReplacePropertyValue(target, Window.MinHeightProperty, 0.0);
            var maxHeight = ReplacePropertyValue(target, Window.MaxHeightProperty, double.PositiveInfinity);

            if (!isDpiChange)
            {
                ScalePropertyValue(target, Window.WidthProperty, scaleX);
                ScalePropertyValue(target, Window.HeightProperty, scaleY);
            }

            ReplacePropertyValue(target, Window.MinWidthProperty, HasValue(minWidth) ? ScaleValue(minWidth, scaleX) : minWidth);
            ReplacePropertyValue(target, Window.MaxWidthProperty, HasValue(maxWidth) ? ScaleValue(maxWidth, scaleX) : maxWidth);
            ReplacePropertyValue(target, Window.MinHeightProperty, HasValue(minHeight) ? ScaleValue(minHeight, scaleY) : minHeight);
            ReplacePropertyValue(target, Window.MaxHeightProperty, HasValue(maxHeight) ? ScaleValue(maxHeight, scaleY) : maxHeight);
        }

        private static void ScaleWindowContent(Window target, double scaleX, double scaleY, bool isDpiChange)
        {
            var firstLevelChild = target.FindVisualChild<FrameworkElement>();
            if (firstLevelChild == null)
            {
                return;
            }

            if (isDpiChange)
            {
                //We use first level child for DPI Scale behavior according recommendations for DpiAware applications:
                //https://learn.microsoft.com/en-us/windows/win32/hidpi/declaring-managed-apps-dpi-aware

                firstLevelChild.LayoutTransform = CreateScaleTransform(scaleX, scaleY);
            }
            else
            {
                var secondLevelChildrenCount = VisualTreeHelper.GetChildrenCount(firstLevelChild);
                for (var i = 0; i < secondLevelChildrenCount; i++)
                {
                    var secondLevelChild = VisualTreeHelper.GetChild(firstLevelChild, i) as FrameworkElement;
                    if (secondLevelChild == null)
                    {
                        continue;
                    }

                    //We use second level child for TextScale behavior according RTL limitation in ArrangeOverride in Window class:
                    //https://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Window.cs,111626a697762d41

                    secondLevelChild.LayoutTransform = CreateScaleTransform(scaleX, scaleY);
                }
            }
        }

        private static void ScalePropertyValue(Window target, DependencyProperty property, double scale)
        {
            var current = target.GetValue<double>(property);

            if (HasValue(current))
            {
                target.SetValue(property, ScaleValue(current, scale));
            }
        }

        private static double ReplacePropertyValue(Window target, DependencyProperty property, double value)
        {
            var originValue = target.GetValue<double>(property);

            target.SetValue(property, value);

            return originValue;
        }

        private static bool HasValue(double value)
        {
            return !double.IsInfinity(value) && !double.IsNaN(value);
        }

        private static double ScaleValue(double value, double scale)
        {
            return Math.Round(value * scale, RoundDigits);
        }

        private static ScaleTransform CreateScaleTransform(double scaleX, double scaleY)
        {
            scaleX = Math.Round(scaleX, RoundDigits);
            scaleY = Math.Round(scaleY, RoundDigits);

            return (ScaleTransform)new ScaleTransform(scaleX, scaleY).GetAsFrozen();
        }

        private const int RoundDigits = 5;
    }
}