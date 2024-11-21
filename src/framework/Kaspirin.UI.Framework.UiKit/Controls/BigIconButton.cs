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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class BigIconButton : IconButtonBase
    {
        #region Icon

        public UIKitIcon_24 Icon
        {
            get { return (UIKitIcon_24)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(UIKitIcon_24), typeof(BigIconButton), new PropertyMetadata(OnIconChanged));

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var icon = (UIKitIcon_24)e.NewValue;
            d.SetValue(IconButtonInternals.Icon24Property, icon);
        }

        #endregion
    }
}
