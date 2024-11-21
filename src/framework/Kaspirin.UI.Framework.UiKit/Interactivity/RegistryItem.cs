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

using Microsoft.Win32;

namespace Kaspirin.UI.Framework.UiKit.Interactivity
{
    public sealed class RegistryItem
    {
        public RegistryItem(string path)
        {
            Guard.ArgumentIsNotNull(path);

            Path = path;
        }

        public RegistryItem(string key, string value, RegistryValueKind registryValueKind)
        {
            Guard.ArgumentIsNotNull(key);
            Guard.ArgumentIsNotNull(value);

            Path = key;
            Value = value;
            IsKey = registryValueKind == RegistryValueKind.None ||
                    registryValueKind == RegistryValueKind.Unknown;
            IsValueValid = !IsKey;
        }

        public string Path { get; private set; }
        public string? Value { get; private set; }
        public bool IsValueValid { get; private set; }
        public bool IsKey { get; private set; }
    }
}
