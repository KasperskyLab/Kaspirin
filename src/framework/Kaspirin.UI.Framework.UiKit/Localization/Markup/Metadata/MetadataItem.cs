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

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Metadata
{
    public sealed class MetadataItem : ViewModelBase
    {
        public MetadataItem(string key, string scope, Type localizerType)
        {
            Key = Guard.EnsureArgumentIsNotNull(key);
            Scope = Guard.EnsureArgumentIsNotNull(scope);
            LocalizerType = Guard.EnsureArgumentIsNotNull(localizerType);
        }

        public MetadataItem Self => this;

        public string Key { get; }
        public string Scope { get; }
        public Type LocalizerType { get; }

        public void Update()
        {
            RaisePropertyChanged(nameof(Self));
        }
    }
}
