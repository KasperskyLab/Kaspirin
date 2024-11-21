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
using System.Windows.Controls.Primitives;

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class PopupExtensions
    {
        public static void WhenOpened(this Popup popup, Action action)
        {
            void OnIsOpenChanged(object? sender, EventArgs e)
            {
                var popup = Guard.EnsureArgumentIsInstanceOfType<Popup>(sender);

                popup.Opened -= OnIsOpenChanged;
                action();
            }

            if (popup.IsOpen)
            {
                action();
            }
            else
            {
                popup.Opened += OnIsOpenChanged;
            }
        }

        public static void WhenClosed(this Popup popup, Action action)
        {
            void OnIsClosedChanged(object? sender, EventArgs e)
            {
                var popup = Guard.EnsureArgumentIsInstanceOfType<Popup>(sender);

                popup.Closed -= OnIsClosedChanged;
                action();
            }

            if (!popup.IsOpen)
            {
                action();
            }
            else
            {
                popup.Closed += OnIsClosedChanged;
            }
        }
    }
}
