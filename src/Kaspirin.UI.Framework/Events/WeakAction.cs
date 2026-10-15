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

using System;

namespace Kaspirin.UI.Framework.Events;

/// <summary>
///     Implements an event for delegates <see cref="Action" /> that stores weak references of its handlers.
/// </summary>
public sealed class WeakAction : BaseWeakEvent<Action>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WeakAction" /> class.
    /// </summary>
    internal WeakAction()
    {
    }

    /// <summary>
    ///     Triggers an event.
    /// </summary>
    public void Invoke()
    {
        InvokeCore();
    }
}
