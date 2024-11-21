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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public abstract class SelectorItem : ListBoxItem
    {
        #region SelectOnCapturedMouseEnter

        public bool SelectOnCapturedMouseEnter
        {
            get { return (bool)GetValue(SelectOnCapturedMouseEnterProperty); }
            set { SetValue(SelectOnCapturedMouseEnterProperty, value); }
        }

        public static readonly DependencyProperty SelectOnCapturedMouseEnterProperty =
            DependencyProperty.Register("SelectOnCapturedMouseEnter", typeof(bool), typeof(SelectorItem), new PropertyMetadata(false));

        #endregion

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (!SelectOnCapturedMouseEnter)
            {
                return;
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!SelectOnCapturedMouseEnter)
            {
                ItemsControl.ItemsControlFromItemContainer(this)?.ReleaseMouseCapture();
            }
        }
    }
}
