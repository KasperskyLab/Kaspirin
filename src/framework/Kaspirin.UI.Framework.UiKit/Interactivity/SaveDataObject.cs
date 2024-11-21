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
using Kaspirin.UI.Framework.Mvvm;

namespace Kaspirin.UI.Framework.UiKit.Interactivity
{
    public class SaveDataObject : InteractionObject
    {
        public SaveDataObject(Action? onSave = null, Action? onDiscard = null, Action? onCancel = null)
        {
            _onSave = onSave;
            _onDiscard = onDiscard;
            _onCancel = onCancel;

            SaveCommand = new DelegateCommand(Save);
            DiscardCommand = new DelegateCommand(Discard);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public SaveDataDecision Decision { get; set; }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DiscardCommand { get; }
        public DelegateCommand CancelCommand { get; }

        protected virtual void OnSaveChanges()
        {

        }

        protected virtual void OnDiscardChanges()
        {

        }

        protected virtual void OnCancel()
        {

        }

        protected sealed override void OnDecided()
        {
            switch (Decision)
            {
                case SaveDataDecision.Stay:
                    OnCancel();
                    _onCancel?.Invoke();
                    break;
                case SaveDataDecision.Save:
                    OnSaveChanges();
                    _onSave?.Invoke();
                    break;
                case SaveDataDecision.Discard:
                    OnDiscardChanges();
                    _onDiscard?.Invoke();
                    break;
                default:
                    break;
            }
        }

        private void Cancel()
        {
            Decision = SaveDataDecision.Stay;
            Handle();
        }

        private void Discard()
        {
            Decision = SaveDataDecision.Discard;
            Handle();
        }

        private void Save()
        {
            Decision = SaveDataDecision.Save;
            Handle();
        }

        private readonly Action? _onSave;
        private readonly Action? _onDiscard;
        private readonly Action? _onCancel;
    }
}
