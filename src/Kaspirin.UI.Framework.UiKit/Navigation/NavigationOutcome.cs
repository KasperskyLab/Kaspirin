// Copyright © 2026 AO Kaspersky Lab.
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

public enum NavigationOutcome
{
    /// <summary>
    ///     The navigation was completed successfully.
    /// </summary>
    Success,

    /// <summary>
    ///     Navigation canceled: prohibited by the options of the active page or rejected upon confirmation.
    /// </summary>
    Cancelled,

    /// <summary>
    ///     Navigation failed. The exception is available via <see cref="NavigationResult.Error" />.
    /// </summary>
    Failed,

    /// <summary>
    ///     Navigation was not required: the landing page is already active, or the goal is missing from the history.
    /// </summary>
    NoOp,
}
