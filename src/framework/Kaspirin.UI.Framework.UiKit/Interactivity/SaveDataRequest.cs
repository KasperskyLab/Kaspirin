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
    public sealed class SaveDataRequest : SaveDataRequest<SaveDataObject>
    {
        public void Raise(Action<SaveDataDecision> onDecided)
        {
            Guard.ArgumentIsNotNull(onDecided);

            base.Raise(new SaveDataObject(), onDecided);
        }

        public new void Raise(SaveDataObject saveDataObject, Action<SaveDataDecision> onDecided)
        {
            Guard.ArgumentIsNotNull(saveDataObject);
            Guard.ArgumentIsNotNull(onDecided);

            base.Raise(saveDataObject, onDecided);
        }

        public new void Raise(SaveDataObject saveDataObject, Action? onSave = null, Action? onDiscard = null, Action? onCancel = null)
        {
            Guard.ArgumentIsNotNull(saveDataObject);

            base.Raise(saveDataObject, onSave, onDiscard, onCancel);
        }

        public new void Raise(SaveDataObject saveDataObject, Action<SaveDataObject>? onSave = null, Action<SaveDataObject>? onDiscard = null, Action<SaveDataObject>? onCancel = null)
        {
            Guard.ArgumentIsNotNull(saveDataObject);

            base.Raise(saveDataObject, onSave, onDiscard, onCancel);
        }

        public void Raise(Action onConfirmed, Action? onCancelled = null)
        {
            Guard.ArgumentIsNotNull(onConfirmed);

            base.Raise(new SaveDataObject(), onConfirmed, onCancelled);
        }

        public void Raise(object dataContext, Action onSave, Action? onDiscard = null, Action? onCancel = null)
        {
            Guard.ArgumentIsNotNull(dataContext);
            Guard.ArgumentIsNotNull(onSave);

            var saveDataObject = new DataContextSaveDataObject(dataContext);

            Raise(saveDataObject, onSave, onDiscard, onCancel);
        }
    }
}
