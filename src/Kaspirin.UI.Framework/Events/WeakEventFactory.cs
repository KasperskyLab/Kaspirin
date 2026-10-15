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
///     The factory of weak events.
/// </summary>
/// <remarks>
///     Example of organizing a weak event:
///     <code>
///     public event EventHandler SomeEvent
///     {
///     add => _someEvent.Subscribe(value);
///     remove => _someEvent.Unsubscribe(value);
///     }
/// <translated />
///     private void OnSomeEvent()
///     {
///     _someEvent.Invoke(this, EventArgs.Empty);
///     }
/// <translated />
///     private readonly WeakEvent _someEvent = WeakEventFactory.CreateForEventHandler();
///     </code>
/// </remarks>
public static class WeakEventFactory
{
    /// <summary>
    ///     Creates a weak event for delegates of type <see cref="EventHandler" />.
    /// </summary>
    /// <returns>
    ///     An instance of a weak event for delegates of type <see cref="EventHandler" />.
    /// </returns>
    public static WeakEvent CreateForEventHandler()
        => new();

    /// <summary>
    ///     Creates a weak event for delegates of type <see cref="EventHandler{TEventArgs}" />.
    /// </summary>
    /// <returns>
    ///     An instance of a weak event for delegates of type <see cref="EventHandler{TEventArgs}" />.
    /// </returns>
    public static WeakEvent<TEventArgs> CreateForEventHandler<TEventArgs>() where TEventArgs : EventArgs
        => new();

    /// <summary>
    ///     Creates a weak event for delegates of type <see cref="Action" />.
    /// </summary>
    /// <returns>
    ///     An instance of a weak event for delegates of type <see cref="Action" />.
    /// </returns>
    public static WeakAction CreateForAction()
        => new();

    /// <summary>
    ///     Creates a weak event for delegates of type <see cref="Action{TArg}" />.
    /// </summary>
    /// <returns>
    ///     An instance of a weak event for delegates of type <see cref="Action{TArg}" />.
    /// </returns>
    public static WeakAction<TArg> CreateForAction<TArg>()
        => new();

    /// <summary>
    ///     Creates a weak event for delegates of type <see cref="Action{TArg1, TArg2}" />.
    /// </summary>
    /// <returns>
    ///     An instance of a weak event for delegates of type <see cref="Action{TArg1, TArg2}" />.
    /// </returns>
    public static WeakAction<TArg1, TArg2> CreateForAction<TArg1, TArg2>()
        => new();

    /// <summary>
    ///     Creates a weak event for delegates of type <see cref="Action{TArg1, TArg2, TArg3}" />.
    /// </summary>
    /// <returns>
    ///     An instance of a weak event for delegates of type <see cref="Action{TArg1, TArg2, TArg3}" />.
    /// </returns>
    public static WeakAction<TArg1, TArg2, TArg3> CreateForAction<TArg1, TArg2, TArg3>()
        => new();

    /// <summary>
    ///     Creates a weak event for delegates of type <see cref="Action{TArg1, TArg2, TArg3, TArg4}" />.
    /// </summary>
    /// <returns>
    ///     An instance of a weak event for delegates of type <see cref="Action{TArg1, TArg2, TArg3, TArg4}" />.
    /// </returns>
    public static WeakAction<TArg1, TArg2, TArg3, TArg4> CreateForAction<TArg1, TArg2, TArg3, TArg4>()
        => new();
}
