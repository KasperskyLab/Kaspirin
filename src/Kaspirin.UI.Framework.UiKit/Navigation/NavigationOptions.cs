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

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class NavigationOptions
{
    /// <summary>
    ///     Specifies whether to leave this page in the region's cache after leaving it. The default value is <see langword="false" />.
    /// </summary>
    public bool KeepInCache { get; set; } = false;

    /// <summary>
    ///     Specifies whether to leave this page in the navigation history after leaving it. The default
    ///     value is <see langword="true" />.
    /// </summary>
    public bool KeepInHistory { get; set; } = true;

    /// <summary>
    ///     Indicates whether navigation from this page is allowed. By default, <see cref="NavigateFromPermission.Allow" />.
    /// </summary>
    public NavigateFromPermission CanNavigateFrom { get; set; } = NavigateFromPermission.Allow;

    /// <summary>
    ///     Delegate of the navigation confirmation request. Used for <see cref="CanNavigateFrom" /> = <see cref="NavigateFromPermission.Ask" />:
    ///     navigation is postponed and continues when the delegate calls the received continuation with
    ///     the value <see langword="true" />, or canceled when <see langword="false" />.
    /// </summary>
    public NavigationConfirmationCallback? ConfirmNavigation { get; set; }
}

