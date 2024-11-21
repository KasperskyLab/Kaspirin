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

namespace Kaspirin.UI.Framework.UiKit.Navigation
{
    public sealed class NavigationOptionsBuilder
    {
        public NavigationOptions Build() => _instance;

        public void CopyFrom(NavigationOptions source)
        {
            _instance.CanNavigateFrom = source.CanNavigateFrom;
            _instance.KeepAlive = source.KeepAlive;
            _instance.SkipOnBackNavigation = source.SkipOnBackNavigation;
        }

        public void SetCanNavigateFrom(bool value) => _instance.CanNavigateFrom = value;

        public void SetKeepAlive(bool value) => _instance.KeepAlive = value;

        public void SetSkipOnBackNavigation(bool value) => _instance.SkipOnBackNavigation = value;

        private readonly NavigationOptions _instance = new();
    }
}