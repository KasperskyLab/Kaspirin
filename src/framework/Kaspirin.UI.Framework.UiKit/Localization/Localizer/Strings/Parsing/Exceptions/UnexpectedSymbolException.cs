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
    public sealed class UnexpectedSymbolException : SyntaxErrorException
    {
        public UnexpectedSymbolException(int startPosition, string fileName, string sourceText)
        {
            Position = new Position(startPosition, startPosition + 1, fileName, sourceText);
            SourceText = sourceText;
        }

        public override string ErrorMessage => $"Unexpected symbol '{Position.GetText(SourceText)}\r\n{Position.GetStartLineTextWithPointer()}'";
        public override Position Position { get; }
        public string SourceText { get; }
    }
}