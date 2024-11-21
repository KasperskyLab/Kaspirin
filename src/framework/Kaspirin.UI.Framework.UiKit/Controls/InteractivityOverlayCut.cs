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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class InteractivityOverlayCut : InteractivityOverlayCutBase
    {
        #region AllowsInteraction

        public bool AllowsInteraction
        {
            get => (bool)GetValue(AllowsInteractionProperty);
            set => SetValue(AllowsInteractionProperty, value);
        }

        public static readonly DependencyProperty AllowsInteractionProperty = DependencyProperty.Register(
            nameof(AllowsInteraction), typeof(bool), typeof(InteractivityOverlayCut), new PropertyMetadata(true));

        #endregion

        #region CloseOnMouseClick

        public bool CloseOnMouseClick
        {
            get => (bool)GetValue(CloseOnMouseClickProperty);
            set => SetValue(CloseOnMouseClickProperty, value);
        }

        public static readonly DependencyProperty CloseOnMouseClickProperty =
            DependencyProperty.Register(nameof(CloseOnMouseClick), typeof(bool), typeof(InteractivityOverlayCut));

        #endregion

        #region CloseOnMouseWheel

        public bool CloseOnMouseWheel
        {
            get => (bool)GetValue(CloseOnMouseWheelProperty);
            set => SetValue(CloseOnMouseWheelProperty, value);
        }

        public static readonly DependencyProperty CloseOnMouseWheelProperty =
            DependencyProperty.Register(nameof(CloseOnMouseWheel), typeof(bool), typeof(InteractivityOverlayCut));

        #endregion

        #region Decorator

        public object? Decorator
        {
            get => GetValue(DecoratorProperty);
            set => SetValue(DecoratorProperty, value);
        }

        public static readonly DependencyProperty DecoratorProperty =
            DependencyProperty.Register(nameof(Decorator), typeof(object), typeof(InteractivityOverlayCut));

        #endregion

        #region DecoratorTemplate

        public DataTemplate? DecoratorTemplate
        {
            get => (DataTemplate?)GetValue(DecoratorTemplateProperty);
            set => SetValue(DecoratorTemplateProperty, value);
        }

        public static readonly DependencyProperty DecoratorTemplateProperty =
            DependencyProperty.Register(nameof(DecoratorTemplate), typeof(DataTemplate), typeof(InteractivityOverlayCut));

        #endregion

        #region DecoratorPosition

        public InteractivityOverlayCutDecoratorPosition DecoratorPosition
        {
            get => (InteractivityOverlayCutDecoratorPosition)GetValue(DecoratorPositionProperty);
            set => SetValue(DecoratorPositionProperty, value);
        }

        public static readonly DependencyProperty DecoratorPositionProperty =
            DependencyProperty.Register(nameof(DecoratorPosition), typeof(InteractivityOverlayCutDecoratorPosition), typeof(InteractivityOverlayCut));

        #endregion

        #region DecoratorHorizontalOffset

        public double DecoratorHorizontalOffset
        {
            get => (double)GetValue(DecoratorHorizontalOffsetProperty);
            set => SetValue(DecoratorHorizontalOffsetProperty, value);
        }

        public static readonly DependencyProperty DecoratorHorizontalOffsetProperty =
            DependencyProperty.Register(nameof(DecoratorHorizontalOffset), typeof(double), typeof(InteractivityOverlayCut),
                new PropertyMetadata(default(double)));

        #endregion

        #region DecoratorVerticalOffset

        public double DecoratorVerticalOffset
        {
            get => (double)GetValue(DecoratorVerticalOffsetProperty);
            set => SetValue(DecoratorVerticalOffsetProperty, value);
        }

        public static readonly DependencyProperty DecoratorVerticalOffsetProperty =
            DependencyProperty.Register(nameof(DecoratorVerticalOffset), typeof(double), typeof(InteractivityOverlayCut),
                new PropertyMetadata(default(double)));

        #endregion
    }
}
