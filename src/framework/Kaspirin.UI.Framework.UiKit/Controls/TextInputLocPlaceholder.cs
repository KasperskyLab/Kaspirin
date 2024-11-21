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
    public sealed class TextInputLocPlaceholder : TextInputPlaceholder
    {
        public TextInputLocPlaceholder(LocExtension locExtension)
        {
            _locExtension = locExtension;
        }

        public override IEnumerable<Inline> GetPlaceholderText(string? value, bool isRTL)
        {
            var placeholderRun = new Run();

            var isEmpty = (value ?? string.Empty).Length == 0;
            if (isEmpty)
            {
                var placeholderText = _locExtension.ProvideValue(placeholderRun, Run.TextProperty);

                placeholderRun.SetValue(Run.TextProperty, placeholderText);
            }

            return new[] { placeholderRun };
        }

        public override string? FilterInputText(string? value)
        {
            return value;
        }

        private readonly LocExtension _locExtension;
    }
}
