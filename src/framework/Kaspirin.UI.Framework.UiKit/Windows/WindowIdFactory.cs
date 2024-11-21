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
    public static class WindowIdFactory
    {
        public static string GetDefaultWindowId<TWindow>() where TWindow : Window
        {
            return GetDefaultWindowId(typeof(TWindow));
        }

        public static string GetDefaultWindowId(Window window)
        {
            return GetDefaultWindowId(window.GetType());
        }

        private static string GetDefaultWindowId(Type windowType)
        {
            // prefix is needed for compatibility with eka::storage::Copy in avp::RegistryExporter
            return $"windowId_{windowType.Name}_{HashProvider.CalculateSha256FromString(windowType.FullName!)}";
        }
    }
}
