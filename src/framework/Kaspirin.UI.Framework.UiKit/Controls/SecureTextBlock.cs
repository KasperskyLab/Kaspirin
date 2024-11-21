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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using BidiSharp;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class SecureTextBlock : Control
    {
        static SecureTextBlock()
        {
            FocusableProperty.OverrideMetadata(typeof(SecureTextBlock), new FrameworkPropertyMetadata(false));
        }

        public SecureTextBlock()
        {
            IsVisibleChanged += OnIsVisibleChanged;
        }

        #region SecureText

        public SecureString SecureText
        {
            get => (SecureString)GetValue(SecureTextProperty);
            set => SetValue(SecureTextProperty, value);
        }

        public static readonly DependencyProperty SecureTextProperty = DependencyProperty.Register(
            nameof(SecureText),
            typeof(SecureString),
            typeof(SecureTextBlock),
            new FrameworkPropertyMetadata(new SecureString(), FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region TextAlignment

        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
            nameof(TextAlignment),
            typeof(TextAlignment),
            typeof(SecureTextBlock),
            new FrameworkPropertyMetadata(default(TextAlignment), FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        protected override Size MeasureOverride(Size constraint)
        {
            var desiredSize = base.MeasureOverride(constraint);

            if (Visibility != Visibility.Collapsed)
            {
                _lineSpacing = FontFamily.LineSpacing;
                _baseline = FontFamily.Baseline;

                return GetDesiredSize(constraint);
            }

            return desiredSize;
        }

        protected override void OnRender(DrawingContext ctx)
        {
            base.OnRender(ctx);

            var bounds = new Rect(RenderSize);
            if (Visibility == Visibility.Visible && !bounds.IsEmpty)
            {
                ctx.PushClip(new RectangleGeometry(bounds));
                try
                {
                    DrawBackground(ctx, bounds);

                    if (SecureText.Length > 0)
                    {
                        DrawText(ctx);
                    }
                }
                finally
                {
                    ctx.Pop();
                }
            }
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is true)
            {
                InvalidateVisual();
            }
        }

        private void DrawBackground(DrawingContext ctx, Rect bounds)
            => ctx.DrawRectangle(Background, null, bounds);

        private void DrawText(DrawingContext ctx)
        {
            var text = SecureText.ToSimpleString();
            SafeNormalize(ref text);

            try
            {
                DrawText(ctx, text, LocalizationManager.Current.DisplayCulture.XmlLanguage);
            }
            finally
            {
                text.CleanupMemory();
            }
        }

        private void DrawText(DrawingContext ctx, string text, XmlLanguage language)
        {
            if (TextAlignment != TextAlignment.Center && Padding.Left + Padding.Right >= RenderSize.Width)
            {
                return;
            }

            var chars = Bidi.LogicalToVisualChars(text, (BaseDirection)FlowDirection);
            ApplyArabicPresentationForms(chars);

            try
            {
                if (FlowDirection == FlowDirection.LeftToRight)
                {
                    DrawGlyphRun(ctx, chars, FontSize, language);
                }
                else
                {
                    var textSize = MeasureChars(chars, FontSize, language);
                    var paddingHorizontalOffset = GetPaddingHorizontalOffset(textSize.Width);

                    ctx.PushTransform(CreateAntiInversionTransform(2 * paddingHorizontalOffset + textSize.Width));

                    try
                    {
                        DrawGlyphRun(ctx, chars, FontSize, language);
                    }
                    finally
                    {
                        ctx.Pop();
                    }
                }
            }
            finally
            {
                ClearList(chars);
            }
        }

        private Transform CreateAntiInversionTransform(double textWidth)
            => new MatrixTransform(-1, 0, 0, 1, textWidth, 0);

        private double GetPaddingHorizontalOffset(double textWidth)
        {
            return TextAlignment switch
            {
                TextAlignment.Left => Padding.Left,
                TextAlignment.Center => (ActualWidth + Padding.Left - textWidth - Padding.Right) / 2,
                TextAlignment.Right => ActualWidth - textWidth - Padding.Right,
                _ => throw new NotImplementedException($"TextAlignment value '{TextAlignment}' is not supported"),
            };
        }

        private void DrawGlyphRun(DrawingContext ctx, IList<char> chars, double renderingEmSize, XmlLanguage language)
        {
            var glyphIndicesAndAdvanceWidths = GetGlyphIndicesAndAdvanceWidths(chars, renderingEmSize, language, out var textWidth);

            var xOffset = GetPaddingHorizontalOffset(textWidth);

            foreach (var (glyphTypeface, (glyphIndices, advanceWidths)) in glyphIndicesAndAdvanceWidths)
            {
                try
                {
                    var yOffset = (_baseline + _lineSpacing - glyphTypeface!.Height) * renderingEmSize;

                    var isBlockLineHeight = TextBlock.GetLineStackingStrategy(this) is LineStackingStrategy.BlockLineHeight;
                    if (isBlockLineHeight)
                    {
                        yOffset = renderingEmSize + _baseline;
                    }

                    yOffset += Padding.Top;

                    if (UseLayoutRounding)
                    {
                        xOffset = Math.Round(xOffset);
                        yOffset = Math.Round(yOffset);
                    }

#if NET6_0_OR_GREATER
                    var dpi = (float)VisualTreeHelper.GetDpi(this).PixelsPerDip;
#endif

                    var glyphRun = new GlyphRun(
                        glyphTypeface,
                        bidiLevel: (int)FlowDirection.LeftToRight,
                        isSideways: false,
                        renderingEmSize,
#if NET6_0_OR_GREATER
                        dpi,
#endif
                        glyphIndices,
                        baselineOrigin: new Point(xOffset, yOffset),
                        advanceWidths,
                        glyphOffsets: null,
                        characters: null,
                        deviceFontName: null,
                        clusterMap: null,
                        caretStops: null,
                        language);

                    ctx.DrawGlyphRun(Foreground, glyphRun);

                    xOffset += advanceWidths.Sum();
                }
                finally
                {
                    // We can't clear glyph indices right after DrawGlyphRun call since it's asynchronous.
                    // Clear asynchronously using RenderPriority.Render.
                    Dispatcher.BeginInvoke(() => ClearList(glyphIndices), DispatcherPriority.Render);
                }
            }
        }

        private (GlyphTypeface GlyphTypeface, (List<ushort> GlyphIndices, List<double> AdvanceWidths))[] GetGlyphIndicesAndAdvanceWidths(
            IList<char> chars,
            double renderingEmSize,
            XmlLanguage language,
            out double textWidth)
        {
            var result = new List<(GlyphTypeface GlyphTypeface, (List<ushort> GlyphIndices, List<double> AdvanceWidths))>();

            var currentGlyphTypeface = default(GlyphTypeface);

            var glyphIndices = new List<ushort>();
            var advanceWidths = new List<double>();

            textWidth = 0;

            foreach (var c in chars)
            {
                if (!TryGetAdvanceWidth(c, renderingEmSize, language, out var glyphTypeface, out var glyphIndex, out var advanceWidth))
                {
                    continue;
                }

                if (glyphTypeface != currentGlyphTypeface)
                {
                    if (currentGlyphTypeface is not null)
                    {
                        result.Add((currentGlyphTypeface, (glyphIndices, advanceWidths)));

                        glyphIndices = new List<ushort>();
                        advanceWidths = new List<double>();
                    }

                    currentGlyphTypeface = glyphTypeface;
                }

                glyphIndices.Add(glyphIndex);
                advanceWidths.Add(advanceWidth);

                textWidth += advanceWidth;
            }

            if (currentGlyphTypeface is not null)
            {
                result.Add((currentGlyphTypeface, (glyphIndices, advanceWidths)));
            }

            return result.ToArray();
        }

        private bool TryGetAdvanceWidth(
            char c,
            double renderingEmSize,
            XmlLanguage language,
            [NotNullWhen(true)] out GlyphTypeface? glyphTypeface,
            out ushort glyphIndex,
            out double advanceWidth)
        {
            glyphTypeface = GetGlyphTypeface(c, language, out var emScale);
            if (glyphTypeface == null || !glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out glyphIndex))
            {
                glyphIndex = 0;
                advanceWidth = 0;
                return false;
            }

            renderingEmSize *= emScale;
            advanceWidth = glyphTypeface.AdvanceWidths[glyphIndex] * renderingEmSize;

            return true;
        }

        private GlyphTypeface? GetGlyphTypeface(char c, XmlLanguage language, out double emScale)
        {
            emScale = 1;
            GlyphTypeface? glyphTypeface = null;
            double typefaceScale = 1;

            if (GetFontFamilies().Any(fontFamily => TryGetGlyphTypeface(fontFamily, c, language, out glyphTypeface, out typefaceScale)))
            {
                emScale = typefaceScale;
                return glyphTypeface;
            }

            return default;
        }

        private Size GetDesiredSize(Size constraint)
        {
            var text = SecureText.ToSimpleString();
            SafeNormalize(ref text);

            try
            {
                var textSize = MeasureText(text, FontSize, LocalizationManager.Current.DisplayCulture.XmlLanguage);

                var height = Padding.Top + textSize.Height + Padding.Bottom;
                var width = Padding.Left + textSize.Width + Padding.Right;

                if (UseLayoutRounding)
                {
                    width = Math.Round(width);
                    height = Math.Round(height);
                }

                return new Size(
                   Math.Min(width, constraint.Width),
                   Math.Min(height, constraint.Height));
            }
            finally
            {
                text.CleanupMemory();
            }
        }

        private Size MeasureText(string text, double renderingEmSize, XmlLanguage language)
        {
            var chars = Bidi.LogicalToVisualChars(text, (BaseDirection)FlowDirection);
            ApplyArabicPresentationForms(chars);

            try
            {
                return MeasureChars(chars, renderingEmSize, language);
            }
            finally
            {
                ClearList(chars);
            }
        }

        private Size MeasureChars(IList<char> chars, double renderingEmSize, XmlLanguage language)
        {
            var height = _lineSpacing * FontSize;
            var width = 0d;

            foreach (var c in chars)
            {
                if (!TryGetAdvanceWidth(c, renderingEmSize, language, out _, out _, out var advanceWidth))
                {
                    continue;
                }

                width += advanceWidth;
            }

            return new Size(width, height);
        }

        private IEnumerable<FontFamily> GetFontFamilies()
        {
            yield return FontFamily;
            yield return new FontFamily("Yu Gothic UI"); // Preferred for Japanese.
            yield return new FontFamily("#GLOBAL USER INTERFACE");
        }

        private bool TryGetGlyphTypeface(FontFamily fontFamily, char c, XmlLanguage language, out GlyphTypeface? glyphTypeface, out double emScale)
        {
            var typeface = new Typeface(fontFamily, FontStyle, FontWeight, FontStretch);
            if (!typeface.TryGetGlyphTypeface(out glyphTypeface) || !glyphTypeface.CharacterToGlyphMap.ContainsKey(c))
            {
                glyphTypeface = null;
                foreach (var fontFamilyMap in fontFamily.FamilyMaps.OrderByDescending(map => map.Language == language))
                {
                    foreach (var unicodeRange in fontFamilyMap.Unicode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var range = unicodeRange.Trim().Split('-');

                        var rangeFrom = int.Parse(range[0], NumberStyles.HexNumber);
                        var rangeTo = (range.Length == 1) ? rangeFrom : int.Parse(range[1], NumberStyles.HexNumber);

                        if (c >= rangeFrom && c <= rangeTo)
                        {
                            var targetFont = new FontFamily(fontFamily.BaseUri, fontFamilyMap.Target);
                            if (TryGetGlyphTypeface(targetFont, c, language, out glyphTypeface, out emScale))
                            {
                                emScale = fontFamilyMap.Scale;
                                return true;
                            }
                        }
                    }
                }
            }

            emScale = 1;
            return glyphTypeface != null;
        }

        private static void SafeNormalize(ref string str)
        {
            var normalizationForm = NormalizationForm.FormC;

            if (str.IsNormalized(normalizationForm))
            {
                return;
            }

            var normalizedText = str.Normalize(normalizationForm);
            str.CleanupMemory();
            str = normalizedText;
        }

        private static void ClearList<T>(List<T> chars)
            => CollectionsMarshal.AsSpan(chars).Clear();

        private static void ApplyArabicPresentationForms(List<char> chars)
        {
            if (chars.Count < 2)
            {
                return;
            }

            var presentationForms = ArabicLetter.PresentationForms;

            var i = -1;
            var currentChar = char.MinValue;
            while (++i < chars.Count)
            {
                var nextChar = currentChar;
                currentChar = chars[i];
                if (!presentationForms.Contains(currentChar))
                {
                    continue;
                }

                var prevChar = i + 1 < chars.Count ? chars[i + 1] : char.MinValue;

                if (ArabicLetter.IsAlifChar(currentChar) && prevChar == ArabicLetter.Lam)
                {
                    switch (currentChar)
                    {
                        case ArabicLetter.Alif:
                            currentChar = ArabicLetter.LamWithAlif;
                            break;
                        case ArabicLetter.AlifMaddah:
                            currentChar = ArabicLetter.LamWithAlifMaddah;
                            break;
                        case ArabicLetter.AlifHamzaA:
                            currentChar = ArabicLetter.LamWithAlifHamzaA;
                            break;
                        case ArabicLetter.AlifHamzaB:
                            currentChar = ArabicLetter.LamWithAlifHamzaB;
                            break;
                    }

                    chars.RemoveAt(i);
                    prevChar = i + 1 < chars.Count ? chars[i + 1] : char.MinValue;
                }

                var letter = presentationForms[currentChar];

                if (presentationForms.Contains(prevChar) && presentationForms[prevChar].HasMedialForm)
                {
                    chars[i] = presentationForms.Contains(nextChar)
                        ? letter.MedialForm
                        : letter.FinalForm;
                }
                else
                {
                    chars[i] = presentationForms.Contains(nextChar)
                        ? letter.InitialForm
                        : letter.IsolatedForm;
                }
            }
        }

        private double _lineSpacing;
        private double _baseline;
    }
}
