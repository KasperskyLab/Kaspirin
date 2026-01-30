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
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.UiKit.Interactivity;

public abstract class InteractionRequestBase<T> : InteractionRequestBase, IInteractionRequest<T> where T : InteractionObject
{
    protected InteractionRequestBase()
    {
        _trace = ComponentTracer.Get(UIKitComponentTracers.Interactivity, this);

        Raised += InvokeTriggerAction;
    }

    public event EventHandler<InteractionRequestedEventArgs>? TriggerActionRaised;

    public event EventHandler<InteractionRequestedEventArgs> Raised;

    public bool IsRaised { get; private set; }

    public T? InteractionObject { get; private set; }

    public Task<T>? InteractionTask { get; private set; }

    public bool SkipTriggerAction { get; set; } = false;

    public bool IsAsync { get; set; } = true;

    public void Close()
    {
        if (InteractionObject != null)
        {
            OnClose();

            InteractionObject.Handle();
        }
    }

    internal protected void InvokeInteraction(T interactionObject, Action<T> callback)
    {
        Guard.ArgumentIsNotNull(interactionObject);
        Guard.ArgumentIsNotNull(callback);

        if (IsRaised)
        {
            _trace.TraceWarning("Interaction suppressed because it is already active");
            return;
        }

        if (interactionObject.IsDecided)
        {
            _trace.TraceWarning("Interaction suppressed because InteractionObject is already handled");
            return;
        }

        IsRaised = true;

        _trace.TraceInformation("Interaction started");

        _callback = callback;
        GetStorage().Add(interactionObject);

        InteractionObject = interactionObject;
        InteractionObject.Decided += OnDecided;

        InteractionTask = CreateInteractionTask();

        if (IsAsync)
        {
            RaiseEvents();
        }
        else
        {
            _trace.TraceInformation("Interaction events raised in sync mode.");

            DispatcherFrameAction.Run(RaiseEvents);
        }
    }

    protected virtual void OnClose() { }

    private void OnDecided()
    {
        Guard.IsNotNull(InteractionObject);
        Guard.IsNotNull(InteractionTask);
        Guard.IsNotNull(_callback);
        Guard.IsNotNull(_interactionTaskCompletionSource);

        GetStorage().Remove(InteractionObject);

        _callback.Invoke(InteractionObject);
        _callback = null;

        _interactionFrameContext?.CloseFrame();
        _interactionFrameContext = null;

        _interactionTaskCompletionSource.SetResult(InteractionObject);
        _interactionTaskCompletionSource = null;

        IsRaised = false;

        InteractionObject.Decided -= OnDecided;
        InteractionObject = null;

        InteractionTask = null;

        _trace.TraceInformation("Interaction completed");
    }

    private void RaiseEvents(DispatcherFrameContext? frameContext = null)
    {
        Guard.IsNotNull(InteractionObject);

        _interactionFrameContext = frameContext;

        var eventArgs = new InteractionRequestedEventArgs(InteractionObject);

        Raised.Invoke(this, eventArgs);
    }

    private void InvokeTriggerAction(object? sender, InteractionRequestedEventArgs eventArgs)
    {
        if (TriggerActionRaised == null)
        {
            _trace.TraceWarning($"Skip {nameof(TriggerActionRaised)}. No handlers have been set for event.");
            return;
        }

        if (SkipTriggerAction)
        {
            _trace.TraceWarning($"Skip {nameof(TriggerActionRaised)}. {nameof(SkipTriggerAction)} is active.");
            return;
        }

        TriggerActionRaised.Invoke(sender, eventArgs);
    }

    private IInteractionObjects GetStorage()
        => ServiceLocator.GetService<IInteractionObjects>();

    private Task<T> CreateInteractionTask()
    {
        _interactionTaskCompletionSource = new TaskCompletionSource<T>();

        return _interactionTaskCompletionSource.Task;
    }

    private Action<T>? _callback;
    private TaskCompletionSource<T>? _interactionTaskCompletionSource;
    private DispatcherFrameContext? _interactionFrameContext;

    private readonly ComponentTracer _trace;
}
