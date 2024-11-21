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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class Bullet : ContentControl
    {
        #region Icon

        public UIKitIcon_12 Icon
        {
            get { return (UIKitIcon_12)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(UIKitIcon_12), typeof(Bullet));

        #endregion

        #region IconBrush

        public Brush? IconBrush
        {
            get { return (Brush?)GetValue(IconBrushProperty); }
            set { SetValue(IconBrushProperty, value); }
        }

        public static readonly DependencyProperty IconBrushProperty =
            DependencyProperty.Register("IconBrush", typeof(Brush), typeof(Bullet));

        #endregion
    }
}
