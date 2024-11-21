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
    public class InteractionRequest<T> : InteractionRequestBase<T> where T : InteractionObject
    {
        public void Raise(T interactionObject)
        {
            Guard.ArgumentIsNotNull(interactionObject);

            Raise(interactionObject, _ => { });
        }

        public void Raise(T interactionObject, Action onHandled)
        {
            Guard.ArgumentIsNotNull(interactionObject);
            Guard.ArgumentIsNotNull(onHandled);

            Raise(interactionObject, o => onHandled.Invoke());
        }

        public void Raise(T interactionObject, Action<T> onHandled)
        {
            Guard.ArgumentIsNotNull(interactionObject);
            Guard.ArgumentIsNotNull(onHandled);

            InvokeInteraction(interactionObject, onHandled);
        }
    }
}
