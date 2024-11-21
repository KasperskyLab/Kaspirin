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

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Converting
{
    public sealed class ResFromBoolExtension : ResFromSourceExtension
    {
        public object? True
        {
            get
            {
                return _resources.Collection.Contains(ResourceCollection.True)
                     ? _resources.Collection[ResourceCollection.True]
                     : null;
            }
            set
            {
                _resources.Collection[ResourceCollection.True] = value;
            }
        }

        public object? False
        {
            get
            {
                return _resources.Collection.Contains(ResourceCollection.False)
                     ? _resources.Collection[ResourceCollection.False]
                     : null;
            }
            set
            {
                _resources.Collection[ResourceCollection.False] = value;
            }
        }

        public override object? GetResource(object? key)
        {
            Guard.ArgumentIsNotNull(key);

            return _resources.GetResource(key);
        }

        private readonly ResFromCollectionExtension _resources = new();
    }
}
