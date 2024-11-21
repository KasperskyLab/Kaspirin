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
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class ContextMenuSelect : ContextMenuButton
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (BindingOperations.GetBindingBase(this, ContentProperty) is null &&
                GetValue(ContentProperty) is null)
            {
                SetBinding(ContentProperty, new Binding()
                {
                    Source = this,
                    Path = SelectedItemProperty.AsPath()
                });
            }
        }

        #region SelectedItem

        public object? SelectedItem
        {
            get { return (object?)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ContextMenuSelect),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ContextMenuSelect)d).UpdateContent();
        }

        #endregion

        private void UpdateContent()
        {
            BindingOperations.GetBindingExpressionBase(this, ContentProperty)?.UpdateTarget();
        }
    }
}
