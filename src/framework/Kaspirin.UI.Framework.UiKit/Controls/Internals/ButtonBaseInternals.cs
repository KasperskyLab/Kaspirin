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
using System.Windows.Controls.Primitives;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal static class ButtonBaseInternals
    {
        #region LeftIcon16

        public static UIKitIcon_16 GetLeftIcon16(DependencyObject obj)
        {
            return (UIKitIcon_16)obj.GetValue(LeftIcon16Property);
        }

        public static void SetLeftIcon16(DependencyObject obj, UIKitIcon_16 value)
        {
            obj.SetValue(LeftIcon16Property, value);
        }

        public static readonly DependencyProperty LeftIcon16Property =
            DependencyProperty.RegisterAttached("LeftIcon16", typeof(UIKitIcon_16), typeof(ButtonBaseInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ButtonBase), nameof(LeftIcon16Property)));

        #endregion

        #region LeftIcon24

        public static UIKitIcon_24 GetLeftIcon24(DependencyObject obj)
        {
            return (UIKitIcon_24)obj.GetValue(LeftIcon24Property);
        }

        public static void SetLeftIcon24(DependencyObject obj, UIKitIcon_24 value)
        {
            obj.SetValue(LeftIcon24Property, value);
        }

        public static readonly DependencyProperty LeftIcon24Property =
            DependencyProperty.RegisterAttached("LeftIcon24", typeof(UIKitIcon_24), typeof(ButtonBaseInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ButtonBase), nameof(LeftIcon24Property)));

        #endregion

        #region RightIcon16

        public static UIKitIcon_16 GetRightIcon16(DependencyObject obj)
        {
            return (UIKitIcon_16)obj.GetValue(RightIcon16Property);
        }

        public static void SetRightIcon16(DependencyObject obj, UIKitIcon_16 value)
        {
            obj.SetValue(RightIcon16Property, value);
        }

        public static readonly DependencyProperty RightIcon16Property =
            DependencyProperty.RegisterAttached("RightIcon16", typeof(UIKitIcon_16), typeof(ButtonBaseInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ButtonBase), nameof(RightIcon16Property)));

        #endregion

        #region RightIcon24

        public static UIKitIcon_24 GetRightIcon24(DependencyObject obj)
        {
            return (UIKitIcon_24)obj.GetValue(RightIcon24Property);
        }

        public static void SetRightIcon24(DependencyObject obj, UIKitIcon_24 value)
        {
            obj.SetValue(RightIcon24Property, value);
        }

        public static readonly DependencyProperty RightIcon24Property =
            DependencyProperty.RegisterAttached("RightIcon24", typeof(UIKitIcon_24), typeof(ButtonBaseInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ButtonBase), nameof(RightIcon24Property)));

        #endregion

        #region IsBusy

        public static bool GetIsBusy(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBusyProperty);
        }

        public static void SetIsBusy(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBusyProperty, value);
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.RegisterAttached("IsBusy", typeof(bool), typeof(ButtonBaseInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ButtonBase), nameof(IsBusyProperty)));

        #endregion
    }
}