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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class TextInputMaskPlaceholder : TextInputPlaceholder
    {
        public TextInputMaskPlaceholder(IEnumerable<TextInputMaskItem> maskItems)
        {
            _maskItems = maskItems.ToArray();
            _displayText = new string(maskItems.Select(x => x.DisplayChar).ToArray());
        }

        public TextInputMaskItem[] GetMaskItems()
            => _maskItems.ToArray();

        public override IEnumerable<Inline> GetPlaceholderText(string? value, bool isRTL)
        {
            value ??= string.Empty;

            if (isRTL)
            {
                if (value.Length == 0)
                {
                    var visibleRun = new Run(_displayText);
                    return new[] { visibleRun };
                }
                else
                {
                    var invisibleRun = new Run(_displayText) { Foreground = Brushes.Transparent };
                    return new[] { invisibleRun };
                }
            }
            else
            {
                var invisibleRun = new Run(value) { Foreground = Brushes.Transparent };
                var visibleRun = new Run(_displayText.Substring(value.Length));

                return new[] { invisibleRun, visibleRun };
            }
        }

        public override string? FilterInputText(string? inputText)
        {
            if (inputText == null)
            {
                return null;
            }

            if (inputText.Length == 0)
            {
                return inputText;
            }

            var inputCharStack = new Stack<char>(inputText.Reverse());
            var inputStringBuilder = new StringBuilder();

            foreach (var maskItem in _maskItems)
            {
                if (maskItem is TextInputMaskStaticItem staticItem)
                {
                    inputStringBuilder.Append(staticItem.DisplayChar);
                }

                if (maskItem is TextInputMaskRegexItem regexItem)
                {
                    while (inputCharStack.Count != 0)
                    {
                        var inputChar = inputCharStack.Pop();
                        if (regexItem.Match(inputChar, out var result))
                        {
                            inputStringBuilder.Append(result);
                            break;
                        }
                    }
                }

                if (inputCharStack.Count == 0)
                {
                    break;
                }
            }

            return inputStringBuilder.ToString();
        }

        private readonly TextInputMaskItem[] _maskItems;
        private readonly string _displayText;
    }
}
