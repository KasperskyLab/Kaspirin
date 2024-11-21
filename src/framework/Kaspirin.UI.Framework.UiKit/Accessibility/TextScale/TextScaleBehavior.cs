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

using PopupWpf = System.Windows.Controls.Primitives.Popup;

namespace Kaspirin.UI.Framework.UiKit.Accessibility.TextScale
{
    public sealed class TextScaleBehavior : Behavior<FrameworkElement, TextScaleBehavior>
    {
        public TextScaleBehavior()
        {
            _textScaleService = ServiceLocator.Instance.GetService<ITextScaleService>();
            _currentScale = OriginScale;
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            _textScaleService.ScaleFactorChanged -= OnScaleFactorChanged;
            _textScaleService.ScaleFactorChanged += OnScaleFactorChanged;

            UpdateLayoutTransform(_textScaleService.ScaleFactor);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            _textScaleService.ScaleFactorChanged -= OnScaleFactorChanged;

            UpdateLayoutTransform(OriginScale);
        }

        protected override void OnAssociatedObjectLoaded()
        {
            base.OnAssociatedObjectLoaded();

            _textScaleService.ScaleFactorChanged -= OnScaleFactorChanged;
            _textScaleService.ScaleFactorChanged += OnScaleFactorChanged;

            UpdateLayoutTransform(_textScaleService.ScaleFactor);
        }

        protected override void OnAssociatedObjectUnloaded()
        {
            base.OnAssociatedObjectLoaded();

            _textScaleService.ScaleFactorChanged -= OnScaleFactorChanged;

            UpdateLayoutTransform(OriginScale);
        }

        private void OnScaleFactorChanged(TextScaleService sender, TextScaleChangedEventArgs eventArgs)
        {
            UpdateLayoutTransform(eventArgs.NewScale);
        }

        private void UpdateLayoutTransform(double scale)
        {
            var oldScale = _currentScale;
            var newScale = scale;

            if (_currentScale.NearlyEqual(scale))
            {
                return;
            }

            _currentScale = scale;

            if (AssociatedObject is Window window)
            {
                ScaleWindow(window, oldScale, newScale);
            }
            else if (AssociatedObject is PopupWpf popup)
            {
                ScalePopup(popup, newScale);
            }
            else if (AssociatedObject is FrameworkElement frameworkElement)
            {
                ScaleFrameworkElement(frameworkElement, newScale);
            }
        }

        private void ScaleWindow(Window target, double oldScale, double newScale)
        {
            target.WhenLoaded(() =>
            {
                WindowScaleHelper.ScaleContentByTextScaleChange(target, newScale);

                var canResize = target.ResizeMode == ResizeMode.CanResize ||
                                target.ResizeMode == ResizeMode.CanResizeWithGrip;

                if (canResize is false)
                {
                    var scaleChangeRatio = newScale / oldScale;
                    var sizeToContent = target.SizeToContent;

                    target.SetCurrentValue(Window.SizeToContentProperty, SizeToContent.Manual);

                    WindowScaleHelper.ScaleWindowByTextScaleChange(target, scaleChangeRatio);

                    target.SetCurrentValue(Window.SizeToContentProperty, sizeToContent);
                }
            });
        }

        private void ScaleFrameworkElement(FrameworkElement target, double scale)
        {
            target.LayoutTransform = (ScaleTransform)new ScaleTransform(scale, scale).GetAsFrozen();
        }

        private void ScalePopup(PopupWpf target, double scale)
        {
            if (target.Child is FrameworkElement popupChild)
            {
                var isPopupOpen = popupChild.IsLoaded && target.IsOpen;
                var isTransformInherited = popupChild is ToolTip ||
                                           popupChild is ContextMenu;

                if (isPopupOpen || isTransformInherited)
                {
                    popupChild.LayoutTransform = (ScaleTransform)new ScaleTransform(scale, scale).GetAsFrozen();
                }
            }
        }

        private const double OriginScale = 1;

        private readonly ITextScaleService _textScaleService;
        private double _currentScale;
    }
}