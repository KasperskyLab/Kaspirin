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
using System.Windows.Markup;
using Kaspirin.UI.Framework.UiKit.Converters.DictionaryConverters;

namespace Kaspirin.UI.Framework.UiKit.Illustrations
{
    [MarkupExtensionReturnType(typeof(Enum))]
    public abstract class UIKitIllustrationExtensionBase : MarkupExtension, IDictionaryConverterItem
    {
        public override object? ProvideValue(IServiceProvider serviceProvider)
            => _value;

        public object? GetItemValue()
            => _value;

        protected void EnsureCanSet<TValue>(ref TValue target, TValue value) where TValue : Enum
        {
            if (_value == null)
            {
                _value = value;
                target = value;
            }
            else
            {
                throw new InvalidOperationException("Only one property can be set");
            }
        }

        private Enum? _value;
    }
}
