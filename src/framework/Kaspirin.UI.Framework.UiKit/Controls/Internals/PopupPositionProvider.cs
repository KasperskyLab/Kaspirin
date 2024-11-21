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
    internal sealed class PopupPositionProvider
    {
        public PopupPositionProvider(Size popupSize, Size targetSize, FlowDirection flowDirection, double popupOffset = 0, double shadowOffset = 0)
        {
            _popupSize = popupSize;
            _targetSize = targetSize;
            _flowDirection = flowDirection;
            _popupOffset = popupOffset;
            _shadowOffset = shadowOffset;
        }

        /// <summary>
        /// <code>
        /// _______________________
        /// |   PlacementTarget   |
        /// -----------------------
        ///                       ↓
        ///           _____________
        ///           |           |
        ///           |           |
        ///           |   Popup   |
        ///           |           |
        ///           |           |
        ///           -------------
        /// </code>
        /// </summary>
        public CustomPopupPlacement[] LocateBottomRight()
        {
            var verticalLocation = _targetSize.Height + _popupOffset - _shadowOffset;
            var horizontalLocation = _flowDirection == FlowDirection.LeftToRight
                ? _targetSize.Width - _popupSize.Width + _shadowOffset
                : -_targetSize.Width - _shadowOffset;

            var popupPoint = new Point(x: horizontalLocation, y: verticalLocation);
            var popupLocation = new CustomPopupPlacement(popupPoint, PopupPrimaryAxis.Horizontal);

            return new[] { popupLocation };
        }

        /// <summary>
        /// <code>
        /// _____________
        /// |           |
        /// |           |
        /// |   Popup   |
        /// |           |
        /// |           |
        /// -------------
        /// ↑
        /// _______________________
        /// |   PlacementTarget   |
        /// -----------------------
        /// </code>
        /// </summary>
        public CustomPopupPlacement[] LocateTopLeft()
        {
            var verticalLocation = -_popupSize.Height - _popupOffset + _shadowOffset;
            var horizontalLocation = _flowDirection == FlowDirection.LeftToRight
                ? -_shadowOffset
                : -_popupSize.Width + _shadowOffset;

            var popupPoint = new Point(x: horizontalLocation, y: verticalLocation);
            var popupLocation = new CustomPopupPlacement(popupPoint, PopupPrimaryAxis.Horizontal);

            return new[] { popupLocation };
        }

        /// <summary>
        /// <code>
        ///      _____________
        ///      |           |
        ///      |           |
        ///      |   Popup   |
        ///      |           |
        ///      |           |
        ///      -------------
        ///            ↑
        /// _______________________
        /// |   PlacementTarget   |
        /// -----------------------
        /// </code>
        /// </summary>
        public CustomPopupPlacement[] LocateTopCenter()
        {
            var topPoint = new Point(
                x: -(_popupSize.Width - _targetSize.Width) / 2d,
                y: -_popupSize.Height - _popupOffset);

            if (_shadowOffset.NotNearlyZero())
            {
                var shadowCorrection = _shadowOffset;

                topPoint = new Point(topPoint.X, topPoint.Y + shadowCorrection);
            }

            if (_flowDirection == FlowDirection.RightToLeft)
            {
                var rtlCorrection = _targetSize.Width;

                topPoint = new Point(topPoint.X - rtlCorrection, topPoint.Y);
            }

            var topLocation = new CustomPopupPlacement(topPoint, PopupPrimaryAxis.Vertical);

            return new[] { topLocation };
        }

        /// <summary>
        /// <code>
        /// _______________________
        /// |   PlacementTarget   |
        /// -----------------------
        ///            ↓
        ///      _____________
        ///      |           |
        ///      |           |
        ///      |   Popup   |
        ///      |           |
        ///      |           |
        ///      -------------
        /// </code>
        /// </summary>
        public CustomPopupPlacement[] LocateBottomCenter()
        {
            var bottomPoint = new Point(
                x: -(_popupSize.Width - _targetSize.Width) / 2d,
                y: _targetSize.Height + _popupOffset);

            if (_shadowOffset.NotNearlyZero())
            {
                var shadowCorrection = _shadowOffset;

                bottomPoint = new Point(bottomPoint.X, bottomPoint.Y - shadowCorrection);
            }

            if (_flowDirection == FlowDirection.RightToLeft)
            {
                var rtlCorrection = _targetSize.Width;

                bottomPoint = new Point(bottomPoint.X - rtlCorrection, bottomPoint.Y);
            }

            var bottomLocation = new CustomPopupPlacement(bottomPoint, PopupPrimaryAxis.Vertical);

            return new[] { bottomLocation };
        }

        /// <summary>
        /// <code>
        ///      _____________
        ///      |           |
        ///      |           |   _______________________
        ///      |   Popup   | ← |   PlacementTarget   |
        ///      |           |   -----------------------
        ///      |           |
        ///      -------------
        /// </code>
        /// </summary>
        public CustomPopupPlacement[] LocateLeftCenter()
        {
            var leftPoint = new Point(
                x: -_popupSize.Width - _popupOffset,
                y: -(_popupSize.Height - _targetSize.Height) / 2d);

            if (_shadowOffset.NotNearlyZero())
            {
                var shadowCorrection = _shadowOffset;

                leftPoint = new Point(leftPoint.X + shadowCorrection, leftPoint.Y);
            }

            if (_flowDirection == FlowDirection.RightToLeft)
            {
                var rtlCorrection = _targetSize.Width;

                leftPoint = new Point(leftPoint.X - rtlCorrection, leftPoint.Y);
            }

            var leftLocation = new CustomPopupPlacement(leftPoint, PopupPrimaryAxis.Horizontal);

            return new[] { leftLocation };
        }

        /// <summary>
        /// <code>
        ///                          _____________
        ///                          |           |
        ///_______________________   |           |
        ///|   PlacementTarget   | → |   Popup   |
        ///-----------------------   |           |
        ///                          |           |
        ///                          -------------
        /// </code>
        /// </summary>
        public CustomPopupPlacement[] LocateRightCenter()
        {
            var rightPoint = new Point(
                x: _targetSize.Width + _popupOffset,
                y: -(_popupSize.Height - _targetSize.Height) / 2d);

            if (_shadowOffset.NotNearlyZero())
            {
                var shadowCorrection = _shadowOffset;

                rightPoint = new Point(rightPoint.X - shadowCorrection, rightPoint.Y);
            }

            if (_flowDirection == FlowDirection.RightToLeft)
            {
                var rtlCorrection = _targetSize.Width;

                rightPoint = new Point(rightPoint.X - rtlCorrection, rightPoint.Y);
            }

            var rightLocation = new CustomPopupPlacement(rightPoint, PopupPrimaryAxis.Horizontal);

            return new[] { rightLocation };
        }

        private readonly Size _popupSize;
        private readonly Size _targetSize;
        private readonly FlowDirection _flowDirection;
        private readonly double _popupOffset;
        private readonly double _shadowOffset;
    }
}
