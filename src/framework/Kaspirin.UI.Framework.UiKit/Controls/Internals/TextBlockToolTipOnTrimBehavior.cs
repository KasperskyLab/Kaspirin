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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class TextBlockToolTipOnTrimBehavior : Behavior<TextBlock, TextBlockToolTipOnTrimBehavior>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.MouseEnter += SetToolTipMouseEnter;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.MouseEnter -= SetToolTipMouseEnter;
        }

        private void SetToolTipMouseEnter(object? sender, EventArgs e)
        {
            Guard.IsNotNull(AssociatedObject);
            SetToolTip(AssociatedObject);
        }

        private void SetToolTip(TextBlock textBlock)
        {
            textBlock.WhenLoaded(() =>
            {
                if (string.IsNullOrEmpty(textBlock.Text))
                {
                    return;
                }

                var currentCompareInfoHash = GetTextBlockCompareInfoHash(textBlock);
                if (_lastCompareInfoHash == currentCompareInfoHash)
                {
                    return;
                }

                _lastCompareInfoHash = currentCompareInfoHash;
                textBlock.ToolTip = textBlock.IsTextTrimmed()
                    ? textBlock.GetTextOrInlineContent()
                    : null;
            });
        }

        private static int GetTextBlockCompareInfoHash(TextBlock textBlock)
            => new TextBlockCompareInfo(textBlock, LocalizationManager.Current.DisplayCulture.CultureInfo).GetHashCode();

        private int _lastCompareInfoHash;

        #region Nested types

        private sealed class TextBlockCompareInfo
        {
            public override int GetHashCode()
                => ((((typeof(TextBlockCompareInfo).GetHashCode() * HashMultiplier
                    + TextInfo.GetHashCode()) * HashMultiplier
                    + InlineInfos
                        .Select(inlineInfo => inlineInfo.GetHashCode())
                        .Aggregate(0, (hash1, hash2) => hash1 * HashMultiplier + hash2)) * HashMultiplier
                    + TextBlockWidth.GetHashCode()) * HashMultiplier
                    + TextBlockHeight.GetHashCode()) * HashMultiplier
                    + CultureInfo.GetHashCode();

            internal TextBlockCompareInfo(TextBlock textBlock, CultureInfo cultureInfo)
            {
                TextInfo = GetTextInfo(textBlock);
                InlineInfos = GetInlinesInfo(textBlock).ToArray();

                TextBlockWidth = textBlock.ActualWidth.ToString("N2");
                TextBlockHeight = textBlock.ActualHeight.ToString("N2");

                CultureInfo = cultureInfo;
            }

            private TextCompareInfo TextInfo { get; }
            private TextCompareInfo[] InlineInfos { get; }

            private string TextBlockWidth { get; }
            private string TextBlockHeight { get; }

            private CultureInfo CultureInfo { get; }

            private static TextCompareInfo GetTextInfo(TextBlock textBlock)
                => new(textBlock.Text, textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch);

            private static IEnumerable<TextCompareInfo> GetInlinesInfo(TextBlock textBlock)
            {
                var inline = textBlock.Inlines.FirstInline;
                while (inline is not null)
                {
                    yield return new TextCompareInfo(
                        TextBlockExtensions.GetTextBetweenTextPointers(inline.ContentStart, inline.ContentEnd, true),
                        inline.FontFamily,
                        inline.FontStyle,
                        inline.FontWeight,
                        inline.FontStretch);

                    inline = inline.NextInline;
                }
            }

            private const int HashMultiplier = -1521134295;
        };

        private sealed class TextCompareInfo
        {
            public override int GetHashCode()
                => ((((typeof(TextCompareInfo).GetHashCode() * HashMultiplier
                    + Text.GetHashCode()) * HashMultiplier
                    + FontFamily.GetHashCode()) * HashMultiplier
                    + FontStyle.GetHashCode()) * HashMultiplier
                    + FontWeight.GetHashCode()) * HashMultiplier
                    + FontStretch.GetHashCode();

            internal TextCompareInfo(string text, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch)
            {
                Text = text;
                FontFamily = fontFamily;
                FontStyle = fontStyle;
                FontWeight = fontWeight;
                FontStretch = fontStretch;
            }

            private string Text { get; }

            private FontFamily FontFamily { get; }
            private FontStyle FontStyle { get; }
            private FontWeight FontWeight { get; }
            private FontStretch FontStretch { get; }

            private const int HashMultiplier = -1521134295;
        };

        #endregion
    }
}