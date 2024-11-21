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

namespace Kaspirin.UI.Framework.UiKit.Converters.DictionaryConverters
{
    public sealed class ResourceIcon : IDictionaryConverterItem
    {
        public UIKitIcon_12 Icon12 { get => _icon12; set => EnsureCanSet(ref _icon12, value); }
        public UIKitIcon_16 Icon16 { get => _icon16; set => EnsureCanSet(ref _icon16, value); }
        public UIKitIcon_24 Icon24 { get => _icon24; set => EnsureCanSet(ref _icon24, value); }
        public UIKitIcon_32 Icon32 { get => _icon32; set => EnsureCanSet(ref _icon32, value); }
        public UIKitIcon_48 Icon48 { get => _icon48; set => EnsureCanSet(ref _icon48, value); }

        public object? GetItemValue()
        {
            return _value;
        }

        private void EnsureCanSet<TValue>(ref TValue target, TValue value)
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

        private object? _value;

        private UIKitIcon_12 _icon12;
        private UIKitIcon_16 _icon16;
        private UIKitIcon_24 _icon24;
        private UIKitIcon_32 _icon32;
        private UIKitIcon_48 _icon48;
    }
}
