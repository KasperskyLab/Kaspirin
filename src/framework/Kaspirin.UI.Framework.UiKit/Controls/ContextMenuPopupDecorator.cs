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
using System.Windows.Controls.Primitives;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class ContextMenuPopupDecorator : PopupDecorator
    {
        static ContextMenuPopupDecorator()
        {
            MaxWidthProperty.OverrideMetadata(typeof(ContextMenuPopupDecorator), new FrameworkPropertyMetadata(UIKitConstants.ContextMenuPopupDecoratorMaxWidth));
            MinWidthProperty.OverrideMetadata(typeof(ContextMenuPopupDecorator), new FrameworkPropertyMetadata(UIKitConstants.ContextMenuPopupDecoratorMinWidth));
        }

        public ContextMenuPopupDecorator()
        {
            SetValue(Grid.IsSharedSizeScopeProperty, true);
        }

        protected override void SetPopupLocation()
        {
            var rootPopup = this.FindVisualParent<ContextMenu>();
            if (rootPopup != null)
            {
                var isTargetContextMenuButton = rootPopup.PlacementTarget?.FindVisualParent<ContextMenuButton>() != null;
                if (isTargetContextMenuButton)
                {
                    rootPopup.Placement = PlacementMode.Custom;
                    rootPopup.CustomPopupPlacementCallback = OnContextMenuButtonCustomPopupPlacementCalled;
                }
                else
                {
                    if (rootPopup.PlacementTarget is NavigationMenuFooterButton)
                    {
                        rootPopup.Placement = PlacementMode.Custom;
                        rootPopup.CustomPopupPlacementCallback = OnNavigationMenuFooterButtonCustomPopupPlacementCalled;
                    }
                }
            }
            else
            {
                var submenuPopup = this.FindLogicalParent<ContextMenuPopup>();
                if (submenuPopup != null)
                {
                    submenuPopup.HorizontalOffset = -ShadowOffset + Offset;
                    submenuPopup.VerticalOffset = -ShadowOffset - Padding.Top;
                }
            }
        }

        private CustomPopupPlacement[] OnContextMenuButtonCustomPopupPlacementCalled(Size popupSize, Size targetSize, Point offset)
        {
            return new PopupPositionProvider(popupSize, targetSize, FlowDirection, Offset, ShadowOffset).LocateBottomRight();
        }

        private CustomPopupPlacement[] OnNavigationMenuFooterButtonCustomPopupPlacementCalled(Size popupSize, Size targetSize, Point offset)
        {
            return new PopupPositionProvider(popupSize, targetSize, FlowDirection, Offset, ShadowOffset).LocateTopLeft();
        }
    }
}
