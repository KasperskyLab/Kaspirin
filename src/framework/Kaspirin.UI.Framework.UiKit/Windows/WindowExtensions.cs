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
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public static class WindowExtensions
    {
        public static IntPtr GetHandle(this Window window, bool ensure = false)
        {
            Guard.ArgumentIsNotNull(window);

            if (ensure)
            {
                return new WindowInteropHelper(window).EnsureHandle();
            }
            else
            {
                return new WindowInteropHelper(window).Handle;
            }
        }

        public static void WhenSourceInitialized(this Window window, Action? action)
        {
            Guard.ArgumentIsNotNull(window);

            void OnSourceInitialized(object? sender, EventArgs e)
            {
                Guard.EnsureIsInstanceOfType<Window>(sender).SourceInitialized -= OnSourceInitialized;
                action?.Invoke();
            }

            void OnClosing(object? sender, CancelEventArgs e)
            {
                Guard.EnsureIsInstanceOfType<Window>(sender).Closing -= OnClosing;
                action = null;
            }

            var isSourceInitialized = window.GetHandle() != IntPtr.Zero;
            if (isSourceInitialized)
            {
                action?.Invoke();
            }
            else
            {
                window.Closing += OnClosing;
                window.SourceInitialized += OnSourceInitialized;
            }
        }

        /// <param name="position">Position in screen coordinates.</param>
        public static void SetWindowPosition(this Window window, Point position)
        {
            Guard.ArgumentIsNotNull(window);

            var service = WindowScreenMonitoringService.GetInstance(window);

            service.WhenInitialized(() =>
            {
                window.Left = position.X * service.GetScaleX(DpiType.DefaultDpi, DpiType.WindowOriginDpi);
                window.Top = position.Y * service.GetScaleY(DpiType.DefaultDpi, DpiType.WindowOriginDpi);

                AdjustWindowPosition(window);
            });
        }

        public static void AdjustWindowPosition(this Window window)
        {
            Guard.ArgumentIsNotNull(window);

            var service = WindowScreenMonitoringService.GetInstance(window);

            service.WhenInitialized(() =>
            {
                var workArea = service.GetScaledWorkArea();

                if (window.Left < workArea.Left)
                {
                    window.Left = workArea.Left;
                }
                else if (window.Left + window.Width > workArea.Right)
                {
                    window.Left = workArea.Right - window.Width;
                }

                if (window.Top + window.Height > workArea.Bottom)
                {
                    window.Top = workArea.Bottom - window.Height;
                }
            });
        }
    }
}
