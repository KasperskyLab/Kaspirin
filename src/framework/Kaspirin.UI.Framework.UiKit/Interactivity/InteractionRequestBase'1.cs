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

namespace Kaspirin.UI.Framework.UiKit.Interactivity
{
    public abstract class InteractionRequestBase<T> : InteractionRequestBase, IInteractionRequest<T> where T : InteractionObject
    {
        public event EventHandler<InteractionRequestedEventArgs> TriggerActionRaised = (s, a) => { };

        public event EventHandler<InteractionRequestedEventArgs> Raised = (s, a) => { };

        public bool IsRaised { get; private set; }

        public T? InteractionObject { get; private set; }

        public void SetSilentMode()
        {
            _silentMode = true;

            _trace.TraceInformation("SilentMode is active");
        }

        public void Close()
        {
            if (InteractionObject != null)
            {
                OnClose();

                InteractionObject.Handle();
            }
        }

        public void CloseAll()
        {
            GetStorage().HandleAll();
        }

        protected void InvokeInteraction(T interactionObject)
        {
            Guard.ArgumentIsNotNull(interactionObject);

            InvokeInteraction(interactionObject, _ => { });
        }

        protected void InvokeInteraction(T interactionObject, Action<T> callback)
        {
            Guard.ArgumentIsNotNull(interactionObject);
            Guard.ArgumentIsNotNull(callback);

            if (IsRaised)
            {
                _trace.TraceWarning("Interaction suppressed because another interaction is active");
                return;
            }

            IsRaised = true;

            _trace.TraceInformation("Interaction started");

            _callback = callback;
            GetStorage().Add(interactionObject);

            InteractionObject = interactionObject;
            InteractionObject.Decided += OnDecided;

            var eventArgs = new InteractionRequestedEventArgs(InteractionObject);

            if (_silentMode is false)
            {
                _trace.TraceInformation("Raise TriggerAction event");
                TriggerActionRaised(this, eventArgs);
            }
            else
            {
                _trace.TraceInformation("TriggerAction event skipped");
            }

            Raised(this, eventArgs);
        }

        protected virtual void OnClose() { }

        private void OnDecided()
        {
            Guard.IsNotNull(InteractionObject);
            Guard.IsNotNull(_callback);

            GetStorage().Remove(InteractionObject);

            _callback.Invoke(InteractionObject);
            _callback = null;

            _silentMode = false;

            InteractionObject.Decided -= OnDecided;
            InteractionObject = null;

            IsRaised = false;

            _trace.TraceInformation("Interaction completed");
        }

        private IInteractionObjects GetStorage()
            => ServiceLocator.Instance.GetService<IInteractionObjects>();

        private bool _silentMode;
        private Action<T>? _callback;

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Interactivity);
    }
}
