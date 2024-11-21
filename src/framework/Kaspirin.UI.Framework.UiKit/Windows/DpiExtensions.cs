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

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public static class DpiExtensions
    {
        public static Point TranslateToDefaultDpi(this FrameworkElement window, Point location)
        {
            var source = PresentationSource.FromVisual(window);
            return new Point(TranslateToDefaultDpiX(location.X, source), TranslateToDefaultDpiY(location.Y, source));
        }

        public static double TranslateToDefaultDpiX(this FrameworkElement window, double value)
        {
            return TranslateToDefaultDpiX(value, PresentationSource.FromVisual(window));
        }

        public static double TranslateToDefaultDpiY(this FrameworkElement window, double value)
        {
            return TranslateToDefaultDpiY(value, PresentationSource.FromVisual(window));
        }

        public static double GetCurrentDpiX(this FrameworkElement window)
        {
            var source = PresentationSource.FromVisual(window);
            if (source?.CompositionTarget != null)
            {
                return 1 / source.CompositionTarget.TransformFromDevice.M11 * Dpi.Default.X;
            }

            return Dpi.Default.X;
        }

        public static double GetCurrentDpiY(this FrameworkElement window)
        {
            var source = PresentationSource.FromVisual(window);
            if (source?.CompositionTarget != null)
            {
                return 1 / source.CompositionTarget.TransformFromDevice.M22 * Dpi.Default.Y;
            }

            return Dpi.Default.Y;
        }

        private static double TranslateToDefaultDpiX(double value, PresentationSource source)
        {
            if (source?.CompositionTarget != null)
            {
                var dpiX = Dpi.Default.X * source.CompositionTarget.TransformFromDevice.M11;
                return value * Dpi.Default.X / dpiX;
            }

            return value;
        }

        private static double TranslateToDefaultDpiY(double value, PresentationSource source)
        {
            if (source?.CompositionTarget != null)
            {
                var dpiY = Dpi.Default.Y * source.CompositionTarget.TransformFromDevice.M22;
                return value * Dpi.Default.Y / dpiY;
            }

            return value;
        }
    }
}