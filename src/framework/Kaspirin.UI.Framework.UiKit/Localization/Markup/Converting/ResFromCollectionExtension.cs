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
    public class ResFromCollectionExtension : ResFromSourceExtension
    {
        public ResourceCollection Collection
        {
            get => _resourceCollection ??= new ResourceCollection();
            set => _resourceCollection = value;
        }

        public override object? GetResource(object? key)
        {
            var resourceItem = GetResourceItem(key);

            if (resourceItem is LocExtension locMarkupItem)
            {
                TrySetParamsToLocExtension(locMarkupItem);
                return locMarkupItem;
            }

            if (resourceItem is ResFromBoolExtension resFromBoolMarkupItem)
            {
                TrySetParamsToLocExtension(resFromBoolMarkupItem.True as LocExtension);
                TrySetParamsToLocExtension(resFromBoolMarkupItem.False as LocExtension);
                return resFromBoolMarkupItem;
            }

            if (resourceItem is ResFromCollectionExtension resFromCollection)
            {
                if (resFromCollection.Collection.ParamsCollection == null)
                {
                    resFromCollection.Collection.ParamsCollection = Collection.ParamsCollection;
                }
            }

            return resourceItem;
        }

        private void TrySetParamsToLocExtension(LocExtension? locExtension)
        {
            if (locExtension == null || Collection.ParamsCollection == null)
            {
                return;
            }

            if (locExtension.Params != null)
            {
                return;
            }

            locExtension.Params = Collection.ParamsCollection;
        }

        private object? GetResourceItem(object? key) => Collection.GetResourceItem(key);

        private ResourceCollection? _resourceCollection;
    }
}
