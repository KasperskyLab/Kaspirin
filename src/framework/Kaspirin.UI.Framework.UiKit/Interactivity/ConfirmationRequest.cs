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

using Kaspirin.UI.Framework.UiKit.Interactivity.Internals;
using System;

namespace Kaspirin.UI.Framework.UiKit.Interactivity
{
    public sealed class ConfirmationRequest : ConfirmationRequest<ConfirmationObject>
    {
        public new void Raise(ConfirmationObject confirmationObject)
        {
            Guard.ArgumentIsNotNull(confirmationObject);

            base.Raise(confirmationObject);
        }

        public new void Raise(ConfirmationObject confirmationObject, Action? onConfirmed = null, Action? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(confirmationObject);

            base.Raise(confirmationObject, onConfirmed, onCancelled);
        }

        public new void Raise(ConfirmationObject confirmationObject, Action<ConfirmationObject>? onConfirmed = null, Action<ConfirmationObject>? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(confirmationObject);

            base.Raise(confirmationObject, onConfirmed, onCancelled);
        }

        public void Raise(Action onConfirmed, Action? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(onConfirmed);

            base.Raise(new ConfirmationObject(), onConfirmed, onCancelled);
        }

        public void Raise(object dataContext, Action onConfirmed, Action? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(dataContext);
            Guard.ArgumentIsNotNull(onConfirmed);

            var confirmationObject = new DataContextConfirmationObject(dataContext);

            base.Raise(confirmationObject, onConfirmed, onCancelled);
        }

        public void Raise(object dataContext, Action<ConfirmationObject> onConfirmed, Action<ConfirmationObject>? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(dataContext);
            Guard.ArgumentIsNotNull(onConfirmed);

            var confirmationObject = new DataContextConfirmationObject(dataContext);

            base.Raise(confirmationObject, onConfirmed, onCancelled);
        }
    }
}
