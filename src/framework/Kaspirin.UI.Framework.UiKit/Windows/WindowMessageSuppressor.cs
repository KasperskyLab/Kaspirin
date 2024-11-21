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

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public static class WindowMessageSuppressor
    {
        public static void ExecuteWithMessageSuppression(
            Window window,
            Action action,
            params WindowMessage[] messages)
        {
            Guard.ArgumentIsNotNull(window);
            Guard.ArgumentIsNotNull(action);
            Guard.ArgumentCollectionIsNotNullOrEmpty(messages);

            static IntPtr wndProc(IntPtr hwnd, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                handled = true;

                return IntPtr.Zero;
            }

            var hookId = $"{string.Join("_", messages)}_SuppressionHook_{Guid.NewGuid()}";

            var hookBuilder = new WindowHookBuilder(hookId);
            foreach (var message in messages)
            {
                hookBuilder.AddHandler(message, wndProc);
            }

            var hook = hookBuilder.Build();

            try
            {
                WindowHookStorage.AddHook(window, hook, hookId);
                action();
            }
            finally
            {
                WindowHookStorage.RemoveHook(window, hookId);
            }
        }
    }
}
