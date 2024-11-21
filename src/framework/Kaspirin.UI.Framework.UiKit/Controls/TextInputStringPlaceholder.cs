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
using System.Windows.Documents;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class TextInputStringPlaceholder : TextInputPlaceholder
    {
        public static TextInputStringPlaceholder Empty { get; } = new(string.Empty);

        public TextInputStringPlaceholder(string text)
        {
            _text = text;
        }

        public override IEnumerable<Inline> GetPlaceholderText(string? value, bool isRTL)
        {
            value ??= string.Empty;
            var isEmpty = value.Length == 0;

            var placeholderRun = new Run(isEmpty ? _text : string.Empty);
            return new[] { placeholderRun };
        }

        public override string? FilterInputText(string? value)
        {
            return value;
        }

        private readonly string _text;
    }
}
