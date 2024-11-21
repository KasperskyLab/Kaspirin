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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions
{
    public sealed class EnumSourceExtension : MarkupExtension
    {
        public Type? Type
        {
            get => _enumType;
            set
            {
                Guard.ArgumentIsNotNull(value);
                Guard.Assert(value.IsEnum);

                _enumType = value;
                _enumValues = Enum.GetValues(_enumType).Cast<Enum>().ToArray();
            }
        }

        public object? Except { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Guard.IsNotNull(_enumType);
            Guard.IsNotNull(_enumValues);

            if (Except == null)
            {
                return _enumValues;
            }
            else
            {
                Enum[] exceptItems;

                if (Except is IEnumerable enumerable)
                {
                    exceptItems = enumerable.Cast<Enum>().Select(EnsureExpectedType).ToArray();
                }
                else if (Except is Enum enumExcept)
                {
                    exceptItems = new[] { EnsureExpectedType(enumExcept) };
                }
                else
                {
                    throw new InvalidOperationException($"Except must be of type Enum or IEnumerable.");
                }

                return _enumValues.Where(v => !exceptItems.Contains(v));
            }
        }

        private Enum EnsureExpectedType(Enum item)
        {
            var itemType = item.GetType();
            if (itemType != Type)
            {
                throw new InvalidOperationException($"Except value '{item}' has invalid type. Expected type - {Type}, actual type - {itemType}.");
            }

            return item;
        }

        private Type? _enumType;
        private IEnumerable<Enum>? _enumValues;
    }
}
