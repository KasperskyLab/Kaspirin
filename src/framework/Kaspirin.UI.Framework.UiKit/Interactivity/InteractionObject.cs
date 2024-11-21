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
    public class InteractionObject : ViewModelBase
    {
        public InteractionObject()
        {
            HandleCommand = new DelegateCommand(Handle);
        }

        public event Action PreviewDecided = () => { };

        public event Action Decided = () => { };

        public DelegateCommand HandleCommand { get; private set; }

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
            OnHandle();

            if (_isDecided)
            {
                return;
            }

            OnPreviewDecided();
            PreviewDecided.Invoke();

            IsDecided = true;

            OnDecided();
            Decided.Invoke();
        }

        public virtual object GetDataContext()
        {
            return this;
        }

        protected virtual void OnPreviewDecided()
        {

        }

        protected virtual void OnDecided()
        {

        }

        protected virtual void OnHandle()
        {

        }

        private bool _isDecided;
    }
}
