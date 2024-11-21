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
    public class SaveDataRequest<T> : InteractionRequestBase<T> where T : SaveDataObject
    {
        public void Raise(T saveDataObject, Action? onSave = null, Action? onDiscard = null, Action? onCancel = null)
        {
            Guard.ArgumentIsNotNull(saveDataObject);

            Raise(saveDataObject, c => onSave?.Invoke(), c => onDiscard?.Invoke(), c => onCancel?.Invoke());
        }

        public void Raise(T saveDataObject, Action<T>? onSave = null, Action<T>? onDiscard = null, Action<T>? onCancel = null)
        {
            Guard.ArgumentIsNotNull(saveDataObject);

            void OnDecided(SaveDataDecision result)
            {
                var action = result == SaveDataDecision.Save
                    ? onSave
                    : result == SaveDataDecision.Discard
                        ? onDiscard
                        : onCancel;

                action?.Invoke(saveDataObject);
            }

            Raise(saveDataObject, OnDecided);
        }

        public void Raise(T saveDataObject, Action<SaveDataDecision> onDecided)
        {
            Guard.ArgumentIsNotNull(saveDataObject);
            Guard.ArgumentIsNotNull(onDecided);

            InvokeInteraction(saveDataObject, result => onDecided(result.Decision));
        }

        protected override void OnClose()
        {
            Guard.IsNotNull(InteractionObject);

            InteractionObject.Decision = SaveDataDecision.Stay;
        }
    }
}
