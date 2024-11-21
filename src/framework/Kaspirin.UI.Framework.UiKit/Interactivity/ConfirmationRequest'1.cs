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
    public class ConfirmationRequest<T> : InteractionRequestBase<T> where T : ConfirmationObject
    {
        public void Raise(T confirmationObject)
        {
            Guard.ArgumentIsNotNull(confirmationObject);

            Raise(confirmationObject, _ => { });
        }

        public void Raise(T confirmationObject, Action? onConfirmed = null, Action? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(confirmationObject);

            Raise(confirmationObject, c => onConfirmed?.Invoke(), c => onCancelled?.Invoke());
        }

        public void Raise(T confirmationObject, Action<T>? onConfirmed = null, Action<T>? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(confirmationObject);

            void OnHandled(T result)
            {
                var action = result.IsConfirmed
                    ? onConfirmed
                    : onCancelled;

                action?.Invoke(confirmationObject);
            }

            InvokeInteraction(confirmationObject, OnHandled);
        }

        protected override void OnClose()
        {
            Guard.IsNotNull(InteractionObject);

            InteractionObject.IsConfirmed = false;
        }
    }
}
