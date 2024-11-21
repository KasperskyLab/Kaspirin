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
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [ContentProperty(nameof(Elements))]
    [MarkupExtensionReturnType(typeof(IEnumerable<TextInputMaskItem>))]
    public sealed class TextInputMaskExtension : MarkupExtension
    {
        public List<TextInputMaskElement> Elements { get; set; } = new();

        public bool ToUpper { get; set; }

        public string? DisplayText { get; set; }

        public string? Mask { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Mask != null)
            {
                if (DisplayText != null && DisplayText.Length != Mask.Length)
                {
                    throw new InvalidOperationException($"DisplayText '{DisplayText}' length must be equal Mask '{Mask}' length.");
                }

                var displayText = DisplayText ?? new string(' ', Mask.Length);

                return new TextInputMaskPlaceholder(Mask.Select((c, i) =>
                {
                    var displayChar = displayText[i];
                    var maskType = (TextInputMaskElementType)char.GetNumericValue(c);
                    var result = maskType switch
                    {
                        TextInputMaskElementType.Digit => new TextInputMaskRegexItem(_digitRegex, ToUpper, displayChar),
                        TextInputMaskElementType.DigitOrEngLetter => new TextInputMaskRegexItem(_digitOrEngLetterRegex, ToUpper, displayChar),
                        TextInputMaskElementType.DigitOrLetter => new TextInputMaskRegexItem(_digitOrLetterRegex, ToUpper, displayChar),
                        _ => (object)new TextInputMaskStaticItem(displayChar),
                    };

                    return (TextInputMaskItem)result;
                }));
            }

            return new TextInputMaskPlaceholder(Elements.SelectMany(e =>
            {
                var maskItem = GetMaskItem(e);
                return Enumerable.Repeat(maskItem, e.RepeatCount);
            }));
        }

        private TextInputMaskItem GetMaskItem(TextInputMaskElement e)
            => e.Type switch
            {
                TextInputMaskElementType.Static => new TextInputMaskStaticItem(e.DisplayChar),
                TextInputMaskElementType.Digit => new TextInputMaskRegexItem(_digitRegex, ToUpper, e.DisplayChar),
                TextInputMaskElementType.DigitOrEngLetter => new TextInputMaskRegexItem(_digitOrEngLetterRegex, ToUpper, e.DisplayChar),
                TextInputMaskElementType.DigitOrLetter => new TextInputMaskRegexItem(_digitOrLetterRegex, ToUpper, e.DisplayChar),
                TextInputMaskElementType.AnyChar => new TextInputMaskRegexItem(_anyCharRegex, ToUpper, e.DisplayChar),
                TextInputMaskElementType.Custom => new TextInputMaskRegexItem(new Regex(e.CustomPattern), ToUpper, e.DisplayChar),
                _ => throw new ArgumentOutOfRangeException($"Value '{e.Type}' of type {nameof(TextInputMaskElementType)} not supported."),
            };

        private static readonly Regex _anyCharRegex = new(@".");
        private static readonly Regex _digitRegex = new(@"\d");
        private static readonly Regex _digitOrLetterRegex = new(@"\w");
        private static readonly Regex _digitOrEngLetterRegex = new(@"[a-zA-Z0-9]");
    }
}
