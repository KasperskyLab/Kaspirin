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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public sealed class WindowScreenMonitoringService
    {
        private WindowScreenMonitoringService(Window window)
        {
            _window = window;
            _windowId = WindowIdFactory.GetDefaultWindowId(_window);
        }

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WindowScreenMonitoringService),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }

        public static bool GetIsEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = Guard.EnsureArgumentIsInstanceOfType<Window>(d);

            var service = window.GetValue(InstanceProperty) as WindowScreenMonitoringService;
            if (service == null)
            {
                service = new WindowScreenMonitoringService(window);

                window.SetValue(InstanceProperty, service);
            }

            var isEnable = (bool)e.NewValue;
            if (isEnable)
            {
                window.WhenSourceInitialized(() => service.Initialize());
            }
            else
            {
                service.Deinitialize();
            }
        }

        #endregion

        #region Instance

        public static readonly DependencyProperty InstanceProperty =
            DependencyProperty.RegisterAttached("Instance", typeof(WindowScreenMonitoringService), typeof(WindowScreenMonitoringService),
                new PropertyMetadata(default(WindowScreenMonitoringService)));

        public static WindowScreenMonitoringService GetInstance(Window window)
        {
            Guard.ArgumentIsNotNull(window);

            return (WindowScreenMonitoringService)window.GetValue(InstanceProperty);
        }

        #endregion

        public static Rect SystemWorkArea => GetSystemWorkArea();

        public static Dpi SystemDpi => GetSystemDpi();

        public static Dpi DefaultDpi => Dpi.Default;

        public Rect? WorkArea { get; private set; }

        public Dpi? WindowDpi { get; private set; }

        public Dpi? OriginDpi { get; private set; }

        public Size? ScreenResolution { get; private set; }

        public Window? TargetWindow { get; private set; }

        public bool IsInitialized { get; private set; }

        public event DpiChangedDelegate WindowDpiChanged = (sender, args) => { };

        public event ScreenResolutionChangedDelegate ScreenResolutionChanged = (sender, args) => { };

        public event Action Initialized = () => { };

        private void Initialize()
        {
            if (IsInitialized || _window == null)
            {
                return;
            }

            IsInitialized = true;

            TargetWindow = _window;
            TargetWindow.Closed += (s, e) => Deinitialize();

            OriginDpi = WindowDpi = GetWindowDpi();
            ScreenResolution = GetScreenResolution();
            WorkArea = GetWindowWorkArea();

            var hook = new WindowHookBuilder(WindowHooks.DpiDisplayChangedHook)
                .AddHandler<IntPtr, MinMaxInfo>(WindowMessage.WM_GETMINMAXINFO, WndProcOnGetMinMaxInfo)
                .AddHandler<int, NativeRectangle>(WindowMessage.WM_DPICHANGED, WndProcOnDpiChanged)
                .AddAsyncHandler<uint, int>(WindowMessage.WM_DISPLAYCHANGE, WndProcOnDisplayChange)
                .Build();

            WindowHookStorage.AddHook(_window, hook, WindowHooks.DpiDisplayChangedHook);

            Initialized.Invoke();

            _tracer.TraceInformation($"Service initialized. {DumpState()}");
        }

        private void Deinitialize()
        {
            if (!IsInitialized)
            {
                return;
            }

            WindowHookStorage.RemoveHook(_windowId, WindowHooks.DpiDisplayChangedHook);

            _window = null;
            IsInitialized = false;
            Initialized = () => { };

            _tracer.TraceInformation($"Service deinitialized. {DumpState()}");
        }

        private IntPtr WndProcOnGetMinMaxInfo(IntPtr hwnd, IntPtr wParam, IntPtr? wStruct, IntPtr lParam, MinMaxInfo? lStruct, ref bool handled)
        {
            if (IsInitialized && lStruct != null)
            {
                var mmi = lStruct.Value;

                if (hwnd.TryGetWindowWorkArea(out var result))
                {
                    mmi.ptMaxPosition.X = (int)result.X;
                    mmi.ptMaxPosition.Y = (int)result.Y;
                    mmi.ptMaxSize.X = (int)result.Width;
                    mmi.ptMaxSize.Y = (int)result.Height;
                }

                Marshal.StructureToPtr(mmi, lParam, true);
            }

            return IntPtr.Zero;
        }

        private IntPtr WndProcOnDpiChanged(IntPtr hwnd, IntPtr wParam, int? wStruct, IntPtr lParam, NativeRectangle? lStruct, ref bool handled)
        {
            if (IsInitialized && TryGetDpi(wStruct, out var newDpi) && lStruct != null)
            {
                var oldDpi = Guard.EnsureIsNotNull(WindowDpi);
                var pos = lStruct.Value;

                var winPosAdvice = new Rect
                {
                    Width = pos.Width,
                    Height = pos.Height,
                    Location = new Point()
                    {
                        X = pos.Left,
                        Y = pos.Top
                    }
                };

                _tracer.TraceInformation($"WM_DPICHANGED appeared with params:\nDPI:{newDpi}.\nPositionAdvice: [Location={winPosAdvice.Location}; Width={winPosAdvice.Width}; Height={winPosAdvice.Height}]");

                if (newDpi.Equals(oldDpi) == false)
                {
                    var args = new DpiChangedEventArguments(oldDpi, newDpi, winPosAdvice);

                    WindowDpi = newDpi;
                    WindowDpiChanged.Invoke(this, args);
                }
            }

            handled = true;

            return IntPtr.Zero;
        }

        private void WndProcOnDisplayChange(uint? wParam, int? lParam)
        {
            if (IsInitialized && TryGetResolution(lParam, out var resolution) && wParam != null)
            {
                var newResolution = resolution.Value;
                var oldResolution = Guard.EnsureIsNotNull(ScreenResolution);
                var bitsPerPixel = wParam;

                _tracer.TraceInformation($"WM_DISPLAYCHANGE appeared with params: Resolution:{newResolution}; Image depth: {bitsPerPixel}.");

                if (newResolution.Equals(oldResolution) == false)
                {
                    WorkArea = GetWindowWorkArea();

                    var args = new ScreenResolutionChangedEventArguments(oldResolution, newResolution);

                    ScreenResolution = newResolution;
                    ScreenResolutionChanged.Invoke(this, args);
                }
            }
        }

        private static bool TryGetResolution(int? param, [NotNullWhen(true)] out Size? resolution)
        {
            if (ParamSplitter.TryGetHiLoWords(param, out var loword, out var hiword) == true)
            {
                resolution = new Size(loword, hiword);
                return true;
            }

            resolution = null;
            return false;
        }

        private static bool TryGetDpi(int? param, [NotNullWhen(true)] out Dpi? dpi)
        {
            if (ParamSplitter.TryGetHiLoWords(param, out var loword, out var hiword))
            {
                dpi = new Dpi(loword, hiword);
                return true;
            }

            dpi = null;
            return false;
        }

        private Dpi GetWindowDpi()
        {
            Guard.IsNotNull(_window);

            var hWnd = _window.GetHandle(ensure: true);
            if (hWnd.TryGetWindowDpi(out var result))
            {
                return result;
            }

            _tracer.TraceWarning($"Failed to get Dpi for {_windowId}. " +
                                 $"SystemDpi value used: {SystemDpi}.");

            return SystemDpi;
        }

        private Rect GetWindowWorkArea()
        {
            Guard.IsNotNull(_window);

            var hWnd = _window.GetHandle(ensure: true);
            if (hWnd.TryGetWindowWorkArea(out var result, normalizeToZero: false))
            {
                return result;
            }

            _tracer.TraceWarning($"Failed to get work area for {_windowId}. " +
                                 $"SystemWorkArea value used: {SystemWorkArea}.");

            return SystemWorkArea;
        }

        private Size GetScreenResolution()
        {
            Guard.IsNotNull(_window);

            var hWnd = _window.GetHandle(ensure: true);

            return hWnd.GetScreenResolution();
        }

        private static Dpi GetSystemDpi()
        {
            using var dc = SafeDcHandle.GetScreenDc();

            return new(dc.GetDeviceCapsX(), dc.GetDeviceCapsY());
        }

        private static Rect GetSystemWorkArea()
        {
            return new(new(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height));
        }

        private string DumpState()
        {
            return new StringBuilder()
                .AppendLine($"Target window: {_windowId}; ")
                .AppendLine($"OriginDPI={OriginDpi}; WindowDPI={WindowDpi}; SystemDPI={SystemDpi}")
                .AppendLine($"WorkArea={WorkArea}; SystemWorkArea={SystemWorkArea}; ScreenResolution={ScreenResolution};")
                .ToString();
        }

        private readonly string _windowId;

        private Window? _window;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(nameof(WindowScreenMonitoringService));
    }
}
