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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class TextBlockExtensions
    {
        public static bool IsTextTrimmed(this TextBlock textBlock)
        {
            Guard.ArgumentIsNotNull(textBlock);

            if (!textBlock.IsArrangeValid || !textBlock.IsMeasureValid)
            {
                return false;
            }

            var formattedText = GetFormattedText(textBlock);
            if (formattedText == null)
            {
                return false;
            }

            formattedText.Trimming = TextTrimming.None;
            formattedText.TextAlignment = textBlock.TextAlignment;

            if (textBlock.TextWrapping == TextWrapping.NoWrap)
            {
                var widthLimit = GetNullable(textBlock.MaxWidth) ?? GetNullable(textBlock.Width);
                var width = widthLimit != null
                    ? Math.Min(textBlock.ActualWidth, widthLimit.Value)
                    : textBlock.ActualWidth;

                if ((int)formattedText.Width > (int)(width - textBlock.Padding.Left - textBlock.Padding.Right))
                {
                    return true;
                }
            }

            formattedText.LineHeight = textBlock.LineHeight;
            formattedText.MaxTextWidth = textBlock.ActualWidth - textBlock.Padding.Left - textBlock.Padding.Right + 0.5;

            var heightLimit = GetNullable(textBlock.MaxHeight) ?? GetNullable(textBlock.Height);
            var height = heightLimit != null
                ? Math.Min(textBlock.ActualHeight, heightLimit.Value)
                : textBlock.ActualHeight;

            return (int)formattedText.Height > (int)(height - textBlock.Padding.Top - textBlock.Padding.Bottom);
        }

        public static string GetTextOrInlineContent(this TextBlock textBlock, bool processLineBreaks = false)
        {
            Guard.ArgumentIsNotNull(textBlock);

            var text = textBlock.Text;
            if (string.IsNullOrWhiteSpace(text) && textBlock.Inlines.Any())
            {
                text = GetTextBetweenTextPointers(
                    textBlock.Inlines.FirstInline.ContentStart,
                    textBlock.Inlines.LastInline.ContentEnd,
                    processLineBreaks);
            }

            return text;
        }

        public static string GetTextBetweenTextPointers(TextPointer start, TextPointer end, bool processLineBreaks = false)
        {
            Guard.ArgumentIsNotNull(start);
            Guard.ArgumentIsNotNull(end);

            var next = start;
            var buffer = new StringBuilder();
            while (next != null && next.CompareTo(end) < 0)
            {
                var pointerContext = next.GetPointerContext(LogicalDirection.Forward);
                switch (pointerContext)
                {
                    case TextPointerContext.ElementStart:
                        if (processLineBreaks && next.GetAdjacentElement(LogicalDirection.Forward)?.GetType() == typeof(LineBreak))
                        {
                            buffer.AppendLine();
                        }

                        break;

                    case TextPointerContext.Text:
                        buffer.Append(next.GetTextInRun(LogicalDirection.Forward));
                        break;
                }

                next = next.GetNextContextPosition(LogicalDirection.Forward);
            }

            return buffer.ToString();
        }

        private static FormattedText? GetFormattedText(TextBlock textBlock)
        {
            if (textBlock.Inlines.Any())
            {
                var firstInline = textBlock.Inlines.FirstInline;
                var lastInline = textBlock.Inlines.LastInline;

                var text = GetTextBetweenTextPointers(firstInline.ContentStart, lastInline.ContentEnd, true);
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }

                var formattedText = new FormattedText(
                    text,
                    LocalizationManager.Current.DisplayCulture.CultureInfo,
                    firstInline.FlowDirection,
                    new Typeface(
                        firstInline.FontFamily,
                        firstInline.FontStyle,
                        firstInline.FontWeight,
                        firstInline.FontStretch),
                    firstInline.FontSize,
                    firstInline.Foreground);

                var index = GetInlineLength(firstInline);
                CustomizeFormattedText(formattedText, firstInline.NextInline, ref index);

                return formattedText;
            }
            else if (!string.IsNullOrEmpty(textBlock.Text))
            {
                return new FormattedText(
                    textBlock.Text,
                    LocalizationManager.Current.DisplayCulture.CultureInfo,
                    textBlock.FlowDirection,
                    new Typeface(
                        textBlock.FontFamily,
                        textBlock.FontStyle,
                        textBlock.FontWeight,
                        textBlock.FontStretch),
                    textBlock.FontSize,
                    textBlock.Foreground);
            }

            return null;
        }

        private static void CustomizeFormattedText(FormattedText formattedText, Inline? inline, ref int index)
        {
            while (inline != null)
            {
                if (inline is Span span && span.Inlines.Any())
                {
                    CustomizeFormattedText(formattedText, span.Inlines.FirstInline, ref index);
                }
                else
                {
                    var count = GetInlineLength(inline);
                    if (count > 0)
                    {
                        formattedText.SetFontTypeface(
                            new Typeface(
                                inline.FontFamily,
                                inline.FontStyle,
                                inline.FontWeight,
                                inline.FontStretch),
                            index,
                            count);

                        formattedText.SetFontSize(inline.FontSize, index, count);
                        formattedText.SetForegroundBrush(inline.Foreground, index, count);

                        index += count;
                    }
                }

                inline = inline.NextInline;
            }
        }

        private static int GetInlineLength(Inline inline)
        {
            if (inline.GetType() == typeof(LineBreak))
            {
                return Environment.NewLine.Length;
            }

            return inline.ContentStart.GetTextRunLength(LogicalDirection.Forward);
        }

        private static double? GetNullable(double value)
            => double.IsNaN(value) || double.IsInfinity(value)
                ? null
                : value;
    }
}