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
using System.Windows.Controls;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors
{
    /// <summary>
    /// Enables blur or acrylic blur effect for content under the window.<para/>
    /// • Supports only Win10+ (in other versions of Windows just does nothing)<para/>
    /// • <see cref="Window.AllowsTransparency"/> should be enabled<para/>
    /// • <see cref="Control.Background"/> or <seealso cref="BlurBackgroundProperty"/>
    /// or <seealso cref="AcrylicBlurBackgroundProperty"/> should be transparent to make blur visible
    /// </summary>
    public sealed class WindowBlurBehavior
    {
        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
           "IsEnabled",
           typeof(bool),
           typeof(WindowBlurBehavior),
           new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject element, bool value)
            => element.SetValue(IsEnabledProperty, value);

        public static bool GetIsEnabled(DependencyObject element)
            => (bool)element.GetValue(IsEnabledProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
            {
                return;
            }

            if ((bool)e.OldValue)
            {
                DisableWindowBlurBehavior(window);
            }

            if ((bool)e.NewValue)
            {
                EnableWindowBlurBehavior(window);
            }
        }

        #endregion

        #region BlurBackground

        /// <summary>Background that is used when blur effect is active. If not set, original background of window is used.</summary>
        public static readonly DependencyProperty BlurBackgroundProperty = DependencyProperty.RegisterAttached(
            "BlurBackground",
            typeof(Brush),
            typeof(WindowBlurBehavior),
            new PropertyMetadata(null, OnBlurBackgroundChanged));

        public static void SetBlurBackground(DependencyObject element, Brush? value)
            => element.SetValue(BlurBackgroundProperty, value);

        public static Brush? GetBlurBackground(DependencyObject element)
            => (Brush?)element.GetValue(BlurBackgroundProperty);

        private static void OnBlurBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
            {
                return;
            }

            var blurService = GetWindowBlurService(window);
            if (blurService == null)
            {
                return;
            }

            blurService.BlurBackground = (Brush?)e.NewValue;
        }

        #endregion

        #region AcrylicBlurBackground

        /// <summary>Background that is used when acrylic blur effect is active. If not set, original background of window is used.</summary>
        public static readonly DependencyProperty AcrylicBlurBackgroundProperty = DependencyProperty.RegisterAttached(
            "AcrylicBlurBackground",
            typeof(Brush),
            typeof(WindowBlurBehavior),
            new PropertyMetadata(null, OnAcrylicBlurBackgroundChanged));

        public static void SetAcrylicBlurBackground(DependencyObject element, Brush? value)
            => element.SetValue(AcrylicBlurBackgroundProperty, value);

        public static Brush? GetAcrylicBlurBackground(DependencyObject element)
            => (Brush?)element.GetValue(AcrylicBlurBackgroundProperty);

        private static void OnAcrylicBlurBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
            {
                return;
            }

            var blurService = GetWindowBlurService(window);
            if (blurService == null)
            {
                return;
            }

            blurService.AcrylicBlurBackground = (Brush?)e.NewValue;
        }

        #endregion

        #region WindowBlurService

        public static readonly DependencyProperty WindowBlurServiceProperty = DependencyProperty.RegisterAttached(
            "WindowBlurService",
            typeof(WindowBlurService),
            typeof(WindowBlurBehavior),
            new PropertyMetadata(null, OnWindowBlurServiceChanged));

        public static void SetWindowBlurService(DependencyObject element, WindowBlurService? value)
            => element.SetValue(WindowBlurServiceProperty, value);

        public static WindowBlurService? GetWindowBlurService(DependencyObject element)
            => (WindowBlurService?)element.GetValue(WindowBlurServiceProperty);

        private static void OnWindowBlurServiceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
            {
                return;
            }

            (e.OldValue as WindowBlurService)?.Detach();

            if (e.NewValue is WindowBlurService windowBlurService)
            {
                var blurBackground = GetBlurBackground(d);
                windowBlurService.BlurBackground = blurBackground;

                var acrylicBlurBackground = GetAcrylicBlurBackground(d);
                windowBlurService.AcrylicBlurBackground = acrylicBlurBackground;

                windowBlurService.Attach(window);
            }
        }

        #endregion

        private static void EnableWindowBlurBehavior(Window window)
        {
            window.StateChanged += OnWindowStateChanged;

            if (window.WindowState != WindowState.Minimized)
            {
                SetBlurService(window);
            }
        }

        private static void DisableWindowBlurBehavior(Window window)
        {
            window.StateChanged -= OnWindowStateChanged;

            ResetBlurService(window);
        }

        private static void OnWindowStateChanged(object? sender, System.EventArgs e)
        {
            var window = Guard.EnsureIsInstanceOfType<Window>(sender);
            if (window.WindowState != WindowState.Minimized)
            {
                SetBlurService(window);
            }
            else
            {
                ResetBlurService(window);
            }
        }

        private static void SetBlurService(Window window)
            => window.SetValue(WindowBlurServiceProperty, new WindowBlurService());

        private static void ResetBlurService(Window window)
            => window.ClearValue(WindowBlurServiceProperty);
    }
}