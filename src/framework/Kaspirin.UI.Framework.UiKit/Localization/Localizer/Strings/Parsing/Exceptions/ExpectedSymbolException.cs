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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Exceptions
{
    public sealed class ExpectedSymbolException : SyntaxErrorException
    {
        public ExpectedSymbolException(char expectedChar, int pos, string fileName, string sourceText)
        {
            ExpectedChar = expectedChar;
            Position = new Position(pos, pos, fileName, sourceText);
        }

        public char ExpectedChar { get; }
        public override Position Position { get; }

        public override string ErrorMessage => Position.Start >= Position.SourceText.Length
            ? $"Expected '{ExpectedChar}' symbol, but End Of File found\r\n{Position.GetStartLineTextWithPointer()}"
            : $"Expected '{ExpectedChar}' symbol\r\n{Position.GetStartLineTextWithPointer()}";
    }
}