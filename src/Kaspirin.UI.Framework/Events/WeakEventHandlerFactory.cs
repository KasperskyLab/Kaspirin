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

using System;

namespace Kaspirin.UI.Framework.Events;

/// <summary>
///     Factory of weak event handlers.
/// </summary>
/// <remarks>
///     Example of using a weak subscription handler:
///     <code>
///     someButton.Click += WeakEventHandlerFactory.Wrap(
///     eventHandler: ClickHandler,
///     cleanup: eh => someButton.Click -= eh);
///     </code>
/// </remarks>
public static class WeakEventHandlerFactory
{
    /// <summary>
    ///     Creates a delegate that stores a weak reference to <paramref name="handler" />.
    /// </summary>
    /// <typeparam name="TEventHandler">
    ///     The type of the event delegate.
    /// </typeparam>
    /// <param name="handler">
    ///     The delegate that runs on the event.
    /// </param>
    /// <param name="cleanup">
    ///     A delegate executed when a weak reference to <paramref name="handler" /> is collected by the garbage collector.
    /// </param>
    /// <remarks>
    ///     It cannot be used for static methods.
    ///     <br /> Checking the weak link of the event is performed every time the event is called, and if
    ///     the weak link is deleted, the delegate <paramref name="cleanup" /> is called in this case.
    /// </remarks>
    /// <returns>
    ///     The delegate that needs to be subscribed to the event.
    /// </returns>
    public static TEventHandler Wrap<TEventHandler>(TEventHandler handler, Action<TEventHandler>? cleanup = null)
        where TEventHandler : Delegate
    {
        return new BaseWeakEventHandler<TEventHandler>(handler, cleanup).ProxyHandler;
    }
}
