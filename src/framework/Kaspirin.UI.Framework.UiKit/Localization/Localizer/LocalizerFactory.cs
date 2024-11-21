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
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer
{
    public sealed class LocalizerFactory : LocalizerFactoryBase
    {
        public LocalizerFactory(LocalizerParameterFactory parameterFactory)
        {
            _parameterFactory = Guard.EnsureArgumentIsNotNull(parameterFactory);
        }

        protected override ILocalizer CreateLocalizer(string scope, Type type)
        {
            Guard.ArgumentIsNotNull(scope);
            Guard.ArgumentIsNotNull(scope);

            var parameters = _parameterFactory.Resolve(scope, type);

            if (type == typeof(IStringLocalizer))
            {
                var fallback = parameters.Scope.FallbackScope;

                return scope == fallback
                    ? new StringLocalizer(parameters)
                    : new StringLocalizer(parameters, (StringLocalizer)Resolve(fallback, typeof(IStringLocalizer)));
            }

            if (type == typeof(IXamlLocalizer))
            {
                return new XamlLocalizer(parameters);
            }

            if (type == typeof(IImageLocalizer))
            {
                return new ImageLocalizer(parameters);
            }

            if (type == typeof(IFileLocalizer))
            {
                return new FileLocalizer(parameters);
            }

            var localizerInterface = type.GetInterfaces().FirstOrDefault(i => typeof(ILocalizer).IsAssignableFrom(i) && i != typeof(ILocalizer));
            if (localizerInterface != null)
            {
                return CreateLocalizer(scope, localizerInterface);
            }

            throw new LocException($"Failed to create localizer. Unknown type {type}");
        }

        private readonly LocalizerParameterFactory _parameterFactory;
    }
}
