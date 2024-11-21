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
using System.Runtime.InteropServices;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public sealed class WindowModalDialogMonitoringService
    {
        private WindowModalDialogMonitoringService(Window window)
        {
            _window = window;
        }

        public event Action<IntPtr>? DialogShowing;

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled", typeof(bool), typeof(WindowModalDialogMonitoringService), new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject element, bool value)
            => element.SetValue(IsEnabledProperty, value);

        public static bool GetIsEnabled(DependencyObject element)
            => (bool)element.GetValue(IsEnabledProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = Guard.EnsureArgumentIsInstanceOfType<Window>(d);

            var service = window.GetValue(InstanceProperty) as WindowModalDialogMonitoringService;
            if (service == null)
            {
                service = new WindowModalDialogMonitoringService(window);

                window.SetValue(InstanceProperty, service);
            }

            var isEnable = (bool)e.NewValue;
            if (isEnable)
            {
                service.Initialize();
            }
            else
            {
                service.Deinitialize();
            }
        }

        #endregion

        #region Instance

        public static readonly DependencyProperty InstanceProperty = DependencyProperty.RegisterAttached(
            "Instance", typeof(WindowModalDialogMonitoringService), typeof(WindowModalDialogMonitoringService), new PropertyMetadata(default(WindowModalDialogMonitoringService)));

        public static WindowModalDialogMonitoringService GetInstance(Window window)
        {
            Guard.ArgumentIsNotNull(window);

            return (WindowModalDialogMonitoringService)window.GetValue(InstanceProperty);
        }

        #endregion

        private void Initialize()
        {
            if (_initialized || _window == null)
            {
                return;
            }

            AttachHook();
            _window.Closed += (s, e) => Deinitialize();

            _initialized = true;

            _tracer.TraceInformation($"Service initialized");
        }

        private void Deinitialize()
        {
            if (!_initialized)
            {
                return;
            }

            DetachHook();

            _window = null;
            _dialogHandle = IntPtr.Zero;

            _initialized = false;

            _tracer.TraceInformation($"Service deinitialized");
        }

        private IntPtr WndProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == 0 && lParam != IntPtr.Zero)
            {
                var cwp =
#if NET6_0_OR_GREATER
                    Marshal.PtrToStructure<CwpStruct>(lParam);
#else
                    (CwpStruct)Marshal.PtrToStructure(lParam, typeof(CwpStruct));
#endif

                if (cwp.Message == (uint)WindowMessage.WM_INITDIALOG)
                {
                    _tracer.TraceInformation($"Modal dialog with handle 0x{cwp.Hwnd:X} is initializing");
                    _dialogHandle = cwp.Hwnd;
                }
                else if (_dialogHandle != IntPtr.Zero &&
                    cwp.Hwnd == _dialogHandle &&
                    cwp.Message == (uint)WindowMessage.WM_SHOWWINDOW)
                {
                    var isShowing = cwp.WParam != IntPtr.Zero;
                    if (isShowing)
                    {
                        _tracer.TraceInformation($"Modal dialog with handle 0x{cwp.Hwnd:X} is showing");
                        _dialogHandle = IntPtr.Zero;
                        DialogShowing?.Invoke(cwp.Hwnd);
                    }
                }
            }

            return User32Dll.CallNextHookEx(_hookHandle, code, wParam, lParam);
        }

        private void AttachHook()
        {
            _tracer.TraceInformation(nameof(AttachHook));

            var threadId = Kernel32Dll.GetCurrentThreadId();
            var hook = new WindowsHookDelegate(WndProc);

            _hookDelegateHandle = GCHandle.Alloc(hook);
            _hookHandle = User32Dll.SetWindowsHookEx(HookType.WH_CALLWNDPROC, hook, IntPtr.Zero, threadId);

            Guard.Assert(_hookHandle != IntPtr.Zero);
        }

        private void DetachHook()
        {
            _tracer.TraceInformation(nameof(DetachHook));

            Guard.Assert(_hookHandle != IntPtr.Zero);
            Guard.Assert(User32Dll.UnhookWindowsHookEx(_hookHandle));

            _hookDelegateHandle.Free();
            _hookHandle = IntPtr.Zero;
        }

        private Window? _window;
        private bool _initialized;
        private GCHandle _hookDelegateHandle;
        private IntPtr _hookHandle;
        private IntPtr _dialogHandle;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(nameof(WindowModalDialogMonitoringService));
    }
}
