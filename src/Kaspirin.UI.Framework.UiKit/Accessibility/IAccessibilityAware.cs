// Copyright © 2025 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Accessibility;

/// <summary>
///     An interface for components that support accessibility features.
/// </summary>
public interface IAccessibilityAware
{
    /// <summary>
    ///     Verifies that the accessibility requirements for the component are met.
    /// </summary>
    /// <remarks>
    ///     This method is used to enable components to verify compliance with accessibility requirements.
    ///     For example: compatibility with screen readers, support for navigation and keyboard interaction,
    ///     component operation in a contrasting theme, processing system text size increase, etc.
    /// </remarks>
    /// <returns>
    ///     Returns <see langword="true" /> if the component meets all its accessibility requirements,
    ///     otherwise - <see langword="false" />.
    /// </returns>
    bool Validate();
}
