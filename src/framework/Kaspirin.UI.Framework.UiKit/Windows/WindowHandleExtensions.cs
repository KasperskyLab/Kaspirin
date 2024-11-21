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
using System.Windows.Interop;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public static class WindowHandleExtensions
    {
        public static WindowStyleExFlags SetWindowStyleEx(this IntPtr hWnd, WindowStyleExFlags windowStyleEx)
            => (WindowStyleExFlags)User32Dll.SetWindowLong(hWnd, WindowPropertyIndex.GWL_EXSTYLE, (int)windowStyleEx);

        public static WindowStyleExFlags GetWindowStyleEx(this IntPtr hWnd)
            => (WindowStyleExFlags)User32Dll.GetWindowLong(hWnd, WindowPropertyIndex.GWL_EXSTYLE);

        public static WindowStyleFlags SetWindowStyle(this IntPtr hWnd, WindowStyleFlags windowStyle)
            => (WindowStyleFlags)User32Dll.SetWindowLong(hWnd, WindowPropertyIndex.GWL_STYLE, (int)windowStyle);

        public static WindowStyleFlags GetWindowStyle(this IntPtr hWnd)
            => (WindowStyleFlags)User32Dll.GetWindowLong(hWnd, WindowPropertyIndex.GWL_STYLE);

        public static Window GetWindow(this IntPtr hWnd)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            return (Window)HwndSource.FromHwnd(hWnd).RootVisual;
        }

        public static Size GetScreenResolution(this IntPtr hWnd)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            var screen = Screen.FromHandle(hWnd);

            return new Size()
            {
                Width = screen.Bounds.Width,
                Height = screen.Bounds.Height
            };
        }

        public static void DragWindow(this IntPtr hWnd)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            User32Dll.SendMessage(hWnd, (uint)WindowMessage.WM_NCLBUTTONDOWN, (IntPtr)2, IntPtr.Zero);
        }

        public static void ResizeWindow(this IntPtr hwnd, WindowResizeDirection direction)
        {
            Guard.Assert(hwnd != IntPtr.Zero);

            const int scSizeWParam = 0xF000;

            User32Dll.SendMessage(hwnd, (uint)WindowMessage.WM_SYSCOMMAND, (IntPtr)(scSizeWParam + direction), IntPtr.Zero);
        }

        public static bool TryGetWindowDpi(this IntPtr hWnd, [NotNullWhen(true)] out Dpi? result)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            if (!OperatingSystemInfo.IsWin81OrHigher)
            {
                result = null;
                return false;
            }

            var handleMonitor = User32Dll.MonitorFromWindow(hWnd, MonitorOptions.MONITOR_DEFAULTTONEAREST);
            if (handleMonitor == IntPtr.Zero)
            {
                result = null;
                return false;
            }

            uint dpiX = 1;
            uint dpiY = 1;

            ShcoreDll.GetDpiForMonitor(handleMonitor, MonitorDpiType.MDT_DEFAULT, ref dpiX, ref dpiY);

            result = new Dpi(dpiX, dpiY);
            return true;
        }

        public static bool TryGetWindowWorkArea(this IntPtr hWnd, out Rect result, bool normalizeToZero = true)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            result = default;

            var monitor = User32Dll.MonitorFromWindow(hWnd, MonitorOptions.MONITOR_DEFAULTTONEAREST);
            if (monitor == IntPtr.Zero)
            {
                return false;
            }

            var monitorInfo = new MonitorInfo();

            if (!User32Dll.GetMonitorInfo(monitor, ref monitorInfo))
            {
                return false;
            }

            var nWorkArea = monitorInfo.Work;
            var nMonitorArea = monitorInfo.Monitor;

            var workArea = new Rect(nWorkArea.Left, nWorkArea.Top, nWorkArea.Width, nWorkArea.Height);

            if (normalizeToZero)
            {
                workArea.Offset(-nMonitorArea.Left, -nMonitorArea.Top);
            }

            result = workArea;
            return true;
        }

        public static bool IsForeground(this IntPtr hWnd)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            var foregroundWindowHandle = User32Dll.GetForegroundWindow();

            return hWnd == foregroundWindowHandle;
        }

        public static bool SetTopMost(this IntPtr hWnd)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            return User32Dll.SetWindowPos(hWnd, (IntPtr)(-1), 0, 0, 0, 0, WindowPositionFlags.SWP_NOMOVE | WindowPositionFlags.SWP_NOSIZE);
        }

        public static bool IsTopMost(this IntPtr hWnd)
        {
            Guard.Assert(hWnd != IntPtr.Zero);

            var style = (WindowStyleExFlags)User32Dll.GetWindowLong(hWnd, WindowPropertyIndex.GWL_EXSTYLE);

            return EnumOperations.HasFlag(style, WindowStyleExFlags.WS_EX_TOPMOST);
        }

        public static IntPtr GetProcessId(this IntPtr hWnd)
        {
            User32Dll.GetWindowThreadProcessId(hWnd, out var processId);

            return processId;
        }
    }
}
