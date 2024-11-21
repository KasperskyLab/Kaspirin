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
    public sealed class NavigationOptions
    {
        /// <summary>
        ///     Specifies whether to leave this page in the region when performing reverse navigation from
        ///     this page. By default, <see langword="false" />.
        /// </summary>
        public bool KeepAlive { get; set; } = false;

        /// <summary>
        ///     Specifies whether to skip this page when performing reverse navigation. By default, <see langword="false" />.
        /// </summary>
        public bool SkipOnBackNavigation { get; set; } = false;

        /// <summary>
        ///     Indicates whether navigation from this page is allowed. By default, <see langword="true" />.
        /// </summary>
        public bool CanNavigateFrom { get; set; } = true;
    }
}