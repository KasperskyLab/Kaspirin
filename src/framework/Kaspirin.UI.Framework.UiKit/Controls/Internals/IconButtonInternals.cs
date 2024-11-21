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

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal class IconButtonInternals
    {
        #region Icon16

        public static UIKitIcon_16 GetIcon16(DependencyObject obj)
        {
            return (UIKitIcon_16)obj.GetValue(Icon16Property);
        }

        public static void SetIcon16(DependencyObject obj, UIKitIcon_16 value)
        {
            obj.SetValue(Icon16Property, value);
        }

        public static readonly DependencyProperty Icon16Property =
            DependencyProperty.RegisterAttached("Icon16", typeof(UIKitIcon_16), typeof(IconButtonInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(IconButtonBase), nameof(Icon16Property)));

        #endregion

        #region Icon24

        public static UIKitIcon_24 GetIcon24(DependencyObject obj)
        {
            return (UIKitIcon_24)obj.GetValue(Icon24Property);
        }

        public static void SetIcon24(DependencyObject obj, UIKitIcon_24 value)
        {
            obj.SetValue(Icon24Property, value);
        }

        public static readonly DependencyProperty Icon24Property =
            DependencyProperty.RegisterAttached("Icon24", typeof(UIKitIcon_24), typeof(IconButtonInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(IconButtonBase), nameof(Icon24Property)));

        #endregion
    }
}
