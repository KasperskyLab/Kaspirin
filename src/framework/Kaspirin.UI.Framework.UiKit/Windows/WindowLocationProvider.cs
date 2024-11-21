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
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public class WindowLocationProvider : MarkupExtension
    {
        public Point GetInitialWindowLocation(Window window)
        {
            Guard.ArgumentIsNotNull(window);

            var service = WindowScreenMonitoringService.GetInstance(window);

            var windowHeight = window.Height * service.GetWindowScaleY(DpiType.DefaultDpi);
            var windowWidth = window.Width * service.GetWindowScaleX(DpiType.DefaultDpi);

            var screenHeight = SystemParameters.FullPrimaryScreenHeight * service.GetScaleY(DpiType.SystemDpi, DpiType.DefaultDpi);
            var screenWidth = SystemParameters.FullPrimaryScreenWidth * service.GetScaleX(DpiType.SystemDpi, DpiType.DefaultDpi);

            var adaptiveHeight = windowHeight > screenHeight
                ? screenHeight
                : windowHeight;
            var adaptiveWidth = windowWidth > screenWidth
                ? screenWidth
                : windowWidth;

            var workAreaWidth = SystemParameters.WorkArea.Width * service.GetScaleY(DpiType.SystemDpi, DpiType.DefaultDpi);
            var workAreaHeight = SystemParameters.WorkArea.Height * service.GetScaleY(DpiType.SystemDpi, DpiType.DefaultDpi);

            var screenPoint = GetInitialWindowLocation(workAreaWidth, workAreaHeight, adaptiveHeight, adaptiveWidth);

            return new Point
            {
                X = screenPoint.X / service.GetWindowScaleX(DpiType.DefaultDpi),
                Y = screenPoint.Y / service.GetWindowScaleY(DpiType.DefaultDpi),
            };
        }

        protected virtual Point GetInitialWindowLocation(double workAreaWidth, double workAreaHeight, double adaptiveHeight, double adaptiveWidth)
        {
            return new Point(
                // Center screen
                x: (workAreaWidth - adaptiveWidth) / 2,
                y: (workAreaHeight - adaptiveHeight) / 2);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
