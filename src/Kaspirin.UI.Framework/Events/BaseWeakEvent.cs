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
using System.Collections.Generic;
using System.Linq;

namespace Kaspirin.UI.Framework.Events;

/// <summary>
///     Implements the logic of an event that stores weak references of its handlers.
/// </summary>
/// <typeparam name="TEventHandler">
///     The type of the event delegate.
/// </typeparam>
public class BaseWeakEvent<TEventHandler> where TEventHandler : Delegate
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseWeakEvent{TEventHandler}" /> class.
    /// </summary>
    protected BaseWeakEvent()
    {
    }

    /// <summary>
    ///     The event that is called during garbage collection of the event handler.
    /// </summary>
    /// <remarks>
    ///     The event is called after the <see cref="InvokeCore" /> method if handlers that have already
    ///     been collected by the garbage collector are found among the event handlers.
    /// </remarks>
    public event Action HandlerCollected = EmptyDelegate.For<Action>();

    /// <summary>
    ///     The number of event handlers.
    /// </summary>
    public int HandlersCount
    {
        get
        {
            lock (_sync)
            {
                return _handlers.Count(h => h.IsAlive);
            }
        }
    }

    /// <summary>
    ///     Signs the event handler.
    /// </summary>
    /// <param name="handler">
    ///     The event handler.
    /// </param>
    public void Subscribe(TEventHandler handler)
    {
        var weakHandler = new BaseWeakEventHandler<TEventHandler>(handler);

        lock (_sync)
        {
            _handlers.Add(weakHandler);
        }
    }

    /// <summary>
    ///     Unsubscribes the event handler.
    /// </summary>
    /// <param name="handler">
    ///     The event handler.
    /// </param>
    public void Unsubscribe(TEventHandler handler)
    {
        lock (_sync)
        {
            _handlers.RemoveAll(h => h.Matches(handler));
        }
    }

    /// <summary>
    ///     Unsubscribes all event handlers.
    /// </summary>
    public void Clear()
    {
        lock (_sync)
        {
            _handlers.Clear();
        }
    }

    /// <summary>
    ///     Triggers an event.
    /// </summary>
    /// <param name="args">
    ///     Event arguments.
    /// </param>
    protected void InvokeCore(params object?[] args)
    {
        bool handlersRemoved;
        BaseWeakEventHandler<TEventHandler>[] handlers;

        lock (_sync)
        {
            handlersRemoved = false;
            handlers = _handlers.ToArray();
        }

        foreach (var handler in handlers)
        {
            if (handler.Invoke(args) is false)
            {
                lock (_sync)
                {
                    handlersRemoved = true;
                    _handlers.Remove(handler);
                }
            }
        }

        if (handlersRemoved)
        {
            HandlerCollected.Invoke();
        }
    }

    private readonly object _sync = new();
    private readonly List<BaseWeakEventHandler<TEventHandler>> _handlers = new();
}
