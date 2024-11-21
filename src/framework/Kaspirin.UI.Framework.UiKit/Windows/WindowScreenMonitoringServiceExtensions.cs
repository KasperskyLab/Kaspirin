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

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public static class WindowScreenMonitoringServiceExtensions
    {
        public static void WhenInitialized(this WindowScreenMonitoringService service, Action action)
        {
            Guard.ArgumentIsNotNull(service);
            Guard.ArgumentIsNotNull(action);

            if (service.IsInitialized)
            {
                action();
            }
            else
            {
                service.Initialized += OnInitialized;
            }

            void OnInitialized()
            {
                service.Initialized -= OnInitialized;
                action();
            }
        }

        public static double GetWindowScaleX(this WindowScreenMonitoringService service, DpiType to)
        {
            return GetScaleX(service, DpiType.WindowCurrentDpi, to);
        }

        public static double GetWindowScaleY(this WindowScreenMonitoringService service, DpiType to)
        {
            return GetScaleY(service, DpiType.WindowCurrentDpi, to);
        }

        public static double GetScaleX(this WindowScreenMonitoringService service, DpiType from, DpiType to)
        {
            Guard.ArgumentIsNotNull(service);

            var relativeToDpi = SelectDpi(service, to);
            var relativeFromDpi = SelectDpi(service, from);

            Guard.IsNotNull(relativeToDpi);
            Guard.IsNotNull(relativeFromDpi);

            return relativeFromDpi.ScaleX / relativeToDpi.ScaleX;
        }

        public static double GetScaleY(this WindowScreenMonitoringService service, DpiType from, DpiType to)
        {
            Guard.ArgumentIsNotNull(service);

            var relativeToDpi = SelectDpi(service, to);
            var relativeFromDpi = SelectDpi(service, from);

            Guard.IsNotNull(relativeToDpi);
            Guard.IsNotNull(relativeFromDpi);

            return relativeFromDpi.ScaleY / relativeToDpi.ScaleY;
        }

        public static WindowSettings GetWindowSettings(this WindowScreenMonitoringService service)
            => new WindowSettingsProvider(Guard.EnsureArgumentIsNotNull(service)).GetSettings();

        public static Rect GetScaledWorkArea(this WindowScreenMonitoringService service)
        {
            Guard.ArgumentIsNotNull(service);

            var scaleX = service.GetScaleX(DpiType.DefaultDpi, DpiType.WindowOriginDpi);
            var scaleY = service.GetScaleY(DpiType.DefaultDpi, DpiType.WindowOriginDpi);

            var workArea = Guard.EnsureIsNotNull(service.WorkArea);

            return new Rect()
            {
                X = workArea.X * scaleX,
                Y = workArea.Y * scaleY,
                Width = workArea.Width * scaleX,
                Height = workArea.Height * scaleY
            };
        }

        private static Dpi? SelectDpi(WindowScreenMonitoringService service, DpiType dpiType)
        {
            return dpiType switch
            {
                DpiType.DefaultDpi => WindowScreenMonitoringService.DefaultDpi,
                DpiType.SystemDpi => WindowScreenMonitoringService.SystemDpi,
                DpiType.WindowOriginDpi => service.OriginDpi,
                DpiType.WindowCurrentDpi => service.WindowDpi,
                _ => throw new ArgumentOutOfRangeException(nameof(dpiType)),
            };
        }
    }
}
