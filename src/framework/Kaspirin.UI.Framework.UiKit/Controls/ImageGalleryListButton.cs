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
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class ImageGalleryListButton : ButtonBase
    {
        #region ImageSource

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageGalleryListButton));

        #endregion

        #region ImageFlowDirection

        public FlowDirection ImageFlowDirection
        {
            get { return (FlowDirection)GetValue(ImageFlowDirectionProperty); }
            set { SetValue(ImageFlowDirectionProperty, value); }
        }

        public static readonly DependencyProperty ImageFlowDirectionProperty =
            DependencyProperty.Register("ImageFlowDirection", typeof(FlowDirection), typeof(ImageGalleryListButton),
                new PropertyMetadata(FlowDirection.LeftToRight));

        #endregion

        #region ImageStretch

        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ImageGalleryListButton),
                new PropertyMetadata(Stretch.Uniform));

        #endregion
    }
}
