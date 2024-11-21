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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public static class ImplicitStyle
    {
        #region Inherit

        public static bool GetInherit(DependencyObject obj)
            => (bool)obj.GetValue(InheritProperty);

        public static void SetInherit(DependencyObject obj, bool value)
            => obj.SetValue(InheritProperty, value);

        public static readonly DependencyProperty InheritProperty = DependencyProperty.RegisterAttached(
            "Inherit",
            typeof(bool),
            typeof(ImplicitStyle),
            new PropertyMetadata(default(bool), OnInheritChanged));

        private static void OnInheritChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is true)
            {
                d.WhenLoaded(() => SetStyles(d));
            }
        }

        #endregion

        private static void SetStyles(DependencyObject source)
            => source.FindVisualParent<UIKitContentPresenter>()?.SetImplicitStyles(source);
    }
}
