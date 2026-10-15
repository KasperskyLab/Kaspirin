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
///     Implements an event for delegates <see cref="EventHandler" /> that stores weak references of its handlers.
/// </summary>
public sealed class WeakEvent : BaseWeakEvent<EventHandler>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WeakEvent" /> class.
    /// </summary>
    internal WeakEvent()
    {
    }

    /// <summary>
    ///     Triggers an event.
    /// </summary>
    /// <param name="sender">
    ///     The sender of the event.
    /// </param>
    /// <param name="args">
    ///     Event arguments.
    /// </param>
    public void Invoke(object sender, EventArgs args)
    {
        InvokeCore(sender, args);
    }
}