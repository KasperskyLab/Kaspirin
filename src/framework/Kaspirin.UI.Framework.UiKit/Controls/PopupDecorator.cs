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
using System.Windows.Media.Effects;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Extensions.Internals;

using WpfPopup = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_BackgroundDecorator, Type = typeof(Decorator))]
    internal abstract class PopupDecorator : ContentControl
    {
        public const string PART_BackgroundDecorator = "PART_BackgroundDecorator";

        static PopupDecorator()
        {
            FlowDirectionProperty.OverrideMetadata(
                typeof(PopupDecorator),
                new FrameworkPropertyMetadata(FlowDirection.LeftToRight, OnFlowDirectionChanged));
        }

        protected PopupDecorator()
        {
            Loaded += OnLoaded;
        }

        #region Offset

        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(double), typeof(PopupDecorator), new PropertyMetadata(UIKitConstants.PopupDecoratorOffset));

        #endregion

        public override void OnApplyTemplate()
        {
            _background = Guard.EnsureIsInstanceOfType<Decorator>(GetTemplateChild(PART_BackgroundDecorator));
        }

        protected double ShadowOffset => _background != null && _background.Effect is DropShadowEffect shadow ? shadow.GetShadowOffset() : 0D;

        protected virtual void SetPopupLocation()
        {
            var rootPopup = this.FindLogicalParent<WpfPopup>();
            if (rootPopup != null)
            {
                rootPopup.Placement = PlacementMode.Custom;
                rootPopup.CustomPopupPlacementCallback = PopupPlacementCallback;
            }
        }

        private static void OnFlowDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PopupDecorator)d).SetPopupLocation();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SetPopupLocation();
        }

        private CustomPopupPlacement[] PopupPlacementCallback(Size popupSize, Size targetSize, Point offset)
        {
            return new PopupPositionProvider(popupSize, targetSize, FlowDirection, Offset, ShadowOffset).LocateBottomRight();
        }

        private Decorator? _background;
    }
}
