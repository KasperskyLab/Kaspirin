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

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors
{
    public sealed class ButtonCheckBehavior : Behavior<Button, ButtonCheckBehavior>
    {
        #region IsChecked

        public static bool GetIsChecked(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCheckedProperty);
        }

        public static void SetIsChecked(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCheckedProperty, value);
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.RegisterAttached("IsChecked", typeof(bool), typeof(ButtonCheckBehavior),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(Button), nameof(IsCheckedProperty)));

        #endregion

        protected override void OnAttached()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.Click += ChangeIsChecked;
        }

        protected override void OnDetaching()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.Click -= ChangeIsChecked;
        }

        private void ChangeIsChecked(object sender, RoutedEventArgs e)
        {
            Guard.IsNotNull(AssociatedObject);

            var isChecked = (bool)AssociatedObject.GetValue(IsCheckedProperty);

            AssociatedObject.SetValue(IsCheckedProperty, !isChecked);
        }
    }
}
