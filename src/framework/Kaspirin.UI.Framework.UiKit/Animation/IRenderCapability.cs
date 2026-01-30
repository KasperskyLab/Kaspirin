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

using System;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.UiKit.Animation;

/// <summary>
///     Allows you to request the current rendering level for the associated object <see cref="Dispatcher" />
///     from the UI stream and register to receive notifications of changes.
/// </summary>
public interface IRenderCapability
{
    /// <summary>
    ///     Occurs when the rendering level for the Dispatcher object changes from the UI stream.
    /// </summary>
    event EventHandler TierChanged;

    /// <summary>
    ///     Gets a value indicating the rendering level of the UI stream.
    /// </summary>
    RenderCapabilityTier Tier { get; }
}