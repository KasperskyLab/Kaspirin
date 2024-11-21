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

using System;
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class TextBlockLinesLimiterBehavior : Behavior<TextBlock, TextBlockLinesLimiterBehavior>
    {
        #region MaxLines

        public static readonly DependencyProperty MaxLinesProperty = DependencyProperty.RegisterAttached(
            "MaxLines",
            typeof(int),
            typeof(TextBlockLinesLimiterBehavior),
            new PropertyMetadata(OnMaxLinesChanged));

        public static int GetMaxLines(TextBlock element)
            => (int)element.GetValue(MaxLinesProperty);

        public static void SetMaxLines(TextBlock element, int value)
            => element.SetValue(MaxLinesProperty, value);

        private static void OnMaxLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBlock textBlock && GetIsEnabled(textBlock))
            {
                GetBehavior(textBlock).UpdateTextBlock();
            }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            SaveOriginalValues();
            UpdateTextBlock();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            ResetToOriginalValues();
        }

        private void UpdateTextBlock()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.WhenLoaded(() =>
            {
                var maxLines = GetMaxLines(AssociatedObject);
                if (maxLines != 0)
                {
                    AssociatedObject.MaxHeight = GetLineHeight(AssociatedObject) * maxLines;
                    AssociatedObject.TextTrimming = TextTrimming.CharacterEllipsis;
                    AssociatedObject.TextWrapping = TextWrapping.Wrap;
                }
                else
                {
                    ResetToOriginalValues();
                }
            });
        }

        private void SaveOriginalValues()
        {
            Guard.IsNotNull(AssociatedObject);

            _originalMaxHeight = AssociatedObject.MaxHeight;
            _originalTextWrapping = AssociatedObject.TextWrapping;
            _originalTextTrimming = AssociatedObject.TextTrimming;
        }

        private void ResetToOriginalValues()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.MaxHeight = _originalMaxHeight;
            AssociatedObject.TextTrimming = _originalTextTrimming;
            AssociatedObject.TextWrapping = _originalTextWrapping;
        }

        private static double GetLineHeight(TextBlock textBlock)
            => textBlock.LineStackingStrategy == LineStackingStrategy.BlockLineHeight && !double.IsNaN(textBlock.LineHeight)
                ? textBlock.LineHeight
                : Math.Ceiling(textBlock.FontSize * textBlock.FontFamily.LineSpacing);

        private double _originalMaxHeight;
        private TextTrimming _originalTextTrimming;
        private TextWrapping _originalTextWrapping;
    }
}