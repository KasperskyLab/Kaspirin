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

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Xaml
{
    public class ResExtension : LocalizationMarkupBase
    {
        public ResExtension() : this(string.Empty) { }

        public ResExtension(string key) : base(key) { }

        public ResExtension(string key, string scope) : base(key, scope) { }

        public bool? Freeze { get; set; } = true;

        protected override object? ProvideValue()
        {
            var resource = ProvideLocalizer<IXamlLocalizer>().GetResource(Key);

            if (resource is Freezable freezableObject)
            {
                if (Freeze is false)
                {
                    return freezableObject.Clone();
                }

                if (Freeze is true && freezableObject.CanFreeze && freezableObject.IsFrozen is false)
                {
                    freezableObject.Freeze();
                }
            }

            return resource;
        }

        protected override ILocalizer PrepareLocalizer()
        {
            return LocalizationManager.Current.LocalizerFactory.Resolve<IXamlLocalizer>(Scope);
        }
    }
}
