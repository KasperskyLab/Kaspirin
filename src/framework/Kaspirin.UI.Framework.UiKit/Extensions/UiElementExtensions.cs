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

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class UiElementExtensions
    {
        public static void WhenVisible(this UIElement uIElement, Action action)
        {
            void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                if (e.NewValue is true)
                {
                    ((UIElement)sender).IsVisibleChanged -= OnVisibleChanged;
                    action();
                }
            }

            if (uIElement.IsVisible)
            {
                action();
            }
            else
            {
                uIElement.IsVisibleChanged += OnVisibleChanged;
            }
        }

        public static void WhenInvisible(this UIElement uIElement, Action action)
        {
            void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                if (e.NewValue is false)
                {
                    ((UIElement)sender).IsVisibleChanged -= OnVisibleChanged;
                    action();
                }
            }

            if (uIElement.IsVisible is false)
            {
                action();
            }
            else
            {
                uIElement.IsVisibleChanged += OnVisibleChanged;
            }
        }
    }
}
