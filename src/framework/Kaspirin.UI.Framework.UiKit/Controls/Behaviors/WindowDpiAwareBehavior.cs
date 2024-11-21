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
using System.Text;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors
{
    public sealed class WindowDpiAwareBehavior : Behavior<Window, WindowDpiAwareBehavior>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            Guard.IsNotNull(AssociatedObject);

            var service = WindowScreenMonitoringService.GetInstance(AssociatedObject);
            service.WindowDpiChanged += OnDpiChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Guard.IsNotNull(AssociatedObject);

            var service = WindowScreenMonitoringService.GetInstance(AssociatedObject);
            service.WindowDpiChanged -= OnDpiChanged;
        }

        private static void OnDpiChanged(WindowScreenMonitoringService service, DpiChangedEventArguments eventArgs)
        {
            if (!OperatingSystemInfo.IsWin81OrHigher)
            {
                _tracer.TraceInformation($"Dpi changing skipped on Win8 or lower");
                return;
            }

            var targetWindow = Guard.EnsureArgumentIsNotNull(service.TargetWindow);

            WindowMessageSuppressor.ExecuteWithMessageSuppression(
                service.TargetWindow,
                () =>
                {
                    var oldDpi = eventArgs.OldDpi;
                    var newDpi = eventArgs.NewDpi;
                    var positionAdvice = eventArgs.PositionAdvice;

                    _tracer.TraceInformation($"Dpi changing from {oldDpi} to {newDpi}. {DumpState(targetWindow)}");

                    var toPrevScaleX = newDpi.X / oldDpi.X;
                    var toPrevScaleY = newDpi.Y / oldDpi.Y;

                    var toOriginScaleX = service.GetWindowScaleX(DpiType.WindowOriginDpi);
                    var toOriginScaleY = service.GetWindowScaleY(DpiType.WindowOriginDpi);

                    WindowScaleHelper.ScaleWindowByDpiChange(targetWindow, toPrevScaleX, toPrevScaleY);
                    WindowScaleHelper.ScaleContentByDpiChange(targetWindow, toOriginScaleX, toOriginScaleY);

                    SetWindowSizeAndPos(targetWindow, positionAdvice);

                    _tracer.TraceInformation($"Dpi changed from {oldDpi} to {newDpi}. {DumpState(targetWindow)}");
                },
                WindowMessage.WM_DPICHANGED);
        }

        private static void SetWindowSizeAndPos(Window window, Rect positionRect)
        {
            var hwnd = window.GetHandle();
            var posFlags = WindowPositionFlags.SWP_NOZORDER |
                           WindowPositionFlags.SWP_NOOWNERZORDER |
                           WindowPositionFlags.SWP_NOACTIVATE;

            User32Dll.SetWindowPos(
                hwnd,
                IntPtr.Zero,
                (int)positionRect.Left,
                (int)positionRect.Top,
                (int)positionRect.Width,
                (int)positionRect.Height,
                posFlags);
        }

        private static string DumpState(FrameworkElement targetElement)
        {
            var currentSize = new Size(targetElement.Width, targetElement.Height);
            var actualSize = new Size(targetElement.ActualWidth, targetElement.ActualHeight);
            var actualMinSize = new Size(targetElement.MinWidth, targetElement.MinHeight);
            var actualMaxSize = new Size(targetElement.MaxWidth, targetElement.MaxHeight);

            return new StringBuilder()
                .AppendLine()
                .AppendLine($"Target element:\t {targetElement.GetType().FullName}; ")
                .AppendLine($"Visual state:\t ArrangeValid={targetElement.IsArrangeValid}; MeasureValid={targetElement.IsMeasureValid}")
                .AppendLine($"Actual sizes:\t CurrSize={currentSize} ActualSize={actualSize} MinSize={actualMinSize} MaxSize={actualMaxSize}")
                .ToString();
        }

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(nameof(WindowDpiAwareBehavior));
    }
}