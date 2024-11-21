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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_Border, Type = typeof(Border))]
    public abstract class TagBase : Control
    {
        public const string PART_Border = "PART_Border";

        protected TagBase()
        {
            _cornerRoundingHelper = new CornerRoundingHelper(this, InvalidateCornerRadius);
        }

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TagBase));

        #endregion

        #region DisableRoundingTopLeft

        public bool DisableRoundingTopLeft
        {
            get { return (bool)GetValue(DisableRoundingTopLeftProperty); }
            set { SetValue(DisableRoundingTopLeftProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingTopLeftProperty =
            CornerRoundingHelper.DisableRoundingTopLeftProperty.AddOwner(typeof(TagBase));

        #endregion

        #region DisableRoundingTopRight

        public bool DisableRoundingTopRight
        {
            get { return (bool)GetValue(DisableRoundingTopRightProperty); }
            set { SetValue(DisableRoundingTopRightProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingTopRightProperty =
            CornerRoundingHelper.DisableRoundingTopRightProperty.AddOwner(typeof(TagBase));

        #endregion

        #region DisableRoundingBottomLeft

        public bool DisableRoundingBottomLeft
        {
            get { return (bool)GetValue(DisableRoundingBottomLeftProperty); }
            set { SetValue(DisableRoundingBottomLeftProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingBottomLeftProperty =
            CornerRoundingHelper.DisableRoundingBottomLeftProperty.AddOwner(typeof(TagBase));

        #endregion

        #region DisableRoundingBottomRight

        public bool DisableRoundingBottomRight
        {
            get { return (bool)GetValue(DisableRoundingBottomRightProperty); }
            set { SetValue(DisableRoundingBottomRightProperty, value); }
        }

        public static readonly DependencyProperty DisableRoundingBottomRightProperty =
            CornerRoundingHelper.DisableRoundingBottomRightProperty.AddOwner(typeof(TagBase));

        #endregion

        public override void OnApplyTemplate()
        {
            _border = GetTemplateChild(PART_Border) as Border;

            InvalidateCornerRadius();
        }

        private void InvalidateCornerRadius()
        {
            if (_border != null)
            {
                _border.CornerRadius = _cornerRoundingHelper.GetCornerRadius();
            }
        }

        private Border? _border;
        private readonly CornerRoundingHelper _cornerRoundingHelper;
    }
}
