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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Controls.Selectors
{
    [ContentProperty(nameof(DataTemplates))]
    public abstract class DataTemplateSelectorBase : DataTemplateSelector
    {
        public ResourceDictionary DataTemplates { get; set; } = new();

        public sealed override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            var key = GetDataTemplateKey(item);
            if (key == null)
            {
                return null;
            }

            if (DataTemplates.Contains(key))
            {
                return DataTemplates[key] as DataTemplate;
            }

            var stringKey = key.ToString();
            if (DataTemplates.Contains(stringKey))
            {
                return DataTemplates[stringKey] as DataTemplate;
            }

            return null;
        }

        protected abstract object? GetDataTemplateKey(object item);
    }
}