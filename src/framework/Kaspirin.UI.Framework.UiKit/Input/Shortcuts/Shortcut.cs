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

using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Input.Shortcuts
{
    public sealed class Shortcut
    {
        public Shortcut()
        {
        }

        public Shortcut(Key key, ModifierKeys modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public Key Key { get; set; }

        public ModifierKeys Modifiers { get; set; }

        public KeyGesture ToKeyGesture()
            => new(Key, Modifiers);

        public bool IsEquivalentTo(KeyGesture gesture)
        {
            Guard.ArgumentIsNotNull(gesture);

            return gesture.Key == Key && gesture.Modifiers == Modifiers;
        }

        public override string ToString()
            => $"Key: {Key}, Modifiers: {Modifiers}";
    }
}
