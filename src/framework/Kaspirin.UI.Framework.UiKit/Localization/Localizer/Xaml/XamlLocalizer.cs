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

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Xaml
{
    public sealed class XamlLocalizer : LocalizerBase, IXamlLocalizer
    {
        public XamlLocalizer(LocalizerParameters parameters) : base(parameters) { }

        #region IXamlLocalizer

        public object? GetResource(string key)
        {
            Guard.ArgumentIsNotNull(key);

            try
            {
                return GetValue<object>(key);
            }
            catch (Exception e)
            {
                e.ProcessAsLocalizerException($"failed to provide resource for key='{key}'.");
                return null;
            }
        }

        #endregion

        protected override IScope CreateScopeObject(Uri scopeUri)
        {
            return new XamlScope(scopeUri, ResourceProvider);
        }
    }
}
