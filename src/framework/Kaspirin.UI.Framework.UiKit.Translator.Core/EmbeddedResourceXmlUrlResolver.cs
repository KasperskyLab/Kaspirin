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
using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal sealed class EmbeddedResourceXmlUrlResolver : XmlUrlResolver
    {
        public EmbeddedResourceXmlUrlResolver(string resourcesDirectory, TaskLoggingHelper log)
        {
            _resourcesDirectory = resourcesDirectory;
            _log = log;
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            var message = $"Schema import resolve '{absoluteUri}'";

            if (absoluteUri.IsFile)
            {
                var schemaFilename = Path.GetFileName(absoluteUri.AbsolutePath);
                var schemaStream = EmbeddedResourceHelper.GetEmbeddedResource(_resourcesDirectory, schemaFilename);

                if (schemaStream != null)
                {
                    _log.LogMessage(MessageImportance.Normal, $"{message} [success]");
                    return schemaStream;
                }
            }

            _log.LogMessage(MessageImportance.High, $"{message} [fail]");
            return null;
        }

        private readonly string _resourcesDirectory;
        private readonly TaskLoggingHelper _log;
    }
}
