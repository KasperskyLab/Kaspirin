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

#pragma warning disable CA1401 // P/Invokes should not be visible

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32
{
    /// <summary>
    ///     Provides API methods for functions from user32.dll .
    /// </summary>
    public static class User32Dll
    {
        #region winuser.h

        /// <summary>
        ///     The SetWindowsHookEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(
            HookType hookType,
            WindowsHookDelegate lpfn,
            IntPtr hMod,
            int dwThreadId);

        /// <summary>
        ///     The UnhookWindowsHookEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(
            IntPtr hhk);

        /// <summary>
        ///     The CallNextHookEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            IntPtr wParam,
            IntPtr lParam);

        /// <summary>
        ///     The SendMessage API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessage">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,
            uint msg,
            IntPtr wParam,
            IntPtr lParam);

        /// <summary>
        ///     The postMessage API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-postmessagew">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(
            IntPtr hWnd,
            uint msg,
            IntPtr wParam,
            IntPtr lParam);

        /// <summary>
        ///     The GetPhysicalCursorPos API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getphysicalcursorpos">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetPhysicalCursorPos(
            out NativePoint lpPoint);

        /// <summary>
        ///     The ScreenToClient API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-screentoclient">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.None, SetLastError = true)]
        public static extern bool ScreenToClient(
            IntPtr hWnd,
            ref NativePoint point);

        /// <summary>
        ///     The EnumWindows API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumwindows">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern bool EnumWindows(
            EnumWindowsProc lpEnumFunc,
            IntPtr lParam);

        /// <summary>
        ///     The MonitorFromWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-monitorfromwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern IntPtr MonitorFromWindow(
            IntPtr hWnd,
            MonitorOptions dwFlags);

        /// <summary>
        ///     The MonitorFromRect API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-monitorfromrect">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern IntPtr MonitorFromRect(
            [In] ref NativeRectangle rect,
            MonitorOptions dwFlags);

        /// <summary>
        ///     The MonitorFromPoint API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-monitorfrompoint">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern IntPtr MonitorFromPoint(
            NativePoint pt,
            MonitorOptions dwFlags);

        /// <summary>
        ///     The SetForegroundWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setforegroundwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(
            IntPtr hWnd);

        /// <summary>
        ///     The GetForegroundWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        ///     The GetWindowText API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(
            IntPtr hWnd,
            [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder text,
            int maxCount);

        /// <summary>
        ///     API method GetWindowTextLength. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengthw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern int GetWindowTextLength(
            IntPtr hWnd);

        /// <summary>
        ///     API method GetWindowThreadProcessId. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int GetWindowThreadProcessId(
            IntPtr hWnd,
            out IntPtr processId);

        /// <summary>
        ///     The GetWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetWindow(
            IntPtr hWnd,
            GetWindowCommands uCmd);

        /// <summary>
        ///     The ShowWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(
            IntPtr hWnd,
            ShowWindowCommands nCmdShow);

        /// <summary>
        ///     The BringWindowToTop API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-bringwindowtotop">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool BringWindowToTop(
            IntPtr hWnd);

        /// <summary>
        ///     The GetTopWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-gettopwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetTopWindow(
            IntPtr hWnd);

        /// <summary>
        ///     The WindowFromPoint API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-windowfrompoint">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern IntPtr WindowFromPoint(
            NativePoint point);

        /// <summary>
        ///     The GetWindowLong API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowLong(
            IntPtr hWnd,
            WindowPropertyIndex index);

        /// <summary>
        ///     The SetWindowLong API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowLong(
            IntPtr hWnd,
            WindowPropertyIndex index,
            int dwNewLong);

        /// <summary>
        ///     The SetFocus API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setfocus">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr SetFocus(
            IntPtr hWnd);

        /// <summary>
        ///     The SetActiveWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setactivewindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr SetActiveWindow(
            IntPtr hWnd);

        /// <summary>
        ///     The GetWindowRect API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(
            IntPtr hWnd,
            out NativeRectangle rectangle);

        /// <summary>
        ///     The GetClientRect API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclientrect">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(
            IntPtr hWnd,
            out NativeRectangle rectangle);

        /// <summary>
        ///     The IsWindowVisible API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowvisible">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(
            IntPtr hWnd);

        /// <summary>
        ///     The GetShellWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getshellwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern IntPtr GetShellWindow();

        /// <summary>
        ///     The GetDesktopWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdesktopwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        ///     The SetParent API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setparent">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr SetParent(
            IntPtr hWndChild,
            IntPtr hWndNewParent);

        /// <summary>
        ///     The getParent API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getparent">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetParent(
            IntPtr hWnd);

        /// <summary>
        ///     The SetWindowPos API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            WindowPositionFlags flags);

        /// <summary>
        ///     The AllowSetForegroundWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-allowsetforegroundwindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern bool AllowSetForegroundWindow(
            int dwProcessId);

        /// <summary>
        ///     The GetActiveWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getactivewindow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern IntPtr GetActiveWindow();

        /// <summary>
        ///     The GetGuiResources API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getguiresources">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int GetGuiResources(
            IntPtr hProcess,
            GetGuiResourceType uiFlags);

        /// <summary>
        ///     The IsClipboardFormatAvailable API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-isclipboardformatavailable">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsClipboardFormatAvailable(
            uint format);

        /// <summary>
        ///     The OpenClipboard API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-openclipboard">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenClipboard(
            IntPtr hWndNewOwner);

        /// <summary>
        ///     The GetClipboardData API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclipboarddata">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetClipboardData(
            uint format);

        /// <summary>
        ///     API method GetClipboardSequenceNumber. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclipboardsequencenumber">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern int GetClipboardSequenceNumber();

        /// <summary>
        ///     The CloseClipboard API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-closeclipboard">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseClipboard();

        /// <summary>
        ///     The GetCursorPos API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(
            out NativePoint point);

        /// <summary>
        ///     The LoadCursor API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadcursorw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr LoadCursor(
            IntPtr hInstance,
            NativeCursors lpCursorName);

        /// <summary>
        ///     The SetCursorPos API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setcursorpos">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(
            int x,
            int y);

        /// <summary>
        ///     The GetDoubleClickTime API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdoubleclicktime">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern uint GetDoubleClickTime();

        /// <summary>
        ///     The SendInput API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern uint SendInput(
            uint cInputs,
            [MarshalAs(UnmanagedType.LPArray), In] InputInfo[] pInputs,
            int cbSize);

        /// <summary>
        ///     The GetLastInputInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getlastinputinfo">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern bool GetLastInputInfo(
            ref LastInputInfo lastInputInfo);

        /// <summary>
        ///     The MouseEvent API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, EntryPoint = "mouse_event")]
        public static extern void MouseEvent(
            MouseInputFlags flags,
            uint dx,
            uint dy,
            uint dwData,
            UIntPtr dwExtraInfo);

        /// <summary>
        ///     The VkKeyScanEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-vkkeyscanexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern short VkKeyScanEx(
            char ch,
            IntPtr dwhkl);

        /// <summary>
        ///     The MapVirtualKeyEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern uint MapVirtualKeyEx(
            uint uCode,
            MapVirtualKeyMapTypes uMapType,
            IntPtr dwhkl);

        /// <summary>
        ///     The GetKeyboardState API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardstate">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool GetKeyboardState(
            byte[] lpKeyState);

        /// <summary>
        ///     The GetKeyboardLayout API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardlayout">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetKeyboardLayout(
            uint idThread);

        /// <summary>
        ///     The GetKeyState API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeystate">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern KeyStateFlags GetKeyState(
            VirtualKey keyCode);

        /// <summary>
        ///     The RegisterHotKey API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id,
            KeyModifierFlags fsModifiers,
            VirtualKey vk);

        /// <summary>
        ///     The UnregisterHotKey API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterhotkey">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id);

        /// <summary>
        ///     The DestroyIcon API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroyicon">Learn more</seealso>.
        /// </summary>
        [SecurityCritical]
        [DllImport(DllName, SetLastError = true)]
        public static extern bool DestroyIcon(
            IntPtr hIcon);

        /// <summary>
        ///     The GetMessageExtraInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmessageextrainfo">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = false)]
        public static extern UIntPtr GetMessageExtraInfo();

        /// <summary>
        ///     The ToUnicodeEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-tounicodeex">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern int ToUnicodeEx(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags,
            IntPtr dwhkl);

        /// <summary>
        ///     The GetMonitorInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern bool GetMonitorInfo(
            IntPtr hMonitor,
            [In, Out] ref MonitorInfo lpmi);

        /// <summary>
        ///     The EnumDisplayMonitors API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaymonitors">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern bool EnumDisplayMonitors(
            IntPtr hdc,
            IntPtr lprcClip,
            MonitorEnumProc lpfnEnum,
            IntPtr dwData);

        /// <summary>
        ///     The GetSystemMetrics API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        public static extern int GetSystemMetrics(
            SystemMetric nIndex);

        /// <summary>
        ///     API method GetClassName. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclassname">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(
            IntPtr hWnd,
            [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder className,
            int maxCount);

        /// <summary>
        ///     The ReleaseDC API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-releasedc">Learn more</seealso>.
        /// </summary>
        [SecurityCritical]
        [DllImport(DllName)]
        public static extern int ReleaseDC(
            IntPtr hWnd,
            IntPtr hDc);

        /// <summary>
        ///     The GetDC API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdc">Learn more</seealso>.
        /// </summary>
        [SecurityCritical]
        [DllImport(DllName)]
        public static extern SafeDcHandle GetDC(
            IntPtr hWnd);

        /// <summary>
        ///     The EnumThreadWindows API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumthreadwindows">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        public static extern bool EnumThreadWindows(
            int dwThreadId,
            EnumThreadWndProc callback,
            IntPtr lParam);

        /// <summary>
        ///     The GetWindowInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowinfo">Learn more</seealso>.
        /// </summary>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(DllName, SetLastError = true)]
        public static extern bool GetWindowInfo(
            IntPtr hwnd,
            ref WindowInfo pwi);

        /// <summary>
        ///     The FindWindow API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindoww">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(
            string? sClassName,
            string? sAppName);

        /// <summary>
        ///     The FindWindowEx API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowexw">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
            string lpszWindow);

        /// <summary>
        ///     The SystemParametersInfo API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfow">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(
            SpiType uiAction,
            uint uiParam,
            out bool pvParam,
            SpiFlags fWinIni);

        /// <summary>
        ///     The AttachThreadInput API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-attachthreadinput">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = true, ExactSpelling = true, EntryPoint = "AttachThreadInput")]
        public static extern bool AttachThreadInput(
            int idAttach,
            int idAttachTo,
            bool attach);

        /// <summary>
        ///     The SetWindowCompositionAttribute API method. <br /><seealso href="https://learn.microsoft.com/en-us/windows/win32/dwm/setwindowcompositionattribute">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowCompositionAttribute(
            IntPtr hWnd,
            ref WindowCompositionAttributeData data);

        #endregion

        private const string DllName = "user32.dll";
    }
}
