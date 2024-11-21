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

using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class TextInputMaskRegexItem : TextInputMaskItem
    {
        public TextInputMaskRegexItem(Regex charRegex, bool uppercase, char displayChar) : base(displayChar)
        {
            _charRegex = charRegex;
            _uppercase = uppercase;
        }

        public bool Match(char value, out char result)
        {
            if (_charRegex.IsMatch(value.ToString()))
            {
                result = _uppercase ? char.ToUpper(value) : value;
                return true;
            }

            result = default;
            return false;
        }

        private readonly Regex _charRegex;
        private readonly bool _uppercase;
    }
}
