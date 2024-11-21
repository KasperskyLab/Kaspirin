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
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_Root, Type = typeof(Border))]
    public class NotificationPanel : ContentControl
    {
        public const string PART_Root = "PART_Root";

        public NotificationPanel()
        {
            _cornerRoundingHelper = new CornerRoundingHelper(this, InvalidateCornerRadius);
        }

        #region Header

        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(NotificationPanel));

        #endregion

        #region Icon

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NotificationPanel));

        #endregion

        #region SubHeader

        public object SubHeader
        {
            get { return GetValue(SubHeaderProperty); }
            set { SetValue(SubHeaderProperty, value); }
        }

        public static readonly DependencyProperty SubHeaderProperty =
            DependencyProperty.Register("SubHeader", typeof(object), typeof(NotificationPanel));

        #endregion

        #region Type

        public NotificationPanelType Type
        {
            get { return (NotificationPanelType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(NotificationPanelType), typeof(NotificationPanel));

        #endregion

        #region RightBar

        public object RightBar
        {
            get { return GetValue(RightBarProperty); }
            set { SetValue(RightBarProperty, value); }
        }

        public static readonly DependencyProperty RightBarProperty =
            DependencyProperty.Register("RightBar", typeof(object), typeof(NotificationPanel));

        #endregion

        #region DisableRoundingTopLeft

        public bool DisableRoundingTopLeft
        {
            get { return (bool)GetValue(DisableRoundingTopLeftProperty); }
            set { SetValue(DisableRoundingTopLeftProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingTopLeftProperty =
            CornerRoundingHelper.DisableRoundingTopLeftProperty.AddOwner(typeof(NotificationPanel));

        #endregion

        #region DisableRoundingTopRight

        public bool DisableRoundingTopRight
        {
            get { return (bool)GetValue(DisableRoundingTopRightProperty); }
            set { SetValue(DisableRoundingTopRightProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingTopRightProperty =
            CornerRoundingHelper.DisableRoundingTopRightProperty.AddOwner(typeof(NotificationPanel));

        #endregion

        #region DisableRoundingBottomLeft

        public bool DisableRoundingBottomLeft
        {
            get { return (bool)GetValue(DisableRoundingBottomLeftProperty); }
            set { SetValue(DisableRoundingBottomLeftProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingBottomLeftProperty =
            CornerRoundingHelper.DisableRoundingBottomLeftProperty.AddOwner(typeof(NotificationPanel));

        #endregion

        #region DisableRoundingBottomRight

        public bool DisableRoundingBottomRight
        {
            get { return (bool)GetValue(DisableRoundingBottomRightProperty); }
            set { SetValue(DisableRoundingBottomRightProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingBottomRightProperty =
            CornerRoundingHelper.DisableRoundingBottomRightProperty.AddOwner(typeof(NotificationPanel));

        #endregion

        public override void OnApplyTemplate()
        {
            _root = GetTemplateChild(PART_Root) as Border;

            InvalidateCornerRadius();
        }

        private void InvalidateCornerRadius()
        {
            if (_root != null)
            {
                _root.CornerRadius = _cornerRoundingHelper.GetCornerRadius();
            }
        }

        private Border? _root;
        private readonly CornerRoundingHelper _cornerRoundingHelper;
    }
}
