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

using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Metadata
{
    public static class MetadataHelper
    {
        public static MetadataItem? GetPropertyMetadata(DependencyObject element, DependencyProperty targetProperty)
        {
            var binding = BindingOperations.GetMultiBinding(element, targetProperty);
            return binding switch
            {
                null => null,
                _ => GetPropertyMetadata(binding)
            };
        }

        public static MetadataItem? GetPropertyMetadata(MultiBinding binding)
        {
            var sources = binding.Bindings.OfType<Binding>().Select(b => b.Source);

            var metaData = sources.OfType<MetadataItem>().FirstOrDefault();
            if (metaData != null)
            {
                return metaData;
            }

            var resourceDelivery = sources.OfType<ResourceDelivery>().FirstOrDefault();
            if (resourceDelivery != null)
            {
                return GetPropertyMetadata(resourceDelivery, ResourceDelivery.ResourceValueProperty);
            }

            return null;
        }
    }
}
