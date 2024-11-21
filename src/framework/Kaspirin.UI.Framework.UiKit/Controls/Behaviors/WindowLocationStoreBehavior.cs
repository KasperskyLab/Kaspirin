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
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors
{
    public sealed class WindowLocationStoreBehavior : Behavior<Window, WindowLocationStoreBehavior>
    {
        #region Storage

        public static readonly DependencyProperty StorageProperty =
            DependencyProperty.RegisterAttached("Storage", typeof(IKeyValueStorage), typeof(WindowLocationStoreBehavior),
                new UIPropertyMetadata(null, OnStorageChanged));

        public static IKeyValueStorage GetStorage(DependencyObject dependencyObject)
        {
            return (IKeyValueStorage)dependencyObject.GetValue(StorageProperty);
        }

        public static void SetStorage(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(StorageProperty, value);
        }

        private static void OnStorageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AttachToWindow((Window)d);
        }

        #endregion

        #region LocationProvider

        public static readonly DependencyProperty LocationProviderProperty =
           DependencyProperty.RegisterAttached("LocationProvider", typeof(WindowLocationProvider), typeof(WindowLocationStoreBehavior),
               new PropertyMetadata(new WindowLocationProvider()));

        public static WindowLocationProvider GetLocationProvider(DependencyObject dependencyObject)
        {
            return (WindowLocationProvider)dependencyObject.GetValue(LocationProviderProperty);
        }

        public static void SetLocationProvider(DependencyObject dependencyObject, WindowLocationProvider value)
        {
            dependencyObject.SetValue(LocationProviderProperty, value);
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            Guard.IsNotNull(AssociatedObject);

            AttachToWindow(AssociatedObject);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Guard.IsNotNull(AssociatedObject);

            DetachFromWindow(AssociatedObject);
        }

        protected override void OnAssociatedObjectUnloaded()
        {
            base.OnAssociatedObjectUnloaded();
            Guard.IsNotNull(AssociatedObject);

            DetachFromWindow(AssociatedObject);
        }

        private static void AttachToWindow(Window window)
        {
            if (!CanAttach(window))
            {
                return;
            }

            window.WhenSourceInitialized(() =>
            {
                if (!CanAttach(window))
                {
                    return;
                }

                var service = WindowScreenMonitoringService.GetInstance(window);

                service.WhenInitialized(() =>
                {
                    if (TryGetWindowSettings(window, out var settings))
                    {
                        ApplyWindowSettings(window, settings);
                    }
                    else
                    {
                        ApplyWindowPosition(window, null);
                    }

                    service.WindowDpiChanged += OnDpiChanged;

                    var windowId = WindowIdFactory.GetDefaultWindowId(window);
                    var windowHook = WindowPositionOrSizeChangedWindowHook.CreateHook(windowId, WindowHooks.PositionOrSizeChangedHook, window);

                    WindowHookStorage.AddHook(windowId, windowHook);

                    TraceInformation(window, "Behavior attached");
                });
            });
        }

        private static void DetachFromWindow(Window window)
        {
            var service = WindowScreenMonitoringService.GetInstance(window);
            service.WindowDpiChanged -= OnDpiChanged;

            WindowHookStorage.RemoveHook(window, WindowHooks.PositionOrSizeChangedHook);

            TraceInformation(window, "Behavior detached");
        }

        private static bool CanAttach(Window window)
        {
            var isEnabled = GetIsEnabled(window);
            if (!isEnabled)
            {
                TraceInformation(window, "Behavior disabled for this window");
                return false;
            }

            var hasStorage = GetStorage(window) != null;
            if (!hasStorage)
            {
                TraceInformation(window, "Behavior disabled without storage");
                return false;
            }

            return true;
        }

        private static bool TryGetWindowSettings(Window window, [NotNullWhen(true)] out WindowSettings? settings)
        {
            settings = null;

            var storage = GetWindowParamsStorage(window);
            if (!storage.SettingsExists())
            {
                TraceInformation(window, $"WindowSettings does not exist");
                return false;
            }

            var windowSettings = storage.GetSettings();
            if (windowSettings.Height <= 0 || windowSettings.Width <= 0)
            {
                TraceInformation(window, "WindowSettings is incorrect");
                return false;
            }

            var windowScreenMonitoring = WindowScreenMonitoringService.GetInstance(window);
            if (windowScreenMonitoring == null || windowSettings.WindowDpi == null || !windowSettings.WindowDpi.Equals(windowScreenMonitoring.WindowDpi))
            {
                TraceInformation(window, "WindowSettings contains different DPI");
                return false;
            }

            settings = windowSettings;
            return true;
        }

        private static void ApplyWindowSettings(Window window, WindowSettings settings)
        {
            ApplyWindowSize(window, settings.Width, settings.Height);

            if (settings.IsMaximized)
            {
                ApplyWindowMaximized(window);
            }
            else
            {
                ApplyWindowPosition(window, settings.Position);
            }

            TraceInformation(window, $"WindowSettings applied");
        }

        private static void ApplyWindowSize(Window window, double width, double height)
        {
            if (window.ResizeMode == ResizeMode.CanResize ||
                window.ResizeMode == ResizeMode.CanResizeWithGrip)
            {
                switch (window.SizeToContent)
                {
                    case SizeToContent.Manual:
                        window.Height = height;
                        window.Width = width;
                        break;
                    case SizeToContent.Width:
                        window.Height = height;
                        break;
                    case SizeToContent.Height:
                        window.Width = width;
                        break;
                    case SizeToContent.WidthAndHeight:
                        break;
                    default:
                        break;
                }

                TraceInformation(window, $"Window size \"{window.Width}x{window.Height}\" applied");
            }
            else
            {
                TraceInformation(window, $"Window size applying skipped because of ResizeMode \"{window.ResizeMode}\"");
            }
        }

        private static void ApplyWindowMaximized(Window window)
        {
            window.WindowState = WindowState.Maximized;

            TraceInformation(window, $"Window state \"{window.WindowState}\" applied");
        }

        private static void ApplyWindowPosition(Window window, Point? position)
        {
            if (position == null || IsWindowOutOfBound(window, position.Value))
            {
                position = GetLocationProvider(window).GetInitialWindowLocation(window);
            }

            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.Left = position.Value.X;
            window.Top = position.Value.Y;

            TraceInformation(window, $"Window position \"{window.Left}x{window.Top}\" applied");
        }

        private static void OnDpiChanged(WindowScreenMonitoringService sender, DpiChangedEventArguments eventArgs)
        {
            var window = sender.TargetWindow;
            Guard.IsNotNull(window);

            SaveParams(window, sender.GetWindowSettings());
        }

        private static WindowSettingsPersistentStorage GetWindowParamsStorage(Window window)
        {
            return new WindowSettingsPersistentStorage(GetStorage(window), window);
        }

        private static void SaveParams(Window window, WindowSettings windowSettings)
        {
            SetWindowIndependentSizeParams(window, windowSettings);
            GetWindowParamsStorage(window).SaveSettings(windowSettings);
        }

        private static void SetWindowIndependentSizeParams(Window window, WindowSettings windowSettings)
        {
            var service = WindowScreenMonitoringService.GetInstance(window);

            windowSettings.Width = (int)(windowSettings.Width / service.GetWindowScaleX(DpiType.WindowOriginDpi));
            windowSettings.Height = (int)(windowSettings.Height / service.GetWindowScaleY(DpiType.WindowOriginDpi));
        }

        private static bool IsWindowOutOfBound(Window window, Point leftTop)
        {
            var screen = Screen.FromHandle(window.GetHandle());
            var windowRect = new System.Drawing.Rectangle((int)leftTop.X, (int)leftTop.Y, (int)window.Width, (int)window.Height);
            var outOfBounds = !screen.WorkingArea.Contains(windowRect);

            if (outOfBounds)
            {
                TraceInformation(window, "window is out of bound");
            }

            return outOfBounds;
        }

        private static void TraceInformation(Window window, string message)
        {
            _trace.TraceInformation($"window {window.GetType().Name} - message: {message}");
        }

        private static void TraceInformation(string message)
        {
            _trace.TraceInformation(message);
        }

        /// <summary>
        /// Window position or size changed hook to help store new settings in registry
        /// </summary>
        private sealed class WindowPositionOrSizeChangedWindowHook : WindowHookBase
        {
            public static WindowPositionOrSizeChangedWindowHook CreateHook(string windowId, string hookId, Window window)
            {
                return new WindowPositionOrSizeChangedWindowHook(windowId, hookId, window);
            }

            private WindowPositionOrSizeChangedWindowHook(string windowId, string hookId, Window window)
                : base(window, hookId, $"{hookId} for {windowId}")
            {
                // Object is stored in static Dictionary, it is important to hold WeakReference to window
                _window = new WeakReference(window);
                _savedSettings = new()
                {
                    Id = windowId
                };

                _hook = new WindowHookBuilder(hookId)
                    .AddAsyncHandler<int, int>(WindowMessage.WM_ENTERSIZEMOVE, WndProcOnEnterSizeMove)
                    .AddAsyncHandler<int, int>(WindowMessage.WM_EXITSIZEMOVE, WndProcOnExitSizeMove)
                    .AddAsyncHandler<int, int>(WindowMessage.WM_MOVE, WndProcOnMove)
                    .AddAsyncHandler<int, int>(WindowMessage.WM_SIZE, WndProcOnSize)
                    .Build();
            }

            public override void Attach()
            {
                Attach(_hook);
            }

            public override void Detach()
            {
                Detach(_hook);
            }

            // When window size or position is changed manually message WM_ENTERSIZEMOVE comes first
            private void WndProcOnEnterSizeMove(int? wParam, int? lParam)
            {
                if (_window.Target is not Window window)
                {
                    TraceInformation("WM_ENTERSIZEMOVE received but window is null");
                    return;
                }

                TraceInformation(window, "WM_ENTERSIZEMOVE received");
                _sizeOrMoveMode = true;
            }

            // When window size or position manually change is finished WM_EXITSIZEMOVE fired only once
            private void WndProcOnExitSizeMove(int? wParam, int? lParam)
            {
                if (_window.Target is not Window window)
                {
                    TraceInformation("WM_EXITSIZEMOVE received but window is null");
                    return;
                }

                TraceInformation(window, "WM_EXITSIZEMOVE received");

                TraceInformation(window, $"Saving window settings");
                SaveWindowParamsWithSpecialSettings(window, targetSize: null, targetPoint: null, isMaximized: null);
                _sizeOrMoveMode = false;
            }

            // WM_MOVE message fired constantly during position change when user is moving window,
            // but it also fired when position is change programmatically
            private void WndProcOnMove(int? wParam, int? lParam)
            {
                if (_sizeOrMoveMode)
                {
                    // Do not handle message in case size is changed manually
                    return;
                }

                if (_window.Target is not Window window)
                {
                    TraceInformation("WM_MOVE received but window is null");
                    return;
                }

                if (!ParamSplitter.TryGetHiLoWords(lParam, out var loword, out var hiword))
                {
                    TraceInformation(window, $"WM_MOVE received but lParam is empty");
                    return;
                }

                TraceInformation(window, $"WM_MOVE received; wParam 0x{wParam:X4} - lParam 0x{lParam:X4}");

                var windowService = WindowScreenMonitoringService.GetInstance(window);

                var targetPoint = new Point
                {
                    X = (int)(loword / windowService.GetScaleX(DpiType.WindowOriginDpi, DpiType.DefaultDpi)),
                    Y = (int)(hiword / windowService.GetScaleX(DpiType.WindowOriginDpi, DpiType.DefaultDpi)),
                };

                var isTargetPointOutsize = targetPoint.X < 0 || targetPoint.Y < 0;
                if (isTargetPointOutsize)
                {
                    TraceInformation(window, $"WM_MOVE skipped because target point is outside of primary screen border");
                    return;
                }

                var isTargetPointEquals = targetPoint.Equals(_savedSettings.Position);
                if (isTargetPointEquals)
                {
                    TraceInformation(window, $"WM_MOVE skipped because target point is equals to already saved window position");
                    return;
                }

                var isTargetMaximized = window.WindowState == WindowState.Maximized;
                if (isTargetMaximized)
                {
                    TraceInformation(window, $"WM_MOVE skipped because target window is in maximized state");
                    return;
                }

                TraceInformation(window, $"Saving window settings with current window position - ({targetPoint.X}x{targetPoint.Y})");
                SaveWindowParamsWithSpecialSettings(window, targetSize: null, targetPoint: targetPoint, isMaximized: null);
            }

            // WM_SIZE message fired constantly during size change when user is resizing window,
            // but it also fired when size is change programmatically
            private void WndProcOnSize(int? wParam, int? lParam)
            {
                if (_sizeOrMoveMode)
                {
                    // Do not handle message in case size is changed manually
                    return;
                }

                if (_window.Target is not Window window)
                {
                    TraceInformation("WM_SIZE received but window is null");
                    return;
                }

                if (!ParamSplitter.TryGetHiLoWords(lParam, out var loword, out var hiword))
                {
                    TraceInformation(window, "WM_SIZE received but lParam is empty");
                    return;
                }

                // Skipping to process SIZE_MINIMIZED case because we never restore window with minimized state
                if (wParam == SIZE_MINIMIZED)
                {
                    TraceInformation(window, "WM_SIZE received but wParam is SIZE_MINIMIZED");
                    return;
                }

                TraceInformation(window, $"WM_SIZE received; wParam 0x{wParam?.ToString("X4")} - lParam 0x{lParam?.ToString("X4")}");

                var windowService = WindowScreenMonitoringService.GetInstance(window);

                var isMaximized = wParam == SIZE_MAXIMIZED;

                var targetSize = new Size
                {
                    Width = isMaximized ? window.ActualWidth : (int)(loword / windowService.GetScaleX(DpiType.WindowOriginDpi, DpiType.DefaultDpi)),
                    Height = isMaximized ? window.ActualHeight : (int)(hiword / windowService.GetScaleY(DpiType.WindowOriginDpi, DpiType.DefaultDpi)),
                };

                var isTargetSizeEquals = targetSize.Width.NearlyEqual(_savedSettings.Width) &&
                                         targetSize.Height.NearlyEqual(_savedSettings.Height);
                var isTargetStateEquals = _savedSettings.IsMaximized == isMaximized;

                if (isTargetStateEquals && isTargetSizeEquals)
                {
                    TraceInformation(window, $"WM_SIZE skipped because target size is equals to already saved window size and Maximize state not changed");
                    return;
                }

                TraceInformation(window, $"Saving window settings with current window size - ({targetSize.Width}x{targetSize.Height})");
                SaveWindowParamsWithSpecialSettings(window, targetSize: targetSize, targetPoint: null, isMaximized: isMaximized);
            }

            /// <summary>
            /// Helps to adjust <see cref="WindowSettings"/> for target size and position
            /// </summary>
            private void SaveWindowParamsWithSpecialSettings(
                Window window,
                Size? targetSize,
                Point? targetPoint,
                bool? isMaximized)
            {
                var windowSettings = WindowScreenMonitoringService.GetInstance(window).GetWindowSettings();

                if (targetSize.HasValue && !targetSize.Value.IsEmpty)
                {
                    windowSettings.Width = (int)targetSize.Value.Width;
                    windowSettings.Height = (int)targetSize.Value.Height;
                }

                if (targetPoint.HasValue)
                {
                    windowSettings.Position = targetPoint;
                }

                if (isMaximized.HasValue)
                {
                    windowSettings.IsMaximized = isMaximized.Value;
                }

                SaveParams(window, windowSettings);

                _savedSettings = windowSettings;
            }

            private const int SIZE_MINIMIZED = 1;
            private const int SIZE_RESTORED = 0;
            private const int SIZE_MAXIMIZED = 2;
            private const int SIZE_MAXSHOW = 3;
            private const int SIZE_MAXHIDE = 4;

            private WindowSettings _savedSettings;
            private bool _sizeOrMoveMode;
            private readonly HwndSourceHook _hook;
            private readonly WeakReference _window;
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(nameof(WindowLocationStoreBehavior));
    }
}