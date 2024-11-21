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
using System.Windows.Input;
using Kaspirin.UI.Framework.UiKit.Controls.Behaviors;
using Kaspirin.UI.Framework.UiKit.Controls.Properties;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_LeftButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_RightButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_ScrollViewer, Type = typeof(ScrollViewer))]
    public sealed class FadeLine : ContentControl
    {
        public const string PART_LeftButton = "PART_LeftButton";
        public const string PART_RightButton = "PART_RightButton";
        public const string PART_ScrollViewer = "PART_ScrollViewer";

        public FadeLine()
        {
            _leftButtonRepeatBehavior = new ButtonRepeatBehavior();
            _leftButtonRepeatBehavior.Repeat += OnLeftButtonRepeat;

            _rightButtonRepeatBehavior = new ButtonRepeatBehavior();
            _rightButtonRepeatBehavior.Repeat += OnRightButtonRepeat;
        }

        public override void OnApplyTemplate()
        {
            _scrollViewer = (ScrollViewer)GetTemplateChild(PART_ScrollViewer);
            _scrollViewer.SetValue(ScrollViewerProps.MouseWheelScrollOrientationProperty, Orientation.Horizontal);
            _scrollViewer.SetValue(ScrollViewerProps.IsBorderFadeEnabledProperty, true);
            _scrollViewer.PreviewKeyDown += OnScrollViewerKeyDown;
            _scrollViewer.ScrollChanged += OnScrollChanged;

            _leftButton = (ButtonBase)GetTemplateChild(PART_LeftButton);
            _rightButton = (ButtonBase)GetTemplateChild(PART_RightButton);

            Interaction.GetBehaviors(_leftButton).Add(_leftButtonRepeatBehavior);
            Interaction.GetBehaviors(_rightButton).Add(_rightButtonRepeatBehavior);
        }

        public void ScrollIntoView(FrameworkElement element)
        {
            if (_scrollViewer != null)
            {
                var scrollFadeWidth = ScrollViewerProps.GetBorderFadeWidth(_scrollViewer);

                var itemBounds = _scrollViewer.GetElementBounds(element);
                if (itemBounds.Left < 0)
                {
                    var offset = new Rect(-scrollFadeWidth, 0, element.ActualWidth, element.ActualHeight);
                    element.BringIntoView(offset);
                }

                if (itemBounds.Right > _scrollViewer.ActualWidth)
                {
                    var offset = new Rect(0, 0, element.ActualWidth + scrollFadeWidth, element.ActualHeight);
                    element.BringIntoView(offset);
                }
            }
        }

        private void OnRightButtonRepeat()
        {
            _scrollViewer?.LineRight();
        }

        private void OnLeftButtonRepeat()
        {
            _scrollViewer?.LineLeft();
        }

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_scrollViewer != null)
            {
                if (_leftButton != null)
                {
                    _leftButton.IsEnabled = _scrollViewer.CanScrollLeft();
                }

                if (_rightButton != null)
                {
                    _rightButton.IsEnabled = _scrollViewer.CanScrollRight();
                }
            }
        }

        private void OnScrollViewerKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                e.Handled = true;

                RaiseEvent(new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, e.Key)
                {
                    RoutedEvent = KeyDownEvent,
                    Source = sender
                });
            }
        }

        private readonly ButtonRepeatBehavior _leftButtonRepeatBehavior;
        private readonly ButtonRepeatBehavior _rightButtonRepeatBehavior;

        private ScrollViewer? _scrollViewer;
        private ButtonBase? _leftButton;
        private ButtonBase? _rightButton;
    }
}
