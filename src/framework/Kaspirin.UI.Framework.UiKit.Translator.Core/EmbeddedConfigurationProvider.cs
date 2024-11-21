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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal sealed class EmbeddedConfigurationProvider
    {
        public EmbeddedConfigurationProvider()
        {
            _configuration = new Dictionary<string, string>();

            var attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(EmbeddedConfigurationAttribute), false)
                .OfType<EmbeddedConfigurationAttribute>();

            foreach (var attribute in attributes)
            {
                if (attribute.Key == null)
                {
                    continue;
                }

                _configuration.Add(attribute.Key, attribute.Value);
            }
        }

        public bool TryGetValue(string key, out string value) => _configuration.TryGetValue(key, out value);

        private readonly Dictionary<string, string> _configuration;
    }
}
