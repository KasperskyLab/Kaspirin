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

    public class ConfirmationObject : InteractionObject
    {
        public ConfirmationObject()
        {
            ConfirmCommand = new DelegateCommand(Confirm, CanConfirm);
        }

        public ConfirmationObject(Action onConfirmed, Action? onCancelled = null) : this()
        {
            Guard.ArgumentIsNotNull(onConfirmed);

            _onConfirmed = onConfirmed;
            _onCancelled = onCancelled;
        }

        public bool IsConfirmed
        {
            get { return _isConfirmed; }
            set
            {
                _isConfirmed = value;
                RaisePropertyChanged(nameof(IsConfirmed));
            }
        }

        public DelegateCommand ConfirmCommand { get; private set; }

        public void Confirm()
        {
            IsConfirmed = true;
            Handle();
        }

        protected virtual bool CanConfirm()
        {
            return true;
        }

        protected virtual void OnConfirmed()
        {

        }

        protected virtual void OnCancelled()
        {

        }

        protected sealed override void OnDecided()
        {
            if (IsConfirmed)
            {
                OnConfirmed();
                _onConfirmed?.Invoke();
            }
            else
            {
                OnCancelled();
                _onCancelled?.Invoke();
            }
        }

        private bool _isConfirmed;
        private readonly Action? _onConfirmed;
        private readonly Action? _onCancelled;
    }
}
