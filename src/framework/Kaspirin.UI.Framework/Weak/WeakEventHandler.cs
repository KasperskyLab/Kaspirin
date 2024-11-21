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

#pragma warning disable  CA1416 // This call site is reachable on all platforms

using System;
using System.ComponentModel;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.Weak
{
    /// <summary>
    ///     Creates a weak subscription to the event.
    /// </summary>
    /// <remarks>
    ///     Example of using a weak subscription:
    ///     <code>
    ///     someButton.Click += WeakEventHandler.Wrap(o.ClickHandler, eh => someButton.Click -= eh);
    ///     </code>
    /// </remarks>
    public static class WeakEventHandler
    {
        /// <summary>
        ///     Creates a delegate that stores a weak reference to <paramref name="eh" />.
        /// </summary>
        /// <param name="eh">
        ///     The delegate that runs on the event.
        /// </param>
        /// <param name="cleanup">
        ///     A delegate that executes when a weak reference to <paramref name="eh" /> is deleted.
        /// </param>
        /// <remarks>
        ///     It cannot be used for static methods.<br /> The weak link of the event is checked every time
        ///     the event is called, and if the weak link is deleted, then the <paramref name="cleanup" />
        ///     delegate is called in this case.
        /// </remarks>
        /// <returns>
        ///     The delegate that needs to be subscribed to the event.
        /// </returns>
        public static Action Wrap(Action eh, Action<Action> cleanup)
        {
            return WeakEventHandlerFactory<Action>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static Action<TArg1> Wrap<TArg1>(Action<TArg1> eh, Action<Action<TArg1>> cleanup)
        {
            return WeakEventHandlerFactory<Action<TArg1>>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static Action<TArg1, TArg2> Wrap<TArg1, TArg2>(Action<TArg1, TArg2> eh, Action<Action<TArg1, TArg2>> cleanup)
        {
            return WeakEventHandlerFactory<Action<TArg1, TArg2>>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static Action<TArg1, TArg2, TArg3> Wrap<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> eh, Action<Action<TArg1, TArg2, TArg3>> cleanup)
        {
            return WeakEventHandlerFactory<Action<TArg1, TArg2, TArg3>>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static TEventHandler Wrap<TEventHandler>(TEventHandler eh, Action<TEventHandler> cleanup) where TEventHandler : Delegate
        {
            return WeakEventHandlerFactory<TEventHandler>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static EventHandler Wrap(EventHandler eh, Action<EventHandler> cleanup)
        {
            return WeakEventHandlerFactory<EventHandler>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static EventHandler<TEventArgs> Wrap<TEventArgs>(EventHandler<TEventArgs> eh, Action<EventHandler<TEventArgs>> cleanup) where TEventArgs : EventArgs
        {
            return WeakEventHandlerFactory<EventHandler<TEventArgs>>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static SessionEndingEventHandler Wrap(SessionEndingEventHandler eh, Action<SessionEndingEventHandler> cleanup)
        {
            return WeakEventHandlerFactory<SessionEndingEventHandler>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static PropertyChangedEventHandler Wrap(PropertyChangedEventHandler eh, Action<PropertyChangedEventHandler> cleanup)
        {
            return WeakEventHandlerFactory<PropertyChangedEventHandler>.Create(eh, cleanup);
        }

        /// <inheritdoc  cref="Wrap(Action, Action{Action})"/>
        public static CancelEventHandler Wrap(CancelEventHandler eh, Action<CancelEventHandler> cleanup)
        {
            return WeakEventHandlerFactory<CancelEventHandler>.Create(eh, cleanup);
        }
    }
}
