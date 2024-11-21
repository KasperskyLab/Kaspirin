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
using System.Windows.Media;

using PopupWpf = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class PopupDpiAwareBehavior : Behavior<PopupWpf, PopupDpiAwareBehavior>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            Guard.IsNotNull(AssociatedObject);

            SetLayoutTransformBinding(AssociatedObject);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Guard.IsNotNull(AssociatedObject);

            ResetLayoutTransformBinding(AssociatedObject);
        }

        private static void SetLayoutTransformBinding(PopupWpf popup)
        {
            FrameworkElement? transformSource = null;

            var window = popup.GetWindow();
            if (window == null)
            {
                var popupTarget = popup.PlacementTarget;
                if (popupTarget != null)
                {
                    transformSource = popupTarget.GetWindow();
                }
            }
            else
            {
                transformSource = window.FindVisualChild<FrameworkElement>();
            }

            if (transformSource != null)
            {
                popup.SetBinding(FrameworkElement.LayoutTransformProperty, new Binding()
                {
                    Source = transformSource,
                    Path = FrameworkElement.LayoutTransformProperty.AsPath(),
                    Converter = new DelegateConverter<GeneralTransform>(t => t?.Inverse)
                });
            }
        }

        private static void ResetLayoutTransformBinding(PopupWpf popup)
        {
            popup.ClearValue(FrameworkElement.LayoutTransformProperty);
        }
    }
}
