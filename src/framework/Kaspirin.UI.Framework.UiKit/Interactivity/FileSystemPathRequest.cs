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
    public sealed class FileSystemPathRequest : InteractionRequestBase<FileSystemPathObject>
    {
        public void Raise(Action<string> onSelected)
        {
            Guard.ArgumentIsNotNull(onSelected);

            RaiseCore(onSelectedPath: onSelected);
        }

        public void Raise(Action<string[]> onSelected)
        {
            Guard.ArgumentIsNotNull(onSelected);

            RaiseCore(onSelectedPaths: onSelected);
        }

        public void Raise(Action<FileSystemPathObject> onSelected)
        {
            Guard.ArgumentIsNotNull(onSelected);

            RaiseCore(onSelected: onSelected);
        }

        public void Raise(FileSystemPathObject fileSystemPathObject, Action<FileSystemPathObject> onSelected)
        {
            Guard.ArgumentIsNotNull(fileSystemPathObject);
            Guard.ArgumentIsNotNull(onSelected);

            RaiseCore(fileSystemPathObject: fileSystemPathObject, onSelected: onSelected);
        }

        private void RaiseCore(
            FileSystemPathObject? fileSystemPathObject = null,
            Action<string>? onSelectedPath = null,
            Action<string[]>? onSelectedPaths = null,
            Action<FileSystemPathObject>? onSelected = null)
        {
            var interactionObject = fileSystemPathObject ?? new FileSystemPathObject();

            void OnHandled(FileSystemPathObject pathObject)
            {
                if (pathObject.IsConfirmed)
                {
                    onSelectedPath?.Invoke(Guard.EnsureIsNotNull(pathObject.Path));
                    onSelectedPaths?.Invoke(Guard.EnsureIsNotNull(pathObject.Paths));
                    onSelected?.Invoke(pathObject);
                }
            }

            using (new DisableWow64FsRedirection(true))
            {
                InvokeInteraction(interactionObject, OnHandled);
            }
        }

        protected override void OnClose()
        {
            Guard.IsNotNull(InteractionObject);

            InteractionObject.IsConfirmed = false;
        }
    }
}
