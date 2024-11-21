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
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public sealed class WindowBlurService
    {
        public void Attach(Window window)
        {
            _window = Guard.EnsureArgumentIsNotNull(window);
            _window.WhenSourceInitialized(() => AttachInternal());
        }

        public void Detach()
        {
            try
            {
                DetachInternal();
            }
            finally
            {
                _window = null;
            }
        }

        public Brush? BlurBackground
        {
            get => _blurBackground;
            set
            {
                if (_blurBackground == value)
                {
                    return;
                }

                _blurBackground = value;

                UpdateWindowBackground();
            }
        }

        public Brush? AcrylicBlurBackground
        {
            get => _acrylicBlurBackground;
            set
            {
                if (_acrylicBlurBackground == value)
                {
                    return;
                }

                _acrylicBlurBackground = value;

                UpdateWindowBackground();
            }
        }

        /// <remarks>
        /// Current implementation considers that only Win10 supports blur.<para/>
        /// Win7 and Win8 are too old and don't support setting AccentPolicy via SetWindowCompositionAttribute() API.<para/>
        /// Win11 has following issues if using SetWindowCompositionAttribute() API:<para/>
        ///     • If using <see cref="AccentState.AccentEnableBlurBehind"/> there are huge lags when moving and resizing the window.<para/>
        ///     • If using <see cref="AccentState.AccentEnableAcrylicBlurBehind"/> API call just do nothing (window is 100% opaque).
        /// </remarks>
        private bool IsBlurSupported { get; } = OperatingSystemInfo.IsWin10OrHigher && !OperatingSystemInfo.IsWin11OrHigher;

        private void AttachInternal()
        {
            if (_window == null)
            {
                return;
            }

            _originalBackground = _window.Background;

            EnableBlur();
            UpdateWindowBackground();
        }

        private void DetachInternal()
        {
            if (!_isEnabled)
            {
                return;
            }

            DisableBlur();
            RestoreWindowBackground();
        }

        private void EnableBlur()
        {
            if (IsBlurSupported)
            {
                var accentState = GetAccentState();
                _isEnabled = SetWindowAccentState(accentState);
            }
        }

        private void DisableBlur()
        {
            if (IsBlurSupported)
            {
                SetWindowAccentState(AccentState.ACCENT_DISABLED);
            }

            _isEnabled = false;
        }

        private bool SetWindowAccentState(AccentState accentState)
        {
            Guard.IsNotNull(_window);

            var hwnd = _window.GetHandle();

            var accentPolicy = new AccentPolicy
            {
                AccentState = accentState,
                GradientColor = ToAbgrHex(DefaultGradientColor)
            };

            var accentPolicyStructSize = Marshal.SizeOf(accentPolicy);
            var accentPtr = Marshal.AllocHGlobal(accentPolicyStructSize);
            Marshal.StructureToPtr(accentPolicy, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentPolicyStructSize,
                Data = accentPtr
            };

            try
            {
                if (!User32Dll.SetWindowCompositionAttribute(hwnd, ref data))
                {
                    Tracer.TraceError($"Unable to set {accentState} accent state for window {_window.GetType().Name}." +
                                      " SetWindowCompositionAttribute call failed.");
                    return false;
                }

                _currentAccentState = accentState;
                return true;
            }
            catch (DllNotFoundException ex)
            {
                Tracer.TraceError($"Unable to set {accentState} accent state for window {_window.GetType().Name}." + Environment.NewLine + ex);
                return false;
            }
            finally
            {
                Marshal.FreeHGlobal(accentPtr);
            }
        }

        private static AccentState GetAccentState()
        {
            if (OperatingSystemInfo.IsWin10Version19H1OrHigher)
            {
                // Acrylic blur was broken since Win10 19H1 because of introduced b-u-g.
                // In Win10 19H1+ using acrylic blur causes notable lags when moving and resizing the window.
                return AccentState.ACCENT_ENABLE_BLURBEHIND;
            }

            if (OperatingSystemInfo.IsWin10Rs4OrHigher)
            {
                // Acrylic blur is supported in Win10 RS4+.
                return AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
            }

            return AccentState.ACCENT_ENABLE_BLURBEHIND;
        }

        private void UpdateWindowBackground()
        {
            if (_currentAccentState == AccentState.ACCENT_ENABLE_BLURBEHIND && BlurBackground != null)
            {
                Guard.EnsureIsNotNull(_window).Background = BlurBackground;
            }
            else if (_currentAccentState == AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND && AcrylicBlurBackground != null)
            {
                Guard.EnsureIsNotNull(_window).Background = AcrylicBlurBackground;
            }
        }

        private void RestoreWindowBackground()
        {
            Guard.EnsureIsNotNull(_window).Background = _originalBackground;
        }

        private static uint ToAbgrHex(Color c)
        {
            return unchecked((uint)(((c.A << 0x18) | (c.B << 0x10) | (c.G << 8) | c.R) & 0xFFFFFFFF));
        }

        private bool _isEnabled;
        private AccentState _currentAccentState;
        private Window? _window;
        private Brush? _blurBackground;
        private Brush? _acrylicBlurBackground;
        private Brush? _originalBackground;

        /// <remarks>Use transparent color since actual color is defined by background brush of the window.</remarks>
        private static readonly Color DefaultGradientColor = Colors.Transparent;

        private static readonly ComponentTracer Tracer = ComponentTracer.Get(nameof(WindowBlurService));
    }
}