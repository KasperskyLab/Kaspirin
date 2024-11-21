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
    internal static class NavigationMenuButtonInternals
    {
        #region MarginLevel1

        public static Thickness GetMarginLevel1(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginLevel1Property);
        }

        public static void SetMarginLevel1(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginLevel1Property, value);
        }

        public static readonly DependencyProperty MarginLevel1Property =
            DependencyProperty.RegisterAttached("MarginLevel1", typeof(Thickness), typeof(NavigationMenuButtonInternals));

        #endregion

        #region MarginLevel2

        public static Thickness GetMarginLevel2(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginLevel2Property);
        }

        public static void SetMarginLevel2(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginLevel2Property, value);
        }

        public static readonly DependencyProperty MarginLevel2Property =
            DependencyProperty.RegisterAttached("MarginLevel2", typeof(Thickness), typeof(NavigationMenuButtonInternals));

        #endregion
    }
}
