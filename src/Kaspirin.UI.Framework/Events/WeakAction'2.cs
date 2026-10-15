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
///     Implements an event for delegates <see cref="Action{T,T}" /> that stores weak references of its handlers.
/// </summary>
public sealed class WeakAction<TArg1, TArg2> : BaseWeakEvent<Action<TArg1, TArg2>>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WeakAction{T,T}" /> class.
    /// </summary>
    internal WeakAction()
    {
    }

    /// <summary>
    ///     Triggers an event.
    /// </summary>
    public void Invoke(TArg1 arg1, TArg2 arg2)
        => InvokeCore(arg1, arg2);
}