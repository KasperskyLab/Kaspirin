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
using PopupWpf = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public class ContextMenuPopup : PopupWpf
    {
        public ContextMenuPopup()
        {
            this.WhenLoaded(() =>
            {
                CustomPopupPlacementCallback = OnCustomPopupPlacementCalled;
                Placement = PlacementMode.Custom;
            });
        }

        private CustomPopupPlacement[] OnCustomPopupPlacementCalled(Size popupSize, Size targetSize, Point offset)
        {
            if (FlowDirection == FlowDirection.LeftToRight)
            {
                var rightPoint = new Point(x: targetSize.Width, y: 0);
                var rightLocation = new CustomPopupPlacement(rightPoint, PopupPrimaryAxis.Horizontal);

                return new[] { rightLocation };
            }
            else
            {
                var leftPoint = new Point(x: -popupSize.Width - targetSize.Width, y: 0);
                var leftLocation = new CustomPopupPlacement(leftPoint, PopupPrimaryAxis.Horizontal);

                return new[] { leftLocation };
            }
        }
    }
}
