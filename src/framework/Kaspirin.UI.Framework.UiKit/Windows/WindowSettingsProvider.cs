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
    public sealed class WindowSettingsProvider
    {
        public WindowSettingsProvider(WindowScreenMonitoringService wsmService)
        {
            _wsmService = Guard.EnsureArgumentIsNotNull(wsmService);
        }

        public WindowSettings GetSettings()
        {
            var window = _wsmService.TargetWindow;
            var windowDpi = _wsmService.WindowDpi;

            Guard.IsNotNull(window);
            Guard.IsNotNull(windowDpi);

            var position = double.IsNaN(window.Left) || double.IsNaN(window.Top)
                ? (Point?)null
                : new Point()
                {
                    X = (int)(window.Left / _wsmService.GetWindowScaleX(DpiType.WindowOriginDpi)),
                    Y = (int)(window.Top / _wsmService.GetWindowScaleY(DpiType.WindowOriginDpi)),
                };

            return new WindowSettings
            {
                Id = WindowIdFactory.GetDefaultWindowId(window),
                IsMaximized = window.WindowState == WindowState.Maximized,
                WindowDpi = windowDpi,
                Height = (int)window.ActualHeight,
                Width = (int)window.ActualWidth,
                Position = position
            };
        }

        private readonly WindowScreenMonitoringService _wsmService;
    }
}
