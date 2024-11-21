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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Input.Focus
{
    public static class InputFocusManager
    {
        public static void ClearInputFocus(UIElement target)
        {
            Guard.ArgumentIsNotNull(target);

            var window = target.GetWindow();
            if (window == null)
            {
                return;
            }

            var hasFocus = target.IsFocused || target.FindVisualChildren<UIElement>().Contains(e => e.IsFocused);
            if (hasFocus)
            {
                FocusManager.SetFocusedElement(window, window);
            }
        }

        public static bool SetInputFocus(UIElement target)
        {
            Guard.ArgumentIsNotNull(target);

            if (!CanSetInputFocus(target, out var parentWindow))
            {
                return false;
            }

            FocusManager.SetFocusedElement(parentWindow, target);
            return true;
        }

        private static bool CanSetInputFocus(UIElement target, [NotNullWhen(true)] out Window? parentWindow)
        {
            Guard.ArgumentIsNotNull(target);

            parentWindow = null;

            if (!target.Focusable || !target.IsVisible)
            {
                return false;
            }

            var notificationLayer = NotificationLayer.FindLayer(target, isModal: true);
            if (notificationLayer?.IsModalState == true)
            {
                var isInsideNotification = target.FindVisualParent<NotificationView>() != null;
                if (isInsideNotification == false)
                {
                    return false;
                }
            }

            parentWindow = target.GetWindow();
            if (parentWindow?.IsVisible != true)
            {
                return false;
            }

            if (!parentWindow.IsActive)
            {
                // If current foreground window belongs to our app, it's totally legit
                // to set focus to any focusable and visible element inside any visible
                // window of our app (active or inactive one, no difference).

                // If current foreground window belongs to some another app, there is
                // not much sense to set focus to an element inside our inactive window.

                // So if it's really important to set focus, then it's necessary to ensure
                // that foreground window belongs to our app before calling SetInputFocus.
                return IsCurrentApplicationForeground();
            }

            return true;
        }

        private static bool IsCurrentApplicationForeground()
        {
            var foregroundWindowHandle = User32Dll.GetForegroundWindow();
            if (foregroundWindowHandle == IntPtr.Zero)
            {
                // No window is currently activated.
                return false;
            }

            var currentProcessId = Kernel32Dll.GetCurrentProcessId();
            var foregroundProcessId = foregroundWindowHandle.GetProcessId().ToInt32();

            return foregroundProcessId == currentProcessId;
        }
    }
}
