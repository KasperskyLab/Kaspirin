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

using System;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit
{
    internal sealed class UIKitStyleHolder : FrameworkElement
    {
        public Style StyleRef
        {
            get { return (Style)GetValue(StyleRefProperty); }
            set { SetValue(StyleRefProperty, value); }
        }

        public static readonly DependencyProperty StyleRefProperty =
            DependencyProperty.Register("StyleRef", typeof(Style), typeof(UIKitStyleHolder), new PropertyMetadata(null, OnStyleRefChanged));

        private static void OnStyleRefChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UIKitStyleHolder)d).StyleRefChanged.Invoke(e.NewValue as Style, e.OldValue as Style);
        }

        public event Action<Style?, Style?> StyleRefChanged = (_, _) => { };
    }
}
