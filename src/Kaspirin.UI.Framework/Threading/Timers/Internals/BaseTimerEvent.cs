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

namespace Kaspirin.UI.Framework.Threading.Timers.Internals;

internal sealed partial class BaseTimerEvent
{
    public BaseTimerEvent(bool useStrongReference, Action? handlerCollected = null)
    {
        _strongRef = useStrongReference;
        _strongEvent = EmptyDelegate.For<Action<ITimer>>();

        _weakEvent = WeakEventFactory.CreateForAction<ITimer>();
        _weakEvent.HandlerCollected += handlerCollected;
    }

    public int HandlersCount => _strongRef ? _strongEvent.GetInvocationList().Length - 1 : _weakEvent.HandlersCount;

    public void Subscribe(Action<ITimer> handler)
    {
        if (_strongRef)
        {
            _strongEvent += handler;
        }
        else
        {
            _weakEvent.Subscribe(handler);
        }
    }

    public void Unsubscribe(Action<ITimer> handler)
    {
        if (_strongRef)
        {
            _strongEvent -= handler;
        }
        else
        {
            _weakEvent.Unsubscribe(handler);
        }
    }

    public void Invoke(ITimer sender)
    {
        if (_strongRef)
        {
            _strongEvent.DynamicInvoke(sender);
        }
        else
        {
            _weakEvent.Invoke(sender);
        }
    }

#pragma warning disable IDE1006 // Naming Styles
    private event Action<ITimer> _strongEvent = EmptyDelegate.For<Action<ITimer>>();
    private readonly WeakAction<ITimer> _weakEvent;
    private readonly bool _strongRef;
#pragma warning restore IDE1006 // Naming Styles
}
