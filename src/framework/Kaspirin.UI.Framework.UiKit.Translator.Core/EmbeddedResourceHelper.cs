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
using System.IO;
using System.Linq;
using System.Reflection;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal static class EmbeddedResourceHelper
    {
        public static Stream GetEmbeddedResource(string resourceDirectory, string resourceName)
        {
            // Replace all slashes by dots.
            var resourcePath = resourceDirectory.Replace(Path.DirectorySeparatorChar, '.');

            var embeddedConfigurationProvider = new EmbeddedConfigurationProvider();
            if (!embeddedConfigurationProvider.TryGetValue(Const.RootNamespaceEmbeddedConfigurationKey, out var rootNamespace))
            {
                throw new Exception("failed to provide assembly root namespace");
            }

            var assembly = Assembly.GetExecutingAssembly();
            var expectedResourcePath = $"{rootNamespace}.{resourcePath}.{resourceName}";
            var resolvedResourcePath = assembly.GetManifestResourceNames()
                .FirstOrDefault(existingResourcePath =>
                    string.Equals(existingResourcePath, expectedResourcePath, StringComparison.InvariantCultureIgnoreCase));

            return resolvedResourcePath switch
            {
                null => null,
                _ => assembly.GetManifestResourceStream(resolvedResourcePath)
            };
        }
    }
}
