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

namespace Kaspirin.UI.Framework.UiKit.Interactivity;

public class InteractionObject : BaseViewModel
{
    public InteractionObject()
    {
        HandleCommand = new DelegateCommand(Handle);
    }

    public InteractionObject(string traceComponent)
        : base(traceComponent)
    {
        HandleCommand = new DelegateCommand(Handle);
    }

    public InteractionObject(ComponentTracer tracer)
        : base(tracer)
    {
        HandleCommand = new DelegateCommand(Handle);
    }

    public InteractionObject(ComponentTracerParameters tracerParameters)
        : base(tracerParameters)
    {
        HandleCommand = new DelegateCommand(Handle);
    }

    public event Action Handled = () => { };

    public event Action Decided = () => { };

    public DelegateCommand HandleCommand { get; }

    public bool IsDecided
    {
        get { return _isDecided; }
        private set
        {
            _isDecided = value;
            RaisePropertyChanged(nameof(IsDecided));
        }
    }

    public void Handle()
    {
        OnHandled();
        Handled.Invoke();

        if (_isDecided)
        {
            return;
        }

        IsDecided = true;

        OnDecided();
        Decided.Invoke();
    }

    public virtual object GetDataContext()
    {
        return this;
    }

    internal void InteractionStarted()
    {
        Tracer.TraceMethodCall();

        if (GetDataContext() is IInteractionAware interactionAware)
        {
            interactionAware.OnInteractionStarted(new(this));
        }

        OnInteractionStarted();
    }

    internal void InteractionCompleted()
    {
        Tracer.TraceMethodCall();

        if (GetDataContext() is IInteractionAware interactionAware)
        {
            interactionAware.OnInteractionCompleted();
        }

        OnInteractionCompleted();
    }

    protected virtual void OnInteractionStarted()
    {
    }

    protected virtual void OnInteractionCompleted()
    {
    }

    protected virtual void OnHandled()
    {
    }

    protected virtual void OnDecided()
    {
    }

    private bool _isDecided;
}
