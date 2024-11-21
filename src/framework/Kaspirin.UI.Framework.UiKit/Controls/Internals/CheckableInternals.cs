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
    internal static class CheckableInternals
    {
        #region Description

        public static object GetDescription(DependencyObject obj)
        {
            return obj.GetValue(DescriptionProperty);
        }

        public static void SetDescription(DependencyObject obj, object value)
        {
            obj.SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.RegisterAttached("Description", typeof(object), typeof(CheckableInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ToggleButton), nameof(DescriptionProperty)));

        #endregion

        #region IsInvalidState

        public static bool GetIsInvalidState(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsInvalidStateProperty);
        }

        public static void SetIsInvalidState(DependencyObject obj, bool value)
        {
            obj.SetValue(IsInvalidStateProperty, value);
        }

        public static readonly DependencyProperty IsInvalidStateProperty =
            DependencyProperty.RegisterAttached("IsInvalidState", typeof(bool), typeof(CheckableInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ToggleButton), nameof(IsInvalidStateProperty)));

        #endregion
    }
}