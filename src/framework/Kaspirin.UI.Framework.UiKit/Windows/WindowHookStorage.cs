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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public static class WindowHookStorage
    {
        public static IList<WindowHookBase> GetHooks(Window window)
        {
            Guard.ArgumentIsNotNull(window);

            var windowId = WindowIdFactory.GetDefaultWindowId(window);

            return GetHooks(windowId);
        }

        public static IList<WindowHookBase> GetHooks(string windowId)
        {
            Guard.ArgumentIsNotNullOrEmpty(windowId);

            if (_hookStorage.ContainsKey(windowId))
            {
                return _hookStorage[windowId].Values.ToList();
            }

            return new List<WindowHookBase>();
        }

        public static void AddHook(Window window, HwndSourceHook hook, string windowHookId)
        {
            Guard.ArgumentIsNotNull(window);
            Guard.ArgumentIsNotNull(hook);
            Guard.ArgumentIsNotNullOrEmpty(windowHookId);

            var windowId = WindowIdFactory.GetDefaultWindowId(window);
            var windowHook = new WindowHook(window, hook, windowHookId, window.GetType().Name);

            AddHook(windowId, windowHook);
        }

        public static void AddHook(Window window, WindowHookBase windowHook)
        {
            Guard.ArgumentIsNotNull(window);
            Guard.ArgumentIsNotNull(windowHook);

            var windowId = WindowIdFactory.GetDefaultWindowId(window);

            AddHook(windowId, windowHook);
        }

        public static void AddHook(string windowId, WindowHookBase windowHook)
        {
            Guard.ArgumentIsNotNullOrEmpty(windowId);
            Guard.ArgumentIsNotNull(windowHook);

            if (!_hookStorage.TryGetValue(windowId, out var windowHookStorage))
            {
                windowHookStorage = new();
                _hookStorage.Add(windowId, windowHookStorage);
            }

            if (!windowHookStorage.ContainsKey(windowHook.HookId))
            {
                windowHookStorage[windowHook.HookId] = windowHook;
                windowHook.Attach();
            }
        }

        public static void RemoveHook(Window window, string windowHookId)
        {
            Guard.ArgumentIsNotNull(window);
            Guard.ArgumentIsNotNullOrEmpty(windowHookId);

            var windowId = WindowIdFactory.GetDefaultWindowId(window);

            RemoveHook(windowId, windowHookId);
        }

        public static void RemoveHook(string windowId, string windowHookId)
        {
            Guard.ArgumentIsNotNullOrEmpty(windowId);
            Guard.ArgumentIsNotNullOrEmpty(windowHookId);

            if (_hookStorage.TryGetValue(windowId, out var windowHookStorage))
            {
                if (windowHookStorage.TryGetValue(windowHookId, out var windowHook))
                {
                    windowHookStorage.Remove(windowHookId);
                    windowHook.Detach();
                }

                if (windowHookStorage.None())
                {
                    _hookStorage.Remove(windowId);
                }
            }
        }

        private static readonly Dictionary<string, Dictionary<string, WindowHookBase>> _hookStorage = new();
    }
}