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

using Kaspirin.UI.Framework.Mvvm;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public static class WindowCommand
    {
        public static readonly ICommand Close = new DelegateCommand<object?>(CloseWindow);
        public static readonly ICommand Minimize = new DelegateCommand<object?>(MinimizeWindow);
        public static readonly ICommand MaximizeOrRestore = new DelegateCommand<object?>(MaximizeWindow);

        private static void CloseWindow(object? obj)
        {
            if (obj == null)
            {
                return;
            }

            var window = Guard.EnsureArgumentIsInstanceOfType<DependencyObject>(obj).GetWindow();
            if (window == null || _closingWindows.Contains(window))
            {
                return;
            }

            _closingWindows.Add(window);

            try
            {
                window.Close();
            }
            finally
            {
                _closingWindows.Remove(window);
            }
        }

        private static void MinimizeWindow(object? obj)
        {
            if (obj == null)
            {
                return;
            }

            var window = Guard.EnsureArgumentIsInstanceOfType<DependencyObject>(obj).GetWindow();
            if (window == null)
            {
                return;
            }

            window.WindowState = WindowState.Minimized;
        }

        private static void MaximizeWindow(object? obj)
        {
            if (obj == null)
            {
                return;
            }

            var window = Guard.EnsureArgumentIsInstanceOfType<DependencyObject>(obj).GetWindow();
            if (window == null)
            {
                return;
            }

            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private static readonly HashSet<Window> _closingWindows = new();
    }
}
